using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void btnLector_Click(object sender, EventArgs e)
        {
            var frm = new frmJsonExplorer();
            frm.ShowDialog(this);
        }

        private void btnRecepcciones_Click(object sender, EventArgs e)
        {
            var frm = new frmRecepcionesCfdiViewer();
            frm.ShowDialog(this);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
