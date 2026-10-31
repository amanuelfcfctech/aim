namespace CFCAIM.Sample
{
	partial class xSampleFurtherAnalysisPending
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xSampleFurtherAnalysisPending));
			DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions7 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject25 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject26 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject27 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject28 = new DevExpress.Utils.SerializableAppearanceObject();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.btnResize = new System.Windows.Forms.Button();
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.grdSampleType = new DevExpress.XtraGrid.GridControl();
			this.grdSampleTypeView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colSampleTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repbtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.panel4.SuspendLayout();
			this.pnlTopBtns.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdSampleType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdSampleTypeView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).BeginInit();
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
			this.panel4.Location = new System.Drawing.Point(1, 1);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(753, 51);
			this.panel4.TabIndex = 157;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.label10.ForeColor = System.Drawing.Color.White;
			this.label10.Location = new System.Drawing.Point(225, 9);
			this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(335, 23);
			this.label10.TabIndex = 149;
			this.label10.Text = "Sample Further Analysis - Pending";
			// 
			// btnResize
			// 
			this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
			this.btnResize.Location = new System.Drawing.Point(709, 2);
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
			// grdSampleType
			// 
			this.grdSampleType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdSampleType.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
			this.grdSampleType.Location = new System.Drawing.Point(1, 153);
			this.grdSampleType.MainView = this.grdSampleTypeView;
			this.grdSampleType.Margin = new System.Windows.Forms.Padding(2);
			this.grdSampleType.Name = "grdSampleType";
			this.grdSampleType.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repbtnDelete});
			this.grdSampleType.Size = new System.Drawing.Size(753, 356);
			this.grdSampleType.TabIndex = 159;
			this.grdSampleType.TabStop = false;
			this.grdSampleType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdSampleTypeView});
			// 
			// grdSampleTypeView
			// 
			this.grdSampleTypeView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.grdSampleTypeView.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.grdSampleTypeView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.grdSampleTypeView.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.grdSampleTypeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSampleTypeID,
            this.colName,
            this.colIsActive,
            this.colUpdateStamp,
            this.colCFCUserID,
            this.colUserName,
            this.colDelete});
			this.grdSampleTypeView.GridControl = this.grdSampleType;
			this.grdSampleTypeView.Name = "grdSampleTypeView";
			this.grdSampleTypeView.OptionsPrint.EnableAppearanceEvenRow = true;
			this.grdSampleTypeView.OptionsPrint.EnableAppearanceOddRow = true;
			this.grdSampleTypeView.OptionsView.EnableAppearanceEvenRow = true;
			// 
			// colSampleTypeID
			// 
			this.colSampleTypeID.Caption = "SampleTypeID";
			this.colSampleTypeID.FieldName = "SampleTypeID";
			this.colSampleTypeID.Name = "colSampleTypeID";
			this.colSampleTypeID.OptionsColumn.ShowInCustomizationForm = false;
			// 
			// colName
			// 
			this.colName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colName.AppearanceCell.Options.UseFont = true;
			this.colName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colName.AppearanceHeader.Options.UseFont = true;
			this.colName.Caption = "Sample Type Name";
			this.colName.FieldName = "SampleTypeName";
			this.colName.Name = "colName";
			this.colName.Visible = true;
			this.colName.VisibleIndex = 0;
			this.colName.Width = 301;
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
			this.colDelete.Caption = "Active";
			this.colDelete.ColumnEdit = this.repbtnDelete;
			this.colDelete.Name = "colDelete";
			this.colDelete.OptionsColumn.ShowInCustomizationForm = false;
			this.colDelete.Visible = true;
			this.colDelete.VisibleIndex = 1;
			this.colDelete.Width = 86;
			// 
			// repbtnDelete
			// 
			this.repbtnDelete.AutoHeight = false;
			editorButtonImageOptions7.Image = global::CFCAIM.Properties.Resources.delete;
			this.repbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject25, serializableAppearanceObject26, serializableAppearanceObject27, serializableAppearanceObject28, "", "Delete", null, DevExpress.Utils.ToolTipAnchor.Default)});
			this.repbtnDelete.Name = "repbtnDelete";
			this.repbtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			// 
			// SampleFurtherAnalysisPending
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(753, 505);
			this.ControlBox = false;
			this.Controls.Add(this.grdSampleType);
			this.Controls.Add(this.panel4);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SampleFurtherAnalysisPending";
			this.Text = "Sample Further Analysis - Pending";
			this.Load += new System.EventHandler(this.SampleFurtherAnalysisPending_Load);
			this.SizeChanged += new System.EventHandler(this.SampleFurtherAnalysisPending_SizeChanged);
			this.Resize += new System.EventHandler(this.btnResize_Click);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.pnlTopBtns.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdSampleType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdSampleTypeView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnResize;
		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;
		private DevExpress.XtraGrid.GridControl grdSampleType;
		private DevExpress.XtraGrid.Views.Grid.GridView grdSampleTypeView;
		private DevExpress.XtraGrid.Columns.GridColumn colSampleTypeID;
		private DevExpress.XtraGrid.Columns.GridColumn colName;
		private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
		private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
		private DevExpress.XtraGrid.Columns.GridColumn colUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colDelete;
		private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDelete;
	}
}