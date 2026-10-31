namespace CFCAIM
{
	partial class DataSourceUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataSourceUser));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.btnDataSourceCrossReference = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnPreviewGridResults = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDataSourceUserName = new System.Windows.Forms.Label();
            this.grdDataSourceUser = new DevExpress.XtraGrid.GridControl();
            this.grdDataSourceUserView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDataSourceUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataSourceTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsFurtherAnalyses = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colIsUserDefined = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeDisplay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlTopBtns.SuspendLayout();
            this.pnlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataSourceUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataSourceUserView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
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
            // pnlAdd
            // 
            this.pnlAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAdd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAdd.Controls.Add(this.btnDataSourceCrossReference);
            this.pnlAdd.Controls.Add(this.txtCode);
            this.pnlAdd.Controls.Add(this.label1);
            this.pnlAdd.Controls.Add(this.btnAdd);
            this.pnlAdd.Controls.Add(this.btnPreviewGridResults);
            this.pnlAdd.Controls.Add(this.txtName);
            this.pnlAdd.Controls.Add(this.lblDataSourceUserName);
            this.pnlAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAdd.Location = new System.Drawing.Point(14, 55);
            this.pnlAdd.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(962, 83);
            this.pnlAdd.TabIndex = 52;
            // 
            // btnDataSourceCrossReference
            // 
            this.btnDataSourceCrossReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDataSourceCrossReference.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataSourceCrossReference.Location = new System.Drawing.Point(766, 5);
            this.btnDataSourceCrossReference.Margin = new System.Windows.Forms.Padding(2);
            this.btnDataSourceCrossReference.Name = "btnDataSourceCrossReference";
            this.btnDataSourceCrossReference.Size = new System.Drawing.Size(188, 30);
            this.btnDataSourceCrossReference.TabIndex = 166;
            this.btnDataSourceCrossReference.Text = "Cross Reference AIM Data Source";
            this.btnDataSourceCrossReference.UseVisualStyleBackColor = true;
            this.btnDataSourceCrossReference.Click += new System.EventHandler(this.btnDataSourceCrossReference_Click);
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Arial", 9F);
            this.txtCode.Location = new System.Drawing.Point(264, 54);
            this.txtCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtCode.MaxLength = 30;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(115, 21);
            this.txtCode.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F);
            this.label1.Location = new System.Drawing.Point(264, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 60;
            this.label1.Text = "Data Source Code";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAdd.Location = new System.Drawing.Point(836, 39);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 37);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnPreviewGridResults
            // 
            this.btnPreviewGridResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviewGridResults.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewGridResults.Location = new System.Drawing.Point(897, 39);
            this.btnPreviewGridResults.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewGridResults.Name = "btnPreviewGridResults";
            this.btnPreviewGridResults.Size = new System.Drawing.Size(57, 37);
            this.btnPreviewGridResults.TabIndex = 58;
            this.btnPreviewGridResults.UseVisualStyleBackColor = true;
            this.btnPreviewGridResults.Click += new System.EventHandler(this.btnPreviewGridResults_Click);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Arial", 9F);
            this.txtName.Location = new System.Drawing.Point(13, 54);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.MaxLength = 30;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(242, 21);
            this.txtName.TabIndex = 1;
            // 
            // lblDataSourceUserName
            // 
            this.lblDataSourceUserName.AutoSize = true;
            this.lblDataSourceUserName.Font = new System.Drawing.Font("Arial", 9F);
            this.lblDataSourceUserName.Location = new System.Drawing.Point(13, 38);
            this.lblDataSourceUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDataSourceUserName.Name = "lblDataSourceUserName";
            this.lblDataSourceUserName.Size = new System.Drawing.Size(112, 15);
            this.lblDataSourceUserName.TabIndex = 31;
            this.lblDataSourceUserName.Text = "Data Source Name";
            // 
            // grdDataSourceUser
            // 
            this.grdDataSourceUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDataSourceUser.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataSourceUser.Location = new System.Drawing.Point(14, 141);
            this.grdDataSourceUser.MainView = this.grdDataSourceUserView;
            this.grdDataSourceUser.Margin = new System.Windows.Forms.Padding(2);
            this.grdDataSourceUser.Name = "grdDataSourceUser";
            this.grdDataSourceUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repbtnDelete});
            this.grdDataSourceUser.Size = new System.Drawing.Size(965, 356);
            this.grdDataSourceUser.TabIndex = 53;
            this.grdDataSourceUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdDataSourceUserView});
            // 
            // grdDataSourceUserView
            // 
            this.grdDataSourceUserView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdDataSourceUserView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdDataSourceUserView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdDataSourceUserView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdDataSourceUserView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDataSourceUserID,
            this.colName,
            this.colDataSourceTypeCode,
            this.colIsActive,
            this.colUpdateStamp,
            this.colCFCUserID,
            this.colIsFurtherAnalyses,
            this.colUserName,
            this.colDelete,
            this.colIsUserDefined,
            this.colTypeDisplay});
            this.grdDataSourceUserView.GridControl = this.grdDataSourceUser;
            this.grdDataSourceUserView.Name = "grdDataSourceUserView";
            this.grdDataSourceUserView.OptionsCustomization.AllowRowSizing = true;
            this.grdDataSourceUserView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdDataSourceUserView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdDataSourceUserView.OptionsView.RowAutoHeight = true;
            this.grdDataSourceUserView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grdDataSourceUserView_CustomDrawCell);
            this.grdDataSourceUserView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdDataSourceUserView_ValidateRow);
            // 
            // colDataSourceUserID
            // 
            this.colDataSourceUserID.Caption = "DataSourceUserID";
            this.colDataSourceUserID.FieldName = "DataSourceUserID";
            this.colDataSourceUserID.Name = "colDataSourceUserID";
            this.colDataSourceUserID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colName
            // 
            this.colName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colName.AppearanceCell.Options.UseFont = true;
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.Caption = "Data Source Name";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 301;
            // 
            // colDataSourceTypeCode
            // 
            this.colDataSourceTypeCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colDataSourceTypeCode.AppearanceCell.Options.UseFont = true;
            this.colDataSourceTypeCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colDataSourceTypeCode.AppearanceHeader.Options.UseFont = true;
            this.colDataSourceTypeCode.Caption = "Data Source Code";
            this.colDataSourceTypeCode.FieldName = "DataSourceTypeCode";
            this.colDataSourceTypeCode.Name = "colDataSourceTypeCode";
            this.colDataSourceTypeCode.Visible = true;
            this.colDataSourceTypeCode.VisibleIndex = 1;
            this.colDataSourceTypeCode.Width = 231;
            // 
            // colIsActive
            // 
            this.colIsActive.Caption = "Active";
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            // 
            // colUpdateStamp
            // 
            this.colUpdateStamp.Caption = "UpdateSamp";
            this.colUpdateStamp.FieldName = "UpdateStamp";
            this.colUpdateStamp.Name = "colUpdateStamp";
            // 
            // colCFCUserID
            // 
            this.colCFCUserID.Caption = "CFCUserID";
            this.colCFCUserID.FieldName = "CFCUserID";
            this.colCFCUserID.Name = "colCFCUserID";
            // 
            // colIsFurtherAnalyses
            // 
            this.colIsFurtherAnalyses.Caption = "IsFurther Analyses";
            this.colIsFurtherAnalyses.FieldName = "IsFurtherAnalyses";
            this.colIsFurtherAnalyses.Name = "colIsFurtherAnalyses";
            // 
            // colUserName
            // 
            this.colUserName.Caption = "UserName";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            // 
            // colDelete
            // 
            this.colDelete.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDelete.AppearanceCell.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDelete.AppearanceHeader.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.repbtnDelete;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.ShowInCustomizationForm = false;
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 3;
            this.colDelete.Width = 86;
            // 
            // repbtnDelete
            // 
            this.repbtnDelete.AutoHeight = false;
            editorButtonImageOptions4.Image = global::CFCAIM.Properties.Resources.delete;
            this.repbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "", "Delete", null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDelete.Name = "repbtnDelete";
            this.repbtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repbtnDelete_ButtonClick);
            // 
            // colIsUserDefined
            // 
            this.colIsUserDefined.Caption = "IsUserDefined";
            this.colIsUserDefined.FieldName = "IsUserDefined";
            this.colIsUserDefined.Name = "colIsUserDefined";
            // 
            // colTypeDisplay
            // 
            this.colTypeDisplay.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colTypeDisplay.AppearanceCell.Options.UseFont = true;
            this.colTypeDisplay.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colTypeDisplay.AppearanceHeader.Options.UseFont = true;
            this.colTypeDisplay.Caption = "Type";
            this.colTypeDisplay.FieldName = "TypeDisplay";
            this.colTypeDisplay.Name = "colTypeDisplay";
            this.colTypeDisplay.Visible = true;
            this.colTypeDisplay.VisibleIndex = 2;
            this.colTypeDisplay.Width = 239;
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
            this.panel4.Size = new System.Drawing.Size(980, 51);
            this.panel4.TabIndex = 155;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(310, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(135, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Data Sources";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(941, 0);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 112;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // DataSourceUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(981, 501);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.grdDataSourceUser);
            this.Controls.Add(this.pnlAdd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DataSourceUser";
            this.Text = "Data Sources";
            this.Load += new System.EventHandler(this.DataSourceUser_Load);
            this.SizeChanged += new System.EventHandler(this.DataSourceUser_SizeChanged);
            this.pnlTopBtns.ResumeLayout(false);
            this.pnlAdd.ResumeLayout(false);
            this.pnlAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataSourceUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDataSourceUserView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Panel pnlAdd;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnPreviewGridResults;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblDataSourceUserName;
		private DevExpress.XtraGrid.GridControl grdDataSourceUser;
		private DevExpress.XtraGrid.Views.Grid.GridView grdDataSourceUserView;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSourceUserID;
		private DevExpress.XtraGrid.Columns.GridColumn colName;
		private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeCode;
		private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
		private DevExpress.XtraGrid.Columns.GridColumn colIsFurtherAnalyses;
		private DevExpress.XtraGrid.Columns.GridColumn colUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colDelete;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDelete;
		private DevExpress.XtraGrid.Columns.GridColumn colIsUserDefined;
		private DevExpress.XtraGrid.Columns.GridColumn colTypeDisplay;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Button btnDataSourceCrossReference;
	}
}