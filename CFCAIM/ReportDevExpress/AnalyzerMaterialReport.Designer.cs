namespace CFCAIM.ReportDevExpress
{
    partial class AnalyzerMaterialReport
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
			this.Detail = new DevExpress.XtraReports.UI.DetailBand();
			this.xrTableAnalyzerMaterial = new DevExpress.XtraReports.UI.XRTable();
			this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
			this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
			this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
			this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
			this.xrLblAnalyzerMaterialCode = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLblAnalyzerMaterialName = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLblIsActive = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
			this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
			this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLblPrintedBy = new DevExpress.XtraReports.UI.XRLabel();
			this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
			this.xrLblPageNo = new DevExpress.XtraReports.UI.XRLabel();
			this.xrPageInfoPageNo = new DevExpress.XtraReports.UI.XRPageInfo();
			this.xrLblDate = new DevExpress.XtraReports.UI.XRLabel();
			this.xrPageInfoDate = new DevExpress.XtraReports.UI.XRPageInfo();
			this.xrLblPageHeading = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
			this.DevReportStyleSheet = new DevExpress.XtraReports.UI.XRControlStyle();
			this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.DevReportEvenRowStyle = new DevExpress.XtraReports.UI.XRControlStyle();
			((System.ComponentModel.ISupportInitialize)(this.xrTableAnalyzerMaterial)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// Detail
			// 
			this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableAnalyzerMaterial});
			this.Detail.EvenStyleName = "DevReportEvenRowStyle";
			this.Detail.HeightF = 25F;
			this.Detail.Name = "Detail";
			this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.Detail.StyleName = "DevReportStyleSheet";
			this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// xrTableAnalyzerMaterial
			// 
			this.xrTableAnalyzerMaterial.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
			this.xrTableAnalyzerMaterial.Name = "xrTableAnalyzerMaterial";
			this.xrTableAnalyzerMaterial.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
			this.xrTableAnalyzerMaterial.SizeF = new System.Drawing.SizeF(750F, 25F);
			// 
			// xrTableRow1
			// 
			this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell3});
			this.xrTableRow1.Name = "xrTableRow1";
			this.xrTableRow1.Weight = 11.5D;
			// 
			// xrTableCell2
			// 
			this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalyzerMaterialCode")});
			this.xrTableCell2.Name = "xrTableCell2";
			this.xrTableCell2.Text = "xrTableCell2";
			this.xrTableCell2.Weight = 0.29682540457589279D;
			// 
			// xrTableCell4
			// 
			this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "AnalyzerMaterialName")});
			this.xrTableCell4.Name = "xrTableCell4";
			this.xrTableCell4.Text = "xrTableCell4";
			this.xrTableCell4.Weight = 0.39111121186755954D;
			// 
			// xrTableCell1
			// 
			this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "IsActive")});
			this.xrTableCell1.Name = "xrTableCell1";
			this.xrTableCell1.Text = "xrTableCell1";
			this.xrTableCell1.Weight = 0.14539676339285712D;
			// 
			// xrTableCell3
			// 
			this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "UpdateStamp", "{0:M/d/yyyy hh:mm tt}")});
			this.xrTableCell3.Name = "xrTableCell3";
			this.xrTableCell3.Text = "xrTableCell3";
			this.xrTableCell3.Weight = 0.30952376302083334D;
			// 
			// TopMargin
			// 
			this.TopMargin.HeightF = 50F;
			this.TopMargin.Name = "TopMargin";
			this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// BottomMargin
			// 
			this.BottomMargin.HeightF = 50F;
			this.BottomMargin.Name = "BottomMargin";
			this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// xrLblAnalyzerMaterialCode
			// 
			this.xrLblAnalyzerMaterialCode.LocationFloat = new DevExpress.Utils.PointFloat(0F, 67.00001F);
			this.xrLblAnalyzerMaterialCode.Name = "xrLblAnalyzerMaterialCode";
			this.xrLblAnalyzerMaterialCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLblAnalyzerMaterialCode.SizeF = new System.Drawing.SizeF(117.7083F, 23F);
			this.xrLblAnalyzerMaterialCode.Text = "Analyzer Product Code";
			// 
			// xrLblAnalyzerMaterialName
			// 
			this.xrLblAnalyzerMaterialName.LocationFloat = new DevExpress.Utils.PointFloat(194.7917F, 67.00001F);
			this.xrLblAnalyzerMaterialName.Name = "xrLblAnalyzerMaterialName";
			this.xrLblAnalyzerMaterialName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLblAnalyzerMaterialName.SizeF = new System.Drawing.SizeF(128.125F, 23F);
			this.xrLblAnalyzerMaterialName.Text = "Analyzer Product Name";
			// 
			// xrLblIsActive
			// 
			this.xrLblIsActive.LocationFloat = new DevExpress.Utils.PointFloat(451.4584F, 67.00001F);
			this.xrLblIsActive.Name = "xrLblIsActive";
			this.xrLblIsActive.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLblIsActive.SizeF = new System.Drawing.SizeF(64.16663F, 23F);
			this.xrLblIsActive.Text = "Is Active";
			// 
			// xrLine2
			// 
			this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 89.99999F);
			this.xrLine2.Name = "xrLine2";
			this.xrLine2.SizeF = new System.Drawing.SizeF(750F, 2.166637F);
			// 
			// PageHeader
			// 
			this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLblPrintedBy,
            this.xrPageInfo1,
            this.xrLblPageNo,
            this.xrPageInfoPageNo,
            this.xrLblDate,
            this.xrPageInfoDate,
            this.xrLblPageHeading,
            this.xrLine1,
            this.xrLblAnalyzerMaterialCode,
            this.xrLblIsActive,
            this.xrLblAnalyzerMaterialName,
            this.xrLine2});
			this.PageHeader.HeightF = 92.16663F;
			this.PageHeader.Name = "PageHeader";
			this.PageHeader.StyleName = "DevReportStyleSheet";
			this.PageHeader.StylePriority.UseFont = false;
			// 
			// xrLabel1
			// 
			this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(546.8751F, 67.00001F);
			this.xrLabel1.Name = "xrLabel1";
			this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLabel1.SizeF = new System.Drawing.SizeF(120.4166F, 23F);
			this.xrLabel1.Text = "UpdateStamp";
			// 
			// xrLblPrintedBy
			// 
			this.xrLblPrintedBy.LocationFloat = new DevExpress.Utils.PointFloat(451.4584F, 23F);
			this.xrLblPrintedBy.Name = "xrLblPrintedBy";
			this.xrLblPrintedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLblPrintedBy.SizeF = new System.Drawing.SizeF(73.95831F, 19.79167F);
			this.xrLblPrintedBy.Text = "Printed By:";
			// 
			// xrPageInfo1
			// 
			this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(525.4167F, 23F);
			this.xrPageInfo1.Name = "xrPageInfo1";
			this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.UserName;
			this.xrPageInfo1.SizeF = new System.Drawing.SizeF(224.5834F, 19.79167F);
			// 
			// xrLblPageNo
			// 
			this.xrLblPageNo.LocationFloat = new DevExpress.Utils.PointFloat(615.6249F, 0F);
			this.xrLblPageNo.Name = "xrLblPageNo";
			this.xrLblPageNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLblPageNo.SizeF = new System.Drawing.SizeF(63.54175F, 23F);
			this.xrLblPageNo.Text = "PageNo.:";
			// 
			// xrPageInfoPageNo
			// 
			this.xrPageInfoPageNo.LocationFloat = new DevExpress.Utils.PointFloat(679.1666F, 0F);
			this.xrPageInfoPageNo.Name = "xrPageInfoPageNo";
			this.xrPageInfoPageNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrPageInfoPageNo.SizeF = new System.Drawing.SizeF(70.83337F, 23F);
			this.xrPageInfoPageNo.StylePriority.UseTextAlignment = false;
			this.xrPageInfoPageNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// xrLblDate
			// 
			this.xrLblDate.LocationFloat = new DevExpress.Utils.PointFloat(451.4584F, 0F);
			this.xrLblDate.Name = "xrLblDate";
			this.xrLblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLblDate.SizeF = new System.Drawing.SizeF(53.12506F, 23F);
			this.xrLblDate.Text = "Date:";
			// 
			// xrPageInfoDate
			// 
			this.xrPageInfoDate.Format = "{0:M/d/yy h:mm tt}";
			this.xrPageInfoDate.LocationFloat = new DevExpress.Utils.PointFloat(504.5834F, 3.178914E-05F);
			this.xrPageInfoDate.Name = "xrPageInfoDate";
			this.xrPageInfoDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrPageInfoDate.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
			this.xrPageInfoDate.SizeF = new System.Drawing.SizeF(111.0416F, 23F);
			this.xrPageInfoDate.StylePriority.UseTextAlignment = false;
			this.xrPageInfoDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
			// 
			// xrLblPageHeading
			// 
			this.xrLblPageHeading.Font = new DevExpress.Drawing.DXFont("Arial", 21.75F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
			this.xrLblPageHeading.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
			this.xrLblPageHeading.Name = "xrLblPageHeading";
			this.xrLblPageHeading.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			this.xrLblPageHeading.SizeF = new System.Drawing.SizeF(351.0417F, 43.83335F);
			this.xrLblPageHeading.StylePriority.UseFont = false;
			this.xrLblPageHeading.Text = "Analyzer Product";
			// 
			// xrLine1
			// 
			this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 43.83335F);
			this.xrLine1.Name = "xrLine1";
			this.xrLine1.SizeF = new System.Drawing.SizeF(750.0001F, 2.166653F);
			// 
			// DevReportStyleSheet
			// 
			this.DevReportStyleSheet.BorderColor = System.Drawing.Color.Transparent;
			this.DevReportStyleSheet.Font = new DevExpress.Drawing.DXFont("Arial Narrow", 11.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
			this.DevReportStyleSheet.Name = "DevReportStyleSheet";
			// 
			// xrControlStyle2
			// 
			this.xrControlStyle2.Name = "xrControlStyle2";
			// 
			// bindingSource1
			// 
			this.bindingSource1.DataSource = typeof(CFCAIMCore.Entity.AnalyzerMaterialInfo);
			// 
			// DevReportEvenRowStyle
			// 
			this.DevReportEvenRowStyle.Name = "DevReportEvenRowStyle";
			this.DevReportEvenRowStyle.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
			// 
			// AnalyzerMaterialReport
			// 
			this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
			this.DataSource = this.bindingSource1;
			this.Font = new DevExpress.Drawing.DXFont("Arial Narrow", 11.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
			this.Margins = new DevExpress.Drawing.DXMargins(50, 50, 50, 50);
			this.ShowPreviewMarginLines = false;
			this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.DevReportStyleSheet,
            this.xrControlStyle2,
            this.DevReportEvenRowStyle});
			this.Version = "13.2";
			((System.ComponentModel.ISupportInitialize)(this.xrTableAnalyzerMaterial)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraReports.UI.XRTable xrTableAnalyzerMaterial;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRLabel xrLblAnalyzerMaterialCode;
        private DevExpress.XtraReports.UI.XRLabel xrLblAnalyzerMaterialName;
        private DevExpress.XtraReports.UI.XRLabel xrLblIsActive;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel xrLblPageNo;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfoPageNo;
        private DevExpress.XtraReports.UI.XRLabel xrLblDate;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfoDate;
        private DevExpress.XtraReports.UI.XRLabel xrLblPageHeading;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRControlStyle DevReportStyleSheet;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle2;
        private DevExpress.XtraReports.UI.XRLabel xrLblPrintedBy;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRControlStyle DevReportEvenRowStyle;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
    }
}
