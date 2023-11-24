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
    public partial class fmTamanyo : Form
    {
        public fmTamanyo()
        {
            InitializeComponent();
        }

        private void tbIzq_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btAceptar.PerformClick();
            }
            switch (e.KeyChar)
            {
                case (char)8:
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '0':
                case ',': break;
                default:
                    e.KeyChar = (char)0; //anulamos la pulsación
                    break;
            }
        }
    }
}
