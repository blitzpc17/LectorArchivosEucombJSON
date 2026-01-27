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
            this.grpRecepciones = new System.Windows.Forms.GroupBox();
            this.dgvRecepciones = new System.Windows.Forms.DataGridView();
            this.grpResumen = new System.Windows.Forms.GroupBox();
            this.dgvResumenProducto = new System.Windows.Forms.DataGridView();
            this.grpDatosGenerales = new System.Windows.Forms.GroupBox();
            this.lblInstalacion = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblRfcContribuyente = new System.Windows.Forms.Label();
            this.lblRfcProveedor = new System.Windows.Forms.Label();
            this.lblRfcRepresentante = new System.Windows.Forms.Label();
            this.lblCaracter = new System.Windows.Forms.Label();
            this.lblModalidadPermiso = new System.Windows.Forms.Label();
            this.lblNumPermiso = new System.Windows.Forms.Label();
            this.txtInstalacion = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtRfcContribuyente = new System.Windows.Forms.TextBox();
            this.txtRfcProveedor = new System.Windows.Forms.TextBox();
            this.txtRfcRepresentante = new System.Windows.Forms.TextBox();
            this.txtCaracter = new System.Windows.Forms.TextBox();
            this.txtModalidadPermiso = new System.Windows.Forms.TextBox();
            this.txtNumPermiso = new System.Windows.Forms.TextBox();
            this.tabVenta = new System.Windows.Forms.TabPage();
            this.dgvVenta = new System.Windows.Forms.DataGridView();
            this.groupBoxOptions.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabInventario.SuspendLayout();
            this.grpRecepciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).BeginInit();
            this.grpResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenProducto)).BeginInit();
            this.grpDatosGenerales.SuspendLayout();
            this.tabVenta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(12, 12);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(760, 23);
            this.txtFolder.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(778, 11);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(110, 25);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Elegir carpeta";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.Location = new System.Drawing.Point(894, 11);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(110, 25);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Procesar";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(1010, 11);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 25);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Exportar Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 41);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1108, 14);
            this.progressBar.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(12, 58);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1108, 20);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Listo.";
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.Controls.Add(this.chkStopOnFirstJson);
            this.groupBoxOptions.Controls.Add(this.chkOnlyFirstJsonPerArchive);
            this.groupBoxOptions.Location = new System.Drawing.Point(12, 81);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(1108, 63);
            this.groupBoxOptions.TabIndex = 6;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Opciones";
            // 
            // chkStopOnFirstJson
            // 
            this.chkStopOnFirstJson.AutoSize = true;
            this.chkStopOnFirstJson.Location = new System.Drawing.Point(12, 22);
            this.chkStopOnFirstJson.Name = "chkStopOnFirstJson";
            this.chkStopOnFirstJson.Size = new System.Drawing.Size(226, 19);
            this.chkStopOnFirstJson.TabIndex = 0;
            this.chkStopOnFirstJson.Text = "Detener al encontrar el primer .json";
            this.chkStopOnFirstJson.UseVisualStyleBackColor = true;
            // 
            // chkOnlyFirstJsonPerArchive
            // 
            this.chkOnlyFirstJsonPerArchive.AutoSize = true;
            this.chkOnlyFirstJsonPerArchive.Checked = true;
            this.chkOnlyFirstJsonPerArchive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlyFirstJsonPerArchive.Location = new System.Drawing.Point(12, 45);
            this.chkOnlyFirstJsonPerArchive.Name = "chkOnlyFirstJsonPerArchive";
            this.chkOnlyFirstJsonPerArchive.Size = new System.Drawing.Size(328, 19);
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
            this.tabMain.Location = new System.Drawing.Point(12, 150);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1108, 520);
            this.tabMain.TabIndex = 7;
            // 
            // tabInventario
            // 
            this.tabInventario.Controls.Add(this.grpRecepciones);
            this.tabInventario.Controls.Add(this.grpResumen);
            this.tabInventario.Controls.Add(this.grpDatosGenerales);
            this.tabInventario.Location = new System.Drawing.Point(4, 24);
            this.tabInventario.Name = "tabInventario";
            this.tabInventario.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventario.Size = new System.Drawing.Size(1100, 492);
            this.tabInventario.TabIndex = 0;
            this.tabInventario.Text = "Inventario";
            this.tabInventario.UseVisualStyleBackColor = true;
            // 
            // grpRecepciones
            // 
            this.grpRecepciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRecepciones.Controls.Add(this.dgvRecepciones);
            this.grpRecepciones.Location = new System.Drawing.Point(6, 250);
            this.grpRecepciones.Name = "grpRecepciones";
            this.grpRecepciones.Size = new System.Drawing.Size(1088, 236);
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
            this.dgvRecepciones.Location = new System.Drawing.Point(6, 22);
            this.dgvRecepciones.Name = "dgvRecepciones";
            this.dgvRecepciones.ReadOnly = true;
            this.dgvRecepciones.RowTemplate.Height = 25;
            this.dgvRecepciones.Size = new System.Drawing.Size(1076, 208);
            this.dgvRecepciones.TabIndex = 0;
            // 
            // grpResumen
            // 
            this.grpResumen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpResumen.Controls.Add(this.dgvResumenProducto);
            this.grpResumen.Location = new System.Drawing.Point(6, 132);
            this.grpResumen.Name = "grpResumen";
            this.grpResumen.Size = new System.Drawing.Size(1088, 112);
            this.grpResumen.TabIndex = 1;
            this.grpResumen.TabStop = false;
            this.grpResumen.Text = "Resumen por Producto";
            // 
            // dgvResumenProducto
            // 
            this.dgvResumenProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResumenProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumenProducto.Location = new System.Drawing.Point(6, 22);
            this.dgvResumenProducto.Name = "dgvResumenProducto";
            this.dgvResumenProducto.ReadOnly = true;
            this.dgvResumenProducto.RowTemplate.Height = 25;
            this.dgvResumenProducto.Size = new System.Drawing.Size(1076, 84);
            this.dgvResumenProducto.TabIndex = 0;
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
            this.grpDatosGenerales.Location = new System.Drawing.Point(6, 6);
            this.grpDatosGenerales.Name = "grpDatosGenerales";
            this.grpDatosGenerales.Size = new System.Drawing.Size(1088, 120);
            this.grpDatosGenerales.TabIndex = 0;
            this.grpDatosGenerales.TabStop = false;
            this.grpDatosGenerales.Text = "Datos Generales";
            // 
            // Labels + Textboxes layout
            // 
            this.lblInstalacion.AutoSize = true;
            this.lblInstalacion.Location = new System.Drawing.Point(10, 24);
            this.lblInstalacion.Name = "lblInstalacion";
            this.lblInstalacion.Size = new System.Drawing.Size(66, 15);
            this.lblInstalacion.Text = "Instalación:";
            this.txtInstalacion.Location = new System.Drawing.Point(90, 21);
            this.txtInstalacion.Name = "txtInstalacion";
            this.txtInstalacion.ReadOnly = true;
            this.txtInstalacion.Size = new System.Drawing.Size(550, 23);
            this.txtInstalacion.TabIndex = 0;

            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(660, 24);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(48, 15);
            this.lblVersion.Text = "Versión:";
            this.txtVersion.Location = new System.Drawing.Point(720, 21);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(160, 23);
            this.txtVersion.TabIndex = 1;

            this.lblRfcContribuyente.AutoSize = true;
            this.lblRfcContribuyente.Location = new System.Drawing.Point(10, 52);
            this.lblRfcContribuyente.Name = "lblRfcContribuyente";
            this.lblRfcContribuyente.Size = new System.Drawing.Size(31, 15);
            this.lblRfcContribuyente.Text = "RFC:";
            this.txtRfcContribuyente.Location = new System.Drawing.Point(90, 49);
            this.txtRfcContribuyente.Name = "txtRfcContribuyente";
            this.txtRfcContribuyente.ReadOnly = true;
            this.txtRfcContribuyente.Size = new System.Drawing.Size(200, 23);
            this.txtRfcContribuyente.TabIndex = 2;

            this.lblRfcProveedor.AutoSize = true;
            this.lblRfcProveedor.Location = new System.Drawing.Point(300, 52);
            this.lblRfcProveedor.Name = "lblRfcProveedor";
            this.lblRfcProveedor.Size = new System.Drawing.Size(82, 15);
            this.lblRfcProveedor.Text = "RFC Proveedor:";
            this.txtRfcProveedor.Location = new System.Drawing.Point(390, 49);
            this.txtRfcProveedor.Name = "txtRfcProveedor";
            this.txtRfcProveedor.ReadOnly = true;
            this.txtRfcProveedor.Size = new System.Drawing.Size(250, 23);
            this.txtRfcProveedor.TabIndex = 3;

            this.lblRfcRepresentante.AutoSize = true;
            this.lblRfcRepresentante.Location = new System.Drawing.Point(660, 52);
            this.lblRfcRepresentante.Name = "lblRfcRepresentante";
            this.lblRfcRepresentante.Size = new System.Drawing.Size(148, 15);
            this.lblRfcRepresentante.Text = "RFC Representante Legal:";
            this.txtRfcRepresentante.Location = new System.Drawing.Point(815, 49);
            this.txtRfcRepresentante.Name = "txtRfcRepresentante";
            this.txtRfcRepresentante.ReadOnly = true;
            this.txtRfcRepresentante.Size = new System.Drawing.Size(250, 23);
            this.txtRfcRepresentante.TabIndex = 4;

            this.lblCaracter.AutoSize = true;
            this.lblCaracter.Location = new System.Drawing.Point(10, 80);
            this.lblCaracter.Name = "lblCaracter";
            this.lblCaracter.Size = new System.Drawing.Size(55, 15);
            this.lblCaracter.Text = "Carácter:";
            this.txtCaracter.Location = new System.Drawing.Point(90, 77);
            this.txtCaracter.Name = "txtCaracter";
            this.txtCaracter.ReadOnly = true;
            this.txtCaracter.Size = new System.Drawing.Size(200, 23);
            this.txtCaracter.TabIndex = 5;

            this.lblModalidadPermiso.AutoSize = true;
            this.lblModalidadPermiso.Location = new System.Drawing.Point(300, 80);
            this.lblModalidadPermiso.Name = "lblModalidadPermiso";
            this.lblModalidadPermiso.Size = new System.Drawing.Size(111, 15);
            this.lblModalidadPermiso.Text = "Modalidad Permiso:";
            this.txtModalidadPermiso.Location = new System.Drawing.Point(420, 77);
            this.txtModalidadPermiso.Name = "txtModalidadPermiso";
            this.txtModalidadPermiso.ReadOnly = true;
            this.txtModalidadPermiso.Size = new System.Drawing.Size(220, 23);
            this.txtModalidadPermiso.TabIndex = 6;

            this.lblNumPermiso.AutoSize = true;
            this.lblNumPermiso.Location = new System.Drawing.Point(660, 80);
            this.lblNumPermiso.Name = "lblNumPermiso";
            this.lblNumPermiso.Size = new System.Drawing.Size(84, 15);
            this.lblNumPermiso.Text = "Núm. Permiso:";
            this.txtNumPermiso.Location = new System.Drawing.Point(750, 77);
            this.txtNumPermiso.Name = "txtNumPermiso";
            this.txtNumPermiso.ReadOnly = true;
            this.txtNumPermiso.Size = new System.Drawing.Size(315, 23);
            this.txtNumPermiso.TabIndex = 7;

            // 
            // tabVenta
            // 
            this.tabVenta.Controls.Add(this.dgvVenta);
            this.tabVenta.Location = new System.Drawing.Point(4, 24);
            this.tabVenta.Name = "tabVenta";
            this.tabVenta.Padding = new System.Windows.Forms.Padding(3);
            this.tabVenta.Size = new System.Drawing.Size(1100, 492);
            this.tabVenta.TabIndex = 1;
            this.tabVenta.Text = "Venta";
            this.tabVenta.UseVisualStyleBackColor = true;
            // 
            // dgvVenta
            // 
            this.dgvVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVenta.Location = new System.Drawing.Point(6, 6);
            this.dgvVenta.Name = "dgvVenta";
            this.dgvVenta.ReadOnly = true;
            this.dgvVenta.RowTemplate.Height = 25;
            this.dgvVenta.Size = new System.Drawing.Size(1088, 480);
            this.dgvVenta.TabIndex = 0;
            // 
            // frmJsonExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 682);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).EndInit();
            this.grpResumen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumenProducto)).EndInit();
            this.grpDatosGenerales.ResumeLayout(false);
            this.grpDatosGenerales.PerformLayout();
            this.tabVenta.ResumeLayout(false);
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

        // Inventario - controles nuevos
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

        // Venta
        private System.Windows.Forms.DataGridView dgvVenta;
    }
}
