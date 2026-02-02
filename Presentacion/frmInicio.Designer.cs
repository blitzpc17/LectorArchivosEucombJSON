namespace Presentacion
{
    partial class frmInicio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLector = new System.Windows.Forms.Button();
            this.btnRecepcciones = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLector
            // 
            this.btnLector.Location = new System.Drawing.Point(52, 30);
            this.btnLector.Name = "btnLector";
            this.btnLector.Size = new System.Drawing.Size(75, 23);
            this.btnLector.TabIndex = 0;
            this.btnLector.Text = "Lector Json";
            this.btnLector.UseVisualStyleBackColor = true;
            this.btnLector.Click += new System.EventHandler(this.btnLector_Click);
            // 
            // btnRecepcciones
            // 
            this.btnRecepcciones.Location = new System.Drawing.Point(52, 76);
            this.btnRecepcciones.Name = "btnRecepcciones";
            this.btnRecepcciones.Size = new System.Drawing.Size(75, 23);
            this.btnRecepcciones.TabIndex = 1;
            this.btnRecepcciones.Text = "Recepciones";
            this.btnRecepcciones.UseVisualStyleBackColor = true;
            this.btnRecepcciones.Click += new System.EventHandler(this.btnRecepcciones_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(52, 126);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 161);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnRecepcciones);
            this.Controls.Add(this.btnLector);
            this.MaximumSize = new System.Drawing.Size(200, 200);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Name = "frmInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLector;
        private System.Windows.Forms.Button btnRecepcciones;
        private System.Windows.Forms.Button btnSalir;
    }
}