namespace Presentacion
{
    partial class formLectorDiarios
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
            this.dgvNodes = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.chkStopOnFirstJson = new System.Windows.Forms.CheckBox();
            this.chkOnlyFirstJsonPerArchive = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodes)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(12, 12);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(720, 23);
            this.txtFolder.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(738, 11);
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
            this.btnProcess.Location = new System.Drawing.Point(854, 11);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(110, 25);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Procesar";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // dgvNodes
            // 
            this.dgvNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNodes.Location = new System.Drawing.Point(12, 88);
            this.dgvNodes.Name = "dgvNodes";
            this.dgvNodes.ReadOnly = true;
            this.dgvNodes.RowTemplate.Height = 25;
            this.dgvNodes.Size = new System.Drawing.Size(952, 470);
            this.dgvNodes.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(12, 59);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(952, 23);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Listo.";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 41);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(720, 15);
            this.progressBar.TabIndex = 5;
            // 
            // chkStopOnFirstJson
            // 
            this.chkStopOnFirstJson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkStopOnFirstJson.AutoSize = true;
            this.chkStopOnFirstJson.Location = new System.Drawing.Point(738, 39);
            this.chkStopOnFirstJson.Name = "chkStopOnFirstJson";
            this.chkStopOnFirstJson.Size = new System.Drawing.Size(226, 19);
            this.chkStopOnFirstJson.TabIndex = 6;
            this.chkStopOnFirstJson.Text = "Detener al encontrar el primer .json";
            this.chkStopOnFirstJson.UseVisualStyleBackColor = true;
            // 
            // chkOnlyFirstJsonPerArchive
            // 
            this.chkOnlyFirstJsonPerArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOnlyFirstJsonPerArchive.AutoSize = true;
            this.chkOnlyFirstJsonPerArchive.Checked = true;
            this.chkOnlyFirstJsonPerArchive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlyFirstJsonPerArchive.Location = new System.Drawing.Point(738, 62);
            this.chkOnlyFirstJsonPerArchive.Name = "chkOnlyFirstJsonPerArchive";
            this.chkOnlyFirstJsonPerArchive.Size = new System.Drawing.Size(228, 19);
            this.chkOnlyFirstJsonPerArchive.TabIndex = 7;
            this.chkOnlyFirstJsonPerArchive.Text = "Solo 1 .json por cada archivo comprimido";
            this.chkOnlyFirstJsonPerArchive.UseVisualStyleBackColor = true;
            // 
            // frmJsonExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 570);
            this.Controls.Add(this.chkOnlyFirstJsonPerArchive);
            this.Controls.Add(this.chkStopOnFirstJson);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dgvNodes);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFolder);
            this.Name = "formLectorDiarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JSON Explorer (carpeta -> zip/rar -> json -> nodos)";
          //  this.Load += new System.EventHandler(this.formLectorDiarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.DataGridView dgvNodes;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox chkStopOnFirstJson;
        private System.Windows.Forms.CheckBox chkOnlyFirstJsonPerArchive;
    }
}
