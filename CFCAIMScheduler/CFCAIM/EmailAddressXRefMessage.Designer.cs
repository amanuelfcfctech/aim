namespace CFCAIM
{
	partial class EmailAddressXRefMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailAddressXRefMessage));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.cmbMessage = new System.Windows.Forms.ComboBox();
            this.btnPreviewResults = new System.Windows.Forms.Button();
            this.lblAIMNureint = new System.Windows.Forms.Label();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.lueCFCUsers = new DevExpress.XtraEditors.LookUpEdit();
            this.btnAddWarningLimit = new System.Windows.Forms.Button();
            this.grdEmailMessageXRef = new DevExpress.XtraGrid.GridControl();
            this.grdEmailMessageXRefView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colEmailAddressXRefMessageID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCUserName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.ColEmailAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMessageID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMessageDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlTopBtns.SuspendLayout();
            this.pnlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCUsers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmailMessageXRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmailMessageXRefView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnPrintPreview);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Controls.Add(this.btnPrint);
            this.pnlTopBtns.Location = new System.Drawing.Point(3, 5);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(238, 38);
            this.pnlTopBtns.TabIndex = 53;
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPrintPreview.Location = new System.Drawing.Point(180, 0);
            this.btnPrintPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(57, 37);
            this.btnPrintPreview.TabIndex = 46;
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 33;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(58, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 37);
            this.btnRefresh.TabIndex = 34;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(119, 0);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(57, 37);
            this.btnPrint.TabIndex = 45;
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // pnlAdd
            // 
            this.pnlAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAdd.Controls.Add(this.cmbMessage);
            this.pnlAdd.Controls.Add(this.btnPreviewResults);
            this.pnlAdd.Controls.Add(this.lblAIMNureint);
            this.pnlAdd.Controls.Add(this.lblMaterial);
            this.pnlAdd.Controls.Add(this.lueCFCUsers);
            this.pnlAdd.Controls.Add(this.btnAddWarningLimit);
            this.pnlAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAdd.Location = new System.Drawing.Point(10, 63);
            this.pnlAdd.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(818, 50);
            this.pnlAdd.TabIndex = 54;
            // 
            // cmbMessage
            // 
            this.cmbMessage.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMessage.FormattingEnabled = true;
            this.cmbMessage.Location = new System.Drawing.Point(387, 13);
            this.cmbMessage.Margin = new System.Windows.Forms.Padding(2);
            this.cmbMessage.Name = "cmbMessage";
            this.cmbMessage.Size = new System.Drawing.Size(197, 23);
            this.cmbMessage.TabIndex = 56;
            this.cmbMessage.SelectedIndexChanged += new System.EventHandler(this.cmbMessage_SelectedIndexChanged);
            this.cmbMessage.Enter += new System.EventHandler(this.cmbMessage_Enter);
            // 
            // btnPreviewResults
            // 
            this.btnPreviewResults.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewResults.Location = new System.Drawing.Point(750, 5);
            this.btnPreviewResults.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewResults.Name = "btnPreviewResults";
            this.btnPreviewResults.Size = new System.Drawing.Size(57, 37);
            this.btnPreviewResults.TabIndex = 59;
            this.btnPreviewResults.UseVisualStyleBackColor = true;
            this.btnPreviewResults.Click += new System.EventHandler(this.btnPreviewResults_Click);
            // 
            // lblAIMNureint
            // 
            this.lblAIMNureint.AutoSize = true;
            this.lblAIMNureint.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAIMNureint.Location = new System.Drawing.Point(258, 17);
            this.lblAIMNureint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAIMNureint.Name = "lblAIMNureint";
            this.lblAIMNureint.Size = new System.Drawing.Size(124, 15);
            this.lblAIMNureint.TabIndex = 63;
            this.lblAIMNureint.Text = "Message Description";
            // 
            // lblMaterial
            // 
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.Font = new System.Drawing.Font("Arial", 9F);
            this.lblMaterial.Location = new System.Drawing.Point(6, 17);
            this.lblMaterial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(62, 15);
            this.lblMaterial.TabIndex = 30;
            this.lblMaterial.Text = "CFC User";
            // 
            // lueCFCUsers
            // 
            this.lueCFCUsers.Location = new System.Drawing.Point(77, 14);
            this.lueCFCUsers.Name = "lueCFCUsers";
            this.lueCFCUsers.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueCFCUsers.Properties.Appearance.Options.UseFont = true;
            this.lueCFCUsers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueCFCUsers.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueCFCUsers.Size = new System.Drawing.Size(166, 22);
            this.lueCFCUsers.TabIndex = 31;
            this.lueCFCUsers.Click += new System.EventHandler(this.lueCFCUsers_Click);
            this.lueCFCUsers.Enter += new System.EventHandler(this.lueCFCUsers_Enter);
            // 
            // btnAddWarningLimit
            // 
            this.btnAddWarningLimit.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAddWarningLimit.Location = new System.Drawing.Point(691, 5);
            this.btnAddWarningLimit.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddWarningLimit.Name = "btnAddWarningLimit";
            this.btnAddWarningLimit.Size = new System.Drawing.Size(57, 37);
            this.btnAddWarningLimit.TabIndex = 5;
            this.btnAddWarningLimit.UseVisualStyleBackColor = true;
            this.btnAddWarningLimit.Click += new System.EventHandler(this.btnAddEmailMessage_Click);
            // 
            // grdEmailMessageXRef
            // 
            this.grdEmailMessageXRef.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdEmailMessageXRef.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdEmailMessageXRef.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdEmailMessageXRef.Location = new System.Drawing.Point(10, 116);
            this.grdEmailMessageXRef.MainView = this.grdEmailMessageXRefView;
            this.grdEmailMessageXRef.Margin = new System.Windows.Forms.Padding(2);
            this.grdEmailMessageXRef.MinimumSize = new System.Drawing.Size(266, 246);
            this.grdEmailMessageXRef.Name = "grdEmailMessageXRef";
            this.grdEmailMessageXRef.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueCFCUserName,
            this.repbtnDelete});
            this.grdEmailMessageXRef.Size = new System.Drawing.Size(818, 284);
            this.grdEmailMessageXRef.TabIndex = 55;
            this.grdEmailMessageXRef.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdEmailMessageXRefView});
            // 
            // grdEmailMessageXRefView
            // 
            this.grdEmailMessageXRefView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.DetailTip.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.Empty.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.EvenRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.FilterCloseButton.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.FilterPanel.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.FixedLine.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.FocusedCell.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.FocusedRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.FooterPanel.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.GroupButton.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.GroupFooter.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.GroupPanel.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.GroupRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.HideSelectionRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.HorzLine.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.OddRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.Preview.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.Row.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.RowSeparator.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.SelectedRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.TopNewRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.VertLine.Options.UseFont = true;
            this.grdEmailMessageXRefView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.Appearance.ViewCaption.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdEmailMessageXRefView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdEmailMessageXRefView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdEmailMessageXRefView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdEmailMessageXRefView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdEmailMessageXRefView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdEmailMessageXRefView.AppearancePrint.Row.Options.UseFont = true;
            this.grdEmailMessageXRefView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmailAddressXRefMessageID,
            this.colCFCUserID,
            this.colCFCUserName,
            this.ColEmailAddress,
            this.colMessageID,
            this.colMessageDescription,
            this.colUserName,
            this.colUpdateStamp,
            this.colDelete});
            this.grdEmailMessageXRefView.GridControl = this.grdEmailMessageXRef;
            this.grdEmailMessageXRefView.Name = "grdEmailMessageXRefView";
            this.grdEmailMessageXRefView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdEmailMessageXRefView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdEmailMessageXRefView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdEmailMessageXRefView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grdEmailMessageXRefView_CustomDrawCell);
            // 
            // colEmailAddressXRefMessageID
            // 
            this.colEmailAddressXRefMessageID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmailAddressXRefMessageID.AppearanceCell.Options.UseFont = true;
            this.colEmailAddressXRefMessageID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colEmailAddressXRefMessageID.AppearanceHeader.Options.UseFont = true;
            this.colEmailAddressXRefMessageID.Caption = "Cross Ref ID";
            this.colEmailAddressXRefMessageID.FieldName = "EmailAddressXRefMessageID";
            this.colEmailAddressXRefMessageID.Name = "colEmailAddressXRefMessageID";
            this.colEmailAddressXRefMessageID.OptionsColumn.AllowEdit = false;
            this.colEmailAddressXRefMessageID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colCFCUserID
            // 
            this.colCFCUserID.Caption = "CFCUserID";
            this.colCFCUserID.FieldName = "CFCUserID";
            this.colCFCUserID.Name = "colCFCUserID";
            this.colCFCUserID.OptionsColumn.AllowEdit = false;
            // 
            // colCFCUserName
            // 
            this.colCFCUserName.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colCFCUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCFCUserName.AppearanceCell.Options.UseBackColor = true;
            this.colCFCUserName.AppearanceCell.Options.UseFont = true;
            this.colCFCUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCFCUserName.AppearanceHeader.Options.UseFont = true;
            this.colCFCUserName.Caption = "CFC UserName";
            this.colCFCUserName.ColumnEdit = this.replueCFCUserName;
            this.colCFCUserName.FieldName = "CFCUserID";
            this.colCFCUserName.Name = "colCFCUserName";
            this.colCFCUserName.OptionsColumn.AllowEdit = false;
            this.colCFCUserName.Visible = true;
            this.colCFCUserName.VisibleIndex = 0;
            this.colCFCUserName.Width = 144;
            // 
            // replueCFCUserName
            // 
            this.replueCFCUserName.AutoHeight = false;
            this.replueCFCUserName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCUserName.Name = "replueCFCUserName";
            // 
            // ColEmailAddress
            // 
            this.ColEmailAddress.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ColEmailAddress.AppearanceCell.Options.UseBackColor = true;
            this.ColEmailAddress.Caption = "Email Address";
            this.ColEmailAddress.FieldName = "EmailAddress";
            this.ColEmailAddress.Name = "ColEmailAddress";
            this.ColEmailAddress.OptionsColumn.AllowEdit = false;
            this.ColEmailAddress.Visible = true;
            this.ColEmailAddress.VisibleIndex = 2;
            this.ColEmailAddress.Width = 171;
            // 
            // colMessageID
            // 
            this.colMessageID.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colMessageID.AppearanceCell.Options.UseBackColor = true;
            this.colMessageID.Caption = "MessageID";
            this.colMessageID.FieldName = "MessageID";
            this.colMessageID.Name = "colMessageID";
            this.colMessageID.OptionsColumn.AllowEdit = false;
            this.colMessageID.OptionsColumn.ShowInCustomizationForm = false;
            this.colMessageID.Width = 70;
            // 
            // colMessageDescription
            // 
            this.colMessageDescription.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colMessageDescription.AppearanceCell.Options.UseBackColor = true;
            this.colMessageDescription.Caption = "Message Description";
            this.colMessageDescription.Name = "colMessageDescription";
            this.colMessageDescription.OptionsColumn.AllowEdit = false;
            this.colMessageDescription.Visible = true;
            this.colMessageDescription.VisibleIndex = 1;
            this.colMessageDescription.Width = 165;
            // 
            // colUserName
            // 
            this.colUserName.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUserName.AppearanceCell.Options.UseBackColor = true;
            this.colUserName.AppearanceCell.Options.UseFont = true;
            this.colUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUserName.AppearanceHeader.Options.UseFont = true;
            this.colUserName.Caption = "User Name";
            this.colUserName.FieldName = "UpdateUserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 3;
            this.colUserName.Width = 128;
            // 
            // colUpdateStamp
            // 
            this.colUpdateStamp.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colUpdateStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdateStamp.AppearanceCell.Options.UseBackColor = true;
            this.colUpdateStamp.AppearanceCell.Options.UseFont = true;
            this.colUpdateStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdateStamp.AppearanceHeader.Options.UseFont = true;
            this.colUpdateStamp.Caption = "UpdateStamp";
            this.colUpdateStamp.FieldName = "UpdateStamp";
            this.colUpdateStamp.Name = "colUpdateStamp";
            this.colUpdateStamp.OptionsColumn.AllowEdit = false;
            this.colUpdateStamp.Visible = true;
            this.colUpdateStamp.VisibleIndex = 4;
            this.colUpdateStamp.Width = 218;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDelete.AppearanceCell.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDelete.AppearanceHeader.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.repbtnDelete;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.ShowInCustomizationForm = false;
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 5;
            this.colDelete.Width = 100;
            // 
            // repbtnDelete
            // 
            this.repbtnDelete.AutoHeight = false;
            editorButtonImageOptions3.Image = global::CFCAIM.Properties.Resources.delete;
            this.repbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "Delete Record", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDelete.Name = "repbtnDelete";
            this.repbtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repbtnDelete_ButtonClick);
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
            this.panel4.Location = new System.Drawing.Point(1, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(830, 51);
            this.panel4.TabIndex = 154;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(336, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(247, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Email Notifications Setup";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(791, 0);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 112;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // EmailAddressXRefMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(830, 402);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.grdEmailMessageXRef);
            this.Controls.Add(this.pnlAdd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EmailAddressXRefMessage";
            this.Text = "Email Notifications Setup";
            this.Load += new System.EventHandler(this.EmailAddressXRefMessage_Load);
            this.SizeChanged += new System.EventHandler(this.EmailAddressXRefMessage_SizeChanged);
            this.pnlTopBtns.ResumeLayout(false);
            this.pnlAdd.ResumeLayout(false);
            this.pnlAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueCFCUsers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmailMessageXRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmailMessageXRefView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnPrintPreview;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Panel pnlAdd;
		private System.Windows.Forms.Button btnPreviewResults;
		private System.Windows.Forms.Label lblAIMNureint;
		private System.Windows.Forms.Label lblMaterial;
		private DevExpress.XtraEditors.LookUpEdit lueCFCUsers;
		private System.Windows.Forms.Button btnAddWarningLimit;
		private DevExpress.XtraGrid.GridControl grdEmailMessageXRef;
		private DevExpress.XtraGrid.Views.Grid.GridView grdEmailMessageXRefView;
		private DevExpress.XtraGrid.Columns.GridColumn colEmailAddressXRefMessageID;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCUserName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
		private DevExpress.XtraGrid.Columns.GridColumn colDelete;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDelete;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
		private DevExpress.XtraGrid.Columns.GridColumn colMessageID;
		private DevExpress.XtraGrid.Columns.GridColumn colMessageDescription;
		private DevExpress.XtraGrid.Columns.GridColumn ColEmailAddress;
		private System.Windows.Forms.ComboBox cmbMessage;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
    }
}