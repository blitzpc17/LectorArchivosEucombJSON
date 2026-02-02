namespace Presentacion
{
    partial class frmRecepcionesCfdiViewer
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
            this.groupBoxFilters = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.cboClaveSubProducto = new System.Windows.Forms.ComboBox();
            this.lblClaveSub = new System.Windows.Forms.Label();
            this.txtUuidFilter = new System.Windows.Forms.TextBox();
            this.lblUuid = new System.Windows.Forms.Label();
            this.dgvCfdis = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsTotal = new System.Windows.Forms.ToolStripLabel();
            this.groupBoxOptions.SuspendLayout();
            this.groupBoxFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCfdis)).BeginInit();
            this.toolStrip1.SuspendLayout();
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
            // groupBoxFilters
            // 
            this.groupBoxFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFilters.Controls.Add(this.btnClear);
            this.groupBoxFilters.Controls.Add(this.cboClaveSubProducto);
            this.groupBoxFilters.Controls.Add(this.lblClaveSub);
            this.groupBoxFilters.Controls.Add(this.txtUuidFilter);
            this.groupBoxFilters.Controls.Add(this.lblUuid);
            this.groupBoxFilters.Location = new System.Drawing.Point(10, 128);
            this.groupBoxFilters.Name = "groupBoxFilters";
            this.groupBoxFilters.Size = new System.Drawing.Size(950, 55);
            this.groupBoxFilters.TabIndex = 7;
            this.groupBoxFilters.TabStop = false;
            this.groupBoxFilters.Text = "Filtros";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(864, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Limpiar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cboClaveSubProducto
            // 
            this.cboClaveSubProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClaveSubProducto.FormattingEnabled = true;
            this.cboClaveSubProducto.Location = new System.Drawing.Point(533, 20);
            this.cboClaveSubProducto.Name = "cboClaveSubProducto";
            this.cboClaveSubProducto.Size = new System.Drawing.Size(250, 21);
            this.cboClaveSubProducto.TabIndex = 3;
            // 
            // lblClaveSub
            // 
            this.lblClaveSub.AutoSize = true;
            this.lblClaveSub.Location = new System.Drawing.Point(433, 23);
            this.lblClaveSub.Name = "lblClaveSub";
            this.lblClaveSub.Size = new System.Drawing.Size(94, 13);
            this.lblClaveSub.TabIndex = 2;
            this.lblClaveSub.Text = "ClaveSubProducto:";
            // 
            // txtUuidFilter
            // 
            this.txtUuidFilter.Location = new System.Drawing.Point(84, 20);
            this.txtUuidFilter.Name = "txtUuidFilter";
            this.txtUuidFilter.Size = new System.Drawing.Size(330, 20);
            this.txtUuidFilter.TabIndex = 1;
            // 
            // lblUuid
            // 
            this.lblUuid.AutoSize = true;
            this.lblUuid.Location = new System.Drawing.Point(10, 23);
            this.lblUuid.Name = "lblUuid";
            this.lblUuid.Size = new System.Drawing.Size(68, 13);
            this.lblUuid.TabIndex = 0;
            this.lblUuid.Text = "Buscar UUID:";
            // 
            // dgvCfdis
            // 
            this.dgvCfdis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCfdis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCfdis.Location = new System.Drawing.Point(10, 190);
            this.dgvCfdis.Name = "dgvCfdis";
            this.dgvCfdis.ReadOnly = true;
            this.dgvCfdis.RowTemplate.Height = 25;
            this.dgvCfdis.Size = new System.Drawing.Size(950, 445);
            this.dgvCfdis.TabIndex = 8;
            this.dgvCfdis.DataSourceChanged += new System.EventHandler(this.dgvCfdis_DataSourceChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsTotal});
            this.toolStrip1.Location = new System.Drawing.Point(0, 636);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(970, 25);
            this.toolStrip1.TabIndex = 9;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel1.Text = "Total de registros:";
            // 
            // tsTotal
            // 
            this.tsTotal.Name = "tsTotal";
            this.tsTotal.Size = new System.Drawing.Size(13, 22);
            this.tsTotal.Text = "0";
            // 
            // frmRecepcionesCfdiViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 661);
            this.Controls.Add(this.dgvCfdis);
            this.Controls.Add(this.groupBoxFilters);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmRecepcionesCfdiViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Viewer CFDIs de Recepciones (Archivo + Producto)";
            this.Load += new System.EventHandler(this.frmRecepcionesCfdiViewer_Load);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.groupBoxFilters.ResumeLayout(false);
            this.groupBoxFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCfdis)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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

        private System.Windows.Forms.GroupBox groupBoxFilters;
        private System.Windows.Forms.Label lblUuid;
        private System.Windows.Forms.TextBox txtUuidFilter;
        private System.Windows.Forms.Label lblClaveSub;
        private System.Windows.Forms.ComboBox cboClaveSubProducto;
        private System.Windows.Forms.Button btnClear;

        private System.Windows.Forms.DataGridView dgvCfdis;

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tsTotal;
    }
}
