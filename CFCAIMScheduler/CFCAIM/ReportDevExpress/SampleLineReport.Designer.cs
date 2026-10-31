namespace CFCAIM.ReportDevExpress
{
	partial class SampleLineReport
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

		#region Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
			this.Detail = new DevExpress.XtraReports.UI.DetailBand();
			this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
			this.xrLblDate = new DevExpress.XtraReports.UI.XRLabel();
			this.xrPageInfoDate = new DevExpress.XtraReports.UI.XRPageInfo();
			this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrlblEndDate = new DevExpress.XtraReports.UI.XRLabel();
			this.xrlblStartDate = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrlblIngredientName = new DevExpress.XtraReports.UI.XRLabel();
			this.xrlblUserName = new DevExpress.XtraReports.UI.XRLabel();
			this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
			((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// Detail
			// 
			this.Detail.Dpi = 100F;
			this.Detail.HeightF = 0F;
			this.Detail.Name = "Detail";
			this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// TopMargin
			// 
			this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLblDate,
            this.xrPageInfoDate,
            this.xrLabel6,
            this.xrLabel2,
            this.xrlblEndDate,
            this.xrlblStartDate,
            this.xrLabel3,
            this.xrLabel4,
            this.xrlblIngredientName,
            this.xrlblUserName,
            this.xrChart1});
			this.TopMargin.Dpi = 100F;
			this.TopMargin.HeightF = 591.6667F;
			this.TopMargin.Name = "TopMargin";
			this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// xrLblDate
			// 
			this.xrLblDate.Dpi = 100F;
			this.xrLblDate.LocationFloat = new DevExpress.Utils.PointFloat(493.625F, 34.41661F);
			this.xrLblDate.Name = "xrLblDate";
			this.xrLblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLblDate.SizeF = new System.Drawing.SizeF(53.12506F, 23F);
			this.xrLblDate.StylePriority.UseTextAlignment = false;
			this.xrLblDate.Text = "Date";
			this.xrLblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			// 
			// xrPageInfoDate
			// 
			this.xrPageInfoDate.Dpi = 100F;
			this.xrPageInfoDate.Format = "{0:M/d/yy h:mm tt}";
			this.xrPageInfoDate.LocationFloat = new DevExpress.Utils.PointFloat(546.75F, 34.41666F);
			this.xrPageInfoDate.Name = "xrPageInfoDate";
			this.xrPageInfoDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrPageInfoDate.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
			this.xrPageInfoDate.SizeF = new System.Drawing.SizeF(111.0416F, 23F);
			this.xrPageInfoDate.StylePriority.UseTextAlignment = false;
			this.xrPageInfoDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// xrLabel6
			// 
			this.xrLabel6.Dpi = 100F;
			this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(33.25002F, 34.41666F);
			this.xrLabel6.Name = "xrLabel6";
			this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel6.SizeF = new System.Drawing.SizeF(137.5F, 24.16667F);
			this.xrLabel6.StylePriority.UseTextAlignment = false;
			this.xrLabel6.Text = "Start Date";
			this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			// 
			// xrLabel2
			// 
			this.xrLabel2.Dpi = 100F;
			this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(262.2501F, 34.41666F);
			this.xrLabel2.Name = "xrLabel2";
			this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel2.SizeF = new System.Drawing.SizeF(81.66672F, 24.16667F);
			this.xrLabel2.StylePriority.UseTextAlignment = false;
			this.xrLabel2.Text = "End Date";
			this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			// 
			// xrlblEndDate
			// 
			this.xrlblEndDate.Dpi = 100F;
			this.xrlblEndDate.LocationFloat = new DevExpress.Utils.PointFloat(348.9169F, 34.41666F);
			this.xrlblEndDate.Name = "xrlblEndDate";
			this.xrlblEndDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrlblEndDate.SizeF = new System.Drawing.SizeF(95.49966F, 24.16667F);
			this.xrlblEndDate.StylePriority.UseTextAlignment = false;
			this.xrlblEndDate.Text = "User Name";
			this.xrlblEndDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// xrlblStartDate
			// 
			this.xrlblStartDate.Dpi = 100F;
			this.xrlblStartDate.LocationFloat = new DevExpress.Utils.PointFloat(176.9167F, 34.41666F);
			this.xrlblStartDate.Name = "xrlblStartDate";
			this.xrlblStartDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrlblStartDate.SizeF = new System.Drawing.SizeF(79.16666F, 24.16667F);
			this.xrlblStartDate.StylePriority.UseTextAlignment = false;
			this.xrlblStartDate.Text = "Ingredient Name";
			this.xrlblStartDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// xrLabel3
			// 
			this.xrLabel3.Dpi = 100F;
			this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(32.24996F, 6.583354F);
			this.xrLabel3.Name = "xrLabel3";
			this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel3.SizeF = new System.Drawing.SizeF(137.5F, 24.16667F);
			this.xrLabel3.StylePriority.UseTextAlignment = false;
			this.xrLabel3.Text = "Ingredient Name";
			this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			// 
			// xrLabel4
			// 
			this.xrLabel4.Dpi = 100F;
			this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(460.2499F, 6.416677F);
			this.xrLabel4.Name = "xrLabel4";
			this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel4.SizeF = new System.Drawing.SizeF(81.66672F, 24.16667F);
			this.xrLabel4.StylePriority.UseTextAlignment = false;
			this.xrLabel4.Text = "User Name";
			this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			// 
			// xrlblIngredientName
			// 
			this.xrlblIngredientName.Dpi = 100F;
			this.xrlblIngredientName.LocationFloat = new DevExpress.Utils.PointFloat(176.9166F, 6.416677F);
			this.xrlblIngredientName.Name = "xrlblIngredientName";
			this.xrlblIngredientName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrlblIngredientName.SizeF = new System.Drawing.SizeF(267.5F, 24.16667F);
			this.xrlblIngredientName.StylePriority.UseTextAlignment = false;
			this.xrlblIngredientName.Text = "Ingredient Name";
			this.xrlblIngredientName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// xrlblUserName
			// 
			this.xrlblUserName.Dpi = 100F;
			this.xrlblUserName.LocationFloat = new DevExpress.Utils.PointFloat(546.75F, 6.416677F);
			this.xrlblUserName.Name = "xrlblUserName";
			this.xrlblUserName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrlblUserName.SizeF = new System.Drawing.SizeF(167.6667F, 24.16667F);
			this.xrlblUserName.StylePriority.UseTextAlignment = false;
			this.xrlblUserName.Text = "User Name";
			this.xrlblUserName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// xrChart1
			// 
			this.xrChart1.BorderColor = System.Drawing.Color.Black;
			this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.xrChart1.DataSource = this.bindingSource1;
			xyDiagram1.AxisX.Label.Angle = 45;
			xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
			xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
			xyDiagram1.AxisY.WholeRange.AlwaysShowZeroLevel = false;
			xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.False;
			xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.False;
			xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.False;
			xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.False;
			this.xrChart1.Diagram = xyDiagram1;
			this.xrChart1.Dpi = 100F;
			this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(10.00002F, 68.33341F);
			this.xrChart1.Name = "xrChart1";
			this.xrChart1.SeriesDataMember = "ValueName";
			this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
			this.xrChart1.SeriesTemplate.ArgumentDataMember = "SampleID";
			this.xrChart1.SeriesTemplate.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
			this.xrChart1.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
			this.xrChart1.SeriesTemplate.ValueDataMembersSerializable = "Value";
			this.xrChart1.SeriesTemplate.View = lineSeriesView1;
			this.xrChart1.SizeF = new System.Drawing.SizeF(830F, 513.3333F);
			// 
			// bindingSource1
			// 
			this.bindingSource1.DataSource = typeof(CFCAIMCore.Entity.GraphInfo);
			// 
			// BottomMargin
			// 
			this.BottomMargin.Dpi = 100F;
			this.BottomMargin.HeightF = 0F;
			this.BottomMargin.Name = "BottomMargin";
			this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// SampleLineReport
			// 
			this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
			this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.bindingSource1});
			this.DataSource = this.bindingSource1;
			this.Margins = new DevExpress.Drawing.DXMargins(0, 0, 592, 0);
			this.ShowPreviewMarginLines = false;
			this.Version = "15.2";
			this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.SampleLineReport_BeforePrint);
			((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.XtraReports.UI.DetailBand Detail;
		private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
		private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
		private DevExpress.XtraReports.UI.XRChart xrChart1;
		private System.Windows.Forms.BindingSource bindingSource1;
		private DevExpress.XtraReports.UI.XRLabel xrLblDate;
		private DevExpress.XtraReports.UI.XRPageInfo xrPageInfoDate;
		private DevExpress.XtraReports.UI.XRLabel xrLabel6;
		private DevExpress.XtraReports.UI.XRLabel xrLabel2;
		private DevExpress.XtraReports.UI.XRLabel xrlblEndDate;
		private DevExpress.XtraReports.UI.XRLabel xrlblStartDate;
		private DevExpress.XtraReports.UI.XRLabel xrLabel3;
		private DevExpress.XtraReports.UI.XRLabel xrLabel4;
		private DevExpress.XtraReports.UI.XRLabel xrlblIngredientName;
		private DevExpress.XtraReports.UI.XRLabel xrlblUserName;
	}
}
