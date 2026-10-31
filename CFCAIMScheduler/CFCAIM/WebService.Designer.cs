namespace CFCAIM
{
	partial class WebService
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebService));
			DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnPrintPreview = new System.Windows.Forms.Button();
			this.pnlAdd = new System.Windows.Forms.Panel();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lueDataSourceType = new DevExpress.XtraEditors.LookUpEdit();
			this.txtWebAddress = new System.Windows.Forms.TextBox();
			this.txtToken = new System.Windows.Forms.TextBox();
			this.lblToken = new System.Windows.Forms.Label();
			this.lblWebAddress = new System.Windows.Forms.Label();
			this.btnAddWebService = new System.Windows.Forms.Button();
			this.grdWebService = new DevExpress.XtraGrid.GridControl();
			this.grdWebServiceView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colWebServiceID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colWebAddress = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colToken = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDataSourceTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.replueDataSourceType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repbtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.replueMaterialCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.replueMaterialName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.replueNutrientCode = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.replueNutrientName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.replueDataSourceUser = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.panel4 = new System.Windows.Forms.Panel();
			this.btnResize = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.pnlTopBtns.SuspendLayout();
			this.pnlAdd.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdWebService)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdWebServiceView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueMaterialCode)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueMaterialName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueNutrientCode)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueNutrientName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceUser)).BeginInit();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Controls.Add(this.btnRefresh);
			this.pnlTopBtns.Controls.Add(this.btnPrint);
			this.pnlTopBtns.Location = new System.Drawing.Point(7, 3);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(176, 37);
			this.pnlTopBtns.TabIndex = 53;
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
			// btnPrintPreview
			// 
			this.btnPrintPreview.Image = global::CFCAIM.Properties.Resources.preview_32x32;
			this.btnPrintPreview.Location = new System.Drawing.Point(794, 67);
			this.btnPrintPreview.Margin = new System.Windows.Forms.Padding(2);
			this.btnPrintPreview.Name = "btnPrintPreview";
			this.btnPrintPreview.Size = new System.Drawing.Size(57, 37);
			this.btnPrintPreview.TabIndex = 6;
			this.btnPrintPreview.UseVisualStyleBackColor = true;
			this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
			// 
			// pnlAdd
			// 
			this.pnlAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlAdd.Controls.Add(this.txtName);
			this.pnlAdd.Controls.Add(this.lblName);
			this.pnlAdd.Controls.Add(this.btnPrintPreview);
			this.pnlAdd.Controls.Add(this.label1);
			this.pnlAdd.Controls.Add(this.lueDataSourceType);
			this.pnlAdd.Controls.Add(this.txtWebAddress);
			this.pnlAdd.Controls.Add(this.txtToken);
			this.pnlAdd.Controls.Add(this.lblToken);
			this.pnlAdd.Controls.Add(this.lblWebAddress);
			this.pnlAdd.Controls.Add(this.btnAddWebService);
			this.pnlAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlAdd.Location = new System.Drawing.Point(7, 53);
			this.pnlAdd.Margin = new System.Windows.Forms.Padding(2);
			this.pnlAdd.Name = "pnlAdd";
			this.pnlAdd.Size = new System.Drawing.Size(994, 114);
			this.pnlAdd.TabIndex = 54;
			// 
			// txtName
			// 
			this.txtName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtName.Location = new System.Drawing.Point(130, 2);
			this.txtName.Margin = new System.Windows.Forms.Padding(2);
			this.txtName.MaxLength = 30;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(338, 21);
			this.txtName.TabIndex = 1;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Font = new System.Drawing.Font("Arial", 9F);
			this.lblName.Location = new System.Drawing.Point(15, 5);
			this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(41, 15);
			this.lblName.TabIndex = 63;
			this.lblName.Text = "Name";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(15, 84);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 15);
			this.label1.TabIndex = 61;
			this.label1.Text = "Data Source Name";
			// 
			// lueDataSourceType
			// 
			this.lueDataSourceType.Location = new System.Drawing.Point(130, 82);
			this.lueDataSourceType.Margin = new System.Windows.Forms.Padding(4);
			this.lueDataSourceType.Name = "lueDataSourceType";
			this.lueDataSourceType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.lueDataSourceType.Properties.Appearance.Options.UseFont = true;
			this.lueDataSourceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lueDataSourceType.Size = new System.Drawing.Size(162, 22);
			this.lueDataSourceType.TabIndex = 4;
			this.lueDataSourceType.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.lueDataSourceType_CustomDisplayText);
			this.lueDataSourceType.Click += new System.EventHandler(this.lueDataSourceType_Click);
			this.lueDataSourceType.Enter += new System.EventHandler(this.lueDataSourceType_Enter);
			// 
			// txtWebAddress
			// 
			this.txtWebAddress.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtWebAddress.Location = new System.Drawing.Point(130, 30);
			this.txtWebAddress.Margin = new System.Windows.Forms.Padding(2);
			this.txtWebAddress.MaxLength = 30;
			this.txtWebAddress.Name = "txtWebAddress";
			this.txtWebAddress.Size = new System.Drawing.Size(338, 21);
			this.txtWebAddress.TabIndex = 2;
			// 
			// txtToken
			// 
			this.txtToken.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtToken.Location = new System.Drawing.Point(130, 55);
			this.txtToken.Margin = new System.Windows.Forms.Padding(2);
			this.txtToken.MaxLength = 50;
			this.txtToken.Name = "txtToken";
			this.txtToken.Size = new System.Drawing.Size(338, 21);
			this.txtToken.TabIndex = 3;
			// 
			// lblToken
			// 
			this.lblToken.AutoSize = true;
			this.lblToken.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblToken.Location = new System.Drawing.Point(15, 58);
			this.lblToken.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblToken.Name = "lblToken";
			this.lblToken.Size = new System.Drawing.Size(40, 15);
			this.lblToken.TabIndex = 60;
			this.lblToken.Text = "Token";
			// 
			// lblWebAddress
			// 
			this.lblWebAddress.AutoSize = true;
			this.lblWebAddress.Font = new System.Drawing.Font("Arial", 9F);
			this.lblWebAddress.Location = new System.Drawing.Point(15, 33);
			this.lblWebAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblWebAddress.Name = "lblWebAddress";
			this.lblWebAddress.Size = new System.Drawing.Size(80, 15);
			this.lblWebAddress.TabIndex = 30;
			this.lblWebAddress.Text = "Web Address";
			// 
			// btnAddWebService
			// 
			this.btnAddWebService.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
			this.btnAddWebService.Location = new System.Drawing.Point(516, 71);
			this.btnAddWebService.Margin = new System.Windows.Forms.Padding(2);
			this.btnAddWebService.Name = "btnAddWebService";
			this.btnAddWebService.Size = new System.Drawing.Size(57, 37);
			this.btnAddWebService.TabIndex = 5;
			this.btnAddWebService.UseVisualStyleBackColor = true;
			this.btnAddWebService.Click += new System.EventHandler(this.btnAddWebService_Click);
			// 
			// grdWebService
			// 
			this.grdWebService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdWebService.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdWebService.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
			this.grdWebService.Location = new System.Drawing.Point(7, 170);
			this.grdWebService.MainView = this.grdWebServiceView;
			this.grdWebService.Margin = new System.Windows.Forms.Padding(2);
			this.grdWebService.MinimumSize = new System.Drawing.Size(266, 246);
			this.grdWebService.Name = "grdWebService";
			this.grdWebService.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueMaterialCode,
            this.replueMaterialName,
            this.replueNutrientCode,
            this.replueNutrientName,
            this.repbtnDelete,
            this.replueDataSourceType,
            this.replueDataSourceUser});
			this.grdWebService.Size = new System.Drawing.Size(995, 264);
			this.grdWebService.TabIndex = 7;
			this.grdWebService.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdWebServiceView});
			// 
			// grdWebServiceView
			// 
			this.grdWebServiceView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.ColumnFilterButton.Options.UseFont = true;
			this.grdWebServiceView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
			this.grdWebServiceView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.CustomizationFormHint.Options.UseFont = true;
			this.grdWebServiceView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.DetailTip.Options.UseFont = true;
			this.grdWebServiceView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.Empty.Options.UseFont = true;
			this.grdWebServiceView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.EvenRow.Options.UseFont = true;
			this.grdWebServiceView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.FilterCloseButton.Options.UseFont = true;
			this.grdWebServiceView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.FilterPanel.Options.UseFont = true;
			this.grdWebServiceView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.FixedLine.Options.UseFont = true;
			this.grdWebServiceView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.FocusedCell.Options.UseFont = true;
			this.grdWebServiceView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.FocusedRow.Options.UseFont = true;
			this.grdWebServiceView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.FooterPanel.Options.UseFont = true;
			this.grdWebServiceView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.GroupButton.Options.UseFont = true;
			this.grdWebServiceView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.GroupFooter.Options.UseFont = true;
			this.grdWebServiceView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.GroupPanel.Options.UseFont = true;
			this.grdWebServiceView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.GroupRow.Options.UseFont = true;
			this.grdWebServiceView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.HeaderPanel.Options.UseFont = true;
			this.grdWebServiceView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.HideSelectionRow.Options.UseFont = true;
			this.grdWebServiceView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.HorzLine.Options.UseFont = true;
			this.grdWebServiceView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.OddRow.Options.UseFont = true;
			this.grdWebServiceView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.Preview.Options.UseFont = true;
			this.grdWebServiceView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.Row.Options.UseFont = true;
			this.grdWebServiceView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.RowSeparator.Options.UseFont = true;
			this.grdWebServiceView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.SelectedRow.Options.UseFont = true;
			this.grdWebServiceView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.TopNewRow.Options.UseFont = true;
			this.grdWebServiceView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.VertLine.Options.UseFont = true;
			this.grdWebServiceView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.Appearance.ViewCaption.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.grdWebServiceView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.grdWebServiceView.AppearancePrint.EvenRow.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.FilterPanel.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.GroupRow.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.Lines.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.grdWebServiceView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.grdWebServiceView.AppearancePrint.OddRow.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.Preview.Options.UseFont = true;
			this.grdWebServiceView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
			this.grdWebServiceView.AppearancePrint.Row.Options.UseFont = true;
			this.grdWebServiceView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWebServiceID,
            this.colName,
            this.colWebAddress,
            this.colToken,
            this.colDataSourceTypeID,
            this.colUserName,
            this.colUpdateStamp,
            this.colDelete});
			this.grdWebServiceView.DetailHeight = 280;
			this.grdWebServiceView.GridControl = this.grdWebService;
			this.grdWebServiceView.Name = "grdWebServiceView";
			this.grdWebServiceView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
			this.grdWebServiceView.OptionsPrint.EnableAppearanceEvenRow = true;
			this.grdWebServiceView.OptionsPrint.EnableAppearanceOddRow = true;
			this.grdWebServiceView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdWebServiceView_ValidateRow);
			// 
			// colWebServiceID
			// 
			this.colWebServiceID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colWebServiceID.AppearanceCell.Options.UseFont = true;
			this.colWebServiceID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colWebServiceID.AppearanceHeader.Options.UseFont = true;
			this.colWebServiceID.Caption = "Web Service ID";
			this.colWebServiceID.FieldName = "WebServiceID";
			this.colWebServiceID.MinWidth = 16;
			this.colWebServiceID.Name = "colWebServiceID";
			this.colWebServiceID.OptionsColumn.ShowInCustomizationForm = false;
			this.colWebServiceID.Width = 60;
			// 
			// colName
			// 
			this.colName.Caption = "Name";
			this.colName.FieldName = "Name";
			this.colName.Name = "colName";
			this.colName.Visible = true;
			this.colName.VisibleIndex = 0;
			this.colName.Width = 115;
			// 
			// colWebAddress
			// 
			this.colWebAddress.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colWebAddress.AppearanceCell.Options.UseFont = true;
			this.colWebAddress.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colWebAddress.AppearanceHeader.Options.UseFont = true;
			this.colWebAddress.Caption = "Web Address";
			this.colWebAddress.FieldName = "WebAddress";
			this.colWebAddress.MinWidth = 16;
			this.colWebAddress.Name = "colWebAddress";
			this.colWebAddress.Visible = true;
			this.colWebAddress.VisibleIndex = 1;
			this.colWebAddress.Width = 213;
			// 
			// colToken
			// 
			this.colToken.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colToken.AppearanceCell.Options.UseFont = true;
			this.colToken.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colToken.AppearanceHeader.Options.UseFont = true;
			this.colToken.Caption = "Token";
			this.colToken.FieldName = "Token";
			this.colToken.Name = "colToken";
			this.colToken.Visible = true;
			this.colToken.VisibleIndex = 2;
			this.colToken.Width = 111;
			// 
			// colDataSourceTypeID
			// 
			this.colDataSourceTypeID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colDataSourceTypeID.AppearanceCell.Options.UseFont = true;
			this.colDataSourceTypeID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colDataSourceTypeID.AppearanceHeader.Options.UseFont = true;
			this.colDataSourceTypeID.Caption = "Data Source";
			this.colDataSourceTypeID.ColumnEdit = this.replueDataSourceType;
			this.colDataSourceTypeID.FieldName = "DataSourceTypeID";
			this.colDataSourceTypeID.MinWidth = 16;
			this.colDataSourceTypeID.Name = "colDataSourceTypeID";
			this.colDataSourceTypeID.OptionsColumn.ShowInCustomizationForm = false;
			this.colDataSourceTypeID.Visible = true;
			this.colDataSourceTypeID.VisibleIndex = 3;
			this.colDataSourceTypeID.Width = 80;
			// 
			// replueDataSourceType
			// 
			this.replueDataSourceType.AutoHeight = false;
			this.replueDataSourceType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueDataSourceType.Name = "replueDataSourceType";
			// 
			// colUserName
			// 
			this.colUserName.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
			this.colUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colUserName.AppearanceCell.Options.UseBackColor = true;
			this.colUserName.AppearanceCell.Options.UseFont = true;
			this.colUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colUserName.AppearanceHeader.Options.UseFont = true;
			this.colUserName.Caption = "User Name";
			this.colUserName.FieldName = "UserName";
			this.colUserName.MinWidth = 16;
			this.colUserName.Name = "colUserName";
			this.colUserName.OptionsColumn.AllowEdit = false;
			this.colUserName.OptionsColumn.ReadOnly = true;
			this.colUserName.Visible = true;
			this.colUserName.VisibleIndex = 4;
			this.colUserName.Width = 59;
			// 
			// colUpdateStamp
			// 
			this.colUpdateStamp.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
			this.colUpdateStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colUpdateStamp.AppearanceCell.Options.UseBackColor = true;
			this.colUpdateStamp.AppearanceCell.Options.UseFont = true;
			this.colUpdateStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colUpdateStamp.AppearanceHeader.Options.UseFont = true;
			this.colUpdateStamp.Caption = "UpdateStamp";
			this.colUpdateStamp.FieldName = "UpdateStamp";
			this.colUpdateStamp.MinWidth = 16;
			this.colUpdateStamp.Name = "colUpdateStamp";
			this.colUpdateStamp.OptionsColumn.ReadOnly = true;
			this.colUpdateStamp.Visible = true;
			this.colUpdateStamp.VisibleIndex = 5;
			this.colUpdateStamp.Width = 66;
			// 
			// colDelete
			// 
			this.colDelete.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
			this.colDelete.AppearanceCell.Options.UseFont = true;
			this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
			this.colDelete.AppearanceHeader.Options.UseFont = true;
			this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
			this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colDelete.Caption = "Delete";
			this.colDelete.ColumnEdit = this.repbtnDelete;
			this.colDelete.MinWidth = 16;
			this.colDelete.Name = "colDelete";
			this.colDelete.Visible = true;
			this.colDelete.VisibleIndex = 6;
			this.colDelete.Width = 41;
			// 
			// repbtnDelete
			// 
			this.repbtnDelete.AutoHeight = false;
			editorButtonImageOptions1.Image = global::CFCAIM.Properties.Resources.delete;
			this.repbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Delete", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Delete Record", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
			this.repbtnDelete.Name = "repbtnDelete";
			this.repbtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repbtnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repbtnDelete_ButtonClick);
			// 
			// replueMaterialCode
			// 
			this.replueMaterialCode.AutoHeight = false;
			this.replueMaterialCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueMaterialCode.Name = "replueMaterialCode";
			// 
			// replueMaterialName
			// 
			this.replueMaterialName.AutoHeight = false;
			this.replueMaterialName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueMaterialName.Name = "replueMaterialName";
			// 
			// replueNutrientCode
			// 
			this.replueNutrientCode.AutoHeight = false;
			this.replueNutrientCode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueNutrientCode.Name = "replueNutrientCode";
			// 
			// replueNutrientName
			// 
			this.replueNutrientName.AutoHeight = false;
			this.replueNutrientName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueNutrientName.Name = "replueNutrientName";
			// 
			// replueDataSourceUser
			// 
			this.replueDataSourceUser.AutoHeight = false;
			this.replueDataSourceUser.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.replueDataSourceUser.Name = "replueDataSourceUser";
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.BackColor = System.Drawing.Color.DarkBlue;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.btnResize);
			this.panel4.Controls.Add(this.label7);
			this.panel4.Controls.Add(this.pnlTopBtns);
			this.panel4.Location = new System.Drawing.Point(1, 1);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(1008, 51);
			this.panel4.TabIndex = 152;
			// 
			// btnResize
			// 
			this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
			this.btnResize.Location = new System.Drawing.Point(970, -1);
			this.btnResize.Margin = new System.Windows.Forms.Padding(2);
			this.btnResize.Name = "btnResize";
			this.btnResize.Size = new System.Drawing.Size(34, 34);
			this.btnResize.TabIndex = 150;
			this.btnResize.UseVisualStyleBackColor = true;
			this.btnResize.Visible = false;
			this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.label7.ForeColor = System.Drawing.Color.White;
			this.label7.Location = new System.Drawing.Point(358, 14);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(127, 23);
			this.label7.TabIndex = 149;
			this.label7.Text = "Web Service";
			// 
			// WebService
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(1009, 440);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.grdWebService);
			this.Controls.Add(this.pnlAdd);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "WebService";
			this.Text = "Web Service";
			this.Load += new System.EventHandler(this.WebService_Load);
			this.SizeChanged += new System.EventHandler(this.WebService_SizeChanged);
			this.pnlTopBtns.ResumeLayout(false);
			this.pnlAdd.ResumeLayout(false);
			this.pnlAdd.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdWebService)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdWebServiceView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueMaterialCode)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueMaterialName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueNutrientCode)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueNutrientName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.replueDataSourceUser)).EndInit();
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
		private System.Windows.Forms.TextBox txtWebAddress;
		private System.Windows.Forms.TextBox txtToken;
		private System.Windows.Forms.Label lblToken;
		private System.Windows.Forms.Label lblWebAddress;
		private System.Windows.Forms.Button btnAddWebService;
		private DevExpress.XtraGrid.GridControl grdWebService;
		private DevExpress.XtraGrid.Views.Grid.GridView grdWebServiceView;
		private DevExpress.XtraGrid.Columns.GridColumn colWebServiceID;
		private DevExpress.XtraGrid.Columns.GridColumn colWebAddress;
		private DevExpress.XtraGrid.Columns.GridColumn colToken;
		private DevExpress.XtraGrid.Columns.GridColumn colUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
		private DevExpress.XtraGrid.Columns.GridColumn colDelete;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDelete;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueMaterialCode;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueMaterialName;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueNutrientCode;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueNutrientName;
		private DevExpress.XtraEditors.LookUpEdit lueDataSourceType;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeID;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueDataSourceType;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueDataSourceUser;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
    }
}