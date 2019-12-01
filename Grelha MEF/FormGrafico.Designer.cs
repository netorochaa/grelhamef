namespace Grelha_MEF
{
    partial class FormGrafico
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart3
            // 
            chartArea7.AxisX.Interval = 1D;
            chartArea7.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea7.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea7.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea7.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea7.AxisX.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea7.AxisX.MaximumAutoSize = 100F;
            chartArea7.AxisX.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea7.AxisX.MinorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea7.AxisY.Interval = 1D;
            chartArea7.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea7.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea7.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea7.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea7.AxisY.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea7.AxisY.MaximumAutoSize = 100F;
            chartArea7.AxisY.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea7.AxisY.MinorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea7.Name = "ChartArea3";
            this.chart3.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.chart3.Legends.Add(legend7);
            this.chart3.Location = new System.Drawing.Point(978, 12);
            this.chart3.Name = "chart3";
            this.chart3.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series7.BorderWidth = 6;
            series7.ChartArea = "ChartArea3";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.chart3.Series.Add(series7);
            this.chart3.Size = new System.Drawing.Size(477, 300);
            this.chart3.TabIndex = 44;
            this.chart3.Text = "chart3";
            // 
            // chart2
            // 
            chartArea8.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea8.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea8.AxisX.MaximumAutoSize = 100F;
            chartArea8.AxisY.Interval = 1D;
            chartArea8.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea8.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea8.AxisY.MajorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea8.AxisY.MaximumAutoSize = 100F;
            chartArea8.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea8.AxisY.MinorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea8.BorderColor = System.Drawing.Color.LightGray;
            chartArea8.Name = "ChartArea2";
            this.chart2.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            this.chart2.Legends.Add(legend8);
            this.chart2.Location = new System.Drawing.Point(495, 12);
            this.chart2.Name = "chart2";
            series8.BorderWidth = 6;
            series8.ChartArea = "ChartArea2";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            this.chart2.Series.Add(series8);
            this.chart2.Size = new System.Drawing.Size(477, 300);
            this.chart2.TabIndex = 43;
            this.chart2.Text = "chart2";
            // 
            // chart1
            // 
            chartArea9.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea9.AxisX.Interval = 1D;
            chartArea9.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea9.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea9.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea9.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea9.AxisX.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea9.AxisX.MaximumAutoSize = 100F;
            chartArea9.AxisX.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea9.AxisX.MinorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea9.AxisY.Interval = 1D;
            chartArea9.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea9.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea9.AxisY.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea9.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea9.AxisY.MaximumAutoSize = 100F;
            chartArea9.AxisY.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea9.AxisY.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            chartArea9.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea9);
            legend9.Name = "Legend1";
            this.chart1.Legends.Add(legend9);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series9.BorderWidth = 5;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series9.IsValueShownAsLabel = true;
            series9.Legend = "Legend1";
            series9.Name = "Series1";
            series9.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series9.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box;
            series9.SmartLabelStyle.IsMarkerOverlappingAllowed = true;
            series9.SmartLabelStyle.MovingDirection = ((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopLeft | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopRight) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomRight)));
            series9.YValuesPerPoint = 4;
            this.chart1.Series.Add(series9);
            this.chart1.Size = new System.Drawing.Size(477, 300);
            this.chart1.TabIndex = 42;
            this.chart1.Text = "chart1";
            // 
            // FormGrafico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 339);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Name = "FormGrafico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGrafico";
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}