namespace CFCAIM.Sample
{
    partial class SampleFurtherAnalysisView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleFurtherAnalysisView));
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grdFurtherAnalysis = new DevExpress.XtraGrid.GridControl();
            this.grdFurtherAnalysisView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSampleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataSourceTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProcessDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMessage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateProcessDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colSampleFurtherAnalysisID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataSourceTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repHyperSampleCode = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.deBegDate = new DevExpress.XtraEditors.DateEdit();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.deEndDate = new DevExpress.XtraEditors.DateEdit();
            this.btnGetRecords = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnUpdateProcessDate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbUnprocessed = new System.Windows.Forms.RadioButton();
            this.rbShowAll = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSampleCode = new System.Windows.Forms.TextBox();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFurtherAnalysis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFurtherAnalysisView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperSampleCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnPreview);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1284, 51);
            this.panel4.TabIndex = 158;
            // 
            // btnPreview
            // 
            this.btnPreview.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreview.Location = new System.Drawing.Point(124, 5);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(57, 37);
            this.btnPreview.TabIndex = 150;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(394, 9);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(224, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Further Analysis - View";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(1240, 2);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(35, 35);
            this.btnResize.TabIndex = 112;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(5, 5);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(114, 37);
            this.pnlTopBtns.TabIndex = 30;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(58, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 37);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 26;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grdFurtherAnalysis
            // 
            this.grdFurtherAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFurtherAnalysis.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdFurtherAnalysis.Location = new System.Drawing.Point(3, 128);
            this.grdFurtherAnalysis.MainView = this.grdFurtherAnalysisView;
            this.grdFurtherAnalysis.Margin = new System.Windows.Forms.Padding(2);
            this.grdFurtherAnalysis.Name = "grdFurtherAnalysis";
            this.grdFurtherAnalysis.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repHyperSampleCode});
            this.grdFurtherAnalysis.Size = new System.Drawing.Size(1276, 490);
            this.grdFurtherAnalysis.TabIndex = 160;
            this.grdFurtherAnalysis.TabStop = false;
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
            this.colMessage,
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
            this.colSampleCode.Caption = "Sample Code";
            this.colSampleCode.FieldName = "SampleCode";
            this.colSampleCode.Name = "colSampleCode";
            this.colSampleCode.OptionsColumn.AllowEdit = false;
            this.colSampleCode.Visible = true;
            this.colSampleCode.VisibleIndex = 0;
            this.colSampleCode.Width = 118;
            // 
            // colSampleDate
            // 
            this.colSampleDate.Caption = "Sample Date";
            this.colSampleDate.FieldName = "SampleDate";
            this.colSampleDate.Name = "colSampleDate";
            this.colSampleDate.OptionsColumn.AllowEdit = false;
            this.colSampleDate.Visible = true;
            this.colSampleDate.VisibleIndex = 1;
            this.colSampleDate.Width = 120;
            // 
            // colDataSourceTypeName
            // 
            this.colDataSourceTypeName.Caption = "DataSource Name";
            this.colDataSourceTypeName.FieldName = "DataSourceTypeName";
            this.colDataSourceTypeName.Name = "colDataSourceTypeName";
            this.colDataSourceTypeName.OptionsColumn.AllowEdit = false;
            this.colDataSourceTypeName.Visible = true;
            this.colDataSourceTypeName.VisibleIndex = 2;
            this.colDataSourceTypeName.Width = 165;
            // 
            // colProcessDate
            // 
            this.colProcessDate.Caption = "Process Date";
            this.colProcessDate.DisplayFormat.FormatString = "MM/dd/yy HH:mm:ss";
            this.colProcessDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colProcessDate.FieldName = "ProcessDate";
            this.colProcessDate.Name = "colProcessDate";
            this.colProcessDate.OptionsColumn.AllowEdit = false;
            this.colProcessDate.Visible = true;
            this.colProcessDate.VisibleIndex = 4;
            this.colProcessDate.Width = 129;
            // 
            // colUpdateStamp
            // 
            this.colUpdateStamp.Caption = "Created Date";
            this.colUpdateStamp.DisplayFormat.FormatString = "MM/dd/yy HH:mm:ss";
            this.colUpdateStamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colUpdateStamp.FieldName = "UpdateStamp";
            this.colUpdateStamp.Name = "colUpdateStamp";
            this.colUpdateStamp.OptionsColumn.AllowEdit = false;
            this.colUpdateStamp.Visible = true;
            this.colUpdateStamp.VisibleIndex = 3;
            this.colUpdateStamp.Width = 125;
            // 
            // colMessage
            // 
            this.colMessage.Caption = "Message";
            this.colMessage.FieldName = "Message";
            this.colMessage.Name = "colMessage";
            this.colMessage.Visible = true;
            this.colMessage.VisibleIndex = 5;
            this.colMessage.Width = 350;
            // 
            // colUserName
            // 
            this.colUserName.Caption = "User Name";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 6;
            this.colUserName.Width = 124;
            // 
            // colUpdateProcessDate
            // 
            this.colUpdateProcessDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdateProcessDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdateProcessDate.Caption = "Update Process Date";
            this.colUpdateProcessDate.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colUpdateProcessDate.FieldName = "UpdateProcessDate";
            this.colUpdateProcessDate.Name = "colUpdateProcessDate";
            this.colUpdateProcessDate.OptionsColumn.ShowInCustomizationForm = false;
            this.colUpdateProcessDate.Visible = true;
            this.colUpdateProcessDate.VisibleIndex = 7;
            this.colUpdateProcessDate.Width = 120;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colSampleFurtherAnalysisID
            // 
            this.colSampleFurtherAnalysisID.Caption = "SampleFurtherAnalysisID";
            this.colSampleFurtherAnalysisID.FieldName = "SampleFurtherAnalysisID";
            this.colSampleFurtherAnalysisID.Name = "colSampleFurtherAnalysisID";
            // 
            // colSampleID
            // 
            this.colSampleID.Caption = "SampleID";
            this.colSampleID.FieldName = "SampleID";
            this.colSampleID.Name = "colSampleID";
            // 
            // colDataSourceTypeID
            // 
            this.colDataSourceTypeID.Caption = "DataSourceTypeID";
            this.colDataSourceTypeID.FieldName = "DataSourceTypeID";
            this.colDataSourceTypeID.Name = "colDataSourceTypeID";
            this.colDataSourceTypeID.Width = 98;
            // 
            // colCFCUserID
            // 
            this.colCFCUserID.Caption = "CFCUserID";
            this.colCFCUserID.FieldName = "CFCUserID";
            this.colCFCUserID.Name = "colCFCUserID";
            this.colCFCUserID.Width = 112;
            // 
            // repHyperSampleCode
            // 
            this.repHyperSampleCode.AutoHeight = false;
            this.repHyperSampleCode.Name = "repHyperSampleCode";
            // 
            // deBegDate
            // 
            this.deBegDate.EditValue = new System.DateTime(((long)(0)));
            this.deBegDate.Location = new System.Drawing.Point(104, 81);
            this.deBegDate.Name = "deBegDate";
            this.deBegDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deBegDate.Properties.Appearance.Options.UseFont = true;
            this.deBegDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBegDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBegDate.Properties.NullDate = new System.DateTime(((long)(0)));
            this.deBegDate.Size = new System.Drawing.Size(104, 22);
            this.deBegDate.TabIndex = 161;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(22, 65);
            this.lblDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(80, 15);
            this.lblDate.TabIndex = 165;
            this.lblDate.Text = "Created Date";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(45, 106);
            this.lblTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 15);
            this.lblTo.TabIndex = 164;
            this.lblTo.Text = "To";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(45, 84);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(36, 15);
            this.lblFrom.TabIndex = 163;
            this.lblFrom.Text = "From";
            // 
            // deEndDate
            // 
            this.deEndDate.EditValue = new System.DateTime(((long)(0)));
            this.deEndDate.Location = new System.Drawing.Point(104, 103);
            this.deEndDate.Name = "deEndDate";
            this.deEndDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deEndDate.Properties.Appearance.Options.UseFont = true;
            this.deEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndDate.Properties.NullDate = new System.DateTime(((long)(0)));
            this.deEndDate.Size = new System.Drawing.Size(104, 22);
            this.deEndDate.TabIndex = 162;
            // 
            // btnGetRecords
            // 
            this.btnGetRecords.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnGetRecords.Image = global::CFCAIM.Properties.Resources.next_32x321;
            this.btnGetRecords.Location = new System.Drawing.Point(354, 81);
            this.btnGetRecords.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetRecords.Name = "btnGetRecords";
            this.btnGetRecords.Size = new System.Drawing.Size(57, 41);
            this.btnGetRecords.TabIndex = 184;
            this.btnGetRecords.UseVisualStyleBackColor = false;
            this.btnGetRecords.Click += new System.EventHandler(this.btnGetRecords_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.Image = global::CFCAIM.Properties.Resources.apply_16x16;
            this.btnCheck.Location = new System.Drawing.Point(1148, 87);
            this.btnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(42, 37);
            this.btnCheck.TabIndex = 185;
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnAcceptAllFiles_Click);
            // 
            // btnUpdateProcessDate
            // 
            this.btnUpdateProcessDate.Image = global::CFCAIM.Properties.Resources.save_32x32;
            this.btnUpdateProcessDate.Location = new System.Drawing.Point(877, 87);
            this.btnUpdateProcessDate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateProcessDate.Name = "btnUpdateProcessDate";
            this.btnUpdateProcessDate.Size = new System.Drawing.Size(42, 37);
            this.btnUpdateProcessDate.TabIndex = 186;
            this.btnUpdateProcessDate.UseVisualStyleBackColor = true;
            this.btnUpdateProcessDate.Click += new System.EventHandler(this.btnUpdateProcessDate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbUnprocessed);
            this.groupBox1.Controls.Add(this.rbShowAll);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(527, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 44);
            this.groupBox1.TabIndex = 192;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Process Date";
            // 
            // rbUnprocessed
            // 
            this.rbUnprocessed.AutoSize = true;
            this.rbUnprocessed.Checked = true;
            this.rbUnprocessed.Font = new System.Drawing.Font("Arial", 9F);
            this.rbUnprocessed.Location = new System.Drawing.Point(32, 19);
            this.rbUnprocessed.Name = "rbUnprocessed";
            this.rbUnprocessed.Size = new System.Drawing.Size(100, 19);
            this.rbUnprocessed.TabIndex = 197;
            this.rbUnprocessed.TabStop = true;
            this.rbUnprocessed.Text = "Unprocessed";
            this.rbUnprocessed.UseVisualStyleBackColor = true;
            this.rbUnprocessed.CheckedChanged += new System.EventHandler(this.rbUnprocessed_CheckedChanged);
            // 
            // rbShowAll
            // 
            this.rbShowAll.AutoSize = true;
            this.rbShowAll.Font = new System.Drawing.Font("Arial", 9F);
            this.rbShowAll.Location = new System.Drawing.Point(151, 19);
            this.rbShowAll.Name = "rbShowAll";
            this.rbShowAll.Size = new System.Drawing.Size(71, 19);
            this.rbShowAll.TabIndex = 196;
            this.rbShowAll.Text = "Show All";
            this.rbShowAll.UseVisualStyleBackColor = true;
            this.rbShowAll.CheckedChanged += new System.EventHandler(this.rbShowAll_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(226, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 193;
            this.label1.Text = "Sample Code";
            // 
            // txtSampleCode
            // 
            this.txtSampleCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSampleCode.Location = new System.Drawing.Point(229, 82);
            this.txtSampleCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtSampleCode.Name = "txtSampleCode";
            this.txtSampleCode.Size = new System.Drawing.Size(103, 21);
            this.txtSampleCode.TabIndex = 194;
            this.txtSampleCode.TabStop = false;
            // 
            // SampleFurtherAnalysisView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 622);
            this.Controls.Add(this.txtSampleCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnUpdateProcessDate);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnGetRecords);
            this.Controls.Add(this.deBegDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.deEndDate);
            this.Controls.Add(this.grdFurtherAnalysis);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SampleFurtherAnalysisView";
            this.Text = "Further Analysis View";
            this.Load += new System.EventHandler(this.SampleFurtherAnalysisView_Load);
            this.SizeChanged += new System.EventHandler(this.SampleFurtherAnalysisView_SizeChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFurtherAnalysis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFurtherAnalysisView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperSampleCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private DevExpress.XtraGrid.GridControl grdFurtherAnalysis;
        private DevExpress.XtraGrid.Views.Grid.GridView grdFurtherAnalysisView;
        private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateProcessDate;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleID;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleDate;
        private DevExpress.XtraEditors.DateEdit deBegDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private DevExpress.XtraEditors.DateEdit deEndDate;
        private System.Windows.Forms.Button btnGetRecords;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnUpdateProcessDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleFurtherAnalysisID;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repHyperSampleCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbUnprocessed;
        private System.Windows.Forms.RadioButton rbShowAll;
        private DevExpress.XtraGrid.Columns.GridColumn colMessage;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSampleCode;
    }
}