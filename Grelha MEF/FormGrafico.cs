using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Grelha_MEF
{
    public partial class FormGrafico : Form
    {
        List<double[]>  elementosGraficoDEC;
        List<double[]>  elementosGraficoDMF;
        List<double[]>  elementosGraficoDMT;
        int             quantidadeElementos;
        List<double[]>  elementosGraficoBase;

        public FormGrafico( List<double[]> elementosGraficoDEC, List<double[]> elementosGraficoDMF, List<double[]> elementosGraficoDMT,
                        int quantidadeElementos, List<double[]> elementosGraficoBase)
        {
            InitializeComponent();

            this.elementosGraficoDEC    = elementosGraficoDEC;
            this.elementosGraficoDMF    = elementosGraficoDMF;
            this.elementosGraficoDMT    = elementosGraficoDMT;
            this.quantidadeElementos    = quantidadeElementos;
            this.elementosGraficoBase   = elementosGraficoBase;

            defineGrafico();
        }
        public void defineGrafico()
        {
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();

            List<double[]> extremidadesDEC = new List<double[]>();
            List<double[]> extremidadesDMF = new List<double[]>();
            List<double[]> extremidadesDMT = new List<double[]>();

            for (int h = 1; h <= quantidadeElementos; h++)
            {
                tracaGrafico(h, addElementoGrafico(1, chart1), chart1, extremidadesDEC);
                tracaGrafico(h, addElementoGrafico(1, chart2), chart2, extremidadesDMF);
                tracaGrafico(h, addElementoGrafico(1, chart3), chart3, extremidadesDMT);
            }

            defineDECeDMT(chart1, extremidadesDEC, true);
            defineDMF(chart2, extremidadesDMF);
            defineDECeDMT(chart3, extremidadesDMT, false);
        }
        public string addElementoGrafico(int tam, Chart chart)
        {
            string indexSeries = "Series";
            int indexCount = chart.Series.Count;
            indexSeries = string.Concat(indexSeries, indexCount);

            chart.Series.Add(indexSeries);
            chart.Series[indexSeries].IsValueShownAsLabel = false;
            chart.Series[indexSeries].IsVisibleInLegend = true;
            chart.Series[indexSeries].ChartType = SeriesChartType.Line;
            chart.Series[indexSeries].BorderWidth = tam;
            //chart.Series["Series" + h].IsXValueIndexed = true;
            //chart.Legends.Add(new Legend());
            //chart.Legends[h - 1].Name = "Elemento" + h;
            //chart.Series["Series" + h].Legend = chart1.Legends[h - 1].Name;

            return indexSeries;
        }
        public void tracaGrafico(int h, string indexSeries, Chart chart, List<double[]> extremidades)
        {
            int indexVetor = h - 1;
            int ultimoIndexSeries = indexVetor - 1;

            double valorElementoGrafico = 1;
            string nameSeries = elementosGraficoBase[indexVetor][0].ToString();

            chart.ChartAreas[0].AxisY.Maximum = quantidadeElementos + 1;
            chart.ChartAreas[0].AxisY.Minimum = -(quantidadeElementos + 1);
            chart.ChartAreas[0].AxisX.Maximum = quantidadeElementos + 1;

            if (indexVetor == 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        chart.Series[indexSeries].Points.AddXY(j, j);
                        Console.WriteLine(indexSeries + " [X " + j + "][Y " + j);
                    }
                    else
                    {
                        if (elementosGraficoBase[indexVetor][1].Equals(90) || elementosGraficoBase[indexVetor][1].Equals(-90))
                        {
                            valorElementoGrafico = elementosGraficoBase[indexVetor][1].Equals(-90) ? -valorElementoGrafico : valorElementoGrafico;

                            chart.Series[indexSeries].Points.AddXY(j, valorElementoGrafico);
                            extremidades.Add(new double[] { j, valorElementoGrafico, elementosGraficoBase[indexVetor][0] });
                            //chart.Series[indexSeries].Label = nameSeries;
                            Console.WriteLine(indexSeries + " [" + j + "] Y: " + valorElementoGrafico);
                        }
                        else if (elementosGraficoBase[indexVetor][1].Equals(0))
                        {
                            chart.Series[indexSeries].Points.AddXY(valorElementoGrafico, 0);
                            extremidades.Add(new double[] { valorElementoGrafico, 0, elementosGraficoBase[indexVetor][0] });
                            //chart.Series[indexSeries].Label = nameSeries;
                            Console.WriteLine(indexSeries + " [" + 0 + "] X: " + valorElementoGrafico);
                        }
                    }
                }
            }
            else
            {
                int ultimoPoint = chart.Series[ultimoIndexSeries].Points.Count;
                if (ultimoPoint > 0) ultimoPoint -= 1;

                double ultimoValorX = chart.Series[ultimoIndexSeries].Points[ultimoPoint].XValue;
                double ultimoValorY = chart.Series[ultimoIndexSeries].Points[ultimoPoint].YValues[0];

                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                        Console.WriteLine(indexSeries + " [X " + ultimoValorX + "][Y " + ultimoValorY);
                    }
                    else
                    {
                        if (elementosGraficoBase[indexVetor][1].Equals(0))
                        {
                            double valor = ultimoValorX + valorElementoGrafico;

                            chart.Series[indexSeries].Points.AddXY(valor, ultimoValorY);
                            extremidades.Add(new double[] { valor, ultimoValorY, elementosGraficoBase[indexVetor][0] });
                            //chart.Series[indexSeries].Label = nameSeries;
                            Console.WriteLine(indexSeries + " [Y " + ultimoValorY + "] X: " + valor);
                        }
                        else
                        {
                            if (elementosGraficoBase[indexVetor][1].Equals(-90))
                            {
                                ultimoValorX = ultimoValorX + valorElementoGrafico;
                                valorElementoGrafico = ultimoValorY - valorElementoGrafico;
                            }
                            else if (elementosGraficoBase[indexVetor][1].Equals(90))
                            {
                                ultimoValorX = ultimoValorX + valorElementoGrafico;
                                valorElementoGrafico = ultimoValorY + valorElementoGrafico;
                            }

                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, valorElementoGrafico);
                            extremidades.Add(new double[] { ultimoValorX, valorElementoGrafico, elementosGraficoBase[indexVetor][0] });
                            //chart.Series[indexSeries].Label = nameSeries;
                            Console.WriteLine(indexSeries + " [X " + ultimoValorX + "] Y: " + valorElementoGrafico);
                        }
                    }
                }
            }
            chart.Series[indexSeries].LegendText = " Elemento " + h + ": " + nameSeries;
        }
        public void defineDECeDMT(Chart chart, List<double[]> extremidades, bool dec)
        {
            for (int i = 1; i <= elementosGraficoBase.Count; i++)
            {
                string indexSeries = addElementoGrafico(4, chart);
                int indexVetor = i - 1;
                int ultimoPoint = chart.Series["Series" + indexVetor].Points.Count - 1;
                double ultimoValorY = 0;
                double ultimoValorX = 0;
                bool valorNegativo = false;
                bool valorNegativoAnterior = false;
                double angulo = elementosGraficoBase[indexVetor][1];

                List<double[]> elementosGraficos = dec ? elementosGraficoDEC : elementosGraficoDMT;

                double valorDEC = 1;
                //Math.Round(Math.Abs(elementosGraficoDEC[indexVetor][0] / 10), 1);
                string legendaValor = string.Empty;

                if (angulo.Equals(0))
                {
                    ultimoValorX = extremidades[indexVetor][0];
                    ultimoValorY = extremidades[indexVetor][1];

                    for (int h = 0; h < (extremidades[indexVetor].Length - 1); h++)
                    {
                        legendaValor = Math.Round(elementosGraficos[indexVetor][h], 2).ToString();
                        chart.Series[indexSeries].LegendText += legendaValor + " | ";

                        if (elementosGraficos[indexVetor][h] < 0)
                        {
                            valorNegativo = true;
                            valorNegativoAnterior = true;
                        }
                        else valorNegativo = false;

                        double ultimoValorXaux = (ultimoValorX) - 1;
                        double valorY = valorNegativoAnterior ? (ultimoValorY) - valorDEC : (ultimoValorY) + valorDEC;

                        if (h.Equals(0))
                        {
                            //INICIO DA BARRA INICIAL
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorY);
                            Console.WriteLine("DEC||DMT " + indexSeries + " X " + ultimoValorXaux + " Y " + ultimoValorY);

                            if (elementosGraficos[indexVetor][h] == 0) // extremidadesDEC[indexVetor][h] == 0 || 
                                continue;
                            else
                            {
                                if (valorNegativoAnterior == true && valorNegativo == false) valorY = -valorY;

                                //BARRA INICIAL
                                chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                                Console.WriteLine("DEC||DMT " + indexSeries + " X " + ultimoValorXaux + " Y " + valorY);
                            }
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            //if (valorNegativoAnterior == true && valorNegativo == false) valorY = -valorY;

                            //valorY = Math.Abs(valorY);

                            if (elementosGraficos[indexVetor][h] == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                                Console.WriteLine("DEC||DMT " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                                continue;
                            }

                            //BARRA MAIOR
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, valorY);
                            //chart.Series[indexSeries].Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DEC||DMT " + indexSeries + " X " + valorY + " Y " + ultimoValorX);

                            //BARRA FECHAMENTO
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DEC||DMT " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                        }
                    }
                }
                else if (angulo.Equals(90) || angulo.Equals(-90))
                {

                    ultimoValorX = extremidades[indexVetor][0];
                    ultimoValorY = extremidades[indexVetor][1];

                    for (int h = 0; h < (extremidades[indexVetor].Length - 1); h++)
                    {
                        legendaValor = Math.Round(elementosGraficos[indexVetor][h], 2).ToString();
                        chart.Series[indexSeries].LegendText += legendaValor + " | ";

                        if (elementosGraficos[indexVetor][h] < 0)
                        {
                            valorNegativo = true;
                            valorNegativoAnterior = true;
                        }
                        else valorNegativo = false;

                        double ultimoValorXaux = (ultimoValorX) - 1;
                        double ultimoValorYaux = angulo.Equals(-90) ? (ultimoValorY) + 1 : (ultimoValorY) - 1;
                        double valorY = valorNegativo ? (ultimoValorYaux) - valorDEC : (ultimoValorYaux) + valorDEC;

                        if (h.Equals(0))
                        {
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorYaux);
                            Console.WriteLine("DEC||DMT " + indexSeries + " X " + ultimoValorXaux + " Y " + ultimoValorYaux);

                            if (elementosGraficoDEC[indexVetor][h] == 0)
                                continue;
                            else
                            {
                                //BARRA INICIAL
                                chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                                Console.WriteLine("DEC||DMT " + indexSeries + " X " + ultimoValorXaux + " Y " + valorY);
                            }
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            valorY = valorNegativoAnterior ? -valorY : valorY;

                            if (elementosGraficos[indexVetor][h] == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                                Console.WriteLine("DEC||DMT " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                                continue;
                            }

                            //BARRA MAIOR
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY - valorDEC);
                            //chart.Series[indexSeries].Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DEC||DMT " + indexSeries + " X " + ultimoValorX + " Y " + (ultimoValorY - valorDEC));

                            //BARRA FECHAMENTO
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DEC||DMT " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                        }
                    }
                }
            }
        }
        public void defineDMF(Chart chart, List<double[]> extremidadesDMF)
        {
            for (int i = 1; i <= elementosGraficoBase.Count; i++)
            {
                string indexSeries = addElementoGrafico(4, chart);
                int indexVetor = i - 1;
                int ultimoPoint = chart.Series["Series" + indexVetor].Points.Count - 1;
                double ultimoValorY = 0;
                double ultimoValorX = 0;
                bool valorNegativo = false;
                double angulo = elementosGraficoBase[indexVetor][1];

                double valorDMF = 1;
                //Math.Round(Math.Abs(elementosGraficoDMF[indexVetor][0] / 10), 1);
                string legendaValor = string.Empty;

                if (angulo.Equals(0))
                {
                    //for (int k = 0; k < extremidadesDMF.Count; k++)
                    //{
                    ultimoValorX = extremidadesDMF[indexVetor][0];
                    ultimoValorY = extremidadesDMF[indexVetor][1];

                    for (int h = 0; h < elementosGraficoDMF[indexVetor].Length; h++)
                    {
                        legendaValor = Math.Round(elementosGraficoDMF[indexVetor][h], 2).ToString();
                        chart.Series[indexSeries].LegendText += legendaValor + " | ";

                        if (elementosGraficoDMF[indexVetor][h] < 0)
                            valorNegativo = true;
                        else valorNegativo = false;

                        double ultimoValorXaux = ultimoValorX - 1;
                        double valorY = valorNegativo ? ultimoValorY + valorDMF : ultimoValorY - valorDMF;

                        if (h.Equals(0))
                        {
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorY);
                            Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorXaux + "] " + ultimoValorY);

                            if (elementosGraficoDMF[indexVetor][h] == 0)
                                continue;
                            else
                            {
                                //BARRA INICIAL DMF
                                chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                                Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorXaux + "] " + valorY);
                            }
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            if (elementosGraficoDMF[indexVetor][h] == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                                Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorX + "] " + ultimoValorY);
                                continue;
                            }

                            //BARRA MAIOR DMF
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, valorY);
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorX + "] " + valorY);

                            //BARRA FECHAMENTO DMF
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorX + "] " + ultimoValorY);
                        }
                    }
                }
                else if (angulo.Equals(90) || angulo.Equals(-90))
                {
                    ultimoValorX = extremidadesDMF[indexVetor][0];
                    ultimoValorY = extremidadesDMF[indexVetor][1];

                    for (int h = 0; h < elementosGraficoDMF[indexVetor].Length; h++)
                    {
                        legendaValor = Math.Round(elementosGraficoDMF[indexVetor][h], 2).ToString();
                        chart.Series[indexSeries].LegendText += legendaValor + " | ";

                        if (elementosGraficoDMF[indexVetor][h] < 0)
                            valorNegativo = true;
                        else valorNegativo = false;

                        double ultimoValorXaux = ultimoValorX - 1;
                        double ultimoValorYaux = angulo.Equals(-90) ? ultimoValorY + 1 : ultimoValorY - 1;
                        double valorY = valorNegativo ? valorDMF : -valorDMF;

                        if (h.Equals(0))
                        {
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorYaux);
                            Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorXaux + "] " + ultimoValorYaux);

                            if (elementosGraficoDMF[indexVetor][h] == 0)
                                continue;

                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            if (elementosGraficoDMF[indexVetor][h] == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorX + "] " + ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";

                                continue;
                            }

                            //BARRA INICIAL DMF
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                            Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorXaux + "] " + valorY);

                            //BARRA MAIOR DMF
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY - valorDMF);
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorX + "] " + (ultimoValorY - valorDMF));

                            //BARRA FECHAMENTO DMF
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DMF " + indexSeries + " [" + ultimoValorX + "] " + ultimoValorY);
                        }

                    }
                }
            }
        }
    }
}
