namespace CFCAIM.ReportDevExpress
{
    partial class SampleAnalysisRequestFormEvonik
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
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLblPrintedBy = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfoDate = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfoPageNo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLblPageNo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrIngredientProductLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPanelComment3 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrComment3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPanelComment2 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrComment2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPanelComment1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrComment1 = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.objectDataSource2 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCompany = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 37F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 30F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel15,
            this.xrLabel13});
            this.Detail.HeightF = 20.83333F;
            this.Detail.Name = "Detail";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(386.6253F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(145.5415F, 18F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "NIR";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SampleCode]")});
            this.xrLabel15.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(532.1667F, 0F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(153.7917F, 18F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel13
            // 
            this.xrLabel13.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[AIMMaterialName]")});
            this.xrLabel13.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(71.62527F, 0F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(315F, 18F);
            this.xrLabel13.StylePriority.UseFont = false;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(685.9586F, 34.66667F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.UserName;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(111.0414F, 16.16665F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            // 
            // xrLblPrintedBy
            // 
            this.xrLblPrintedBy.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLblPrintedBy.LocationFloat = new DevExpress.Utils.PointFloat(622.2087F, 34.16663F);
            this.xrLblPrintedBy.Name = "xrLblPrintedBy";
            this.xrLblPrintedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblPrintedBy.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLblPrintedBy.StylePriority.UseFont = false;
            this.xrLblPrintedBy.StylePriority.UseTextAlignment = false;
            this.xrLblPrintedBy.Text = "Printed By:";
            this.xrLblPrintedBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfoDate
            // 
            this.xrPageInfoDate.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfoDate.LocationFloat = new DevExpress.Utils.PointFloat(685.9584F, 16.16665F);
            this.xrPageInfoDate.Name = "xrPageInfoDate";
            this.xrPageInfoDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfoDate.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfoDate.SizeF = new System.Drawing.SizeF(111.0416F, 16.16665F);
            this.xrPageInfoDate.StylePriority.UseFont = false;
            this.xrPageInfoDate.StylePriority.UseTextAlignment = false;
            this.xrPageInfoDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrPageInfoDate.TextFormatString = "{0:M/d/yy h:mm tt}";
            // 
            // xrPageInfoPageNo
            // 
            this.xrPageInfoPageNo.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfoPageNo.LocationFloat = new DevExpress.Utils.PointFloat(685.9584F, 0F);
            this.xrPageInfoPageNo.Name = "xrPageInfoPageNo";
            this.xrPageInfoPageNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfoPageNo.SizeF = new System.Drawing.SizeF(111.0416F, 16.16665F);
            this.xrPageInfoPageNo.StylePriority.UseFont = false;
            this.xrPageInfoPageNo.StylePriority.UseTextAlignment = false;
            this.xrPageInfoPageNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLblPageNo
            // 
            this.xrLblPageNo.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLblPageNo.LocationFloat = new DevExpress.Utils.PointFloat(622.2087F, 0F);
            this.xrLblPageNo.Name = "xrLblPageNo";
            this.xrLblPageNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblPageNo.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLblPageNo.StylePriority.UseFont = false;
            this.xrLblPageNo.StylePriority.UseTextAlignment = false;
            this.xrLblPageNo.Text = "Page No:";
            this.xrLblPageNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLblDate
            // 
            this.xrLblDate.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLblDate.LocationFloat = new DevExpress.Utils.PointFloat(622.2087F, 16.16665F);
            this.xrLblDate.Name = "xrLblDate";
            this.xrLblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblDate.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLblDate.StylePriority.UseFont = false;
            this.xrLblDate.StylePriority.UseTextAlignment = false;
            this.xrLblDate.Text = "Date:";
            this.xrLblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(622.2087F, 50.8333F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "Company:";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrIngredientProductLabel
            // 
            this.xrIngredientProductLabel.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrIngredientProductLabel.LocationFloat = new DevExpress.Utils.PointFloat(71.62527F, 0F);
            this.xrIngredientProductLabel.Name = "xrIngredientProductLabel";
            this.xrIngredientProductLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrIngredientProductLabel.SizeF = new System.Drawing.SizeF(315F, 16.08331F);
            this.xrIngredientProductLabel.StylePriority.UseFont = false;
            this.xrIngredientProductLabel.StylePriority.UseTextAlignment = false;
            this.xrIngredientProductLabel.Text = "Ingredient Name";
            this.xrIngredientProductLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(532.1667F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(153.7917F, 16.08331F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Sample Code";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel7,
            this.xrLabel4,
            this.xrIngredientProductLabel});
            this.GroupHeader1.HeightF = 21.74988F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(386.6253F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(145.5415F, 16.08331F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Type of Analysis";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0F, 17.66655F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(790F, 4.08333F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "_________________________________________________________________________________" +
    "________________________________";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanelComment3,
            this.xrPanelComment2,
            this.xrPanelComment1});
            this.PageHeader.HeightF = 129.667F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // xrPanelComment3
            // 
            this.xrPanelComment3.CanShrink = true;
            this.xrPanelComment3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrComment3});
            this.xrPanelComment3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 83.33334F);
            this.xrPanelComment3.Name = "xrPanelComment3";
            this.xrPanelComment3.SizeF = new System.Drawing.SizeF(794.0003F, 41.66667F);
            // 
            // xrComment3
            // 
            this.xrComment3.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrComment3.LocationFloat = new DevExpress.Utils.PointFloat(40.33334F, 0F);
            this.xrComment3.Name = "xrComment3";
            this.xrComment3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrComment3.SizeF = new System.Drawing.SizeF(623.1296F, 39.29176F);
            this.xrComment3.StylePriority.UseFont = false;
            // 
            // xrPanelComment2
            // 
            this.xrPanelComment2.CanShrink = true;
            this.xrPanelComment2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrComment2});
            this.xrPanelComment2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 41.66667F);
            this.xrPanelComment2.Name = "xrPanelComment2";
            this.xrPanelComment2.SizeF = new System.Drawing.SizeF(794.0003F, 41.66667F);
            // 
            // xrComment2
            // 
            this.xrComment2.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrComment2.LocationFloat = new DevExpress.Utils.PointFloat(40.33334F, 0F);
            this.xrComment2.Name = "xrComment2";
            this.xrComment2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrComment2.SizeF = new System.Drawing.SizeF(623.1296F, 39.29176F);
            this.xrComment2.StylePriority.UseFont = false;
            // 
            // xrPanelComment1
            // 
            this.xrPanelComment1.CanShrink = true;
            this.xrPanelComment1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrComment1});
            this.xrPanelComment1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanelComment1.Name = "xrPanelComment1";
            this.xrPanelComment1.SizeF = new System.Drawing.SizeF(794.0003F, 41.66667F);
            // 
            // xrComment1
            // 
            this.xrComment1.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrComment1.LocationFloat = new DevExpress.Utils.PointFloat(40.33334F, 0F);
            this.xrComment1.Name = "xrComment1";
            this.xrComment1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrComment1.SizeF = new System.Drawing.SizeF(623.1296F, 39.29176F);
            this.xrComment1.StylePriority.UseFont = false;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(CFCAIMCore.Entity.SampleSubmittedSamplesReportInfo);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // objectDataSource2
            // 
            this.objectDataSource2.DataSource = typeof(CFCAIMCore.Entity.SampleAnalysisRequestFormInfo);
            this.objectDataSource2.Name = "objectDataSource2";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCompany,
            this.xrLabel25,
            this.xrPageInfo1,
            this.xrLblPrintedBy,
            this.xrPageInfoDate,
            this.xrPageInfoPageNo,
            this.xrLblPageNo,
            this.xrLblDate,
            this.xrLabel5});
            this.ReportHeader.HeightF = 75.37487F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new DevExpress.Drawing.DXFont("Arial", 16F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(540.417F, 50.83332F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.Text = "Sample Analysis Request Form - Evonik Degussa";
            // 
            // xrCompany
            // 
            this.xrCompany.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrCompany.LocationFloat = new DevExpress.Utils.PointFloat(685.9586F, 50.8333F);
            this.xrCompany.Name = "xrCompany";
            this.xrCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrCompany.SizeF = new System.Drawing.SizeF(111.0414F, 16.17F);
            this.xrCompany.StylePriority.UseFont = false;
            this.xrCompany.StylePriority.UseTextAlignment = false;
            this.xrCompany.Text = "Company Name";
            this.xrCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // SampleAnalysisRequestFormEvonik
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.GroupHeader1,
            this.PageHeader,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1,
            this.objectDataSource2});
            this.DataSource = this.objectDataSource2;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(28, 25, 37, 30);
            this.ShowPreviewMarginLines = false;
            this.Version = "20.1";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.SampleAnalysisRequestFormEvonik_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource2;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLblPrintedBy;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfoDate;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfoPageNo;
        private DevExpress.XtraReports.UI.XRLabel xrLblPageNo;
        private DevExpress.XtraReports.UI.XRLabel xrLblDate;
        private DevExpress.XtraReports.UI.XRLabel xrLabel25;
        private DevExpress.XtraReports.UI.XRLabel xrIngredientProductLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRPanel xrPanelComment1;
        private DevExpress.XtraReports.UI.XRLabel xrComment1;
        private DevExpress.XtraReports.UI.XRPanel xrPanelComment3;
        private DevExpress.XtraReports.UI.XRLabel xrComment3;
        private DevExpress.XtraReports.UI.XRPanel xrPanelComment2;
        private DevExpress.XtraReports.UI.XRLabel xrComment2;
        private DevExpress.XtraReports.UI.XRLabel xrCompany;
    }
}
