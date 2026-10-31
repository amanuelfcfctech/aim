namespace CFCAIM
{
    partial class FileTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileTypes));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grdFileType = new DevExpress.XtraGrid.GridControl();
            this.grdFileTypeView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDataSourceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeleteFileType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDeleteFileType = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colDataSourceTypeRefFileTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDataSourceTypeID2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSystem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFileType = new System.Windows.Forms.Label();
            this.lueFileType = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDataSource = new System.Windows.Forms.Label();
            this.lueDataSourceType = new DevExpress.XtraEditors.LookUpEdit();
            this.btnAddFileType = new System.Windows.Forms.Button();
            this.btnPreviewGridFileType = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileTypeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteFileType)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueFileType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(4, 5);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(115, 38);
            this.pnlTopBtns.TabIndex = 31;
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
            // grdFileType
            // 
            this.grdFileType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFileType.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdFileType.Location = new System.Drawing.Point(14, 151);
            this.grdFileType.MainView = this.grdFileTypeView;
            this.grdFileType.Margin = new System.Windows.Forms.Padding(2);
            this.grdFileType.Name = "grdFileType";
            this.grdFileType.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repbtnDeleteFileType});
            this.grdFileType.Size = new System.Drawing.Size(698, 262);
            this.grdFileType.TabIndex = 55;
            this.grdFileType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdFileTypeView});
            // 
            // grdFileTypeView
            // 
            this.grdFileTypeView.Appearance.GroupPanel.BackColor = System.Drawing.Color.White;
            this.grdFileTypeView.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.grdFileTypeView.Appearance.GroupPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.grdFileTypeView.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.grdFileTypeView.Appearance.GroupPanel.Options.UseBackColor = true;
            this.grdFileTypeView.Appearance.GroupPanel.Options.UseBorderColor = true;
            this.grdFileTypeView.Appearance.GroupPanel.Options.UseForeColor = true;
            this.grdFileTypeView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdFileTypeView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdFileTypeView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdFileTypeView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdFileTypeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDataSourceName,
            this.colFileTypeName,
            this.colDeleteFileType,
            this.colDataSourceTypeRefFileTypeID,
            this.colDataSourceTypeID2,
            this.colFileTypeID,
            this.colIsSystem});
            this.grdFileTypeView.DetailHeight = 280;
            this.grdFileTypeView.GridControl = this.grdFileType;
            this.grdFileTypeView.Name = "grdFileTypeView";
            this.grdFileTypeView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdFileTypeView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdFileTypeView.PaintStyleName = "Flat";
            // 
            // colDataSourceName
            // 
            this.colDataSourceName.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colDataSourceName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDataSourceName.AppearanceCell.Options.UseBackColor = true;
            this.colDataSourceName.AppearanceCell.Options.UseFont = true;
            this.colDataSourceName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDataSourceName.AppearanceHeader.Options.UseFont = true;
            this.colDataSourceName.Caption = "Data Source";
            this.colDataSourceName.FieldName = "DataSourceTypeName";
            this.colDataSourceName.MinWidth = 16;
            this.colDataSourceName.Name = "colDataSourceName";
            this.colDataSourceName.OptionsColumn.AllowEdit = false;
            this.colDataSourceName.Visible = true;
            this.colDataSourceName.VisibleIndex = 0;
            this.colDataSourceName.Width = 294;
            // 
            // colFileTypeName
            // 
            this.colFileTypeName.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colFileTypeName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colFileTypeName.AppearanceCell.Options.UseBackColor = true;
            this.colFileTypeName.AppearanceCell.Options.UseFont = true;
            this.colFileTypeName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colFileTypeName.AppearanceHeader.Options.UseFont = true;
            this.colFileTypeName.Caption = "File Type";
            this.colFileTypeName.FieldName = "FileTypeName";
            this.colFileTypeName.MinWidth = 16;
            this.colFileTypeName.Name = "colFileTypeName";
            this.colFileTypeName.OptionsColumn.AllowEdit = false;
            this.colFileTypeName.Visible = true;
            this.colFileTypeName.VisibleIndex = 1;
            this.colFileTypeName.Width = 296;
            // 
            // colDeleteFileType
            // 
            this.colDeleteFileType.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDeleteFileType.AppearanceCell.Options.UseFont = true;
            this.colDeleteFileType.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDeleteFileType.AppearanceHeader.Options.UseFont = true;
            this.colDeleteFileType.AppearanceHeader.Options.UseTextOptions = true;
            this.colDeleteFileType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeleteFileType.Caption = "Delete";
            this.colDeleteFileType.ColumnEdit = this.repbtnDeleteFileType;
            this.colDeleteFileType.MinWidth = 16;
            this.colDeleteFileType.Name = "colDeleteFileType";
            this.colDeleteFileType.OptionsColumn.ShowInCustomizationForm = false;
            this.colDeleteFileType.Visible = true;
            this.colDeleteFileType.VisibleIndex = 2;
            this.colDeleteFileType.Width = 94;
            // 
            // repbtnDeleteFileType
            // 
            this.repbtnDeleteFileType.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.repbtnDeleteFileType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", "Delete", null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDeleteFileType.Name = "repbtnDeleteFileType";
            this.repbtnDeleteFileType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDeleteFileType.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repbtnDeleteFileType_ButtonClick);
            // 
            // colDataSourceTypeRefFileTypeID
            // 
            this.colDataSourceTypeRefFileTypeID.Caption = "DataSourceTypeRefFileTypeID";
            this.colDataSourceTypeRefFileTypeID.FieldName = "DataSourceTypeRefFileTypeID";
            this.colDataSourceTypeRefFileTypeID.MinWidth = 16;
            this.colDataSourceTypeRefFileTypeID.Name = "colDataSourceTypeRefFileTypeID";
            this.colDataSourceTypeRefFileTypeID.Width = 60;
            // 
            // colDataSourceTypeID2
            // 
            this.colDataSourceTypeID2.Caption = "DataSourceTypeID";
            this.colDataSourceTypeID2.FieldName = "DataSourceTypeID";
            this.colDataSourceTypeID2.MinWidth = 16;
            this.colDataSourceTypeID2.Name = "colDataSourceTypeID2";
            this.colDataSourceTypeID2.OptionsColumn.ShowInCustomizationForm = false;
            this.colDataSourceTypeID2.Width = 60;
            // 
            // colFileTypeID
            // 
            this.colFileTypeID.Caption = "File Type ID";
            this.colFileTypeID.FieldName = "FileTypeID";
            this.colFileTypeID.MinWidth = 16;
            this.colFileTypeID.Name = "colFileTypeID";
            this.colFileTypeID.Width = 60;
            // 
            // colIsSystem
            // 
            this.colIsSystem.Caption = "IsSystem";
            this.colIsSystem.FieldName = "IsSystem";
            this.colIsSystem.MinWidth = 16;
            this.colIsSystem.Name = "colIsSystem";
            this.colIsSystem.Width = 60;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblFileType);
            this.panel1.Controls.Add(this.lueFileType);
            this.panel1.Controls.Add(this.lblDataSource);
            this.panel1.Controls.Add(this.lueDataSourceType);
            this.panel1.Controls.Add(this.btnAddFileType);
            this.panel1.Controls.Add(this.btnPreviewGridFileType);
            this.panel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(14, 95);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 53);
            this.panel1.TabIndex = 54;
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Font = new System.Drawing.Font("Arial", 9F);
            this.lblFileType.Location = new System.Drawing.Point(306, 8);
            this.lblFileType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(55, 15);
            this.lblFileType.TabIndex = 69;
            this.lblFileType.Text = "File Type";
            // 
            // lueFileType
            // 
            this.lueFileType.Location = new System.Drawing.Point(306, 24);
            this.lueFileType.Margin = new System.Windows.Forms.Padding(4);
            this.lueFileType.Name = "lueFileType";
            this.lueFileType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueFileType.Properties.Appearance.Options.UseFont = true;
            this.lueFileType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueFileType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueFileType.Properties.DropDownRows = 12;
            this.lueFileType.Size = new System.Drawing.Size(115, 22);
            this.lueFileType.TabIndex = 68;
            // 
            // lblDataSource
            // 
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Font = new System.Drawing.Font("Arial", 9F);
            this.lblDataSource.Location = new System.Drawing.Point(14, 8);
            this.lblDataSource.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(75, 15);
            this.lblDataSource.TabIndex = 67;
            this.lblDataSource.Text = "Data Source";
            // 
            // lueDataSourceType
            // 
            this.lueDataSourceType.Location = new System.Drawing.Point(14, 24);
            this.lueDataSourceType.Margin = new System.Windows.Forms.Padding(4);
            this.lueDataSourceType.Name = "lueDataSourceType";
            this.lueDataSourceType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDataSourceType.Properties.Appearance.Options.UseFont = true;
            this.lueDataSourceType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueDataSourceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDataSourceType.Properties.DropDownRows = 25;
            this.lueDataSourceType.Size = new System.Drawing.Size(241, 22);
            this.lueDataSourceType.TabIndex = 66;
            this.lueDataSourceType.EditValueChanged += new System.EventHandler(this.lueDataSourceType_EditValueChanged);
            // 
            // btnAddFileType
            // 
            this.btnAddFileType.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFileType.Image")));
            this.btnAddFileType.Location = new System.Drawing.Point(565, 7);
            this.btnAddFileType.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddFileType.Name = "btnAddFileType";
            this.btnAddFileType.Size = new System.Drawing.Size(57, 37);
            this.btnAddFileType.TabIndex = 56;
            this.btnAddFileType.UseVisualStyleBackColor = true;
            this.btnAddFileType.Click += new System.EventHandler(this.btnAddFileType_Click);
            // 
            // btnPreviewGridFileType
            // 
            this.btnPreviewGridFileType.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviewGridFileType.Image")));
            this.btnPreviewGridFileType.Location = new System.Drawing.Point(626, 7);
            this.btnPreviewGridFileType.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewGridFileType.Name = "btnPreviewGridFileType";
            this.btnPreviewGridFileType.Size = new System.Drawing.Size(57, 37);
            this.btnPreviewGridFileType.TabIndex = 58;
            this.btnPreviewGridFileType.UseVisualStyleBackColor = true;
            this.btnPreviewGridFileType.Click += new System.EventHandler(this.btnPreviewGridFileType_Click);
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
            this.panel4.Size = new System.Drawing.Size(716, 51);
            this.panel4.TabIndex = 154;
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
            this.label10.Size = new System.Drawing.Size(267, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "File Types for Data Sources";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = ((System.Drawing.Image)(resources.GetObject("btnResize.Image")));
            this.btnResize.Location = new System.Drawing.Point(677, 0);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 112;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // FileTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(716, 418);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.grdFileType);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(13, 13);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FileTypes";
            this.Text = "File Types for Data Sources";
            this.Load += new System.EventHandler(this.FileTypes_Load);
            this.SizeChanged += new System.EventHandler(this.FileTypes_SizeChanged);
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFileType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileTypeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteFileType)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueFileType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private DevExpress.XtraGrid.GridControl grdFileType;
        private DevExpress.XtraGrid.Views.Grid.GridView grdFileTypeView;
        private DevExpress.XtraGrid.Columns.GridColumn colDataSourceName;
        private DevExpress.XtraGrid.Columns.GridColumn colFileTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colDeleteFileType;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeleteFileType;
        private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeRefFileTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colDataSourceTypeID2;
        private DevExpress.XtraGrid.Columns.GridColumn colFileTypeID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFileType;
        private DevExpress.XtraEditors.LookUpEdit lueFileType;
        private System.Windows.Forms.Label lblDataSource;
        private DevExpress.XtraEditors.LookUpEdit lueDataSourceType;
        private System.Windows.Forms.Button btnAddFileType;
        private System.Windows.Forms.Button btnPreviewGridFileType;
		private DevExpress.XtraGrid.Columns.GridColumn colIsSystem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
    }
}