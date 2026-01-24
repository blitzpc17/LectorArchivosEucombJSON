using Models;
using SharpCompress.Archives;
using SharpCompress.Common;
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

namespace Presentacion
{
    public partial class formLectorDiarios : Form
    {
        private readonly string[] _archiveExtensions = new[] { ".zip", ".rar" };

        public formLectorDiarios()
        {
            InitializeComponent();
        }

        private void frmJsonExplorer_Load(object sender, EventArgs e)
        {
            // Prepara el grid
            dgvNodes.AutoGenerateColumns = true;
            dgvNodes.AllowUserToAddRows = false;
            dgvNodes.AllowUserToDeleteRows = false;
            dgvNodes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog
            {
                Description = "Selecciona la carpeta que contiene archivos .json o .zip/.rar",
               // UseDescriptionForTitle = true,
                ShowNewFolderButton = false
            };

            if (dlg.ShowDialog() == DialogResult.OK)
                txtFolder.Text = dlg.SelectedPath;
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            var folder = txtFolder.Text?.Trim();
            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder))
            {
                MessageBox.Show("Selecciona una carpeta válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnProcess.Enabled = false;
            btnBrowse.Enabled = false;
            dgvNodes.DataSource = null;
            lblStatus.Text = "Procesando...";
            progressBar.Style = ProgressBarStyle.Marquee;

            try
            {
                var result = await Task.Run(() => ProcessFolder(folder));
                // Bind al grid
                dgvNodes.DataSource = result;

                lblStatus.Text = $"Listo. Nodos: {result.Rows.Count:n0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error.";
            }
            finally
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                btnProcess.Enabled = true;
                btnBrowse.Enabled = true;
            }
        }

        private DataTable ProcessFolder(string rootFolder)
        {
            // Tabla para el grid
            var table = BuildTableSchema();

            // Carpeta temporal para extracciones
            var tempRoot = Path.Combine(Path.GetTempPath(), "JX", Guid.NewGuid().ToString("N").Substring(0, 8));
            Directory.CreateDirectory(tempRoot);

            try
            {
                var jsonFiles = CollectJsonFiles(rootFolder, tempRoot);

                if (jsonFiles.Count == 0)
                    throw new InvalidOperationException("No se encontró ningún archivo .json (ni dentro de .zip/.rar).");

                // Si quieres detener al primer json (por UI)
                if (chkStopOnFirstJson.Checked)
                    jsonFiles = jsonFiles.Take(1).ToList();

                // Procesa cada json encontrado
                foreach (var jsonPath in jsonFiles)
                {
                    // 1) Deserializa a tus clases (por si quieres usar el objeto tipado)
                    //    Nota: si el JSON no coincide 100%, igualmente hacemos el parse por nodos.
                    try
                    {
                        var typed = DeserializeTyped(jsonPath);
                        // Aquí podrías usar `typed` para algo adicional si lo deseas.
                    }
                    catch
                    {
                        // Ignoramos errores de tipado y continuamos con el parse por nodos.
                    }

                    // 2) Parse y flatten de nodos
                    FlattenJsonToTable(jsonPath, table);
                }

                return table;
            }
            finally
            {
                // Limpia temporales (si te interesa conservarlos, comenta esto)
                try { Directory.Delete(tempRoot, true); } catch { /* noop */ }
            }
        }

        private List<string> CollectJsonFiles(string rootFolder, string tempRoot)
        {
            var foundJson = new List<string>();

            // Cola de "carpetas a explorar"
            var folderQueue = new Queue<string>();
            folderQueue.Enqueue(rootFolder);

            // Para evitar loops si un archivo se re-extrae
            var extractedArchives = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            while (folderQueue.Count > 0)
            {
                var currentFolder = folderQueue.Dequeue();

                // 1) Primero, JSON directos
                foreach (var json in Directory.EnumerateFiles(currentFolder, "*.json", SearchOption.AllDirectories))
                {
                    foundJson.Add(json);
                    if (chkStopOnFirstJson.Checked) return foundJson;
                }

                // 2) Luego, archivos comprimidos (zip/rar) en esta carpeta (recursivo)
                var archives = Directory.EnumerateFiles(currentFolder, "*.*", SearchOption.AllDirectories)
                    .Where(f => _archiveExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase));

                foreach (var archivePath in archives)
                {
                    if (chkStopOnFirstJson.Checked && foundJson.Count > 0) return foundJson;

                    if (extractedArchives.Contains(archivePath))
                        continue;

                    extractedArchives.Add(archivePath);

                    var extractTo = Path.Combine(tempRoot, "A" + extractedArchives.Count.ToString("00000"));
                    Directory.CreateDirectory(extractTo);

                    ExtractArchive(archivePath, extractTo);

                    // Si en lo extraído hay json, agrégalos
                    var extractedJson = Directory.EnumerateFiles(extractTo, "*.json", SearchOption.AllDirectories).ToList();
                    if (extractedJson.Count > 0)
                    {
                        if (chkOnlyFirstJsonPerArchive.Checked)
                            foundJson.Add(extractedJson.First());
                        else
                            foundJson.AddRange(extractedJson);

                        if (chkStopOnFirstJson.Checked) return foundJson;
                    }

                    // Si dentro hay más archives, en siguiente iteración los procesamos
                    folderQueue.Enqueue(extractTo);
                }
            }

            return foundJson;
        }

        private void ExtractArchive(string archivePath, string destinationFolder)
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

                    // Extraer manualmente para evitar líos de ExtractFullPath
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
            // Normaliza separadores
            var parts = key.Replace('\\', '/')
                           .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                           .Where(p => p != "." && p != "..")
                           .ToList();

            if (parts.Count == 0) return null;

            // Limpia caracteres inválidos
            for (int i = 0; i < parts.Count; i++)
                parts[i] = SanitizeFileName(parts[i]);

            // Acorta nombre final si es enorme
            var lastIndex = parts.Count - 1;
            var fileName = parts[lastIndex];

            if (fileName.Length > 80)
            {
                var ext = Path.GetExtension(fileName);
                var name = Path.GetFileNameWithoutExtension(fileName);
                parts[lastIndex] = Hash8(name) + ext;
            }

            // Limita profundidad para reducir rutas largas (últimas 6 carpetas + archivo)
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
                char ch = name[i];
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


        private EDSReport DeserializeTyped(string jsonPath)
        {
            var json = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<EDSReport>(json, options);
        }

        private void FlattenJsonToTable(string jsonPath, DataTable table)
        {
            var jsonText = File.ReadAllText(jsonPath);

            var doc = JsonDocument.Parse(jsonText, new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip
            });

            WalkElement(
                element: doc.RootElement,
                path: "$",
                sourceFile: jsonPath,
                addRow: (src, p, kind, value, extra) =>
                {
                    var row = table.NewRow();
                    row["SourceFile"] = src;
                    row["Path"] = p;
                    row["Kind"] = kind;
                    row["Value"] = value;
                    row["Extra"] = extra;
                    table.Rows.Add(row);
                });
        }

        private void WalkElement(JsonElement element, string path, string sourceFile,
            Action<string, string, string, string, string> addRow)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    addRow(sourceFile, path, "Object", "", "");
                    foreach (var prop in element.EnumerateObject())
                    {
                        var childPath = $"{path}.{prop.Name}";
                        WalkElement(prop.Value, childPath, sourceFile, addRow);
                    }
                    break;

                case JsonValueKind.Array:
                    var count = element.GetArrayLength();
                    addRow(sourceFile, path, "Array", "", $"Count={count}");
                    int i = 0;
                    foreach (var item in element.EnumerateArray())
                    {
                        var childPath = $"{path}[{i}]";
                        WalkElement(item, childPath, sourceFile, addRow);
                        i++;
                    }
                    break;

                case JsonValueKind.String:
                    addRow(sourceFile, path, "String", element.GetString() ?? "", "");
                    break;

                case JsonValueKind.Number:
                    addRow(sourceFile, path, "Number", element.ToString(), "");
                    break;

                case JsonValueKind.True:
                case JsonValueKind.False:
                    addRow(sourceFile, path, "Boolean", element.GetBoolean().ToString(), "");
                    break;

                case JsonValueKind.Null:
                    addRow(sourceFile, path, "Null", "null", "");
                    break;

                default:
                    addRow(sourceFile, path, element.ValueKind.ToString(), element.ToString(), "");
                    break;
            }
        }

        private DataTable BuildTableSchema()
        {
            var dt = new DataTable();
            dt.Columns.Add("SourceFile", typeof(string));
            dt.Columns.Add("Path", typeof(string));
            dt.Columns.Add("Kind", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Extra", typeof(string));
            return dt;
        }
    }
}

