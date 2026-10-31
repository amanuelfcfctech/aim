namespace CFCAIM
{
	partial class ExportBatchPending
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportBatchPending));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnResize = new System.Windows.Forms.Button();
            this.btnPreviewGridResults = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grdHeader = new DevExpress.XtraGrid.GridControl();
            this.viewHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUpdateIngredientHeaderID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColTarget = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colViewDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repViewDetail = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repViewDetail)).BeginInit();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(775, 0);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 146;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // btnPreviewGridResults
            // 
            this.btnPreviewGridResults.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewGridResults.Location = new System.Drawing.Point(673, 4);
            this.btnPreviewGridResults.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewGridResults.Name = "btnPreviewGridResults";
            this.btnPreviewGridResults.Size = new System.Drawing.Size(57, 37);
            this.btnPreviewGridResults.TabIndex = 28;
            this.btnPreviewGridResults.UseVisualStyleBackColor = true;
            this.btnPreviewGridResults.Click += new System.EventHandler(this.btnPreviewGridResults_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(62, 1);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(54, 37);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(2, 1);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 19;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grdHeader
            // 
            this.grdHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdHeader.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdHeader.Location = new System.Drawing.Point(2, 61);
            this.grdHeader.MainView = this.viewHeader;
            this.grdHeader.Name = "grdHeader";
            this.grdHeader.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repViewDetail});
            this.grdHeader.Size = new System.Drawing.Size(814, 360);
            this.grdHeader.TabIndex = 25;
            this.grdHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewHeader});
            this.grdHeader.Click += new System.EventHandler(this.grdHeader_Click);
            // 
            // viewHeader
            // 
            this.viewHeader.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.viewHeader.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.viewHeader.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.viewHeader.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.DetailTip.Options.UseFont = true;
            this.viewHeader.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.Empty.Options.UseFont = true;
            this.viewHeader.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.EvenRow.Options.UseFont = true;
            this.viewHeader.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.FilterCloseButton.Options.UseFont = true;
            this.viewHeader.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.FilterPanel.Options.UseFont = true;
            this.viewHeader.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.FixedLine.Options.UseFont = true;
            this.viewHeader.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.FocusedCell.Options.UseFont = true;
            this.viewHeader.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.FocusedRow.Options.UseFont = true;
            this.viewHeader.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.FooterPanel.Options.UseFont = true;
            this.viewHeader.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.GroupButton.Options.UseFont = true;
            this.viewHeader.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.GroupFooter.Options.UseFont = true;
            this.viewHeader.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.GroupPanel.Options.UseFont = true;
            this.viewHeader.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.GroupRow.Options.UseFont = true;
            this.viewHeader.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.HeaderPanel.Options.UseFont = true;
            this.viewHeader.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.HideSelectionRow.Options.UseFont = true;
            this.viewHeader.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.HorzLine.Options.UseFont = true;
            this.viewHeader.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.OddRow.Options.UseFont = true;
            this.viewHeader.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.Preview.Options.UseFont = true;
            this.viewHeader.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.Row.Options.UseFont = true;
            this.viewHeader.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.RowSeparator.Options.UseFont = true;
            this.viewHeader.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.SelectedRow.Options.UseFont = true;
            this.viewHeader.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.TopNewRow.Options.UseFont = true;
            this.viewHeader.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.VertLine.Options.UseFont = true;
            this.viewHeader.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.Appearance.ViewCaption.Options.UseFont = true;
            this.viewHeader.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.viewHeader.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.viewHeader.AppearancePrint.EvenRow.Options.UseFont = true;
            this.viewHeader.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.viewHeader.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.viewHeader.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.viewHeader.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.GroupRow.Options.UseFont = true;
            this.viewHeader.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.viewHeader.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.Lines.Options.UseFont = true;
            this.viewHeader.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.viewHeader.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.viewHeader.AppearancePrint.OddRow.Options.UseFont = true;
            this.viewHeader.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.Preview.Options.UseFont = true;
            this.viewHeader.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.viewHeader.AppearancePrint.Row.Options.UseFont = true;
            this.viewHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUpdateIngredientHeaderID,
            this.colUpdateName,
            this.colCreateDate,
            this.ColTarget,
            this.colCFCUserID,
            this.colUserName,
            this.colStatus,
            this.colViewDetail});
            this.viewHeader.GridControl = this.grdHeader;
            this.viewHeader.Name = "viewHeader";
            this.viewHeader.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.viewHeader.OptionsPrint.EnableAppearanceEvenRow = true;
            this.viewHeader.OptionsPrint.EnableAppearanceOddRow = true;
            // 
            // colUpdateIngredientHeaderID
            // 
            this.colUpdateIngredientHeaderID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdateIngredientHeaderID.AppearanceCell.Options.UseFont = true;
            this.colUpdateIngredientHeaderID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdateIngredientHeaderID.AppearanceHeader.Options.UseFont = true;
            this.colUpdateIngredientHeaderID.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdateIngredientHeaderID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdateIngredientHeaderID.Caption = "UpdateIngredientHeaderID";
            this.colUpdateIngredientHeaderID.FieldName = "UpdateIngredientHeaderID";
            this.colUpdateIngredientHeaderID.Name = "colUpdateIngredientHeaderID";
            this.colUpdateIngredientHeaderID.OptionsColumn.ShowInCustomizationForm = false;
            this.colUpdateIngredientHeaderID.Width = 86;
            // 
            // colUpdateName
            // 
            this.colUpdateName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdateName.AppearanceCell.Options.UseFont = true;
            this.colUpdateName.AppearanceCell.Options.UseTextOptions = true;
            this.colUpdateName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colUpdateName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdateName.AppearanceHeader.Options.UseFont = true;
            this.colUpdateName.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpdateName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpdateName.Caption = "Export Batch Name";
            this.colUpdateName.FieldName = "UpdateName";
            this.colUpdateName.Name = "colUpdateName";
            this.colUpdateName.OptionsColumn.ReadOnly = true;
            this.colUpdateName.Visible = true;
            this.colUpdateName.VisibleIndex = 0;
            this.colUpdateName.Width = 107;
            // 
            // colCreateDate
            // 
            this.colCreateDate.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCreateDate.AppearanceCell.Options.UseFont = true;
            this.colCreateDate.AppearanceCell.Options.UseTextOptions = true;
            this.colCreateDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDate.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCreateDate.AppearanceHeader.Options.UseFont = true;
            this.colCreateDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreateDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDate.Caption = "Created Date";
            this.colCreateDate.FieldName = "CreateDateString";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.ReadOnly = true;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 1;
            this.colCreateDate.Width = 115;
            // 
            // ColTarget
            // 
            this.ColTarget.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColTarget.AppearanceCell.Options.UseFont = true;
            this.ColTarget.AppearanceCell.Options.UseTextOptions = true;
            this.ColTarget.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColTarget.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColTarget.AppearanceHeader.Options.UseFont = true;
            this.ColTarget.AppearanceHeader.Options.UseTextOptions = true;
            this.ColTarget.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColTarget.Caption = "Target";
            this.ColTarget.FieldName = "Target";
            this.ColTarget.Name = "ColTarget";
            this.ColTarget.Visible = true;
            this.ColTarget.VisibleIndex = 3;
            // 
            // colCFCUserID
            // 
            this.colCFCUserID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCFCUserID.AppearanceCell.Options.UseFont = true;
            this.colCFCUserID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCFCUserID.AppearanceHeader.Options.UseFont = true;
            this.colCFCUserID.Caption = "User ID";
            this.colCFCUserID.FieldName = "CFCUserID";
            this.colCFCUserID.Name = "colCFCUserID";
            // 
            // colUserName
            // 
            this.colUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUserName.AppearanceCell.Options.UseFont = true;
            this.colUserName.AppearanceCell.Options.UseTextOptions = true;
            this.colUserName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUserName.AppearanceHeader.Options.UseFont = true;
            this.colUserName.AppearanceHeader.Options.UseTextOptions = true;
            this.colUserName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUserName.Caption = "User Name";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.ReadOnly = true;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 2;
            this.colUserName.Width = 103;
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colStatus.AppearanceCell.Options.UseFont = true;
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.ReadOnly = true;
            this.colStatus.Width = 111;
            // 
            // colViewDetail
            // 
            this.colViewDetail.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colViewDetail.AppearanceCell.Options.UseFont = true;
            this.colViewDetail.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colViewDetail.AppearanceHeader.Options.UseFont = true;
            this.colViewDetail.AppearanceHeader.Options.UseTextOptions = true;
            this.colViewDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colViewDetail.Caption = "View Detail";
            this.colViewDetail.ColumnEdit = this.repViewDetail;
            this.colViewDetail.Name = "colViewDetail";
            this.colViewDetail.Visible = true;
            this.colViewDetail.VisibleIndex = 4;
            this.colViewDetail.Width = 57;
            // 
            // repViewDetail
            // 
            this.repViewDetail.AutoHeight = false;
            editorButtonImageOptions1.Image = global::CFCAIM.Properties.Resources.fwd_icon;
            this.repViewDetail.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repViewDetail.Name = "repViewDetail";
            this.repViewDetail.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repViewDetail.Click += new System.EventHandler(this.repViewDetail_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.btnPreviewGridResults);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(814, 51);
            this.panel4.TabIndex = 154;
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Location = new System.Drawing.Point(4, 3);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(117, 39);
            this.pnlTopBtns.TabIndex = 155;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(334, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(166, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Exports Pending";
            // 
            // ExportBatchPending
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(817, 470);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.grdHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExportBatchPending";
            this.Text = "Export Pending";
            this.Load += new System.EventHandler(this.ExportBatchPending_Load);
            this.SizeChanged += new System.EventHandler(this.ExportBatchPending_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.grdHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repViewDetail)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;
		private DevExpress.XtraGrid.GridControl grdHeader;
		private DevExpress.XtraGrid.Views.Grid.GridView viewHeader;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateIngredientHeaderID;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateName;
		private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
		private DevExpress.XtraGrid.Columns.GridColumn colUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colStatus;
		private DevExpress.XtraGrid.Columns.GridColumn colViewDetail;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repViewDetail;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
		private System.Windows.Forms.Button btnPreviewGridResults;
		private DevExpress.XtraGrid.Columns.GridColumn ColTarget;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlTopBtns;
	}
}