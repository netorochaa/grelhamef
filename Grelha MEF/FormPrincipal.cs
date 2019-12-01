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

            matrizGlobalEstrutura = calculaMatrizGlobalDosElementos(matrizesGlobaisElementosEspalhamento, grausLiberdadeGlobal, "MATRIZ GLOBAL DA ESTRUTURA");
        }

        public void inicializaVetorCargasExternas(int tamanhoVetor, int quantidadeNos)
        {
            vetorCargasExternas = new double[tamanhoVetor];

            //Console.WriteLine("\r\n VETOR DE CARGAS EXTERNAS \r\n");

            for (int i = 1; i <= quantidadeNos; i++)
            {
                Control[] control = this.Controls.Find("textBoxForcaNo" + i.ToString(), true);
                TextBox textbox = control[0] as TextBox;

                if (textbox.Enabled && !String.IsNullOrEmpty(textbox.Text.Trim()))
                {
                    vetorCargasExternas[(i - 1) * quantidadeNos] = double.Parse(textbox.Text);
                }
            }

            //for (int i2 = 0; i2 < vetorCargasExternas.Length; i2++)
            //    Console.WriteLine("Indice [" + (i2 + 1) + "]" + vetorCargasExternas[i2]);
        }

        public double[,] espalhamentoMatrizRigidezGlobalNoSistemaGlobal(double[,] matrizGlobalDoElemento, int grausLiberdadeGlobal, int[] localElemento, string nome)
        {
            int countI = 0;
            double[,] matrizResultante = new double[grausLiberdadeGlobal, grausLiberdadeGlobal];

            //Console.WriteLine("\r\n" + nome + "\r\n");

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
                    {
                        matrizResultante[i, j] = double.NaN;
                    }
                    //Console.WriteLine("\r\nIndice [" + (i + 1) + ", " + (j + 1) + "] " + matrizResultante[i, j]);
                }
                if (!localElemento[i].Equals(0) && !localElemento[i].Equals(0)) countI++;
                //if (countI.Equals(6)) countI = countI - 3;
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

            double b = double.Parse(textBoxB.Text, provider);
            double h = double.Parse(textBoxH.Text, provider);
            double moduloYoung = double.Parse(textBoxModuloYoung.Text, provider) * Math.Pow(10, 9);
            double coeficientePoisson = double.Parse(textBoxCoeficientePoisson.Text, provider);
            double comprimentoBarra = double.Parse(comprimentoBarraElem, provider);

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

                if (condicaoX)
                {
                    int comecoNO = (i - 1) * 3;
                    aplicaCondicoesContornoPorGrauLiberdade(matrizResultante, comecoNO, grausLiberdadeGlobal);

                }
                if (condicaoY)
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
            Control[] control = this.Controls.Find("checkedListBoxNo" + no.ToString() + grauLiberdade, true);
            CheckedListBox checkedListBox = control[0] as CheckedListBox;
            return checkedListBox.GetItemChecked(1);
        }

        public void defineVetoresDeslocRotGlobalPeloNo(double[] vetorDeslocamentoERotacaoGlobal, int quantidadeElementos, int grausLiberdadeGlobal)
        {
            vetoresDeslocGiroGlobalElem = new List<double[]>();

            for (int i = 0; i < quantidadeElementos; i++)
            {
                int comecoNo = i * 3;
                int count = 0;
                double[] vetorResultante = new double[grausLiberdadeLocal.Length];

                for (int j = comecoNo; j < grausLiberdadeGlobal; j++)
                {
                    vetorResultante[count] = vetorDeslocamentoERotacaoGlobal[j];
                    //Console.WriteLine(vetorResultante[count]);
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
                elementosGraficoDEC.Add(new double[] { vetoresEsforcosInternosElem[i][0], vetoresEsforcosInternosElem[i][3] }); //DEC

            for (int i = 0; i < vetoresEsforcosInternosElem.Count; i++)
                elementosGraficoDMF.Add(new double[] { vetoresEsforcosInternosElem[i][1], vetoresEsforcosInternosElem[i][4] }); // DMF

            for (int i = 0; i < vetoresEsforcosInternosElem.Count; i++)
                elementosGraficoDMT.Add(new double[] { vetoresEsforcosInternosElem[i][2], vetoresEsforcosInternosElem[i][5] }); // DMT
        }
        //EVENTOS
        private void limpaCamposElementosNos()
        {
            for (int j = 1; j <= 26; j++)
            {
                Control[] textBoxE = this.Controls.Find("textBoxComprimentoE"+j.ToString(), true);
                TextBox thistextBoxE = textBoxE[0] as TextBox;
                thistextBoxE.Text = string.Empty;

                Control[] comboBoxAng = this.Controls.Find("comboBoxAnguloE" + j.ToString(), true);
                ComboBox thiscomboBoxAng = comboBoxAng[0] as ComboBox;
                thiscomboBoxAng.SelectedIndex = 0;

                Control[] comboBoxDir = this.Controls.Find("comboBoxAnguloDirE" + j.ToString(), true);
                ComboBox thiscomboBoxDir = comboBoxDir[0] as ComboBox;
                thiscomboBoxDir.SelectedIndex = 0;

                Control[] textBoxForca = this.Controls.Find("textBoxForcaNo" + j.ToString(), true);
                TextBox thistextBoxForca = textBoxForca[0] as TextBox;
                thistextBoxForca.Text = string.Empty;
                
            }
        }

        private void buttonCalculaMatrizRigidezElementoEmCoordenadasLocais_Click(object sender, EventArgs e)
        {
            quantidadeNos = Convert.ToInt32(textBoxQuantidadeNos.Text);
            grausLiberdadeGlobal = Convert.ToInt32(quantidadeNos * 3);
            quantidadeElementos = Convert.ToInt32(numericUpDownQuantidadeElementos.Value);

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

            FormGrafico graf = new FormGrafico(elementosGraficoDEC, elementosGraficoDMF, elementosGraficoDMT, quantidadeElementos, elementosGraficoBase);
            graf.Show();
        }

        private void numericUpDownQuantidadeElementos_ValueChanged(object sender, EventArgs e)
        {
            int elementos = (int)numericUpDownQuantidadeElementos.Value;
            //LOOP PARA DESABILITAR GRUPBOXS
            for (int j = elementos; j < 24; j++)
            {
                Control[] groupBoxElemento = this.Controls.Find("groupBoxElemento" + j.ToString(), true);
                GroupBox thisGroupBoxElemento = groupBoxElemento[0] as GroupBox;

                Control[] groupBoxNo = this.Controls.Find("groupBoxNo" + (j + 1).ToString(), true);
                GroupBox thisGroupBoxNo = groupBoxNo[0] as GroupBox;

                thisGroupBoxNo.Visible = false;
                thisGroupBoxNo.Enabled = false;
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
        }

        public void selecaoUnicaNoCheckedlistbox(CheckedListBox check)
        {
            if (check.SelectedIndex.Equals(0)) check.SetItemChecked(1, false);
            else if (check.SelectedIndex.Equals(1))
            {
                check.SetItemChecked(0, false);
            }
        }

        public void verificaForcaNo1()
        {
            if (checkedListBoxNo1X.SelectedIndex.Equals(0) &&
                checkedListBoxNo1Y.SelectedIndex.Equals(0) &&
                checkedListBoxNo1Z.SelectedIndex.Equals(0)) textBoxForcaNo1.Enabled = true;
            else textBoxForcaNo1.Enabled = false;
        }

        public void verificaForcaNo2()
        {
            if (checkedListBoxNo2X.SelectedIndex.Equals(0) &&
                checkedListBoxNo2Y.SelectedIndex.Equals(0) &&
                checkedListBoxNo2Z.SelectedIndex.Equals(0)) textBoxForcaNo2.Enabled = true;
            else textBoxForcaNo2.Enabled = false;
        }

        public void verificaForcaNo3()
        {
            if (checkedListBoxNo3X.SelectedIndex.Equals(0) &&
                checkedListBoxNo3Y.SelectedIndex.Equals(0) &&
                checkedListBoxNo3Z.SelectedIndex.Equals(0)) textBoxForcaNo3.Enabled = true;
            else textBoxForcaNo3.Enabled = false;
        }

        public void verificaForcaNo4()
        {
            if (checkedListBoxNo4X.SelectedIndex.Equals(0) &&
                checkedListBoxNo4Y.SelectedIndex.Equals(0) &&
                checkedListBoxNo4Z.SelectedIndex.Equals(0)) textBoxForcaNo4.Enabled = true;
            else textBoxForcaNo4.Enabled = false;
        }

        public void verificaForcaNo5()
        {
            if (checkedListBoxNo5X.SelectedIndex.Equals(0) &&
                checkedListBoxNo5Y.SelectedIndex.Equals(0) &&
                checkedListBoxNo5Z.SelectedIndex.Equals(0)) textBoxForcaNo5.Enabled = true;
            else textBoxForcaNo5.Enabled = false;
        }

        public void verificaForcaNo6()
        {
            if (checkedListBoxNo6X.SelectedIndex.Equals(0) &&
                checkedListBoxNo6Y.SelectedIndex.Equals(0) &&
                checkedListBoxNo6Z.SelectedIndex.Equals(0)) textBoxForcaNo6.Enabled = true;
            else textBoxForcaNo6.Enabled = false;
        }

        public bool verificaPossivelHabilitacaoDoCampoForca()
        {
            int i = 0;

            if (textBoxForcaNo1.Enabled) i++;
            if (textBoxForcaNo2.Enabled) i++;
            if (textBoxForcaNo3.Enabled) i++;
            if (textBoxForcaNo4.Enabled) i++;
            if (textBoxForcaNo5.Enabled) i++;
            if (textBoxForcaNo6.Enabled) i++;

            if (i.Equals(Convert.ToInt32(textBoxQuantidadeNos.Text))) return false;
            else return true;
        }

        private void checkedListBoxNo1X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo1X);
            verificaForcaNo1();
        }

        private void checkedListBoxNo1Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo1Y);
            verificaForcaNo1();
        }

        private void checkedListBoxNo1Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo1Z);
            verificaForcaNo1();
        }

        private void checkedListBoxNo2X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo2X);
            verificaForcaNo2();
        }

        private void checkedListBoxNo2Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo2Y);
            verificaForcaNo2();
        }

        private void checkedListBoxNo2Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo2Z);
            verificaForcaNo2();
        }

        private void checkedListBoxNo3X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo3X);
            verificaForcaNo3();
        }

        private void checkedListBoxNo3Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo3Y);
            verificaForcaNo3();
        }

        private void checkedListBoxNo3Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo3Z);
            verificaForcaNo3();
        }

        private void checkedListBoxNo4X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo4X);
            verificaForcaNo4();
        }

        private void checkedListBoxNo4Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo4Y);
            verificaForcaNo4();
        }

        private void checkedListBoxNo4Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo4Z);
            verificaForcaNo4();
        }

        private void checkedListBoxNo5X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo5X);
            verificaForcaNo5();
        }

        private void checkedListBoxNo5Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo5Y);
            verificaForcaNo5();
        }

        private void checkedListBoxNo5Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo5Z);
            verificaForcaNo5();
        }

        private void checkedListBoxNo6X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo6X);
            verificaForcaNo6();
        }

        private void checkedListBoxNo6Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo6Y);
            verificaForcaNo6();
        }

        private void checkedListBoxNo6Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo5Z);
            verificaForcaNo6();
        }

        private void textBoxForcaNo1_EnabledChanged(object sender, EventArgs e)
        {
            if (!verificaPossivelHabilitacaoDoCampoForca()) textBoxForcaNo1.Enabled = false;
        }

        private void textBoxForcaNo2_EnabledChanged(object sender, EventArgs e)
        {
            if (!verificaPossivelHabilitacaoDoCampoForca()) textBoxForcaNo2.Enabled = false;
        }

        private void textBoxForcaNo3_EnabledChanged(object sender, EventArgs e)
        {
            if (!verificaPossivelHabilitacaoDoCampoForca()) textBoxForcaNo3.Enabled = false;
        }

        private void textBoxForcaNo4_EnabledChanged(object sender, EventArgs e)
        {
            if (!verificaPossivelHabilitacaoDoCampoForca()) textBoxForcaNo4.Enabled = false;
        }

        private void textBoxForcaNo5_EnabledChanged(object sender, EventArgs e)
        {
            if (!verificaPossivelHabilitacaoDoCampoForca()) textBoxForcaNo5.Enabled = false;
        }

        private void textBoxForcaNo6_EnabledChanged(object sender, EventArgs e)
        {
            if (!verificaPossivelHabilitacaoDoCampoForca()) textBoxForcaNo6.Enabled = false;
        }

        private void comboBoxAnguloE1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE1.SelectedItem.Equals("0")) comboBoxAnguloDirE1.Enabled = false;
            else comboBoxAnguloDirE1.Enabled = true;
        }

        private void comboBoxAnguloE2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE2.SelectedItem.Equals("0")) comboBoxAnguloDirE2.Enabled = false;
            else comboBoxAnguloDirE2.Enabled = true;
        }

        private void comboBoxAnguloE3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE3.SelectedItem.Equals("0")) comboBoxAnguloDirE3.Enabled = false;
            else comboBoxAnguloDirE3.Enabled = true;
        }

        private void comboBoxAnguloE4_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE4.SelectedItem.Equals("0")) comboBoxAnguloDirE4.Enabled = false;
            else comboBoxAnguloDirE4.Enabled = true;
        }

        private void comboBoxAnguloE5_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE5.SelectedItem.Equals("0")) comboBoxAnguloDirE5.Enabled = false;
            else comboBoxAnguloDirE5.Enabled = true;
        }

        private void comboBoxAnguloE6_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE6.SelectedItem.Equals("0")) comboBoxAnguloDirE6.Enabled = false;
            else comboBoxAnguloDirE6.Enabled = true;
        }

        private void comboBoxAnguloE7_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE7.SelectedItem.Equals("0")) comboBoxAnguloDirE7.Enabled = false;
            else comboBoxAnguloDirE7.Enabled = true;
        }

        private void comboBoxAnguloE8_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE8.SelectedItem.Equals("0")) comboBoxAnguloDirE8.Enabled = false;
            else comboBoxAnguloDirE8.Enabled = true;
        }

        private void comboBoxAnguloE9_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE9.SelectedItem.Equals("0")) comboBoxAnguloDirE9.Enabled = false;
            else comboBoxAnguloDirE9.Enabled = true;
        }

        private void comboBoxAnguloE10_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxAnguloE10.SelectedItem.Equals("0")) comboBoxAnguloDirE10.Enabled = false;
            else comboBoxAnguloDirE10.Enabled = true;
        }

        private void buttonProximo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxB.Text) &&
                !string.IsNullOrEmpty(textBoxH.Text))
            {
                panel2.Enabled = true;
                panel2.Visible = true;
                panel3.Enabled = true;
                panel3.Visible = true;
                chart1.Enabled = true;
                chart1.Visible = true;
                textBoxB.ReadOnly = true;
                textBoxH.ReadOnly = true;
                buttonProximo.Enabled = false;
                buttonLimpar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Preencha os campos");
            }
        }

        private void buttonLimpar_Click(object sender, EventArgs e)
        {
            limpaCamposElementosNos();
            numericUpDownQuantidadeElementos.Value = 1;
            textBoxQuantidadeNos.Text = ((int)(numericUpDownQuantidadeElementos.Value + 1)).ToString();
            chart1.Series.Clear();
            textBoxB.Text = string.Empty;
            textBoxH.Text = string.Empty;
            textBoxModuloYoung.Text = string.Empty;
            textBoxCoeficientePoisson.Text = string.Empty;
            panel2.Visible = false;
            panel3.Visible = false;
            textBoxB.ReadOnly = false;
            textBoxH.ReadOnly = false;
            buttonProximo.Enabled = true;
        }

        private void timerintro_Tick(object sender, EventArgs e)
        {
            this.Controls.Remove(panelintro);
            panelintro.Dispose();
        }
    }
}