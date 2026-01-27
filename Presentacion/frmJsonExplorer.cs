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

        private List<ReportItem> _reports = new List<ReportItem>();

        private DataTable _dtResumenProducto;
        private DataTable _dtRecepciones;
        private DataTable _dtVenta;

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
            SetupGrid(dgvResumenProducto);
            SetupGrid(dgvRecepciones);
            SetupGrid(dgvVenta);

            ClearDatosGenerales();

            lblStatus.Text = "Listo.";
            progressBar.Style = ProgressBarStyle.Blocks;
            btnExport.Enabled = false;
        }

        private void SetupGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ClearDatosGenerales()
        {
            txtInstalacion.Text = "";
            txtVersion.Text = "";
            txtRfcContribuyente.Text = "";
            txtRfcProveedor.Text = "";
            txtRfcRepresentante.Text = "";
            txtCaracter.Text = "";
            txtModalidadPermiso.Text = "";
            txtNumPermiso.Text = "";
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

            ClearDatosGenerales();

            dgvResumenProducto.DataSource = null;
            dgvRecepciones.DataSource = null;
            dgvVenta.DataSource = null;

            _dtResumenProducto = null;
            _dtRecepciones = null;
            _dtVenta = null;
            _reports = new List<ReportItem>();

            progressBar.Style = ProgressBarStyle.Marquee;
            lblStatus.Text = "Procesando...";

            try
            {
                var result = await Task.Run(() => LoadAll(folder));

                _reports = result.Reports;

                // Datos generales: si hay muchos json, tomamos el primero como "cabecera"
                FillDatosGenerales(_reports.Count > 0 ? _reports[0].Report : null);

                _dtResumenProducto = result.ResumenProducto;
                _dtRecepciones = result.Recepciones;
                _dtVenta = result.Venta;

                dgvResumenProducto.DataSource = _dtResumenProducto;
                dgvRecepciones.DataSource = _dtRecepciones;
                dgvVenta.DataSource = _dtVenta;

                btnExport.Enabled =
                    (_dtResumenProducto != null && _dtResumenProducto.Rows.Count > 0) ||
                    (_dtRecepciones != null && _dtRecepciones.Rows.Count > 0) ||
                    (_dtVenta != null && _dtVenta.Rows.Count > 0);

                lblStatus.Text = $"Listo. Resumen: {_dtResumenProducto.Rows.Count:N0} | Recepciones: {_dtRecepciones.Rows.Count:N0} | Venta: {_dtVenta.Rows.Count:N0}";
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

        private void FillDatosGenerales(EDSReport r)
        {
            if (r == null) return;

            var instalacion = ((r.ClaveInstalacion ?? "").Trim() + " - " + (r.DescripcionInstalacion ?? "").Trim()).Trim(' ', '-');

            txtInstalacion.Text = instalacion;
            txtVersion.Text = r.Version ?? "";
            txtRfcContribuyente.Text = r.RfcContribuyente ?? "";
            txtRfcProveedor.Text = r.RfcProveedor ?? "";
            txtRfcRepresentante.Text = r.RfcRepresentanteLegal ?? "";
            txtCaracter.Text = r.Caracter ?? "";
            txtModalidadPermiso.Text = r.ModalidadPermiso ?? "";
            txtNumPermiso.Text = r.NumPermiso ?? "";
        }

        // =========================
        // EXPORT: exporta el grid principal del TAB ACTIVO
        // Inventario: exporta Recepciones (CFDI) si está seleccionado el group (por simplicidad exporta el que tenga filas > 0),
        // Venta: exporta dgvVenta.
        // =========================
        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable table;
            string sheetName;
            string defaultFile;

            if (tabMain.SelectedTab == tabVenta)
            {
                table = _dtVenta;
                sheetName = "Venta";
                defaultFile = "Venta.xlsx";
            }
            else
            {
                // En inventario te conviene exportar Recepciones (detalle CFDI).
                // Si no hay, exporta resumen.
                if (_dtRecepciones != null && _dtRecepciones.Rows.Count > 0)
                {
                    table = _dtRecepciones;
                    sheetName = "Recepciones";
                    defaultFile = "Recepciones.xlsx";
                }
                else
                {
                    table = _dtResumenProducto;
                    sheetName = "ResumenProducto";
                    defaultFile = "ResumenProducto.xlsx";
                }
            }

            if (table == null || table.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = defaultFile;

                if (sfd.ShowDialog() != DialogResult.OK) return;

                using (var wb = new XLWorkbook())
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

        // =========================
        // PIPELINE: cargar reportes + construir tablas inventario/recepciones/venta
        // =========================
        private LoadResult LoadAll(string rootFolder)
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

                // Inventario: resumen por producto (subproducto)
                var dtResumen = BuildResumenProducto(reports);

                // Inventario: detalle recepciones CFDI (desde Recepcion.Complemento.Nacional.CFDIs)
                var dtRecep = BuildRecepcionesCfdiTable(reports);

                // Venta: detalle de entregas (si ya lo tenías)
                var dtVenta = BuildVentasTable(reports);

                return new LoadResult
                {
                    Reports = reports,
                    ResumenProducto = dtResumen,
                    Recepciones = dtRecep,
                    Venta = dtVenta
                };
            }
            finally
            {
                try { Directory.Delete(tempRoot, true); } catch { }
            }
        }

        private class LoadResult
        {
            public List<ReportItem> Reports { get; set; }
            public DataTable ResumenProducto { get; set; }
            public DataTable Recepciones { get; set; }
            public DataTable Venta { get; set; }
        }

        // =========================
        // RESUMEN POR PRODUCTO (por ClaveSubProducto):
        // - Inventario final del mes: sumatoria Existencias.VolumenExistencias
        // - Veces que entró producto: conteo CFDIs en Recepciones relacionados al subproducto
        // - Total litros factura: sumatoria Recepcion.VolumenRecepcion.ValorNumerico
        // =========================
        private DataTable BuildResumenProducto(List<ReportItem> items)
        {
            var dict = new Dictionary<string, ResProdAgg>(StringComparer.OrdinalIgnoreCase);

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

                    ResProdAgg agg;
                    if (!dict.TryGetValue(key, out agg))
                    {
                        agg = new ResProdAgg { ClaveSubProducto = key };
                        dict[key] = agg;
                    }

                    if (p.Tanque == null) continue;

                    for (int tIndex = 0; tIndex < p.Tanque.Count; tIndex++)
                    {
                        var t = p.Tanque[tIndex];
                        if (t == null) continue;

                        // Inventario al final (Existencias.VolumenExistencias)
                        if (t.Existencias != null && t.Existencias.VolumenExistencias.HasValue)
                            agg.InventarioFinal += t.Existencias.VolumenExistencias.Value;

                        // Total litros factura (sum Recepcion.VolumenRecepcion.ValorNumerico)
                        // Veces que entró: conteo de CFDIs en recepciones
                        if (t.Recepciones != null && t.Recepciones.Recepcion != null)
                        {
                            for (int rx = 0; rx < t.Recepciones.Recepcion.Count; rx++)
                            {
                                var rec = t.Recepciones.Recepcion[rx];
                                if (rec == null) continue;

                                if (rec.VolumenRecepcion != null)
                                    agg.TotalLitrosFactura += rec.VolumenRecepcion.ValorNumerico;

                                // Conteo CFDIs en complemento
                                int cfdisCount = CountCfdis(rec);
                                agg.NumEntradas += cfdisCount;
                            }
                        }
                    }
                }
            }

            var dt = new DataTable();
            dt.Columns.Add("ClaveSubProducto", typeof(string));
            dt.Columns.Add("InventarioFinalMes", typeof(decimal));
            dt.Columns.Add("NumVecesEntroProducto", typeof(int));
            dt.Columns.Add("TotalLitrosFactura", typeof(decimal));

            foreach (var agg in dict.Values.OrderBy(x => x.ClaveSubProducto))
            {
                var row = dt.NewRow();
                row["ClaveSubProducto"] = agg.ClaveSubProducto;
                row["InventarioFinalMes"] = agg.InventarioFinal;
                row["NumVecesEntroProducto"] = agg.NumEntradas;
                row["TotalLitrosFactura"] = agg.TotalLitrosFactura;
                dt.Rows.Add(row);
            }

            return dt;
        }

        private class ResProdAgg
        {
            public string ClaveSubProducto;
            public decimal InventarioFinal;
            public int NumEntradas;
            public decimal TotalLitrosFactura;
        }

        private int CountCfdis(Recepcion rec)
        {
            if (rec == null || rec.Complemento == null || rec.Complemento.Nacional == null) return 0;

            int count = 0;
            for (int i = 0; i < rec.Complemento.Nacional.Count; i++)
            {
                var nac = rec.Complemento.Nacional[i];
                if (nac == null || nac.CFDIs == null) continue;
                count += nac.CFDIs.Count;
            }
            return count;
        }

        // =========================
        // RECEPCIONES CFDI (DETALLE)
        // Columnas:
        // # (NumeroDeRegistro), Nombre cliente, RFC Cliente proveedor, CFDI, Long., Fecha y hora,
        // Precio Compra, Precio Venta Publico, Valor numerico
        // =========================
        private DataTable BuildRecepcionesCfdiTable(List<ReportItem> items)
        {
            var dt = new DataTable();

            dt.Columns.Add("#", typeof(int));
            dt.Columns.Add("NombreCliente", typeof(string));
            dt.Columns.Add("RfcClienteOProveedor", typeof(string));
            dt.Columns.Add("CFDI", typeof(string));
            dt.Columns.Add("Long.", typeof(string));
            dt.Columns.Add("FechaYHora", typeof(string));
            dt.Columns.Add("PrecioCompra", typeof(decimal));
            dt.Columns.Add("PrecioVentaPublico", typeof(decimal));
            dt.Columns.Add("ValorNumerico", typeof(decimal));

            // extras opcionales
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

                                    var row = dt.NewRow();

                                    row["#"] = rec.NumeroDeRegistro ?? 0;
                                    row["NombreCliente"] = nac.NombreClienteOProveedor ?? "";
                                    row["RfcClienteOProveedor"] = nac.RfcClienteOProveedor ?? "";
                                    row["CFDI"] = cfdi.Cfdi ?? "";

                                    // Long.: si tienes algún campo exacto, cámbialo aquí.
                                    // Si no existe, dejamos el UUID corto por conveniencia.
                                    row["Long."] = ShortCfdi(cfdi.Cfdi);

                                    row["FechaYHora"] = cfdi.FechaYHoraTransaccion.HasValue
                                        ? cfdi.FechaYHoraTransaccion.Value.ToString("yyyy-MM-dd HH:mm:ss")
                                        : "";

                                    row["PrecioCompra"] = cfdi.PrecioCompra ?? 0m;
                                    row["PrecioVentaPublico"] = cfdi.PrecioDeVentaAlPublico ?? 0m;

                                    decimal vol = 0m;
                                    if (cfdi.VolumenDocumentado != null)
                                        vol = cfdi.VolumenDocumentado.ValorNumerico;
                                    row["ValorNumerico"] = vol;

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

        private string ShortCfdi(string cfdi)
        {
            if (string.IsNullOrWhiteSpace(cfdi)) return "";
            var s = cfdi.Trim();
            if (s.Length <= 12) return s;
            return s.Substring(0, 8) + "...";
        }

        // =========================
        // VENTA (de Entregas -> CFDIs)
        // Deja como estaba, o ajusta a tu necesidad
        // =========================
        private DataTable BuildVentasTable(List<ReportItem> items)
        {
            var dt = new DataTable();

            dt.Columns.Add("RfcClienteOProveedor", typeof(string));
            dt.Columns.Add("NombreClienteOProveedor", typeof(string));
            dt.Columns.Add("Cfdi", typeof(string));
            dt.Columns.Add("FechaYHoraTransaccion", typeof(string));
            dt.Columns.Add("ValorNumerico", typeof(decimal));

            // extras
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
                    if (p == null || p.Tanque == null) continue;

                    for (int tIndex = 0; tIndex < p.Tanque.Count; tIndex++)
                    {
                        var t = p.Tanque[tIndex];
                        if (t == null || t.Entregas == null || t.Entregas.Entrega == null) continue;

                        for (int eIndex = 0; eIndex < t.Entregas.Entrega.Count; eIndex++)
                        {
                            var entrega = t.Entregas.Entrega[eIndex];
                            if (entrega == null || entrega.Complemento == null || entrega.Complemento.Nacional == null) continue;

                            for (int nIndex = 0; nIndex < entrega.Complemento.Nacional.Count; nIndex++)
                            {
                                var nac = entrega.Complemento.Nacional[nIndex];
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
                                    row["ValorNumerico"] = (cfdi.VolumenDocumentado != null) ? cfdi.VolumenDocumentado.ValorNumerico : 0m;

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

        // =========================
        // Cargar reportes
        // =========================
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
    }
}
