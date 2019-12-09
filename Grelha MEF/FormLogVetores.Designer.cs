namespace Grelha_MEF
{
    partial class FormLogVetores
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
            this.richTextBoxLogVetores = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxLogVetores
            // 
            this.richTextBoxLogVetores.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxLogVetores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLogVetores.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBoxLogVetores.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxLogVetores.Location = new System.Drawing.Point(12, 0);
            this.richTextBoxLogVetores.Name = "richTextBoxLogVetores";
            this.richTextBoxLogVetores.ReadOnly = true;
            this.richTextBoxLogVetores.Size = new System.Drawing.Size(311, 986);
            this.richTextBoxLogVetores.TabIndex = 0;
            this.richTextBoxLogVetores.Text = "";
            // 
            // FormLogVetores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(323, 986);
            this.Controls.Add(this.richTextBoxLogVetores);
            this.Name = "FormLogVetores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxLogVetores;
    }
}