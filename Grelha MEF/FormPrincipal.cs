using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace Grelha_MEF
{
    public partial class Form1 : Form
    {
        List<double[,]> matrizesLocaisElementos;
        List<double[,]> matrizesGlobaisElementosEspalhamento;
        List<double[,]> matrizesRotPorAnguloDoElem;
        List<double[]>  vetoresDeslocGiroGlobalElem;
        List<double[]>  vetoresEsforcosInternosElem;
        List<double[]>  elementosGraficoBase;
        List<double[]>  elementosGraficoDEC;
        List<double[]>  elementosGraficoDMF;
        List<double[]>  elementosGraficoDMT;
        List<int[]>     elementosLocais;
        int[]           grausLiberdadeLocal = new int[6]{ 1, 2, 3, 4, 5, 6 };
        int             grausLiberdadeGlobal;
        int             quantidadeNos;
        int             quantidadeElementos;
        double[,]       matrizGlobalEstrutura;
        double[]        vetorCargasExternas;

        public Form1()
        {
            InitializeComponent();
        }

        public void inicializaVetoresElementosLocais(int gl, int qtdEle)
        {
            elementosLocais = new List<int[]>();

            for (int i = 0; i < qtdEle; i++)
            {
                int[] aux = new int[gl];
                for (int j = i * 3; j < gl; j++)
                {
                    if (j % 3 == 0)
                    {
                        grausLiberdadeLocal.CopyTo(aux, j);
                        break;
                    }
                    else aux[j] = 0;
                }
                elementosLocais.Add(aux);
            }
        }
        public void inicializaMatrizGlobalEstrutura(int grausLiberdadeGlobal, int quantidadeElementos)
        {
            matrizGlobalEstrutura                = new double[grausLiberdadeGlobal, grausLiberdadeGlobal];
            matrizesRotPorAnguloDoElem           = new List<double[,]>();
            elementosGraficoBase                 = new List<double[]>();
            matrizesLocaisElementos              = new List<double[,]>();
            matrizesGlobaisElementosEspalhamento = new List<double[,]>();
            try
            {
                for (int i = 1; i <= quantidadeElementos; i++)
                {
                    Control[] combobox = this.Controls.Find("comboBoxAnguloE" + i.ToString(), true);
                    ComboBox angulo = combobox[0] as ComboBox;
                    Control[] comboboxDirecao = this.Controls.Find("comboBoxAnguloDirE" + i.ToString(), true);
                    ComboBox anguloDir = comboboxDirecao[0] as ComboBox;
                    Control[] textbox = this.Controls.Find("textBoxComprimentoE" + i.ToString(), true);
                    TextBox comprimento = textbox[0] as TextBox;
                    double anguloConvertido = anguloDir.Text == "-" ? -Convert.ToInt32(angulo.Text) : Convert.ToInt32(angulo.Text);
                    elementosGraficoBase.Add(new double[] { double.Parse(comprimento.Text), anguloConvertido });
                    matrizesLocaisElementos.Add(calculaMatrizRigidezEmCoordenadasLocais(comprimento.Text));
                    matrizesGlobaisElementosEspalhamento.Add(espalhamentoMatrizRigidezGlobalNoSistemaGlobal(
                                                               MatrixUtil.multiplicacaoMatrizes(
                                                                   grausLiberdadeLocal,
                                                                   MatrixUtil.multiplicacaoMatrizes(
                                                                        grausLiberdadeLocal,
                                                                        MatrixUtil.matrizRotacaoInversa(Convert.ToInt32(angulo.Text)), matrizesLocaisElementos[i - 1], string.Empty),
                                                                        MatrixUtil.matrizRotacao(matrizesRotPorAnguloDoElem, Convert.ToInt32(angulo.Text)), string.Empty
                                                                ), grausLiberdadeGlobal,
                                                                   elementosLocais[i - 1],
                                                                   string.Empty
                                                             ));
                }
            }
            catch (FormatException e)
            {
                MessageBox.Show("Há Campo(s) em branco ou dados foram informados incorretamente.");
            }
            matrizGlobalEstrutura = calculaMatrizGlobalDosElementos(matrizesGlobaisElementosEspalhamento, grausLiberdadeGlobal, "MATRIZ GLOBAL DA ESTRUTURA");
        }
        public void inicializaVetorCargasExternas(int tamanhoVetor, int quantidadeNos)
        {
            vetorCargasExternas = new double[tamanhoVetor];
            for (int i = 1; i <= quantidadeNos; i++)
            {
                Control[] control = this.Controls.Find("textBoxForcaNo" + i.ToString(), true);
                TextBox textbox = control[0] as TextBox;
                if (textbox.Enabled && !String.IsNullOrEmpty(textbox.Text.Trim()))
                {
                    int indice = (i - 1) * 3;
                    vetorCargasExternas[indice] = double.Parse(textbox.Text);
                }
            }
        }
        public double[,] espalhamentoMatrizRigidezGlobalNoSistemaGlobal(double[,] matrizGlobalDoElemento, int grausLiberdadeGlobal, int[] localElemento, string nome)
        {
            int countI = 0;
            double[,] matrizResultante = new double[grausLiberdadeGlobal, grausLiberdadeGlobal];
            for (int i = 0; i < grausLiberdadeGlobal; i++)
            {
                int countJ = 0;
                for (int j = 0; j < grausLiberdadeGlobal; j++)
                {
                    //percorre elementos
                    if (!localElemento[j].Equals(0) && !localElemento[i].Equals(0))
                    {
                        matrizResultante[i, j] = matrizGlobalDoElemento[countI, countJ];
                        countJ++;
                    }
                    else
                        matrizResultante[i, j] = double.NaN;
                }
                if (!localElemento[i].Equals(0) && !localElemento[i].Equals(0)) countI++;
            }
            return matrizResultante;
        }
        public double[,] calculaMatrizGlobalDosElementos(List<double[,]> elementosNaMatrizGlobal, int grausLiberdadeGlobal, string nome)
        {
            double[,] matrizResultante = new double[grausLiberdadeGlobal, grausLiberdadeGlobal];
            for (int h = 0; h < elementosNaMatrizGlobal.Count; h++)
            {
                if (h.Equals(0))
                {
                    matrizResultante = elementosNaMatrizGlobal[h];
                    continue;
                }
                for (int i = 0; i < grausLiberdadeGlobal; i++)
                {
                    for (int j = 0; j < grausLiberdadeGlobal; j++)
                    {
                        //percorre elementos
                        if (!double.IsNaN(elementosNaMatrizGlobal[h][i, j]) && !double.IsNaN(matrizResultante[i, j]))
                            matrizResultante[i, j] = matrizResultante[i, j] + elementosNaMatrizGlobal[h][i, j];
                        else if (!double.IsNaN(elementosNaMatrizGlobal[h][i, j]) && double.IsNaN(matrizResultante[i, j]))
                            matrizResultante[i, j] = elementosNaMatrizGlobal[h][i, j];
                        else if (double.IsNaN(elementosNaMatrizGlobal[h][i, j]) && !double.IsNaN(matrizResultante[i, j]))
                            matrizResultante[i, j] = matrizResultante[i, j];
                        else
                            matrizResultante[i, j] = 0;
                    }
                }
            }
            return matrizResultante;
        }
        public double[,] calculaMatrizRigidezEmCoordenadasLocais(string comprimentoBarraElem)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberDecimalDigits = 2;

            double b                    = double.Parse(textBoxB.Text, provider);
            double h                    = double.Parse(textBoxH.Text, provider);
            double moduloYoung          = double.Parse(textBoxModuloYoung.Text, provider) * Math.Pow(10, 9);
            double coeficientePoisson   = double.Parse(textBoxCoeficientePoisson.Text, provider);
            double comprimentoBarra     = double.Parse(comprimentoBarraElem, provider);

            //momento inércia
            double I = (b * Math.Pow(h, 3)) / 12;
            //modulo young
            double E = moduloYoung;
            //coeficiente Poisson
            double V = coeficientePoisson;
            //modulo de elasticidade transversal
            double G = E / (2 * (1 + V));
            //momento inércia torção
            double J = h * Math.Pow(b, 3) * (((double)1 / 3) - 0.21 * (b / h) * (1 - (Math.Pow(b, 4) / (12 * Math.Pow(h, 4)))));
            double[,] matrizRigidezElementoEmCoordenadasLocais = new double[6, 6]
            {
                {Math.Round((12*E*I)/Math.Pow(comprimentoBarra, 3), 3), 0, Math.Round((6*E*I)/Math.Pow(comprimentoBarra, 2), 3), Math.Round((-12*E*I)/Math.Pow(comprimentoBarra, 3), 3), 0, Math.Round((6*E*I)/Math.Pow(comprimentoBarra, 2), 3)},
                {0, Math.Round((G*J)/comprimentoBarra, 3), 0, 0, Math.Round((-G*J)/comprimentoBarra, 3), 0},
                {Math.Round((6*E*I)/Math.Pow(comprimentoBarra, 2), 3), 0, Math.Round((4*E*I)/comprimentoBarra, 3), Math.Round((-6*E*I)/Math.Pow(comprimentoBarra, 2), 3), 0, Math.Round((2*E*I)/comprimentoBarra, 3)},
                {Math.Round((-12*E*I)/Math.Pow(comprimentoBarra, 3), 3), 0, Math.Round((-6*E*I)/Math.Pow(comprimentoBarra, 2), 3), Math.Round((12*E*I)/Math.Pow(comprimentoBarra, 3), 3), 0, Math.Round((-6*E*I)/Math.Pow(comprimentoBarra, 2), 3)},
                {0, Math.Round((-G*J)/comprimentoBarra, 3), 0, 0, Math.Round((G*J)/comprimentoBarra, 3), 0},
                {Math.Round((6*E*I)/Math.Pow(comprimentoBarra, 2), 3), 0, Math.Round((2*E*I)/comprimentoBarra, 3), Math.Round((-6*E*I)/Math.Pow(comprimentoBarra, 2), 3), 0, Math.Round((4*E*I)/comprimentoBarra, 3)}
            };
            return matrizRigidezElementoEmCoordenadasLocais;
        }
        
        //MATRIZ DA ESTRUTURA COM AS CONDIÇÕES DE CONTORNO
        public double[,] defineMatrizEstruturaComCondicoesDeContorno(double[,] matrizGlobalDosElementos, int quantidadeNos, int grausLiberdadeGlobal)
        {
            double[,] matrizResultante = matrizGlobalDosElementos;
            for (int i = 1; i <= quantidadeNos; i++)
            {
                bool condicaoX = defineSeRestrito(i, "X");
                bool condicaoY = defineSeRestrito(i, "Y");
                bool condicaoZ = defineSeRestrito(i, "Z");
                
                if (condicaoY)
                {
                    int comecoNO = (i - 1) * 3;
                    aplicaCondicoesContornoPorGrauLiberdade(matrizResultante, comecoNO, grausLiberdadeGlobal);
                }
                if (condicaoX)
                {
                    int comecoNO = ((i - 1) * 3) + 1;
                    aplicaCondicoesContornoPorGrauLiberdade(matrizResultante, comecoNO, grausLiberdadeGlobal);
                }
                if (condicaoZ)
                {
                    int comecoNO = ((i - 1) * 3) + 2;
                    aplicaCondicoesContornoPorGrauLiberdade(matrizResultante, comecoNO, grausLiberdadeGlobal);
                }
            }
            Console.WriteLine("defineMatrizEstruturaComCondicoesDeContorno");
            Console.WriteLine(MatrixUtil.MatrixAsString(matrizResultante));
            return matrizResultante;
        }
        public double[,] aplicaCondicoesContornoPorGrauLiberdade(double[,] matrizResultante, int comecoNO, int grausLiberdadeGlobal)
        {
            for (int j = 0; j < grausLiberdadeGlobal; j++)
            {
                matrizResultante[comecoNO, j] = 0;
                matrizResultante[j, comecoNO] = 0;
            }
            matrizResultante[comecoNO, comecoNO] = 1;
            return matrizResultante;
        }
        public bool defineSeRestrito(int no, string grauLiberdade)
        {
            Control[] control = this.Controls.Find("checkBoxFixaNo" + no.ToString() + grauLiberdade, true);
            CheckBox checkBox = control[0] as CheckBox;
            return checkBox.Checked;
        }
        
        public void defineVetoresDeslocRotGlobalPeloNo(double[] vetorDeslocamentoERotacaoGlobal, int quantidadeElementos, int grausLiberdadeGlobal)
        {
            vetoresDeslocGiroGlobalElem = new List<double[]>();
            for (int i = 0; i < quantidadeElementos; i++)
            {
                int comecoNo             = i * 3;
                int count                = 0;
                double[] vetorResultante = new double[grausLiberdadeLocal.Length];
                for (int j = comecoNo; j < grausLiberdadeGlobal; j++)
                {
                    vetorResultante[count] = vetorDeslocamentoERotacaoGlobal[j];
                    count++;
                    if (count == grausLiberdadeLocal.Length) break;
                }
                vetoresDeslocGiroGlobalElem.Add(vetorResultante);
            }
        }
        public void aplicandoDeslocamentosLocaisElementos(int quantidadeElementos)
        {
            for (int i = 0; i < quantidadeElementos; i++)
                vetoresDeslocGiroGlobalElem[i] = MatrixUtil.multiplicacaoMatrizComVetor(matrizesRotPorAnguloDoElem[i], vetoresDeslocGiroGlobalElem[i], "DESLOCAMENTOS E GIROS - LOCAIS - ELEMENTO: " + (i + 1));
        }
        public void aplicandoDeslocamentosInternosElementos(int quantidadeElementos)
        {
            vetoresEsforcosInternosElem = new List<double[]>();
            for (int i = 0; i < quantidadeElementos; i++)
                vetoresEsforcosInternosElem.Add(MatrixUtil.multiplicacaoMatrizComVetor(matrizesLocaisElementos[i], vetoresDeslocGiroGlobalElem[i], "ESFORÇOS INTERNOS - ELEMENTO: " + (i + 1)));

            incrementaElementosGrafResultadoFinal(vetoresEsforcosInternosElem);
        }
        public void incrementaElementosGrafResultadoFinal(List<double[]> vetoresEsforcosInternosElem)
        {
            elementosGraficoDEC = new List<double[]>();
            elementosGraficoDMF = new List<double[]>();
            elementosGraficoDMT = new List<double[]>();

            for (int i = 0; i < vetoresEsforcosInternosElem.Count; i++)
                elementosGraficoDEC.Add(new double[] { vetoresEsforcosInternosElem[i][0], -(vetoresEsforcosInternosElem[i][3]) }); //DEC
            for (int i = 0; i < vetoresEsforcosInternosElem.Count; i++)
                elementosGraficoDMF.Add(new double[] { vetoresEsforcosInternosElem[i][1], vetoresEsforcosInternosElem[i][4] }); //DMT E DMF ESTÃO TROCADOS
            for (int i = 0; i < vetoresEsforcosInternosElem.Count; i++)
                elementosGraficoDMT.Add(new double[] { -(vetoresEsforcosInternosElem[i][2]), vetoresEsforcosInternosElem[i][5] }); //DMT E DMF ESTÃO TROCADOS
        }
        
        //EVENTOS
        private void limpaCamposElementosNos()
        {
            for (int j = 1; j <= 26; j++)
            {
                Control[] textBoxE   = this.Controls.Find("textBoxComprimentoE"+j.ToString(), true);
                TextBox thistextBoxE = textBoxE[0] as TextBox;
                thistextBoxE.Text    = string.Empty;

                Control[] comboBoxAng         = this.Controls.Find("comboBoxAnguloE" + j.ToString(), true);
                ComboBox thiscomboBoxAng      = comboBoxAng[0] as ComboBox;
                thiscomboBoxAng.SelectedIndex = 0;

                Control[] comboBoxDir         = this.Controls.Find("comboBoxAnguloDirE" + j.ToString(), true);
                ComboBox thiscomboBoxDir      = comboBoxDir[0] as ComboBox;
                thiscomboBoxDir.SelectedIndex = 0;

                Control[] textBoxForca   = this.Controls.Find("textBoxForcaNo" + j.ToString(), true);
                TextBox thistextBoxForca = textBoxForca[0] as TextBox;
                thistextBoxForca.Text    = string.Empty;
            }
        }

        private void buttonCalculaMatrizRigidezElementoEmCoordenadasLocais_Click(object sender, EventArgs e)
        {
            quantidadeNos        = Convert.ToInt32(textBoxQuantidadeNos.Text);
            grausLiberdadeGlobal = quantidadeNos * 3;
            quantidadeElementos  = Convert.ToInt32(numericUpDownQuantidadeElementos.Value);

            inicializaVetoresElementosLocais(grausLiberdadeGlobal, quantidadeElementos);
            inicializaMatrizGlobalEstrutura(grausLiberdadeGlobal, quantidadeElementos);

            double[][] inverse = MatrixUtil.MatrixInverse(defineMatrizEstruturaComCondicoesDeContorno(matrizGlobalEstrutura, quantidadeNos, grausLiberdadeGlobal));
            if (inverse == null)
            {
                MessageBox.Show("Matriz não pode ser invertida.");
                return;
            }
            double[,] matrizInversaGlobalDaEstrutura = MatrixUtil.MatrixReConverter(inverse);
            Console.WriteLine(MatrixUtil.MatrixAsString(matrizInversaGlobalDaEstrutura));

            inicializaVetorCargasExternas(matrizInversaGlobalDaEstrutura.GetLength(1), quantidadeNos);
            defineVetoresDeslocRotGlobalPeloNo(MatrixUtil.multiplicacaoMatrizComVetor(matrizInversaGlobalDaEstrutura, vetorCargasExternas, "VETOR DE DESLOCAMENTO E GIROS - GLOBAL"), quantidadeElementos, grausLiberdadeGlobal);
            aplicandoDeslocamentosLocaisElementos(quantidadeElementos);
            aplicandoDeslocamentosInternosElementos(quantidadeElementos);
            //subtracaoEsforcosIntVetorCargasEle(vetoresEsforcosInternosElem, vetor);

            FormGrafico graf = new FormGrafico(elementosGraficoDEC, elementosGraficoDMF, elementosGraficoDMT, quantidadeElementos,
                                                elementosGraficoBase, vetoresDeslocGiroGlobalElem, vetoresEsforcosInternosElem);

            inverse = null;

            graf.Show();
        }

        private void numericUpDownQuantidadeElementos_ValueChanged(object sender, EventArgs e)
        {
            int elementos = (int)numericUpDownQuantidadeElementos.Value;
            //LOOP PARA DESABILITAR GRUPBOXS
            for (int j = elementos; j < 27; j++)
            {
                Control[] groupBoxElemento    = this.Controls.Find("groupBoxElemento" + j.ToString(), true);
                GroupBox thisGroupBoxElemento = groupBoxElemento[0] as GroupBox;

                Control[] groupBoxNo    = this.Controls.Find("groupBoxNo" + (j + 1).ToString(), true);
                GroupBox thisGroupBoxNo = groupBoxNo[0] as GroupBox;

                thisGroupBoxNo.Visible       = false;
                thisGroupBoxNo.Enabled       = false;
                thisGroupBoxElemento.Visible = false;
                thisGroupBoxElemento.Enabled = false;
            }
            //LOOP PARA HABILITAR GRUPBOXS
            for (int i = 1; i <= elementos; i++)
            {
                Control[] groupBoxElemento    = this.Controls.Find("groupBoxElemento" + i.ToString(), true);
                GroupBox thisGroupBoxElemento = groupBoxElemento[0] as GroupBox;

                Control[] groupBoxNo    = this.Controls.Find("groupBoxNo" + (i+1).ToString(), true);
                GroupBox thisGroupBoxNo = groupBoxNo[0] as GroupBox;

                thisGroupBoxNo.Visible       = true;
                thisGroupBoxNo.Enabled       = true;
                thisGroupBoxElemento.Visible = true;
                thisGroupBoxElemento.Enabled = true;
            }
            textBoxQuantidadeNos.Text = ((int)numericUpDownQuantidadeElementos.Value + 1).ToString();
            comboBoxAngulo_SelectedIndexChanged(numericUpDownQuantidadeElementos, null);
        }
        /*
         * HABILITA OU DESABILITA TEXTBOX DA FORÇA DO NÓ
         * */
        public void verificaForcaNo(int numero)
        {
            string[] letras = new string[] { "X", "Y", "Z" };

            Control[] textBoxForcaNo    = this.Controls.Find("textBoxForcaNo" + numero.ToString(), true);
            TextBox thistextBoxForcaNo  = textBoxForcaNo[0] as TextBox;

            for (int i = 0; i < letras.Length; i++)
            {
                Control[] control = this.Controls.Find("checkBoxFixaNo" + numero.ToString() + letras[i], true);
                CheckBox checkBox = control[0] as CheckBox;

                if (!checkBox.Checked) thistextBoxForcaNo.Enabled = true;
                else
                {
                    thistextBoxForcaNo.Enabled = false;
                    break;
                }
            }
        }

        private void buttonProximo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxB.Text) &&
                !string.IsNullOrEmpty(textBoxH.Text))
            {
                panel2.Enabled                           = true;
                panel2.Visible                           = true;
                panel3.Enabled                           = true;
                panel3.Visible                           = true;
                chart1.Enabled                           = true;
                chart1.Visible                           = true;
                buttonProximo.Visible                    = false;
                buttonLimpar.Enabled                     = true;
                elementosGraficoBase                     = new List<double[]>();
                labelQtdEementos.Visible                 = true;
                labelQtdNos.Visible                      = true;
                numericUpDownQuantidadeElementos.Visible = true;
                textBoxQuantidadeNos.Visible             = true;
            }
            else
            {
                MessageBox.Show("Preencha os campos");
            }
        }

        private void buttonLimpar_Click(object sender, EventArgs e)
        {
            limpaCamposElementosNos();
            chart1.Series.Clear();
            numericUpDownQuantidadeElementos.Value   = 1;
            textBoxQuantidadeNos.Text                = ((int)(numericUpDownQuantidadeElementos.Value + 1)).ToString();
            textBoxB.Text                            = string.Empty;
            textBoxH.Text                            = string.Empty;
            textBoxModuloYoung.Text                  = string.Empty;
            textBoxCoeficientePoisson.Text           = string.Empty;
            panel2.Visible                           = false;
            panel3.Visible                           = false;
            buttonProximo.Visible                    = true;
            labelQtdEementos.Visible                 = false;
            labelQtdNos.Visible                      = false;
            numericUpDownQuantidadeElementos.Visible = false;
            textBoxQuantidadeNos.Visible             = false;
        }

        private void timerintro_Tick(object sender, EventArgs e)
        {
            this.Controls.Remove(panelintro);
            panelintro.Dispose();
            panel1.Visible = true;
            timerintro.Enabled = false;
        }

        string[] separador = { "No" };

        private void checkBoxFixaNo1_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo1X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo1X.Checked) checkBoxFixaNo1Z.Checked = true; else checkBoxFixaNo1Z.Checked = false;
            //if (checkBoxFixaNo1Z.Checked) checkBoxFixaNo1X.Checked = true; else checkBoxFixaNo1X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo2_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo2X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo2X.Checked) checkBoxFixaNo2Z.Checked = true; else checkBoxFixaNo2Z.Checked = false;
            //if (checkBoxFixaNo2Z.Checked) checkBoxFixaNo2X.Checked = true; else checkBoxFixaNo2X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo3_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo3X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo3X.Checked) checkBoxFixaNo3Z.Checked = true; else checkBoxFixaNo3Z.Checked = false;
            //if (checkBoxFixaNo3Z.Checked) checkBoxFixaNo3X.Checked = true; else checkBoxFixaNo3X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo4_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo4X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo4X.Checked) checkBoxFixaNo4Z.Checked = true; else checkBoxFixaNo4Z.Checked = false;
            //if (checkBoxFixaNo4Z.Checked) checkBoxFixaNo4X.Checked = true; else checkBoxFixaNo4X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo5_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo5X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo5X.Checked) checkBoxFixaNo5Z.Checked = true; else checkBoxFixaNo5Z.Checked = false;
            //if (checkBoxFixaNo5Z.Checked) checkBoxFixaNo5X.Checked = true; else checkBoxFixaNo5X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo6_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo6X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo6X.Checked) checkBoxFixaNo6Z.Checked = true; else checkBoxFixaNo6Z.Checked = false;
            //if (checkBoxFixaNo6Z.Checked) checkBoxFixaNo6X.Checked = true; else checkBoxFixaNo6X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo7_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo7X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo7X.Checked) checkBoxFixaNo7Z.Checked = true; else checkBoxFixaNo7Z.Checked = false;
            //if (checkBoxFixaNo7Z.Checked) checkBoxFixaNo7X.Checked = true; else checkBoxFixaNo7X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo8_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo8X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo8X.Checked) checkBoxFixaNo8Z.Checked = true; else checkBoxFixaNo8Z.Checked = false;
            //if (checkBoxFixaNo8Z.Checked) checkBoxFixaNo8X.Checked = true; else checkBoxFixaNo8X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo9_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo9X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo9X.Checked) checkBoxFixaNo9Z.Checked = true; else checkBoxFixaNo9Z.Checked = false;
            //if (checkBoxFixaNo9Z.Checked) checkBoxFixaNo9X.Checked = true; else checkBoxFixaNo9X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo10_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo10X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo10X.Checked) checkBoxFixaNo10Z.Checked = true; else checkBoxFixaNo10Z.Checked = false;
            //if (checkBoxFixaNo10Z.Checked) checkBoxFixaNo10X.Checked = true; else checkBoxFixaNo10X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo11_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo11X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo11X.Checked) checkBoxFixaNo11Z.Checked = true; else checkBoxFixaNo11Z.Checked = false;
            //if (checkBoxFixaNo11Z.Checked) checkBoxFixaNo11X.Checked = true; else checkBoxFixaNo11X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo12_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString = checkBoxFixaNo12X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo12X.Checked) checkBoxFixaNo12Z.Checked = true; else checkBoxFixaNo12Z.Checked = false;
            //if (checkBoxFixaNo12Z.Checked) checkBoxFixaNo12X.Checked = true; else checkBoxFixaNo12X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo13_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo13X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo13X.Checked) checkBoxFixaNo13Z.Checked = true; else checkBoxFixaNo13Z.Checked = false;
            //if (checkBoxFixaNo13Z.Checked) checkBoxFixaNo13X.Checked = true; else checkBoxFixaNo13X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo14_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString = checkBoxFixaNo14X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo14X.Checked) checkBoxFixaNo14Z.Checked = true; else checkBoxFixaNo14Z.Checked = false;
            //if (checkBoxFixaNo14Z.Checked) checkBoxFixaNo14X.Checked = true; else checkBoxFixaNo14X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo15_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo15X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo15X.Checked) checkBoxFixaNo15Z.Checked = true; else checkBoxFixaNo15Z.Checked = false;
            //if (checkBoxFixaNo15Z.Checked) checkBoxFixaNo15X.Checked = true; else checkBoxFixaNo15X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo16_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString = checkBoxFixaNo16X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo16X.Checked) checkBoxFixaNo16Z.Checked = true; else checkBoxFixaNo16Z.Checked = false;
            //if (checkBoxFixaNo16Z.Checked) checkBoxFixaNo16X.Checked = true; else checkBoxFixaNo16X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo17_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo17X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo17X.Checked) checkBoxFixaNo17Z.Checked = true; else checkBoxFixaNo17Z.Checked = false;
            //if (checkBoxFixaNo17Z.Checked) checkBoxFixaNo17X.Checked = true; else checkBoxFixaNo17X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo18_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo18X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo18X.Checked) checkBoxFixaNo18Z.Checked = true; else checkBoxFixaNo18Z.Checked = false;
            //if (checkBoxFixaNo18Z.Checked) checkBoxFixaNo18X.Checked = true; else checkBoxFixaNo18X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo19_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString = checkBoxFixaNo19X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo19X.Checked) checkBoxFixaNo19Z.Checked = true; else checkBoxFixaNo19Z.Checked = false;
            //if (checkBoxFixaNo19Z.Checked) checkBoxFixaNo19X.Checked = true; else checkBoxFixaNo19X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo20_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo20X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo20X.Checked) checkBoxFixaNo20Z.Checked = true; else checkBoxFixaNo20Z.Checked = false;
            //if (checkBoxFixaNo20Z.Checked) checkBoxFixaNo20X.Checked = true; else checkBoxFixaNo20X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo21_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo21X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo21X.Checked) checkBoxFixaNo21Z.Checked = true; else checkBoxFixaNo21Z.Checked = false;
            //if (checkBoxFixaNo21Z.Checked) checkBoxFixaNo21X.Checked = true; else checkBoxFixaNo21X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo22_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo22X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo22X.Checked) checkBoxFixaNo22Z.Checked = true; else checkBoxFixaNo22Z.Checked = false;
            //if (checkBoxFixaNo22Z.Checked) checkBoxFixaNo22X.Checked = true; else checkBoxFixaNo22X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo23_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo23X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo23X.Checked) checkBoxFixaNo23Z.Checked = true; else checkBoxFixaNo23Z.Checked = false;
            //if (checkBoxFixaNo23Z.Checked) checkBoxFixaNo23X.Checked = true; else checkBoxFixaNo23X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo24_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo24X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo24X.Checked) checkBoxFixaNo24Z.Checked = true; else checkBoxFixaNo24Z.Checked = false;
            //if (checkBoxFixaNo24Z.Checked) checkBoxFixaNo24X.Checked = true; else checkBoxFixaNo24X.Checked = false;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo25_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo25X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo25X.Checked) checkBoxFixaNo25Z.Checked = true; else checkBoxFixaNo25Z.Enabled = true;
            //if (checkBoxFixaNo25Z.Checked) checkBoxFixaNo25X.Checked = true; else checkBoxFixaNo25X.Enabled = true;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo26_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo26X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo26X.Checked) checkBoxFixaNo26Z.Enabled = false; else checkBoxFixaNo26Z.Enabled = true;
            //if (checkBoxFixaNo26Z.Checked) checkBoxFixaNo26X.Enabled = false; else checkBoxFixaNo26X.Enabled = true;
            verificaForcaNo(numeroNo);
        }
        private void checkBoxFixaNo27_CheckedChanged(object sender, EventArgs e)
        {
            string[] quebraString   = checkBoxFixaNo27X.Name.Split(separador, 3, StringSplitOptions.RemoveEmptyEntries);
            int numeroNo            = Convert.ToInt32(quebraString[1].Remove(quebraString[1].Length - 1));
            //if (checkBoxFixaNo27X.Checked) checkBoxFixaNo27Z.Enabled = false; else checkBoxFixaNo27Z.Enabled = true;
            //if (checkBoxFixaNo27Z.Checked) checkBoxFixaNo27X.Enabled = false; else checkBoxFixaNo27X.Enabled = true;
            verificaForcaNo(numeroNo);
        }
        
        private void comboBoxAngulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int quantidadeElementos = Convert.ToInt32(numericUpDownQuantidadeElementos.Value);
            List<double[]> elementosGraficoBase = new List<double[]>();

            for (int i = 1; i <= quantidadeElementos; i++)
            {
                Control[] control = this.Controls.Find("comboBoxAnguloE" + i.ToString(), true);
                ComboBox comboBox = control[0] as ComboBox;

                if(comboBox.Text != string.Empty) elementosGraficoBase.Add(new double[] { 0.0, double.Parse(comboBox.Text)});
            }
            FormGrafico graf = new FormGrafico(quantidadeElementos, elementosGraficoBase);
            graf.defineGrafico(chart1);
        }

        private void comboBoxAnguloE_EnabledChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            combobox.Text = "0";
        }

        private void comboBoxAnguloDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            int quantidadeElementos = Convert.ToInt32(numericUpDownQuantidadeElementos.Value);
            List<double[]> elementosGraficoBase = new List<double[]>();

            for (int i = 1; i <= quantidadeElementos; i++)
            {
                Control[] control1 = this.Controls.Find("comboBoxAnguloE" + i.ToString(), true);
                ComboBox comboBox = control1[0] as ComboBox;
                Control[] control2 = this.Controls.Find("comboBoxAnguloDirE" + i.ToString(), true);
                ComboBox comboBoxDir = control2[0] as ComboBox;

                if (comboBox.Text == string.Empty) return;

                double anguloConvertido = comboBoxDir.Text == "-" ? -Convert.ToInt32(comboBox.Text) : Convert.ToInt32(comboBox.Text);

                if (comboBox.Text != string.Empty) elementosGraficoBase.Add(new double[] { 0.0, anguloConvertido});
            }
            FormGrafico graf = new FormGrafico(quantidadeElementos, elementosGraficoBase);
            graf.defineGrafico(chart1);
        }

        private void comboBoxAnguloE_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            string[] separador = { "AnguloE" };

            string[] numeroE = combobox.Name.Split(separador, 2, StringSplitOptions.RemoveEmptyEntries);

            Control[] control = this.Controls.Find("comboBoxAnguloDirE" + numeroE[1].ToString(), true);
            ComboBox comboboxDir = control[0] as ComboBox;

            if (combobox.SelectedItem.Equals("0")) comboboxDir.Enabled = false;
            else comboboxDir.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSobre sobre = new FormSobre();
            sobre.Show();
        }
    }
}