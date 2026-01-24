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
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Presentacion
{
    public partial class frmJsonExplorer : Form
    {
        private readonly string[] _archiveExtensions = new[] { ".zip", ".rar" };

        private DataTable _inventoryTable;
        private DataTable _salesTable;

        private class ReportItem
        {
            public string SourceFile { get; set; }
            public EDSReport Report { get; set; }
        }

        public frmJsonExplorer()
        {
            InitializeComponent();
        }

        private void frmJsonExplorer_Load(object sender, EventArgs e)
        {
            SetupGrid(dgvInventario);
            SetupGrid(dgvVenta);

            lblStatus.Text = "Listo.";
            progressBar.Style = ProgressBarStyle.Blocks;
            btnExport.Enabled = false;
        }

        private void SetupGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
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

            dgvInventario.DataSource = null;
            dgvVenta.DataSource = null;
            _inventoryTable = null;
            _salesTable = null;

            progressBar.Style = ProgressBarStyle.Marquee;
            lblStatus.Text = "Procesando...";

            try
            {
                var result = await Task.Run(() => LoadAllTables(folder));

                _inventoryTable = result.Item1;
                _salesTable = result.Item2;

                dgvInventario.DataSource = _inventoryTable;
                dgvVenta.DataSource = _salesTable;

                btnExport.Enabled = ((_inventoryTable != null && _inventoryTable.Rows.Count > 0) ||
                                     (_salesTable != null && _salesTable.Rows.Count > 0));

                lblStatus.Text = $"Listo. Inventario: {_inventoryTable.Rows.Count:N0} filas | Venta: {_salesTable.Rows.Count:N0} filas";
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

        private Tuple<DataTable, DataTable> LoadAllTables(string rootFolder)
        {
            var tempRoot = Path.Combine(Path.GetTempPath(), "JX", Guid.NewGuid().ToString("N").Substring(0, 8));
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

                // Inventario consolidado por ClaveSubProducto
                var inv = BuildSubProductoSummaryTable(reports);

                // Venta (detalle CFDIs)
                var sales = BuildVentasTable(reports);

                return Tuple.Create(inv, sales);
            }
            finally
            {
                try { Directory.Delete(tempRoot, true); } catch { }
            }
        }

        // ==========================
        //  EXPORT: exporta TAB ACTIVO
        // ==========================
        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable table;
            string defaultName;

            if (tabMain.SelectedTab == tabInventario)
            {
                table = _inventoryTable;
                defaultName = "Inventario_Resumen.xlsx";
            }
            else
            {
                table = _salesTable;
                defaultName = "Venta_Detalle.xlsx";
            }

            if (table == null || table.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar en este tab.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = defaultName;
                if (sfd.ShowDialog() != DialogResult.OK) return;

                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(tabMain.SelectedTab.Text);

                    // Encabezados
                    for (int c = 0; c < table.Columns.Count; c++)
                        ws.Cell(1, c + 1).SetValue(table.Columns[c].ColumnName);

                    // Datos
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
                    wb.SaveAs(sfd.FileName);
                }

                MessageBox.Show("Exportado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        // ==========================
        //  INVENTARIO: agrupado por ClaveSubProducto (sumatorias)
        // ==========================
        private DataTable BuildSubProductoSummaryTable(List<ReportItem> items)
        {
            var dict = new Dictionary<string, Agg>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < items.Count; i++)
            {
                var r = items[i].Report;
                if (r == null || r.Producto == null) continue;

                for (int pIndex = 0; pIndex < r.Producto.Count; pIndex++)
                {
                    var p = r.Producto[pIndex];
                    if (p == null) continue;

                    var key = (p.ClaveSubProducto ?? "").Trim();
                    if (key.Length == 0) key = "(SIN_CLAVESUBPRODUCTO)";

                    Agg agg;
                    if (!dict.TryGetValue(key, out agg))
                    {
                        agg = new Agg { ClaveSubProducto = key };
                        dict[key] = agg;
                    }

                    if (p.Tanque == null) continue;

                    for (int tIndex = 0; tIndex < p.Tanque.Count; tIndex++)
                    {
                        var t = p.Tanque[tIndex];
                        if (t == null || t.Recepciones == null) continue;

                        agg.TotalRecepciones += (t.Recepciones.TotalRecepciones ?? 0);
                        agg.TotalDocumentos += (t.Recepciones.TotalDocumentos ?? 0);
                        agg.SumaCompras += (t.Recepciones.SumaCompras ?? 0m);

                        if (t.Recepciones.SumaVolumenRecepcion != null)
                        {
                            agg.SumaVolRecepcion += t.Recepciones.SumaVolumenRecepcion.ValorNumerico;
                            var u = t.Recepciones.SumaVolumenRecepcion.UnidadDeMedida;
                            if (!string.IsNullOrWhiteSpace(u))
                                agg.RegisterUnidad(u);
                        }
                    }
                }
            }

            var dt = new DataTable();
            dt.Columns.Add("ClaveSubProducto", typeof(string));
            dt.Columns.Add("TotalRecepciones", typeof(int));
            dt.Columns.Add("TotalDocumentos", typeof(int));
            dt.Columns.Add("SumaCompras", typeof(decimal));
            dt.Columns.Add("SumaVolRecepcion", typeof(decimal));
            dt.Columns.Add("UnidadVol", typeof(string));

            foreach (var agg in dict.Values.OrderByDescending(x => x.SumaVolRecepcion))
            {
                var row = dt.NewRow();
                row["ClaveSubProducto"] = agg.ClaveSubProducto;
                row["TotalRecepciones"] = agg.TotalRecepciones;
                row["TotalDocumentos"] = agg.TotalDocumentos;
                row["SumaCompras"] = agg.SumaCompras;
                row["SumaVolRecepcion"] = agg.SumaVolRecepcion;
                row["UnidadVol"] = agg.UnidadVol;
                dt.Rows.Add(row);
            }

            return dt;
        }

        private class Agg
        {
            public string ClaveSubProducto;
            public int TotalRecepciones;
            public int TotalDocumentos;
            public decimal SumaCompras;
            public decimal SumaVolRecepcion;

            private string _unidad;
            private bool _mix;

            public void RegisterUnidad(string unidad)
            {
                if (_mix) return;
                if (_unidad == null) _unidad = unidad;
                else if (!string.Equals(_unidad, unidad, StringComparison.OrdinalIgnoreCase))
                    _mix = true;
            }

            public string UnidadVol
            {
                get { return _mix ? "MIX" : (_unidad ?? ""); }
            }
        }

        // ==========================
        //  VENTA: detalle CFDIs desde ENTREGAS
        //  Columnas requeridas:
        //  RfcClienteOProveedor, NombreClienteOProveedor, Cfdi, FechaYHoraTransaccion, ValorNumerico
        // ==========================
        private DataTable BuildVentasTable(List<ReportItem> items)
        {
            var dt = new DataTable();

            // Requeridas
            dt.Columns.Add("RfcClienteOProveedor", typeof(string));
            dt.Columns.Add("NombreClienteOProveedor", typeof(string));
            dt.Columns.Add("Cfdi", typeof(string));
            dt.Columns.Add("FechaYHoraTransaccion", typeof(string));
            dt.Columns.Add("ValorNumerico", typeof(decimal));

            // Extra útil “obtenido de EDSReport” (si no lo quieres, lo borras)
            dt.Columns.Add("RFCContribuyente", typeof(string));
            dt.Columns.Add("NumPermiso", typeof(string));
            dt.Columns.Add("FechaCorte", typeof(string));
            dt.Columns.Add("ClaveSubProducto", typeof(string));
            dt.Columns.Add("ClaveProducto", typeof(string));
            dt.Columns.Add("SourceFile", typeof(string));

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var r = item.Report;
                if (r == null || r.Producto == null) continue;

                for (int pIndex = 0; pIndex < r.Producto.Count; pIndex++)
                {
                    var p = r.Producto[pIndex];
                    if (p == null) continue;

                    if (p.Tanque == null) continue;

                    for (int tIndex = 0; tIndex < p.Tanque.Count; tIndex++)
                    {
                        var t = p.Tanque[tIndex];
                        if (t == null || t.Entregas == null || t.Entregas.Entrega == null) continue;

                        for (int eIndex = 0; eIndex < t.Entregas.Entrega.Count; eIndex++)
                        {
                            var entrega = t.Entregas.Entrega[eIndex];
                            if (entrega == null || entrega.Complemento == null) continue;

                            var comp = entrega.Complemento;
                            if (comp.Nacional == null) continue;

                            for (int nIndex = 0; nIndex < comp.Nacional.Count; nIndex++)
                            {
                                var nac = comp.Nacional[nIndex];
                                if (nac == null || nac.CFDIs == null) continue;

                                for (int cIndex = 0; cIndex < nac.CFDIs.Count; cIndex++)
                                {
                                    var cfdi = nac.CFDIs[cIndex];
                                    if (cfdi == null) continue;

                                    var row = dt.NewRow();

                                    row["RfcClienteOProveedor"] = nac.RfcClienteOProveedor ?? "";
                                    row["NombreClienteOProveedor"] = nac.NombreClienteOProveedor ?? "";
                                    row["Cfdi"] = cfdi.Cfdi ?? "";
                                    row["FechaYHoraTransaccion"] = cfdi.FechaYHoraTransaccion.HasValue
                                        ? cfdi.FechaYHoraTransaccion.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                        : "";

                                    // ValorNumerico: volumen documentado
                                    decimal vol = 0m;
                                    if (cfdi.VolumenDocumentado != null)
                                        vol = cfdi.VolumenDocumentado.ValorNumerico;

                                    row["ValorNumerico"] = vol;

                                    // Extras
                                    row["RFCContribuyente"] = r.RfcContribuyente ?? "";
                                    row["NumPermiso"] = r.NumPermiso ?? "";
                                    row["FechaCorte"] = r.FechaYHoraCorte.HasValue ? r.FechaYHoraCorte.Value.ToString("yyyy-MM-dd HH:mm") : "";
                                    row["ClaveSubProducto"] = p.ClaveSubProducto ?? "";
                                    row["ClaveProducto"] = p.ClaveProducto ?? "";
                                    row["SourceFile"] = Path.GetFileName(item.SourceFile);

                                    dt.Rows.Add(row);
                                }
                            }
                        }
                    }
                }
            }

            return dt;
        }

        // ==========================
        //  Cargar reportes (EDSReport)
        // ==========================
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
                    // Ignora jsons con error de parse (si quieres, log)
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

        // ==========================
        //  CollectJsonFilesRecursive (SIN duplicados)
        //  + extracción segura ya la tienes; úsala aquí
        // ==========================
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

        // ==========================
        //  Extract seguro (C# 7.3) + rutas cortas
        // ==========================
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

        private void dgvInventario_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgvInventario.DataSource == null) return;
            tsTotalRegistrosInventario.Text = $"{dgvInventario.Rows.Count:N0}";

        }

        private void dgvVenta_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgvVenta.DataSource == null) return;
            tsTotalRegistrosVenta.Text = $"{dgvVenta.Rows.Count:N0}";
        }
    }
}
