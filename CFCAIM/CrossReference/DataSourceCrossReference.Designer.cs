namespace CFCAIM.CrossReference
{
    partial class DataSourceCrossReference
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSourceCrossReference));
            this.grdDataSource = new DevExpress.XtraGrid.GridControl();
            this.grdDataSourceView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDataSourceDetailID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataSourceHeaderID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAnalyzerDataSourceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAnalyzerDataSourceTypeName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colDataSourceTypeName_ADST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueAIMDataSourceTypeName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDeleteAnalyzerDataSourceType = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnPreviewGridResultsAnalyzerDataSource = new System.Windows.Forms.Button();
            this.btnAddAnalyzerDataSource = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lueAnalyzerDataSourceType = new DevExpress.XtraEditors.LookUpEdit();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lueAIMDataSourceType = new DevExpress.XtraEditors.LookUpEdit();
            this.label21 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataSourceView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAnalyzerDataSourceTypeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMDataSourceTypeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteAnalyzerDataSourceType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAnalyzerDataSourceType.Properties)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMDataSourceType.Properties)).BeginInit();
            this.pnlTopBtns.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdDataSource
            // 
            this.grdDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataSource.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdDataSource.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdDataSource.Location = new System.Drawing.Point(3, 136);
            this.grdDataSource.MainView = this.grdDataSourceView;
            this.grdDataSource.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdDataSource.MinimumSize = new System.Drawing.Size(266, 246);
            this.grdDataSource.Name = "grdDataSource";
            this.grdDataSource.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueAIMDataSourceTypeName,
            this.repbtnDeleteAnalyzerDataSourceType,
            this.replueAnalyzerDataSourceTypeName});
            this.grdDataSource.Size = new System.Drawing.Size(829, 252);
            this.grdDataSource.TabIndex = 73;
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
            this.colDataSourceDetailID,
            this.colDataSourceHeaderID,
            this.colAnalyzerDataSourceName,
            this.colDataSourceTypeName_ADST,
            this.gridColumn38,
            this.gridColumn39,
            this.gridColumn41});
            this.grdDataSourceView.GridControl = this.grdDataSource;
            this.grdDataSourceView.Name = "grdDataSourceView";
            this.grdDataSourceView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdDataSourceView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdDataSourceView.OptionsPrint.EnableAppearanceOddRow = true;
            // 
            // colDataSourceDetailID
            // 
            this.colDataSourceDetailID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colDataSourceDetailID.AppearanceCell.Options.UseFont = true;
            this.colDataSourceDetailID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDataSourceDetailID.AppearanceHeader.Options.UseFont = true;
            this.colDataSourceDetailID.Caption = "DataSourceDetailID";
            this.colDataSourceDetailID.FieldName = "DataSourceDetailID";
            this.colDataSourceDetailID.Name = "colDataSourceDetailID";
            this.colDataSourceDetailID.OptionsColumn.ShowInCustomizationForm = false;
            this.colDataSourceDetailID.Width = 117;
            // 
            // colDataSourceHeaderID
            // 
            this.colDataSourceHeaderID.Caption = "DataSourceHeaderID";
            this.colDataSourceHeaderID.FieldName = "DataSourceHeaderID";
            this.colDataSourceHeaderID.Name = "colDataSourceHeaderID";
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
            this.colAnalyzerDataSourceName.VisibleIndex = 0;
            this.colAnalyzerDataSourceName.Width = 476;
            // 
            // replueAnalyzerDataSourceTypeName
            // 
            this.replueAnalyzerDataSourceTypeName.AutoHeight = false;
            this.replueAnalyzerDataSourceTypeName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAnalyzerDataSourceTypeName.Name = "replueAnalyzerDataSourceTypeName";
            // 
            // colDataSourceTypeName_ADST
            // 
            this.colDataSourceTypeName_ADST.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colDataSourceTypeName_ADST.AppearanceCell.Options.UseFont = true;
            this.colDataSourceTypeName_ADST.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDataSourceTypeName_ADST.AppearanceHeader.Options.UseFont = true;
            this.colDataSourceTypeName_ADST.Caption = "AIM Data Source Name";
            this.colDataSourceTypeName_ADST.ColumnEdit = this.replueAIMDataSourceTypeName;
            this.colDataSourceTypeName_ADST.FieldName = "DataSourceTypeID";
            this.colDataSourceTypeName_ADST.Name = "colDataSourceTypeName_ADST";
            this.colDataSourceTypeName_ADST.Visible = true;
            this.colDataSourceTypeName_ADST.VisibleIndex = 1;
            this.colDataSourceTypeName_ADST.Width = 405;
            // 
            // replueAIMDataSourceTypeName
            // 
            this.replueAIMDataSourceTypeName.AutoHeight = false;
            this.replueAIMDataSourceTypeName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueAIMDataSourceTypeName.Name = "replueAIMDataSourceTypeName";
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
            this.repbtnDeleteAnalyzerDataSourceType.Click += new System.EventHandler(this.repbtnDeleteAnalyzerDataSourceType_Click);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdDataSource;
            this.gridView2.Name = "gridView2";
            // 
            // btnPreviewGridResultsAnalyzerDataSource
            // 
            this.btnPreviewGridResultsAnalyzerDataSource.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewGridResultsAnalyzerDataSource.Location = new System.Drawing.Point(767, 82);
            this.btnPreviewGridResultsAnalyzerDataSource.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPreviewGridResultsAnalyzerDataSource.Name = "btnPreviewGridResultsAnalyzerDataSource";
            this.btnPreviewGridResultsAnalyzerDataSource.Size = new System.Drawing.Size(54, 37);
            this.btnPreviewGridResultsAnalyzerDataSource.TabIndex = 72;
            this.btnPreviewGridResultsAnalyzerDataSource.UseVisualStyleBackColor = true;
            this.btnPreviewGridResultsAnalyzerDataSource.Click += new System.EventHandler(this.btnPreviewGridResultsAnalyzerDataSource_Click);
            // 
            // btnAddAnalyzerDataSource
            // 
            this.btnAddAnalyzerDataSource.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAddAnalyzerDataSource.Location = new System.Drawing.Point(709, 82);
            this.btnAddAnalyzerDataSource.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddAnalyzerDataSource.Name = "btnAddAnalyzerDataSource";
            this.btnAddAnalyzerDataSource.Size = new System.Drawing.Size(54, 37);
            this.btnAddAnalyzerDataSource.TabIndex = 71;
            this.btnAddAnalyzerDataSource.UseVisualStyleBackColor = true;
            this.btnAddAnalyzerDataSource.Click += new System.EventHandler(this.btnAddAnalyzerDataSource_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lueAnalyzerDataSourceType);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox5.Location = new System.Drawing.Point(12, 64);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(330, 58);
            this.groupBox5.TabIndex = 69;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Data Source Name";
            // 
            // lueAnalyzerDataSourceType
            // 
            this.lueAnalyzerDataSourceType.Location = new System.Drawing.Point(54, 27);
            this.lueAnalyzerDataSourceType.Name = "lueAnalyzerDataSourceType";
            this.lueAnalyzerDataSourceType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueAnalyzerDataSourceType.Properties.Appearance.Options.UseFont = true;
            this.lueAnalyzerDataSourceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAnalyzerDataSourceType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueAnalyzerDataSourceType.Size = new System.Drawing.Size(273, 22);
            this.lueAnalyzerDataSourceType.TabIndex = 38;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial", 9F);
            this.label18.Location = new System.Drawing.Point(13, 30);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 15);
            this.label18.TabIndex = 39;
            this.label18.Text = "Name";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lueAIMDataSourceType);
            this.groupBox8.Controls.Add(this.label21);
            this.groupBox8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox8.Location = new System.Drawing.Point(348, 64);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(318, 58);
            this.groupBox8.TabIndex = 70;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "AIM Data Source Name";
            // 
            // lueAIMDataSourceType
            // 
            this.lueAIMDataSourceType.Location = new System.Drawing.Point(55, 27);
            this.lueAIMDataSourceType.Name = "lueAIMDataSourceType";
            this.lueAIMDataSourceType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueAIMDataSourceType.Properties.Appearance.Options.UseFont = true;
            this.lueAIMDataSourceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAIMDataSourceType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueAIMDataSourceType.Size = new System.Drawing.Size(258, 22);
            this.lueAIMDataSourceType.TabIndex = 39;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Arial", 9F);
            this.label21.Location = new System.Drawing.Point(14, 30);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 15);
            this.label21.TabIndex = 37;
            this.label21.Text = "Name";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(794, 2);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(30, 26);
            this.btnResize.TabIndex = 114;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Location = new System.Drawing.Point(3, 3);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(116, 39);
            this.pnlTopBtns.TabIndex = 115;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(1, 1);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 37;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(58, 1);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 37);
            this.btnRefresh.TabIndex = 38;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Location = new System.Drawing.Point(5, 3);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(831, 51);
            this.panel4.TabIndex = 155;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(290, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(325, 23);
            this.label3.TabIndex = 149;
            this.label3.Text = "Cross Reference AIM Data Source";
            // 
            // DataSourceCrossReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(836, 390);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.grdDataSource);
            this.Controls.Add(this.btnPreviewGridResultsAnalyzerDataSource);
            this.Controls.Add(this.btnAddAnalyzerDataSource);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox8);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DataSourceCrossReference";
            this.Text = "Cross Reference AIM Data Source";
            this.Load += new System.EventHandler(this.DataSourceCrossReference_Load);
            this.SizeChanged += new System.EventHandler(this.DataSourceCrossReference_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.grdDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataSourceView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAnalyzerDataSourceTypeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueAIMDataSourceTypeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteAnalyzerDataSourceType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAnalyzerDataSourceType.Properties)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMDataSourceType.Properties)).EndInit();
            this.pnlTopBtns.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdDataSource;
        private DevExpress.XtraGrid.Views.Grid.GridView grdDataSourceView;
        private DevExpress.XtraGrid.Columns.GridColumn colDataSourceDetailID;
        private DevExpress.XtraGrid.Columns.GridColumn colAnalyzerDataSourceName;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAnalyzerDataSourceTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeName_ADST;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueAIMDataSourceTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeleteAnalyzerDataSourceType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.Button btnPreviewGridResultsAnalyzerDataSource;
        private System.Windows.Forms.Button btnAddAnalyzerDataSource;
        private System.Windows.Forms.GroupBox groupBox5;
        private DevExpress.XtraEditors.LookUpEdit lueAnalyzerDataSourceType;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox8;
        private DevExpress.XtraEditors.LookUpEdit lueAIMDataSourceType;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSourceHeaderID;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label3;
	}
}