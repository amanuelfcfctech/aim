namespace CFCAIM
{
	partial class MessageDetailInside
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageDetailInside));
			this.replueDataSourceTypeWarning = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnExit = new System.Windows.Forms.Button();
			this.grdMessage = new DevExpress.XtraGrid.GridControl();
			this.grdMessageView = new DevExpress.XtraGrid.Views.Grid.GridView();
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
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceTypeWarning)).BeginInit();
			this.pnlTopBtns.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdMessage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageView)).BeginInit();
			this.SuspendLayout();
			// 
			// replueDataSourceTypeWarning
			// 
			this.replueDataSourceTypeWarning.AutoHeight = false;
			this.replueDataSourceTypeWarning.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueDataSourceTypeWarning.Name = "replueDataSourceTypeWarning";
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Location = new System.Drawing.Point(24, 13);
			this.pnlTopBtns.Margin = new System.Windows.Forms.Padding(4);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(84, 55);
			this.pnlTopBtns.TabIndex = 65;
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
			// grdMessage
			// 
			this.grdMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdMessage.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdMessage.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdMessage.Location = new System.Drawing.Point(24, 74);
			this.grdMessage.MainView = this.grdMessageView;
			this.grdMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdMessage.Name = "grdMessage";
			this.grdMessage.Size = new System.Drawing.Size(900, 423);
			this.grdMessage.TabIndex = 66;
			this.grdMessage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMessageView});
			// 
			// grdMessageView
			// 
			this.grdMessageView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.ColumnFilterButton.Options.UseFont = true;
			this.grdMessageView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
			this.grdMessageView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.CustomizationFormHint.Options.UseFont = true;
			this.grdMessageView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.DetailTip.Options.UseFont = true;
			this.grdMessageView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.Empty.Options.UseFont = true;
			this.grdMessageView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.EvenRow.Options.UseFont = true;
			this.grdMessageView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.FilterCloseButton.Options.UseFont = true;
			this.grdMessageView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.FilterPanel.Options.UseFont = true;
			this.grdMessageView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.FixedLine.Options.UseFont = true;
			this.grdMessageView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.FocusedCell.Options.UseFont = true;
			this.grdMessageView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.FocusedRow.Options.UseFont = true;
			this.grdMessageView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.FooterPanel.Options.UseFont = true;
			this.grdMessageView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.GroupButton.Options.UseFont = true;
			this.grdMessageView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.GroupFooter.Options.UseFont = true;
			this.grdMessageView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.GroupPanel.Options.UseFont = true;
			this.grdMessageView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.GroupRow.Options.UseFont = true;
			this.grdMessageView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.HeaderPanel.Options.UseFont = true;
			this.grdMessageView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.HideSelectionRow.Options.UseFont = true;
			this.grdMessageView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.HorzLine.Options.UseFont = true;
			this.grdMessageView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.OddRow.Options.UseFont = true;
			this.grdMessageView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.Preview.Options.UseFont = true;
			this.grdMessageView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.Row.Options.UseFont = true;
			this.grdMessageView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.RowSeparator.Options.UseFont = true;
			this.grdMessageView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.SelectedRow.Options.UseFont = true;
			this.grdMessageView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.TopNewRow.Options.UseFont = true;
			this.grdMessageView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.VertLine.Options.UseFont = true;
			this.grdMessageView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.Appearance.ViewCaption.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.grdMessageView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.grdMessageView.AppearancePrint.EvenRow.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.FilterPanel.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.GroupRow.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.Lines.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.grdMessageView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.grdMessageView.AppearancePrint.OddRow.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.Preview.Options.UseFont = true;
			this.grdMessageView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageView.AppearancePrint.Row.Options.UseFont = true;
			this.grdMessageView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
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
			this.grdMessageView.GridControl = this.grdMessage;
			this.grdMessageView.Name = "grdMessageView";
			this.grdMessageView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
			this.grdMessageView.OptionsPrint.EnableAppearanceEvenRow = true;
			this.grdMessageView.OptionsPrint.EnableAppearanceOddRow = true;
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
			this.colDataSource.ColumnEdit = this.replueDataSourceTypeWarning;
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
			// MessageDetailInside
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(936, 529);
			this.Controls.Add(this.grdMessage);
			this.Controls.Add(this.pnlTopBtns);
			this.Name = "MessageDetailInside";
			this.Text = "Messages";
			this.Load += new System.EventHandler(this.MessageDetailInside_Load);
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceTypeWarning)).EndInit();
			this.pnlTopBtns.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdMessage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnExit;
		private DevExpress.XtraGrid.GridControl grdMessage;
		private DevExpress.XtraGrid.Views.Grid.GridView grdMessageView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn colAnalyzerNutrientID;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSource;
		private DevExpress.XtraGrid.Columns.GridColumn colAimMaterialName;
		private DevExpress.XtraGrid.Columns.GridColumn colResultValue;
		private DevExpress.XtraGrid.Columns.GridColumn colMax;
		private DevExpress.XtraGrid.Columns.GridColumn colMin;
		private DevExpress.XtraGrid.Columns.GridColumn colMessage;
		private DevExpress.XtraGrid.Columns.GridColumn colAimNutrientName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueDataSourceTypeWarning;
		private DevExpress.XtraGrid.Columns.GridColumn colAnalyzerNutrientCode;
	}
}