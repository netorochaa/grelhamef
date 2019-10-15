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
    public partial class Form1 : Form
    {   
        //Matriz de rigidez local
        //elemento 1
        public static int[,] rigidezLocalElemento1 = new int[6,6] 
            {  
                {11, 12, 13, 14, 15, 16},
                {21, 22, 23, 24, 25, 26},
                {31, 32, 33, 34, 35, 36},
                {41, 42, 43, 44, 45, 46},
                {51, 52, 53, 54, 55, 56},
                {61, 62, 63, 64, 65, 66}
            };
        //elemento 2
        public static int[,] rigidezLocalElemento2 = new int[6,6] 
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

        public Form1()
        {
            //InitializeComponent();
            inicializaVetores();
            inicializaMatrizElemento1();
            inicializaMatrizElemento2();
            inicializaMatrizGlobalDosElementos();
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
                    Console.WriteLine("Indice [" + (i + 1) + ", " + (j + 1) + "] " + matrizGlobalEstrutura[i, j]);
                }
            }
        }

        //**INSERIR BOTÃO PARA INICIALIZAR ESSA MATRIZ**
        public void inicializaMatrizRigidezEmCoordenadasLocais(double b, double h, double moduloYoung, double coeficientePoisson, double L1, double L2)
        {
            //Calculando Separadamente
            double I = (b * Math.Pow(h, 3))/12;
            //string unidadeMedidaI = "m⁴";

            double E = moduloYoung * Math.Pow(10, 9);
            //string unidadeMedidaE = "kN/m²";

            double V = coeficientePoisson;

            double G = E / (2 * (1 + V));
            //string unidadeMedidaG = "kN/m²";

            double J = h * Math.Pow(b, 3) * ((1 / 3) - 0.21 * (b / h) * (1 - (Math.Pow(b, 4) / (12 * Math.Pow(h, 4)))));
            //string unidadeMedidaJ = "m⁴";

            //Matriz de rigidez do elemento em coordenadas locais
            double[,] matrizRigidezElementoEmCoordenadasLocais = new double[6, 6]
                {
                    {(12*E*I)/Math.Pow(L1, 3), 0, (G*E*I)/Math.Pow(L1, 2), (-12*E*I)/Math.Pow(L1, 3), 0, (6*E*I)/Math.Pow(L1, 2)},
                    {0, (G*J)/L1, 0, 0, (-G*J)/L1, 0},
                    {(6*E*I)/Math.Pow(L1, 2), 0, (4*E*I)/L1, (-6*E*I)/Math.Pow(L1, 2), 0, (2*E*I)/L1},
                    {(-12*E*I)/Math.Pow(L1, 3), 0, (-6*E*I)/Math.Pow(L1, 2), (12*E*I)/Math.Pow(L1, 3), 0, 0},
                    {0, (-G*J)/L1, 0, 0, (G*J)/L1, 0},
                    {(6*E*I)/Math.Pow(L1, 2), 0, (2*E*I)/L1, (-6*E*I)/Math.Pow(L1, 2), 0, (4*E*I)/L1}
                };
        }

        //**INSERIR BOTÃO PARA INICIALIZAR ESSA MATRIZ**
        public void inicializaMatrizRotacao(double conseno, double seno)
        {
            double[,] matrizRotacao = new double[6, 6]
                {
                    {1, 0, 0, 0, 0, 0},
                    {0, conseno, seno, 0, 0, 0},
                    {0, -seno, conseno, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 0, conseno, seno},
                    {0, 0, 0, 0, -seno, conseno}
                };
        }
    }
}
