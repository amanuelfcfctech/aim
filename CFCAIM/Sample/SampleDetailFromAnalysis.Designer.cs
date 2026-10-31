namespace CFCAIM
{
    partial class SampleDetailFromAnalysis
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleDetailFromAnalysis));
            this.grdAnalyst = new DevExpress.XtraGrid.GridControl();
            this.grdViewAnalyst = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSampleResultID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumberOfSample = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reptxtNoOfSample = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colSampleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replnkSampleID = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colAIMLocationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMOwnerLocationCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSamplePlant = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCIngredientCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaterial = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCIngredientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCNutrientCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCNutrientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAnalyzerNutrientID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAnalyzerMaterialID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrentNutrientVal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProposedNutrientValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDifference = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMean = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStandardDeviation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsInclude = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reprbIsInclude = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFirstQuartile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThirdQuartile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpper95Mean = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLower95Mean = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpper90Mean = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLower90Mean = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeanPlusStandardDeviation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeanMinusStandardDeviation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOutlierLowerMild = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOutlierLowerExtreme = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOutlierUpperMild = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOutlierUpperExtreme = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMMaterialID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMMaterialCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMMaterialName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMNutrientID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMNutrientCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMNutrientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLMax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUMin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUMax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrimmedIsValidOutlier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleAIMID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsIncludeModified = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reprbIsIncludeModified = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdAnalyst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewAnalyst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtNoOfSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replnkSampleID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reprbIsInclude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reprbIsIncludeModified)).BeginInit();
            this.pnlTopBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdAnalyst
            // 
            this.grdAnalyst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAnalyst.Cursor = System.Windows.Forms.Cursors.Default;
            gridLevelNode1.RelationName = "Level1";
            this.grdAnalyst.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grdAnalyst.Location = new System.Drawing.Point(13, 56);
            this.grdAnalyst.MainView = this.grdViewAnalyst;
            this.grdAnalyst.Name = "grdAnalyst";
            this.grdAnalyst.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reprbIsInclude,
            this.reptxtNoOfSample,
            this.replnkSampleID,
            this.reprbIsIncludeModified});
            this.grdAnalyst.Size = new System.Drawing.Size(930, 367);
            this.grdAnalyst.TabIndex = 9;
            this.grdAnalyst.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewAnalyst});
            
            // 
            // grdViewAnalyst
            // 
            this.grdViewAnalyst.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.DetailTip.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.Empty.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.Empty.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.EvenRow.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.FilterCloseButton.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.FilterPanel.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.FixedLine.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.FocusedCell.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.FocusedRow.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.FooterPanel.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.GroupButton.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.GroupFooter.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.GroupPanel.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.GroupRow.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.HideSelectionRow.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.HorzLine.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.OddRow.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.Preview.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.Preview.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.Row.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.Row.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.RowSeparator.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.SelectedRow.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.TopNewRow.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.VertLine.Options.UseFont = true;
            this.grdViewAnalyst.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.Appearance.ViewCaption.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdViewAnalyst.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdViewAnalyst.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.Lines.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdViewAnalyst.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdViewAnalyst.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.Preview.Options.UseFont = true;
            this.grdViewAnalyst.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 8.25F);
            this.grdViewAnalyst.AppearancePrint.Row.Options.UseFont = true;
            this.grdViewAnalyst.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSampleResultID,
            this.colNumberOfSample,
            this.colSampleID,
            this.colAIMLocationID,
            this.colAIMOwnerLocationCode,
            this.colSamplePlant,
            this.colCFCIngredientCode,
            this.colMaterial,
            this.colCFCIngredientName,
            this.colCFCNutrientCode,
            this.colCFCNutrientName,
            this.colAnalyzerNutrientID,
            this.colAnalyzerMaterialID,
            this.colUnit,
            this.colSampleValue,
            this.colCurrentNutrientVal,
            this.colProposedNutrientValue,
            this.colDifference,
            this.colMean,
            this.colStandardDeviation,
            this.colIsInclude,
            this.colDate,
            this.colFirstQuartile,
            this.colThirdQuartile,
            this.colMin,
            this.colMax,
            this.colUpper95Mean,
            this.colLower95Mean,
            this.colUpper90Mean,
            this.colLower90Mean,
            this.colMeanPlusStandardDeviation,
            this.colMeanMinusStandardDeviation,
            this.colOutlierLowerMild,
            this.colOutlierLowerExtreme,
            this.colOutlierUpperMild,
            this.colOutlierUpperExtreme,
            this.colAIMMaterialID,
            this.colAIMMaterialCode,
            this.colAIMMaterialName,
            this.colAIMNutrientID,
            this.colAIMNutrientCode,
            this.colAIMNutrientName,
            this.colSampleSize,
            this.colSampleCode,
            this.colLMin,
            this.colLMax,
            this.colUMin,
            this.colUMax,
            this.colTrimmedIsValidOutlier,
            this.colSampleAIMID,
            this.colIsIncludeModified});
            this.grdViewAnalyst.GridControl = this.grdAnalyst;
            this.grdViewAnalyst.Name = "grdViewAnalyst";
            this.grdViewAnalyst.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdViewAnalyst.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdViewAnalyst.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdViewAnalyst.OptionsView.EnableAppearanceEvenRow = true;
            this.grdViewAnalyst.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grdViewAnalyst_CustomDrawCell);
            // 
            // colSampleResultID
            // 
            this.colSampleResultID.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colSampleResultID.AppearanceCell.Options.UseFont = true;
            this.colSampleResultID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colSampleResultID.AppearanceHeader.Options.UseFont = true;
            this.colSampleResultID.Caption = "Sample Result ID";
            this.colSampleResultID.FieldName = "SampleResultID";
            this.colSampleResultID.Name = "colSampleResultID";
            // 
            // colNumberOfSample
            // 
            this.colNumberOfSample.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colNumberOfSample.AppearanceCell.Options.UseFont = true;
            this.colNumberOfSample.AppearanceCell.Options.UseTextOptions = true;
            this.colNumberOfSample.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberOfSample.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colNumberOfSample.AppearanceHeader.Options.UseFont = true;
            this.colNumberOfSample.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumberOfSample.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumberOfSample.Caption = "No. of Samples";
            this.colNumberOfSample.ColumnEdit = this.reptxtNoOfSample;
            this.colNumberOfSample.FieldName = "NumberOfSample";
            this.colNumberOfSample.Name = "colNumberOfSample";
            this.colNumberOfSample.Width = 81;
            // 
            // reptxtNoOfSample
            // 
            this.reptxtNoOfSample.AutoHeight = false;
            this.reptxtNoOfSample.Name = "reptxtNoOfSample";
            // 
            // colSampleID
            // 
            this.colSampleID.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colSampleID.AppearanceCell.Options.UseFont = true;
            this.colSampleID.AppearanceCell.Options.UseTextOptions = true;
            this.colSampleID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colSampleID.AppearanceHeader.Options.UseFont = true;
            this.colSampleID.AppearanceHeader.Options.UseTextOptions = true;
            this.colSampleID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleID.Caption = "SampleID";
            this.colSampleID.ColumnEdit = this.replnkSampleID;
            this.colSampleID.FieldName = "SampleID";
            this.colSampleID.Name = "colSampleID";
            this.colSampleID.Visible = true;
            this.colSampleID.VisibleIndex = 13;
            this.colSampleID.Width = 86;
            // 
            // replnkSampleID
            // 
            this.replnkSampleID.AutoHeight = false;
            this.replnkSampleID.Name = "replnkSampleID";
            this.replnkSampleID.Click += new System.EventHandler(this.replnkSampleID_Click);
            // 
            // colAIMLocationID
            // 
            this.colAIMLocationID.Caption = "AIMOwnerLocationID";
            this.colAIMLocationID.FieldName = "AIMOwnerLocationID";
            this.colAIMLocationID.Name = "colAIMLocationID";
            this.colAIMLocationID.Width = 95;
            // 
            // colAIMOwnerLocationCode
            // 
            this.colAIMOwnerLocationCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colAIMOwnerLocationCode.AppearanceCell.Options.UseFont = true;
            this.colAIMOwnerLocationCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colAIMOwnerLocationCode.AppearanceHeader.Options.UseFont = true;
            this.colAIMOwnerLocationCode.Caption = "AIM Owner Location Code";
            this.colAIMOwnerLocationCode.FieldName = "LocationCode";
            this.colAIMOwnerLocationCode.Name = "colAIMOwnerLocationCode";
            this.colAIMOwnerLocationCode.OptionsColumn.AllowEdit = false;
            this.colAIMOwnerLocationCode.Visible = true;
            this.colAIMOwnerLocationCode.VisibleIndex = 4;
            this.colAIMOwnerLocationCode.Width = 95;
            // 
            // colSamplePlant
            // 
            this.colSamplePlant.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colSamplePlant.AppearanceCell.Options.UseFont = true;
            this.colSamplePlant.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colSamplePlant.AppearanceHeader.Options.UseFont = true;
            this.colSamplePlant.Caption = "Sample Plant";
            this.colSamplePlant.FieldName = "SamplePlant";
            this.colSamplePlant.Name = "colSamplePlant";
            this.colSamplePlant.OptionsColumn.AllowEdit = false;
            this.colSamplePlant.Width = 35;
            // 
            // colCFCIngredientCode
            // 
            this.colCFCIngredientCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCFCIngredientCode.AppearanceCell.Options.UseFont = true;
            this.colCFCIngredientCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCFCIngredientCode.AppearanceHeader.Options.UseFont = true;
            this.colCFCIngredientCode.Caption = "FRM Ingredient Code";
            this.colCFCIngredientCode.FieldName = "CFCIngredientCode";
            this.colCFCIngredientCode.Name = "colCFCIngredientCode";
            this.colCFCIngredientCode.OptionsColumn.AllowEdit = false;
            this.colCFCIngredientCode.Width = 80;
            // 
            // colMaterial
            // 
            this.colMaterial.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMaterial.AppearanceCell.Options.UseFont = true;
            this.colMaterial.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMaterial.AppearanceHeader.Options.UseFont = true;
            this.colMaterial.Caption = "Material";
            this.colMaterial.FieldName = "Material";
            this.colMaterial.FieldNameSortGroup = "Material";
            this.colMaterial.Name = "colMaterial";
            this.colMaterial.OptionsColumn.AllowEdit = false;
            this.colMaterial.Width = 80;
            // 
            // colCFCIngredientName
            // 
            this.colCFCIngredientName.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCFCIngredientName.AppearanceCell.Options.UseFont = true;
            this.colCFCIngredientName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCFCIngredientName.AppearanceHeader.Options.UseFont = true;
            this.colCFCIngredientName.Caption = "FRM Ingredient Name";
            this.colCFCIngredientName.FieldName = "CFCIngredientName";
            this.colCFCIngredientName.Name = "colCFCIngredientName";
            this.colCFCIngredientName.OptionsColumn.AllowEdit = false;
            this.colCFCIngredientName.Width = 125;
            // 
            // colCFCNutrientCode
            // 
            this.colCFCNutrientCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCFCNutrientCode.AppearanceCell.Options.UseFont = true;
            this.colCFCNutrientCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCFCNutrientCode.AppearanceHeader.Options.UseFont = true;
            this.colCFCNutrientCode.Caption = "FRM Nutrient Code";
            this.colCFCNutrientCode.FieldName = "CFCNutrientCode";
            this.colCFCNutrientCode.Name = "colCFCNutrientCode";
            this.colCFCNutrientCode.OptionsColumn.AllowEdit = false;
            this.colCFCNutrientCode.Width = 76;
            // 
            // colCFCNutrientName
            // 
            this.colCFCNutrientName.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCFCNutrientName.AppearanceCell.Options.UseFont = true;
            this.colCFCNutrientName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCFCNutrientName.AppearanceHeader.Options.UseFont = true;
            this.colCFCNutrientName.Caption = "FRM Nutrient Name";
            this.colCFCNutrientName.FieldName = "CFCNutrientName";
            this.colCFCNutrientName.Name = "colCFCNutrientName";
            this.colCFCNutrientName.OptionsColumn.AllowEdit = false;
            this.colCFCNutrientName.Width = 125;
            // 
            // colAnalyzerNutrientID
            // 
            this.colAnalyzerNutrientID.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colAnalyzerNutrientID.AppearanceCell.Options.UseFont = true;
            this.colAnalyzerNutrientID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colAnalyzerNutrientID.AppearanceHeader.Options.UseFont = true;
            this.colAnalyzerNutrientID.Caption = "Analyzer NutrientID";
            this.colAnalyzerNutrientID.FieldName = "AnalyzerNutrientID";
            this.colAnalyzerNutrientID.Name = "colAnalyzerNutrientID";
            // 
            // colAnalyzerMaterialID
            // 
            this.colAnalyzerMaterialID.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colAnalyzerMaterialID.AppearanceCell.Options.UseFont = true;
            this.colAnalyzerMaterialID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colAnalyzerMaterialID.AppearanceHeader.Options.UseFont = true;
            this.colAnalyzerMaterialID.Caption = "Analyzer Material ID";
            this.colAnalyzerMaterialID.FieldName = "AnalyzerMaterialID";
            this.colAnalyzerMaterialID.Name = "colAnalyzerMaterialID";
            // 
            // colUnit
            // 
            this.colUnit.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colUnit.AppearanceCell.Options.UseFont = true;
            this.colUnit.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colUnit.AppearanceHeader.Options.UseFont = true;
            this.colUnit.Caption = "Unit";
            this.colUnit.FieldName = "Unit";
            this.colUnit.Name = "colUnit";
            this.colUnit.OptionsColumn.AllowEdit = false;
            this.colUnit.Visible = true;
            this.colUnit.VisibleIndex = 5;
            this.colUnit.Width = 95;
            // 
            // colSampleValue
            // 
            this.colSampleValue.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colSampleValue.AppearanceCell.Options.UseFont = true;
            this.colSampleValue.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colSampleValue.AppearanceHeader.Options.UseFont = true;
            this.colSampleValue.Caption = "SampleValue";
            this.colSampleValue.DisplayFormat.FormatString = "N3";
            this.colSampleValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSampleValue.FieldName = "SampleValue";
            this.colSampleValue.Name = "colSampleValue";
            this.colSampleValue.OptionsColumn.AllowEdit = false;
            this.colSampleValue.Visible = true;
            this.colSampleValue.VisibleIndex = 6;
            this.colSampleValue.Width = 95;
            // 
            // colCurrentNutrientVal
            // 
            this.colCurrentNutrientVal.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCurrentNutrientVal.AppearanceCell.Options.UseFont = true;
            this.colCurrentNutrientVal.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colCurrentNutrientVal.AppearanceHeader.Options.UseFont = true;
            this.colCurrentNutrientVal.Caption = "Current Nut Value";
            this.colCurrentNutrientVal.DisplayFormat.FormatString = "N3";
            this.colCurrentNutrientVal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrentNutrientVal.FieldName = "CurrentNutrientVal";
            this.colCurrentNutrientVal.Name = "colCurrentNutrientVal";
            this.colCurrentNutrientVal.OptionsColumn.AllowEdit = false;
            this.colCurrentNutrientVal.Visible = true;
            this.colCurrentNutrientVal.VisibleIndex = 7;
            this.colCurrentNutrientVal.Width = 95;
            // 
            // colProposedNutrientValue
            // 
            this.colProposedNutrientValue.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colProposedNutrientValue.AppearanceCell.Options.UseFont = true;
            this.colProposedNutrientValue.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colProposedNutrientValue.AppearanceHeader.Options.UseFont = true;
            this.colProposedNutrientValue.Caption = "Proposed Nut Value";
            this.colProposedNutrientValue.DisplayFormat.FormatString = "N3";
            this.colProposedNutrientValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProposedNutrientValue.FieldName = "ProposedNutrientValue";
            this.colProposedNutrientValue.Name = "colProposedNutrientValue";
            this.colProposedNutrientValue.Visible = true;
            this.colProposedNutrientValue.VisibleIndex = 8;
            this.colProposedNutrientValue.Width = 95;
            // 
            // colDifference
            // 
            this.colDifference.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colDifference.AppearanceCell.Options.UseFont = true;
            this.colDifference.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colDifference.AppearanceHeader.Options.UseFont = true;
            this.colDifference.Caption = "% Diff";
            this.colDifference.DisplayFormat.FormatString = "N3";
            this.colDifference.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDifference.FieldName = "Difference";
            this.colDifference.Name = "colDifference";
            this.colDifference.OptionsColumn.AllowEdit = false;
            this.colDifference.Visible = true;
            this.colDifference.VisibleIndex = 9;
            this.colDifference.Width = 95;
            // 
            // colMean
            // 
            this.colMean.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMean.AppearanceCell.Options.UseFont = true;
            this.colMean.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMean.AppearanceHeader.Options.UseFont = true;
            this.colMean.Caption = "Mean";
            this.colMean.DisplayFormat.FormatString = "N3";
            this.colMean.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMean.FieldName = "ProposedMean";
            this.colMean.Name = "colMean";
            this.colMean.OptionsColumn.AllowEdit = false;
            this.colMean.Visible = true;
            this.colMean.VisibleIndex = 10;
            this.colMean.Width = 77;
            // 
            // colStandardDeviation
            // 
            this.colStandardDeviation.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colStandardDeviation.AppearanceCell.Options.UseFont = true;
            this.colStandardDeviation.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colStandardDeviation.AppearanceHeader.Options.UseFont = true;
            this.colStandardDeviation.Caption = "STD DEV";
            this.colStandardDeviation.DisplayFormat.FormatString = "N3";
            this.colStandardDeviation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colStandardDeviation.FieldName = "ProposedStandardDeviation";
            this.colStandardDeviation.Name = "colStandardDeviation";
            this.colStandardDeviation.OptionsColumn.AllowEdit = false;
            this.colStandardDeviation.Visible = true;
            this.colStandardDeviation.VisibleIndex = 11;
            this.colStandardDeviation.Width = 100;
            // 
            // colIsInclude
            // 
            this.colIsInclude.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colIsInclude.AppearanceCell.Options.UseFont = true;
            this.colIsInclude.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colIsInclude.AppearanceHeader.Options.UseFont = true;
            this.colIsInclude.Caption = "Include";
            this.colIsInclude.ColumnEdit = this.reprbIsInclude;
            this.colIsInclude.FieldName = "IsInclude";
            this.colIsInclude.Name = "colIsInclude";
            this.colIsInclude.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colIsInclude.Visible = true;
            this.colIsInclude.VisibleIndex = 14;
            this.colIsInclude.Width = 55;
            // 
            // reprbIsInclude
            // 
            this.reprbIsInclude.AutoHeight = false;
            this.reprbIsInclude.Caption = "Check";
            this.reprbIsInclude.FullFocusRect = true;
            this.reprbIsInclude.ImageOptions.ImageIndexChecked = 1;
            this.reprbIsInclude.ImageOptions.ImageIndexUnchecked = 0;
            this.reprbIsInclude.Name = "reprbIsInclude";
            this.reprbIsInclude.Tag = true;
            this.reprbIsInclude.ValueGrayed = false;
            this.reprbIsInclude.EditValueChanged += new System.EventHandler(this.reprbIsInclude_EditValueChanged);
            // 
            // colDate
            // 
            this.colDate.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colDate.AppearanceCell.Options.UseFont = true;
            this.colDate.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colDate.AppearanceHeader.Options.UseFont = true;
            this.colDate.Caption = "Sample Date";
            this.colDate.FieldName = "SampleDate";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.AllowEdit = false;
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 12;
            this.colDate.Width = 100;
            // 
            // colFirstQuartile
            // 
            this.colFirstQuartile.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colFirstQuartile.AppearanceCell.Options.UseFont = true;
            this.colFirstQuartile.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colFirstQuartile.AppearanceHeader.Options.UseFont = true;
            this.colFirstQuartile.Caption = "First Quartile";
            this.colFirstQuartile.FieldName = "FirstQuartile";
            this.colFirstQuartile.Name = "colFirstQuartile";
            this.colFirstQuartile.OptionsColumn.AllowEdit = false;
            // 
            // colThirdQuartile
            // 
            this.colThirdQuartile.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colThirdQuartile.AppearanceCell.Options.UseFont = true;
            this.colThirdQuartile.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colThirdQuartile.AppearanceHeader.Options.UseFont = true;
            this.colThirdQuartile.Caption = "Third Quartile";
            this.colThirdQuartile.FieldName = "ThirdQuartile";
            this.colThirdQuartile.Name = "colThirdQuartile";
            this.colThirdQuartile.OptionsColumn.AllowEdit = false;
            // 
            // colMin
            // 
            this.colMin.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMin.AppearanceCell.Options.UseFont = true;
            this.colMin.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMin.AppearanceHeader.Options.UseFont = true;
            this.colMin.Caption = "Min";
            this.colMin.FieldName = "Min";
            this.colMin.Name = "colMin";
            this.colMin.OptionsColumn.AllowEdit = false;
            // 
            // colMax
            // 
            this.colMax.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMax.AppearanceCell.Options.UseFont = true;
            this.colMax.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMax.AppearanceHeader.Options.UseFont = true;
            this.colMax.Caption = "Max";
            this.colMax.FieldName = "Max";
            this.colMax.Name = "colMax";
            this.colMax.OptionsColumn.AllowEdit = false;
            // 
            // colUpper95Mean
            // 
            this.colUpper95Mean.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colUpper95Mean.AppearanceCell.Options.UseFont = true;
            this.colUpper95Mean.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colUpper95Mean.AppearanceHeader.Options.UseFont = true;
            this.colUpper95Mean.Caption = "Upper95Mean";
            this.colUpper95Mean.FieldName = "Upper95Mean";
            this.colUpper95Mean.Name = "colUpper95Mean";
            this.colUpper95Mean.OptionsColumn.AllowEdit = false;
            // 
            // colLower95Mean
            // 
            this.colLower95Mean.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colLower95Mean.AppearanceCell.Options.UseFont = true;
            this.colLower95Mean.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colLower95Mean.AppearanceHeader.Options.UseFont = true;
            this.colLower95Mean.Caption = "Lower95Mean";
            this.colLower95Mean.FieldName = "Lower95Mean";
            this.colLower95Mean.Name = "colLower95Mean";
            this.colLower95Mean.OptionsColumn.AllowEdit = false;
            // 
            // colUpper90Mean
            // 
            this.colUpper90Mean.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colUpper90Mean.AppearanceCell.Options.UseFont = true;
            this.colUpper90Mean.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colUpper90Mean.AppearanceHeader.Options.UseFont = true;
            this.colUpper90Mean.Caption = "Upper90Mean";
            this.colUpper90Mean.FieldName = "Upper90Mean";
            this.colUpper90Mean.Name = "colUpper90Mean";
            // 
            // colLower90Mean
            // 
            this.colLower90Mean.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colLower90Mean.AppearanceCell.Options.UseFont = true;
            this.colLower90Mean.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colLower90Mean.AppearanceHeader.Options.UseFont = true;
            this.colLower90Mean.Caption = "Lower90Mean";
            this.colLower90Mean.FieldName = "Lower90Mean";
            this.colLower90Mean.Name = "colLower90Mean";
            this.colLower90Mean.OptionsColumn.AllowEdit = false;
            // 
            // colMeanPlusStandardDeviation
            // 
            this.colMeanPlusStandardDeviation.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMeanPlusStandardDeviation.AppearanceCell.Options.UseFont = true;
            this.colMeanPlusStandardDeviation.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMeanPlusStandardDeviation.AppearanceHeader.Options.UseFont = true;
            this.colMeanPlusStandardDeviation.Caption = "MeanPlusStandardDeviation";
            this.colMeanPlusStandardDeviation.FieldName = "MeanPlusStandardDeviation";
            this.colMeanPlusStandardDeviation.Name = "colMeanPlusStandardDeviation";
            this.colMeanPlusStandardDeviation.OptionsColumn.AllowEdit = false;
            // 
            // colMeanMinusStandardDeviation
            // 
            this.colMeanMinusStandardDeviation.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMeanMinusStandardDeviation.AppearanceCell.Options.UseFont = true;
            this.colMeanMinusStandardDeviation.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colMeanMinusStandardDeviation.AppearanceHeader.Options.UseFont = true;
            this.colMeanMinusStandardDeviation.Caption = "MeanMinusStandardDeviation";
            this.colMeanMinusStandardDeviation.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMeanMinusStandardDeviation.FieldName = "MeanMinusStandardDeviation";
            this.colMeanMinusStandardDeviation.Name = "colMeanMinusStandardDeviation";
            this.colMeanMinusStandardDeviation.OptionsColumn.AllowEdit = false;
            // 
            // colOutlierLowerMild
            // 
            this.colOutlierLowerMild.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colOutlierLowerMild.AppearanceCell.Options.UseFont = true;
            this.colOutlierLowerMild.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colOutlierLowerMild.AppearanceHeader.Options.UseFont = true;
            this.colOutlierLowerMild.Caption = "Outlier Lower Mild";
            this.colOutlierLowerMild.FieldName = "OutlierLowerMild";
            this.colOutlierLowerMild.Name = "colOutlierLowerMild";
            // 
            // colOutlierLowerExtreme
            // 
            this.colOutlierLowerExtreme.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colOutlierLowerExtreme.AppearanceCell.Options.UseFont = true;
            this.colOutlierLowerExtreme.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colOutlierLowerExtreme.AppearanceHeader.Options.UseFont = true;
            this.colOutlierLowerExtreme.Caption = "Outlier Lower Extreme";
            this.colOutlierLowerExtreme.FieldName = "OutlierLowerExtreme";
            this.colOutlierLowerExtreme.Name = "colOutlierLowerExtreme";
            // 
            // colOutlierUpperMild
            // 
            this.colOutlierUpperMild.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colOutlierUpperMild.AppearanceCell.Options.UseFont = true;
            this.colOutlierUpperMild.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colOutlierUpperMild.AppearanceHeader.Options.UseFont = true;
            this.colOutlierUpperMild.Caption = "Outlier Upper Mild";
            this.colOutlierUpperMild.FieldName = "OutlierUpperMild";
            this.colOutlierUpperMild.Name = "colOutlierUpperMild";
            // 
            // colOutlierUpperExtreme
            // 
            this.colOutlierUpperExtreme.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colOutlierUpperExtreme.AppearanceCell.Options.UseFont = true;
            this.colOutlierUpperExtreme.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colOutlierUpperExtreme.AppearanceHeader.Options.UseFont = true;
            this.colOutlierUpperExtreme.Caption = "OutlierUpperExtreme";
            this.colOutlierUpperExtreme.FieldName = "OutlierUpperExtreme";
            this.colOutlierUpperExtreme.Name = "colOutlierUpperExtreme";
            // 
            // colAIMMaterialID
            // 
            this.colAIMMaterialID.Caption = "AIMMaterialID";
            this.colAIMMaterialID.FieldName = "AIMMaterialID";
            this.colAIMMaterialID.Name = "colAIMMaterialID";
            // 
            // colAIMMaterialCode
            // 
            this.colAIMMaterialCode.Caption = "AIMMaterialCode";
            this.colAIMMaterialCode.FieldName = "AIMMaterialCode";
            this.colAIMMaterialCode.Name = "colAIMMaterialCode";
            this.colAIMMaterialCode.Visible = true;
            this.colAIMMaterialCode.VisibleIndex = 0;
            this.colAIMMaterialCode.Width = 88;
            // 
            // colAIMMaterialName
            // 
            this.colAIMMaterialName.Caption = "AIMMaterialName";
            this.colAIMMaterialName.FieldName = "AIMMaterialName";
            this.colAIMMaterialName.Name = "colAIMMaterialName";
            this.colAIMMaterialName.Visible = true;
            this.colAIMMaterialName.VisibleIndex = 1;
            this.colAIMMaterialName.Width = 88;
            // 
            // colAIMNutrientID
            // 
            this.colAIMNutrientID.Caption = "AIMNutrientID";
            this.colAIMNutrientID.FieldName = "AIMNutrientID";
            this.colAIMNutrientID.Name = "colAIMNutrientID";
            // 
            // colAIMNutrientCode
            // 
            this.colAIMNutrientCode.Caption = "AIMNutrientCode";
            this.colAIMNutrientCode.FieldName = "AIMNutrientCode";
            this.colAIMNutrientCode.Name = "colAIMNutrientCode";
            this.colAIMNutrientCode.Visible = true;
            this.colAIMNutrientCode.VisibleIndex = 2;
            this.colAIMNutrientCode.Width = 58;
            // 
            // colAIMNutrientName
            // 
            this.colAIMNutrientName.Caption = "AIMNutrientName";
            this.colAIMNutrientName.FieldName = "AIMNutrientName";
            this.colAIMNutrientName.Name = "colAIMNutrientName";
            this.colAIMNutrientName.Visible = true;
            this.colAIMNutrientName.VisibleIndex = 3;
            this.colAIMNutrientName.Width = 90;
            // 
            // colSampleSize
            // 
            this.colSampleSize.Caption = "Sample Size";
            this.colSampleSize.FieldName = "SampleSize";
            this.colSampleSize.Name = "colSampleSize";
            // 
            // colSampleCode
            // 
            this.colSampleCode.Caption = "Sample Code";
            this.colSampleCode.FieldName = "SampleCode";
            this.colSampleCode.Name = "colSampleCode";
            this.colSampleCode.Visible = true;
            this.colSampleCode.VisibleIndex = 15;
            // 
            // colLMin
            // 
            this.colLMin.Caption = "LMin";
            this.colLMin.FieldName = "LMin";
            this.colLMin.Name = "colLMin";
            // 
            // colLMax
            // 
            this.colLMax.Caption = "LMax";
            this.colLMax.FieldName = "LMax";
            this.colLMax.Name = "colLMax";
            // 
            // colUMin
            // 
            this.colUMin.Caption = "UMin";
            this.colUMin.FieldName = "UMin";
            this.colUMin.Name = "colUMin";
            // 
            // colUMax
            // 
            this.colUMax.Caption = "UMax";
            this.colUMax.FieldName = "UMax";
            this.colUMax.Name = "colUMax";
            // 
            // colTrimmedIsValidOutlier
            // 
            this.colTrimmedIsValidOutlier.Caption = "IsValidOutlier";
            this.colTrimmedIsValidOutlier.FieldName = "IsTrimmedValidOutlier";
            this.colTrimmedIsValidOutlier.Name = "colTrimmedIsValidOutlier";
            // 
            // colSampleAIMID
            // 
            this.colSampleAIMID.Caption = "SampleAIMID";
            this.colSampleAIMID.FieldName = "SampleAIMID";
            this.colSampleAIMID.Name = "colSampleAIMID";
            this.colSampleAIMID.OptionsColumn.AllowShowHide = false;
            // 
            // colIsIncludeModified
            // 
            this.colIsIncludeModified.Caption = "IsIncludeModified";
            this.colIsIncludeModified.ColumnEdit = this.reprbIsIncludeModified;
            this.colIsIncludeModified.FieldName = "IsIncludeModified";
            this.colIsIncludeModified.Name = "colIsIncludeModified";
            // 
            // reprbIsIncludeModified
            // 
            this.reprbIsIncludeModified.AutoHeight = false;
            this.reprbIsIncludeModified.Caption = "Check";
            this.reprbIsIncludeModified.FullFocusRect = true;
            this.reprbIsIncludeModified.Name = "reprbIsIncludeModified";
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(13, 12);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(82, 38);
            this.pnlTopBtns.TabIndex = 29;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(2, 2);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 4;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtCount
            // 
            this.txtCount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCount.Enabled = false;
            this.txtCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCount.Location = new System.Drawing.Point(829, 12);
            this.txtCount.Margin = new System.Windows.Forms.Padding(2);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(37, 19);
            this.txtCount.TabIndex = 30;
            this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(750, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Sample Count";
            // 
            // SampleDetailFromAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(955, 435);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.pnlTopBtns);
            this.Controls.Add(this.grdAnalyst);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SampleDetailFromAnalysis";
            this.Text = "Sample Detail From Analysis";
            this.Load += new System.EventHandler(this.SampleDetailFromAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdAnalyst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewAnalyst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtNoOfSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replnkSampleID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reprbIsInclude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reprbIsIncludeModified)).EndInit();
            this.pnlTopBtns.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdAnalyst;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewAnalyst;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleResultID;
        private DevExpress.XtraGrid.Columns.GridColumn colNumberOfSample;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptxtNoOfSample;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleID;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMLocationID;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMOwnerLocationCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSamplePlant;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCIngredientCode;
        private DevExpress.XtraGrid.Columns.GridColumn colMaterial;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCIngredientName;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCNutrientCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCNutrientName;
        private DevExpress.XtraGrid.Columns.GridColumn colAnalyzerNutrientID;
        private DevExpress.XtraGrid.Columns.GridColumn colAnalyzerMaterialID;
        private DevExpress.XtraGrid.Columns.GridColumn colUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleValue;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrentNutrientVal;
        private DevExpress.XtraGrid.Columns.GridColumn colProposedNutrientValue;
        private DevExpress.XtraGrid.Columns.GridColumn colDifference;
        private DevExpress.XtraGrid.Columns.GridColumn colMean;
        private DevExpress.XtraGrid.Columns.GridColumn colStandardDeviation;
        private DevExpress.XtraGrid.Columns.GridColumn colIsInclude;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit reprbIsInclude;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colFirstQuartile;
        private DevExpress.XtraGrid.Columns.GridColumn colThirdQuartile;
        private DevExpress.XtraGrid.Columns.GridColumn colMin;
        private DevExpress.XtraGrid.Columns.GridColumn colMax;
        private DevExpress.XtraGrid.Columns.GridColumn colUpper95Mean;
        private DevExpress.XtraGrid.Columns.GridColumn colLower95Mean;
        private DevExpress.XtraGrid.Columns.GridColumn colUpper90Mean;
        private DevExpress.XtraGrid.Columns.GridColumn colLower90Mean;
        private DevExpress.XtraGrid.Columns.GridColumn colMeanPlusStandardDeviation;
        private DevExpress.XtraGrid.Columns.GridColumn colMeanMinusStandardDeviation;
        private DevExpress.XtraGrid.Columns.GridColumn colOutlierLowerMild;
        private DevExpress.XtraGrid.Columns.GridColumn colOutlierLowerExtreme;
        private DevExpress.XtraGrid.Columns.GridColumn colOutlierUpperMild;
        private DevExpress.XtraGrid.Columns.GridColumn colOutlierUpperExtreme;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMMaterialID;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMMaterialCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMMaterialName;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMNutrientID;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMNutrientCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMNutrientName;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleSize;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnExit;
		private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit replnkSampleID;
		private System.Windows.Forms.TextBox txtCount;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraGrid.Columns.GridColumn colSampleCode;
		private DevExpress.XtraGrid.Columns.GridColumn colLMin;
		private DevExpress.XtraGrid.Columns.GridColumn colLMax;
		private DevExpress.XtraGrid.Columns.GridColumn colUMin;
		private DevExpress.XtraGrid.Columns.GridColumn colUMax;
		private DevExpress.XtraGrid.Columns.GridColumn colTrimmedIsValidOutlier;
		private DevExpress.XtraGrid.Columns.GridColumn colSampleAIMID;
		private DevExpress.XtraGrid.Columns.GridColumn colIsIncludeModified;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit reprbIsIncludeModified;
	}
}