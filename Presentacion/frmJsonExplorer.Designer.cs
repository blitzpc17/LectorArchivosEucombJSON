namespace Presentacion
{
    partial class frmJsonExplorer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.chkStopOnFirstJson = new System.Windows.Forms.CheckBox();
            this.chkOnlyFirstJsonPerArchive = new System.Windows.Forms.CheckBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabInventario = new System.Windows.Forms.TabPage();
            this.btnExportInventario = new System.Windows.Forms.Button();
            this.grpRecepciones = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsTotalRecepciones = new System.Windows.Forms.ToolStripLabel();
            this.dgvRecepciones = new System.Windows.Forms.DataGridView();
            this.grpResumen = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsTotalProductos = new System.Windows.Forms.ToolStripLabel();
            this.dgvResumenProducto = new System.Windows.Forms.DataGridView();
            this.grpDatosGenerales = new System.Windows.Forms.GroupBox();
            this.txtNumPermiso = new System.Windows.Forms.TextBox();
            this.txtModalidadPermiso = new System.Windows.Forms.TextBox();
            this.txtCaracter = new System.Windows.Forms.TextBox();
            this.txtRfcRepresentante = new System.Windows.Forms.TextBox();
            this.txtRfcProveedor = new System.Windows.Forms.TextBox();
            this.txtRfcContribuyente = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtInstalacion = new System.Windows.Forms.TextBox();
            this.lblNumPermiso = new System.Windows.Forms.Label();
            this.lblModalidadPermiso = new System.Windows.Forms.Label();
            this.lblCaracter = new System.Windows.Forms.Label();
            this.lblRfcRepresentante = new System.Windows.Forms.Label();
            this.lblRfcProveedor = new System.Windows.Forms.Label();
            this.lblRfcContribuyente = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblInstalacion = new System.Windows.Forms.Label();
            this.tabVenta = new System.Windows.Forms.TabPage();
            this.btnExportVenta = new System.Windows.Forms.Button();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsTotalVentas = new System.Windows.Forms.ToolStripLabel();
            this.dgvVenta = new System.Windows.Forms.DataGridView();
            this.groupBoxOptions.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabInventario.SuspendLayout();
            this.grpRecepciones.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).BeginInit();
            this.grpResumen.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenProducto)).BeginInit();
            this.grpDatosGenerales.SuspendLayout();
            this.tabVenta.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(10, 10);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(652, 20);
            this.txtFolder.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(667, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(94, 22);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Elegir carpeta";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.Location = new System.Drawing.Point(766, 10);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(94, 22);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Procesar";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(866, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 22);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Exportar Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(10, 36);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(950, 12);
            this.progressBar.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(10, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(950, 17);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Listo.";
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.Controls.Add(this.chkStopOnFirstJson);
            this.groupBoxOptions.Controls.Add(this.chkOnlyFirstJsonPerArchive);
            this.groupBoxOptions.Location = new System.Drawing.Point(10, 70);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(950, 55);
            this.groupBoxOptions.TabIndex = 6;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Opciones";
            // 
            // chkStopOnFirstJson
            // 
            this.chkStopOnFirstJson.AutoSize = true;
            this.chkStopOnFirstJson.Location = new System.Drawing.Point(10, 19);
            this.chkStopOnFirstJson.Name = "chkStopOnFirstJson";
            this.chkStopOnFirstJson.Size = new System.Drawing.Size(190, 17);
            this.chkStopOnFirstJson.TabIndex = 0;
            this.chkStopOnFirstJson.Text = "Detener al encontrar el primer .json";
            this.chkStopOnFirstJson.UseVisualStyleBackColor = true;
            // 
            // chkOnlyFirstJsonPerArchive
            // 
            this.chkOnlyFirstJsonPerArchive.AutoSize = true;
            this.chkOnlyFirstJsonPerArchive.Checked = true;
            this.chkOnlyFirstJsonPerArchive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlyFirstJsonPerArchive.Location = new System.Drawing.Point(10, 39);
            this.chkOnlyFirstJsonPerArchive.Name = "chkOnlyFirstJsonPerArchive";
            this.chkOnlyFirstJsonPerArchive.Size = new System.Drawing.Size(274, 17);
            this.chkOnlyFirstJsonPerArchive.TabIndex = 1;
            this.chkOnlyFirstJsonPerArchive.Text = "Solo 1 .json por cada archivo comprimido (ZIP/RAR)";
            this.chkOnlyFirstJsonPerArchive.UseVisualStyleBackColor = true;
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabInventario);
            this.tabMain.Controls.Add(this.tabVenta);
            this.tabMain.Location = new System.Drawing.Point(10, 130);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(950, 521);
            this.tabMain.TabIndex = 7;
            // 
            // tabInventario
            // 
            this.tabInventario.Controls.Add(this.btnExportInventario);
            this.tabInventario.Controls.Add(this.grpRecepciones);
            this.tabInventario.Controls.Add(this.grpResumen);
            this.tabInventario.Controls.Add(this.grpDatosGenerales);
            this.tabInventario.Location = new System.Drawing.Point(4, 22);
            this.tabInventario.Name = "tabInventario";
            this.tabInventario.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventario.Size = new System.Drawing.Size(942, 495);
            this.tabInventario.TabIndex = 0;
            this.tabInventario.Text = "Inventario";
            this.tabInventario.UseVisualStyleBackColor = true;
            // 
            // btnExportInventario
            // 
            this.btnExportInventario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportInventario.Location = new System.Drawing.Point(820, 6);
            this.btnExportInventario.Name = "btnExportInventario";
            this.btnExportInventario.Size = new System.Drawing.Size(116, 23);
            this.btnExportInventario.TabIndex = 99;
            this.btnExportInventario.Text = "Exportar Inventario";
            this.btnExportInventario.UseVisualStyleBackColor = true;
            this.btnExportInventario.Click += new System.EventHandler(this.btnExportInventario_Click);
            // 
            // grpDatosGenerales
            // 
            this.grpDatosGenerales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosGenerales.Controls.Add(this.txtNumPermiso);
            this.grpDatosGenerales.Controls.Add(this.txtModalidadPermiso);
            this.grpDatosGenerales.Controls.Add(this.txtCaracter);
            this.grpDatosGenerales.Controls.Add(this.txtRfcRepresentante);
            this.grpDatosGenerales.Controls.Add(this.txtRfcProveedor);
            this.grpDatosGenerales.Controls.Add(this.txtRfcContribuyente);
            this.grpDatosGenerales.Controls.Add(this.txtVersion);
            this.grpDatosGenerales.Controls.Add(this.txtInstalacion);
            this.grpDatosGenerales.Controls.Add(this.lblNumPermiso);
            this.grpDatosGenerales.Controls.Add(this.lblModalidadPermiso);
            this.grpDatosGenerales.Controls.Add(this.lblCaracter);
            this.grpDatosGenerales.Controls.Add(this.lblRfcRepresentante);
            this.grpDatosGenerales.Controls.Add(this.lblRfcProveedor);
            this.grpDatosGenerales.Controls.Add(this.lblRfcContribuyente);
            this.grpDatosGenerales.Controls.Add(this.lblVersion);
            this.grpDatosGenerales.Controls.Add(this.lblInstalacion);
            this.grpDatosGenerales.Location = new System.Drawing.Point(5, 32);
            this.grpDatosGenerales.Name = "grpDatosGenerales";
            this.grpDatosGenerales.Size = new System.Drawing.Size(933, 104);
            this.grpDatosGenerales.TabIndex = 0;
            this.grpDatosGenerales.TabStop = false;
            this.grpDatosGenerales.Text = "Datos Generales";
            // 
            // txtNumPermiso
            // 
            this.txtNumPermiso.Location = new System.Drawing.Point(643, 67);
            this.txtNumPermiso.Name = "txtNumPermiso";
            this.txtNumPermiso.ReadOnly = true;
            this.txtNumPermiso.Size = new System.Drawing.Size(271, 20);
            this.txtNumPermiso.TabIndex = 7;
            // 
            // txtModalidadPermiso
            // 
            this.txtModalidadPermiso.Location = new System.Drawing.Point(360, 67);
            this.txtModalidadPermiso.Name = "txtModalidadPermiso";
            this.txtModalidadPermiso.ReadOnly = true;
            this.txtModalidadPermiso.Size = new System.Drawing.Size(189, 20);
            this.txtModalidadPermiso.TabIndex = 6;
            // 
            // txtCaracter
            // 
            this.txtCaracter.Location = new System.Drawing.Point(77, 67);
            this.txtCaracter.Name = "txtCaracter";
            this.txtCaracter.ReadOnly = true;
            this.txtCaracter.Size = new System.Drawing.Size(172, 20);
            this.txtCaracter.TabIndex = 5;
            // 
            // txtRfcRepresentante
            // 
            this.txtRfcRepresentante.Location = new System.Drawing.Point(699, 42);
            this.txtRfcRepresentante.Name = "txtRfcRepresentante";
            this.txtRfcRepresentante.ReadOnly = true;
            this.txtRfcRepresentante.Size = new System.Drawing.Size(215, 20);
            this.txtRfcRepresentante.TabIndex = 4;
            // 
            // txtRfcProveedor
            // 
            this.txtRfcProveedor.Location = new System.Drawing.Point(334, 42);
            this.txtRfcProveedor.Name = "txtRfcProveedor";
            this.txtRfcProveedor.ReadOnly = true;
            this.txtRfcProveedor.Size = new System.Drawing.Size(215, 20);
            this.txtRfcProveedor.TabIndex = 3;
            // 
            // txtRfcContribuyente
            // 
            this.txtRfcContribuyente.Location = new System.Drawing.Point(77, 42);
            this.txtRfcContribuyente.Name = "txtRfcContribuyente";
            this.txtRfcContribuyente.ReadOnly = true;
            this.txtRfcContribuyente.Size = new System.Drawing.Size(172, 20);
            this.txtRfcContribuyente.TabIndex = 2;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(617, 18);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(138, 20);
            this.txtVersion.TabIndex = 1;
            // 
            // txtInstalacion
            // 
            this.txtInstalacion.Location = new System.Drawing.Point(77, 18);
            this.txtInstalacion.Name = "txtInstalacion";
            this.txtInstalacion.ReadOnly = true;
            this.txtInstalacion.Size = new System.Drawing.Size(472, 20);
            this.txtInstalacion.TabIndex = 0;
            // 
            // lblNumPermiso
            // 
            this.lblNumPermiso.AutoSize = true;
            this.lblNumPermiso.Location = new System.Drawing.Point(566, 69);
            this.lblNumPermiso.Name = "lblNumPermiso";
            this.lblNumPermiso.Size = new System.Drawing.Size(75, 13);
            this.lblNumPermiso.TabIndex = 8;
            this.lblNumPermiso.Text = "Núm. Permiso:";
            // 
            // lblModalidadPermiso
            // 
            this.lblModalidadPermiso.AutoSize = true;
            this.lblModalidadPermiso.Location = new System.Drawing.Point(257, 69);
            this.lblModalidadPermiso.Name = "lblModalidadPermiso";
            this.lblModalidadPermiso.Size = new System.Drawing.Size(99, 13);
            this.lblModalidadPermiso.TabIndex = 9;
            this.lblModalidadPermiso.Text = "Modalidad Permiso:";
            // 
            // lblCaracter
            // 
            this.lblCaracter.AutoSize = true;
            this.lblCaracter.Location = new System.Drawing.Point(9, 69);
            this.lblCaracter.Name = "lblCaracter";
            this.lblCaracter.Size = new System.Drawing.Size(50, 13);
            this.lblCaracter.TabIndex = 10;
            this.lblCaracter.Text = "Carácter:";
            // 
            // lblRfcRepresentante
            // 
            this.lblRfcRepresentante.AutoSize = true;
            this.lblRfcRepresentante.Location = new System.Drawing.Point(566, 45);
            this.lblRfcRepresentante.Name = "lblRfcRepresentante";
            this.lblRfcRepresentante.Size = new System.Drawing.Size(133, 13);
            this.lblRfcRepresentante.TabIndex = 11;
            this.lblRfcRepresentante.Text = "RFC Representante Legal:";
            // 
            // lblRfcProveedor
            // 
            this.lblRfcProveedor.AutoSize = true;
            this.lblRfcProveedor.Location = new System.Drawing.Point(257, 45);
            this.lblRfcProveedor.Name = "lblRfcProveedor";
            this.lblRfcProveedor.Size = new System.Drawing.Size(83, 13);
            this.lblRfcProveedor.TabIndex = 12;
            this.lblRfcProveedor.Text = "RFC Proveedor:";
            // 
            // lblRfcContribuyente
            // 
            this.lblRfcContribuyente.AutoSize = true;
            this.lblRfcContribuyente.Location = new System.Drawing.Point(9, 45);
            this.lblRfcContribuyente.Name = "lblRfcContribuyente";
            this.lblRfcContribuyente.Size = new System.Drawing.Size(31, 13);
            this.lblRfcContribuyente.TabIndex = 13;
            this.lblRfcContribuyente.Text = "RFC:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(566, 21);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 13);
            this.lblVersion.TabIndex = 14;
            this.lblVersion.Text = "Versión:";
            // 
            // lblInstalacion
            // 
            this.lblInstalacion.AutoSize = true;
            this.lblInstalacion.Location = new System.Drawing.Point(9, 21);
            this.lblInstalacion.Name = "lblInstalacion";
            this.lblInstalacion.Size = new System.Drawing.Size(61, 13);
            this.lblInstalacion.TabIndex = 15;
            this.lblInstalacion.Text = "Instalación:";
            // 
            // grpResumen
            // 
            this.grpResumen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResumen.Controls.Add(this.toolStrip1);
            this.grpResumen.Controls.Add(this.dgvResumenProducto);
            this.grpResumen.Location = new System.Drawing.Point(5, 142);
            this.grpResumen.Name = "grpResumen";
            this.grpResumen.Size = new System.Drawing.Size(933, 154);
            this.grpResumen.TabIndex = 1;
            this.grpResumen.TabStop = false;
            this.grpResumen.Text = "Resumen por Producto";
            // 
            // dgvResumenProducto
            // 
            this.dgvResumenProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResumenProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumenProducto.Location = new System.Drawing.Point(5, 19);
            this.dgvResumenProducto.Name = "dgvResumenProducto";
            this.dgvResumenProducto.ReadOnly = true;
            this.dgvResumenProducto.RowTemplate.Height = 25;
            this.dgvResumenProducto.Size = new System.Drawing.Size(922, 104);
            this.dgvResumenProducto.TabIndex = 0;
            this.dgvResumenProducto.DataSourceChanged += new System.EventHandler(this.dgvResumenProducto_DataSourceChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tsTotalProductos});
            this.toolStrip1.Location = new System.Drawing.Point(3, 126);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(927, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel2.Text = "Total de registros:";
            // 
            // tsTotalProductos
            // 
            this.tsTotalProductos.Name = "tsTotalProductos";
            this.tsTotalProductos.Size = new System.Drawing.Size(13, 22);
            this.tsTotalProductos.Text = "0";
            // 
            // grpRecepciones
            // 
            this.grpRecepciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRecepciones.Controls.Add(this.toolStrip2);
            this.grpRecepciones.Controls.Add(this.dgvRecepciones);
            this.grpRecepciones.Location = new System.Drawing.Point(5, 302);
            this.grpRecepciones.Name = "grpRecepciones";
            this.grpRecepciones.Size = new System.Drawing.Size(933, 190);
            this.grpRecepciones.TabIndex = 2;
            this.grpRecepciones.TabStop = false;
            this.grpRecepciones.Text = "Recepciones (CFDI)";
            // 
            // dgvRecepciones
            // 
            this.dgvRecepciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecepciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecepciones.Location = new System.Drawing.Point(5, 19);
            this.dgvRecepciones.Name = "dgvRecepciones";
            this.dgvRecepciones.ReadOnly = true;
            this.dgvRecepciones.RowTemplate.Height = 25;
            this.dgvRecepciones.Size = new System.Drawing.Size(922, 140);
            this.dgvRecepciones.TabIndex = 0;
            this.dgvRecepciones.DataSourceChanged += new System.EventHandler(this.dgvRecepciones_DataSourceChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsTotalRecepciones});
            this.toolStrip2.Location = new System.Drawing.Point(3, 162);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(927, 25);
            this.toolStrip2.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel1.Text = "Total de registros:";
            // 
            // tsTotalRecepciones
            // 
            this.tsTotalRecepciones.Name = "tsTotalRecepciones";
            this.tsTotalRecepciones.Size = new System.Drawing.Size(13, 22);
            this.tsTotalRecepciones.Text = "0";
            // 
            // tabVenta
            // 
            this.tabVenta.Controls.Add(this.btnExportVenta);
            this.tabVenta.Controls.Add(this.toolStrip3);
            this.tabVenta.Controls.Add(this.dgvVenta);
            this.tabVenta.Location = new System.Drawing.Point(4, 22);
            this.tabVenta.Name = "tabVenta";
            this.tabVenta.Padding = new System.Windows.Forms.Padding(3);
            this.tabVenta.Size = new System.Drawing.Size(942, 495);
            this.tabVenta.TabIndex = 1;
            this.tabVenta.Text = "Venta";
            this.tabVenta.UseVisualStyleBackColor = true;
            // 
            // btnExportVenta
            // 
            this.btnExportVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportVenta.Location = new System.Drawing.Point(826, 6);
            this.btnExportVenta.Name = "btnExportVenta";
            this.btnExportVenta.Size = new System.Drawing.Size(110, 23);
            this.btnExportVenta.TabIndex = 100;
            this.btnExportVenta.Text = "Exportar Venta";
            this.btnExportVenta.UseVisualStyleBackColor = true;
            this.btnExportVenta.Click += new System.EventHandler(this.btnExportVenta_Click);
            // 
            // dgvVenta
            // 
            this.dgvVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVenta.Location = new System.Drawing.Point(5, 35);
            this.dgvVenta.Name = "dgvVenta";
            this.dgvVenta.ReadOnly = true;
            this.dgvVenta.RowTemplate.Height = 25;
            this.dgvVenta.Size = new System.Drawing.Size(933, 429);
            this.dgvVenta.TabIndex = 0;
            this.dgvVenta.DataSourceChanged += new System.EventHandler(this.dgvVenta_DataSourceChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tsTotalVentas});
            this.toolStrip3.Location = new System.Drawing.Point(3, 467);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(936, 25);
            this.toolStrip3.TabIndex = 1;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel3.Text = "Total de registros:";
            // 
            // tsTotalVentas
            // 
            this.tsTotalVentas.Name = "tsTotalVentas";
            this.tsTotalVentas.Size = new System.Drawing.Size(13, 22);
            this.tsTotalVentas.Text = "0";
            // 
            // frmJsonExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 661);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFolder);
            this.Name = "frmJsonExplorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventario / Venta (JSON / ZIP / RAR)";
            this.Load += new System.EventHandler(this.frmJsonExplorer_Load);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabInventario.ResumeLayout(false);
            this.grpRecepciones.ResumeLayout(false);
            this.grpRecepciones.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).EndInit();
            this.grpResumen.ResumeLayout(false);
            this.grpResumen.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenProducto)).EndInit();
            this.grpDatosGenerales.ResumeLayout(false);
            this.grpDatosGenerales.PerformLayout();
            this.tabVenta.ResumeLayout(false);
            this.tabVenta.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.CheckBox chkStopOnFirstJson;
        private System.Windows.Forms.CheckBox chkOnlyFirstJsonPerArchive;

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabInventario;
        private System.Windows.Forms.TabPage tabVenta;

        // ✅ Botones por tab
        private System.Windows.Forms.Button btnExportInventario;
        private System.Windows.Forms.Button btnExportVenta;

        private System.Windows.Forms.GroupBox grpDatosGenerales;
        private System.Windows.Forms.Label lblInstalacion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblRfcContribuyente;
        private System.Windows.Forms.Label lblRfcProveedor;
        private System.Windows.Forms.Label lblRfcRepresentante;
        private System.Windows.Forms.Label lblCaracter;
        private System.Windows.Forms.Label lblModalidadPermiso;
        private System.Windows.Forms.Label lblNumPermiso;

        private System.Windows.Forms.TextBox txtInstalacion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtRfcContribuyente;
        private System.Windows.Forms.TextBox txtRfcProveedor;
        private System.Windows.Forms.TextBox txtRfcRepresentante;
        private System.Windows.Forms.TextBox txtCaracter;
        private System.Windows.Forms.TextBox txtModalidadPermiso;
        private System.Windows.Forms.TextBox txtNumPermiso;

        private System.Windows.Forms.GroupBox grpResumen;
        private System.Windows.Forms.DataGridView dgvResumenProducto;

        private System.Windows.Forms.GroupBox grpRecepciones;
        private System.Windows.Forms.DataGridView dgvRecepciones;

        private System.Windows.Forms.DataGridView dgvVenta;

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tsTotalRecepciones;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel tsTotalProductos;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel tsTotalVentas;
    }
}
