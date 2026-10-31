namespace CFCAIM.Cloud
{
    partial class PendingSample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingSample));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grdPendingSample = new DevExpress.XtraGrid.GridControl();
            this.grdPendingSampleView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ColAnalyzerPendingSampleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repHyperSampleCode = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colCreatedStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSentStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequestCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReceivedStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSpectraName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNIRAnalysis = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleFurtherAnalysisID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDeleteFileType = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbReceived = new System.Windows.Forms.RadioButton();
            this.rbSent = new System.Windows.Forms.RadioButton();
            this.rbCreated = new System.Windows.Forms.RadioButton();
            this.deBegDate = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.deEndDate = new DevExpress.XtraEditors.DateEdit();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnGetRecords = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingSampleView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperSampleCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteFileType)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).BeginInit();
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
            this.label10.Size = new System.Drawing.Size(175, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Pending Samples";
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
            this.pnlTopBtns.Size = new System.Drawing.Size(184, 38);
            this.pnlTopBtns.TabIndex = 104;
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
            // grdPendingSample
            // 
            this.grdPendingSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPendingSample.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdPendingSample.Location = new System.Drawing.Point(3, 133);
            this.grdPendingSample.MainView = this.grdPendingSampleView;
            this.grdPendingSample.Margin = new System.Windows.Forms.Padding(2);
            this.grdPendingSample.Name = "grdPendingSample";
            this.grdPendingSample.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repbtnDeleteFileType,
            this.repHyperSampleCode});
            this.grdPendingSample.Size = new System.Drawing.Size(959, 487);
            this.grdPendingSample.TabIndex = 158;
            this.grdPendingSample.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdPendingSampleView});
            // 
            // grdPendingSampleView
            // 
            this.grdPendingSampleView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdPendingSampleView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdPendingSampleView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdPendingSampleView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdPendingSampleView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColAnalyzerPendingSampleID,
            this.colSampleID,
            this.colSampleCode,
            this.colCreatedStamp,
            this.colSentStamp,
            this.colRequestCode,
            this.colReceivedStamp,
            this.colSpectraName,
            this.colNIRAnalysis,
            this.colSampleFurtherAnalysisID});
            this.grdPendingSampleView.DetailHeight = 280;
            this.grdPendingSampleView.GridControl = this.grdPendingSample;
            this.grdPendingSampleView.Name = "grdPendingSampleView";
            this.grdPendingSampleView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdPendingSampleView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdPendingSampleView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.grdPendingSampleView_CustomColumnDisplayText);
            // 
            // ColAnalyzerPendingSampleID
            // 
            this.ColAnalyzerPendingSampleID.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ColAnalyzerPendingSampleID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColAnalyzerPendingSampleID.AppearanceCell.Options.UseBackColor = true;
            this.ColAnalyzerPendingSampleID.AppearanceCell.Options.UseFont = true;
            this.ColAnalyzerPendingSampleID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColAnalyzerPendingSampleID.AppearanceHeader.Options.UseFont = true;
            this.ColAnalyzerPendingSampleID.Caption = "AnalyzerPendingSampleID";
            this.ColAnalyzerPendingSampleID.FieldName = "AnalyzerPendingSampleID";
            this.ColAnalyzerPendingSampleID.MinWidth = 16;
            this.ColAnalyzerPendingSampleID.Name = "ColAnalyzerPendingSampleID";
            this.ColAnalyzerPendingSampleID.OptionsColumn.AllowEdit = false;
            this.ColAnalyzerPendingSampleID.Width = 107;
            // 
            // colSampleID
            // 
            this.colSampleID.AppearanceCell.BackColor = System.Drawing.Color.White;
            this.colSampleID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSampleID.AppearanceCell.Options.UseBackColor = true;
            this.colSampleID.AppearanceCell.Options.UseFont = true;
            this.colSampleID.AppearanceCell.Options.UseTextOptions = true;
            this.colSampleID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSampleID.AppearanceHeader.Options.UseFont = true;
            this.colSampleID.AppearanceHeader.Options.UseTextOptions = true;
            this.colSampleID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleID.Caption = "SampleID";
            this.colSampleID.FieldName = "SampleID";
            this.colSampleID.MinWidth = 16;
            this.colSampleID.Name = "colSampleID";
            this.colSampleID.OptionsColumn.AllowEdit = false;
            this.colSampleID.Width = 58;
            // 
            // colSampleCode
            // 
            this.colSampleCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSampleCode.AppearanceCell.Options.UseFont = true;
            this.colSampleCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSampleCode.AppearanceHeader.Options.UseFont = true;
            this.colSampleCode.Caption = "Sample Code";
            this.colSampleCode.ColumnEdit = this.repHyperSampleCode;
            this.colSampleCode.FieldName = "SampleCode";
            this.colSampleCode.Name = "colSampleCode";
            this.colSampleCode.Visible = true;
            this.colSampleCode.VisibleIndex = 0;
            this.colSampleCode.Width = 103;
            // 
            // repHyperSampleCode
            // 
            this.repHyperSampleCode.AutoHeight = false;
            this.repHyperSampleCode.Name = "repHyperSampleCode";
            this.repHyperSampleCode.Click += new System.EventHandler(this.repHyperSampleCode_Click);
            // 
            // colCreatedStamp
            // 
            this.colCreatedStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCreatedStamp.AppearanceCell.Options.UseFont = true;
            this.colCreatedStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCreatedStamp.AppearanceHeader.Options.UseFont = true;
            this.colCreatedStamp.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreatedStamp.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreatedStamp.Caption = "Created Date";
            this.colCreatedStamp.FieldName = "CreatedStamp";
            this.colCreatedStamp.MinWidth = 16;
            this.colCreatedStamp.Name = "colCreatedStamp";
            this.colCreatedStamp.OptionsColumn.AllowEdit = false;
            this.colCreatedStamp.OptionsColumn.ShowInCustomizationForm = false;
            this.colCreatedStamp.Visible = true;
            this.colCreatedStamp.VisibleIndex = 1;
            this.colCreatedStamp.Width = 109;
            // 
            // colSentStamp
            // 
            this.colSentStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSentStamp.AppearanceCell.Options.UseFont = true;
            this.colSentStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSentStamp.AppearanceHeader.Options.UseFont = true;
            this.colSentStamp.Caption = "Sent Date";
            this.colSentStamp.FieldName = "SentStamp";
            this.colSentStamp.MinWidth = 16;
            this.colSentStamp.Name = "colSentStamp";
            this.colSentStamp.OptionsColumn.AllowEdit = false;
            this.colSentStamp.Visible = true;
            this.colSentStamp.VisibleIndex = 2;
            this.colSentStamp.Width = 104;
            // 
            // colRequestCode
            // 
            this.colRequestCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colRequestCode.AppearanceCell.Options.UseFont = true;
            this.colRequestCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colRequestCode.AppearanceHeader.Options.UseFont = true;
            this.colRequestCode.Caption = "Request Code";
            this.colRequestCode.FieldName = "RequestCode";
            this.colRequestCode.MinWidth = 16;
            this.colRequestCode.Name = "colRequestCode";
            this.colRequestCode.OptionsColumn.AllowEdit = false;
            this.colRequestCode.OptionsColumn.ShowInCustomizationForm = false;
            this.colRequestCode.Visible = true;
            this.colRequestCode.VisibleIndex = 3;
            this.colRequestCode.Width = 99;
            // 
            // colReceivedStamp
            // 
            this.colReceivedStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colReceivedStamp.AppearanceCell.Options.UseFont = true;
            this.colReceivedStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colReceivedStamp.AppearanceHeader.Options.UseFont = true;
            this.colReceivedStamp.Caption = "Received Date";
            this.colReceivedStamp.FieldName = "ReceivedStamp";
            this.colReceivedStamp.MinWidth = 16;
            this.colReceivedStamp.Name = "colReceivedStamp";
            this.colReceivedStamp.OptionsColumn.AllowEdit = false;
            this.colReceivedStamp.Visible = true;
            this.colReceivedStamp.VisibleIndex = 4;
            this.colReceivedStamp.Width = 115;
            // 
            // colSpectraName
            // 
            this.colSpectraName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSpectraName.AppearanceCell.Options.UseFont = true;
            this.colSpectraName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSpectraName.AppearanceHeader.Options.UseFont = true;
            this.colSpectraName.Caption = "Spectra Name";
            this.colSpectraName.FieldName = "SpectraName";
            this.colSpectraName.Name = "colSpectraName";
            this.colSpectraName.OptionsColumn.AllowEdit = false;
            this.colSpectraName.Visible = true;
            this.colSpectraName.VisibleIndex = 5;
            this.colSpectraName.Width = 196;
            // 
            // colNIRAnalysis
            // 
            this.colNIRAnalysis.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colNIRAnalysis.AppearanceCell.Options.UseFont = true;
            this.colNIRAnalysis.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colNIRAnalysis.AppearanceHeader.Options.UseFont = true;
            this.colNIRAnalysis.Caption = "NIR Analysis";
            this.colNIRAnalysis.FieldName = "NIRAnalysis";
            this.colNIRAnalysis.Name = "colNIRAnalysis";
            this.colNIRAnalysis.OptionsColumn.AllowEdit = false;
            this.colNIRAnalysis.Visible = true;
            this.colNIRAnalysis.VisibleIndex = 6;
            this.colNIRAnalysis.Width = 212;
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
            this.colSampleFurtherAnalysisID.OptionsColumn.AllowEdit = false;
            // 
            // repbtnDeleteFileType
            // 
            this.repbtnDeleteFileType.AutoHeight = false;
            editorButtonImageOptions3.Image = global::CFCAIM.Properties.Resources.delete;
            this.repbtnDeleteFileType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", "Delete", null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDeleteFileType.Name = "repbtnDeleteFileType";
            this.repbtnDeleteFileType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbReceived);
            this.panel1.Controls.Add(this.rbSent);
            this.panel1.Controls.Add(this.rbCreated);
            this.panel1.Controls.Add(this.deBegDate);
            this.panel1.Controls.Add(this.lblTo);
            this.panel1.Controls.Add(this.lblFrom);
            this.panel1.Controls.Add(this.deEndDate);
            this.panel1.Location = new System.Drawing.Point(12, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 69);
            this.panel1.TabIndex = 191;
            // 
            // rbReceived
            // 
            this.rbReceived.AutoSize = true;
            this.rbReceived.Font = new System.Drawing.Font("Arial", 9F);
            this.rbReceived.Location = new System.Drawing.Point(14, 43);
            this.rbReceived.Name = "rbReceived";
            this.rbReceived.Size = new System.Drawing.Size(105, 19);
            this.rbReceived.TabIndex = 195;
            this.rbReceived.Text = "Received Date";
            this.rbReceived.UseVisualStyleBackColor = true;
            this.rbReceived.CheckedChanged += new System.EventHandler(this.rbReceived_CheckedChanged);
            // 
            // rbSent
            // 
            this.rbSent.AutoSize = true;
            this.rbSent.Font = new System.Drawing.Font("Arial", 9F);
            this.rbSent.Location = new System.Drawing.Point(14, 25);
            this.rbSent.Name = "rbSent";
            this.rbSent.Size = new System.Drawing.Size(79, 19);
            this.rbSent.TabIndex = 194;
            this.rbSent.Text = "Sent Date";
            this.rbSent.UseVisualStyleBackColor = true;
            this.rbSent.CheckedChanged += new System.EventHandler(this.rbSent_CheckedChanged);
            // 
            // rbCreated
            // 
            this.rbCreated.AutoSize = true;
            this.rbCreated.Checked = true;
            this.rbCreated.Font = new System.Drawing.Font("Arial", 9F);
            this.rbCreated.Location = new System.Drawing.Point(14, 7);
            this.rbCreated.Name = "rbCreated";
            this.rbCreated.Size = new System.Drawing.Size(98, 19);
            this.rbCreated.TabIndex = 193;
            this.rbCreated.TabStop = true;
            this.rbCreated.Text = "Created Date";
            this.rbCreated.UseVisualStyleBackColor = true;
            this.rbCreated.CheckedChanged += new System.EventHandler(this.rbCreated_CheckedChanged);
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
            // btnPreview
            // 
            this.btnPreview.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreview.Location = new System.Drawing.Point(112, 1);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(57, 37);
            this.btnPreview.TabIndex = 153;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnGetRecords
            // 
            this.btnGetRecords.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnGetRecords.Image = global::CFCAIM.Properties.Resources.next_32x321;
            this.btnGetRecords.Location = new System.Drawing.Point(361, 76);
            this.btnGetRecords.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetRecords.Name = "btnGetRecords";
            this.btnGetRecords.Size = new System.Drawing.Size(57, 41);
            this.btnGetRecords.TabIndex = 192;
            this.btnGetRecords.UseVisualStyleBackColor = false;
            this.btnGetRecords.Click += new System.EventHandler(this.btnGetRecords_Click);
            // 
            // PendingSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(965, 622);
            this.Controls.Add(this.btnGetRecords);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grdPendingSample);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PendingSample";
            this.Text = "Pending Samples";
            this.Load += new System.EventHandler(this.PendingSample_Load);
            this.SizeChanged += new System.EventHandler(this.PendingSample_SizeChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingSampleView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperSampleCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteFileType)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBegDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private DevExpress.XtraGrid.GridControl grdPendingSample;
        private DevExpress.XtraGrid.Views.Grid.GridView grdPendingSampleView;
        private DevExpress.XtraGrid.Columns.GridColumn ColAnalyzerPendingSampleID;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleID;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedStamp;
        private DevExpress.XtraGrid.Columns.GridColumn colSentStamp;
        private DevExpress.XtraGrid.Columns.GridColumn colRequestCode;
        private DevExpress.XtraGrid.Columns.GridColumn colReceivedStamp;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeleteFileType;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repHyperSampleCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSpectraName;
        private DevExpress.XtraGrid.Columns.GridColumn colNIRAnalysis;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleFurtherAnalysisID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbReceived;
        private System.Windows.Forms.RadioButton rbSent;
        private System.Windows.Forms.RadioButton rbCreated;
        private DevExpress.XtraEditors.DateEdit deBegDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private DevExpress.XtraEditors.DateEdit deEndDate;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnGetRecords;
    }
}