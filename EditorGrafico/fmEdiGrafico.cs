using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorGrafico
{
    public partial class fmEdiGrafico : Form
    {
        Rectangle rectInvalido; // Figuras intermedias de dibujado
        Rectangle rect;
        bool pulsado, rellenando; // Para controlar botón ratón y relleno
        Point[] puntos; // Se usa para lápiz y goma
        int actualX, actualY, OrigenX, OrigenY; // Coordenadas del dibujo 
        Pen lapiz, goma; // línea Contorno con ancho, color, estilo y goma con fondo y ancho
        SolidBrush relleno, colorTexto; // Rellenos 
        HatchBrush estilorelleno; // Estilo del relleno
        Bitmap mibmp; // Explicado en anterior proyecto
        Graphics g1, g2; //Superficie de dibujado no permanente 1º, permanente 2º
        string accion; //Tipos Figuras: 
                       // Linea, Trazo, Goma, Rectangulo, Circulo, Elipse, Texto, BorrarSeleccion
        Font mifuente; // Tipo fuente Para la figura de Texto
        string mitexto; // Texto a dibujar
        Bitmap CuadradoBoton; // Gráfico que se muestra en botones de la barra Estándar

        public fmEdiGrafico()
        {
            InitializeComponent();
        }
        private void Inicializar()
        {
            accion = "Trazo"; // Valores por defecto en instrumentos de dibujado
            puntos = new Point[0];
            lapiz = new Pen(Color.Black, 1); // color y grosor de la línea 
            colorTexto = new SolidBrush(Color.FromArgb(0, 0, 0));
            goma = new Pen(Color.White, 10);
            relleno = new SolidBrush(Color.FromArgb(255, 255, 255));//
            mibmp = new Bitmap(pbEdigrafi.Width, pbEdigrafi.Height);
            g1 = pbEdigrafi.CreateGraphics(); // Graphics de formas provisionales
            g2 = Graphics.FromImage(mibmp); // Formas definitivas
            pbEdigrafi.Image = mibmp;
            pbEdigrafi.BackColor = Color.White;
            CrearCursorLapiz();
            rellenando = false;
            desmarca(); // desmarcamos todo 
            tsbTrazo.Checked = true; //marcamos figura por defecto
            itTrazo.Checked = true;
            itLinea5.Checked = true; // Del ítem de menú Estilo/Tipo Línea/sólido 
            itSinRelleno.Checked = true; // Del ítem de menú Estilo/Tipo Relleno 
            dlgColores.Color = Color.White;
            CreaCuadradoBoton(); // Creamos y ponemos imagen de cuadro de color inicial
            tsbColorFondo.Image = CuadradoBoton; // en estos botónes
            tsbColorlapiz.Image = CuadradoBoton;
            tsbColorRelleno.Image = CuadradoBoton;
            tsl2.Text = "Grosor Linea: " + lapiz.Width.ToString();//Nombre Statuslabels 
            tsl3.Text = "Grosor Goma: " + goma.Width.ToString();// de statusstrip
        }


        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void fmEdiGrafico_Load(object sender, EventArgs e)
        {
            Inicializar();
        }

        private void itNuevo_Click(object sender, EventArgs e)
        {
            Inicializar();
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
