namespace CFCAIM
{
	partial class Pending
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pending));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.grdHeader = new DevExpress.XtraGrid.GridControl();
			this.viewHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colUpdateIngredientHeaderID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUpdateName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDetail = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repbtnDetail = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.rbActive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.grdHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.viewHeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rbActive)).BeginInit();
			this.pnlTopBtns.SuspendLayout();
			this.SuspendLayout();
			// 
			// grdHeader
			// 
			this.grdHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdHeader.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grdHeader.Location = new System.Drawing.Point(3, 49);
			this.grdHeader.MainView = this.viewHeader;
			this.grdHeader.Name = "grdHeader";
			this.grdHeader.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rbActive,
            this.repbtnDetail});
			this.grdHeader.Size = new System.Drawing.Size(834, 363);
			this.grdHeader.TabIndex = 25;
			this.grdHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewHeader});
			this.grdHeader.Click += new System.EventHandler(this.grdHeader_Click);
			// 
			// viewHeader
			// 
			this.viewHeader.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.ColumnFilterButton.Options.UseFont = true;
			this.viewHeader.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
			this.viewHeader.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.CustomizationFormHint.Options.UseFont = true;
			this.viewHeader.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.DetailTip.Options.UseFont = true;
			this.viewHeader.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.Empty.Options.UseFont = true;
			this.viewHeader.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.EvenRow.Options.UseFont = true;
			this.viewHeader.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.FilterCloseButton.Options.UseFont = true;
			this.viewHeader.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.FilterPanel.Options.UseFont = true;
			this.viewHeader.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.FixedLine.Options.UseFont = true;
			this.viewHeader.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.FocusedCell.Options.UseFont = true;
			this.viewHeader.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.FocusedRow.Options.UseFont = true;
			this.viewHeader.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.FooterPanel.Options.UseFont = true;
			this.viewHeader.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.GroupButton.Options.UseFont = true;
			this.viewHeader.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.GroupFooter.Options.UseFont = true;
			this.viewHeader.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.GroupPanel.Options.UseFont = true;
			this.viewHeader.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.GroupRow.Options.UseFont = true;
			this.viewHeader.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.HeaderPanel.Options.UseFont = true;
			this.viewHeader.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.HideSelectionRow.Options.UseFont = true;
			this.viewHeader.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.HorzLine.Options.UseFont = true;
			this.viewHeader.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.OddRow.Options.UseFont = true;
			this.viewHeader.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.Preview.Options.UseFont = true;
			this.viewHeader.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.Row.Options.UseFont = true;
			this.viewHeader.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.RowSeparator.Options.UseFont = true;
			this.viewHeader.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.SelectedRow.Options.UseFont = true;
			this.viewHeader.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.TopNewRow.Options.UseFont = true;
			this.viewHeader.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.VertLine.Options.UseFont = true;
			this.viewHeader.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.Appearance.ViewCaption.Options.UseFont = true;
			this.viewHeader.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.EvenRow.Options.UseFont = true;
			this.viewHeader.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.FilterPanel.Options.UseFont = true;
			this.viewHeader.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.FooterPanel.Options.UseFont = true;
			this.viewHeader.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.GroupFooter.Options.UseFont = true;
			this.viewHeader.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.GroupRow.Options.UseFont = true;
			this.viewHeader.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.HeaderPanel.Options.UseFont = true;
			this.viewHeader.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.Lines.Options.UseFont = true;
			this.viewHeader.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.OddRow.Options.UseFont = true;
			this.viewHeader.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.Preview.Options.UseFont = true;
			this.viewHeader.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.viewHeader.AppearancePrint.Row.Options.UseFont = true;
			this.viewHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUpdateIngredientHeaderID,
            this.colUpdateName,
            this.colCreateDate,
            this.colCFCUserID,
            this.colStatus,
            this.colDetail});
			this.viewHeader.GridControl = this.grdHeader;
			this.viewHeader.Name = "viewHeader";
			this.viewHeader.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
			this.viewHeader.OptionsPrint.EnableAppearanceEvenRow = true;
			this.viewHeader.OptionsPrint.EnableAppearanceOddRow = true;
			// 
			// colUpdateIngredientHeaderID
			// 
			this.colUpdateIngredientHeaderID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colUpdateIngredientHeaderID.AppearanceCell.Options.UseFont = true;
			this.colUpdateIngredientHeaderID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colUpdateIngredientHeaderID.AppearanceHeader.Options.UseFont = true;
			this.colUpdateIngredientHeaderID.AppearanceHeader.Options.UseTextOptions = true;
			this.colUpdateIngredientHeaderID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colUpdateIngredientHeaderID.Caption = "UpdateIngredientHeaderID";
			this.colUpdateIngredientHeaderID.FieldName = "UpdateIngredientHeaderID";
			this.colUpdateIngredientHeaderID.Name = "colUpdateIngredientHeaderID";
			this.colUpdateIngredientHeaderID.Width = 86;
			// 
			// colUpdateName
			// 
			this.colUpdateName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colUpdateName.AppearanceCell.Options.UseFont = true;
			this.colUpdateName.AppearanceCell.Options.UseTextOptions = true;
			this.colUpdateName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.colUpdateName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colUpdateName.AppearanceHeader.Options.UseFont = true;
			this.colUpdateName.AppearanceHeader.Options.UseTextOptions = true;
			this.colUpdateName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colUpdateName.Caption = "Export Batch Name";
			this.colUpdateName.FieldName = "UpdateName";
			this.colUpdateName.Name = "colUpdateName";
			this.colUpdateName.OptionsColumn.ReadOnly = true;
			this.colUpdateName.Visible = true;
			this.colUpdateName.VisibleIndex = 0;
			this.colUpdateName.Width = 230;
			// 
			// colCreateDate
			// 
			this.colCreateDate.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCreateDate.AppearanceCell.Options.UseFont = true;
			this.colCreateDate.AppearanceCell.Options.UseTextOptions = true;
			this.colCreateDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCreateDate.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCreateDate.AppearanceHeader.Options.UseFont = true;
			this.colCreateDate.AppearanceHeader.Options.UseTextOptions = true;
			this.colCreateDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCreateDate.Caption = "Created Date";
			this.colCreateDate.FieldName = "CreateDate";
			this.colCreateDate.Name = "colCreateDate";
			this.colCreateDate.OptionsColumn.ReadOnly = true;
			this.colCreateDate.Visible = true;
			this.colCreateDate.VisibleIndex = 1;
			this.colCreateDate.Width = 158;
			// 
			// colCFCUserID
			// 
			this.colCFCUserID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCFCUserID.AppearanceCell.Options.UseFont = true;
			this.colCFCUserID.AppearanceCell.Options.UseTextOptions = true;
			this.colCFCUserID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCFCUserID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colCFCUserID.AppearanceHeader.Options.UseFont = true;
			this.colCFCUserID.AppearanceHeader.Options.UseTextOptions = true;
			this.colCFCUserID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCFCUserID.Caption = "User ID";
			this.colCFCUserID.FieldName = "CFCUserID";
			this.colCFCUserID.Name = "colCFCUserID";
			this.colCFCUserID.OptionsColumn.ReadOnly = true;
			this.colCFCUserID.Visible = true;
			this.colCFCUserID.VisibleIndex = 2;
			this.colCFCUserID.Width = 155;
			// 
			// colStatus
			// 
			this.colStatus.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colStatus.AppearanceCell.Options.UseFont = true;
			this.colStatus.AppearanceCell.Options.UseTextOptions = true;
			this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colStatus.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colStatus.AppearanceHeader.Options.UseFont = true;
			this.colStatus.AppearanceHeader.Options.UseTextOptions = true;
			this.colStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colStatus.Caption = "Status";
			this.colStatus.FieldName = "Status";
			this.colStatus.Name = "colStatus";
			this.colStatus.OptionsColumn.ReadOnly = true;
			this.colStatus.Visible = true;
			this.colStatus.VisibleIndex = 3;
			this.colStatus.Width = 144;
			// 
			// colDetail
			// 
			this.colDetail.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colDetail.AppearanceCell.Options.UseFont = true;
			this.colDetail.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colDetail.AppearanceHeader.Options.UseFont = true;
			this.colDetail.AppearanceHeader.Options.UseTextOptions = true;
			this.colDetail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colDetail.Caption = "View Detail";
			this.colDetail.ColumnEdit = this.repbtnDetail;
			this.colDetail.Name = "colDetail";
			this.colDetail.Visible = true;
			this.colDetail.VisibleIndex = 4;
			this.colDetail.Width = 149;
			// 
			// repbtnDetail
			// 
			this.repbtnDetail.AutoHeight = false;
			this.repbtnDetail.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repbtnDetail.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.repbtnDetail.Name = "repbtnDetail";
			this.repbtnDetail.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repbtnDetail.Click += new System.EventHandler(this.repbtnDetail_Click);
			// 
			// rbActive
			// 
			this.rbActive.AutoHeight = false;
			this.rbActive.Caption = "Check";
			this.rbActive.FullFocusRect = true;
			this.rbActive.ImageIndexChecked = 1;
			this.rbActive.ImageIndexUnchecked = 0;
			this.rbActive.LookAndFeel.SkinName = "DevExpress Dark Style";
			this.rbActive.Name = "rbActive";
			this.rbActive.Tag = true;
			this.rbActive.ValueGrayed = false;
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnRefresh);
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTopBtns.Location = new System.Drawing.Point(0, 0);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(837, 50);
			this.pnlTopBtns.TabIndex = 26;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
			this.btnRefresh.Location = new System.Drawing.Point(65, 7);
			this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(53, 37);
			this.btnRefresh.TabIndex = 22;
			this.btnRefresh.UseVisualStyleBackColor = true;
			// 
			// btnExit
			// 
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(11, 7);
			this.btnExit.Margin = new System.Windows.Forms.Padding(2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(50, 37);
			this.btnExit.TabIndex = 21;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// Pending
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(837, 424);
			this.Controls.Add(this.grdHeader);
			this.Controls.Add(this.pnlTopBtns);
			this.Name = "Pending";
			this.Text = "Pending";
			this.Load += new System.EventHandler(this.Pending_Load);
			((System.ComponentModel.ISupportInitialize)(this.grdHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.viewHeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rbActive)).EndInit();
			this.pnlTopBtns.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl grdHeader;
		private DevExpress.XtraGrid.Views.Grid.GridView viewHeader;
		private DevExpress.XtraGrid.Columns.GridColumn colDetail;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDetail;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateIngredientHeaderID;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateName;
		private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
		private DevExpress.XtraGrid.Columns.GridColumn colStatus;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rbActive;
		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnRefresh;
	}
}