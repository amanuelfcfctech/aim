namespace CFCAIM
{
	partial class EmailAddress
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailAddress));
			DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnPreviewGridResults = new System.Windows.Forms.Button();
			this.pnlAdd = new System.Windows.Forms.Panel();
			this.btnAdd = new System.Windows.Forms.Button();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtIPAddress = new System.Windows.Forms.TextBox();
			this.lblIPAddress = new System.Windows.Forms.Label();
			this.txtEmailAddress = new System.Windows.Forms.TextBox();
			this.lblEmailAddress = new System.Windows.Forms.Label();
			this.grdEmailAddress = new DevExpress.XtraGrid.GridControl();
			this.grdEmailAddressView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colEmailAddressID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colEmailAddress = new DevExpress.XtraGrid.Columns.GridColumn();
			this.ColEmailIPAddress = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colEmailUserName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colEmailPassword = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repbtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.btnResize = new System.Windows.Forms.Button();
			this.pnlTopBtns.SuspendLayout();
			this.pnlAdd.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdEmailAddress)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdEmailAddressView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).BeginInit();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnRefresh);
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Location = new System.Drawing.Point(3, 3);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(115, 37);
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
			// btnPreviewGridResults
			// 
			this.btnPreviewGridResults.Image = global::CFCAIM.Properties.Resources.preview_32x32;
			this.btnPreviewGridResults.Location = new System.Drawing.Point(791, 8);
			this.btnPreviewGridResults.Margin = new System.Windows.Forms.Padding(2);
			this.btnPreviewGridResults.Name = "btnPreviewGridResults";
			this.btnPreviewGridResults.Size = new System.Drawing.Size(57, 37);
			this.btnPreviewGridResults.TabIndex = 58;
			this.btnPreviewGridResults.UseVisualStyleBackColor = true;
			this.btnPreviewGridResults.Click += new System.EventHandler(this.btnPreviewGridResults_Click);
			// 
			// pnlAdd
			// 
			this.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlAdd.Controls.Add(this.btnAdd);
			this.pnlAdd.Controls.Add(this.txtPassword);
			this.pnlAdd.Controls.Add(this.label2);
			this.pnlAdd.Controls.Add(this.txtUserName);
			this.pnlAdd.Controls.Add(this.label1);
			this.pnlAdd.Controls.Add(this.txtIPAddress);
			this.pnlAdd.Controls.Add(this.btnPreviewGridResults);
			this.pnlAdd.Controls.Add(this.lblIPAddress);
			this.pnlAdd.Controls.Add(this.txtEmailAddress);
			this.pnlAdd.Controls.Add(this.lblEmailAddress);
			this.pnlAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlAdd.Location = new System.Drawing.Point(2, 55);
			this.pnlAdd.Margin = new System.Windows.Forms.Padding(2);
			this.pnlAdd.Name = "pnlAdd";
			this.pnlAdd.Size = new System.Drawing.Size(857, 53);
			this.pnlAdd.TabIndex = 49;
			// 
			// btnAdd
			// 
			this.btnAdd.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
			this.btnAdd.Location = new System.Drawing.Point(730, 8);
			this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(57, 37);
			this.btnAdd.TabIndex = 56;
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Arial", 9F);
			this.txtPassword.Location = new System.Drawing.Point(552, 24);
			this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
			this.txtPassword.MaxLength = 30;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(110, 21);
			this.txtPassword.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9F);
			this.label2.Location = new System.Drawing.Point(552, 8);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 15);
			this.label2.TabIndex = 62;
			this.label2.Text = "Password";
			// 
			// txtUserName
			// 
			this.txtUserName.Font = new System.Drawing.Font("Arial", 9F);
			this.txtUserName.Location = new System.Drawing.Point(394, 24);
			this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
			this.txtUserName.MaxLength = 30;
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(156, 21);
			this.txtUserName.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9F);
			this.label1.Location = new System.Drawing.Point(394, 8);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 15);
			this.label1.TabIndex = 60;
			this.label1.Text = "User Name";
			// 
			// txtIPAddress
			// 
			this.txtIPAddress.Font = new System.Drawing.Font("Arial", 9F);
			this.txtIPAddress.Location = new System.Drawing.Point(237, 24);
			this.txtIPAddress.Margin = new System.Windows.Forms.Padding(2);
			this.txtIPAddress.MaxLength = 30;
			this.txtIPAddress.Name = "txtIPAddress";
			this.txtIPAddress.Size = new System.Drawing.Size(156, 21);
			this.txtIPAddress.TabIndex = 2;
			// 
			// lblIPAddress
			// 
			this.lblIPAddress.AutoSize = true;
			this.lblIPAddress.Font = new System.Drawing.Font("Arial", 9F);
			this.lblIPAddress.Location = new System.Drawing.Point(237, 8);
			this.lblIPAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblIPAddress.Name = "lblIPAddress";
			this.lblIPAddress.Size = new System.Drawing.Size(66, 15);
			this.lblIPAddress.TabIndex = 57;
			this.lblIPAddress.Text = "IP Address";
			// 
			// txtEmailAddress
			// 
			this.txtEmailAddress.Font = new System.Drawing.Font("Arial", 9F);
			this.txtEmailAddress.Location = new System.Drawing.Point(13, 24);
			this.txtEmailAddress.Margin = new System.Windows.Forms.Padding(2);
			this.txtEmailAddress.MaxLength = 30;
			this.txtEmailAddress.Name = "txtEmailAddress";
			this.txtEmailAddress.Size = new System.Drawing.Size(222, 21);
			this.txtEmailAddress.TabIndex = 1;
			// 
			// lblEmailAddress
			// 
			this.lblEmailAddress.AutoSize = true;
			this.lblEmailAddress.Font = new System.Drawing.Font("Arial", 9F);
			this.lblEmailAddress.Location = new System.Drawing.Point(13, 8);
			this.lblEmailAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblEmailAddress.Name = "lblEmailAddress";
			this.lblEmailAddress.Size = new System.Drawing.Size(133, 15);
			this.lblEmailAddress.TabIndex = 31;
			this.lblEmailAddress.Text = "Account Email Address";
			// 
			// grdEmailAddress
			// 
			this.grdEmailAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdEmailAddress.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdEmailAddress.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
			this.grdEmailAddress.Location = new System.Drawing.Point(2, 114);
			this.grdEmailAddress.MainView = this.grdEmailAddressView;
			this.grdEmailAddress.Margin = new System.Windows.Forms.Padding(2);
			this.grdEmailAddress.MinimumSize = new System.Drawing.Size(266, 246);
			this.grdEmailAddress.Name = "grdEmailAddress";
			this.grdEmailAddress.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repbtnDelete});
			this.grdEmailAddress.Size = new System.Drawing.Size(855, 397);
			this.grdEmailAddress.TabIndex = 55;
			this.grdEmailAddress.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdEmailAddressView});
			// 
			// grdEmailAddressView
			// 
			this.grdEmailAddressView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.ColumnFilterButton.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.CustomizationFormHint.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.DetailTip.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.Empty.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.EvenRow.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.FilterCloseButton.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.FilterPanel.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.FixedLine.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.FocusedCell.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.FocusedRow.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.FooterPanel.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.GroupButton.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.GroupFooter.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.GroupPanel.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.GroupRow.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.HeaderPanel.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.HideSelectionRow.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.HorzLine.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.OddRow.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.Preview.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.Row.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.RowSeparator.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.SelectedRow.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.TopNewRow.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.VertLine.Options.UseFont = true;
			this.grdEmailAddressView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.Appearance.ViewCaption.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.grdEmailAddressView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.grdEmailAddressView.AppearancePrint.EvenRow.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.FilterPanel.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.GroupRow.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.Lines.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.grdEmailAddressView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.grdEmailAddressView.AppearancePrint.OddRow.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.Preview.Options.UseFont = true;
			this.grdEmailAddressView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
			this.grdEmailAddressView.AppearancePrint.Row.Options.UseFont = true;
			this.grdEmailAddressView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colEmailAddressID,
            this.colEmailAddress,
            this.ColEmailIPAddress,
            this.colEmailUserName,
            this.colEmailPassword,
            this.colCFCUserID,
            this.colUpdateStamp,
            this.colDelete});
			this.grdEmailAddressView.GridControl = this.grdEmailAddress;
			this.grdEmailAddressView.Name = "grdEmailAddressView";
			this.grdEmailAddressView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
			this.grdEmailAddressView.OptionsPrint.EnableAppearanceEvenRow = true;
			this.grdEmailAddressView.OptionsPrint.EnableAppearanceOddRow = true;
			this.grdEmailAddressView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdEmailAddressView_ValidateRow);
			// 
			// colEmailAddressID
			// 
			this.colEmailAddressID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colEmailAddressID.AppearanceCell.Options.UseFont = true;
			this.colEmailAddressID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colEmailAddressID.AppearanceHeader.Options.UseFont = true;
			this.colEmailAddressID.Caption = "Email Address ID";
			this.colEmailAddressID.FieldName = "EmailAddressID";
			this.colEmailAddressID.Name = "colEmailAddressID";
			this.colEmailAddressID.OptionsColumn.ShowInCustomizationForm = false;
			// 
			// colEmailAddress
			// 
			this.colEmailAddress.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colEmailAddress.AppearanceCell.Options.UseFont = true;
			this.colEmailAddress.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colEmailAddress.AppearanceHeader.Options.UseFont = true;
			this.colEmailAddress.Caption = "Email Address";
			this.colEmailAddress.FieldName = "EmailAddress";
			this.colEmailAddress.Name = "colEmailAddress";
			this.colEmailAddress.Visible = true;
			this.colEmailAddress.VisibleIndex = 0;
			this.colEmailAddress.Width = 278;
			// 
			// ColEmailIPAddress
			// 
			this.ColEmailIPAddress.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ColEmailIPAddress.AppearanceCell.Options.UseFont = true;
			this.ColEmailIPAddress.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ColEmailIPAddress.AppearanceHeader.Options.UseFont = true;
			this.ColEmailIPAddress.Caption = "IP Address";
			this.ColEmailIPAddress.FieldName = "EmailIPAddress";
			this.ColEmailIPAddress.Name = "ColEmailIPAddress";
			this.ColEmailIPAddress.Visible = true;
			this.ColEmailIPAddress.VisibleIndex = 1;
			this.ColEmailIPAddress.Width = 232;
			// 
			// colEmailUserName
			// 
			this.colEmailUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colEmailUserName.AppearanceCell.Options.UseFont = true;
			this.colEmailUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colEmailUserName.AppearanceHeader.Options.UseFont = true;
			this.colEmailUserName.Caption = "User Name";
			this.colEmailUserName.FieldName = "EmailUserName";
			this.colEmailUserName.Name = "colEmailUserName";
			this.colEmailUserName.Visible = true;
			this.colEmailUserName.VisibleIndex = 2;
			this.colEmailUserName.Width = 235;
			// 
			// colEmailPassword
			// 
			this.colEmailPassword.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colEmailPassword.AppearanceCell.Options.UseFont = true;
			this.colEmailPassword.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colEmailPassword.AppearanceHeader.Options.UseFont = true;
			this.colEmailPassword.Caption = "Password";
			this.colEmailPassword.FieldName = "EmailPassword";
			this.colEmailPassword.Name = "colEmailPassword";
			this.colEmailPassword.Width = 180;
			// 
			// colCFCUserID
			// 
			this.colCFCUserID.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
			this.colCFCUserID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCFCUserID.AppearanceCell.Options.UseBackColor = true;
			this.colCFCUserID.AppearanceCell.Options.UseFont = true;
			this.colCFCUserID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCFCUserID.AppearanceHeader.Options.UseFont = true;
			this.colCFCUserID.Caption = "User ID";
			this.colCFCUserID.FieldName = "CFCUserID";
			this.colCFCUserID.Name = "colCFCUserID";
			this.colCFCUserID.OptionsColumn.AllowEdit = false;
			this.colCFCUserID.Width = 107;
			// 
			// colUpdateStamp
			// 
			this.colUpdateStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colUpdateStamp.AppearanceCell.Options.UseFont = true;
			this.colUpdateStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colUpdateStamp.AppearanceHeader.Options.UseFont = true;
			this.colUpdateStamp.Caption = "UpdateStamp";
			this.colUpdateStamp.FieldName = "UpdateStamp";
			this.colUpdateStamp.Name = "colUpdateStamp";
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
			this.colDelete.VisibleIndex = 3;
			this.colDelete.Width = 105;
			// 
			// repbtnDelete
			// 
			this.repbtnDelete.AutoHeight = false;
			editorButtonImageOptions1.Image = global::CFCAIM.Properties.Resources.delete;
			this.repbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Delete Record", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
			this.repbtnDelete.Name = "repbtnDelete";
			this.repbtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repbtnDelete.Click += new System.EventHandler(this.repbtnDelete_Click);
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
			this.panel4.Size = new System.Drawing.Size(861, 51);
			this.panel4.TabIndex = 154;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.label10.ForeColor = System.Drawing.Color.White;
			this.label10.Location = new System.Drawing.Point(354, 10);
			this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(210, 23);
			this.label10.TabIndex = 149;
			this.label10.Text = "Outgoing Mail Server";
			// 
			// btnResize
			// 
			this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
			this.btnResize.Location = new System.Drawing.Point(822, 0);
			this.btnResize.Margin = new System.Windows.Forms.Padding(2);
			this.btnResize.Name = "btnResize";
			this.btnResize.Size = new System.Drawing.Size(34, 34);
			this.btnResize.TabIndex = 112;
			this.btnResize.UseVisualStyleBackColor = true;
			this.btnResize.Visible = false;
			this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
			// 
			// EmailAddress
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(862, 522);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.grdEmailAddress);
			this.Controls.Add(this.pnlAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "EmailAddress";
			this.Text = "Outgoing Mail Server";
			this.Load += new System.EventHandler(this.EmailAddress_Load);
			this.SizeChanged += new System.EventHandler(this.EmailAddress_SizeChanged);
			this.pnlTopBtns.ResumeLayout(false);
			this.pnlAdd.ResumeLayout(false);
			this.pnlAdd.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdEmailAddress)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdEmailAddressView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).EndInit();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnPreviewGridResults;
		private System.Windows.Forms.Panel pnlAdd;
		private System.Windows.Forms.TextBox txtEmailAddress;
		private System.Windows.Forms.Label lblEmailAddress;
		private DevExpress.XtraGrid.GridControl grdEmailAddress;
		private DevExpress.XtraGrid.Views.Grid.GridView grdEmailAddressView;
		private DevExpress.XtraGrid.Columns.GridColumn colEmailAddressID;
		private DevExpress.XtraGrid.Columns.GridColumn colEmailAddress;
		private DevExpress.XtraGrid.Columns.GridColumn ColEmailIPAddress;
		private DevExpress.XtraGrid.Columns.GridColumn colEmailUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colEmailPassword;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
		private DevExpress.XtraGrid.Columns.GridColumn colDelete;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDelete;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtIPAddress;
		private System.Windows.Forms.Label lblIPAddress;
		private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
    }
}