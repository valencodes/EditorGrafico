using EditorGrafico.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorGrafico
{
    public partial class fmEdiGrafico : Form
    {

        fmTexto VentanaTexto = new fmTexto();
        fmTamanyo VentanaTamanyo = new fmTamanyo();
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
        private bool cambios = false;

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

        private void desmarca()
        {
            foreach (ToolStripButton boton in tsLateral.Items)
            { // desmarcamos botones laterales de figuras 
                boton.Checked = false;
            }
            // Desmarcamos botones e ítems de figuras que no están 
            tsbGoma.Checked = false; // en el mismo submenú o barra de botones
            itGoma.Checked = false;
            tsbBorrarSeleccion.Checked = false;
            itBorrarSeleccion.Checked = false;
            desmarcamenu(itFiguras.DropDownItems); // Desmarcamos toda la opción de menú
        }

        private void desmarcamenu(ToolStripItemCollection listamenu)
        {
            foreach (ToolStripItem miItem in listamenu)
            {
                ((ToolStripMenuItem)miItem).Checked = false;
            }
        }

        private void CreaCuadradoBoton()
        {
            Bitmap btemp = new Bitmap(15, 15);
            Graphics gtemp = Graphics.FromImage(btemp);
            gtemp.FillRectangle(new SolidBrush(dlgColores.Color), 0, 0, 16, 16);
            CuadradoBoton = new Bitmap(btemp);
            gtemp.Dispose();
            btemp.Dispose();
        }

        private void CrearCursorLapiz()
        {
            IntPtr intprCursor = Properties.Resources.lapizcortoNuevo1.GetHicon();
            pbEdigrafi.Cursor = new Cursor(intprCursor);
        }

        private void CrearCursorGoma()
        {
            // Width es float parámetro bitmap es int32
            int diametroG = Convert.ToInt32(goma.Width);
            Bitmap bmpGoma = new Bitmap(diametroG + 1, diametroG + 1);
            SolidBrush rellenoborra = new SolidBrush(pbEdigrafi.BackColor);
            Graphics gGoma = Graphics.FromImage(bmpGoma);
            gGoma.FillRectangle(rellenoborra, 1, 1, diametroG - 1, diametroG - 1);
            gGoma.DrawRectangle(new Pen(Color.Black, 1), 0, 0, diametroG, diametroG);
            gGoma.Dispose();
            IntPtr intprCursorGoma = bmpGoma.GetHicon();
            pbEdigrafi.Cursor = new Cursor(intprCursorGoma);
        }

        private void agregarPunto(Point punto)
        {
            //agregamos los puntos obtenidos sobre variable global puntos
            Point[] temp = new Point[puntos.Length + 1];
            puntos.CopyTo(temp, 0);
            puntos = temp;
            puntos[puntos.Length - 1] = punto;
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
            if (cambios)
            {
                DialogResult resultado = MessageBox.Show("Hay Cambios sin Guardar. Guardas ? ", "Guardar Cambios", MessageBoxButtons.YesNoCancel);
                switch (resultado)
                {
                    case DialogResult.Yes: // Si contesta si
                        itGuardar.PerformClick();// Invocamos evento botón guardar 
                        break; //Después de Guardar Continuamos con operación de nuevo
                    case DialogResult.Cancel: // Si decide cancelar 
                        return; // Abortamos operación de nuevo
                }
            }
            Inicializar();
        }
        

        private void tsbLinea_Click(object sender, EventArgs e)
        {
            desmarca();
            if (sender is ToolStripMenuItem)//true al hacer click en cualquier ítem de menú 
            {
                ToolStripMenuItem elemento = sender as ToolStripMenuItem; // Guardamos ítem 
                elemento.Checked = true; // clikado usando sender 
                accion = elemento.Name.Substring(2, elemento.Name.Length - 2); // guardamos 
                                                                               //en variable accion la figura a dibujar que extraemos del nombre del ítem
                foreach (ToolStripButton boton in tsLateral.Items)
                { // Localizamos a través del tag desde el ítem el botón de igual figura
                    if (boton.Tag == elemento.Tag) // mismo tag en botón e ítem
                        boton.Checked = true;
                }
            }
            else//sucede cuando hace clik en botón y el código es similar al anterior
            {
                ToolStripButton boton = sender as ToolStripButton; // Guardamos item 
                                                                   // clikado usando clase indicada en variable 
                boton.Checked = true;
                accion = boton.Name.Substring(3, boton.Name.Length - 3);
                foreach (ToolStripMenuItem mitem in itFiguras.DropDownItems)
                {
                    if (mitem.Tag == boton.Tag)
                        mitem.Checked = true;
                }
            }
            if (accion == "Texto") // acción del texto requiere de una actuación distinta
            {
                VentanaTexto.Location = new Point(Left + 30, Top + 190); // posición form
                if (VentanaTexto.ShowDialog() == DialogResult.OK) // mostramos form
                {
                    mifuente = VentanaTexto.mifuente; //fuente y color elegido
                    colorTexto.Color = VentanaTexto.colortexto.Color;
                    mitexto = VentanaTexto.tbTexto.Text; //texto tecleado
                }
            }
            CrearCursorLapiz(); //ponemos la imagen de un lápiz como puntero del ratón
        }


        private void itFiguras_Click(object sender, EventArgs e)
        {

        }

        private void tsbGoma_Click(object sender, EventArgs e)
        {
                accion = "Goma"; //Indicamos figura a dibujar
                CrearCursorGoma(); //Creamos puntero ratón
                desmarca(); //desmarcammos todas la figuras
                tsbGoma.Checked = true; //Marcamos la elegida
                itGoma.Checked = true;

        }

        private void itGoma_Click(object sender, EventArgs e)
        {
        
        }

        private void tsbBorrarSeleccion_Click_1(object sender, EventArgs e)
        {
            accion = "BorrarSeleccion"; //Indicamos figura a dibujar
            CrearCursorGoma(); //Creamos puntero ratón
            desmarca(); //desmarcammos todas la figuras
            tsbBorrarSeleccion.Checked = true; //Marcamos la elegida
            itBorrarSeleccion.Checked = true;
        }

        private void pbEdigrafi_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Si pulsó el botón izquierdo del ratón 
            {
                pulsado = true; // Botón de ratón pulsado, se está dibujando
                OrigenX = e.X; // Coordenadas de inicio del dibujo, 
                OrigenY = e.Y;
                actualX = e.X; // Coordenadas fin del dibujo. Al principio coinciden
                actualY = e.Y;
                rectInvalido = new Rectangle(OrigenX - 3, OrigenY - 3, 20, 20); // rectángulo 
            }
        }

        private void pbEdigrafi_MouseMove(object sender, MouseEventArgs e)
        {
            // Actualizar barra de estado con coordenadas
            tsl4.Text = "X: " + e.X.ToString() + " Y: " + e.Y.ToString();
            if (pulsado) // Se mueve el ratón con el botón izquierdo pulsado
            {
                if (accion == "Trazo" || accion == "Goma") //Figuras para dibujar de forma 
                { //permanente
                    agregarPunto(new Point(e.X, e.Y)); //pasamos coordenadas a función
                    Dibujar(); // Función que dibuja solo estas figuras
                }
                actualX = e.X; // Coordenadas finales
                actualY = e.Y;
                if (accion != "Circulo")
                { // Rectángulo de la forma a dibujar igual para todas las figuras 
                  // Llama a pbEdigrafi_Paint para dibujar provisionalmente. 
                    rect = new Rectangle(Math.Min(OrigenX, actualX), Math.Min(OrigenY,
                    actualY), Math.Abs(actualX - OrigenX), Math.Abs(actualY - OrigenY));
                }
                else
                { //distinto con Círculo teorema Pitágoras sqrt raíz cuadrada pow potencia
                    int radio = Convert.ToInt32(Math.Truncate(Math.Sqrt(Math.Pow((OrigenX
                    - actualX), 2) + Math.Pow((OrigenY - actualY), 2))));
                    rect = new Rectangle(OrigenX - radio, OrigenY - radio, radio * 2, radio * 2);
                }
                // Luego se invalida el área no permanente de dibujo para borrar la forma 
                // anteriormente dibujada
                pbEdigrafi.Invalidate(rectInvalido);
                //después se dibujará con los últimos datos obtenidos .
            }
        }

        private void pbEdigrafi_MouseUp(object sender, MouseEventArgs e)
        {
            if (pulsado)
            {
                Dibujar2(); //Dibujamos de forma permanente al soltar el botón del ratón 
            }
            pulsado = false;
            //Aqui reinicializamos a puntos para que no se unan las líneas al volver a dibujar 
            puntos = new Point[0];
            //ponemos la imagen dibujada como fondo para que el dibujar sea más rápido. 
            pbEdigrafi.Image = mibmp;
        }

        private void pbEdigrafi_Paint(object sender, PaintEventArgs e)
        {
            if (!pulsado) return;
            switch (accion) // Usamos métodos de dibujado explicados en anterior proyecto 
            {
                case "Linea":
                    e.Graphics.DrawLine(lapiz, OrigenX, OrigenY, actualX, actualY);
                    break;
                case "Rectangulo":
                    if (rellenando)
                        if (itSolido.Checked)
                            e.Graphics.FillRectangle(relleno, new Rectangle(Math.Min(OrigenX,
                                actualX), Math.Min(OrigenY, actualY), Math.Abs(actualX - OrigenX),
                                Math.Abs(actualY - OrigenY)));
                        else
                            e.Graphics.FillRectangle(estilorelleno, new Rectangle(Math.Min(OrigenX,
                                actualX), Math.Min(OrigenY, actualY), Math.Abs(actualX - OrigenX),
                                Math.Abs(actualY - OrigenY)));
                    e.Graphics.DrawRectangle(lapiz, Math.Min(OrigenX, actualX), Math.Min(OrigenY,
                        actualY), Math.Abs(actualX - OrigenX), Math.Abs(actualY - OrigenY));
                    break;
                case "Circulo": // Teorema de pitágoras
                    int radio = Convert.ToInt32(Math.Truncate(Math.Sqrt(Math.Pow((OrigenX -
                    actualX), 2) + Math.Pow((OrigenY - actualY), 2))));
                    if (rellenando)
                        if (itSolido.Checked)
                            e.Graphics.FillEllipse(relleno, OrigenX - radio, OrigenY - radio,
                            radio * 2, radio * 2);
                        else
                            e.Graphics.FillEllipse(estilorelleno, OrigenX - radio, OrigenY
                            - radio, radio * 2, radio * 2);
                    e.Graphics.DrawEllipse(lapiz, OrigenX - radio, OrigenY - radio, radio * 2,
                    radio * 2);
                    break;
                case "Elipse":
                    if (rellenando)
                        if (itSolido.Checked)
                            e.Graphics.FillEllipse(relleno, new Rectangle(Math.Min(OrigenX,
                            actualX), Math.Min(OrigenY, actualY), Math.Abs(actualX - OrigenX),
                            Math.Abs(actualY - OrigenY)));
                        else
                            e.Graphics.FillEllipse(estilorelleno, new Rectangle(Math.Min(OrigenX,
                            actualX), Math.Min(OrigenY, actualY), Math.Abs(actualX - OrigenX),
                            Math.Abs(actualY - OrigenY)));
                    e.Graphics.DrawEllipse(lapiz, Math.Min(OrigenX, actualX), Math.Min(OrigenY,
                    actualY), Math.Abs(actualX - OrigenX), Math.Abs(actualY - OrigenY));
                    break;
                case "BorrarSeleccion":
                    Pen lapizborra = new Pen(Color.Black, 1);
                    lapizborra.DashStyle = DashStyle.Custom; //Estilo personalizado 8 px
                    lapizborra.DashPattern = new float[] { 8, 8 }; // de línea y 8 de hueco
                    e.Graphics.DrawRectangle(lapizborra, Math.Min(OrigenX, actualX),
                    Math.Min(OrigenY, actualY), Math.Abs(actualX - OrigenX),
                    Math.Abs(actualY - OrigenY));
                    break;
            }
            rectInvalido = new Rectangle(rect.X - 3, rect.Y - 3, rect.Width + 20, rect.Height + 20);
            cambios = true;
        }

        private void Dibujar()
        {
            Graphics g2 = Graphics.FromImage(mibmp);
            switch (accion)
            {
                case "Trazo":
                    if (puntos.Length > 1)
                    {
                        g1.DrawLines(lapiz, puntos); //Permite ver la evolución del pintado
                        g2.DrawLines(lapiz, puntos); // Recoge todo lo dibujado 
                    }
                    break;
                case "Goma":
                    if (puntos.Length > 1)
                    {
                        g1.DrawLines(goma, puntos);
                        g2.DrawLines(goma, puntos);
                    }
                    break;
            }
        }

        private void Dibujar2()
        {
            Graphics g2 = Graphics.FromImage(mibmp);
            switch (accion)
            {
                case "Linea":
                    g2.DrawLine(lapiz, OrigenX, OrigenY, actualX, actualY);
                    break;
                case "Rectangulo":
                    if (rellenando)
                        if (itLinea5.Checked)
                        {
                            g2.FillRectangle(relleno, new Rectangle(Math.Min(OrigenX,
                            actualX), Math.Min(OrigenY, actualY), Math.Abs(actualX - OrigenX),
                            Math.Abs(actualY - OrigenY)));
                        }
                        else
                        {
                            g2.FillRectangle(estilorelleno, new Rectangle(Math.Min(OrigenX,
                            actualX), Math.Min(OrigenY, actualY), Math.Abs(actualX - OrigenX),
                            Math.Abs(actualY - OrigenY)));
                        }
                    g2.DrawRectangle(lapiz, Math.Min(OrigenX, actualX), Math.Min(OrigenY,
                    actualY), Math.Abs(actualX - OrigenX), Math.Abs(actualY - OrigenY));
                    break;
                case "Elipse":
                    if (rellenando)
                        if (itLinea5.Checked)
                            g2.FillEllipse(relleno, new Rectangle(Math.Min(OrigenX,
                            actualX), Math.Min(OrigenY, actualY), Math.Abs(actualX -
                            OrigenX), Math.Abs(actualY - OrigenY)));
                        else
                            g2.FillEllipse(estilorelleno, new Rectangle(Math.Min(OrigenX,
                            actualX), Math.Min(OrigenY, actualY), Math.Abs(actualX -
                            OrigenX), Math.Abs(actualY - OrigenY)));
                    g2.DrawEllipse(lapiz, Math.Min(OrigenX, actualX), Math.Min(OrigenY,
                    actualY), Math.Abs(actualX - OrigenX), Math.Abs(actualY - OrigenY));
                    break;
                case "Circulo":
                    // teorema de pitágoras
                    int radio = Convert.ToInt32(Math.Truncate(Math.Sqrt(Math.Pow((OrigenX -
                    actualX), 2) + Math.Pow((OrigenY - actualY), 2))));
                    if (rellenando)
                        if (itLinea5.Checked)
                            g2.FillEllipse(relleno, OrigenX - radio, OrigenY - radio, radio *
                            2, radio * 2);
                        else
                            g2.FillEllipse(estilorelleno, OrigenX - radio, OrigenY - radio,
                            radio * 2, radio * 2);
                    g2.DrawEllipse(lapiz, OrigenX - radio, OrigenY - radio, radio * 2, radio * 2);
                    break;
                case "Texto":
                    g2.DrawString(mitexto, mifuente, colorTexto, actualX, actualY - 10);
                    break;
                case "BorrarSeleccion":
                    SolidBrush rellenoborra = new SolidBrush(pbEdigrafi.BackColor);
                    g2.FillRectangle(rellenoborra, new Rectangle(Math.Min(OrigenX,
                    actualX), Math.Min(OrigenY, actualY), Math.Abs(actualX - OrigenX),
                    Math.Abs(actualY - OrigenY)));
                    break;
            }
        }

        private void tsbColorlapiz_Click(object sender, EventArgs e)
        {
            dlgColores.Color = lapiz.Color;
            if (dlgColores.ShowDialog() == DialogResult.OK)
            {
                CreaCuadradoBoton(); // Creamos imagen para el botón
                tsbColorlapiz.Image = CuadradoBoton; // Asignamos imagen a botón
                lapiz.Color = dlgColores.Color;
            }
        }

        private void tsbColorFondo_Click(object sender, EventArgs e)
        {
            dlgColores.Color = pbEdigrafi.BackColor;
            if (dlgColores.ShowDialog() == DialogResult.OK)
            {
                CreaCuadradoBoton();
                tsbColorFondo.Image = CuadradoBoton;
                pbEdigrafi.BackColor = dlgColores.Color;
                if (tsbGoma.Checked || tsbBorrarSeleccion.Checked)
                { // Borramos con figura dibujada 
                    goma = new Pen(pbEdigrafi.BackColor, 7); // con el color de fondo 
                    CrearCursorGoma(); //Cambiamos cursor si estamos borrando
                }
            }
        }

        private void tsbColorRelleno_Click(object sender, EventArgs e)
        {
            if (!rellenando) // Es necesario aplicar un tipo relleno, por defecto
            { // está sin relleno
                MessageBox.Show("Elige Tipo Relleno en Estilo");
            }
            dlgColores.Color = relleno.Color;
            if (dlgColores.ShowDialog() == DialogResult.OK)
            {
                CreaCuadradoBoton(); // ponemos imagen con color en botón
                tsbColorRelleno.Image = CuadradoBoton;
                relleno.Color = dlgColores.Color;
            }
            if (!itSinRelleno.Checked) //Si el tiporelleno ya se ha elegido y se cambia 
            { // el color, aplicamos de nuevo porque el color y tipo se aplican a la 
              // vez y esto se hace en los ítems de menú
                foreach (ToolStripItem miItem in itTipoRelleno.DropDownItems)
                {
                    if (((ToolStripMenuItem)miItem).Checked)
                        ((ToolStripMenuItem)miItem).PerformClick();
                }
            }
        }

        private void itAbrir_Click(object sender, EventArgs e)
        {
       
                if (cambios)
                {
                    DialogResult resultado = MessageBox.Show("Hay Cambios sin Guardar. Guardas ? ", "Guardar Cambios", MessageBoxButtons.YesNoCancel);
                    switch (resultado)
                    {
                        case DialogResult.Yes: // Si contesta si
                            itGuardar.PerformClick();// Invocamos evento botón guardar 
                            break; //Después de Guardar Continuamos con operación de nuevo
                        case DialogResult.Cancel: // Si decide cancelar 
                            return; // Abortamos operación de nuevo
                    }
                    
                }
            dlgAbrirDibujo.FileName = Text;
            if (dlgAbrirDibujo.ShowDialog() == DialogResult.OK &&
            dlgAbrirDibujo.FileName.Length > 0)
            {
                Inicializar();
                Graphics g2 = Graphics.FromImage(mibmp);
                pbEdigrafi.Image = Image.FromFile(dlgAbrirDibujo.FileName);
                g2.DrawImage(pbEdigrafi.Image, new Point(0, 0));
                Text = dlgAbrirDibujo.FileName;
                g2.Dispose();
            }
        }

        private void Unir(Bitmap fondo)
        {
            Graphics g = Graphics.FromImage(fondo);
            g.DrawImage(mibmp, 0, 0);
            mibmp = new Bitmap(fondo);
            g.Dispose();
            fondo.Dispose();
        }

        private void itGuardar_Click(object sender, EventArgs e)
        {
            dlgGuardarDibujo.FileName = Text;
            if (dlgGuardarDibujo.ShowDialog() == DialogResult.OK &&
            dlgGuardarDibujo.FileName.Length > 0)
            {
                Bitmap fondo = new Bitmap(mibmp.Width, mibmp.Height); //Creamos el fondo
                Graphics g = Graphics.FromImage(fondo);
                g.FillRectangle(new SolidBrush(pbEdigrafi.BackColor), 0, 0,// Dibujamos 
                mibmp.Width, mibmp.Height); //el fondo
                g.Dispose();
                Unir(fondo);
                fondo.Dispose();
                if (dlgAbrirDibujo.FilterIndex == 1) // Guardamos en bitmap o jpg 
                {
                    mibmp.Save(dlgGuardarDibujo.FileName, ImageFormat.Bmp);
                }
                else
                {
                    mibmp.Save(dlgGuardarDibujo.FileName, ImageFormat.Jpeg);
                }
                Text = dlgGuardarDibujo.FileName;
            }
        }

        private void itTamanyo_Click(object sender, EventArgs e)
        {
            VentanaTamanyo.Text = "Tamaño de Dibujo en Pixeles";
            VentanaTamanyo.lbIz.Text = "Ancho";
            VentanaTamanyo.lbDer.Text = "Alto";
            VentanaTamanyo.tbIzq.MaxLength = 4;
            VentanaTamanyo.tbDer.MaxLength = 4;
            VentanaTamanyo.tbIzq.Text = Convert.ToString(pbEdigrafi.Width);
            VentanaTamanyo.tbDer.Text = Convert.ToString(pbEdigrafi.Height);
            if (VentanaTamanyo.ShowDialog() == DialogResult.OK)
            {
                pbEdigrafi.Width = int.Parse(VentanaTamanyo.tbIzq.Text);
                pbEdigrafi.Height = int.Parse(VentanaTamanyo.tbDer.Text);
                Bitmap fondo = new Bitmap(pbEdigrafi.Width, pbEdigrafi.Height);
                Graphics g = Graphics.FromImage(fondo);
                g.FillRectangle(new SolidBrush(pbEdigrafi.BackColor), 0, 0, mibmp.Width,
                mibmp.Height);
                g.Dispose();
                Unir(fondo);
                fondo.Dispose();
            }
            tsl1.Text = "Tamaño del Dibujo: " + Convert.ToString(pbEdigrafi.Width);
        }

        private void grosorLíneaHomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VentanaTamanyo.Text = "Grosor de Línea y Goma en Puntos";
            VentanaTamanyo.lbIz.Text = "Grosor Línea";
            VentanaTamanyo.lbDer.Text = "Grosor Goma";
            VentanaTamanyo.tbIzq.MaxLength = 2;
            VentanaTamanyo.tbDer.MaxLength = 2;
            VentanaTamanyo.tbIzq.Text = Convert.ToString(lapiz.Width);
            VentanaTamanyo.tbDer.Text = Convert.ToString(goma.Width);
            Single copia = goma.Width; // Para saber si ha cambiado grosor goma
            if (VentanaTamanyo.ShowDialog() == DialogResult.OK)
            {
                lapiz.Width = int.Parse(VentanaTamanyo.tbIzq.Text); //Cambio de tipo de 
                goma.Width = int.Parse(VentanaTamanyo.tbDer.Text); // string a entero
                if (!tsbGoma.Checked) // Cambia grosor goma estando elegida otra figura.
                {
                    if (copia != goma.Width) // Aviso
                        MessageBox.Show("Los cambios de grosor no se aplicarán hasta que elijas goma"); 
                }
                else
                    CrearCursorGoma(); // Ponemos cursor al ratón
            }
            tsl2.Text = "Grosor Linea: " + lapiz.Width.ToString();
            tsl3.Text = "Grosor Goma: " + goma.Width.ToString();
        }

        private void ddtsbGrosor_Click(object sender, EventArgs e)
        {
            // Identificamos ítem para marcar por el tag y el grosor del lápiz
            foreach (ToolStripMenuItem miGrosor in ddtsbGrosor.DropDownItems)
            {
                if (Convert.ToSingle(miGrosor.Tag) == lapiz.Width)
                    miGrosor.Checked = true;
                else
                    miGrosor.Checked = false;
            }
        }

        private void itG1_Click(object sender, EventArgs e)
        {
            if (lapiz.Width > 0 && sender is ToolStripMenuItem)
            {
                ToolStripMenuItem elemento = sender as ToolStripMenuItem;
                lapiz.Width = Convert.ToSingle(elemento.Tag);
                tsl2.Text = "Grosor Linea: " + lapiz.Width.ToString();
            }
        }

        private void fmEdiGrafico_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus) && goma.Width < 100 &&
 accion == "Goma")
            {
                goma.Width++;
                CrearCursorGoma();
            }
            if ((e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus) && lapiz.Width < 100
             && accion == "Trazo")
                lapiz.Width++;
            if ((e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus) && goma.Width > 1
             && accion == "Goma")
            {
                goma.Width--;
                CrearCursorGoma();
            }
            if ((e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus) && lapiz.Width > 1
             && accion == "Trazo")
                lapiz.Width--;
            tsl2.Text = "Grosor Linea: " + lapiz.Width.ToString();
            tsl3.Text = "Grosor Goma: " + goma.Width.ToString();
        }

        private void itLinea1_Click(object sender, EventArgs e)
        {
            desmarcamenu(itTipoLinea.DropDownItems);
            ToolStripMenuItem elemento = sender as ToolStripMenuItem;
            int mitag = Convert.ToInt32(elemento.Tag);
            elemento.Checked = true;
            switch (mitag)
            {
                case 1:
                    lapiz.DashStyle = DashStyle.Dash; //Cambia estilo línea
                    break;
                case 2:
                    lapiz.DashStyle = DashStyle.DashDot;
                    break;
                case 3:
                    lapiz.DashStyle = DashStyle.Dot;
                    break;
                case 4:
                    lapiz.DashStyle = DashStyle.DashDotDot;
                    break;
                case 5:
                    lapiz.DashStyle = DashStyle.Solid;
                    break;
            }
        }

        private void tipoLíneaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void itVertical_Click(object sender, EventArgs e)
        {
            desmarcamenu(itTipoRelleno.DropDownItems);
            rellenando = true;
            ToolStripMenuItem elemento = sender as ToolStripMenuItem;
            int mitag = Convert.ToInt32(elemento.Tag);
            elemento.Checked = true;
            switch (mitag)
            {
                case 1:
                    estilorelleno = new HatchBrush(HatchStyle.Vertical, lapiz.Color, relleno.Color);
                    break;
                case 2:
                    estilorelleno = new HatchBrush(HatchStyle.Cross, lapiz.Color, relleno.Color);
                    break;
                case 3:
                    estilorelleno = new HatchBrush(HatchStyle.Horizontal, lapiz.Color, relleno.Color);
                    break;
                case 4:
                    estilorelleno = new HatchBrush(HatchStyle.ForwardDiagonal, lapiz.Color,
                    relleno.Color);
                    break;
                case 5:
                    estilorelleno = new HatchBrush(HatchStyle.ZigZag, lapiz.Color, relleno.Color);
                    break;
                case 6:
                    estilorelleno = new HatchBrush(HatchStyle.Plaid, lapiz.Color, relleno.Color);
                    break;
                case 7:
                    estilorelleno = new HatchBrush(HatchStyle.Percent20, lapiz.Color, relleno.Color);
                    break; //Si marca solido sin elegir color no sale nada
                case 8:
                    rellenando = false;
                    relleno = new SolidBrush(Color.FromArgb(255, 255, 255));//
                    tsbColorRelleno.Image = Properties.Resources.brocha;
                    break;
            }
        }

        private void itInstrucciones_Click(object sender, EventArgs e)
        {
            // Si utilizáramos StreamReader y ReadToEnd() las letras acentuadas se pierden.
            FileStream fe = null;
            try
            { // Creamos un flujo desde el fichero texto.txt
                fe = new FileStream(".\\ficheros\\ayuda.txt", //Sin @ escapando \
                FileMode.Open, FileAccess.Read);
                char[] cBuffer = new char[(int)fe.Length]; //Creamos array de char con tamaño 
                                                           // del fichero
                byte[] bBuffer = new byte[(int)fe.Length]; //Creamos array de byte igual
                                                           // Read Lee del fichero lo que se indica
                fe.Read(bBuffer, 0, (int)fe.Length); //Leemos del fichero en formato byte
                                                     // Creamos un objeto string con el texto leído 
                Array.Copy(bBuffer, cBuffer, bBuffer.Length); //Copiamos lo que hemos leído 
                                                              // en un array de byte en un array de char
                String str = new String(cBuffer, 0, (int)fe.Length); //Creamos el string a 
                                                                     // partir del array de char
                MessageBox.Show(str, "Ayuda Para Dibujar"); // Mostramos el texto leído
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (fe != null) fe.Close(); // Cerramos el fichero
            }

        }

        private void fmEdiGrafico_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cambios)
            {
                DialogResult resultado = MessageBox.Show("Hay Cambios sin Guardar. ¿Deseas guardarlos?", "Guardar Cambios", MessageBoxButtons.YesNoCancel);
                switch (resultado)
                {
                    case DialogResult.Yes:
                        itGuardar.PerformClick();
                        break;
                    case DialogResult.No:
                        cambios = false;
                        Application.Exit();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
    
        }

        private void itSalir_Click(object sender, EventArgs e)
        {
            if (cambios)
            {
                DialogResult resultado = MessageBox.Show("Hay Cambios sin Guardar. ¿Deseas guardarlos?", "Guardar Cambios", MessageBoxButtons.YesNoCancel);
                switch (resultado)
                {
                    case DialogResult.Yes:
                        itGuardar.PerformClick();
                        break;
                    case DialogResult.No:
                        cambios = false;
                        Application.Exit();
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
            Application.Exit(); 
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
