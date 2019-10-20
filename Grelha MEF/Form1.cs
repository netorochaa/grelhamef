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

        //Matriz de rigidez local **TESTE** 
        //elemento 1
        public static double[,] rigidezteste1 = new double[6, 6] 
            {  
                {1,	2,	3,	4,	5,	6},
                {7,	8,	9,	10,	11,	12},
                {13, 14, 15, 16, 17, 18},
                {19, 20, 21, 22, 23, 24},
                {25, 26, 27, 28, 29, 30},
                {31, 32, 33, 34, 35, 36}
            };
        //elemento 2
        public static double[,] rigidezteste2 = new double[6, 6] 
            {  
                {36, 35, 34, 33, 32, 31},
                {30, 29, 28, 27, 26, 25},
                {24, 23, 22, 21, 20, 19},
                {18, 17, 16, 15, 14, 13},
                {12, 11, 10, 9, 8, 7},
                {6, 5, 4, 3, 2, 1}
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
        string[,] matrizLocalSistemaGlobalElemeto1 = new string[grausLiberdadeGlobal.Length, grausLiberdadeGlobal.Length];

        //Vetores totais do elemento 2
        public static int quantidadeLocalElemento2 = sistemaLocalElemento2No1.Length + sistemaLocalElemento2No2.Length + sistemaLocalElemento2No3.Length;
        public static int[] localElemento2         = new int[quantidadeLocalElemento2];
        //Cria matriz local do elemento 2
        string[,] matrizLocalSistemaGlobalElemeto2 = new string[grausLiberdadeGlobal.Length, grausLiberdadeGlobal.Length];

        //Matriz global dos elementos
        string[,] matrizGlobalEstrutura = new string[grausLiberdadeGlobal.Length, grausLiberdadeGlobal.Length];

        //Associando nó global a nó local
        //elemento 1
        public static int[] noLocalElemento1  = { 1, 2 };
        public static int[] noGlobalElemento1 = { 1, 3 };
        //elemento 2
        public static int[] noLocalElemento2  = { 1, 2 };
        public static int[] noGlobalElemento2 = { 1, 2 };    
    
        //Matriz de rotação para o ângulo 0
        public static double[,] matrizRotacaoAngulo0 = new double[6, 6] 
            {  
                {1, 0, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 0},
                {0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0},
                {0, 0, 0, 0, 1, 0},
                {0, 0, 0, 0, 0, 1}
            };
        //Matriz de rotação para o ângulo 90
        public static double[,] matrizRotacaoAngulo90 = new double[6, 6] 
            {  
                {1, 0, 0, 0, 0, 0},
                {0, 0, -1, 0, 0, 0},
                {0, 1, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0},
                {0, 0, 0, 0, 0, -1},
                {0, 0, 0, 0, 1, 0}
            };

        public Form1()
        {
            InitializeComponent();

            groupBoxElemento2.Visible = false;
            groupBoxElemento3.Visible = false;
            groupBoxElemento4.Visible = false;
            groupBoxElemento5.Visible = false;
            groupBoxElemento6.Visible = false;
            groupBoxElemento7.Visible = false;
            groupBoxElemento8.Visible = false;
            groupBoxElemento9.Visible = false;
            groupBoxElemento10.Visible = false;

            inicializaVetores();
            inicializaMatrizElemento1();
            inicializaMatrizElemento2();
            inicializaMatrizGlobalDosElementos();
            //calculaMatrizRigidezAplicandoMatrizRotacao(rigidezteste1, rigidezteste2);
            inicializaMatrizRotacao((Convert.ToInt32(comboBoxAnguloE1.Text)));
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

        public void inicializaMatrizElemento1()
        {
            for (int i = 0; i < grausLiberdadeGlobal.Length; i++)
            {
                for (int j = 0; j < grausLiberdadeGlobal.Length; j++)
                {
                    //percorre elementos
                    if (!localElemento1[j].Equals(0) && !localElemento1[i].Equals(0))
                        matrizLocalSistemaGlobalElemeto1[i, j] = "K" + localElemento1[i] + "," + localElemento1[j] + "(1)";
                    else
                        matrizLocalSistemaGlobalElemeto1[i, j] = "-";
                    //Console.WriteLine("\r\nIndice [" + (i + 1) + ", " + (j + 1) + "] " + matrizLocalElemeto1[i, j]);
                }
            }
        }

        public void inicializaMatrizElemento2()
        {
            for (int i = 0; i < grausLiberdadeGlobal.Length; i++)
            {
                for (int j = 0; j < grausLiberdadeGlobal.Length; j++)
                {
                    //percorre elementos
                    if (!localElemento2[j].Equals(0) && !localElemento2[i].Equals(0))
                        matrizLocalSistemaGlobalElemeto2[i, j] = "K" + localElemento2[i] + "," + localElemento2[j] + "(2)";
                    else
                        matrizLocalSistemaGlobalElemeto2[i, j] = "-";
                    //Console.WriteLine("\r\nIndice [" + (i + 1) + ", " + (j + 1) + "] " + matrizLocalElemeto2[i, j]);
                }
            }
        }

        public void inicializaMatrizGlobalDosElementos()
        {
            for (int i = 0; i < grausLiberdadeGlobal.Length; i++)
            {
                for (int j = 0; j < grausLiberdadeGlobal.Length; j++)
                {                    
                    //percorre elementos
                    if (!matrizLocalSistemaGlobalElemeto1[i, j].Equals("-") && !matrizLocalSistemaGlobalElemeto2[i, j].Equals("-"))
                        matrizGlobalEstrutura[i, j] = matrizLocalSistemaGlobalElemeto1[i, j] + "+" + matrizLocalSistemaGlobalElemeto2[i, j];
                    else if (!matrizLocalSistemaGlobalElemeto1[i, j].Equals("-") && matrizLocalSistemaGlobalElemeto2[i, j].Equals("-"))
                        matrizGlobalEstrutura[i, j] = matrizLocalSistemaGlobalElemeto1[i, j];
                    else if (matrizLocalSistemaGlobalElemeto1[i, j].Equals("-") && !matrizLocalSistemaGlobalElemeto2[i, j].Equals("-"))
                        matrizGlobalEstrutura[i, j] = matrizLocalSistemaGlobalElemeto2[i, j];
                    else
                        matrizGlobalEstrutura[i, j] = "0";

                    //matrizGlobalElemetos[i, j] = matrizLocalElemeto1[i, j] + " + " + matrizLocalElemeto2[i, j];
                    //else
                    //    matrizGlobalElemetos[i, j] = "0";
                    //Console.WriteLine("Indice [" + (i + 1) + ", " + (j + 1) + "] " + matrizGlobalEstrutura[i, j]);
                }
            }
        }

        //**INSERIR BOTÃO PARA INICIALIZAR ESSA MATRIZ**
        public double[,] inicializaMatrizRigidezEmCoordenadasLocais(double b, double h, double moduloYoung, double coeficientePoisson, double comprimentoBarra)
        {
            //Calculando Separadamente
            //momento inércia
            double I = (b * Math.Pow(h, 3)) / 12;
            //string unidadeMedidaI = "m⁴";
            Console.WriteLine("I: " + I);

            //modulo young
            double E = moduloYoung;
            //string unidadeMedidaE = "kN/m²";
            Console.WriteLine("E: " + E);

            //coeficiente Poisson
            double V = coeficientePoisson;

            //modulo de elasticidade transversal
            double G = E / (2 * (1 + V));
            //string unidadeMedidaG = "kN/m²";
            Console.WriteLine("G: " + G);            

            //momento inércia torção
            double J = h * Math.Pow(b, 3) * (((double)1 / 3) - 0.21 * (b / h) * (1 - (Math.Pow(b, 4) / (12 * Math.Pow(h, 4)))));
            //string unidadeMedidaJ = "m⁴";
            Console.WriteLine("J: " + J);
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

        //**INSERIR BOTÃO PARA INICIALIZAR ESSA MATRIZ**
        public void inicializaMatrizRotacao(int x)
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
            
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Console.WriteLine("\r\nIndice [" + (i + 1) + ", " + (j + 1) + "] " + matrizRotacao[i, j]);
                }
            }
        }

        //Substituindo na matriz de rigidez do elemento e aplicando a matriz de rotação
        public void calculaMatrizRigidezAplicandoMatrizRotacao(double[,] matrizRigidezElementoEmCoordenadasLocais, double[,] matrizRotacao)
        {
            double valorCelula = 0;
            int quantidadeCelulas = matrizRigidezElementoEmCoordenadasLocais.Length;
            int celula = 0;
            int i = 0;
            int cont = 0;

            for (int j = 0; j < 36; j++)
            {
                valorCelula += matrizRigidezElementoEmCoordenadasLocais[i, j] * matrizRotacao[j, cont];
                Console.WriteLine(matrizRigidezElementoEmCoordenadasLocais[i, j] + " * " + matrizRotacao[j, cont]);

                celula++;                

                if(celula.Equals(6) || celula.Equals(6*2) || celula.Equals(6*3) ||
                   celula.Equals(6*4) || celula.Equals(6*5))
                {
                    j = -1;
                    cont++;
                    Console.WriteLine(valorCelula);
                    valorCelula = 0;
                }
                else if (celula.Equals(6 * 6) && i < 5)
                {
                    i++;
                    cont = 0;
                    j = -1;
                    celula = 0;
                    Console.WriteLine(valorCelula);
                    valorCelula = 0;
                }
                else if (i.Equals(5) && celula.Equals(6 * 6))
                {
                    break;
                }

            }
            Console.WriteLine(celula);
            valorCelula = 0;   
        }

        private void buttonCalculaMatrizRigidezElementoEmCoordenadasLocais_Click(object sender, EventArgs e)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberDecimalDigits = 2;
            //provider.NumberGroupSizes = new int[] { 2 };
            //provider.NumberGroupSizes = new int[] { 2 };

            double b = double.Parse(textBoxB.Text, provider);
            double h = double.Parse(textBoxH.Text, provider);
            double moduloYoung = double.Parse(textBoxModuloYoung.Text, provider) * Math.Pow(10, 9);
            double coeficientePoisson = double.Parse(textBoxCoeficientePoisson.Text, provider);
            double l1 = double.Parse(textBoxComprimentoE1.Text, provider);

            Console.WriteLine("b: " + b);
            Console.WriteLine("h: " + h);
            Console.WriteLine("moduloYoung: " + moduloYoung);
            Console.WriteLine("coeficientePoisson: " + coeficientePoisson);
            Console.WriteLine("L1: " + l1);

            double[,] teste1 = inicializaMatrizRigidezEmCoordenadasLocais(b, h, moduloYoung, coeficientePoisson, l1);
            
            //for (int i = 0; i < 6; i++)
            //{
            //    for (int j = 0; j < 6; j++)
            //    {
            //        Console.WriteLine("\r\nIndice [" + (i + 1) + ", " + (j + 1) + "] " + teste2[i, j]);
            //    }
            //}
        }

        private void numericUpDownQuantidadeElementos_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownQuantidadeElementos.Value.Equals(2) || numericUpDownQuantidadeElementos.Value > 2) groupBoxElemento2.Visible = true;
            else groupBoxElemento2.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(3) || numericUpDownQuantidadeElementos.Value > 3) groupBoxElemento3.Visible = true;
            else groupBoxElemento3.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(4) || numericUpDownQuantidadeElementos.Value > 4) groupBoxElemento4.Visible = true;
            else groupBoxElemento4.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(5) || numericUpDownQuantidadeElementos.Value > 5) groupBoxElemento5.Visible = true;
            else groupBoxElemento5.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(6) || numericUpDownQuantidadeElementos.Value > 6) groupBoxElemento6.Visible = true;
            else groupBoxElemento6.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(7) || numericUpDownQuantidadeElementos.Value > 7) groupBoxElemento7.Visible = true;
            else groupBoxElemento7.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(8) || numericUpDownQuantidadeElementos.Value > 8) groupBoxElemento8.Visible = true;
            else groupBoxElemento8.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(9) || numericUpDownQuantidadeElementos.Value > 9) groupBoxElemento9.Visible = true;
            else groupBoxElemento9.Visible = false;
            if (numericUpDownQuantidadeElementos.Value.Equals(10)) groupBoxElemento10.Visible = true;
            else groupBoxElemento10.Visible = false;
        }
    }
}
