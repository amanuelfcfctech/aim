namespace CFCAIM.ReportDevExpress
{
    partial class SampleActivityReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleActivityReport));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLblPrintedBy = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfoDate = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfoPageNo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLblPageNo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCompany = new DevExpress.XtraReports.UI.XRRichText();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblPageHeading = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.objectDataSource2 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel12,
            this.xrLabel7,
            this.xrLabel15});
            this.Detail.HeightF = 18.83332F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "IngredientName")});
            this.xrLabel5.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(558.0419F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(191.9581F, 18F);
            this.xrLabel5.StylePriority.UseFont = false;
            // 
            // xrLabel12
            // 
            this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ProductName")});
            this.xrLabel12.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(366.0838F, 0F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(191.9581F, 18F);
            this.xrLabel12.StylePriority.UseFont = false;
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "OwnerLocationName")});
            this.xrLabel7.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(164.1666F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(199.1618F, 18F);
            this.xrLabel7.StylePriority.UseFont = false;
            // 
            // xrLabel15
            // 
            this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SampleCode")});
            this.xrLabel15.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(51.66664F, 0F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(112.5F, 18F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(CFCAIMCore.Entity.SampleActivityReportInfo);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 50.58333F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 68.50001F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel6,
            this.xrLabel2,
            this.xrLabel3,
            this.xrLine2,
            this.xrPageInfo1,
            this.xrLblPrintedBy,
            this.xrPageInfoDate,
            this.xrPageInfoPageNo,
            this.xrLblPageNo,
            this.xrLblDate,
            this.txtCompany,
            this.xrLabel25,
            this.xrLblPageHeading});
            this.PageHeader.HeightF = 102.0834F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(558.0419F, 81.83341F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(191.9581F, 16.16665F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.Text = "Ingredient";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(366.0838F, 81.83341F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(191.9581F, 16.16665F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.Text = "Product";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(51.66664F, 81.83341F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(112.5F, 16.16665F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Sample Code";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(164.1666F, 81.83341F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(199.1618F, 16.16665F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "Owner Location";
            // 
            // xrLine2
            // 
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 98.00005F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(750F, 2F);
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(626.9585F, 34.41665F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.UserName;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(111.0414F, 16.16665F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            // 
            // xrLblPrintedBy
            // 
            this.xrLblPrintedBy.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLblPrintedBy.LocationFloat = new DevExpress.Utils.PointFloat(563.2085F, 34.41663F);
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
            this.xrPageInfoDate.LocationFloat = new DevExpress.Utils.PointFloat(626.9582F, 18.24999F);
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
            this.xrPageInfoPageNo.LocationFloat = new DevExpress.Utils.PointFloat(626.9582F, 2.083333F);
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
            this.xrLblPageNo.LocationFloat = new DevExpress.Utils.PointFloat(563.2085F, 2.083333F);
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
            this.xrLblDate.LocationFloat = new DevExpress.Utils.PointFloat(563.2085F, 18.24997F);
            this.xrLblDate.Name = "xrLblDate";
            this.xrLblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblDate.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLblDate.StylePriority.UseFont = false;
            this.xrLblDate.StylePriority.UseTextAlignment = false;
            this.xrLblDate.Text = "Date:";
            this.xrLblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtCompany
            // 
            this.txtCompany.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.txtCompany.LocationFloat = new DevExpress.Utils.PointFloat(626.9585F, 50.5833F);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.SerializableRtfString = resources.GetString("txtCompany.SerializableRtfString");
            this.txtCompany.SizeF = new System.Drawing.SizeF(108.0417F, 16.66667F);
            this.txtCompany.StylePriority.UseFont = false;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(563.2085F, 51.08331F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "Company:";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLblPageHeading
            // 
            this.xrLblPageHeading.Font = new DevExpress.Drawing.DXFont("Arial", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLblPageHeading.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLblPageHeading.Name = "xrLblPageHeading";
            this.xrLblPageHeading.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblPageHeading.SizeF = new System.Drawing.SizeF(427.5F, 23F);
            this.xrLblPageHeading.StylePriority.UseFont = false;
            this.xrLblPageHeading.Text = "Sample Activity Report";
            // 
            // xrLabel10
            // 
            this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CreatedStampGroup")});
            this.xrLabel10.Font = new DevExpress.Drawing.DXFont("Arial", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(363.3285F, 30.62499F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CreatedStampGroup", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending)});
            this.GroupHeader1.HeightF = 40.625F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1});
            this.GroupFooter1.HeightF = 18.75F;
            this.GroupFooter1.Level = 1;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrLine1
            // 
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(750F, 2F);
            // 
            // objectDataSource2
            // 
            this.objectDataSource2.DataSource = typeof(CFCAIMCore.Entity.SampleActivityReportInfo);
            this.objectDataSource2.Name = "objectDataSource2";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(CFCAIMCore.Entity.SampleComparisonReportInfo);
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CreatedStamp", DevExpress.XtraReports.UI.XRColumnSortOrder.Descending)});
            this.GroupHeader2.HeightF = 1.041667F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // PageFooter
            // 
            this.PageFooter.HeightF = 23.95833F;
            this.PageFooter.Name = "PageFooter";
            // 
            // SampleActivityReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.GroupHeader1,
            this.GroupFooter1,
            this.GroupHeader2,
            this.PageFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource2,
            this.objectDataSource1});
            this.DataSource = this.objectDataSource2;
            this.Margins = new DevExpress.Drawing.DXMargins(50, 50, 51, 69);
            this.Version = "20.1";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.SampleActivityReport_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLblPageHeading;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLblPrintedBy;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfoDate;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfoPageNo;
        private DevExpress.XtraReports.UI.XRLabel xrLblPageNo;
        private DevExpress.XtraReports.UI.XRLabel xrLblDate;
        private DevExpress.XtraReports.UI.XRRichText txtCompany;
        private DevExpress.XtraReports.UI.XRLabel xrLabel25;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    }
}
