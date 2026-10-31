namespace CFCAIM.Cloud
{
    partial class FurtherAnalysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FurtherAnalysis));
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grdFurtherAnalysis = new DevExpress.XtraGrid.GridControl();
            this.grdFurtherAnalysisView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSampleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repHyperSampleCode = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colSampleDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataSourceTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProcessDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateProcessDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colSampleFurtherAnalysisID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataSourceTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbProcess = new System.Windows.Forms.RadioButton();
            this.rbCreated = new System.Windows.Forms.RadioButton();
            this.rbSample = new System.Windows.Forms.RadioButton();
            this.deBegDate = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.deEndDate = new DevExpress.XtraEditors.DateEdit();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.lueDataSourceType = new DevExpress.XtraEditors.LookUpEdit();
            this.btnGetRecords = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFurtherAnalysis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFurtherAnalysisView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperSampleCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Location = new System.Drawing.Point(-1, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(968, 51);
            this.panel4.TabIndex = 157;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(371, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Further Analysis";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(929, 0);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 113;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnPreview);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Location = new System.Drawing.Point(3, 3);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(187, 38);
            this.pnlTopBtns.TabIndex = 104;
            // 
            // btnPreview
            // 
            this.btnPreview.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreview.Location = new System.Drawing.Point(112, 1);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(57, 37);
            this.btnPreview.TabIndex = 152;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(1, 1);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 89;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(55, 1);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 37);
            this.btnRefresh.TabIndex = 88;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // grdFurtherAnalysis
            // 
            this.grdFurtherAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFurtherAnalysis.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdFurtherAnalysis.Location = new System.Drawing.Point(3, 133);
            this.grdFurtherAnalysis.MainView = this.grdFurtherAnalysisView;
            this.grdFurtherAnalysis.Margin = new System.Windows.Forms.Padding(2);
            this.grdFurtherAnalysis.Name = "grdFurtherAnalysis";
            this.grdFurtherAnalysis.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repHyperSampleCode});
            this.grdFurtherAnalysis.Size = new System.Drawing.Size(959, 487);
            this.grdFurtherAnalysis.TabIndex = 161;
            this.grdFurtherAnalysis.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdFurtherAnalysisView});
            // 
            // grdFurtherAnalysisView
            // 
            this.grdFurtherAnalysisView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdFurtherAnalysisView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdFurtherAnalysisView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdFurtherAnalysisView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdFurtherAnalysisView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSampleCode,
            this.colSampleDate,
            this.colDataSourceTypeName,
            this.colProcessDate,
            this.colUpdateStamp,
            this.colUserName,
            this.colUpdateProcessDate,
            this.colSampleFurtherAnalysisID,
            this.colSampleID,
            this.colDataSourceTypeID,
            this.colCFCUserID});
            this.grdFurtherAnalysisView.GridControl = this.grdFurtherAnalysis;
            this.grdFurtherAnalysisView.Name = "grdFurtherAnalysisView";
            this.grdFurtherAnalysisView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdFurtherAnalysisView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdFurtherAnalysisView.OptionsView.EnableAppearanceEvenRow = true;
            this.grdFurtherAnalysisView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grdFurtherAnalysisView_CustomColumnDisplayText);
            // 
            // colSampleCode
            // 
            this.colSampleCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSampleCode.AppearanceCell.Options.UseFont = true;
            this.colSampleCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSampleCode.AppearanceHeader.Options.UseFont = true;
            this.colSampleCode.Caption = "Sample Code";
            this.colSampleCode.ColumnEdit = this.repHyperSampleCode;
            this.colSampleCode.FieldName = "SampleCode";
            this.colSampleCode.Name = "colSampleCode";
            this.colSampleCode.Visible = true;
            this.colSampleCode.VisibleIndex = 0;
            this.colSampleCode.Width = 106;
            // 
            // repHyperSampleCode
            // 
            this.repHyperSampleCode.AutoHeight = false;
            this.repHyperSampleCode.Name = "repHyperSampleCode";
            this.repHyperSampleCode.Click += new System.EventHandler(this.repHyperSampleCode_Click);
            // 
            // colSampleDate
            // 
            this.colSampleDate.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSampleDate.AppearanceCell.Options.UseFont = true;
            this.colSampleDate.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSampleDate.AppearanceHeader.Options.UseFont = true;
            this.colSampleDate.Caption = "Sample Date";
            this.colSampleDate.FieldName = "SampleDate";
            this.colSampleDate.Name = "colSampleDate";
            this.colSampleDate.OptionsColumn.AllowEdit = false;
            this.colSampleDate.Visible = true;
            this.colSampleDate.VisibleIndex = 1;
            this.colSampleDate.Width = 108;
            // 
            // colDataSourceTypeName
            // 
            this.colDataSourceTypeName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colDataSourceTypeName.AppearanceCell.Options.UseFont = true;
            this.colDataSourceTypeName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colDataSourceTypeName.AppearanceHeader.Options.UseFont = true;
            this.colDataSourceTypeName.Caption = "DataSource Name";
            this.colDataSourceTypeName.FieldName = "DataSourceTypeName";
            this.colDataSourceTypeName.Name = "colDataSourceTypeName";
            this.colDataSourceTypeName.OptionsColumn.AllowEdit = false;
            this.colDataSourceTypeName.Visible = true;
            this.colDataSourceTypeName.VisibleIndex = 2;
            this.colDataSourceTypeName.Width = 147;
            // 
            // colProcessDate
            // 
            this.colProcessDate.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colProcessDate.AppearanceCell.Options.UseFont = true;
            this.colProcessDate.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colProcessDate.AppearanceHeader.Options.UseFont = true;
            this.colProcessDate.Caption = "Process Date";
            this.colProcessDate.DisplayFormat.FormatString = "MM/dd/yy HH:mm:ss";
            this.colProcessDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colProcessDate.FieldName = "ProcessDate";
            this.colProcessDate.Name = "colProcessDate";
            this.colProcessDate.OptionsColumn.AllowEdit = false;
            this.colProcessDate.Visible = true;
            this.colProcessDate.VisibleIndex = 4;
            this.colProcessDate.Width = 153;
            // 
            // colUpdateStamp
            // 
            this.colUpdateStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colUpdateStamp.AppearanceCell.Options.UseFont = true;
            this.colUpdateStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colUpdateStamp.AppearanceHeader.Options.UseFont = true;
            this.colUpdateStamp.Caption = "Created Date";
            this.colUpdateStamp.DisplayFormat.FormatString = "MM/dd/yy HH:mm:ss";
            this.colUpdateStamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colUpdateStamp.FieldName = "UpdateStamp";
            this.colUpdateStamp.Name = "colUpdateStamp";
            this.colUpdateStamp.OptionsColumn.AllowEdit = false;
            this.colUpdateStamp.Visible = true;
            this.colUpdateStamp.VisibleIndex = 3;
            this.colUpdateStamp.Width = 135;
            // 
            // colUserName
            // 
            this.colUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colUserName.AppearanceCell.Options.UseFont = true;
            this.colUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colUserName.AppearanceHeader.Options.UseFont = true;
            this.colUserName.Caption = "User Name";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 5;
            this.colUserName.Width = 151;
            // 
            // colUpdateProcessDate
            // 
            this.colUpdateProcessDate.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colUpdateProcessDate.AppearanceCell.Options.UseFont = true;
            this.colUpdateProcessDate.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colUpdateProcessDate.AppearanceHeader.Options.UseFont = true;
            this.colUpdateProcessDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdateProcessDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdateProcessDate.Caption = "Update Process Date";
            this.colUpdateProcessDate.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colUpdateProcessDate.FieldName = "UpdateProcessDate";
            this.colUpdateProcessDate.Name = "colUpdateProcessDate";
            this.colUpdateProcessDate.OptionsColumn.ShowInCustomizationForm = false;
            this.colUpdateProcessDate.Width = 134;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colSampleFurtherAnalysisID
            // 
            this.colSampleFurtherAnalysisID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSampleFurtherAnalysisID.AppearanceCell.Options.UseFont = true;
            this.colSampleFurtherAnalysisID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSampleFurtherAnalysisID.AppearanceHeader.Options.UseFont = true;
            this.colSampleFurtherAnalysisID.Caption = "SampleFurtherAnalysisID";
            this.colSampleFurtherAnalysisID.FieldName = "SampleFurtherAnalysisID";
            this.colSampleFurtherAnalysisID.Name = "colSampleFurtherAnalysisID";
            // 
            // colSampleID
            // 
            this.colSampleID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSampleID.AppearanceCell.Options.UseFont = true;
            this.colSampleID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSampleID.AppearanceHeader.Options.UseFont = true;
            this.colSampleID.Caption = "SampleID";
            this.colSampleID.FieldName = "SampleID";
            this.colSampleID.Name = "colSampleID";
            // 
            // colDataSourceTypeID
            // 
            this.colDataSourceTypeID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colDataSourceTypeID.AppearanceCell.Options.UseFont = true;
            this.colDataSourceTypeID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colDataSourceTypeID.AppearanceHeader.Options.UseFont = true;
            this.colDataSourceTypeID.Caption = "DataSourceTypeID";
            this.colDataSourceTypeID.FieldName = "DataSourceTypeID";
            this.colDataSourceTypeID.Name = "colDataSourceTypeID";
            this.colDataSourceTypeID.Width = 98;
            // 
            // colCFCUserID
            // 
            this.colCFCUserID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCUserID.AppearanceCell.Options.UseFont = true;
            this.colCFCUserID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colCFCUserID.AppearanceHeader.Options.UseFont = true;
            this.colCFCUserID.Caption = "CFCUserID";
            this.colCFCUserID.FieldName = "CFCUserID";
            this.colCFCUserID.Name = "colCFCUserID";
            this.colCFCUserID.Width = 112;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbProcess);
            this.panel1.Controls.Add(this.rbCreated);
            this.panel1.Controls.Add(this.rbSample);
            this.panel1.Controls.Add(this.deBegDate);
            this.panel1.Controls.Add(this.lblTo);
            this.panel1.Controls.Add(this.lblFrom);
            this.panel1.Controls.Add(this.deEndDate);
            this.panel1.Location = new System.Drawing.Point(12, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 69);
            this.panel1.TabIndex = 190;
            // 
            // rbProcess
            // 
            this.rbProcess.AutoSize = true;
            this.rbProcess.Font = new System.Drawing.Font("Arial", 9F);
            this.rbProcess.Location = new System.Drawing.Point(14, 43);
            this.rbProcess.Name = "rbProcess";
            this.rbProcess.Size = new System.Drawing.Size(100, 19);
            this.rbProcess.TabIndex = 195;
            this.rbProcess.Text = "Process Date";
            this.rbProcess.UseVisualStyleBackColor = true;
            this.rbProcess.CheckedChanged += new System.EventHandler(this.rbProcess_CheckedChanged);
            // 
            // rbCreated
            // 
            this.rbCreated.AutoSize = true;
            this.rbCreated.Font = new System.Drawing.Font("Arial", 9F);
            this.rbCreated.Location = new System.Drawing.Point(14, 25);
            this.rbCreated.Name = "rbCreated";
            this.rbCreated.Size = new System.Drawing.Size(98, 19);
            this.rbCreated.TabIndex = 194;
            this.rbCreated.Text = "Created Date";
            this.rbCreated.UseVisualStyleBackColor = true;
            this.rbCreated.CheckedChanged += new System.EventHandler(this.rbCreated_CheckedChanged);
            // 
            // rbSample
            // 
            this.rbSample.AutoSize = true;
            this.rbSample.Checked = true;
            this.rbSample.Font = new System.Drawing.Font("Arial", 9F);
            this.rbSample.Location = new System.Drawing.Point(14, 7);
            this.rbSample.Name = "rbSample";
            this.rbSample.Size = new System.Drawing.Size(97, 19);
            this.rbSample.TabIndex = 193;
            this.rbSample.TabStop = true;
            this.rbSample.Text = "Sample Date";
            this.rbSample.UseVisualStyleBackColor = true;
            this.rbSample.CheckedChanged += new System.EventHandler(this.rbSample_CheckedChanged);
            // 
            // deBegDate
            // 
            this.deBegDate.EditValue = new System.DateTime(((long)(0)));
            this.deBegDate.Location = new System.Drawing.Point(190, 14);
            this.deBegDate.Name = "deBegDate";
            this.deBegDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deBegDate.Properties.Appearance.Options.UseFont = true;
            this.deBegDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBegDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBegDate.Properties.NullDate = new System.DateTime(((long)(0)));
            this.deBegDate.Size = new System.Drawing.Size(104, 22);
            this.deBegDate.TabIndex = 189;
            this.deBegDate.EditValueChanged += new System.EventHandler(this.deBegDate_EditValueChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(139, 39);
            this.lblTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 15);
            this.lblTo.TabIndex = 192;
            this.lblTo.Text = "To";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(139, 17);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(36, 15);
            this.lblFrom.TabIndex = 191;
            this.lblFrom.Text = "From";
            // 
            // deEndDate
            // 
            this.deEndDate.EditValue = new System.DateTime(((long)(0)));
            this.deEndDate.Location = new System.Drawing.Point(190, 36);
            this.deEndDate.Name = "deEndDate";
            this.deEndDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deEndDate.Properties.Appearance.Options.UseFont = true;
            this.deEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.NullDate = new System.DateTime(((long)(0)));
            this.deEndDate.Size = new System.Drawing.Size(104, 22);
            this.deEndDate.TabIndex = 190;
            this.deEndDate.EditValueChanged += new System.EventHandler(this.deEndDate_EditValueChanged);
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Font = new System.Drawing.Font("Arial", 9F);
            this.lblDataSource.Location = new System.Drawing.Point(351, 80);
            this.lblDataSource.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(75, 15);
            this.lblDataSource.TabIndex = 192;
            this.lblDataSource.Text = "Data Source";
            // 
            // lueDataSourceType
            // 
            this.lueDataSourceType.Location = new System.Drawing.Point(351, 96);
            this.lueDataSourceType.Margin = new System.Windows.Forms.Padding(4);
            this.lueDataSourceType.Name = "lueDataSourceType";
            this.lueDataSourceType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDataSourceType.Properties.Appearance.Options.UseFont = true;
            this.lueDataSourceType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueDataSourceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDataSourceType.Properties.DropDownRows = 25;
            this.lueDataSourceType.Size = new System.Drawing.Size(241, 22);
            this.lueDataSourceType.TabIndex = 191;
            this.lueDataSourceType.EditValueChanged += new System.EventHandler(this.lueDataSourceType_EditValueChanged);
            // 
            // btnGetRecords
            // 
            this.btnGetRecords.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnGetRecords.Image = global::CFCAIM.Properties.Resources.next_32x321;
            this.btnGetRecords.Location = new System.Drawing.Point(621, 80);
            this.btnGetRecords.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetRecords.Name = "btnGetRecords";
            this.btnGetRecords.Size = new System.Drawing.Size(57, 41);
            this.btnGetRecords.TabIndex = 193;
            this.btnGetRecords.UseVisualStyleBackColor = false;
            this.btnGetRecords.Click += new System.EventHandler(this.btnGetRecords_Click);
            // 
            // FurtherAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(965, 622);
            this.Controls.Add(this.btnGetRecords);
            this.Controls.Add(this.lblDataSource);
            this.Controls.Add(this.lueDataSourceType);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grdFurtherAnalysis);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FurtherAnalysis";
            this.Text = "Further Analysis";
            this.Load += new System.EventHandler(this.FurtherAnalysis_Load);
            this.SizeChanged += new System.EventHandler(this.FurtherAnalysis_SizeChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFurtherAnalysis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFurtherAnalysisView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperSampleCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private DevExpress.XtraGrid.GridControl grdFurtherAnalysis;
        private DevExpress.XtraGrid.Views.Grid.GridView grdFurtherAnalysisView;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateProcessDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleFurtherAnalysisID;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleID;
        private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repHyperSampleCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbSample;
        private DevExpress.XtraEditors.DateEdit deBegDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private DevExpress.XtraEditors.DateEdit deEndDate;
        private System.Windows.Forms.RadioButton rbProcess;
        private System.Windows.Forms.RadioButton rbCreated;
        private System.Windows.Forms.Label lblDataSource;
        private DevExpress.XtraEditors.LookUpEdit lueDataSourceType;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnGetRecords;
    }
}