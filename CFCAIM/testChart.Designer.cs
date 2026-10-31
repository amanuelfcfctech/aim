namespace CFCAIM
{
    partial class testChart
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            this.chartGraphControlBySampleCode = new DevExpress.XtraCharts.ChartControl();
            this.graphInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnGetChartData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartGraphControlBySampleCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chartGraphControlBySampleCode
            // 
            this.chartGraphControlBySampleCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            xyDiagram1.AxisX.Title.Text = "Sample Code";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            xyDiagram1.AxisY.Title.Text = "Result Value";
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            this.chartGraphControlBySampleCode.Diagram = xyDiagram1;
            this.chartGraphControlBySampleCode.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.LeftOutside;
            this.chartGraphControlBySampleCode.Legend.MarkerSize = new System.Drawing.Size(25, 25);
            this.chartGraphControlBySampleCode.Location = new System.Drawing.Point(3, 207);
            this.chartGraphControlBySampleCode.Name = "chartGraphControlBySampleCode";
            this.chartGraphControlBySampleCode.SeriesDataMember = "ValueName";
            this.chartGraphControlBySampleCode.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartGraphControlBySampleCode.SeriesTemplate.ArgumentDataMember = "SampleID";
            this.chartGraphControlBySampleCode.SeriesTemplate.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            this.chartGraphControlBySampleCode.SeriesTemplate.CrosshairHighlightPoints = DevExpress.Utils.DefaultBoolean.True;
            this.chartGraphControlBySampleCode.SeriesTemplate.CrosshairLabelPattern = "{S} Result: {V}";
            this.chartGraphControlBySampleCode.SeriesTemplate.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartGraphControlBySampleCode.SeriesTemplate.ValueDataMembersSerializable = "Value";
            lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartGraphControlBySampleCode.SeriesTemplate.View = lineSeriesView1;
            this.chartGraphControlBySampleCode.Size = new System.Drawing.Size(898, 314);
            this.chartGraphControlBySampleCode.TabIndex = 155;
            this.chartGraphControlBySampleCode.CustomDrawCrosshair += new DevExpress.XtraCharts.CustomDrawCrosshairEventHandler(this.chartGraphControlBySampleCode_CustomDrawCrosshair);
            this.chartGraphControlBySampleCode.CustomDrawSeriesPoint += new DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(this.chartGraphControlBySampleCode_CustomDrawSeriesPoint);
            // 
            // graphInfoBindingSource
            // 
            this.graphInfoBindingSource.DataSource = typeof(CFCAIMCore.Entity.GraphInfo);
            // 
            // btnGetChartData
            // 
            this.btnGetChartData.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnGetChartData.Location = new System.Drawing.Point(500, 112);
            this.btnGetChartData.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetChartData.Name = "btnGetChartData";
            this.btnGetChartData.Size = new System.Drawing.Size(57, 39);
            this.btnGetChartData.TabIndex = 156;
            this.btnGetChartData.UseVisualStyleBackColor = true;
            this.btnGetChartData.Click += new System.EventHandler(this.btnGetChartData_Click);
            // 
            // testChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 571);
            this.Controls.Add(this.btnGetChartData);
            this.Controls.Add(this.chartGraphControlBySampleCode);
            this.Name = "testChart";
            this.Text = "testChart";
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGraphControlBySampleCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartGraphControlBySampleCode;
        private System.Windows.Forms.BindingSource graphInfoBindingSource;
        private System.Windows.Forms.Button btnGetChartData;
    }
}