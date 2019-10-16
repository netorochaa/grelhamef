namespace Grelha_MEF
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxModuloYoung = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCoeficientePoisson = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxL1 = new System.Windows.Forms.TextBox();
            this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "B";
            // 
            // textBoxB
            // 
            this.textBoxB.Location = new System.Drawing.Point(16, 30);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(100, 20);
            this.textBoxB.TabIndex = 1;
            this.textBoxB.Text = "0.15";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "H";
            // 
            // textBoxH
            // 
            this.textBoxH.Location = new System.Drawing.Point(131, 30);
            this.textBoxH.Name = "textBoxH";
            this.textBoxH.Size = new System.Drawing.Size(100, 20);
            this.textBoxH.TabIndex = 3;
            this.textBoxH.Text = "0.30";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Modulo Young";
            // 
            // textBoxModuloYoung
            // 
            this.textBoxModuloYoung.Location = new System.Drawing.Point(244, 30);
            this.textBoxModuloYoung.Name = "textBoxModuloYoung";
            this.textBoxModuloYoung.Size = new System.Drawing.Size(100, 20);
            this.textBoxModuloYoung.TabIndex = 5;
            this.textBoxModuloYoung.Text = "300";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(355, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Coeficiente Poisson";
            // 
            // textBoxCoeficientePoisson
            // 
            this.textBoxCoeficientePoisson.Location = new System.Drawing.Point(355, 30);
            this.textBoxCoeficientePoisson.Name = "textBoxCoeficientePoisson";
            this.textBoxCoeficientePoisson.Size = new System.Drawing.Size(100, 20);
            this.textBoxCoeficientePoisson.TabIndex = 7;
            this.textBoxCoeficientePoisson.Text = "0.20";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(468, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Comprimento da barra 1";
            // 
            // textBoxL1
            // 
            this.textBoxL1.Location = new System.Drawing.Point(471, 29);
            this.textBoxL1.Name = "textBoxL1";
            this.textBoxL1.Size = new System.Drawing.Size(100, 20);
            this.textBoxL1.TabIndex = 9;
            this.textBoxL1.Text = "10.4";
            // 
            // buttonCalculaMatrizRigidezElementoEmCoordenadasLocais
            // 
            this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais.Location = new System.Drawing.Point(617, 26);
            this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais.Name = "buttonCalculaMatrizRigidezElementoEmCoordenadasLocais";
            this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais.Size = new System.Drawing.Size(75, 23);
            this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais.TabIndex = 10;
            this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais.Text = "Calcular";
            this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais.UseVisualStyleBackColor = true;
            this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais.Click += new System.EventHandler(this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 90);
            this.Controls.Add(this.buttonCalculaMatrizRigidezElementoEmCoordenadasLocais);
            this.Controls.Add(this.textBoxL1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxCoeficientePoisson);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxModuloYoung);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxB);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxModuloYoung;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCoeficientePoisson;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxL1;
        private System.Windows.Forms.Button buttonCalculaMatrizRigidezElementoEmCoordenadasLocais;

    }
}

