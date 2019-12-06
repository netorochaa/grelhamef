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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDEC = new System.Windows.Forms.TabPage();
            this.tabPageDMF = new System.Windows.Forms.TabPage();
            this.tabPageDMT = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageDEC.SuspendLayout();
            this.tabPageDMF.SuspendLayout();
            this.tabPageDMT.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart3
            // 
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MaximumAutoSize = 100F;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.Interval = 1D;
            chartArea1.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.MaximumAutoSize = 100F;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea3";
            this.chart3.ChartAreas.Add(chartArea1);
            this.chart3.Dock = System.Windows.Forms.DockStyle.Left;
            legend1.Name = "Legend1";
            this.chart3.Legends.Add(legend1);
            this.chart3.Location = new System.Drawing.Point(0, 0);
            this.chart3.Name = "chart3";
            this.chart3.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.BorderWidth = 6;
            series1.ChartArea = "ChartArea3";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart3.Series.Add(series1);
            this.chart3.Size = new System.Drawing.Size(477, 772);
            this.chart3.TabIndex = 44;
            this.chart3.Text = "chart3";
            // 
            // chart2
            // 
            chartArea2.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisX.MaximumAutoSize = 100F;
            chartArea2.AxisY.Interval = 1D;
            chartArea2.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MaximumAutoSize = 100F;
            chartArea2.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MinorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea2.BorderColor = System.Drawing.Color.LightGray;
            chartArea2.Name = "ChartArea2";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Left;
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(3, 3);
            this.chart2.Name = "chart2";
            series2.BorderWidth = 6;
            series2.ChartArea = "ChartArea2";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(477, 766);
            this.chart2.TabIndex = 43;
            this.chart2.Text = "chart2";
            // 
            // chart1
            // 
            chartArea3.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea3.AxisX.Interval = 1D;
            chartArea3.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisX.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisX.MaximumAutoSize = 100F;
            chartArea3.AxisX.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisX.MinorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.Interval = 1D;
            chartArea3.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea3.AxisY.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.MaximumAutoSize = 100F;
            chartArea3.AxisY.MinorGrid.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea3.AxisY.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Left;
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series3.BorderWidth = 5;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.IsValueShownAsLabel = true;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series3.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series3.SmartLabelStyle.CalloutStyle = System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.Box;
            series3.SmartLabelStyle.IsMarkerOverlappingAllowed = true;
            series3.SmartLabelStyle.MovingDirection = ((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopLeft | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopRight) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomRight)));
            series3.YValuesPerPoint = 4;
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(477, 766);
            this.chart1.TabIndex = 42;
            this.chart1.Text = "chart1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDEC);
            this.tabControl1.Controls.Add(this.tabPageDMF);
            this.tabControl1.Controls.Add(this.tabPageDMT);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1468, 798);
            this.tabControl1.TabIndex = 45;
            // 
            // tabPageDEC
            // 
            this.tabPageDEC.Controls.Add(this.chart1);
            this.tabPageDEC.Location = new System.Drawing.Point(4, 22);
            this.tabPageDEC.Name = "tabPageDEC";
            this.tabPageDEC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDEC.Size = new System.Drawing.Size(1460, 772);
            this.tabPageDEC.TabIndex = 0;
            this.tabPageDEC.Text = "DEC";
            this.tabPageDEC.UseVisualStyleBackColor = true;
            // 
            // tabPageDMF
            // 
            this.tabPageDMF.Controls.Add(this.chart2);
            this.tabPageDMF.Location = new System.Drawing.Point(4, 22);
            this.tabPageDMF.Name = "tabPageDMF";
            this.tabPageDMF.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDMF.Size = new System.Drawing.Size(1460, 772);
            this.tabPageDMF.TabIndex = 1;
            this.tabPageDMF.Text = "DMF";
            this.tabPageDMF.UseVisualStyleBackColor = true;
            // 
            // tabPageDMT
            // 
            this.tabPageDMT.Controls.Add(this.chart3);
            this.tabPageDMT.Location = new System.Drawing.Point(4, 22);
            this.tabPageDMT.Name = "tabPageDMT";
            this.tabPageDMT.Size = new System.Drawing.Size(1460, 772);
            this.tabPageDMT.TabIndex = 2;
            this.tabPageDMT.Text = "DMT";
            this.tabPageDMT.UseVisualStyleBackColor = true;
            // 
            // FormGrafico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 798);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGrafico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGrafico";
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageDEC.ResumeLayout(false);
            this.tabPageDMF.ResumeLayout(false);
            this.tabPageDMT.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDEC;
        private System.Windows.Forms.TabPage tabPageDMF;
        private System.Windows.Forms.TabPage tabPageDMT;
    }
}