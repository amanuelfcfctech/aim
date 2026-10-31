namespace CFCAIM.Sample
{
    partial class SampleType
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleType));
			DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.btnResize = new System.Windows.Forms.Button();
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.pnlAdd = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rbActiveRecords = new System.Windows.Forms.RadioButton();
			this.rbDeletedRecords = new System.Windows.Forms.RadioButton();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnPreviewGridResults = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblDataSourceUserName = new System.Windows.Forms.Label();
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
			this.pnlAdd.SuspendLayout();
			this.groupBox2.SuspendLayout();
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
			this.panel4.Location = new System.Drawing.Point(1, 2);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(778, 51);
			this.panel4.TabIndex = 156;
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
			this.label10.Size = new System.Drawing.Size(142, 23);
			this.label10.TabIndex = 149;
			this.label10.Text = "Sample Types";
			// 
			// btnResize
			// 
			this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
			this.btnResize.Location = new System.Drawing.Point(734, 2);
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
			// pnlAdd
			// 
			this.pnlAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlAdd.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlAdd.Controls.Add(this.groupBox2);
			this.pnlAdd.Controls.Add(this.btnAdd);
			this.pnlAdd.Controls.Add(this.btnPreviewGridResults);
			this.pnlAdd.Controls.Add(this.txtName);
			this.pnlAdd.Controls.Add(this.lblDataSourceUserName);
			this.pnlAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlAdd.Location = new System.Drawing.Point(1, 51);
			this.pnlAdd.Margin = new System.Windows.Forms.Padding(2);
			this.pnlAdd.Name = "pnlAdd";
			this.pnlAdd.Size = new System.Drawing.Size(775, 83);
			this.pnlAdd.TabIndex = 157;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rbActiveRecords);
			this.groupBox2.Controls.Add(this.rbDeletedRecords);
			this.groupBox2.Location = new System.Drawing.Point(338, 14);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox2.Size = new System.Drawing.Size(132, 51);
			this.groupBox2.TabIndex = 155;
			this.groupBox2.TabStop = false;
			// 
			// rbActiveRecords
			// 
			this.rbActiveRecords.AutoSize = true;
			this.rbActiveRecords.Checked = true;
			this.rbActiveRecords.Font = new System.Drawing.Font("Arial", 9F);
			this.rbActiveRecords.Location = new System.Drawing.Point(6, 10);
			this.rbActiveRecords.Name = "rbActiveRecords";
			this.rbActiveRecords.Size = new System.Drawing.Size(106, 19);
			this.rbActiveRecords.TabIndex = 57;
			this.rbActiveRecords.TabStop = true;
			this.rbActiveRecords.Text = "Active Records";
			this.rbActiveRecords.UseVisualStyleBackColor = true;
			this.rbActiveRecords.CheckedChanged += new System.EventHandler(this.rbActiveRecords_CheckedChanged);
			// 
			// rbDeletedRecords
			// 
			this.rbDeletedRecords.AutoSize = true;
			this.rbDeletedRecords.Font = new System.Drawing.Font("Arial", 9F);
			this.rbDeletedRecords.Location = new System.Drawing.Point(6, 29);
			this.rbDeletedRecords.Name = "rbDeletedRecords";
			this.rbDeletedRecords.Size = new System.Drawing.Size(116, 19);
			this.rbDeletedRecords.TabIndex = 58;
			this.rbDeletedRecords.Text = "Inactive Records";
			this.rbDeletedRecords.UseVisualStyleBackColor = true;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
			this.btnAdd.Location = new System.Drawing.Point(649, 39);
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
			this.btnPreviewGridResults.Location = new System.Drawing.Point(710, 39);
			this.btnPreviewGridResults.Margin = new System.Windows.Forms.Padding(2);
			this.btnPreviewGridResults.Name = "btnPreviewGridResults";
			this.btnPreviewGridResults.Size = new System.Drawing.Size(57, 37);
			this.btnPreviewGridResults.TabIndex = 58;
			this.btnPreviewGridResults.TabStop = false;
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
			this.lblDataSourceUserName.Size = new System.Drawing.Size(115, 15);
			this.lblDataSourceUserName.TabIndex = 31;
			this.lblDataSourceUserName.Text = "Sample Type Name";
			// 
			// grdSampleType
			// 
			this.grdSampleType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdSampleType.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
			this.grdSampleType.Location = new System.Drawing.Point(2, 137);
			this.grdSampleType.MainView = this.grdSampleTypeView;
			this.grdSampleType.Margin = new System.Windows.Forms.Padding(2);
			this.grdSampleType.Name = "grdSampleType";
			this.grdSampleType.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repbtnDelete});
			this.grdSampleType.Size = new System.Drawing.Size(772, 318);
			this.grdSampleType.TabIndex = 158;
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
			this.grdSampleTypeView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.grdSampleTypeView_RowCellStyle);
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
			editorButtonImageOptions1.Image = global::CFCAIM.Properties.Resources.delete;
			this.repbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", "Delete", null, DevExpress.Utils.ToolTipAnchor.Default)});
			this.repbtnDelete.Name = "repbtnDelete";
			this.repbtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.repbtnDelete.Click += new System.EventHandler(this.repbtnDelete_Click);
			// 
			// SampleType
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(780, 458);
			this.Controls.Add(this.pnlAdd);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.grdSampleType);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SampleType";
			this.Text = "Sample Types";
			this.Load += new System.EventHandler(this.SampleType_Load);
			this.SizeChanged += new System.EventHandler(this.SampleType_SizeChanged);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.pnlTopBtns.ResumeLayout(false);
			this.pnlAdd.ResumeLayout(false);
			this.pnlAdd.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Panel pnlAdd;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnPreviewGridResults;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDataSourceUserName;
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbActiveRecords;
        private System.Windows.Forms.RadioButton rbDeletedRecords;
    }
}