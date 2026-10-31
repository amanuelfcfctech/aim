namespace CFCAIM
{
	partial class MessageDetail_Test
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageDetail_Test));
			this.grdMessageInside = new DevExpress.XtraGrid.GridControl();
			this.grdMessageInsideView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colAnalyzerNutrientID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDataSource = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colAnalyzerNutrientCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colAimMaterialName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colAimNutrientName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colResultValue = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMax = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMessage = new DevExpress.XtraGrid.Columns.GridColumn();
			this.grdMessageOutside = new DevExpress.XtraGrid.GridControl();
			this.grdMessageOutsideView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.replueDataSourceTypeWarning = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.colNutrient = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colGlobalH = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colLimit = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.replnkMessages = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnExit = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageInside)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageInsideView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageOutside)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageOutsideView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceTypeWarning)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replnkMessages)).BeginInit();
			this.pnlTopBtns.SuspendLayout();
			this.SuspendLayout();
			// 
			// grdMessageInside
			// 
			this.grdMessageInside.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdMessageInside.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdMessageInside.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdMessageInside.Location = new System.Drawing.Point(12, 328);
			this.grdMessageInside.MainView = this.grdMessageInsideView;
			this.grdMessageInside.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdMessageInside.Name = "grdMessageInside";
			this.grdMessageInside.Size = new System.Drawing.Size(1003, 256);
			this.grdMessageInside.TabIndex = 67;
			this.grdMessageInside.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMessageInsideView});
			// 
			// grdMessageInsideView
			// 
			this.grdMessageInsideView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.ColumnFilterButton.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.CustomizationFormHint.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.DetailTip.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.Empty.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.EvenRow.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.FilterCloseButton.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.FilterPanel.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.FixedLine.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.FocusedCell.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.FocusedRow.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.FooterPanel.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.GroupButton.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.GroupFooter.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.GroupPanel.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.GroupRow.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.HeaderPanel.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.HideSelectionRow.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.HorzLine.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.OddRow.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.Preview.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.Row.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.RowSeparator.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.SelectedRow.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.TopNewRow.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.VertLine.Options.UseFont = true;
			this.grdMessageInsideView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.Appearance.ViewCaption.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.grdMessageInsideView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.grdMessageInsideView.AppearancePrint.EvenRow.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.FilterPanel.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.GroupRow.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.Lines.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.grdMessageInsideView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.grdMessageInsideView.AppearancePrint.OddRow.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.Preview.Options.UseFont = true;
			this.grdMessageInsideView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageInsideView.AppearancePrint.Row.Options.UseFont = true;
			this.grdMessageInsideView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn1,
            this.gridColumn2,
            this.colAnalyzerNutrientID,
            this.colDataSource,
            this.colAnalyzerNutrientCode,
            this.colAimMaterialName,
            this.colAimNutrientName,
            this.colResultValue,
            this.colMax,
            this.colMin,
            this.colMessage});
			this.grdMessageInsideView.GridControl = this.grdMessageInside;
			this.grdMessageInsideView.Name = "grdMessageInsideView";
			this.grdMessageInsideView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
			this.grdMessageInsideView.OptionsPrint.EnableAppearanceEvenRow = true;
			this.grdMessageInsideView.OptionsPrint.EnableAppearanceOddRow = true;
			// 
			// gridColumn11
			// 
			this.gridColumn11.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn11.AppearanceCell.Options.UseFont = true;
			this.gridColumn11.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn11.AppearanceHeader.Options.UseFont = true;
			this.gridColumn11.Caption = "SampleResultPredictID";
			this.gridColumn11.FieldName = "SampleResultPredictID";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.OptionsColumn.AllowEdit = false;
			this.gridColumn11.Width = 189;
			// 
			// gridColumn1
			// 
			this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn1.AppearanceCell.Options.UseFont = true;
			this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn1.AppearanceHeader.Options.UseFont = true;
			this.gridColumn1.Caption = "SampleResultID";
			this.gridColumn1.FieldName = "SampleResultID";
			this.gridColumn1.Name = "gridColumn1";
			this.gridColumn1.OptionsColumn.AllowEdit = false;
			this.gridColumn1.Width = 121;
			// 
			// gridColumn2
			// 
			this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn2.AppearanceCell.Options.UseFont = true;
			this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn2.AppearanceHeader.Options.UseFont = true;
			this.gridColumn2.Caption = "Sample ID";
			this.gridColumn2.FieldName = "SampleID";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Width = 91;
			// 
			// colAnalyzerNutrientID
			// 
			this.colAnalyzerNutrientID.Caption = "AnalyzerNutrientID";
			this.colAnalyzerNutrientID.FieldName = "AnalyzerNutrientID";
			this.colAnalyzerNutrientID.Name = "colAnalyzerNutrientID";
			// 
			// colDataSource
			// 
			this.colDataSource.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colDataSource.AppearanceCell.Options.UseFont = true;
			this.colDataSource.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colDataSource.AppearanceHeader.Options.UseFont = true;
			this.colDataSource.Caption = "Data Source";
			this.colDataSource.FieldName = "DataSourceTypeID";
			this.colDataSource.Name = "colDataSource";
			this.colDataSource.OptionsColumn.AllowEdit = false;
			this.colDataSource.Width = 127;
			// 
			// colAnalyzerNutrientCode
			// 
			this.colAnalyzerNutrientCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colAnalyzerNutrientCode.AppearanceCell.Options.UseFont = true;
			this.colAnalyzerNutrientCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colAnalyzerNutrientCode.AppearanceHeader.Options.UseFont = true;
			this.colAnalyzerNutrientCode.Caption = "Analyzer Nutrient Code";
			this.colAnalyzerNutrientCode.FieldName = "AnalyzerNutrientCode";
			this.colAnalyzerNutrientCode.Name = "colAnalyzerNutrientCode";
			this.colAnalyzerNutrientCode.Visible = true;
			this.colAnalyzerNutrientCode.VisibleIndex = 1;
			this.colAnalyzerNutrientCode.Width = 171;
			// 
			// colAimMaterialName
			// 
			this.colAimMaterialName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colAimMaterialName.AppearanceCell.Options.UseFont = true;
			this.colAimMaterialName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colAimMaterialName.AppearanceHeader.Options.UseFont = true;
			this.colAimMaterialName.Caption = "AIM Material Name";
			this.colAimMaterialName.FieldName = "AimMaterialName";
			this.colAimMaterialName.Name = "colAimMaterialName";
			this.colAimMaterialName.OptionsColumn.AllowEdit = false;
			this.colAimMaterialName.Visible = true;
			this.colAimMaterialName.VisibleIndex = 2;
			this.colAimMaterialName.Width = 134;
			// 
			// colAimNutrientName
			// 
			this.colAimNutrientName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colAimNutrientName.AppearanceCell.Options.UseFont = true;
			this.colAimNutrientName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colAimNutrientName.AppearanceHeader.Options.UseFont = true;
			this.colAimNutrientName.Caption = "AIM Nutrient Name";
			this.colAimNutrientName.FieldName = "AimNutrientName";
			this.colAimNutrientName.Name = "colAimNutrientName";
			this.colAimNutrientName.Visible = true;
			this.colAimNutrientName.VisibleIndex = 3;
			this.colAimNutrientName.Width = 131;
			// 
			// colResultValue
			// 
			this.colResultValue.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colResultValue.AppearanceCell.Options.UseFont = true;
			this.colResultValue.AppearanceCell.Options.UseTextOptions = true;
			this.colResultValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colResultValue.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colResultValue.AppearanceHeader.Options.UseFont = true;
			this.colResultValue.AppearanceHeader.Options.UseTextOptions = true;
			this.colResultValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colResultValue.Caption = "Result Value";
			this.colResultValue.DisplayFormat.FormatString = "N3";
			this.colResultValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colResultValue.FieldName = "ResultValue";
			this.colResultValue.Name = "colResultValue";
			this.colResultValue.Visible = true;
			this.colResultValue.VisibleIndex = 6;
			this.colResultValue.Width = 102;
			// 
			// colMax
			// 
			this.colMax.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colMax.AppearanceCell.Options.UseFont = true;
			this.colMax.AppearanceCell.Options.UseTextOptions = true;
			this.colMax.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colMax.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colMax.AppearanceHeader.Options.UseFont = true;
			this.colMax.AppearanceHeader.Options.UseTextOptions = true;
			this.colMax.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colMax.Caption = "Max";
			this.colMax.DisplayFormat.FormatString = "n3";
			this.colMax.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colMax.FieldName = "MaxWarning";
			this.colMax.Name = "colMax";
			this.colMax.OptionsColumn.AllowEdit = false;
			this.colMax.OptionsColumn.ReadOnly = true;
			this.colMax.Visible = true;
			this.colMax.VisibleIndex = 5;
			this.colMax.Width = 70;
			// 
			// colMin
			// 
			this.colMin.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colMin.AppearanceCell.Options.UseFont = true;
			this.colMin.AppearanceCell.Options.UseTextOptions = true;
			this.colMin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colMin.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colMin.AppearanceHeader.Options.UseFont = true;
			this.colMin.AppearanceHeader.Options.UseTextOptions = true;
			this.colMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colMin.Caption = "Min";
			this.colMin.DisplayFormat.FormatString = "n3";
			this.colMin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colMin.FieldName = "MinWarning";
			this.colMin.Name = "colMin";
			this.colMin.OptionsColumn.AllowEdit = false;
			this.colMin.OptionsColumn.ReadOnly = true;
			this.colMin.Visible = true;
			this.colMin.VisibleIndex = 4;
			this.colMin.Width = 73;
			// 
			// colMessage
			// 
			this.colMessage.AppearanceCell.BackColor = System.Drawing.Color.White;
			this.colMessage.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colMessage.AppearanceCell.ForeColor = System.Drawing.Color.Black;
			this.colMessage.AppearanceCell.Options.UseBackColor = true;
			this.colMessage.AppearanceCell.Options.UseFont = true;
			this.colMessage.AppearanceCell.Options.UseForeColor = true;
			this.colMessage.AppearanceCell.Options.UseTextOptions = true;
			this.colMessage.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.colMessage.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.colMessage.AppearanceHeader.ForeColor = System.Drawing.Color.Red;
			this.colMessage.AppearanceHeader.Options.UseFont = true;
			this.colMessage.AppearanceHeader.Options.UseForeColor = true;
			this.colMessage.AppearanceHeader.Options.UseTextOptions = true;
			this.colMessage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colMessage.Caption = "Message";
			this.colMessage.FieldName = "Message";
			this.colMessage.Name = "colMessage";
			this.colMessage.OptionsColumn.AllowEdit = false;
			this.colMessage.Visible = true;
			this.colMessage.VisibleIndex = 0;
			this.colMessage.Width = 199;
			// 
			// grdMessageOutside
			// 
			this.grdMessageOutside.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdMessageOutside.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdMessageOutside.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdMessageOutside.Location = new System.Drawing.Point(12, 67);
			this.grdMessageOutside.MainView = this.grdMessageOutsideView;
			this.grdMessageOutside.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdMessageOutside.Name = "grdMessageOutside";
			this.grdMessageOutside.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueDataSourceTypeWarning,
            this.replnkMessages});
			this.grdMessageOutside.Size = new System.Drawing.Size(1003, 256);
			this.grdMessageOutside.TabIndex = 68;
			this.grdMessageOutside.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMessageOutsideView});
			// 
			// grdMessageOutsideView
			// 
			this.grdMessageOutsideView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.ColumnFilterButton.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.CustomizationFormHint.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.DetailTip.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.Empty.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.EvenRow.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.FilterCloseButton.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.FilterPanel.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.FixedLine.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.FocusedCell.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.FocusedRow.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.FooterPanel.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.GroupButton.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.GroupFooter.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.GroupPanel.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.GroupRow.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.HeaderPanel.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.HideSelectionRow.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.HorzLine.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.OddRow.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.Preview.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.Row.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.RowSeparator.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.SelectedRow.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.TopNewRow.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.VertLine.Options.UseFont = true;
			this.grdMessageOutsideView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.Appearance.ViewCaption.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.grdMessageOutsideView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.grdMessageOutsideView.AppearancePrint.EvenRow.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.FilterPanel.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.GroupRow.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.Lines.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.grdMessageOutsideView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.grdMessageOutsideView.AppearancePrint.OddRow.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.Preview.Options.UseFont = true;
			this.grdMessageOutsideView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageOutsideView.AppearancePrint.Row.Options.UseFont = true;
			this.grdMessageOutsideView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.colNutrient,
            this.gridColumn8,
            this.colGlobalH,
            this.colLimit,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn12});
			this.grdMessageOutsideView.GridControl = this.grdMessageOutside;
			this.grdMessageOutsideView.Name = "grdMessageOutsideView";
			this.grdMessageOutsideView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
			this.grdMessageOutsideView.OptionsPrint.EnableAppearanceEvenRow = true;
			this.grdMessageOutsideView.OptionsPrint.EnableAppearanceOddRow = true;
			// 
			// gridColumn3
			// 
			this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn3.AppearanceCell.Options.UseFont = true;
			this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn3.AppearanceHeader.Options.UseFont = true;
			this.gridColumn3.Caption = "SampleResultPredictID";
			this.gridColumn3.FieldName = "SampleResultPredictID";
			this.gridColumn3.Name = "gridColumn3";
			this.gridColumn3.OptionsColumn.AllowEdit = false;
			this.gridColumn3.Width = 189;
			// 
			// gridColumn4
			// 
			this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn4.AppearanceCell.Options.UseFont = true;
			this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn4.AppearanceHeader.Options.UseFont = true;
			this.gridColumn4.Caption = "SampleResultID";
			this.gridColumn4.FieldName = "SampleResultID";
			this.gridColumn4.Name = "gridColumn4";
			this.gridColumn4.OptionsColumn.AllowEdit = false;
			this.gridColumn4.Width = 121;
			// 
			// gridColumn5
			// 
			this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn5.AppearanceCell.Options.UseFont = true;
			this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn5.AppearanceHeader.Options.UseFont = true;
			this.gridColumn5.Caption = "Sample ID";
			this.gridColumn5.FieldName = "SampleID";
			this.gridColumn5.Name = "gridColumn5";
			this.gridColumn5.OptionsColumn.AllowEdit = false;
			this.gridColumn5.Width = 91;
			// 
			// gridColumn6
			// 
			this.gridColumn6.Caption = "AnalyzerNutrientID";
			this.gridColumn6.FieldName = "AnalyzerNutrientID";
			this.gridColumn6.Name = "gridColumn6";
			// 
			// gridColumn7
			// 
			this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn7.AppearanceCell.Options.UseFont = true;
			this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn7.AppearanceHeader.Options.UseFont = true;
			this.gridColumn7.Caption = "Data Source";
			this.gridColumn7.ColumnEdit = this.replueDataSourceTypeWarning;
			this.gridColumn7.FieldName = "DataSourceTypeID";
			this.gridColumn7.Name = "gridColumn7";
			this.gridColumn7.OptionsColumn.AllowEdit = false;
			this.gridColumn7.Width = 127;
			// 
			// replueDataSourceTypeWarning
			// 
			this.replueDataSourceTypeWarning.AutoHeight = false;
			this.replueDataSourceTypeWarning.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueDataSourceTypeWarning.Name = "replueDataSourceTypeWarning";
			// 
			// colNutrient
			// 
			this.colNutrient.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colNutrient.AppearanceCell.Options.UseFont = true;
			this.colNutrient.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colNutrient.AppearanceHeader.Options.UseFont = true;
			this.colNutrient.Caption = "Nutrient Code";
			this.colNutrient.FieldName = "AnalyzerNutrientCode";
			this.colNutrient.Name = "colNutrient";
			this.colNutrient.OptionsColumn.AllowEdit = false;
			this.colNutrient.Visible = true;
			this.colNutrient.VisibleIndex = 1;
			this.colNutrient.Width = 138;
			// 
			// gridColumn8
			// 
			this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn8.AppearanceCell.Options.UseFont = true;
			this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn8.AppearanceHeader.Options.UseFont = true;
			this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn8.Caption = "Result Value";
			this.gridColumn8.DisplayFormat.FormatString = "N3";
			this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn8.FieldName = "ResultValue";
			this.gridColumn8.Name = "gridColumn8";
			this.gridColumn8.Visible = true;
			this.gridColumn8.VisibleIndex = 6;
			this.gridColumn8.Width = 112;
			// 
			// colGlobalH
			// 
			this.colGlobalH.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colGlobalH.AppearanceCell.Options.UseFont = true;
			this.colGlobalH.AppearanceCell.Options.UseTextOptions = true;
			this.colGlobalH.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colGlobalH.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colGlobalH.AppearanceHeader.Options.UseFont = true;
			this.colGlobalH.AppearanceHeader.Options.UseTextOptions = true;
			this.colGlobalH.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colGlobalH.Caption = "GlobalH";
			this.colGlobalH.DisplayFormat.FormatString = "n3";
			this.colGlobalH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colGlobalH.FieldName = "GlobalH";
			this.colGlobalH.Name = "colGlobalH";
			this.colGlobalH.OptionsColumn.AllowEdit = false;
			this.colGlobalH.OptionsColumn.ReadOnly = true;
			this.colGlobalH.Visible = true;
			this.colGlobalH.VisibleIndex = 2;
			this.colGlobalH.Width = 84;
			// 
			// colLimit
			// 
			this.colLimit.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colLimit.AppearanceCell.Options.UseFont = true;
			this.colLimit.AppearanceCell.Options.UseTextOptions = true;
			this.colLimit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colLimit.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colLimit.AppearanceHeader.Options.UseFont = true;
			this.colLimit.AppearanceHeader.Options.UseTextOptions = true;
			this.colLimit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colLimit.Caption = "Limit";
			this.colLimit.DisplayFormat.FormatString = "n3";
			this.colLimit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colLimit.FieldName = "Limit";
			this.colLimit.Name = "colLimit";
			this.colLimit.OptionsColumn.AllowEdit = false;
			this.colLimit.OptionsColumn.ReadOnly = true;
			this.colLimit.Visible = true;
			this.colLimit.VisibleIndex = 3;
			this.colLimit.Width = 114;
			// 
			// gridColumn9
			// 
			this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn9.AppearanceCell.Options.UseFont = true;
			this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn9.AppearanceHeader.Options.UseFont = true;
			this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn9.Caption = "Max Material Range";
			this.gridColumn9.DisplayFormat.FormatString = "n3";
			this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn9.FieldName = "MaxMaterialRange";
			this.gridColumn9.Name = "gridColumn9";
			this.gridColumn9.OptionsColumn.AllowEdit = false;
			this.gridColumn9.OptionsColumn.ReadOnly = true;
			this.gridColumn9.Visible = true;
			this.gridColumn9.VisibleIndex = 5;
			this.gridColumn9.Width = 153;
			// 
			// gridColumn10
			// 
			this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn10.AppearanceCell.Options.UseFont = true;
			this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn10.AppearanceHeader.Options.UseFont = true;
			this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn10.Caption = "Min Material Range";
			this.gridColumn10.DisplayFormat.FormatString = "n3";
			this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.gridColumn10.FieldName = "MinMaterialRange";
			this.gridColumn10.Name = "gridColumn10";
			this.gridColumn10.OptionsColumn.AllowEdit = false;
			this.gridColumn10.OptionsColumn.ReadOnly = true;
			this.gridColumn10.Visible = true;
			this.gridColumn10.VisibleIndex = 4;
			this.gridColumn10.Width = 141;
			// 
			// gridColumn12
			// 
			this.gridColumn12.AppearanceCell.BackColor = System.Drawing.Color.White;
			this.gridColumn12.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn12.AppearanceCell.ForeColor = System.Drawing.Color.Black;
			this.gridColumn12.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn12.AppearanceCell.Options.UseFont = true;
			this.gridColumn12.AppearanceCell.Options.UseForeColor = true;
			this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumn12.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.gridColumn12.AppearanceHeader.ForeColor = System.Drawing.Color.Red;
			this.gridColumn12.AppearanceHeader.Options.UseFont = true;
			this.gridColumn12.AppearanceHeader.Options.UseForeColor = true;
			this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn12.Caption = "Message";
			this.gridColumn12.FieldName = "Message";
			this.gridColumn12.Name = "gridColumn12";
			this.gridColumn12.OptionsColumn.AllowEdit = false;
			this.gridColumn12.Visible = true;
			this.gridColumn12.VisibleIndex = 0;
			this.gridColumn12.Width = 243;
			// 
			// replnkMessages
			// 
			this.replnkMessages.AutoHeight = false;
			this.replnkMessages.Name = "replnkMessages";
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Location = new System.Drawing.Point(13, 6);
			this.pnlTopBtns.Margin = new System.Windows.Forms.Padding(4);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(84, 55);
			this.pnlTopBtns.TabIndex = 69;
			// 
			// btnExit
			// 
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(3, 7);
			this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(67, 46);
			this.btnExit.TabIndex = 52;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// MessageDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1049, 588);
			this.Controls.Add(this.pnlTopBtns);
			this.Controls.Add(this.grdMessageOutside);
			this.Controls.Add(this.grdMessageInside);
			this.Name = "MessageDetail";
			this.Text = "MessageDetail";
			this.Load += new System.EventHandler(this.MessageDetail_Load);
			((System.ComponentModel.ISupportInitialize)(this.grdMessageInside)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageInsideView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageOutside)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageOutsideView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceTypeWarning)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replnkMessages)).EndInit();
			this.pnlTopBtns.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl grdMessageInside;
		private DevExpress.XtraGrid.Views.Grid.GridView grdMessageInsideView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn colAnalyzerNutrientID;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSource;
		private DevExpress.XtraGrid.Columns.GridColumn colAnalyzerNutrientCode;
		private DevExpress.XtraGrid.Columns.GridColumn colAimMaterialName;
		private DevExpress.XtraGrid.Columns.GridColumn colAimNutrientName;
		private DevExpress.XtraGrid.Columns.GridColumn colResultValue;
		private DevExpress.XtraGrid.Columns.GridColumn colMax;
		private DevExpress.XtraGrid.Columns.GridColumn colMin;
		private DevExpress.XtraGrid.Columns.GridColumn colMessage;
		private DevExpress.XtraGrid.GridControl grdMessageOutside;
		private DevExpress.XtraGrid.Views.Grid.GridView grdMessageOutsideView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueDataSourceTypeWarning;
		private DevExpress.XtraGrid.Columns.GridColumn colNutrient;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
		private DevExpress.XtraGrid.Columns.GridColumn colGlobalH;
		private DevExpress.XtraGrid.Columns.GridColumn colLimit;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
		private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit replnkMessages;
		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnExit;
	}
}