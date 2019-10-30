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
        public static int[] vetorGrausLiberdadeGlobal         = new int[quantidadeGrausLiberdadeGlobal];

        //Vetores totais do elemento 1
        public static int quantidadeLocalElemento1 = sistemaLocalElemento1No1.Length + sistemaLocalElemento1No2.Length + sistemaLocalElemento2No3.Length;
        public static int[] localElemento1         = new int[quantidadeLocalElemento1];
        //Cria matriz local do elemento 2
        double[,] matrizLocalSistemaGlobalElemeto1 = new double[vetorGrausLiberdadeGlobal.Length, vetorGrausLiberdadeGlobal.Length];

        //Vetores totais do elemento 2
        public static int quantidadeLocalElemento2 = sistemaLocalElemento2No1.Length + sistemaLocalElemento2No2.Length + sistemaLocalElemento2No3.Length;
        public static int[] localElemento2         = new int[quantidadeLocalElemento2];
        //Cria matriz local do elemento 2
        double[,] matrizLocalSistemaGlobalElemeto2 = new double[vetorGrausLiberdadeGlobal.Length, vetorGrausLiberdadeGlobal.Length];

        //Matriz global dos elementos
        string[,] matrizGlobalEstrutura = new string[vetorGrausLiberdadeGlobal.Length, vetorGrausLiberdadeGlobal.Length];

        //Associando nó global a nó local
        //elemento 1
        //public static int[] noLocalElemento1  = { 1, 2 };
        public static double[] nosGlobaisDosElementos;
        //elemento 2
        //public static int[] noLocalElemento2  = { 1, 2 };
        //public static double[] noGlobalElemento2;
  
        //Elementos em matrizes globais
        static List<double[,]> elementosNaMatrizGlobal;

        //Sistemas locais
        static List<int[]> elementosLocais;
        //Associação com elementos e nós
        static int[] grausLiberdadeLocal = new int[6]{1, 2, 3, 4, 5, 6};

        //Graus de liberdade
        static int grausLiberdadeGlobal;
        //quantidadeElementos
        static int quantidadeElementos;
        
        //Vetor das cargas externas
        public static double[] vetorCargasExternas;

        public Form1()
        {
            InitializeComponent();

            grausLiberdadeGlobal = Convert.ToInt32(textBoxQuantidadeNos.Text) * 3;
            quantidadeElementos = Convert.ToInt32(numericUpDownQuantidadeElementos.Value);

            inicializaVetores(grausLiberdadeGlobal, quantidadeElementos);
            inicializaMatrizesGlobais();

            double[,] matrizRigidezGlobalNoSistemaGlobalElemento1 = espalhamentoMatrizRigidezGlobalNoSistemaGlobal(elementosNaMatrizGlobal[0], quantidadeGrausLiberdadeGlobal, elementosLocais[0], "matrizRigidezGlobalNoSistemaGlobalElemento1");
            double[,] matrizRigidezGlobalNoSistemaGlobalElemento2 = espalhamentoMatrizRigidezGlobalNoSistemaGlobal(elementosNaMatrizGlobal[1], quantidadeGrausLiberdadeGlobal, elementosLocais[1], "matrizRigidezGlobalNoSistemaGlobalElemento2");

            //matrizGlobalDosElementos = inicializaMatrizGlobalDosElementos(matrizRigidezGlobalNoSistemaGlobalElemento1, matrizRigidezGlobalNoSistemaGlobalElemento2, quantidadeGrausLiberdadeGlobal, "MATRIZ GLOBAL DA ESTRUTURA");
            //double[,] matrizEstruturaComCondicoesDeContorno = inicializaMatrizEstruturaComCondicoesDeContorno(matrizGlobalDosElementos, 2, 1, 1, 1, 3);
            //double[,] matrizInversaDaEstrutura = invert(matrizEstruturaComCondicoesDeContorno); 
            //double[] vetorDeslocamentoERotacaoGlobal = multiplicacaoMatrizComVetor(matrizInversaDaEstrutura, vetorCargasExternas, "VETOR DE DESLOCAMENTO E ROTAÇÃO GLOBAL");
        }

        public void inicializaVetores(int gl, int qtdEle)
        {
            elementosLocais = new List<int[]>();

            for (int i = 0; i < qtdEle; i++)
            {
                int[] teste = new int[gl];
                for (int j = i; j < gl; j++)
                {
                    if ((i * j) % 3 == 0)
                    {
                        grausLiberdadeLocal.CopyTo(teste, i * j);
                        break;
                    }
                    else teste[j] = 0;
                }
                elementosLocais.Add(teste);
            }
        }
        
        public void inicializaMatrizesGlobais()
        {
            int quantidadeElementos = Convert.ToInt32(numericUpDownQuantidadeElementos.Value);

            elementosNaMatrizGlobal = new List<double[,]>();

            for (int i = 1; i <= quantidadeElementos; i++)
            {
                Control[] combobox = this.Controls.Find("comboBoxAnguloE" + i.ToString(), true);
                ComboBox angulo = combobox[0] as ComboBox;
                Control[] textbox = this.Controls.Find("textBoxComprimentoE" + i.ToString(), true);
                TextBox comprimento = textbox[0] as TextBox;

                elementosNaMatrizGlobal.Add(multiplicacaoMatrizes(multiplicacaoMatrizes(defineMatrizRotacaoPeloAnguloInversa(Convert.ToInt32(angulo.Text)), calculaMatrizRigidezEmCoordenadasLocais(comprimento.Text), String.Empty), inicializaMatrizRotacao(Convert.ToInt32(angulo.Text)), "GLOBAL DOS ELEMENTOS " + i + " - ANGULO: " + angulo.Text));
            }
        }

        //Espalhamento da matriz de rigidez global no sistema global
        public double[,] espalhamentoMatrizRigidezGlobalNoSistemaGlobal(double[,] matrizGlobalDoElemento, int quantidadeGrausLiberdadeGlobal, int[] localElemento, string nome)
        {
            int countI = 0;
            double[,] matrizResultante = new double[quantidadeGrausLiberdadeGlobal,quantidadeGrausLiberdadeGlobal];

            //Console.WriteLine("\r\n" + nome + "\r\n");

            for (int i = 0; i < quantidadeGrausLiberdadeGlobal; i++)
            {
                int countJ = 0;

                for (int j = 0; j < quantidadeGrausLiberdadeGlobal; j++)
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
                    Console.WriteLine("\r\nIndice [" + (i + 1) + ", " + (j + 1) + "] " + matrizResultante[i, j]);
                }
                countI++;
                if (countI.Equals(6)) countI = countI - 3;
            }

            return matrizResultante;
        }

        //MATRIZ GLOBAL DOS ELEMENTOS
        public double[,] calculaMatrizGlobalDosElementos(double[,] matrizRigidezGlobalNoSistemaGlobalElemento1, double[,] matrizRigidezGlobalNoSistemaGlobalElemento2, int quantidadeGrausLiberdadeGlobal, string nome)
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
            int quantidadeCelulas = (matrizA.Length + matrizB.Length)/2;
            int tamDimensao = (matrizA.GetLength(0) + matrizA.GetLength(1)) / 2;
            int celula = 0;
            int i = 0;
            int cont = 0;            

            double[,] matrizResultante = new double[tamDimensao, tamDimensao];

            //Console.WriteLine("\r\n" + nome + "\r\n");

            for (int j = 0; j < quantidadeCelulas; j++)
            {
                valorCelula += matrizA[i, j] * matrizB[j, cont];
                //Console.WriteLine(matrizA[i, j] + " * " + matrizB[j, cont]);

                celula++;

                if (celula.Equals(tamDimensao) || celula.Equals(tamDimensao * 2) || celula.Equals(tamDimensao * 3) ||
                   celula.Equals(tamDimensao * 4) || celula.Equals(tamDimensao * 5))
                {
                    matrizResultante[i, cont] = valorCelula;
                    //Console.WriteLine("Índice [" + (i + 1) + "," + (cont + 1) + "] " + valorCelula);

                    j = -1;
                    cont++;
                    valorCelula = 0;
                }
                else if (celula.Equals(tamDimensao * 6) && i < tamDimensao-1)
                {
                    matrizResultante[i, cont] = valorCelula;
                    //Console.WriteLine("Índice [" + (i + 1) + "," + (cont + 1) + "] " + valorCelula);

                    i++;
                    cont = 0;
                    j = -1;
                    celula = 0;
                    valorCelula = 0;
                }
                else if (i.Equals(tamDimensao - 1) && celula.Equals(tamDimensao * 6))
                {
                    matrizResultante[i, cont] = valorCelula;
                    //Console.WriteLine("Índice [" + (i + 1) + "," + (cont + 1) + "] " + valorCelula);
                    break;
                }

            }
            valorCelula = 0;

            return matrizResultante;

        }

        //ALGORITMO DE MULTIPLICAÇÃO DE MATRIZES
        public double[] multiplicacaoMatrizComVetor(double[,] matriz, double[] vetor, string nome)
        {
            double valorCelula = 0;
            int quantidadeCelulas = matriz.Length;
            int tamDimensao = matriz.GetLength(1);

            double[] vetorResultante = new double[tamDimensao];

            Console.WriteLine("\r\n" + nome + "\r\n");

            for (int i = 0; i < tamDimensao; i++)
            {
                for (int j = 0; j < tamDimensao; j++)
                {
                    valorCelula += matriz[i, j] * vetor[j];
                    //Console.WriteLine(matriz[i, j] + " * " + vetor[j]);
                }
                Console.WriteLine("Índice [" + (i + 1) + "] " + valorCelula);
                valorCelula = 0;
            }

            return vetorResultante;

        }

        //MATRIZ DE RIGIDEZ EM COORDENADAS LOCAIS | double b, double h, double moduloYoung, double coeficientePoisson, double comprimentoBarra**
        public double[,] calculaMatrizRigidezEmCoordenadasLocais(string comprimentoBarraDoElemento)
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
        public double[,] defineMatrizEstruturaComCondicoesDeContorno(double[,] matrizGlobalDosElementos, int no, int condicaoX, int condicaoY, int condicaoZ, int quantidadeNos) 
        {
            int tamanhoMatriz = 3*quantidadeNos;
            double[,] matrizResultante = new double[,] { };

            //Console.WriteLine("MATRIZ DA ESTRUTURA COM AS CONDIÇÕES DE CONTORNO");

            //RESTRINGINDO X Y Z
            if (condicaoX.Equals(1) && condicaoY.Equals(1) && condicaoZ.Equals(1))
            {
                int diminuendo = 3;
                int comecoNo = 0;
                int tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - diminuendo;
                matrizResultante = new double[tamanhoMatrizAplicandoCondicoes, tamanhoMatrizAplicandoCondicoes];

                //NO 1 - XYZ
                if (no.Equals(1))
                {
                    comecoNo = diminuendo;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            matrizResultante[i - diminuendo, j - diminuendo] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (i - diminuendo) + ", " + (j - diminuendo) + "] " + matrizResultante[i - diminuendo, j - diminuendo]);
                        }
                    }
                }
                //NO 2 - XYZ
                else if (no.Equals(2))
                {
                    int countI = 0;
                    int countJ = 0;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        if (quantidadeNos > 2)
                            if (i.Equals(diminuendo) || i.Equals(diminuendo + 1) || i.Equals(diminuendo + 2)) continue;
                        countJ = 0;
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals(diminuendo) || j.Equals(diminuendo + 1) || j.Equals(diminuendo + 2)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI+1)+ ", " + (countJ+1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 3 - XYZ
                if (no.Equals(3))
                {
                    for (int i = comecoNo; i < tamanhoMatriz - diminuendo; i++)
                    {
                        for (int j = comecoNo; j < tamanhoMatriz - diminuendo; j++)
                        {
                            matrizResultante[i, j] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (i + 1) + ", " + (j + 1) + "] " + matrizResultante[i, j]);
                        }
                    }
                }
            }
            //RESTRINGINDO Y Z
            else if (!condicaoX.Equals(1) && condicaoY.Equals(1) && condicaoZ.Equals(1))
            {
                int diminuendo = 2;
                int tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - diminuendo;
                matrizResultante = new double[tamanhoMatrizAplicandoCondicoes, tamanhoMatrizAplicandoCondicoes];

                //NO 1 - YZ
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
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }

                }
                //NO 2 - YZ
                if (no.Equals(2))
                {
                    int comecoNo = 3;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo + diminuendo) - 1) || i.Equals(comecoNo + diminuendo)) continue;
                        countJ = 0;
                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo + diminuendo) - 1) || j.Equals(comecoNo + diminuendo)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 3 - YZ
                if (no.Equals(3))
                {
                    int comecoNo = 6;

                    for (int i = 0; i < tamanhoMatriz - diminuendo; i++)
                    {
                        if (i.Equals((comecoNo + diminuendo) - 1) || i.Equals(comecoNo + diminuendo)) continue;

                        for (int j = 0; j < tamanhoMatriz - diminuendo; j++)
                        {
                            if (j.Equals((comecoNo + diminuendo) - 1) || j.Equals(comecoNo + diminuendo)) continue;

                            matrizResultante[i, j] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (i + 1) + ", " + (j + 1) + "] " + matrizResultante[i, j]);
                        }
                    }
                }
            }
            //RESTRINGINDO X Z
            else if (condicaoX.Equals(1) && !condicaoY.Equals(1) && condicaoZ.Equals(1))
            {
                int diminuendo = 2;
                int tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - diminuendo;
                matrizResultante = new double[tamanhoMatrizAplicandoCondicoes, tamanhoMatrizAplicandoCondicoes];

                //NO 1 - XZ
                if (no.Equals(1))
                {
                    int comecoNo = 0;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals(diminuendo - 2) || i.Equals(diminuendo)) continue;
                        countJ = 0;
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals(diminuendo - 2) || j.Equals(diminuendo)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 2 - XZ
                if (no.Equals(2))
                {
                    int comecoNo = 3;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo + diminuendo) - 2) || i.Equals(comecoNo + diminuendo)) continue;
                        countJ = 0;
                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo + diminuendo) - 2) || j.Equals(comecoNo + diminuendo)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 3 - XZ
                if (no.Equals(3))
                {
                    int comecoNo = 6;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo + diminuendo) - 2) || i.Equals(comecoNo + diminuendo)) continue;
                        countJ = 0;

                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo + diminuendo) - 2) || j.Equals(comecoNo + diminuendo)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
            }
            //RESTRINGINDO X Y
            else if (condicaoX.Equals(1) && condicaoY.Equals(1) && !condicaoZ.Equals(1))
            {
                int diminuendo = 2;
                int tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - diminuendo;
                matrizResultante = new double[tamanhoMatrizAplicandoCondicoes, tamanhoMatrizAplicandoCondicoes];

                //NO 1 - XY
                if (no.Equals(1))
                {
                    int comecoNo = 0;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals(diminuendo - 2) || i.Equals(diminuendo - 1)) continue;
                        countJ = 0;
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals(diminuendo - 2) || j.Equals(diminuendo - 1)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 2 - XY
                if (no.Equals(2))
                {
                    int comecoNo = 3;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo + diminuendo) - 2) || i.Equals((comecoNo + diminuendo) - 1)) continue;
                        countJ = 0;
                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo + diminuendo) - 2) || j.Equals((comecoNo + diminuendo) - 1)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 3 - XY
                if (no.Equals(3))
                {
                    int comecoNo = 6;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo + diminuendo) - 2) || i.Equals((comecoNo + diminuendo) - 1)) continue;
                        countJ = 0;

                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo + diminuendo) - 2) || j.Equals((comecoNo + diminuendo) - 1)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
            }
            //RESTRIGINDO X
            else if (condicaoX.Equals(1) && !condicaoY.Equals(1) && !condicaoZ.Equals(1))
            {
                int diminuendo = 1;
                int tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - diminuendo;
                matrizResultante = new double[tamanhoMatrizAplicandoCondicoes, tamanhoMatrizAplicandoCondicoes];

                //NO 1 - X
                if (no.Equals(1))
                {
                    int comecoNo = 0;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals(comecoNo)) continue;
                        countJ = 0;
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals(comecoNo)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 2 - X
                if (no.Equals(2))
                {
                    int comecoNo = 3;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo))) continue;
                        countJ = 0;
                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo))) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 3 - X
                if (no.Equals(3))
                {
                    int comecoNo = 6;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo))) continue;
                        countJ = 0;

                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo))) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
            }
            //RESTRIGINDO Y
            else if (!condicaoX.Equals(1) && condicaoY.Equals(1) && !condicaoZ.Equals(1))
            {
                int diminuendo = 1;
                int tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - diminuendo;
                matrizResultante = new double[tamanhoMatrizAplicandoCondicoes, tamanhoMatrizAplicandoCondicoes];

                //NO 1 - Y
                if (no.Equals(1))
                {
                    int comecoNo = 0;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals(comecoNo + 1)) continue;
                        countJ = 0;
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals(comecoNo + 1)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 2 - Y
                if (no.Equals(2))
                {
                    int comecoNo = 3;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo + 1))) continue;
                        countJ = 0;
                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo + 1))) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 3 - Y
                if (no.Equals(3))
                {
                    int comecoNo = 6;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo + 1))) continue;
                        countJ = 0;

                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo + 1))) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
            }
            //RESTRIGINDO Z
            else if (!condicaoX.Equals(1) && !condicaoY.Equals(1) && condicaoZ.Equals(1))
            {
                int diminuendo = 1;
                int tamanhoMatrizAplicandoCondicoes = tamanhoMatriz - diminuendo;
                matrizResultante = new double[tamanhoMatrizAplicandoCondicoes, tamanhoMatrizAplicandoCondicoes];

                //NO 1 - Z
                if (no.Equals(1))
                {
                    int comecoNo = 0;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = comecoNo; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals(comecoNo + 2)) continue;
                        countJ = 0;
                        for (int j = comecoNo; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals(comecoNo + 2)) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 2 - Z
                if (no.Equals(2))
                {
                    int comecoNo = 3;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo + 2))) continue;
                        countJ = 0;
                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo + 2))) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
                //NO 3 - Z
                if (no.Equals(3))
                {
                    int comecoNo = 6;
                    int countI = 0;
                    int countJ = 0;

                    for (int i = 0; i < tamanhoMatriz; i++)
                    {
                        if (i.Equals((comecoNo + 2))) continue;
                        countJ = 0;

                        for (int j = 0; j < tamanhoMatriz; j++)
                        {
                            if (j.Equals((comecoNo + 2))) continue;

                            matrizResultante[countI, countJ] = matrizGlobalDosElementos[i, j];
                            //Console.WriteLine("Indice [" + (countI + 1) + ", " + (countJ + 1) + "] " + matrizResultante[countI, countJ]);
                            countJ++;
                        }
                        countI++;
                    }
                }
            }
            else matrizResultante = matrizGlobalDosElementos;
            //inicializaVetorCargasExternas(matrizResultante.GetLength(1));

            return matrizResultante;
        }

        //INICIALIZA VETOR DE CARGAS EXTERNAS
        public void inicializaVetorCargasExternas(int tamanhoMatriz, int noLiga1, int noLiga2)
        {
            int comecoNo1 = 0;
            int comecoNo2 = 3;
            int comecoNo3 = 6;
            int comecoNo4 = 9;
            int comecoNo5 = 12;
            int comecoNo6 = 15;

            vetorCargasExternas = new double[tamanhoMatriz];
            for (int i = 0; i < vetorCargasExternas.Length; i++)
                vetorCargasExternas[i] = 0;

            if (textBoxForcaNo1.Enabled && !String.IsNullOrEmpty(textBoxForcaNo1.Text.Trim()))
            {
                vetorCargasExternas[comecoNo1] = double.Parse(textBoxForcaNo1.Text);

            }
            if (textBoxForcaNo2.Enabled && !String.IsNullOrEmpty(textBoxForcaNo2.Text.Trim()))
                vetorCargasExternas[comecoNo2] = double.Parse(textBoxForcaNo2.Text);
            if (textBoxForcaNo3.Enabled && !String.IsNullOrEmpty(textBoxForcaNo3.Text.Trim()))
                vetorCargasExternas[comecoNo3] = double.Parse(textBoxForcaNo3.Text);
            if (textBoxForcaNo4.Enabled && !String.IsNullOrEmpty(textBoxForcaNo4.Text.Trim()))
                vetorCargasExternas[comecoNo4] = double.Parse(textBoxForcaNo4.Text);
            if (textBoxForcaNo5.Enabled && !String.IsNullOrEmpty(textBoxForcaNo5.Text.Trim()))
                vetorCargasExternas[comecoNo5] = double.Parse(textBoxForcaNo5.Text);
            if (textBoxForcaNo6.Enabled && String.IsNullOrEmpty(textBoxForcaNo6.Text.Trim()))
                vetorCargasExternas[comecoNo6] = double.Parse(textBoxForcaNo6.Text);

            for (int i2 = 0; i2 < vetorCargasExternas.Length; i2++)
                Console.WriteLine("\r\nIndice [" + (i2 + 1) + "]" + vetorCargasExternas[i2]);
        }

        //MATRIZ INVERSA
        public static double[,] invert(double[,] matriz)
        {
            Console.WriteLine("matrizEstruturaComCondicoesDeContornoInversa");

            double[,] originalMatrix = matriz;
            double[,] cofator = new double[matriz.GetLength(0), matriz.GetLength(1)];
            double[,] adjunta = new double[matriz.GetLength(1), matriz.GetLength(0)];
            double[,] resultado = new double[matriz.GetLength(1), matriz.GetLength(0)];

            for (int i = 0; i <= matriz.GetLength(0); i++)
            {
                for (int j = 0; j <= matriz.GetLength(1); j++)
                {
                    matriz = TrimArray(i, j, originalMatrix);
                    cofator[i, j] = Math.Round(Math.Pow(-1, i + j) * gerarDeterminante(matriz));
                }
            }
            adjunta = GerarTransposta(cofator);
            resultado = multiplicarPorNumeroQualquer(adjunta, 1 / gerarDeterminante(originalMatrix));

            for (int i = 0; i <= matriz.GetLength(0); i++)
            {
                Console.Write("[");
                for (int j = 0; j <= matriz.GetLength(1); j++)
                {
                    Console.Write(resultado[i, j] + " ,");
                }
                Console.WriteLine("]");
            }

            return resultado;
        }
        public static double[,] TrimArray(int rowToRemove, int columnToRemove, double[,] originalArray)
        {
            double[,] result = new double[originalArray.GetLength(0) - 1, originalArray.GetLength(1) - 1];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                    continue;

                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    if (k == columnToRemove)
                        continue;

                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }

            return result;
        }
        public static double[,] GerarTransposta(double[,] matriz)
        {
            double[,] matrizTransposta = new double[matriz.GetLength(1), matriz.GetLength(0)];

            for (int x = 0; x < matrizTransposta.GetLength(0); x++)
            {
                for (int y = 0; y < matrizTransposta.GetLength(1); y++)
                {
                    matrizTransposta[x, y] += matriz[y, x];
                }
            }
            return matrizTransposta;
        }
        public static double[,] multiplicarPorNumeroQualquer(double[,] matriz, double numeroQualquer)
        {
            double[,] matrizResultado = new double[matriz.GetLength(0), matriz.GetLength(1)];
            int linhas = matriz.GetLength(0);
            int colunas = matriz.GetLength(1);


            for (int x = 0; x < linhas; x++)
            {
                for (int y = 0; y < colunas; y++)
                {
                    matrizResultado[x, y] = matriz[x, y] * numeroQualquer;
                }
            }

            return matrizResultado;
        }
        public static double gerarDeterminante(double[,] matriz)
        {
            double[,] parametro = matriz;
            double resultado = 0;


            if (matriz.GetLength(0) == 1)
            {
                return matriz[0, 0];
            }

            for (int i = 0; i < parametro.GetLength(1); i++)
            {
                matriz = TrimArray(0, i, parametro);
                resultado += parametro[0, i] * (double)Math.Pow(-1, 0 + i) * gerarDeterminante(matriz);
            }

            return resultado;
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

        private void buttonCalculaMatrizRigidezElementoEmCoordenadasLocais_Click(object sender, EventArgs e)
        {
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
    }
}
