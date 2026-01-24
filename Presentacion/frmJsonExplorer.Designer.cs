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
            this.dgvInventario = new System.Windows.Forms.DataGridView();
            this.tabVenta = new System.Windows.Forms.TabPage();
            this.dgvVenta = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsTotalRegistrosInventario = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsTotalRegistrosVenta = new System.Windows.Forms.ToolStripLabel();
            this.groupBoxOptions.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabInventario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).BeginInit();
            this.tabVenta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
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
            this.tabMain.Size = new System.Drawing.Size(950, 451);
            this.tabMain.TabIndex = 7;
            // 
            // tabInventario
            // 
            this.tabInventario.Controls.Add(this.toolStrip1);
            this.tabInventario.Controls.Add(this.dgvInventario);
            this.tabInventario.Location = new System.Drawing.Point(4, 22);
            this.tabInventario.Name = "tabInventario";
            this.tabInventario.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventario.Size = new System.Drawing.Size(942, 425);
            this.tabInventario.TabIndex = 0;
            this.tabInventario.Text = "Inventario";
            this.tabInventario.UseVisualStyleBackColor = true;
            // 
            // dgvInventario
            // 
            this.dgvInventario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventario.Location = new System.Drawing.Point(5, 5);
            this.dgvInventario.Name = "dgvInventario";
            this.dgvInventario.ReadOnly = true;
            this.dgvInventario.RowTemplate.Height = 25;
            this.dgvInventario.Size = new System.Drawing.Size(933, 416);
            this.dgvInventario.TabIndex = 0;
            this.dgvInventario.DataSourceChanged += new System.EventHandler(this.dgvInventario_DataSourceChanged);
            // 
            // tabVenta
            // 
            this.tabVenta.Controls.Add(this.toolStrip2);
            this.tabVenta.Controls.Add(this.dgvVenta);
            this.tabVenta.Location = new System.Drawing.Point(4, 22);
            this.tabVenta.Name = "tabVenta";
            this.tabVenta.Padding = new System.Windows.Forms.Padding(3);
            this.tabVenta.Size = new System.Drawing.Size(942, 425);
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
            this.dgvVenta.Location = new System.Drawing.Point(5, 5);
            this.dgvVenta.Name = "dgvVenta";
            this.dgvVenta.ReadOnly = true;
            this.dgvVenta.RowTemplate.Height = 25;
            this.dgvVenta.Size = new System.Drawing.Size(933, 416);
            this.dgvVenta.TabIndex = 0;
            this.dgvVenta.DataSourceChanged += new System.EventHandler(this.dgvVenta_DataSourceChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsTotalRegistrosInventario});
            this.toolStrip1.Location = new System.Drawing.Point(3, 397);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(936, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel1.Text = "Total de registros:";
            // 
            // tsTotalRegistrosInventario
            // 
            this.tsTotalRegistrosInventario.Name = "tsTotalRegistrosInventario";
            this.tsTotalRegistrosInventario.Size = new System.Drawing.Size(13, 22);
            this.tsTotalRegistrosInventario.Text = "0";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tsTotalRegistrosVenta});
            this.toolStrip2.Location = new System.Drawing.Point(3, 397);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(936, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(99, 22);
            this.toolStripLabel2.Text = "Total de registros:";
            // 
            // tsTotalRegistrosVenta
            // 
            this.tsTotalRegistrosVenta.Name = "tsTotalRegistrosVenta";
            this.tsTotalRegistrosVenta.Size = new System.Drawing.Size(13, 22);
            this.tsTotalRegistrosVenta.Text = "0";
            // 
            // frmJsonExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 591);
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
            this.tabInventario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventario)).EndInit();
            this.tabVenta.ResumeLayout(false);
            this.tabVenta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
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

        private System.Windows.Forms.DataGridView dgvInventario;
        private System.Windows.Forms.DataGridView dgvVenta;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel tsTotalRegistrosInventario;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel tsTotalRegistrosVenta;
    }
}
