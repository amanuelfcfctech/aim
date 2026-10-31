namespace CFCAIM.Cloud
{
    partial class PNECredit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PNECredit));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grdPNECredit = new DevExpress.XtraGrid.GridControl();
            this.grdPNECreditView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPNECreditID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPNEKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPNEDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDeletePNECredit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnlAddMaterial = new System.Windows.Forms.Panel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblMaterialName = new System.Windows.Forms.Label();
            this.lblMaterialCode = new System.Windows.Forms.Label();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPNECredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPNECreditView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeletePNECredit)).BeginInit();
            this.pnlAddMaterial.SuspendLayout();
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
            this.panel4.Size = new System.Drawing.Size(972, 51);
            this.panel4.TabIndex = 156;
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
            this.label10.Size = new System.Drawing.Size(123, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "PNE Credits";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(933, 0);
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
            this.pnlTopBtns.Size = new System.Drawing.Size(196, 38);
            this.pnlTopBtns.TabIndex = 104;
            // 
            // btnPreview
            // 
            this.btnPreview.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreview.Location = new System.Drawing.Point(112, 1);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(57, 37);
            this.btnPreview.TabIndex = 154;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
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
            this.btnRefresh.Click += new System.EventHandler(this.PNECredit_Load);
            // 
            // grdPNECredit
            // 
            this.grdPNECredit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPNECredit.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdPNECredit.Location = new System.Drawing.Point(3, 125);
            this.grdPNECredit.MainView = this.grdPNECreditView;
            this.grdPNECredit.Margin = new System.Windows.Forms.Padding(2);
            this.grdPNECredit.Name = "grdPNECredit";
            this.grdPNECredit.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repbtnDeletePNECredit});
            this.grdPNECredit.Size = new System.Drawing.Size(963, 446);
            this.grdPNECredit.TabIndex = 157;
            this.grdPNECredit.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdPNECreditView});
            // 
            // grdPNECreditView
            // 
            this.grdPNECreditView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdPNECreditView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdPNECreditView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdPNECreditView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdPNECreditView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPNECreditID,
            this.colCredit,
            this.colPNEKey,
            this.colPNEDescription,
            this.colUpdateStamp,
            this.colCFCUserName,
            this.colDelete});
            this.grdPNECreditView.DetailHeight = 280;
            this.grdPNECreditView.GridControl = this.grdPNECredit;
            this.grdPNECreditView.Name = "grdPNECreditView";
            this.grdPNECreditView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdPNECreditView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdPNECreditView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdPNECreditView_ValidateRow);
            // 
            // colPNECreditID
            // 
            this.colPNECreditID.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colPNECreditID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPNECreditID.AppearanceCell.Options.UseBackColor = true;
            this.colPNECreditID.AppearanceCell.Options.UseFont = true;
            this.colPNECreditID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPNECreditID.AppearanceHeader.Options.UseFont = true;
            this.colPNECreditID.Caption = "PNECreditID";
            this.colPNECreditID.FieldName = "PNECreditID";
            this.colPNECreditID.MinWidth = 16;
            this.colPNECreditID.Name = "colPNECreditID";
            this.colPNECreditID.OptionsColumn.AllowEdit = false;
            this.colPNECreditID.Width = 107;
            // 
            // colCredit
            // 
            this.colCredit.AppearanceCell.BackColor = System.Drawing.Color.White;
            this.colCredit.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCredit.AppearanceCell.Options.UseBackColor = true;
            this.colCredit.AppearanceCell.Options.UseFont = true;
            this.colCredit.AppearanceCell.Options.UseTextOptions = true;
            this.colCredit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCredit.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colCredit.AppearanceHeader.Options.UseFont = true;
            this.colCredit.AppearanceHeader.Options.UseTextOptions = true;
            this.colCredit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCredit.Caption = "Credit";
            this.colCredit.FieldName = "Credit";
            this.colCredit.MinWidth = 16;
            this.colCredit.Name = "colCredit";
            this.colCredit.OptionsColumn.AllowEdit = false;
            this.colCredit.Visible = true;
            this.colCredit.VisibleIndex = 0;
            this.colCredit.Width = 61;
            // 
            // colPNEKey
            // 
            this.colPNEKey.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPNEKey.AppearanceCell.Options.UseFont = true;
            this.colPNEKey.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPNEKey.AppearanceHeader.Options.UseFont = true;
            this.colPNEKey.AppearanceHeader.Options.UseTextOptions = true;
            this.colPNEKey.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPNEKey.Caption = "PNE Key";
            this.colPNEKey.FieldName = "PNEKey";
            this.colPNEKey.MinWidth = 16;
            this.colPNEKey.Name = "colPNEKey";
            this.colPNEKey.OptionsColumn.ShowInCustomizationForm = false;
            this.colPNEKey.Visible = true;
            this.colPNEKey.VisibleIndex = 1;
            this.colPNEKey.Width = 237;
            // 
            // colPNEDescription
            // 
            this.colPNEDescription.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colPNEDescription.AppearanceCell.Options.UseFont = true;
            this.colPNEDescription.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colPNEDescription.AppearanceHeader.Options.UseFont = true;
            this.colPNEDescription.Caption = "PNE Description";
            this.colPNEDescription.FieldName = "PNEDescription";
            this.colPNEDescription.MinWidth = 16;
            this.colPNEDescription.Name = "colPNEDescription";
            this.colPNEDescription.Visible = true;
            this.colPNEDescription.VisibleIndex = 2;
            this.colPNEDescription.Width = 238;
            // 
            // colUpdateStamp
            // 
            this.colUpdateStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colUpdateStamp.AppearanceCell.Options.UseFont = true;
            this.colUpdateStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colUpdateStamp.AppearanceHeader.Options.UseFont = true;
            this.colUpdateStamp.Caption = "Update Stamp";
            this.colUpdateStamp.FieldName = "UpdateStamp";
            this.colUpdateStamp.MinWidth = 16;
            this.colUpdateStamp.Name = "colUpdateStamp";
            this.colUpdateStamp.OptionsColumn.AllowEdit = false;
            this.colUpdateStamp.OptionsColumn.ShowInCustomizationForm = false;
            this.colUpdateStamp.Visible = true;
            this.colUpdateStamp.VisibleIndex = 3;
            this.colUpdateStamp.Width = 156;
            // 
            // colCFCUserName
            // 
            this.colCFCUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCUserName.AppearanceCell.Options.UseFont = true;
            this.colCFCUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colCFCUserName.AppearanceHeader.Options.UseFont = true;
            this.colCFCUserName.Caption = "User Name";
            this.colCFCUserName.FieldName = "UserName";
            this.colCFCUserName.MinWidth = 16;
            this.colCFCUserName.Name = "colCFCUserName";
            this.colCFCUserName.OptionsColumn.AllowEdit = false;
            this.colCFCUserName.Visible = true;
            this.colCFCUserName.VisibleIndex = 4;
            this.colCFCUserName.Width = 188;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colDelete.AppearanceHeader.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.repbtnDeletePNECredit;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 5;
            this.colDelete.Width = 58;
            // 
            // repbtnDeletePNECredit
            // 
            this.repbtnDeletePNECredit.AutoHeight = false;
            editorButtonImageOptions1.Image = global::CFCAIM.Properties.Resources.delete;
            this.repbtnDeletePNECredit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", "Delete", null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDeletePNECredit.Name = "repbtnDeletePNECredit";
            this.repbtnDeletePNECredit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDeletePNECredit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repbtnDeletePNECredit_ButtonClick);
            // 
            // pnlAddMaterial
            // 
            this.pnlAddMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAddMaterial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAddMaterial.Controls.Add(this.txtDescription);
            this.pnlAddMaterial.Controls.Add(this.txtKey);
            this.pnlAddMaterial.Controls.Add(this.lblMaterialName);
            this.pnlAddMaterial.Controls.Add(this.lblMaterialCode);
            this.pnlAddMaterial.Controls.Add(this.btnAddRecord);
            this.pnlAddMaterial.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddMaterial.Location = new System.Drawing.Point(3, 55);
            this.pnlAddMaterial.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAddMaterial.Name = "pnlAddMaterial";
            this.pnlAddMaterial.Size = new System.Drawing.Size(962, 66);
            this.pnlAddMaterial.TabIndex = 158;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Arial", 9F);
            this.txtDescription.Location = new System.Drawing.Point(488, 18);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.MaxLength = 30;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(219, 21);
            this.txtDescription.TabIndex = 2;
            // 
            // txtKey
            // 
            this.txtKey.Font = new System.Drawing.Font("Arial", 9F);
            this.txtKey.Location = new System.Drawing.Point(146, 18);
            this.txtKey.Margin = new System.Windows.Forms.Padding(2);
            this.txtKey.MaxLength = 10;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(219, 21);
            this.txtKey.TabIndex = 1;
            // 
            // lblMaterialName
            // 
            this.lblMaterialName.AutoSize = true;
            this.lblMaterialName.Font = new System.Drawing.Font("Arial", 9F);
            this.lblMaterialName.Location = new System.Drawing.Point(386, 21);
            this.lblMaterialName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaterialName.Name = "lblMaterialName";
            this.lblMaterialName.Size = new System.Drawing.Size(98, 15);
            this.lblMaterialName.TabIndex = 31;
            this.lblMaterialName.Text = "PNE Description";
            // 
            // lblMaterialCode
            // 
            this.lblMaterialCode.AutoSize = true;
            this.lblMaterialCode.Font = new System.Drawing.Font("Arial", 9F);
            this.lblMaterialCode.Location = new System.Drawing.Point(87, 21);
            this.lblMaterialCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaterialCode.Name = "lblMaterialCode";
            this.lblMaterialCode.Size = new System.Drawing.Size(55, 15);
            this.lblMaterialCode.TabIndex = 30;
            this.lblMaterialCode.Text = "PNE Key";
            // 
            // btnAddRecord
            // 
            this.btnAddRecord.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAddRecord.Location = new System.Drawing.Point(736, 10);
            this.btnAddRecord.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddRecord.Name = "btnAddRecord";
            this.btnAddRecord.Size = new System.Drawing.Size(57, 37);
            this.btnAddRecord.TabIndex = 5;
            this.btnAddRecord.UseVisualStyleBackColor = true;
            this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
            // 
            // PNECredit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(969, 575);
            this.Controls.Add(this.pnlAddMaterial);
            this.Controls.Add(this.grdPNECredit);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PNECredit";
            this.Text = "PNE Credits";
            this.Load += new System.EventHandler(this.PNECredit_Load);
            this.SizeChanged += new System.EventHandler(this.PNECredit_SizeChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPNECredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPNECreditView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeletePNECredit)).EndInit();
            this.pnlAddMaterial.ResumeLayout(false);
            this.pnlAddMaterial.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private DevExpress.XtraGrid.GridControl grdPNECredit;
        private DevExpress.XtraGrid.Views.Grid.GridView grdPNECreditView;
        private DevExpress.XtraGrid.Columns.GridColumn colPNECreditID;
        private DevExpress.XtraGrid.Columns.GridColumn colCredit;
        private DevExpress.XtraGrid.Columns.GridColumn colPNEKey;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeletePNECredit;
        private DevExpress.XtraGrid.Columns.GridColumn colPNEDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCUserName;
        private System.Windows.Forms.Button btnPreview;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private System.Windows.Forms.Panel pnlAddMaterial;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Label lblMaterialName;
        private System.Windows.Forms.Label lblMaterialCode;
        private System.Windows.Forms.Button btnAddRecord;
    }
}