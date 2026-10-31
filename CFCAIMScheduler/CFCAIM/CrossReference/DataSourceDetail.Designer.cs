namespace CFCAIM.CrossReference
{
	partial class DataSourceDetail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSourceDetail));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnResize = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.grdDataSource = new DevExpress.XtraGrid.GridControl();
			this.grdDataSourceView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colDataSourceHeaderID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDataSourceDetailID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDataSourceTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colAnalyzerDataSourceTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDataSourceTypeName_ADST = new DevExpress.XtraGrid.Columns.GridColumn();
			this.replueAIMDataSourceTypeName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.colAnalyzerDataSourceName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.replueAnalyzerDataSourceTypeName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repbtnDeleteAnalyzerDataSourceType = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.btnPreviewGridResultsAnalyzerDataSource = new System.Windows.Forms.Button();
			this.btnAddAnalyzerDataSource = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lueAnalyzerDataSourceType = new DevExpress.XtraEditors.LookUpEdit();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lueAIMDataSourceType = new DevExpress.XtraEditors.LookUpEdit();
			this.label2 = new System.Windows.Forms.Label();
			this.pnlTopBtns.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdDataSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdDataSourceView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueAIMDataSourceTypeName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueAnalyzerDataSourceTypeName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteAnalyzerDataSourceType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueAnalyzerDataSourceType.Properties)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueAIMDataSourceType.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Controls.Add(this.btnRefresh);
			this.pnlTopBtns.Location = new System.Drawing.Point(4, 6);
			this.pnlTopBtns.Margin = new System.Windows.Forms.Padding(4);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(155, 60);
			this.pnlTopBtns.TabIndex = 123;
			// 
			// btnExit
			// 
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(7, 7);
			this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(67, 46);
			this.btnExit.TabIndex = 37;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
			this.btnRefresh.Location = new System.Drawing.Point(79, 7);
			this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(71, 46);
			this.btnRefresh.TabIndex = 38;
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// btnResize
			// 
			this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
			this.btnResize.Location = new System.Drawing.Point(1069, 6);
			this.btnResize.Name = "btnResize";
			this.btnResize.Size = new System.Drawing.Size(38, 33);
			this.btnResize.TabIndex = 122;
			this.btnResize.UseVisualStyleBackColor = true;
			this.btnResize.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.label1.ForeColor = System.Drawing.Color.Maroon;
			this.label1.Location = new System.Drawing.Point(358, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(331, 24);
			this.label1.TabIndex = 121;
			this.label1.Text = "Cross Reference AIM Data Source";
			// 
			// grdDataSource
			// 
			this.grdDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdDataSource.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdDataSource.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdDataSource.Location = new System.Drawing.Point(1, 169);
			this.grdDataSource.MainView = this.grdDataSourceView;
			this.grdDataSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.grdDataSource.MinimumSize = new System.Drawing.Size(333, 308);
			this.grdDataSource.Name = "grdDataSource";
			this.grdDataSource.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueAIMDataSourceTypeName,
            this.repbtnDeleteAnalyzerDataSourceType,
            this.replueAnalyzerDataSourceTypeName});
			this.grdDataSource.Size = new System.Drawing.Size(1042, 334);
			this.grdDataSource.TabIndex = 120;
			this.grdDataSource.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDataSourceView,
            this.gridView2});
			// 
			// grdDataSourceView
			// 
			this.grdDataSourceView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.ColumnFilterButton.Options.UseFont = true;
			this.grdDataSourceView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
			this.grdDataSourceView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.CustomizationFormHint.Options.UseFont = true;
			this.grdDataSourceView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.DetailTip.Options.UseFont = true;
			this.grdDataSourceView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.Empty.Options.UseFont = true;
			this.grdDataSourceView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.EvenRow.Options.UseFont = true;
			this.grdDataSourceView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.FilterCloseButton.Options.UseFont = true;
			this.grdDataSourceView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.FilterPanel.Options.UseFont = true;
			this.grdDataSourceView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.FixedLine.Options.UseFont = true;
			this.grdDataSourceView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.FocusedCell.Options.UseFont = true;
			this.grdDataSourceView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.FocusedRow.Options.UseFont = true;
			this.grdDataSourceView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.FooterPanel.Options.UseFont = true;
			this.grdDataSourceView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.GroupButton.Options.UseFont = true;
			this.grdDataSourceView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.GroupFooter.Options.UseFont = true;
			this.grdDataSourceView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.GroupPanel.Options.UseFont = true;
			this.grdDataSourceView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.GroupRow.Options.UseFont = true;
			this.grdDataSourceView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.HeaderPanel.Options.UseFont = true;
			this.grdDataSourceView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.HideSelectionRow.Options.UseFont = true;
			this.grdDataSourceView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.HorzLine.Options.UseFont = true;
			this.grdDataSourceView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.OddRow.Options.UseFont = true;
			this.grdDataSourceView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.Preview.Options.UseFont = true;
			this.grdDataSourceView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.Row.Options.UseFont = true;
			this.grdDataSourceView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.RowSeparator.Options.UseFont = true;
			this.grdDataSourceView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.SelectedRow.Options.UseFont = true;
			this.grdDataSourceView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.TopNewRow.Options.UseFont = true;
			this.grdDataSourceView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.VertLine.Options.UseFont = true;
			this.grdDataSourceView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.Appearance.ViewCaption.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.grdDataSourceView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.grdDataSourceView.AppearancePrint.EvenRow.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.FilterPanel.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.GroupRow.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.Lines.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.grdDataSourceView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.grdDataSourceView.AppearancePrint.OddRow.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.Preview.Options.UseFont = true;
			this.grdDataSourceView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
			this.grdDataSourceView.AppearancePrint.Row.Options.UseFont = true;
			this.grdDataSourceView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDataSourceHeaderID,
            this.colDataSourceDetailID,
            this.colDataSourceTypeID,
            this.colAnalyzerDataSourceTypeID,
            this.colDataSourceTypeName_ADST,
            this.colAnalyzerDataSourceName,
            this.gridColumn40,
            this.gridColumn38,
            this.gridColumn39,
            this.gridColumn41});
			this.grdDataSourceView.GridControl = this.grdDataSource;
			this.grdDataSourceView.Name = "grdDataSourceView";
			this.grdDataSourceView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
			this.grdDataSourceView.OptionsPrint.EnableAppearanceEvenRow = true;
			this.grdDataSourceView.OptionsPrint.EnableAppearanceOddRow = true;
			// 
			// colDataSourceHeaderID
			// 
			this.colDataSourceHeaderID.Caption = "gridColumn1";
			this.colDataSourceHeaderID.FieldName = "DataSourceHeaderID";
			this.colDataSourceHeaderID.Name = "colDataSourceHeaderID";
			// 
			// colDataSourceDetailID
			// 
			this.colDataSourceDetailID.Caption = "DataSourceDetailID";
			this.colDataSourceDetailID.FieldName = "DataSourceDetailID";
			this.colDataSourceDetailID.Name = "colDataSourceDetailID";
			// 
			// colDataSourceTypeID
			// 
			this.colDataSourceTypeID.Caption = "DataSourceTypeID";
			this.colDataSourceTypeID.FieldName = "DataSourceTypeID";
			this.colDataSourceTypeID.Name = "colDataSourceTypeID";
			// 
			// colAnalyzerDataSourceTypeID
			// 
			this.colAnalyzerDataSourceTypeID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colAnalyzerDataSourceTypeID.AppearanceCell.Options.UseFont = true;
			this.colAnalyzerDataSourceTypeID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colAnalyzerDataSourceTypeID.AppearanceHeader.Options.UseFont = true;
			this.colAnalyzerDataSourceTypeID.Caption = "Analyzer DataSource TypeID";
			this.colAnalyzerDataSourceTypeID.FieldName = "AnalyzerDataSourceTypeID";
			this.colAnalyzerDataSourceTypeID.Name = "colAnalyzerDataSourceTypeID";
			this.colAnalyzerDataSourceTypeID.OptionsColumn.ShowInCustomizationForm = false;
			this.colAnalyzerDataSourceTypeID.Width = 117;
			// 
			// colDataSourceTypeName_ADST
			// 
			this.colDataSourceTypeName_ADST.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colDataSourceTypeName_ADST.AppearanceCell.Options.UseFont = true;
			this.colDataSourceTypeName_ADST.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colDataSourceTypeName_ADST.AppearanceHeader.Options.UseFont = true;
			this.colDataSourceTypeName_ADST.Caption = "AIM Data Source Name";
			this.colDataSourceTypeName_ADST.ColumnEdit = this.replueAIMDataSourceTypeName;
			this.colDataSourceTypeName_ADST.FieldName = "AIMDataSourceTypeID";
			this.colDataSourceTypeName_ADST.Name = "colDataSourceTypeName_ADST";
			this.colDataSourceTypeName_ADST.Visible = true;
			this.colDataSourceTypeName_ADST.VisibleIndex = 0;
			this.colDataSourceTypeName_ADST.Width = 405;
			// 
			// replueAIMDataSourceTypeName
			// 
			this.replueAIMDataSourceTypeName.AutoHeight = false;
			this.replueAIMDataSourceTypeName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueAIMDataSourceTypeName.Name = "replueAIMDataSourceTypeName";
			// 
			// colAnalyzerDataSourceName
			// 
			this.colAnalyzerDataSourceName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colAnalyzerDataSourceName.AppearanceHeader.Options.UseFont = true;
			this.colAnalyzerDataSourceName.Caption = "Data Source Name";
			this.colAnalyzerDataSourceName.ColumnEdit = this.replueAnalyzerDataSourceTypeName;
			this.colAnalyzerDataSourceName.FieldName = "AnalyzerDataSourceTypeID";
			this.colAnalyzerDataSourceName.Name = "colAnalyzerDataSourceName";
			this.colAnalyzerDataSourceName.Visible = true;
			this.colAnalyzerDataSourceName.VisibleIndex = 1;
			this.colAnalyzerDataSourceName.Width = 476;
			// 
			// replueAnalyzerDataSourceTypeName
			// 
			this.replueAnalyzerDataSourceTypeName.AutoHeight = false;
			this.replueAnalyzerDataSourceTypeName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueAnalyzerDataSourceTypeName.Name = "replueAnalyzerDataSourceTypeName";
			// 
			// gridColumn40
			// 
			this.gridColumn40.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn40.AppearanceCell.Options.UseFont = true;
			this.gridColumn40.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.gridColumn40.AppearanceHeader.Options.UseFont = true;
			this.gridColumn40.Caption = "Data Source";
			this.gridColumn40.FieldName = "DataSourceUserName";
			this.gridColumn40.Name = "gridColumn40";
			this.gridColumn40.OptionsColumn.AllowEdit = false;
			this.gridColumn40.Width = 158;
			// 
			// gridColumn38
			// 
			this.gridColumn38.AppearanceCell.BackColor = System.Drawing.Color.White;
			this.gridColumn38.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn38.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn38.AppearanceCell.Options.UseFont = true;
			this.gridColumn38.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumn38.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn38.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn38.AppearanceHeader.Options.UseFont = true;
			this.gridColumn38.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn38.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn38.Caption = "User Name";
			this.gridColumn38.FieldName = "UserName";
			this.gridColumn38.Name = "gridColumn38";
			this.gridColumn38.OptionsColumn.AllowEdit = false;
			this.gridColumn38.Width = 140;
			// 
			// gridColumn39
			// 
			this.gridColumn39.AppearanceCell.BackColor = System.Drawing.Color.White;
			this.gridColumn39.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn39.AppearanceCell.Options.UseBackColor = true;
			this.gridColumn39.AppearanceCell.Options.UseFont = true;
			this.gridColumn39.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn39.AppearanceHeader.Options.UseFont = true;
			this.gridColumn39.Caption = "Update Stamp";
			this.gridColumn39.DisplayFormat.FormatString = "g";
			this.gridColumn39.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.gridColumn39.FieldName = "UpdateStamp";
			this.gridColumn39.Name = "gridColumn39";
			this.gridColumn39.OptionsColumn.AllowEdit = false;
			this.gridColumn39.OptionsColumn.ReadOnly = true;
			this.gridColumn39.OptionsColumn.TabStop = false;
			this.gridColumn39.Width = 137;
			// 
			// gridColumn41
			// 
			this.gridColumn41.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.gridColumn41.AppearanceCell.Options.UseFont = true;
			this.gridColumn41.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.gridColumn41.AppearanceHeader.Options.UseFont = true;
			this.gridColumn41.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn41.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn41.Caption = "Delete";
			this.gridColumn41.ColumnEdit = this.repbtnDeleteAnalyzerDataSourceType;
			this.gridColumn41.FieldName = "colDelete";
			this.gridColumn41.Name = "gridColumn41";
			this.gridColumn41.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
			this.gridColumn41.UnboundType = DevExpress.Data.UnboundColumnType.Object;
			this.gridColumn41.Visible = true;
			this.gridColumn41.VisibleIndex = 2;
			this.gridColumn41.Width = 135;
			// 
			// repbtnDeleteAnalyzerDataSourceType
			// 
			this.repbtnDeleteAnalyzerDataSourceType.AutoHeight = false;
			this.repbtnDeleteAnalyzerDataSourceType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::CFCAIM.Properties.Resources.delete, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", "Delete Record", null, true)});
			this.repbtnDeleteAnalyzerDataSourceType.Name = "repbtnDeleteAnalyzerDataSourceType";
			this.repbtnDeleteAnalyzerDataSourceType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			// 
			// gridView2
			// 
			this.gridView2.GridControl = this.grdDataSource;
			this.gridView2.Name = "gridView2";
			// 
			// btnPreviewGridResultsAnalyzerDataSource
			// 
			this.btnPreviewGridResultsAnalyzerDataSource.Image = global::CFCAIM.Properties.Resources.preview_32x32;
			this.btnPreviewGridResultsAnalyzerDataSource.Location = new System.Drawing.Point(956, 105);
			this.btnPreviewGridResultsAnalyzerDataSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnPreviewGridResultsAnalyzerDataSource.Name = "btnPreviewGridResultsAnalyzerDataSource";
			this.btnPreviewGridResultsAnalyzerDataSource.Size = new System.Drawing.Size(67, 46);
			this.btnPreviewGridResultsAnalyzerDataSource.TabIndex = 119;
			this.btnPreviewGridResultsAnalyzerDataSource.UseVisualStyleBackColor = true;
			// 
			// btnAddAnalyzerDataSource
			// 
			this.btnAddAnalyzerDataSource.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
			this.btnAddAnalyzerDataSource.Location = new System.Drawing.Point(883, 105);
			this.btnAddAnalyzerDataSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnAddAnalyzerDataSource.Name = "btnAddAnalyzerDataSource";
			this.btnAddAnalyzerDataSource.Size = new System.Drawing.Size(67, 46);
			this.btnAddAnalyzerDataSource.TabIndex = 118;
			this.btnAddAnalyzerDataSource.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lueAnalyzerDataSourceType);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.groupBox2.Location = new System.Drawing.Point(422, 90);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox2.Size = new System.Drawing.Size(413, 73);
			this.groupBox2.TabIndex = 116;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Data Source Name";
			// 
			// lueAnalyzerDataSourceType
			// 
			this.lueAnalyzerDataSourceType.Location = new System.Drawing.Point(68, 34);
			this.lueAnalyzerDataSourceType.Margin = new System.Windows.Forms.Padding(4);
			this.lueAnalyzerDataSourceType.Name = "lueAnalyzerDataSourceType";
			this.lueAnalyzerDataSourceType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.lueAnalyzerDataSourceType.Properties.Appearance.Options.UseFont = true;
			this.lueAnalyzerDataSourceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lueAnalyzerDataSourceType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			this.lueAnalyzerDataSourceType.Size = new System.Drawing.Size(341, 24);
			this.lueAnalyzerDataSourceType.TabIndex = 40;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 9F);
			this.label3.Location = new System.Drawing.Point(16, 38);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 17);
			this.label3.TabIndex = 39;
			this.label3.Text = "Name";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lueAIMDataSourceType);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
			this.groupBox1.Location = new System.Drawing.Point(1, 90);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(397, 73);
			this.groupBox1.TabIndex = 117;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "AIM Data Source Name";
			// 
			// lueAIMDataSourceType
			// 
			this.lueAIMDataSourceType.Location = new System.Drawing.Point(69, 34);
			this.lueAIMDataSourceType.Margin = new System.Windows.Forms.Padding(4);
			this.lueAIMDataSourceType.Name = "lueAIMDataSourceType";
			this.lueAIMDataSourceType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.lueAIMDataSourceType.Properties.Appearance.Options.UseFont = true;
			this.lueAIMDataSourceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lueAIMDataSourceType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			this.lueAIMDataSourceType.Size = new System.Drawing.Size(323, 24);
			this.lueAIMDataSourceType.TabIndex = 38;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9F);
			this.label2.Location = new System.Drawing.Point(17, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 17);
			this.label2.TabIndex = 37;
			this.label2.Text = "Name";
			// 
			// DataSourceDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(1044, 506);
			this.Controls.Add(this.pnlTopBtns);
			this.Controls.Add(this.btnResize);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.grdDataSource);
			this.Controls.Add(this.btnPreviewGridResultsAnalyzerDataSource);
			this.Controls.Add(this.btnAddAnalyzerDataSource);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DataSourceDetail";
			this.Text = "Cross Reference AIM Data Source";
			this.Load += new System.EventHandler(this.DataSourceDetail_Load);
			this.SizeChanged += new System.EventHandler(this.DataSourceCrossReference_SizeChanged);
			this.pnlTopBtns.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdDataSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdDataSourceView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueAIMDataSourceTypeName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueAnalyzerDataSourceTypeName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteAnalyzerDataSourceType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueAnalyzerDataSourceType.Properties)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueAIMDataSourceType.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnResize;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraGrid.GridControl grdDataSource;
		private DevExpress.XtraGrid.Views.Grid.GridView grdDataSourceView;
		private DevExpress.XtraGrid.Columns.GridColumn colAnalyzerDataSourceTypeID;
		private DevExpress.XtraGrid.Columns.GridColumn colAnalyzerDataSourceName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAnalyzerDataSourceTypeName;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeName_ADST;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMDataSourceTypeName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeleteAnalyzerDataSourceType;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSourceHeaderID;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
		private System.Windows.Forms.Button btnPreviewGridResultsAnalyzerDataSource;
		private System.Windows.Forms.Button btnAddAnalyzerDataSource;
		private System.Windows.Forms.GroupBox groupBox2;
		private DevExpress.XtraEditors.LookUpEdit lueAnalyzerDataSourceType;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private DevExpress.XtraEditors.LookUpEdit lueAIMDataSourceType;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSourceDetailID;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeID;
	}
}