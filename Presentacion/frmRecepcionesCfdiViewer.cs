using ClosedXML.Excel;
using Models;
using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion
{
    public partial class frmRecepcionesCfdiViewer : Form
    {
        private readonly string[] _archiveExtensions = new[] { ".zip", ".rar" };

        private List<ReportItem> _reports = new List<ReportItem>();

        private DataTable _dtCfdisFull;
        private DataView _dvCfdis;

        private class ReportItem
        {
            public string SourceFile { get; set; }
            public EDSReport Report { get; set; }
        }

        public frmRecepcionesCfdiViewer()
        {
            InitializeComponent();
        }

        private void frmRecepcionesCfdiViewer_Load(object sender, EventArgs e)
        {
            SetupGrid(dgvCfdis);

            progressBar.Style = ProgressBarStyle.Blocks;
            lblStatus.Text = "Listo.";

            btnExport.Enabled = false;

            cboClaveSubProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboClaveSubProducto.Items.Clear();
            cboClaveSubProducto.Items.Add("(TODOS)");
            cboClaveSubProducto.SelectedIndex = 0;

            txtUuidFilter.TextChanged += (s, ev) => ApplyFilters();
            cboClaveSubProducto.SelectedIndexChanged += (s, ev) => ApplyFilters();
        }

        private void SetupGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Selecciona la carpeta que contiene .json o .zip/.rar";
                dlg.ShowNewFolderButton = false;

                if (dlg.ShowDialog() == DialogResult.OK)
                    txtFolder.Text = dlg.SelectedPath;
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            var folder = (txtFolder.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
            {
                MessageBox.Show("Selecciona una carpeta válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnBrowse.Enabled = false;
            btnProcess.Enabled = false;
            btnExport.Enabled = false;

            dgvCfdis.DataSource = null;
            _dtCfdisFull = null;
            _dvCfdis = null;
            _reports = new List<ReportItem>();

            cboClaveSubProducto.Items.Clear();
            cboClaveSubProducto.Items.Add("(TODOS)");
            cboClaveSubProducto.SelectedIndex = 0;

            txtUuidFilter.Text = "";

            progressBar.Style = ProgressBarStyle.Marquee;
            lblStatus.Text = "Procesando...";

            try
            {
                var result = await System.Threading.Tasks.Task.Run(() => LoadAll(folder));

                _reports = result;
                _dtCfdisFull = BuildRecepcionesCfdisTable(_reports);
                _dvCfdis = new DataView(_dtCfdisFull);

                dgvCfdis.DataSource = _dvCfdis;

                FillClaveSubProductoCombo(_dtCfdisFull);

                btnExport.Enabled = (_dtCfdisFull != null && _dtCfdisFull.Rows.Count > 0);

                lblStatus.Text = $"Listo. CFDIs recepciones: {(_dtCfdisFull?.Rows.Count ?? 0):N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error.";
            }
            finally
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                btnBrowse.Enabled = true;
                btnProcess.Enabled = true;
            }
        }

        private void FillClaveSubProductoCombo(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            var keys = dt.AsEnumerable()
                .Select(r => (r["ClaveSubProducto"] ?? "").ToString().Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();

            cboClaveSubProducto.Items.Clear();
            cboClaveSubProducto.Items.Add("(TODOS)");
            for (int i = 0; i < keys.Count; i++)
                cboClaveSubProducto.Items.Add(keys[i]);

            cboClaveSubProducto.SelectedIndex = 0;
        }

        private void ApplyFilters()
        {
            if (_dvCfdis == null) return;

            var filters = new List<string>();

            // filtro por ClaveSubProducto
            var selected = cboClaveSubProducto.SelectedItem != null ? cboClaveSubProducto.SelectedItem.ToString() : "(TODOS)";
            if (!string.IsNullOrWhiteSpace(selected) && !selected.Equals("(TODOS)", StringComparison.OrdinalIgnoreCase))
            {
                var safe = selected.Replace("'", "''");
                filters.Add("ClaveSubProducto = '" + safe + "'");
            }

            // filtro por UUID contiene
            var uuid = (txtUuidFilter.Text ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(uuid))
            {
                var safe = uuid.Replace("'", "''");
                // LIKE para buscar por contiene (case-insensitive depende de DataTable; normalmente funciona bien)
                filters.Add("CFDI LIKE '%" + safe + "%'");
            }

            _dvCfdis.RowFilter = string.Join(" AND ", filters);

            lblStatus.Text = $"Mostrando: {_dvCfdis.Count:N0} / {(_dtCfdisFull?.Rows.Count ?? 0):N0}";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUuidFilter.Text = "";
            cboClaveSubProducto.SelectedIndex = 0;
            ApplyFilters();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_dtCfdisFull == null || _dtCfdisFull.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable toExport;
            if (_dvCfdis != null && (!string.IsNullOrWhiteSpace(_dvCfdis.RowFilter)))
                toExport = _dvCfdis.ToTable();
            else
                toExport = _dtCfdisFull;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = "Recepciones_CFDIs.xlsx";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                using (var wb = new XLWorkbook())
                {
                    AddWorksheetFromTable(wb, "RecepcionesCFDIs", toExport);
                    wb.SaveAs(sfd.FileName);
                }

                MessageBox.Show("Exportado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddWorksheetFromTable(XLWorkbook wb, string sheetName, DataTable table)
        {
            var ws = wb.Worksheets.Add(sheetName);

            for (int c = 0; c < table.Columns.Count; c++)
                ws.Cell(1, c + 1).SetValue(table.Columns[c].ColumnName);

            for (int r = 0; r < table.Rows.Count; r++)
            {
                for (int c = 0; c < table.Columns.Count; c++)
                {
                    var col = table.Columns[c];
                    var value = table.Rows[r][c];
                    SetCellValue(ws.Cell(r + 2, c + 1), value, col.DataType);
                }
            }

            ws.Columns().AdjustToContents();
        }

        private static void SetCellValue(IXLCell cell, object value, Type dataType)
        {
            if (value == null || value == DBNull.Value)
            {
                cell.SetValue(string.Empty);
                return;
            }

            if (dataType == typeof(int) || dataType == typeof(long) || dataType == typeof(short))
            {
                cell.SetValue(Convert.ToInt64(value));
                return;
            }

            if (dataType == typeof(decimal) || dataType == typeof(double) || dataType == typeof(float))
            {
                cell.SetValue(Convert.ToDecimal(value));
                return;
            }

            if (dataType == typeof(bool))
            {
                cell.SetValue(Convert.ToBoolean(value));
                return;
            }

            if (dataType == typeof(DateTime))
            {
                cell.SetValue(Convert.ToDateTime(value));
                return;
            }

            cell.SetValue(value.ToString());
        }

        // =========================
        // PIPELINE: Collect JSON -> Load Reports
        // =========================
        private List<ReportItem> LoadAll(string rootFolder)
        {
            var tempRoot = Path.Combine(Path.GetTempPath(), "JX", "RXV", Guid.NewGuid().ToString("N").Substring(0, 8));
            Directory.CreateDirectory(tempRoot);

            try
            {
                var jsonFiles = CollectJsonFilesRecursive(rootFolder, tempRoot);
                if (jsonFiles.Count == 0)
                    throw new InvalidOperationException("No se encontró ningún .json (ni dentro de .zip/.rar).");

                if (chkStopOnFirstJson.Checked)
                    jsonFiles = jsonFiles.Take(1).ToList();

                var reports = LoadReports(jsonFiles);
                if (reports.Count == 0)
                    throw new InvalidOperationException("Se encontraron .json pero ninguno deserializó a EDSReport (revisa el modelo).");

                return reports;
            }
            finally
            {
                try { Directory.Delete(tempRoot, true); } catch { }
            }
        }

        private List<ReportItem> LoadReports(List<string> jsonFiles)
        {
            var list = new List<ReportItem>();

            for (int i = 0; i < jsonFiles.Count; i++)
            {
                var jsonPath = jsonFiles[i];
                try
                {
                    var report = DeserializeTyped(jsonPath);
                    if (report != null)
                    {
                        list.Add(new ReportItem
                        {
                            SourceFile = jsonPath,
                            Report = report
                        });
                    }
                }
                catch
                {
                }
            }

            return list;
        }

        private EDSReport DeserializeTyped(string jsonPath)
        {
            var json = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<EDSReport>(json, options);
        }

        // =========================
        // ✅ DATA: todos los CFDIs de recepciones
        // =========================
        private DataTable BuildRecepcionesCfdisTable(List<ReportItem> items)
        {
            var dt = new DataTable();

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("Archivo", typeof(string));
            dt.Columns.Add("ClaveSubProducto", typeof(string));
            dt.Columns.Add("ClaveProducto", typeof(string));

            dt.Columns.Add("NombreCliente", typeof(string));
            dt.Columns.Add("RfcClienteOProveedor", typeof(string));

            dt.Columns.Add("CFDI", typeof(string));
            dt.Columns.Add("FechaYHora", typeof(string));
            dt.Columns.Add("PrecioCompra", typeof(decimal));
            dt.Columns.Add("PrecioVentaPublico", typeof(decimal));
            dt.Columns.Add("ValorNumerico", typeof(decimal));

            // (opcional) para trazar rápido
            dt.Columns.Add("ProductoIndex", typeof(int));
            dt.Columns.Add("TanqueIndex", typeof(int));
            dt.Columns.Add("RecepcionIndex", typeof(int));
            dt.Columns.Add("NacionalIndex", typeof(int));
            dt.Columns.Add("CfdiIndex", typeof(int));

            int globalRow = 0;

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var r = item.Report;
                if (r == null || r.Producto == null) continue;

                var fileName = item.SourceFile != null ? Path.GetFileName(item.SourceFile) : "(sin_archivo)";

                for (int pIndex = 0; pIndex < r.Producto.Count; pIndex++)
                {
                    var p = r.Producto[pIndex];
                    if (p == null || p.Tanque == null) continue;

                    for (int tIndex = 0; tIndex < p.Tanque.Count; tIndex++)
                    {
                        var t = p.Tanque[tIndex];
                        if (t == null || t.Recepciones == null || t.Recepciones.Recepcion == null) continue;

                        for (int rx = 0; rx < t.Recepciones.Recepcion.Count; rx++)
                        {
                            var rec = t.Recepciones.Recepcion[rx];
                            if (rec == null || rec.Complemento == null || rec.Complemento.Nacional == null) continue;

                            for (int nIndex = 0; nIndex < rec.Complemento.Nacional.Count; nIndex++)
                            {
                                var nac = rec.Complemento.Nacional[nIndex];
                                if (nac == null || nac.CFDIs == null) continue;

                                for (int cIndex = 0; cIndex < nac.CFDIs.Count; cIndex++)
                                {
                                    var cfdi = nac.CFDIs[cIndex];
                                    if (cfdi == null) continue;

                                    globalRow++;

                                    var row = dt.NewRow();
                                    row["#"] = globalRow;

                                    row["Archivo"] = fileName;
                                    row["ClaveSubProducto"] = p.ClaveSubProducto ?? "";
                                    row["ClaveProducto"] = p.ClaveProducto ?? "";

                                    row["NombreCliente"] = nac.NombreClienteOProveedor ?? "";
                                    row["RfcClienteOProveedor"] = nac.RfcClienteOProveedor ?? "";

                                    row["CFDI"] = cfdi.Cfdi ?? "";

                                    row["FechaYHora"] = cfdi.FechaYHoraTransaccion.HasValue
                                        ? cfdi.FechaYHoraTransaccion.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                        : "";

                                    row["PrecioCompra"] = cfdi.PrecioCompra ?? 0m;
                                    row["PrecioVentaPublico"] = cfdi.PrecioDeVentaAlPublico ?? 0m;

                                    decimal vol = 0m;
                                    if (cfdi.VolumenDocumentado != null)
                                        vol = cfdi.VolumenDocumentado.ValorNumerico;
                                    row["ValorNumerico"] = vol;

                                    row["ProductoIndex"] = pIndex;
                                    row["TanqueIndex"] = tIndex;
                                    row["RecepcionIndex"] = rx;
                                    row["NacionalIndex"] = nIndex;
                                    row["CfdiIndex"] = cIndex;

                                    dt.Rows.Add(row);
                                }
                            }
                        }
                    }
                }
            }

            return dt;
        }

        // =========================
        // Collect JSON (sin duplicados)
        // =========================
        private List<string> CollectJsonFilesRecursive(string rootFolder, string tempRoot)
        {
            var foundJson = new List<string>();
            var foundSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var visitedArchives = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var visitedFolders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            int archiveIndex = 0;

            Action<string> walk = null;
            walk = (path) =>
            {
                if (chkStopOnFirstJson.Checked && foundJson.Count > 0) return;

                string fullPath;
                try { fullPath = Path.GetFullPath(path); }
                catch { return; }

                if (!visitedFolders.Add(fullPath))
                    return;

                List<string> files;
                try { files = SafeEnumerateFiles(path).ToList(); }
                catch { files = new List<string>(); }

                for (int i = 0; i < files.Count; i++)
                {
                    if (chkStopOnFirstJson.Checked && foundJson.Count > 0) return;

                    var f = files[i];
                    var ext = Path.GetExtension(f);

                    if (ext.Equals(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        if (foundSet.Add(f)) foundJson.Add(f);
                        if (chkStopOnFirstJson.Checked) return;
                    }
                    else if (IsArchive(f))
                    {
                        if (!visitedArchives.Add(f)) continue;

                        archiveIndex++;
                        var extractTo = Path.Combine(tempRoot, "A" + archiveIndex.ToString("00000"));
                        Directory.CreateDirectory(extractTo);

                        ExtractArchiveSafe(f, extractTo);

                        var extractedJsons = SafeEnumerateFilesDeep(extractTo)
                            .Where(x => Path.GetExtension(x).Equals(".json", StringComparison.OrdinalIgnoreCase))
                            .ToList();

                        if (extractedJsons.Count > 0)
                        {
                            if (chkOnlyFirstJsonPerArchive.Checked)
                            {
                                var one = extractedJsons[0];
                                if (foundSet.Add(one)) foundJson.Add(one);
                            }
                            else
                            {
                                for (int k = 0; k < extractedJsons.Count; k++)
                                {
                                    var j = extractedJsons[k];
                                    if (foundSet.Add(j)) foundJson.Add(j);
                                }
                            }

                            if (chkStopOnFirstJson.Checked && foundJson.Count > 0) return;
                        }

                        walk(extractTo);
                    }
                }

                List<string> dirs;
                try { dirs = SafeEnumerateDirectories(path).ToList(); }
                catch { dirs = new List<string>(); }

                for (int i = 0; i < dirs.Count; i++)
                {
                    if (chkStopOnFirstJson.Checked && foundJson.Count > 0) return;
                    walk(dirs[i]);
                }
            };

            walk(rootFolder);
            return foundJson;
        }

        private bool IsArchive(string filePath)
        {
            var ext = Path.GetExtension(filePath);
            for (int i = 0; i < _archiveExtensions.Length; i++)
                if (_archiveExtensions[i].Equals(ext, StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }

        private IEnumerable<string> SafeEnumerateFiles(string folder)
        {
            try { return Directory.EnumerateFiles(folder, "*.*", SearchOption.TopDirectoryOnly); }
            catch { return Enumerable.Empty<string>(); }
        }

        private IEnumerable<string> SafeEnumerateDirectories(string folder)
        {
            try { return Directory.EnumerateDirectories(folder, "*", SearchOption.TopDirectoryOnly); }
            catch { return Enumerable.Empty<string>(); }
        }

        private IEnumerable<string> SafeEnumerateFilesDeep(string folder)
        {
            var stack = new Stack<string>();
            stack.Push(folder);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                foreach (var f in SafeEnumerateFiles(current))
                    yield return f;

                foreach (var d in SafeEnumerateDirectories(current))
                    stack.Push(d);
            }
        }

        // =========================
        // Extract seguro ZIP/RAR
        // =========================
        private void ExtractArchiveSafe(string archivePath, string destinationFolder)
        {
            Directory.CreateDirectory(destinationFolder);

            using (var archive = ArchiveFactory.Open(archivePath))
            {
                foreach (var entry in archive.Entries.Where(e => !e.IsDirectory))
                {
                    var relative = SanitizeRelativePath(entry.Key);
                    if (string.IsNullOrWhiteSpace(relative))
                        continue;

                    var baseFull = Path.GetFullPath(destinationFolder);
                    var outPath = Path.GetFullPath(Path.Combine(destinationFolder, relative));

                    if (!outPath.StartsWith(baseFull, StringComparison.OrdinalIgnoreCase))
                        continue;

                    var outDir = Path.GetDirectoryName(outPath);
                    if (!string.IsNullOrWhiteSpace(outDir))
                        Directory.CreateDirectory(outDir);

                    using (var inStream = entry.OpenEntryStream())
                    using (var outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        inStream.CopyTo(outStream);
                    }
                }
            }
        }

        private static string SanitizeRelativePath(string key)
        {
            var parts = key.Replace('\\', '/')
                           .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                           .Where(p => p != "." && p != "..")
                           .ToList();

            if (parts.Count == 0) return null;

            for (int i = 0; i < parts.Count; i++)
                parts[i] = SanitizeFileName(parts[i]);

            int lastIndex = parts.Count - 1;
            var fileName = parts[lastIndex];

            if (fileName.Length > 80)
            {
                var ext = Path.GetExtension(fileName);
                var name = Path.GetFileNameWithoutExtension(fileName);
                parts[lastIndex] = Hash8(name) + ext;
            }

            if (parts.Count > 7)
                parts = parts.Skip(parts.Count - 7).ToList();

            return Path.Combine(parts.ToArray());
        }

        private static string SanitizeFileName(string name)
        {
            var invalid = Path.GetInvalidFileNameChars();
            var sb = new StringBuilder(name.Length);

            for (int i = 0; i < name.Length; i++)
            {
                var ch = name[i];
                sb.Append(invalid.Contains(ch) ? '_' : ch);
            }

            var cleaned = sb.ToString().Trim().TrimEnd('.');
            return string.IsNullOrWhiteSpace(cleaned) ? "file" : cleaned;
        }

        private static string Hash8(string input)
        {
            using (var sha1 = SHA1.Create())
            {
                var bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input ?? ""));
                var hex = BitConverter.ToString(bytes).Replace("-", "");
                return hex.Substring(0, 8);
            }
        }

        private void dgvCfdis_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgvCfdis.DataSource == null) return;
            tsTotal.Text = dgvCfdis.RowCount.ToString("N0");
        }
    }
}
