using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grelha_MEF
{
    public static class MatrixUtil
    {
        //MATRIZ INVERSA DOC MICROSOFT
        public static double[][] MatrixCreate(int rows, int cols)
        {   // creates a matrix initialized to all 0.0s   // do error checking here?
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols]; // auto init to 0.0   
            return result;
        }
        public static double[][] MatrixDuplicate(double[][] matrix)
        {
            // assumes matrix is not null.
            double[][] result = MatrixCreate(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; ++i) // copy the values     
                for (int j = 0; j < matrix[i].Length; ++j)
                    result[i][j] = matrix[i][j];
            return result;
        }
        public static double[][] MatrixDecompose(double[][] matrix, out int[] perm, out int toggle)
        {
            // Doolittle LUP decomposition.
            // assumes matrix is square.
            int n = matrix.Length; // convenience   
            double[][] result = MatrixDuplicate(matrix);
            perm = new int[n];
            for (int i = 0; i < n; ++i)
            {
                perm[i] = i;
            }
            toggle = 1;

            for (int j = 0; j < n - 1; ++j) // each column
            {
                double colMax = Math.Abs(result[j][j]); // largest val in col j
                int pRow = j;
                for (int i = j + 1; i < n; ++i)
                {
                    if (result[i][j] > colMax)
                    {
                        colMax = result[i][j];
                        pRow = i;
                    }
                }
                if (pRow != j) // swap rows
                {
                    double[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;
                    int tmp = perm[pRow]; // and swap perm info
                    perm[pRow] = perm[j];
                    perm[j] = tmp;
                    toggle = -toggle; // row-swap toggle
                }
                if (Math.Abs(result[j][j]) < 1.0E-20)
                    return null; // consider a throw
                for (int i = j + 1; i < n; ++i)
                {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < n; ++k)
                        result[i][k] -= result[i][j] * result[j][k];
                }
            } // main j column loop
            return result;
        }
        public static double[] HelperSolve(double[][] luMatrix, double[] b)
        {
            // solve luMatrix * x = b
            int n = luMatrix.Length;
            double[] x = new double[n];
            b.CopyTo(x, 0);
            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                    sum -= luMatrix[i][j] * x[j];
                x[i] = sum;
            }
            x[n - 1] /= luMatrix[n - 1][n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                    sum -= luMatrix[i][j] * x[j];
                x[i] = sum / luMatrix[i][i];
            }
            return x;
        }
        public static double[][] MatrixInverse(double[,] a)
        {
            double[][] m = MatrixConverter(a);
            int n = m.Length;
            double[][] result = MatrixDuplicate(m);
            int[] perm; int toggle;
            double[][] lum = MatrixDecompose(m, out perm, out toggle);
            if (lum == null)
            {
                //throw new Exception("Unable to compute inverse");
                return null;
            }
            double[] b = new double[n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == perm[j])
                        b[j] = 1.0;
                    else
                        b[j] = 0.0;
                }
                double[] x = HelperSolve(lum, b);
                for (int j = 0; j < n; ++j)
                    result[j][i] = x[j];
            }
            return result;
        }
        public static double[][] MatrixConverter(double[,] matrizIncorporada)
        {
            // return matrix with values between minVal and maxVal
            int n = matrizIncorporada.GetLength(0);
            double[][] result = MatrixCreate(n, n);
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    result[i][j] = matrizIncorporada[i, j];
            return result;
        }
        public static double[,] MatrixReConverter(double[][] matrizDeMatrizes)
        {
            int n = matrizDeMatrizes.Length;
            double[,] result = new double[n, n];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    result[i, j] = matrizDeMatrizes[i][j];
            return result;
        }
        public static string MatrixAsString(double[,] matrix)
        {
            string s = "";
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                    s += matrix[i, j].ToString("F3").PadLeft(8) + " "; s += Environment.NewLine;
            }
            return s;
        }
        public static string VectorAsString(double[] vector)
        {
            string s = "";
            for (int i = 0; i < vector.GetLength(0); ++i)
            {
                    s += vector[i].ToString("F3").PadLeft(8) + " "; s += Environment.NewLine;
            }
            return s;
        }
        public static string VectorAsStringForLogForm(double[] vector)
        {
            string s = "";
            for (int i = 0; i < vector.GetLength(0); ++i)
            {
                s += vector[i].ToString("F3").PadLeft(8) + " "; 
                s += Environment.NewLine;
            }
            return s;
        }
        public static double[,] matrizRotacaoInversa(int x)
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

            double[,] matrizResultante;

            if (x == 90) matrizResultante = matrizRotacaoAngulo90Inversa;
            else matrizResultante = matrizRotacaoAngulo0Inversa;

            return matrizResultante;
        }
        public static double[,] matrizRotacao(List<double[,]> matrizesRotPorAnguloDoElem, int x)
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

            matrizesRotPorAnguloDoElem.Add(matrizRotacao);

            return matrizRotacao;
        }
        public static double[,] multiplicacaoMatrizes(int[] grausLiberdadeLocal, double[,] matrizA, double[,] matrizB, string nome)
        {
            double valorCelula = 0;
            int quantidadeCelulas = (matrizA.Length + matrizB.Length) / 2;
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
                else if (celula.Equals(tamDimensao * grausLiberdadeLocal.Length) && i < tamDimensao - 1)
                {
                    matrizResultante[i, cont] = valorCelula;
                    //Console.WriteLine("Índice [" + (i + 1) + "," + (cont + 1) + "] " + valorCelula);

                    i++;
                    cont = 0;
                    j = -1;
                    celula = 0;
                    valorCelula = 0;
                }
                else if (i.Equals(tamDimensao - 1) && celula.Equals(tamDimensao * grausLiberdadeLocal.Length))
                {
                    matrizResultante[i, cont] = valorCelula;
                    break;
                }

            }
            valorCelula = 0;

            return matrizResultante;

        }
        public static double[] multiplicacaoMatrizComVetor(double[,] matriz, double[] vetor, string nome)
        {
            double valorCelula = 0;
            int tamDimensao = vetor.Length;

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
                vetorResultante[i] = valorCelula;
                valorCelula = 0;
            }

            return vetorResultante;

        }
    }
}
