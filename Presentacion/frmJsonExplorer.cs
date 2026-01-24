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
using ClosedXML.Excel;
using Models;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace Presentacion

{
    public partial class frmJsonExplorer : Form
    {
        // Ajusta si quieres soportar más extensiones
        private readonly string[] _archiveExtensions = new[] { ".zip", ".rar" };

        private DataTable _lastSummary;

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
            dgvSummary.AutoGenerateColumns = true;
            dgvSummary.AllowUserToAddRows = false;
            dgvSummary.AllowUserToDeleteRows = false;
            dgvSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            lblStatus.Text = "Listo.";
            progressBar.Style = ProgressBarStyle.Blocks;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Selecciona la carpeta que contiene .json o .zip/.rar";
                dlg.ShowNewFolderButton = false;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = dlg.SelectedPath;
                }
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
            dgvSummary.DataSource = null;
            _lastSummary = null;

            progressBar.Style = ProgressBarStyle.Marquee;
            lblStatus.Text = "Procesando...";

            try
            {
                var dt = await Task.Run(() => ProcessFolderSummary(folder));

                _lastSummary = dt;
                dgvSummary.DataSource = dt;

                lblStatus.Text = "Listo. Filas: " + dt.Rows.Count.ToString("N0");
                btnExport.Enabled = dt.Rows.Count > 0;
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_lastSummary == null || _lastSummary.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = "ResumenRecepciones.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Resumen");

                    // Encabezados
                    for (int c = 0; c < _lastSummary.Columns.Count; c++)
                        ws.Cell(1, c + 1).SetValue(_lastSummary.Columns[c].ColumnName);

                    // Datos
                    for (int r = 0; r < _lastSummary.Rows.Count; r++)
                    {
                        for (int c = 0; c < _lastSummary.Columns.Count; c++)
                        {
                            var col = _lastSummary.Columns[c];
                            var value = _lastSummary.Rows[r][c];
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

            // Usa el tipo real de la columna (DataColumn.DataType)
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

            // Todo lo demás como texto
            cell.SetValue(value.ToString());
        }

        // ==========================
        //  Pipeline principal
        // ==========================
        private DataTable ProcessFolderSummary(string rootFolder)
        {
            // Temp MUY corto para evitar rutas largas en .NET Framework
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

                return BuildProductSummaryTable(reports);
            }
            finally
            {
                try { Directory.Delete(tempRoot, true); } catch { }
            }
        }

        // ==========================
        //  Buscar JSON + extraer ZIP/RAR recursivo
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

                // Evita re-entrar al mismo folder
                if (!visitedFolders.Add(fullPath))
                    return;

                // Archivos del directorio actual (enumerar una sola vez)
                IEnumerable<string> files;
                try
                {
                    files = SafeEnumerateFiles(path).ToList(); // materializa para no enumerar 2 veces
                }
                catch
                {
                    files = Enumerable.Empty<string>();
                }

                foreach (var f in files)
                {
                    if (chkStopOnFirstJson.Checked && foundJson.Count > 0) return;

                    var ext = Path.GetExtension(f);

                    if (ext.Equals(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        if (foundSet.Add(f))
                            foundJson.Add(f);

                        if (chkStopOnFirstJson.Checked) return;
                    }
                    else if (IsArchive(f))
                    {
                        if (!visitedArchives.Add(f)) continue;

                        archiveIndex++;
                        var extractTo = Path.Combine(tempRoot, "A" + archiveIndex.ToString("00000"));
                        Directory.CreateDirectory(extractTo);

                        ExtractArchiveSafe(f, extractTo);

                        // JSON extraídos (dedupe)
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
                                foreach (var j in extractedJsons)
                                    if (foundSet.Add(j)) foundJson.Add(j);
                            }

                            if (chkStopOnFirstJson.Checked && foundJson.Count > 0) return;
                        }

                        // Busca archives anidados dentro de lo extraído
                        walk(extractTo);
                    }
                }

                // Subcarpetas (ojo: no vuelvas a caminar folders ya visitados)
                IEnumerable<string> dirs;
                try
                {
                    dirs = SafeEnumerateDirectories(path).ToList();
                }
                catch
                {
                    dirs = Enumerable.Empty<string>();
                }

                foreach (var d in dirs)
                {
                    if (chkStopOnFirstJson.Checked && foundJson.Count > 0) return;
                    walk(d);
                }
            };

            walk(rootFolder);
            return foundJson;
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
            // enumeración recursiva "segura"
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

        private bool IsArchive(string filePath)
        {
            var ext = Path.GetExtension(filePath);
            for (int i = 0; i < _archiveExtensions.Length; i++)
            {
                if (_archiveExtensions[i].Equals(ext, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
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

                    // Evita path traversal
                    if (!outPath.StartsWith(baseFull, StringComparison.OrdinalIgnoreCase))
                        continue;

                    var outDir = Path.GetDirectoryName(outPath);
                    if (!string.IsNullOrWhiteSpace(outDir))
                        Directory.CreateDirectory(outDir);

                    // Extraer manualmente (evita problemas con ExtractFullPath)
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

            // limpiar por segmento
            for (int i = 0; i < parts.Count; i++)
                parts[i] = SanitizeFileName(parts[i]);

            // acortar nombre final si es enorme
            int lastIndex = parts.Count - 1;
            var fileName = parts[lastIndex];

            if (fileName.Length > 80)
            {
                var ext = Path.GetExtension(fileName);
                var name = Path.GetFileNameWithoutExtension(fileName);
                parts[lastIndex] = Hash8(name) + ext;
            }

            // limita profundidad (últimas 6 carpetas + archivo)
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

        // ==========================
        //  Cargar lista de objetos (EDSReport)
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
                    // si quieres, aquí podrías loguear el archivo fallido
                }
            }

            return list;
        }

        private EDSReport DeserializeTyped(string jsonPath)
        {
            var json = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<EDSReport>(json, options);
        }

        // ==========================
        //  Resumen: agrupar Producto y sumar Recepciones
        // ==========================
        private DataTable BuildProductSummaryTable(List<ReportItem> items)
        {
            var dt = new DataTable();
            dt.Columns.Add("Archivo", typeof(string));
            dt.Columns.Add("RFC", typeof(string));
            dt.Columns.Add("Permiso", typeof(string));
            dt.Columns.Add("FechaCorte", typeof(string));

            dt.Columns.Add("ClaveProducto", typeof(string));
            dt.Columns.Add("ClaveSubProducto", typeof(string));

            dt.Columns.Add("TotalRecepciones", typeof(int));
            dt.Columns.Add("TotalDocumentos", typeof(int));
            dt.Columns.Add("SumaCompras", typeof(decimal));
            dt.Columns.Add("SumaVolRecepcion", typeof(decimal));
            dt.Columns.Add("UnidadVol", typeof(string));

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var r = item.Report;
                if (r == null || r.Producto == null) continue;

                for (int pIndex = 0; pIndex < r.Producto.Count; pIndex++)
                {
                    var p = r.Producto[pIndex];
                    if (p == null) continue;

                    int totalRecep = 0;
                    int totalDocs = 0;
                    decimal sumaCompras = 0m;
                    decimal sumaVol = 0m;

                    string unidad = null;
                    bool unidadMix = false;

                    if (p.Tanque != null)
                    {
                        for (int tIndex = 0; tIndex < p.Tanque.Count; tIndex++)
                        {
                            var t = p.Tanque[tIndex];
                            if (t == null || t.Recepciones == null) continue;

                            totalRecep += (t.Recepciones.TotalRecepciones ?? 0);
                            totalDocs += (t.Recepciones.TotalDocumentos ?? 0);
                            sumaCompras += (t.Recepciones.SumaCompras ?? 0m);

                            if (t.Recepciones.SumaVolumenRecepcion != null)
                            {
                                sumaVol += t.Recepciones.SumaVolumenRecepcion.ValorNumerico;

                                var u = t.Recepciones.SumaVolumenRecepcion.UnidadDeMedida;
                                if (!string.IsNullOrWhiteSpace(u))
                                {
                                    if (unidad == null) unidad = u;
                                    else if (!string.Equals(unidad, u, StringComparison.OrdinalIgnoreCase))
                                        unidadMix = true;
                                }
                            }
                        }
                    }

                    var row = dt.NewRow();
                    row["Archivo"] = Path.GetFileName(item.SourceFile);
                    row["RFC"] = r.RfcContribuyente ?? "";
                    row["Permiso"] = r.NumPermiso ?? "";
                    row["FechaCorte"] = (r.FechaYHoraCorte.HasValue ? r.FechaYHoraCorte.Value.ToString("yyyy-MM-dd HH:mm") : "");

                    row["ClaveProducto"] = p.ClaveProducto ?? "";
                    row["ClaveSubProducto"] = p.ClaveSubProducto ?? "";

                    row["TotalRecepciones"] = totalRecep;
                    row["TotalDocumentos"] = totalDocs;
                    row["SumaCompras"] = sumaCompras;
                    row["SumaVolRecepcion"] = sumaVol;
                    row["UnidadVol"] = unidadMix ? "MIX" : (unidad ?? "");

                    dt.Rows.Add(row);
                }
            }

            return dt;
        }
    }
}

