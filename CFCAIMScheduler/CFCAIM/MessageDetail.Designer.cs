namespace CFCAIM.Sample
{
	partial class SampleMessageDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleMessageDetail));
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnExit = new System.Windows.Forms.Button();
			this.grdMessageDetail = new DevExpress.XtraGrid.GridControl();
			this.grdMessageDetailView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDataSource = new DevExpress.XtraGrid.Columns.GridColumn();
			this.replueDataSourceTypeWarning = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.colNutrient = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colGlobalH = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colLimit = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMax = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMessage = new DevExpress.XtraGrid.Columns.GridColumn();
			this.replnkMessages = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
			this.colResultValue = new DevExpress.XtraGrid.Columns.GridColumn();
			this.pnlTopBtns.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageDetailView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceTypeWarning)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replnkMessages)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Location = new System.Drawing.Point(13, 13);
			this.pnlTopBtns.Margin = new System.Windows.Forms.Padding(4);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(103, 48);
			this.pnlTopBtns.TabIndex = 30;
			// 
			// btnExit
			// 
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(3, 2);
			this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(67, 46);
			this.btnExit.TabIndex = 4;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// grdMessageDetail
			// 
			this.grdMessageDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdMessageDetail.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdMessageDetail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdMessageDetail.Location = new System.Drawing.Point(12, 67);
			this.grdMessageDetail.MainView = this.grdMessageDetailView;
			this.grdMessageDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdMessageDetail.Name = "grdMessageDetail";
			this.grdMessageDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueDataSourceTypeWarning,
            this.replnkMessages});
			this.grdMessageDetail.Size = new System.Drawing.Size(988, 384);
			this.grdMessageDetail.TabIndex = 51;
			this.grdMessageDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMessageDetailView});
			// 
			// grdMessageDetailView
			// 
			this.grdMessageDetailView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.ColumnFilterButton.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.CustomizationFormHint.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.DetailTip.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.Empty.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.EvenRow.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.FilterCloseButton.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.FilterPanel.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.FixedLine.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.FocusedCell.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.FocusedRow.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.FooterPanel.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.GroupButton.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.GroupFooter.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.GroupPanel.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.GroupRow.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.HeaderPanel.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.HideSelectionRow.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.HorzLine.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.OddRow.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.Preview.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.Row.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.RowSeparator.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.SelectedRow.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.TopNewRow.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.VertLine.Options.UseFont = true;
			this.grdMessageDetailView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.Appearance.ViewCaption.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.grdMessageDetailView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.grdMessageDetailView.AppearancePrint.EvenRow.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.FilterPanel.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.GroupRow.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.Lines.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.grdMessageDetailView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.grdMessageDetailView.AppearancePrint.OddRow.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.Preview.Options.UseFont = true;
			this.grdMessageDetailView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdMessageDetailView.AppearancePrint.Row.Options.UseFont = true;
			this.grdMessageDetailView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn1,
            this.gridColumn2,
            this.colDataSource,
            this.colNutrient,
            this.colResultValue,
            this.colGlobalH,
            this.colLimit,
            this.colMax,
            this.colMin,
            this.colMessage});
			this.grdMessageDetailView.GridControl = this.grdMessageDetail;
			this.grdMessageDetailView.Name = "grdMessageDetailView";
			this.grdMessageDetailView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
			this.grdMessageDetailView.OptionsPrint.EnableAppearanceEvenRow = true;
			this.grdMessageDetailView.OptionsPrint.EnableAppearanceOddRow = true;
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
			this.colDataSource.Width = 181;
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
			this.colNutrient.VisibleIndex = 0;
			this.colNutrient.Width = 135;
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
			this.colGlobalH.VisibleIndex = 1;
			this.colGlobalH.Width = 93;
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
			this.colLimit.VisibleIndex = 2;
			this.colLimit.Width = 87;
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
			this.colMax.Caption = "Max Material Range";
			this.colMax.DisplayFormat.FormatString = "n3";
			this.colMax.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colMax.FieldName = "MaxMaterialRange";
			this.colMax.Name = "colMax";
			this.colMax.OptionsColumn.AllowEdit = false;
			this.colMax.OptionsColumn.ReadOnly = true;
			this.colMax.Visible = true;
			this.colMax.VisibleIndex = 4;
			this.colMax.Width = 170;
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
			this.colMin.Caption = "Min Material Range";
			this.colMin.DisplayFormat.FormatString = "n3";
			this.colMin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colMin.FieldName = "MinMaterialRange";
			this.colMin.Name = "colMin";
			this.colMin.OptionsColumn.AllowEdit = false;
			this.colMin.OptionsColumn.ReadOnly = true;
			this.colMin.Visible = true;
			this.colMin.VisibleIndex = 3;
			this.colMin.Width = 162;
			// 
			// colMessage
			// 
			this.colMessage.AppearanceCell.BackColor = System.Drawing.Color.White;
			this.colMessage.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colMessage.AppearanceCell.ForeColor = System.Drawing.Color.Black;
			this.colMessage.AppearanceCell.Options.UseBackColor = true;
			this.colMessage.AppearanceCell.Options.UseFont = true;
			this.colMessage.AppearanceCell.Options.UseForeColor = true;
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
			this.colMessage.VisibleIndex = 6;
			this.colMessage.Width = 205;
			// 
			// replnkMessages
			// 
			this.replnkMessages.AutoHeight = false;
			this.replnkMessages.Name = "replnkMessages";
			// 
			// colResultValue
			// 
			this.colResultValue.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colResultValue.AppearanceCell.Options.UseFont = true;
			this.colResultValue.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colResultValue.AppearanceHeader.Options.UseFont = true;
			this.colResultValue.Caption = "Result Value";
			this.colResultValue.FieldName = "ResultValue";
			this.colResultValue.Name = "colResultValue";
			this.colResultValue.OptionsColumn.AllowEdit = false;
			this.colResultValue.Visible = true;
			this.colResultValue.VisibleIndex = 5;
			this.colResultValue.Width = 118;
			// 
			// SampleMessageDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1005, 462);
			this.Controls.Add(this.grdMessageDetail);
			this.Controls.Add(this.pnlTopBtns);
			this.Name = "SampleMessageDetail";
			this.Text = "Message Detail";
			this.Load += new System.EventHandler(this.SampleMessageDetail_Load);
			this.pnlTopBtns.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdMessageDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdMessageDetailView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceTypeWarning)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replnkMessages)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnExit;
		private DevExpress.XtraGrid.GridControl grdMessageDetail;
		private DevExpress.XtraGrid.Views.Grid.GridView grdMessageDetailView;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSource;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueDataSourceTypeWarning;
		private DevExpress.XtraGrid.Columns.GridColumn colNutrient;
		private DevExpress.XtraGrid.Columns.GridColumn colResultValue;
		private DevExpress.XtraGrid.Columns.GridColumn colGlobalH;
		private DevExpress.XtraGrid.Columns.GridColumn colLimit;
		private DevExpress.XtraGrid.Columns.GridColumn colMax;
		private DevExpress.XtraGrid.Columns.GridColumn colMin;
		private DevExpress.XtraGrid.Columns.GridColumn colMessage;
		private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit replnkMessages;
	}
}