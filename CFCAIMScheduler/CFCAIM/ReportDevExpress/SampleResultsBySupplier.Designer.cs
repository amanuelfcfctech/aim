namespace CFCAIM.ReportDevExpress
{
    partial class SampleResultsBySupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleResultsBySupplier));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.txtCompany = new DevExpress.XtraReports.UI.XRRichText();
            this.xrLblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblPageNo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfoPageNo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfoDate = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLblPrintedBy = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLblPageHeading = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.GroupHeaderSupplier = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooterSupplier = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.GroupHeaderSupplierLoc = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooterSupplierLoc = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.GroupHeaderSampleDate = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooterSampleDate = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.GroupHeaderSampleCode = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooterSampleCode = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel7});
            this.Detail.HeightF = 18.12502F;
            this.Detail.KeepTogether = true;
            this.Detail.KeepTogetherWithDetailReports = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ResultValue", "{0:#.00}")});
            this.xrLabel8.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(661.0413F, 0F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(88.95862F, 17F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "xrLabel8";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalyzerNutrientName")});
            this.xrLabel7.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(469.3748F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(191.6665F, 17F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 37.5F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel25,
            this.txtCompany,
            this.xrLblDate,
            this.xrLblPageNo,
            this.xrPageInfoPageNo,
            this.xrPageInfoDate,
            this.xrLblPrintedBy,
            this.xrPageInfo1,
            this.xrLblPageHeading});
            this.PageHeader.HeightF = 87.49997F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel25
            // 
            this.xrLabel25.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(575.2087F, 48.99998F);
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.Text = "Company:";
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtCompany
            // 
            this.txtCompany.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.txtCompany.LocationFloat = new DevExpress.Utils.PointFloat(638.9586F, 48.49997F);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.SerializableRtfString = resources.GetString("txtCompany.SerializableRtfString");
            this.txtCompany.SizeF = new System.Drawing.SizeF(111.0414F, 16.66667F);
            this.txtCompany.StylePriority.UseFont = false;
            // 
            // xrLblDate
            // 
            this.xrLblDate.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLblDate.LocationFloat = new DevExpress.Utils.PointFloat(575.2087F, 16.16664F);
            this.xrLblDate.Name = "xrLblDate";
            this.xrLblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblDate.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLblDate.StylePriority.UseFont = false;
            this.xrLblDate.StylePriority.UseTextAlignment = false;
            this.xrLblDate.Text = "Date:";
            this.xrLblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLblPageNo
            // 
            this.xrLblPageNo.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLblPageNo.LocationFloat = new DevExpress.Utils.PointFloat(575.2087F, 0F);
            this.xrLblPageNo.Name = "xrLblPageNo";
            this.xrLblPageNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblPageNo.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLblPageNo.StylePriority.UseFont = false;
            this.xrLblPageNo.StylePriority.UseTextAlignment = false;
            this.xrLblPageNo.Text = "Page No:";
            this.xrLblPageNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfoPageNo
            // 
            this.xrPageInfoPageNo.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfoPageNo.LocationFloat = new DevExpress.Utils.PointFloat(638.9584F, 0F);
            this.xrPageInfoPageNo.Name = "xrPageInfoPageNo";
            this.xrPageInfoPageNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfoPageNo.SizeF = new System.Drawing.SizeF(111.0416F, 16.16665F);
            this.xrPageInfoPageNo.StylePriority.UseFont = false;
            this.xrPageInfoPageNo.StylePriority.UseTextAlignment = false;
            this.xrPageInfoPageNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfoDate
            // 
            this.xrPageInfoDate.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfoDate.LocationFloat = new DevExpress.Utils.PointFloat(638.9584F, 16.16664F);
            this.xrPageInfoDate.Name = "xrPageInfoDate";
            this.xrPageInfoDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfoDate.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfoDate.SizeF = new System.Drawing.SizeF(111.0416F, 16.16665F);
            this.xrPageInfoDate.StylePriority.UseFont = false;
            this.xrPageInfoDate.StylePriority.UseTextAlignment = false;
            this.xrPageInfoDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrPageInfoDate.TextFormatString = "{0:M/d/yy h:mm tt}";
            // 
            // xrLblPrintedBy
            // 
            this.xrLblPrintedBy.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLblPrintedBy.LocationFloat = new DevExpress.Utils.PointFloat(575.2087F, 32.33329F);
            this.xrLblPrintedBy.Name = "xrLblPrintedBy";
            this.xrLblPrintedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblPrintedBy.SizeF = new System.Drawing.SizeF(63.74976F, 16.16665F);
            this.xrLblPrintedBy.StylePriority.UseFont = false;
            this.xrLblPrintedBy.StylePriority.UseTextAlignment = false;
            this.xrLblPrintedBy.Text = "Printed By:";
            this.xrLblPrintedBy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new DevExpress.Drawing.DXFont("Arial", 7.8F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(638.9586F, 32.33331F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.UserName;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(111.0414F, 16.16665F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            // 
            // xrLblPageHeading
            // 
            this.xrLblPageHeading.Font = new DevExpress.Drawing.DXFont("Arial", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLblPageHeading.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLblPageHeading.Name = "xrLblPageHeading";
            this.xrLblPageHeading.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLblPageHeading.SizeF = new System.Drawing.SizeF(427.5F, 23F);
            this.xrLblPageHeading.StylePriority.UseFont = false;
            this.xrLblPageHeading.Text = "Sample Results By Supplier";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(229.1669F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(509.7918F, 17F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Sample Information";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(114.5835F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(114.5834F, 17F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Sample #";
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SupplierLocationName")});
            this.xrLabel6.Font = new DevExpress.Drawing.DXFont("Arial", 14F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(114.5835F, 33.41667F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(635.4164F, 33.41667F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "xrLabel6";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SupplierName")});
            this.xrLabel5.Font = new DevExpress.Drawing.DXFont("Arial", 14F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(114.5835F, 0F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(635.4164F, 33.41667F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "xrLabel5";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SampleCode")});
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(114.5835F, 2F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(114.5834F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Notes")});
            this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(229.1669F, 2F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(509.7918F, 23F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "xrLabel4";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Font = new DevExpress.Drawing.DXFont("Arial", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(114.5834F, 33.41667F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Supplier:";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new DevExpress.Drawing.DXFont("Arial", 14F, DevExpress.Drawing.DXFontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0F, 33.41667F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(114.5834F, 33.41667F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "Location:";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(469.3748F, 45.83333F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(191.6665F, 17F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "Analyzer Nutrient Name";
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(661.0413F, 45.83333F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(88.95874F, 17F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Result";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLine2
            // 
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(740F, 2F);
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(114.5834F, 17F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.Text = "Sample Date";
            // 
            // xrLabel14
            // 
            this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SampleDate", "{0:M/d/yyyy}")});
            this.xrLabel14.Font = new DevExpress.Drawing.DXFont("Arial", 9F);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(114.5834F, 23F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "xrLabel14";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(CFCAIMCore.Entity.SampleResultsBySupplierInfo);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // GroupHeaderSupplier
            // 
            this.GroupHeaderSupplier.BackColor = System.Drawing.Color.AliceBlue;
            this.GroupHeaderSupplier.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabel6,
            this.xrLabel10,
            this.xrLabel5});
            this.GroupHeaderSupplier.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("SupplierName", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeaderSupplier.HeightF = 83.33334F;
            this.GroupHeaderSupplier.Level = 3;
            this.GroupHeaderSupplier.Name = "GroupHeaderSupplier";
            this.GroupHeaderSupplier.StylePriority.UseBackColor = false;
            // 
            // GroupFooterSupplier
            // 
            this.GroupFooterSupplier.Level = 3;
            this.GroupFooterSupplier.Name = "GroupFooterSupplier";
            this.GroupFooterSupplier.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBandExceptLastEntry;
            // 
            // GroupHeaderSupplierLoc
            // 
            this.GroupHeaderSupplierLoc.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel13,
            this.xrLabel3,
            this.xrLabel2});
            this.GroupHeaderSupplierLoc.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("SupplierLocationName", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeaderSupplierLoc.HeightF = 20.83333F;
            this.GroupHeaderSupplierLoc.Level = 2;
            this.GroupHeaderSupplierLoc.Name = "GroupHeaderSupplierLoc";
            this.GroupHeaderSupplierLoc.RepeatEveryPage = true;
            // 
            // GroupFooterSupplierLoc
            // 
            this.GroupFooterSupplierLoc.HeightF = 0F;
            this.GroupFooterSupplierLoc.Level = 2;
            this.GroupFooterSupplierLoc.Name = "GroupFooterSupplierLoc";
            // 
            // GroupHeaderSampleDate
            // 
            this.GroupHeaderSampleDate.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("SampleDate", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeaderSampleDate.HeightF = 0F;
            this.GroupHeaderSampleDate.KeepTogether = true;
            this.GroupHeaderSampleDate.Level = 1;
            this.GroupHeaderSampleDate.Name = "GroupHeaderSampleDate";
            // 
            // GroupFooterSampleDate
            // 
            this.GroupFooterSampleDate.HeightF = 0F;
            this.GroupFooterSampleDate.KeepTogether = true;
            this.GroupFooterSampleDate.Level = 1;
            this.GroupFooterSampleDate.Name = "GroupFooterSampleDate";
            // 
            // GroupHeaderSampleCode
            // 
            this.GroupHeaderSampleCode.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel14,
            this.xrLabel11,
            this.xrLabel4,
            this.xrLabel1,
            this.xrLabel12,
            this.xrLine2});
            this.GroupHeaderSampleCode.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("SampleCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeaderSampleCode.HeightF = 63.66666F;
            this.GroupHeaderSampleCode.KeepTogether = true;
            this.GroupHeaderSampleCode.Name = "GroupHeaderSampleCode";
            // 
            // GroupFooterSampleCode
            // 
            this.GroupFooterSampleCode.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15});
            this.GroupFooterSampleCode.HeightF = 16.66667F;
            this.GroupFooterSampleCode.KeepTogether = true;
            this.GroupFooterSampleCode.Name = "GroupFooterSampleCode";
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(114.5834F, 16.16665F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.Text = "     ";
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrControlStyle2
            // 
            this.xrControlStyle2.Name = "xrControlStyle2";
            this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // SampleResultsBySupplier
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.GroupHeaderSupplier,
            this.GroupFooterSupplier,
            this.GroupHeaderSupplierLoc,
            this.GroupFooterSupplierLoc,
            this.GroupHeaderSampleDate,
            this.GroupFooterSampleDate,
            this.GroupHeaderSampleCode,
            this.GroupFooterSampleCode});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Margins = new DevExpress.Drawing.DXMargins(49, 51, 38, 100);
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.xrControlStyle2});
            this.Version = "20.1";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.SampleResultsBySupplier_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLblPageHeading;
        private DevExpress.XtraReports.UI.XRLabel xrLabel25;
        private DevExpress.XtraReports.UI.XRRichText txtCompany;
        private DevExpress.XtraReports.UI.XRLabel xrLblDate;
        private DevExpress.XtraReports.UI.XRLabel xrLblPageNo;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfoPageNo;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfoDate;
        private DevExpress.XtraReports.UI.XRLabel xrLblPrintedBy;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel14;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderSupplier;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooterSupplier;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderSupplierLoc;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooterSupplierLoc;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderSampleDate;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooterSampleDate;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeaderSampleCode;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooterSampleCode;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle2;
    }
}
