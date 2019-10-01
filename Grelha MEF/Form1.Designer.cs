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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxQtdElementos = new System.Windows.Forms.TextBox();
            this.textBoxNoLocal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxNoGlobal = new System.Windows.Forms.TextBox();
            this.textBoxQtdNos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxGrausLiberdade = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Qtd de Elementos";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxQtdElementos
            // 
            this.textBoxQtdElementos.Location = new System.Drawing.Point(135, 77);
            this.textBoxQtdElementos.Name = "textBoxQtdElementos";
            this.textBoxQtdElementos.ReadOnly = true;
            this.textBoxQtdElementos.Size = new System.Drawing.Size(130, 20);
            this.textBoxQtdElementos.TabIndex = 2;
            this.textBoxQtdElementos.Text = "2";
            // 
            // textBoxNoLocal
            // 
            this.textBoxNoLocal.Location = new System.Drawing.Point(15, 134);
            this.textBoxNoLocal.Name = "textBoxNoLocal";
            this.textBoxNoLocal.Size = new System.Drawing.Size(100, 20);
            this.textBoxNoLocal.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(132, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Nó Global";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxNoGlobal
            // 
            this.textBoxNoGlobal.Location = new System.Drawing.Point(135, 134);
            this.textBoxNoGlobal.Name = "textBoxNoGlobal";
            this.textBoxNoGlobal.Size = new System.Drawing.Size(130, 20);
            this.textBoxNoGlobal.TabIndex = 10;
            // 
            // textBoxQtdNos
            // 
            this.textBoxQtdNos.Location = new System.Drawing.Point(15, 77);
            this.textBoxQtdNos.Name = "textBoxQtdNos";
            this.textBoxQtdNos.Size = new System.Drawing.Size(100, 20);
            this.textBoxQtdNos.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Qtd. de Nós (Sist global)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Nó Local";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Grelha 1"});
            this.comboBox1.Location = new System.Drawing.Point(13, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(252, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 410);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(281, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Graus de Liberdade";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxGrausLiberdade
            // 
            this.textBoxGrausLiberdade.Location = new System.Drawing.Point(284, 77);
            this.textBoxGrausLiberdade.Name = "textBoxGrausLiberdade";
            this.textBoxGrausLiberdade.ReadOnly = true;
            this.textBoxGrausLiberdade.Size = new System.Drawing.Size(130, 20);
            this.textBoxGrausLiberdade.TabIndex = 14;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 190);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(405, 169);
            this.dataGridView1.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 445);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxGrausLiberdade);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxNoGlobal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxNoLocal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxQtdElementos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxQtdNos);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxQtdElementos;
        private System.Windows.Forms.TextBox textBoxNoLocal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxNoGlobal;
        private System.Windows.Forms.TextBox textBoxQtdNos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxGrausLiberdade;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

