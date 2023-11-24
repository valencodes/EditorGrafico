using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorGrafico
{
    public partial class fmEdiGrafico : Form
    {
        public fmEdiGrafico()
        {
            InitializeComponent();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click_2(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void barraEstandarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            itBarraEstandar.Checked = !itBarraEstandar.Checked;
            itcBarraEstandar.Checked = !itcBarraEstandar.Checked;
            // Mostramos barra correspondiente o no
            tsEstandar.Visible = itBarraEstandar.Checked;
        }

        private void itBarraLateral_Click(object sender, EventArgs e)
        {
            itBarraLateral.Checked = !itBarraLateral.Checked;
            itcBarraLateral.Checked = !itcBarraLateral.Checked;
            // Mostramos barra correspondiente o no
            tsLateral.Visible = itBarraLateral.Checked;
        }

        private void itBarraEstado_Click(object sender, EventArgs e)
        {
            itBarraEstado.Checked = !itBarraEstado.Checked;
            itcBarraEstado.Checked = !itcBarraEstado.Checked;
            // Mostramos barra correspondiente o no
            stEstado.Visible = itBarraEstado.Checked;
        }

        private void fmEdiGrafico_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
}
