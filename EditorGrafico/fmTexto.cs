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
    public partial class fmTexto : Form
    {
        public SolidBrush colortexto;
        public Font mifuente; // Tipo fuente Para la figura de Texto
        
        public fmTexto()
        {
            InitializeComponent();
        }

        private void fmTexto_Load(object sender, EventArgs e)
        {
            colortexto = new SolidBrush(Color.Black);
            mifuente = new Font("Arial", 15);
        }

        private void btFuente_Click(object sender, EventArgs e)
        {
            colortexto = new SolidBrush(Color.Black);//
            mifuente = new Font("Arial", 15);
            dlgFuente.Font = mifuente;
            if (dlgFuente.ShowDialog() == DialogResult.OK)
            {
                mifuente = dlgFuente.Font;
                colortexto.Color = dlgFuente.Color;
            }
        }
    }
}
