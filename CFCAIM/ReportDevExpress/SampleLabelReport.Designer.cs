namespace CFCAIM.ReportDevExpress
{
    partial class SampleLabelReport
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
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabelCaption5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelCaption1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelField3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelCaption3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelField4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelCaption4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelField2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelField5 = new DevExpress.XtraReports.UI.XRLabel();
            this.dbSampleDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelField1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelCaption2 = new DevExpress.XtraReports.UI.XRLabel();
            this.bcSampleID = new DevExpress.XtraReports.UI.XRBarCode();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabelCaption5,
            this.xrLabelCaption1,
            this.xrLabelField3,
            this.xrLabelCaption3,
            this.xrLabel2,
            this.xrLabelField4,
            this.xrLabelCaption4,
            this.xrLabelField2,
            this.xrLabelField5,
            this.dbSampleDate,
            this.xrLabelField1,
            this.xrLabelCaption2,
            this.bcSampleID});
            this.Detail.HeightF = 186.4583F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabelCaption5
            // 
            this.xrLabelCaption5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelCaption5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line5Caption]")});
            this.xrLabelCaption5.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.xrLabelCaption5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 154.5F);
            this.xrLabelCaption5.Name = "xrLabelCaption5";
            this.xrLabelCaption5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelCaption5.SizeF = new System.Drawing.SizeF(99.99998F, 23F);
            this.xrLabelCaption5.StylePriority.UseBorders = false;
            this.xrLabelCaption5.StylePriority.UseFont = false;
            this.xrLabelCaption5.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelCaption5.WordWrap = false;
            // 
            // xrLabelCaption1
            // 
            this.xrLabelCaption1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelCaption1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line1Caption]")});
            this.xrLabelCaption1.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.xrLabelCaption1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 62.5F);
            this.xrLabelCaption1.Name = "xrLabelCaption1";
            this.xrLabelCaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelCaption1.SizeF = new System.Drawing.SizeF(99.99998F, 23F);
            this.xrLabelCaption1.StylePriority.UseBorders = false;
            this.xrLabelCaption1.StylePriority.UseFont = false;
            this.xrLabelCaption1.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelCaption1.WordWrap = false;
            // 
            // xrLabelField3
            // 
            this.xrLabelField3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelField3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line3Data]")});
            this.xrLabelField3.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.xrLabelField3.LocationFloat = new DevExpress.Utils.PointFloat(100F, 108.5F);
            this.xrLabelField3.Name = "xrLabelField3";
            this.xrLabelField3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelField3.SizeF = new System.Drawing.SizeF(180F, 23F);
            this.xrLabelField3.StylePriority.UseBorders = false;
            this.xrLabelField3.StylePriority.UseFont = false;
            this.xrLabelField3.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelField3.WordWrap = false;
            // 
            // xrLabelCaption3
            // 
            this.xrLabelCaption3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelCaption3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line3Caption]")});
            this.xrLabelCaption3.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabelCaption3.LocationFloat = new DevExpress.Utils.PointFloat(1.589457E-05F, 108.5F);
            this.xrLabelCaption3.Name = "xrLabelCaption3";
            this.xrLabelCaption3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelCaption3.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabelCaption3.StylePriority.UseBorders = false;
            this.xrLabelCaption3.StylePriority.UseFont = false;
            this.xrLabelCaption3.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelCaption3.WordWrap = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 23F);
            // 
            // xrLabelField4
            // 
            this.xrLabelField4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelField4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line4Data]")});
            this.xrLabelField4.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.xrLabelField4.LocationFloat = new DevExpress.Utils.PointFloat(100F, 131.5F);
            this.xrLabelField4.Name = "xrLabelField4";
            this.xrLabelField4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelField4.SizeF = new System.Drawing.SizeF(180F, 23F);
            this.xrLabelField4.StylePriority.UseBorders = false;
            this.xrLabelField4.StylePriority.UseFont = false;
            this.xrLabelField4.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelField4.WordWrap = false;
            // 
            // xrLabelCaption4
            // 
            this.xrLabelCaption4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelCaption4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line4Caption]")});
            this.xrLabelCaption4.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabelCaption4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 131.5F);
            this.xrLabelCaption4.Name = "xrLabelCaption4";
            this.xrLabelCaption4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelCaption4.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabelCaption4.StylePriority.UseBorders = false;
            this.xrLabelCaption4.StylePriority.UseFont = false;
            this.xrLabelCaption4.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelCaption4.WordWrap = false;
            // 
            // xrLabelField2
            // 
            this.xrLabelField2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelField2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line2Data]")});
            this.xrLabelField2.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.xrLabelField2.LocationFloat = new DevExpress.Utils.PointFloat(99.99998F, 85.5F);
            this.xrLabelField2.Name = "xrLabelField2";
            this.xrLabelField2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelField2.SizeF = new System.Drawing.SizeF(180F, 23F);
            this.xrLabelField2.StylePriority.UseBorders = false;
            this.xrLabelField2.StylePriority.UseFont = false;
            this.xrLabelField2.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelField2.WordWrap = false;
            // 
            // xrLabelField5
            // 
            this.xrLabelField5.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelField5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line5Data]")});
            this.xrLabelField5.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.xrLabelField5.LocationFloat = new DevExpress.Utils.PointFloat(100F, 154.5F);
            this.xrLabelField5.Name = "xrLabelField5";
            this.xrLabelField5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelField5.SizeF = new System.Drawing.SizeF(180F, 23F);
            this.xrLabelField5.StylePriority.UseBorders = false;
            this.xrLabelField5.StylePriority.UseFont = false;
            this.xrLabelField5.Text = "xrLabelField5";
            this.xrLabelField5.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelField5.WordWrap = false;
            // 
            // dbSampleDate
            // 
            this.dbSampleDate.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.dbSampleDate.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SampleDate]")});
            this.dbSampleDate.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.dbSampleDate.LocationFloat = new DevExpress.Utils.PointFloat(179.5834F, 39.49999F);
            this.dbSampleDate.Name = "dbSampleDate";
            this.dbSampleDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dbSampleDate.SizeF = new System.Drawing.SizeF(100.4166F, 23F);
            this.dbSampleDate.StylePriority.UseBorders = false;
            this.dbSampleDate.StylePriority.UseFont = false;
            this.dbSampleDate.Text = "dbSampleDate";
            this.dbSampleDate.TextFormatString = "{0:MM/dd/yyyy}";
            // 
            // xrLabelField1
            // 
            this.xrLabelField1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelField1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line1Data]")});
            this.xrLabelField1.Font = new DevExpress.Drawing.DXFont("Arial", 11.25F);
            this.xrLabelField1.LocationFloat = new DevExpress.Utils.PointFloat(100F, 62.5F);
            this.xrLabelField1.Name = "xrLabelField1";
            this.xrLabelField1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelField1.SizeF = new System.Drawing.SizeF(180F, 23F);
            this.xrLabelField1.StylePriority.UseBorders = false;
            this.xrLabelField1.StylePriority.UseFont = false;
            this.xrLabelField1.Text = "xrLabelField1";
            this.xrLabelField1.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelField1.WordWrap = false;
            // 
            // xrLabelCaption2
            // 
            this.xrLabelCaption2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabelCaption2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Line2Caption]")});
            this.xrLabelCaption2.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabelCaption2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 85.5F);
            this.xrLabelCaption2.Name = "xrLabelCaption2";
            this.xrLabelCaption2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabelCaption2.SizeF = new System.Drawing.SizeF(99.99998F, 23F);
            this.xrLabelCaption2.StylePriority.UseBorders = false;
            this.xrLabelCaption2.StylePriority.UseFont = false;
            this.xrLabelCaption2.TextTrimming = DevExpress.Drawing.DXStringTrimming.EllipsisCharacter;
            this.xrLabelCaption2.WordWrap = false;
            // 
            // bcSampleID
            // 
            this.bcSampleID.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.bcSampleID.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SampleID]")});
            this.bcSampleID.Font = new DevExpress.Drawing.DXFont("Arial", 12F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.bcSampleID.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.bcSampleID.Name = "bcSampleID";
            this.bcSampleID.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.bcSampleID.SizeF = new System.Drawing.SizeF(280F, 52.16667F);
            this.bcSampleID.StylePriority.UseBorders = false;
            this.bcSampleID.StylePriority.UseFont = false;
            this.bcSampleID.Symbology = code128Generator1;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 10F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 10F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(CFCAIMCore.Entity.SampleLabelInfo);
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(CFCAIMCore.Entity.SampleLabelFormatInfo);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // SampleLabelReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.bindingSource1,
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Margins = new DevExpress.Drawing.DXMargins(10F, 10F, 10F, 10F);
            this.PageHeight = 200;
            this.PageWidth = 300;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
            this.ShowPreviewMarginLines = false;
            this.Version = "24.1";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.SampleLabelReport_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRBarCode bcSampleID;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraReports.UI.XRLabel dbSampleDate;
        private DevExpress.XtraReports.UI.XRLabel xrLabelField1;
        private DevExpress.XtraReports.UI.XRLabel xrLabelCaption2;
        private DevExpress.XtraReports.UI.XRLabel xrLabelField2;
        private DevExpress.XtraReports.UI.XRLabel xrLabelField5;
        private DevExpress.XtraReports.UI.XRLabel xrLabelCaption4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabelField4;
        private DevExpress.XtraReports.UI.XRLabel xrLabelCaption3;
        private DevExpress.XtraReports.UI.XRLabel xrLabelField3;
		private DevExpress.XtraReports.UI.XRLabel xrLabelCaption1;
		private DevExpress.XtraReports.UI.XRLabel xrLabelCaption5;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    }
}
