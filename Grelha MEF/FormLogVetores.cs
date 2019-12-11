using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grelha_MEF
{
    public partial class FormLogVetores : Form
    {
        public FormLogVetores(List<double[]> vetor, string titulo)
        {
            InitializeComponent();

            bool esforcosInternos = titulo == "ESFORÇOS INTERNOS" ? true : false;
            this.Text = titulo;
            richTextBoxLogVetores.Text += titulo + Environment.NewLine + Environment.NewLine;

            for (int i = 0; i < vetor.Count; i++ )
            {
                richTextBoxLogVetores.Text += "Elemento " + (i+1).ToString() + Environment.NewLine;
                richTextBoxLogVetores.Text += MatrixUtil.VectorAsStringForLogForm(vetor[i], esforcosInternos);
            }
        }
    }
}
