﻿namespace EditorGrafico
{
    partial class fmTamanyo
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
            this.lbIz = new System.Windows.Forms.Label();
            this.lbDer = new System.Windows.Forms.Label();
            this.tbIzq = new System.Windows.Forms.TextBox();
            this.tbDer = new System.Windows.Forms.TextBox();
            this.lbPxIz = new System.Windows.Forms.Label();
            this.lbPxDer = new System.Windows.Forms.Label();
            this.btAceptar = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbIz
            // 
            this.lbIz.AutoSize = true;
            this.lbIz.Location = new System.Drawing.Point(30, 30);
            this.lbIz.Name = "lbIz";
            this.lbIz.Size = new System.Drawing.Size(38, 13);
            this.lbIz.TabIndex = 0;
            this.lbIz.Text = "Ancho";
            // 
            // lbDer
            // 
            this.lbDer.AutoSize = true;
            this.lbDer.Location = new System.Drawing.Point(170, 30);
            this.lbDer.Name = "lbDer";
            this.lbDer.Size = new System.Drawing.Size(25, 13);
            this.lbDer.TabIndex = 1;
            this.lbDer.Text = "Alto";
            // 
            // tbIzq
            // 
            this.tbIzq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbIzq.Location = new System.Drawing.Point(33, 46);
            this.tbIzq.MaxLength = 3000;
            this.tbIzq.Name = "tbIzq";
            this.tbIzq.Size = new System.Drawing.Size(78, 20);
            this.tbIzq.TabIndex = 2;
            this.tbIzq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIzq_KeyPress);
            // 
            // tbDer
            // 
            this.tbDer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDer.Location = new System.Drawing.Point(173, 46);
            this.tbDer.MaxLength = 3000;
            this.tbDer.Name = "tbDer";
            this.tbDer.Size = new System.Drawing.Size(78, 20);
            this.tbDer.TabIndex = 2;
            this.tbDer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIzq_KeyPress);
            // 
            // lbPxIz
            // 
            this.lbPxIz.AutoSize = true;
            this.lbPxIz.Location = new System.Drawing.Point(118, 52);
            this.lbPxIz.Name = "lbPxIz";
            this.lbPxIz.Size = new System.Drawing.Size(18, 13);
            this.lbPxIz.TabIndex = 3;
            this.lbPxIz.Text = "px";
            // 
            // lbPxDer
            // 
            this.lbPxDer.AutoSize = true;
            this.lbPxDer.Location = new System.Drawing.Point(257, 52);
            this.lbPxDer.Name = "lbPxDer";
            this.lbPxDer.Size = new System.Drawing.Size(18, 13);
            this.lbPxDer.TabIndex = 3;
            this.lbPxDer.Text = "px";
            // 
            // btAceptar
            // 
            this.btAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAceptar.Location = new System.Drawing.Point(33, 99);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(75, 23);
            this.btAceptar.TabIndex = 4;
            this.btAceptar.Text = "Aceptar";
            this.btAceptar.UseVisualStyleBackColor = true;
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancelar.Location = new System.Drawing.Point(173, 99);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(75, 23);
            this.btCancelar.TabIndex = 4;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            // 
            // fmTamanyo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 152);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btAceptar);
            this.Controls.Add(this.lbPxDer);
            this.Controls.Add(this.lbPxIz);
            this.Controls.Add(this.tbDer);
            this.Controls.Add(this.tbIzq);
            this.Controls.Add(this.lbDer);
            this.Controls.Add(this.lbIz);
            this.Name = "fmTamanyo";
            this.Text = "fmTamanyo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbPxIz;
        private System.Windows.Forms.Label lbPxDer;
        public System.Windows.Forms.TextBox tbIzq;
        public System.Windows.Forms.TextBox tbDer;
        public System.Windows.Forms.Button btAceptar;
        public System.Windows.Forms.Button btCancelar;
        public System.Windows.Forms.Label lbIz;
        public System.Windows.Forms.Label lbDer;
    }
}