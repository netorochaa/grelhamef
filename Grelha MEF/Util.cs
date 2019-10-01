using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grelha_MEF
{
    class Util
    {
        //Graus de liberdade (associando local a global)
        //no 1
        public static int[] sistemaGlobalGrausLiberdadeNo1 = new int[] { 1, 2, 3 };
        public static int[] sistemaLocalElementos1No1 = new int[] { 1, 2, 3 };
        public static int[] sistemaLocalElementos2No1 = new int[] { 1, 2, 3 };
        //no 2
        public static int[] sistemaGlobalGrausLiberdadeNo2 = new int[] { 4, 5, 6 };
        public static int[] sistemaLocalElementos1No2 = new int[] { 0, 0, 0 };
        public static int[] sistemaLocalElementos2No2 = new int[] { 4, 5, 6 };
        //no 3
        public static int[] sistemaGlobalGrausLiberdadeNo3 = new int[] { 7, 8, 9 };
        public static int[] sistemaLocalElementos1No3 = new int[] { 4, 5, 6 };
        public static int[] sistemaLocalElementos2No3 = new int[] { 0, 0, 0 };
        //item 1: graus de liberdade
        //item 2: elemento 1
        //item 3: elemento 2
        public static Tuple<int[], int[], int[]> grauLiberdadeNo1AssociandoLocalAGlobal = new Tuple<int[], int[], int[]>(sistemaGlobalGrausLiberdadeNo1, sistemaLocalElementos1No1, sistemaLocalElementos2No1);
        public static Tuple<int[], int[], int[]> grauLiberdadeNo2AssociandoLocalAGlobal = new Tuple<int[], int[], int[]>(sistemaGlobalGrausLiberdadeNo2, sistemaLocalElementos1No2, sistemaLocalElementos2No2);
        public static Tuple<int[], int[], int[]> grauLiberdadeNo3AssociandoLocalAGlobal = new Tuple<int[], int[], int[]>(sistemaGlobalGrausLiberdadeNo3, sistemaLocalElementos1No3, sistemaLocalElementos2No3);

        public static int quantidadeGrausLiberdadeGlobal = sistemaGlobalGrausLiberdadeNo1.Length + sistemaGlobalGrausLiberdadeNo2.Length + sistemaGlobalGrausLiberdadeNo3.Length;
        public static int[] grausLiberdadeGlobal = new int[quantidadeGrausLiberdadeGlobal];

        //Associando nó global a nó local
        //elemento 1
        public static int[] noLocalElemento1 = { 1, 2 };
        public static int[] noGlobalElemento1 = { 1, 3 };
        //elemento 2
        public static int[] noLocalElemento2 = { 1, 2 };
        public static int[] noGlobalElemento2 = { 1, 2 };
        //item 1: nó local do elemento
        //item 2: nó global do elemento
        public static Tuple<int[], int[]> elemento1AssociandoGlobalALocal = new Tuple<int[], int[]>(noLocalElemento1, noGlobalElemento1);
        public static Tuple<int[], int[]> elemento2AssociandoGlobalALocal = new Tuple<int[], int[]>(noLocalElemento2, noGlobalElemento2);

        //Montagem da matriz local - elemento 1

        /*public static Tuple<int, int[], int[]> kl1 = new Tuple<int[,], int[,], int[,], int[,], int[,], int[,], int[,], int[,], int[,]>();

           { 
                {11, 12, 13, 0, 0, 0, 14, 15, 16},
                {21, 22, 23, 0, 0, 0, 24, 25, 26},
                {31, 32, 33, 0, 0, 0, 34, 35, 36},
                {0,   0,  0, 0, 0, 0,  0,  0,  0},
                {0,   0,  0, 0, 0, 0,  0,  0,  0},
                {0,   0,  0, 0, 0, 0,  0,  0,  0},
                {41, 42, 43, 0, 0, 0, 44, 45, 46},
                {51, 52, 53, 0, 0, 0, 54, 55, 56},
                {61, 62, 63, 0, 0, 0, 64, 65, 66},
            };
         */
    }
}
