namespace EditorGrafico
{
    partial class fmTexto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmTexto));
            this.tbTexto = new System.Windows.Forms.TextBox();
            this.btFuente = new System.Windows.Forms.Button();
            this.btAplicar = new System.Windows.Forms.Button();
            this.btSalir = new System.Windows.Forms.Button();
            this.dlgFuente = new System.Windows.Forms.FontDialog();
            this.SuspendLayout();
            // 
            // tbTexto
            // 
            this.tbTexto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTexto.Location = new System.Drawing.Point(12, 12);
            this.tbTexto.Name = "tbTexto";
            this.tbTexto.Size = new System.Drawing.Size(100, 20);
            this.tbTexto.TabIndex = 0;
            // 
            // btFuente
            // 
            this.btFuente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFuente.Image = ((System.Drawing.Image)(resources.GetObject("btFuente.Image")));
            this.btFuente.Location = new System.Drawing.Point(118, 12);
            this.btFuente.Name = "btFuente";
            this.btFuente.Size = new System.Drawing.Size(30, 20);
            this.btFuente.TabIndex = 1;
            this.btFuente.UseVisualStyleBackColor = true;
            this.btFuente.Click += new System.EventHandler(this.btFuente_Click);
            // 
            // btAplicar
            // 
            this.btAplicar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAplicar.Image = ((System.Drawing.Image)(resources.GetObject("btAplicar.Image")));
            this.btAplicar.Location = new System.Drawing.Point(154, 12);
            this.btAplicar.Name = "btAplicar";
            this.btAplicar.Size = new System.Drawing.Size(30, 20);
            this.btAplicar.TabIndex = 1;
            this.btAplicar.UseVisualStyleBackColor = true;
            // 
            // btSalir
            // 
            this.btSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSalir.Image = ((System.Drawing.Image)(resources.GetObject("btSalir.Image")));
            this.btSalir.Location = new System.Drawing.Point(190, 12);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(30, 20);
            this.btSalir.TabIndex = 1;
            this.btSalir.UseVisualStyleBackColor = true;
            // 
            // fmTexto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 48);
            this.ControlBox = false;
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.btAplicar);
            this.Controls.Add(this.btFuente);
            this.Controls.Add(this.tbTexto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fmTexto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "fmTexto";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.fmTexto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FontDialog dlgFuente;
        public System.Windows.Forms.TextBox tbTexto;
        public System.Windows.Forms.Button btFuente;
        public System.Windows.Forms.Button btAplicar;
        public System.Windows.Forms.Button btSalir;
    }
}