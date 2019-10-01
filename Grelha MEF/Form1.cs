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
        //Graus de liberdade (associando local a global)
        //no 1
        public static int[] sistemaGlobalGrausLiberdadeNo1  = new int[] { 1, 2, 3 };
        public static int[] sistemaLocalElemento1No1       = new int[] { 1, 2, 3 };
        public static int[] sistemaLocalElemento2No1       = new int[] { 1, 2, 3 };
        //no 2
        public static int[] sistemaGlobalGrausLiberdadeNo2  = new int[] { 4, 5, 6 };
        public static int[] sistemaLocalElemento1No2       = new int[] { 0, 0, 0 };
        public static int[] sistemaLocalElemento2No2       = new int[] { 4, 5, 6 };
        //no 3
        public static int[] sistemaGlobalGrausLiberdadeNo3  = new int[] { 7, 8, 9 };
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
        string[,] matrizLocalElemeto1 = new string[grausLiberdadeGlobal.Length, grausLiberdadeGlobal.Length];

        //Vetores totais do elemento 2
        public static int quantidadeLocalElemento2 = sistemaLocalElemento2No1.Length + sistemaLocalElemento2No2.Length + sistemaLocalElemento2No3.Length;
        public static int[] localElemento2         = new int[quantidadeLocalElemento2];
        //Cria matriz local do elemento 2
        string[,] matrizLocalElemeto2 = new string[grausLiberdadeGlobal.Length, grausLiberdadeGlobal.Length];

        //Matriz global dos elementos
        string[,] matrizGlobalElemetos = new string[grausLiberdadeGlobal.Length, grausLiberdadeGlobal.Length];

        //Associando nó global a nó local
        //elemento 1
        public static int[] noLocalElemento1  = { 1, 2 };
        public static int[] noGlobalElemento1 = { 1, 3 };
        //elemento 2
        public static int[] noLocalElemento2  = { 1, 2 };
        public static int[] noGlobalElemento2 = { 1, 2 };
        //item 1: nó local do elemento
        //item 2: nó global do elemento
        public static Tuple<int[], int[]> elemento1AssociandoGlobalALocal = new Tuple<int[], int[]>(noLocalElemento1, noGlobalElemento1);
        public static Tuple<int[], int[]> elemento2AssociandoGlobalALocal = new Tuple<int[], int[]>(noLocalElemento2, noGlobalElemento2);

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
                        matrizLocalElemeto1[i, j] = "K" + localElemento1[i] + "," + localElemento1[j] + "(1)";
                    else
                        matrizLocalElemeto1[i, j] = "-";
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
                        matrizLocalElemeto2[i, j] = "K" + localElemento2[i] + "," + localElemento2[j] + "(2)";
                    else
                        matrizLocalElemeto2[i, j] = "-";
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
                    if (!matrizLocalElemeto1[i, j].Equals("-") && !matrizLocalElemeto2[i, j].Equals("-"))
                        matrizGlobalElemetos[i, j] = matrizLocalElemeto1[i, j] + "+" + matrizLocalElemeto2[i, j];
                    else if (!matrizLocalElemeto1[i, j].Equals("-") && matrizLocalElemeto2[i, j].Equals("-"))                    
                        matrizGlobalElemetos[i, j] = matrizLocalElemeto1[i, j];
                    else if (matrizLocalElemeto1[i, j].Equals("-") && !matrizLocalElemeto2[i, j].Equals("-") )
                        matrizGlobalElemetos[i, j] = matrizLocalElemeto2[i, j];
                    else
                        matrizGlobalElemetos[i, j] = "0";

                    //matrizGlobalElemetos[i, j] = matrizLocalElemeto1[i, j] + " + " + matrizLocalElemeto2[i, j];
                    //else
                    //    matrizGlobalElemetos[i, j] = "0";
                    Console.WriteLine("Indice [" + (i + 1) + ", " + (j + 1) + "] " + matrizGlobalElemetos[i, j]);
                }
            }
        }

    }
}
