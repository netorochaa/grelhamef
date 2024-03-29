﻿using System;
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
        List<double[]> elementosGraficoDEC;
        List<double[]> elementosGraficoDMT;
        List<double[]> elementosGraficoDMF;
        List<double[]> elementosGraficoBase;
        List<double[]> vetoresDeslocGiroGlobalElem;
        List<double[]> vetoresEsforcosInternosElem;
        int quantidadeElementos;

        public FormGrafico(int quantidadeElementos, List<double[]> elementosGraficoBase)
        {
            this.quantidadeElementos = quantidadeElementos;
            this.elementosGraficoBase = elementosGraficoBase;
        }

        public FormGrafico( List<double[]> elementosGraficoDEC, List<double[]> elementosGraficoDMT, List<double[]> elementosGraficoDMF,
                            int quantidadeElementos, List<double[]> elementosGraficoBase, List<double[]> vetoresDeslocGiroGlobalElem,
                            List<double[]> vetoresEsforcosInternosElem)
        {
            InitializeComponent();

            this.elementosGraficoDEC         = elementosGraficoDEC;
            this.elementosGraficoDMT         = elementosGraficoDMT;
            this.elementosGraficoDMF         = elementosGraficoDMF;
            this.quantidadeElementos         = quantidadeElementos;
            this.elementosGraficoBase        = elementosGraficoBase;
            this.vetoresDeslocGiroGlobalElem = vetoresDeslocGiroGlobalElem;
            this.vetoresEsforcosInternosElem = vetoresEsforcosInternosElem;

            defineGrafico(null);
        }

        public void defineGrafico(Chart chart)
        {
            if(chart != null)
            {
                chart.Series.Clear();

                for (int h = 1; h <= elementosGraficoBase.Count; h++)
                {
                    tracaGrafico(h, addElementoGrafico(3, chart, Color.Black), chart, null, elementosGraficoBase);
                }
            }
            else
            {
                chart1.Series.Clear();
                chart2.Series.Clear();
                chart3.Series.Clear();
                List<double[]> extremidadesDEC = new List<double[]>();
                List<double[]> extremidadesDMT = new List<double[]>();
                List<double[]> extremidadesDMF = new List<double[]>();
                for (int h = 1; h <= quantidadeElementos; h++)
                {
                    tracaGrafico(h, addElementoGrafico(1, chart1, Color.Black), chart1, extremidadesDEC, elementosGraficoBase);
                    tracaGrafico(h, addElementoGrafico(1, chart2, Color.Black), chart2, extremidadesDMT, elementosGraficoBase);
                    tracaGrafico(h, addElementoGrafico(1, chart3, Color.Black), chart3, extremidadesDMF, elementosGraficoBase);
                }
                defineDEC(chart1, extremidadesDEC);
                defineDMF(chart2, extremidadesDMF);
                defineDMT(chart3, extremidadesDMT);
            }
        }
        public string addElementoGrafico(int tam, Chart chart, Color cor)
        {
            string indexSeries = "Series";
            int indexCount = chart.Series.Count;
            indexSeries = string.Concat(indexSeries, indexCount);

            chart.Series.Add(indexSeries);
            chart.Series[indexSeries].IsValueShownAsLabel = false;
            chart.Series[indexSeries].IsVisibleInLegend = true;
            chart.Series[indexSeries].ChartType = SeriesChartType.Line;
            chart.Series[indexSeries].BorderWidth = tam;
            chart.Series[indexSeries].Color = cor;
            chart.Series[indexSeries].CustomProperties = "IsXAxisQuantitative=True, EmptyPointValue=Zero, LabelStyle=TopLeft";
            
            return indexSeries;
        }
        public void tracaGrafico(int h, string indexSeries, Chart chart, List<double[]> extremidades, List<double[]> elementosGraficoBase)
        {
            int indexVetor              = h - 1;
            int ultimoIndexSeries       = indexVetor - 1;
            double valorElementoGrafico = 1;
            string nameSeries           = extremidades != null ? elementosGraficoBase[indexVetor][0].ToString() : "";

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
                            if(extremidades != null)
                                extremidades.Add(new double[] { j, valorElementoGrafico, elementosGraficoBase[indexVetor][0] });
                            Console.WriteLine(indexSeries + " [" + j + "] Y: " + valorElementoGrafico);
                        }
                        else if (elementosGraficoBase[indexVetor][1].Equals(0))
                        {
                            chart.Series[indexSeries].Points.AddXY(valorElementoGrafico, 0);
                            if (extremidades != null)
                                extremidades.Add(new double[] { valorElementoGrafico, 0, elementosGraficoBase[indexVetor][0] });
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
                            if (extremidades != null)
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
                            if (extremidades != null) 
                                extremidades.Add(new double[] { ultimoValorX, valorElementoGrafico, elementosGraficoBase[indexVetor][0] });
                            //chart.Series[indexSeries].Label = nameSeries;
                            Console.WriteLine(indexSeries + " [X " + ultimoValorX + "] Y: " + valorElementoGrafico);
                        }
                    }
                }
            }
            chart.Series[indexSeries].LegendText = " Elemento " + h;
        }
        public void defineDEC(Chart chart, List<double[]> extremidadesDEC)
        {
            for (int i = 1; i <= elementosGraficoBase.Count; i++)
            {
                string indexSeries = addElementoGrafico(4, chart, Color.SteelBlue);
                int indexVetor = i - 1;
                int ultimoPoint = chart.Series["Series" + indexVetor].Points.Count - 1;
                double ultimoValorY = 0;
                double ultimoValorX = 0;
                bool valorNegativo = false;
                bool valorNegativoAnterior = false;
                double angulo = elementosGraficoBase[indexVetor][1];

                double valorInteracaoGrafico = 1;
                string legendaValor = string.Empty;

                if (angulo.Equals(0))
                {
                    ultimoValorX = extremidadesDEC[indexVetor][0];
                    ultimoValorY = extremidadesDEC[indexVetor][1];

                    for (int h = 0; h < (extremidadesDEC[indexVetor].Length - 1); h++)
                    {
                        double valorEsfInternoNO = Math.Round(elementosGraficoDEC[indexVetor][h], 3);
                        legendaValor = valorEsfInternoNO.ToString("F2");
                        chart.Series[indexSeries].LegendText += legendaValor + " | ";

                        if (valorEsfInternoNO < 0)
                        {
                            valorNegativo = true;
                            valorNegativoAnterior = true;
                        }
                        else valorNegativo = false;

                        double ultimoValorXaux = (ultimoValorX) - 1;
                        double valorY = valorNegativoAnterior ? (ultimoValorY) - valorInteracaoGrafico : (ultimoValorY) + valorInteracaoGrafico;
                        //if (!dec) valorY = valorNegativoAnterior ? (ultimoValorY) + valorInteracaoGrafico : (ultimoValorY) - valorInteracaoGrafico;

                        if (h.Equals(0))
                        {
                            //INICIO DA BARRA INICIAL
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorY);
                            Console.WriteLine("DEC " + indexSeries + " X " + ultimoValorXaux + " Y " + ultimoValorY);

                            if (valorEsfInternoNO == 0) // extremidadesDEC[indexVetor][h] == 0 || 
                                continue;
                            else
                            {
                                if (valorNegativoAnterior == true && valorNegativo == false) valorY = -valorY;

                                //BARRA INICIAL
                                chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                                Console.WriteLine("DEC " + indexSeries + " X " + ultimoValorXaux + " Y " + valorY);
                            }
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            //if (valorNegativoAnterior == true && valorNegativo == false) valorY = -valorY;

                            //valorY = Math.Abs(valorY);

                            if (valorEsfInternoNO == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                                Console.WriteLine("DEC " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                                continue;
                            }

                            //BARRA MAIOR
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, valorY);
                            //chart.Series[indexSeries].Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DEC " + indexSeries + " X " + valorY + " Y " + ultimoValorX);

                            //BARRA FECHAMENTO
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DEC " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                        }
                    }
                }
                else if (angulo.Equals(90) || angulo.Equals(-90))
                {

                    ultimoValorX = extremidadesDEC[indexVetor][0];
                    ultimoValorY = extremidadesDEC[indexVetor][1];
                    double valorY = 0;

                    for (int h = 0; h < (extremidadesDEC[indexVetor].Length - 1); h++)
                    {
                        //TODO VALOR DO 2º NÓ DO ELEMENTO SERÁ NEGATIVO

                        double valorEsfInternoNO = Math.Round(elementosGraficoDEC[indexVetor][h], 3);
                        legendaValor = valorEsfInternoNO.ToString("F2");
                        chart.Series[indexSeries].LegendText += legendaValor + " - ";

                        if (valorEsfInternoNO < 0)
                        {
                            valorNegativo = true;
                            valorNegativoAnterior = true;
                        }
                        else valorNegativo = false;

                        double ultimoValorXaux = (ultimoValorX) - 1;
                        double ultimoValorYaux;
                        //= angulo.Equals(-90) && h == 0 ? (ultimoValorY) + 1 : (ultimoValorY) - 1;
                        //ultimoValorYaux = angulo.Equals(-90) && h > 0 ? (ultimoValorY) + 1 : (ultimoValorY) - 1;
                        if (angulo.Equals(-90))
                        {
                            if (h == 0) ultimoValorYaux = (ultimoValorY) + 1;
                            else ultimoValorYaux = (valorY) - 1;
                        }
                        else
                        {
                            if (h == 0) ultimoValorYaux = (ultimoValorY) - 1;
                            else ultimoValorYaux = (valorY) + 1;
                        }

                        valorY = valorNegativoAnterior ? (ultimoValorYaux) - valorInteracaoGrafico : (ultimoValorYaux) + valorInteracaoGrafico;
                        //if (!dec) valorY = valorNegativoAnterior ? (ultimoValorY) + valorInteracaoGrafico : (ultimoValorY) - valorInteracaoGrafico;

                        if (h.Equals(0))
                        {
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorYaux);
                            Console.WriteLine("DEC " + indexSeries + " X " + ultimoValorXaux + " Y " + ultimoValorYaux);

                            if (valorEsfInternoNO == 0)
                                continue;
                            else
                            {
                                //BARRA INICIAL
                                chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                                Console.WriteLine("DEC " + indexSeries + " X " + ultimoValorXaux + " Y " + valorY);
                            }
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            //valorY = valorNegativoAnterior ? -valorY : valorY;

                            if (valorEsfInternoNO == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                                Console.WriteLine("DEC " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                                continue;
                            }

                            //BARRA MAIOR
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorYaux);
                            //else chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorY);
                            //chart.Series[indexSeries].Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DEC " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorYaux);

                            //BARRA FECHAMENTO
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DEC " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                        }
                    }
                }
            }
        }
        public void defineDMF(Chart chart, List<double[]> extremidadesDMF)
        {
            for (int i = 1; i <= elementosGraficoBase.Count; i++)
            {
                string indexSeries = addElementoGrafico(4, chart, Color.SteelBlue);
                int indexVetor = i - 1;
                int ultimoPoint = chart.Series["Series" + indexVetor].Points.Count - 1;
                double ultimoValorY = 0;
                double ultimoValorX = 0;
                bool valorNegativo = false;
                bool valorNegativoAnterior = false;
                double angulo = elementosGraficoBase[indexVetor][1];

                double valorInteracaoGrafico = 1;
                string legendaValor = string.Empty;

                if (angulo.Equals(0))
                {
                    ultimoValorX = extremidadesDMF[indexVetor][0];
                    ultimoValorY = extremidadesDMF[indexVetor][1];

                    for (int h = 0; h < (extremidadesDMF[indexVetor].Length - 1); h++)
                    {
                        double valorEsfInternoNO = Math.Round(elementosGraficoDMF[indexVetor][h], 3);
                        legendaValor = valorEsfInternoNO.ToString("F2");
                        chart.Series[indexSeries].LegendText += legendaValor + " | ";

                        if (valorEsfInternoNO < 0)
                        {
                            valorNegativo = true;
                            valorNegativoAnterior = true;
                        }
                        else valorNegativo = false;

                        double ultimoValorXaux = (ultimoValorX) - 1;
                        double valorY = valorNegativoAnterior ? (ultimoValorY) + valorInteracaoGrafico : (ultimoValorY) - valorInteracaoGrafico;

                        if (h.Equals(0))
                        {
                            //INICIO DA BARRA INICIAL
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorY);
                            Console.WriteLine("DMF " + indexSeries + " X " + ultimoValorXaux + " Y " + ultimoValorY);

                            if (valorEsfInternoNO == 0)
                                continue;
                            else
                            {
                                //if (valorNegativoAnterior == true && valorNegativo == false) valorY = -valorY;

                                //BARRA INICIAL
                                chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                                Console.WriteLine("DMF " + indexSeries + " X " + ultimoValorXaux + " Y " + valorY);
                            }
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            //if (valorNegativoAnterior == true && valorNegativo == false) valorY = -valorY;

                            //valorY = Math.Abs(valorY);

                            if (valorEsfInternoNO == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                                Console.WriteLine("DMF " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                                continue;
                            }

                            //BARRA MAIOR
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, valorY);
                            //chart.Series[indexSeries].Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DMF " + indexSeries + " X " + valorY + " Y " + ultimoValorX);

                            //BARRA FECHAMENTO
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DMF " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                        }
                    }
                }
                else if (angulo.Equals(90) || angulo.Equals(-90))
                {

                    ultimoValorX = extremidadesDMF[indexVetor][0];
                    ultimoValorY = extremidadesDMF[indexVetor][1];
                    double valorY = 0;

                    for (int h = 0; h < (extremidadesDMF[indexVetor].Length - 1); h++)
                    {
                        //TODO VALOR DO 2º NÓ DO ELEMENTO SERÁ NEGATIVO

                        double valorEsfInternoNO = Math.Round(elementosGraficoDMF[indexVetor][h], 3);
                        legendaValor = Math.Round(valorEsfInternoNO).ToString("F2");
                        chart.Series[indexSeries].LegendText += legendaValor + " - ";

                        if (valorEsfInternoNO < 0)
                        {
                            valorNegativo = true;
                            valorNegativoAnterior = true;
                        }
                        else valorNegativo = false;

                        double ultimoValorXaux = (ultimoValorX) - 1;
                        double ultimoValorYaux;
                        //= angulo.Equals(-90) && h == 0 ? (ultimoValorY) + 1 : (ultimoValorY) - 1;
                        //ultimoValorYaux = angulo.Equals(-90) && h > 0 ? (ultimoValorY) + 1 : (ultimoValorY) - 1;
                        if (angulo.Equals(-90))
                        {
                            if (h == 0) ultimoValorYaux = (ultimoValorY) + 1;
                            else ultimoValorYaux = (valorY) - 1;
                        }
                        else
                        {
                            if (h == 0) ultimoValorYaux = (ultimoValorY) - 1;
                            else 
                                ultimoValorYaux = valorNegativo ? (ultimoValorY) + 1 : (ultimoValorY) - 1;
                        }

                        valorY = valorNegativoAnterior ? (ultimoValorYaux) + valorInteracaoGrafico : (ultimoValorYaux) - valorInteracaoGrafico;

                        if (h.Equals(0))
                        {
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorYaux);
                            Console.WriteLine("DMF " + indexSeries + " X " + ultimoValorXaux + " Y " + ultimoValorYaux);

                            if (valorEsfInternoNO == 0)
                                continue;
                            else
                            {
                                //BARRA INICIAL
                                chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                                Console.WriteLine("DMF " + indexSeries + " X " + ultimoValorXaux + " Y " + valorY);
                            }
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            //valorY = valorNegativoAnterior ? -valorY : valorY;

                            if (valorEsfInternoNO == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                                Console.WriteLine("DMF " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                                continue;
                            }

                            //BARRA MAIOR
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorYaux);
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DMF " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorYaux);

                            //BARRA FECHAMENTO
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DMF " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                        }
                    }
                }
            }
        }
        public void defineDMT(Chart chart, List<double[]> extremidadesDMT)
        {
            for (int i = 1; i <= elementosGraficoBase.Count; i++)
            {
                string indexSeries = addElementoGrafico(4, chart, Color.SteelBlue);
                int indexVetor = i - 1;
                int ultimoPoint = chart.Series["Series" + indexVetor].Points.Count - 1;
                double ultimoValorY = 0;
                double ultimoValorX = 0;
                bool valorNegativo = false;
                bool valorNegativoAnterior = false;
                double angulo = elementosGraficoBase[indexVetor][1];

                double valorInteracaoGrafico = 1;
                string legendaValor = string.Empty;

                if (angulo.Equals(0))
                {
                    ultimoValorX = extremidadesDMT[indexVetor][0];
                    ultimoValorY = extremidadesDMT[indexVetor][1];

                    for (int h = 0; h < (extremidadesDMT[indexVetor].Length - 1); h++)
                    {
                        double valorEsfInternoNO = Math.Round(elementosGraficoDMT[indexVetor][h], 3);
                        legendaValor = valorEsfInternoNO.ToString("F2");
                        chart.Series[indexSeries].LegendText += legendaValor + " | ";

                        double ultimoValorXaux = (ultimoValorX) - 1;
                        double valorY = 0;
                        //= valorNegativoAnterior && valorEsfInternoNO > 0 ? (ultimoValorY) - valorInteracaoGrafico : (ultimoValorY) + valorInteracaoGrafico;

                        if (valorEsfInternoNO < 0)
                        {
                            valorNegativo = true;
                            valorNegativoAnterior = true;
                        }
                        else valorNegativo = false;

                        //if (!dec) valorY = valorNegativoAnterior ? (ultimoValorY) + valorInteracaoGrafico : (ultimoValorY) - valorInteracaoGrafico;

                        if (h.Equals(0))
                        {
                            valorY = valorNegativoAnterior ? (ultimoValorY) - valorInteracaoGrafico : (ultimoValorY) + valorInteracaoGrafico;
                            //INICIO DA BARRA INICIAL
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorY);
                            Console.WriteLine("DMT " + indexSeries + " X " + ultimoValorXaux + " Y " + ultimoValorY);

                            if (valorEsfInternoNO == 0) // extremidadesDEC[indexVetor][h] == 0 || 
                                continue;
                            else
                            {
                                if (valorNegativoAnterior == true && valorNegativo == false) valorY = -valorY;

                                //BARRA INICIAL
                                chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                                Console.WriteLine("DMT " + indexSeries + " X " + ultimoValorXaux + " Y " + valorY);
                            }
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            //valorY = Math.Abs(valorY);

                            if (valorEsfInternoNO > 0) valorY = valorNegativoAnterior ? (ultimoValorY) - valorInteracaoGrafico : (ultimoValorY) + valorInteracaoGrafico;
                            else if (valorEsfInternoNO < 0) valorY = valorNegativoAnterior ? (ultimoValorY) + valorInteracaoGrafico : (ultimoValorY) - valorInteracaoGrafico;

                            if (valorEsfInternoNO == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                                Console.WriteLine("DMT " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                                continue;
                            }

                            //BARRA MAIOR
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, valorY);
                            //chart.Series[indexSeries].Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DMT " + indexSeries + " X " + valorY + " Y " + ultimoValorX);

                            //BARRA FECHAMENTO
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DMT " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                        }
                    }
                }
                else if (angulo.Equals(90) || angulo.Equals(-90))
                {
                    ultimoValorX = extremidadesDMT[indexVetor][0];
                    ultimoValorY = extremidadesDMT[indexVetor][1];
                    double valorY = 0;

                    for (int h = 0; h < (extremidadesDMT[indexVetor].Length - 1); h++)
                    {
                        //TODO VALOR DO 2º NÓ DO ELEMENTO SERÁ NEGATIVO

                        double valorEsfInternoNO = Math.Round(elementosGraficoDMT[indexVetor][h], 3);
                        legendaValor = Math.Round(valorEsfInternoNO).ToString("F2");
                        chart.Series[indexSeries].LegendText += legendaValor + " - ";

                        double ultimoValorYaux = angulo.Equals(-90) ? (ultimoValorY) + 1 : (ultimoValorY) - 1;
                        if (h > 0) ultimoValorYaux = angulo.Equals(-90) ? (valorY) - 1 : (valorY) + 1;
                        valorY = valorNegativoAnterior ? (ultimoValorYaux) - valorInteracaoGrafico : (ultimoValorYaux) + valorInteracaoGrafico;

                        if (valorNegativoAnterior == true && valorNegativo == false && valorY > 0) valorY = -(valorY);
                        else if (valorNegativoAnterior == false && valorNegativo == true && valorY < 0) valorY = Math.Abs(valorY);

                        if (valorEsfInternoNO < 0)
                        {
                            valorNegativo = true;
                            valorNegativoAnterior = true;
                        }
                        else valorNegativo = false;
                        //if (!dec) valorY = valorNegativoAnterior ? (ultimoValorY) + valorInteracaoGrafico : (ultimoValorY) - valorInteracaoGrafico;

                        double ultimoValorXaux = (ultimoValorX) - 1;

                        if (h.Equals(0))
                        {
                            chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorYaux);
                            Console.WriteLine("DMT " + indexSeries + " X " + ultimoValorXaux + " Y " + ultimoValorYaux);

                            if (valorEsfInternoNO == 0)
                                continue;
                            else
                            {
                                //BARRA INICIAL
                                chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, valorY);
                                Console.WriteLine("DMT " + indexSeries + " X " + ultimoValorXaux + " Y " + valorY);
                            }
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                        }
                        else
                        {
                            //valorY = valorNegativoAnterior ? -valorY : valorY;

                            if (valorEsfInternoNO == 0)
                            {
                                chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                                chart.Series[indexSeries].Points.Last().Label = legendaValor;
                                chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                                Console.WriteLine("DMT " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                                continue;
                            }

                            //BARRA MAIOR
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorYaux);//(ultimoValorY - valorInteracaoGrafico)
                            //else chart.Series[indexSeries].Points.AddXY(ultimoValorXaux, ultimoValorY);
                            //chart.Series[indexSeries].Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().Label = legendaValor;
                            chart.Series[indexSeries].Points.Last().LegendText += legendaValor + " | ";
                            Console.WriteLine("DMT " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorYaux);

                            //BARRA FECHAMENTO
                            chart.Series[indexSeries].Points.AddXY(ultimoValorX, ultimoValorY);
                            Console.WriteLine("DMT " + indexSeries + " X " + ultimoValorX + " Y " + ultimoValorY);
                        }
                    }
                }
            }
        }

        private void deslocamentosEGirosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogVetores log = new FormLogVetores(vetoresDeslocGiroGlobalElem, "DESLOCAMENTOS E GIROS");
            log.Show();
        }

        private void esforçosInternosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogVetores log = new FormLogVetores(vetoresEsforcosInternosElem, "ESFORÇOS INTERNOS");
            log.Show();
        }
    }
}
