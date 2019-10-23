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

namespace Grelha_MEF
{
    public partial class Form1 : Form
    {   
        //Matriz de rigidez local
        //elemento 1
        public static double[,] rigidezLocalElemento1 = new double[6, 6] 
            {  
                {11, 12, 13, 14, 15, 16},
                {21, 22, 23, 24, 25, 26},
                {31, 32, 33, 34, 35, 36},
                {41, 42, 43, 44, 45, 46},
                {51, 52, 53, 54, 55, 56},
                {61, 62, 63, 64, 65, 66}
            };
        //elemento 2
        public static double[,] rigidezLocalElemento2 = new double[6, 6] 
            {  
                {11, 12, 13, 14, 15, 16},
                {21, 22, 23, 24, 25, 26},
                {31, 32, 33, 34, 35, 36},
                {41, 42, 43, 44, 45, 46},
                {51, 52, 53, 54, 55, 56},
                {61, 62, 63, 64, 65, 66}
            };

        //Graus de liberdade (associando local a global)
        //no 1
        public static int[] sistemaGlobalGrausLiberdadeNo1 = new int[] { 1, 2, 3 };
        public static int[] sistemaLocalElemento1No1       = new int[] { 1, 2, 3 };
        public static int[] sistemaLocalElemento2No1       = new int[] { 1, 2, 3 };
        //no 2
        public static int[] sistemaGlobalGrausLiberdadeNo2 = new int[] { 4, 5, 6 };
        public static int[] sistemaLocalElemento1No2       = new int[] { 0, 0, 0 };
        public static int[] sistemaLocalElemento2No2       = new int[] { 4, 5, 6 };
        //no 3
        public static int[] sistemaGlobalGrausLiberdadeNo3 = new int[] { 7, 8, 9 };
        public static int[] sistemaLocalElemento1No3       = new int[] { 4, 5, 6 };
        public static int[] sistemaLocalElemento2No3       = new int[] { 0, 0, 0 };
        //item 1: graus de liberdade
        //item 2: elemento 1
        //item 3: elemento 2
        public static Tuple<int[], int[], int[]> grauLiberdadeNo1AssociandoLocalAGlobal = new Tuple<int[], int[], int[]>(sistemaGlobalGrausLiberdadeNo1, sistemaLocalElemento1No1, sistemaLocalElemento2No1);
        public static Tuple<int[], int[], int[]> grauLiberdadeNo2AssociandoLocalAGlobal = new Tuple<int[], int[], int[]>(sistemaGlobalGrausLiberdadeNo2, sistemaLocalElemento1No2, sistemaLocalElemento2No2);
        public static Tuple<int[], int[], int[]> grauLiberdadeNo3AssociandoLocalAGlobal = new Tuple<int[], int[], int[]>(sistemaGlobalGrausLiberdadeNo3, sistemaLocalElemento1No3, sistemaLocalElemento2No3);

        //Graus de liberdade total
        public static int quantidadeGrausLiberdadeGlobal = sistemaGlobalGrausLiberdadeNo1.Length + sistemaGlobalGrausLiberdadeNo2.Length + sistemaGlobalGrausLiberdadeNo3.Length;
        public static int[] grausLiberdadeGlobal         = new int[quantidadeGrausLiberdadeGlobal];

        //Vetores totais do elemento 1
        public static int quantidadeLocalElemento1 = sistemaLocalElemento1No1.Length + sistemaLocalElemento1No2.Length + sistemaLocalElemento2No3.Length;
        public static int[] localElemento1         = new int[quantidadeLocalElemento1];
        //Cria matriz local do elemento 2
        double[,] matrizLocalSistemaGlobalElemeto1 = new double[grausLiberdadeGlobal.Length, grausLiberdadeGlobal.Length];

        //Vetores totais do elemento 2
        public static int quantidadeLocalElemento2 = sistemaLocalElemento2No1.Length + sistemaLocalElemento2No2.Length + sistemaLocalElemento2No3.Length;
        public static int[] localElemento2         = new int[quantidadeLocalElemento2];
        //Cria matriz local do elemento 2
        double[,] matrizLocalSistemaGlobalElemeto2 = new double[grausLiberdadeGlobal.Length, grausLiberdadeGlobal.Length];

        //Matriz global dos elementos
        string[,] matrizGlobalEstrutura = new string[grausLiberdadeGlobal.Length, grausLiberdadeGlobal.Length];

        //Associando nó global a nó local
        //elemento 1
        public static int[] noLocalElemento1  = { 1, 2 };
        public static int[] noGlobalElemento1 = { 1, 3 };
        //elemento 2
        public static int[] noLocalElemento2  = { 1, 2 };
        public static int[] noGlobalElemento2 = { 1, 2 };  
  
        //
        public static double[,] matrizGlobalDosElementos;

        public Form1()
        {
            InitializeComponent();

            groupBoxElemento3.Visible = false;
            groupBoxElemento4.Visible = false;
            groupBoxElemento5.Visible = false;
            groupBoxElemento6.Visible = false;
            groupBoxElemento7.Visible = false;
            groupBoxElemento8.Visible = false;
            groupBoxElemento9.Visible = false;
            groupBoxElemento10.Visible = false;

            inicializaVetores();
            
            double[,] matrizGlobalDoElemento1 = multiplicacaoMatrizes(multiplicacaoMatrizes(defineMatrizRotacaoPeloAnguloInversa(Convert.ToInt32(comboBoxAnguloE1.Text)), inicializaMatrizRigidezEmCoordenadasLocais(textBoxComprimentoE1.Text), String.Empty), inicializaMatrizRotacao(Convert.ToInt32(comboBoxAnguloE1.Text)), "GLOBAL DOS ELEMENTOS 1 - ANGULO: " + comboBoxAnguloE1.Text);
            double[,] matrizGlobalDoElemento2 = multiplicacaoMatrizes(multiplicacaoMatrizes(defineMatrizRotacaoPeloAnguloInversa(Convert.ToInt32(comboBoxAnguloE2.Text)), inicializaMatrizRigidezEmCoordenadasLocais(textBoxComprimentoE2.Text), String.Empty), inicializaMatrizRotacao(Convert.ToInt32(comboBoxAnguloE2.Text)), "GLOBAL DOS ELEMENTOS 2 - ANGULO: " + comboBoxAnguloE2.Text);

            double[,] matrizRigidezGlobalNoSistemaGlobalElemento1 = espalhamentoMatrizRigidezGlobalNoSistemaGlobal(matrizGlobalDoElemento1, quantidadeGrausLiberdadeGlobal, localElemento1, "matrizRigidezGlobalNoSistemaGlobalElemento1");
            double[,] matrizRigidezGlobalNoSistemaGlobalElemento2 = espalhamentoMatrizRigidezGlobalNoSistemaGlobal(matrizGlobalDoElemento2, quantidadeGrausLiberdadeGlobal, localElemento2, "matrizRigidezGlobalNoSistemaGlobalElemento2");

            matrizGlobalDosElementos = inicializaMatrizGlobalDosElementos(matrizRigidezGlobalNoSistemaGlobalElemento1, matrizRigidezGlobalNoSistemaGlobalElemento2, quantidadeGrausLiberdadeGlobal, "MATRIZ GLOBAL DA ESTRUTURA");
            inicializaMatrizEstruturaComCondicoesDeContorno(matrizGlobalDosElementos, 1, 0, 1, 1, 2);
        }

        public void inicializaVetores()
        {
            sistemaGlobalGrausLiberdadeNo1.CopyTo(grausLiberdadeGlobal, 0);
            sistemaGlobalGrausLiberdadeNo2.CopyTo(grausLiberdadeGlobal, sistemaGlobalGrausLiberdadeNo1.Length);
            sistemaGlobalGrausLiberdadeNo3.CopyTo(grausLiberdadeGlobal, sistemaGlobalGrausLiberdadeNo1.Length + sistemaGlobalGrausLiberdadeNo2.Length);

            sistemaLocalElemento1No1.CopyTo(localElemento1, 0);
            sistemaLocalElemento1No2.CopyTo(localElemento1, sistemaLocalElemento1No1.Length);
            sistemaLocalElemento1No3.CopyTo(localElemento1, sistemaLocalElemento1No1.Length + sistemaLocalElemento1No2.Length);

            sistemaLocalElemento2No1.CopyTo(localElemento2, 0);
            sistemaLocalElemento2No2.CopyTo(localElemento2, sistemaLocalElemento2No1.Length);
            sistemaLocalElemento2No3.CopyTo(localElemento2, sistemaLocalElemento2No1.Length + sistemaLocalElemento2No2.Length);
        }

        //Espalhamento da matriz de rigidez global no sistema global
        public double[,] espalhamentoMatrizRigidezGlobalNoSistemaGlobal(double[,] matrizGlobalDoElemento, int quantidadeGrausLiberdadeGlobal, int[] localElemento, string nome)
        {
            int countI = 0;
            double[,] matrizResultante = new double[quantidadeGrausLiberdadeGlobal,quantidadeGrausLiberdadeGlobal];

            Console.WriteLine("\r\n" + nome + "\r\n");

            for (int i = 0; i < quantidadeGrausLiberdadeGlobal; i++)
            {
                int countJ = 0;

                for (int j = 0; j < quantidadeGrausLiberdadeGlobal; j++)
                {
                    //percorre elementos
                    if (!localElemento[j].Equals(0) && !localElemento[i].Equals(0))
                    {
                        //matrizLocalSistemaGlobalElemeto1[i, j] = "K" + localElemento1[i] + "," + localElemento1[j] + "(1)";
                        matrizResultante[i, j] = matrizGlobalDoElemento[countI, countJ];
                        countJ++;
                    }
                    else
                    {
                        //matrizLocalSistemaGlobalElemeto1[i, j] = "-";
                        matrizResultante[i, j] = double.NaN;
                    }
                    //Console.WriteLine("\r\nIndice [" + (i + 1) + ", " + (j + 1) + "] " + matrizResultante[i, j]);
                }
                countI++;
                if (countI.Equals(6)) countI = countI - 3;
            }

            return matrizResultante;
        }

        //MATRIZ GLOBAL DOS ELEMENTOS
        public double[,] inicializaMatrizGlobalDosElementos(double[,] matrizRigidezGlobalNoSistemaGlobalElemento1, double[,] matrizRigidezGlobalNoSistemaGlobalElemento2, int quantidadeGrausLiberdadeGlobal, string nome)
        {
            double[,] matrizResultante = new double[quantidadeGrausLiberdadeGlobal, quantidadeGrausLiberdadeGlobal];

            for (int i = 0; i < quantidadeGrausLiberdadeGlobal; i++)
            {
                for (int j = 0; j < quantidadeGrausLiberdadeGlobal; j++)
                {
                    //percorre elementos
                    if (!double.IsNaN(matrizRigidezGlobalNoSistemaGlobalElemento1[i, j]) && !double.IsNaN(matrizRigidezGlobalNoSistemaGlobalElemento2[i, j]))
                        matrizResultante[i, j] = matrizRigidezGlobalNoSistemaGlobalElemento1[i, j] + matrizRigidezGlobalNoSistemaGlobalElemento2[i, j];
                    else if (!double.IsNaN(matrizRigidezGlobalNoSistemaGlobalElemento1[i, j]) && double.IsNaN(matrizRigidezGlobalNoSistemaGlobalElemento2[i, j]))
                        matrizResultante[i, j] = matrizRigidezGlobalNoSistemaGlobalElemento1[i, j];
                    else if (double.IsNaN(matrizRigidezGlobalNoSistemaGlobalElemento1[i, j]) && !double.IsNaN(matrizRigidezGlobalNoSistemaGlobalElemento2[i, j]))
                        matrizResultante[i, j] = matrizRigidezGlobalNoSistemaGlobalElemento2[i, j];
                    else
                        matrizResultante[i, j] = 0;

                    //matrizGlobalElemetos[i, j] = matrizLocalElemeto1[i, j] + " + " + matrizLocalElemeto2[i, j];
                    //else
                    //    matrizGlobalElemetos[i, j] = "0";
                    //Console.WriteLine("Indice [" + (i + 1) + ", " + (j + 1) + "] " + matrizResultante[i, j]);
                }
            }
            return matrizResultante;
        }

        //ALGORITMO DE MULTIPLICAÇÃO DE MATRIZES
        public double[,] multiplicacaoMatrizes(double[,] matrizA, double[,] matrizB, string nome)
        {
            double valorCelula = 0;
            int quantidadeCelulas = 36;
            int celula = 0;
            int i = 0;
            int cont = 0;

            double[,] matrizResultante = new double[6,6];

            //Console.WriteLine("\r\n" + nome + "\r\n");

            for (int j = 0; j < quantidadeCelulas; j++)
            {
                valorCelula += matrizA[i, j] * matrizB[j, cont];
                //Console.WriteLine(matrizA[i, j] + " * " + matrizB[j, cont]);

                celula++;

                if (celula.Equals(6) || celula.Equals(6 * 2) || celula.Equals(6 * 3) ||
                   celula.Equals(6 * 4) || celula.Equals(6 * 5))
                {
                    matrizResultante[i, cont] = valorCelula;
                    //Console.WriteLine("Índice [" + (i + 1) + "," + (cont + 1) + "] " + valorCelula);

                    j = -1;
                    cont++;
                    valorCelula = 0;
                }
                else if (celula.Equals(6 * 6) && i < 5)
                {
                    matrizResultante[i, cont] = valorCelula;
                    //Console.WriteLine("Índice [" + (i + 1) + "," + (cont + 1) + "] " + valorCelula);

                    i++;
                    cont = 0;
                    j = -1;
                    celula = 0;
                    valorCelula = 0;
                }
                else if (i.Equals(5) && celula.Equals(6 * 6))
                {
                    matrizResultante[i, cont] = valorCelula;
                    //Console.WriteLine("Índice [" + (i + 1) + "," + (cont + 1) + "] " + valorCelula);
                    break;
                }

            }
            valorCelula = 0;

            return matrizResultante;

        }

        //MATRIZ DE RIGIDEZ EM COORDENADAS LOCAIS | double b, double h, double moduloYoung, double coeficientePoisson, double comprimentoBarra**
        public double[,] inicializaMatrizRigidezEmCoordenadasLocais(string comprimentoBarraDoElemento)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberDecimalDigits = 2;

            double b = double.Parse(textBoxB.Text, provider);
            double h = double.Parse(textBoxH.Text, provider);
            double moduloYoung = double.Parse(textBoxModuloYoung.Text, provider) * Math.Pow(10, 9);
            double coeficientePoisson = double.Parse(textBoxCoeficientePoisson.Text, provider);
            double comprimentoBarra = double.Parse(comprimentoBarraDoElemento, provider);

            //Calculando Separadamente
            //momento inércia
            double I = (b * Math.Pow(h, 3)) / 12;
            //string unidadeMedidaI = "m⁴";
            //Console.WriteLine("I: " + I);

            //modulo young
            double E = moduloYoung;
            //string unidadeMedidaE = "kN/m²";
            //Console.WriteLine("E: " + E);

            //coeficiente Poisson
            double V = coeficientePoisson;

            //modulo de elasticidade transversal
            double G = E / (2 * (1 + V));
            //string unidadeMedidaG = "kN/m²";
            //Console.WriteLine("G: " + G);

            //momento inércia torção
            double J = h * Math.Pow(b, 3) * (((double)1 / 3) - 0.21 * (b / h) * (1 - (Math.Pow(b, 4) / (12 * Math.Pow(h, 4)))));
            //string unidadeMedidaJ = "m⁴";
            //Console.WriteLine("J: " + J);
            //Console.WriteLine("1: " + h * Math.Pow(b, 3));
            //Console.WriteLine("2: " + Math.Round((double)1 / 3, 3) + "-");
            //Console.WriteLine("2-3: " + (0.21 * (b / h)) + "*");
            //Console.WriteLine("3: " + (1 - (Math.Pow(b, 4)/(12 * Math.Pow(h, 4)))));

            //Matriz de rigidez do elemento em coordenadas locais
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

        //MATRIZ DE ROTAÇÃO
        public double[,] inicializaMatrizRotacao(int x)
        {
            double[,] matrizRotacao = new double[6, 6]
                {
                    {1, 0, 0, 0, 0, 0},
                    {0, Math.Round(Math.Cos(x), 0), Math.Round(Math.Sin(x), 0), 0, 0, 0},
                    {0, Math.Round(-Math.Sin(x), 0), Math.Round(Math.Cos(x), 0), 0, 0, 0},
                    {0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 0, Math.Round(Math.Cos(x), 0), Math.Round(Math.Sin(x), 0)},
                    {0, 0, 0, 0, Math.Round(-Math.Sin(x), 0), Math.Round(Math.Cos(x), 0)}
                };

            //for (int i = 0; i < 6; i++)
            //{
            //    for (int j = 0; j < 6; j++)
            //    {
            //        Console.WriteLine("\r\nIndice [" + (i + 1) + ", " + (j + 1) + "] " + matrizRotacao[i, j]);
            //    }
            //}

            return matrizRotacao;
        }

        //MATRIZ DE ROTAÇÃO INVERSA
        public double[,] defineMatrizRotacaoPeloAnguloInversa(int x)
        {
            //Matriz de rotação para o ângulo 0
            double[,] matrizRotacaoAngulo0Inversa = new double[6, 6] 
                {  
                    {1, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0},
                    {0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 0, 1, 0},
                    {0, 0, 0, 0, 0, 1}
                };
            //Matriz de rotação para o ângulo 90
            double[,] matrizRotacaoAngulo90Inversa = new double[6, 6] 
                {  
                    {1, 0, 0, 0, 0, 0},
                    {0, 0, -1, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 0, 0, -1},
                    {0, 0, 0, 0, 1, 0}
                };

            return x == 90 ? matrizRotacaoAngulo90Inversa : matrizRotacaoAngulo0Inversa;
        }
        
        //MATRIZ DA ESTRUTURA COM AS CONDIÇÕES DE CONTORNO
        public void inicializaMatrizEstruturaComCondicoesDeContorno(double[,] matrizGlobalDosElementos, int no, int condicaoX, int condicaoY, int condicaoZ, int quantidadeNos) 
        {
            int tamanhoMatriz = 3*quantidadeNos;
            double[,] matrizResultante = new double[,]{};

            if (condicaoX.Equals(1) && condicaoY.Equals(1) && condicaoZ.Equals(1))
            {
                int diminuendo = 3;
                int tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - diminuendo;
                matrizResultante = new double[tamanhoMatrizAplicandoCondicoes, tamanhoMatrizAplicandoCondicoes];

                if (no.Equals(1))
                {
                    int comecoNo = diminuendo;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            matrizResultante[i - diminuendo, j - diminuendo] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (i - diminuendo) + ", " + (j - diminuendo) + "] " + matrizResultante[i - diminuendo, j - diminuendo]);
                        }
                    }
                }
                else if (no.Equals(2))
                {
                    int comecoNo = 0;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        if (quantidadeNos > 2)
                            if (i.Equals(diminuendo) || i.Equals(diminuendo + 1) || i.Equals(diminuendo + 2)) continue;
                        countJ = 0;
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            if(j.Equals(diminuendo) || j.Equals(diminuendo + 1) || j.Equals(diminuendo + 2)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            Console.WriteLine("Indice [" + (countI+1)+ ", " + (countJ+1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                if (no.Equals(3))
                {
                    int comecoNo = 0;

                    for (int i = comecoNo; i < tamanhoMatriz - diminuendo; i++)
                    {
                        for (int j = comecoNo; j < tamanhoMatriz - diminuendo; j++)
                        {
                            matrizResultante[i, j] = matrizGlobalDosElementos[i, j];
                            Console.WriteLine("Indice [" + (i + 1) + ", " + (j + 1) + "] " + matrizResultante[i, j]);
                        }
                    }
                }
            }
            else if (!condicaoX.Equals(1) && condicaoY.Equals(1) && condicaoZ.Equals(1))
            {
                int diminuendo = 2;
                int tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - diminuendo;
                matrizResultante = new double[tamanhoMatrizAplicandoCondicoes, tamanhoMatrizAplicandoCondicoes];

                if (no.Equals(1))
                {
                    int comecoNo = 0;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals(diminuendo - 1) || i.Equals(diminuendo)) continue;
                        countJ = 0;
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals(diminuendo - 1) || j.Equals(diminuendo)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                
            }
            //         condicaoX.Equals(1) && !condicaoY.Equals(1) && condicaoZ.Equals(1) ||
            //         condicaoX.Equals(1) && condicaoY.Equals(1) && !condicaoZ.Equals(1)) tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - 2;
            //else tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - 1;      
        }


        //EVENTOS
        private void numericUpDownQuantidadeElementos_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownQuantidadeElementos.Value.Equals(2) || numericUpDownQuantidadeElementos.Value > 2)
            { groupBoxElemento2.Visible = true; groupBoxNo3.Visible = true; }
            else{ groupBoxElemento2.Visible = false; groupBoxNo3.Visible = false; }
            if (numericUpDownQuantidadeElementos.Value.Equals(3) || numericUpDownQuantidadeElementos.Value > 3) 
            { groupBoxElemento3.Visible = true; groupBoxNo4.Visible = true; }
            else{ groupBoxElemento3.Visible = false; groupBoxNo4.Visible = false; }
            if (numericUpDownQuantidadeElementos.Value.Equals(4) || numericUpDownQuantidadeElementos.Value > 4) 
            { groupBoxElemento4.Visible = true; groupBoxNo5.Visible = true; }
            else{ groupBoxElemento4.Visible = false; groupBoxNo5.Visible = false; }
            if (numericUpDownQuantidadeElementos.Value.Equals(5) || numericUpDownQuantidadeElementos.Value > 5) 
            { groupBoxElemento5.Visible = true; groupBoxNo6.Visible = true; }
            else{ groupBoxElemento5.Visible = false; groupBoxNo6.Visible = false; }
            if (numericUpDownQuantidadeElementos.Value.Equals(6) || numericUpDownQuantidadeElementos.Value > 6) 
                groupBoxElemento6.Visible = true;
            else groupBoxElemento6.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(7) || numericUpDownQuantidadeElementos.Value > 7) 
                groupBoxElemento7.Visible = true;
            else groupBoxElemento7.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(8) || numericUpDownQuantidadeElementos.Value > 8) 
                groupBoxElemento8.Visible = true;
            else groupBoxElemento8.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(9) || numericUpDownQuantidadeElementos.Value > 9) 
                groupBoxElemento9.Visible = true;
            else groupBoxElemento9.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(10)) groupBoxElemento10.Visible = true;
            else groupBoxElemento10.Visible = false;

            textBoxQuantidadeNos.Text = ((int)numericUpDownQuantidadeElementos.Value + 1).ToString() ;
        }

        public void selecaoUnicaNoCheckedlistbox(CheckedListBox check)
        {
            if (check.Equals(0)) check.SetItemChecked(1, false);
            else if (check.SelectedIndex.Equals(1)) check.SetItemChecked(0, false);
        }

        private void checkedListBoxNo1X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo1X);
        }

        private void checkedListBoxNo1Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo1Y);
        }

        private void checkedListBoxNo1Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo1Z);
        }

        private void checkedListBoxNo2X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo2X);
        }

        private void checkedListBoxNo2Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo2Y);
        }

        private void checkedListBoxNo2Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo2Z);
        }

        private void checkedListBoxNo3X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo3X);
        }

        private void checkedListBoxNo3Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo3Y);
        }

        private void checkedListBoxNo3Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo3Z);
        }

        private void checkedListBoxNo4X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo4X);
        }

        private void checkedListBoxNo4Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo4Y);
        }

        private void checkedListBoxNo4Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo4Z);
        }

        private void checkedListBoxNo5X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo5X);
        }

        private void checkedListBoxNo5Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo5Y);
        }

        private void checkedListBoxNo5Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo5Z);
        }

        private void checkedListBoxNo6X_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo6X);
        }

        private void checkedListBoxNo6Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo6Y);
        }

        private void checkedListBoxNo6Z_SelectedIndexChanged(object sender, EventArgs e)
        {
            selecaoUnicaNoCheckedlistbox(checkedListBoxNo5Z);
        }

        private void buttonCalculaMatrizRigidezElementoEmCoordenadasLocais_Click(object sender, EventArgs e)
        {
            inicializaMatrizEstruturaComCondicoesDeContorno(matrizGlobalDosElementos, 1, checkedListBoxNo1X.SelectedIndex, checkedListBoxNo1Y.SelectedIndex, checkedListBoxNo1Z.SelectedIndex, Convert.ToInt32(textBoxQuantidadeNos.Text));
        }

        
    }
}
