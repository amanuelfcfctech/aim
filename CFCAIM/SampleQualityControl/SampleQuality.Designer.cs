namespace CFCAIM.SampleQualityControl
{
    partial class SampleQuality
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleQuality));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPreviewGridQualityControl = new System.Windows.Forms.Button();
            this.grdQualityControl = new DevExpress.XtraGrid.GridControl();
            this.grdQualityControlView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSampleQualityEquationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleQualityEquationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEdit1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colEquation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquationText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repDelete1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdQualityControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdQualityControlView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repDelete1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnAdd);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Controls.Add(this.btnPreviewGridQualityControl);
            this.panel4.Location = new System.Drawing.Point(2, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(963, 51);
            this.panel4.TabIndex = 157;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(122, 3);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 39);
            this.btnAdd.TabIndex = 150;
            this.btnAdd.TabStop = false;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(217, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(328, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Sample Quality Control Equations";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(921, 2);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(35, 34);
            this.btnResize.TabIndex = 112;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(3, 3);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(114, 37);
            this.pnlTopBtns.TabIndex = 79;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(58, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 39);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 39);
            this.btnExit.TabIndex = 26;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPreviewGridQualityControl
            // 
            this.btnPreviewGridQualityControl.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewGridQualityControl.Location = new System.Drawing.Point(650, 3);
            this.btnPreviewGridQualityControl.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewGridQualityControl.Name = "btnPreviewGridQualityControl";
            this.btnPreviewGridQualityControl.Size = new System.Drawing.Size(57, 39);
            this.btnPreviewGridQualityControl.TabIndex = 58;
            this.btnPreviewGridQualityControl.TabStop = false;
            this.btnPreviewGridQualityControl.UseVisualStyleBackColor = true;
            this.btnPreviewGridQualityControl.Click += new System.EventHandler(this.btnPreviewGridQualityControl_Click);
            // 
            // grdQualityControl
            // 
            this.grdQualityControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdQualityControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdQualityControl.Location = new System.Drawing.Point(7, 57);
            this.grdQualityControl.MainView = this.grdQualityControlView;
            this.grdQualityControl.Margin = new System.Windows.Forms.Padding(2);
            this.grdQualityControl.Name = "grdQualityControl";
            this.grdQualityControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repEdit1,
            this.repDelete1});
            this.grdQualityControl.Size = new System.Drawing.Size(956, 532);
            this.grdQualityControl.TabIndex = 158;
            this.grdQualityControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdQualityControlView});
            this.grdQualityControl.Click += new System.EventHandler(this.grdQualityControl_Click);
            // 
            // grdQualityControlView
            // 
            this.grdQualityControlView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdQualityControlView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdQualityControlView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdQualityControlView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdQualityControlView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSampleQualityEquationID,
            this.colSampleQualityEquationName,
            this.colEdit1,
            this.colEquation,
            this.colEquationText,
            this.colUpdateStamp,
            this.colUserName,
            this.colCFCUserID,
            this.colDelete});
            this.grdQualityControlView.GridControl = this.grdQualityControl;
            this.grdQualityControlView.Name = "grdQualityControlView";
            this.grdQualityControlView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdQualityControlView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdQualityControlView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdQualityControlView_ValidateRow);
            // 
            // colSampleQualityEquationID
            // 
            this.colSampleQualityEquationID.Caption = "SampleQualityEquationID";
            this.colSampleQualityEquationID.FieldName = "SampleQualityEquationID";
            this.colSampleQualityEquationID.Name = "colSampleQualityEquationID";
            this.colSampleQualityEquationID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colSampleQualityEquationName
            // 
            this.colSampleQualityEquationName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSampleQualityEquationName.AppearanceCell.Options.UseFont = true;
            this.colSampleQualityEquationName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSampleQualityEquationName.AppearanceHeader.Options.UseFont = true;
            this.colSampleQualityEquationName.Caption = "Equation Name";
            this.colSampleQualityEquationName.FieldName = "SampleQualityEquationName";
            this.colSampleQualityEquationName.Name = "colSampleQualityEquationName";
            this.colSampleQualityEquationName.Visible = true;
            this.colSampleQualityEquationName.VisibleIndex = 1;
            this.colSampleQualityEquationName.Width = 691;
            // 
            // colEdit1
            // 
            this.colEdit1.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colEdit1.AppearanceCell.Options.UseFont = true;
            this.colEdit1.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colEdit1.AppearanceHeader.Options.UseFont = true;
            this.colEdit1.AppearanceHeader.Options.UseTextOptions = true;
            this.colEdit1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEdit1.Caption = "Edit";
            this.colEdit1.ColumnEdit = this.repEdit1;
            this.colEdit1.Name = "colEdit1";
            this.colEdit1.ToolTip = "Edit Equation";
            this.colEdit1.Visible = true;
            this.colEdit1.VisibleIndex = 0;
            this.colEdit1.Width = 131;
            // 
            // repEdit1
            // 
            this.repEdit1.AutoHeight = false;
            editorButtonImageOptions1.Image = global::CFCAIM.Properties.Resources.customization_16x16;
            this.repEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Edit Equation", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repEdit1.Name = "repEdit1";
            this.repEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repEdit1.Click += new System.EventHandler(this.repEdit1_Click);
            // 
            // colEquation
            // 
            this.colEquation.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colEquation.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colEquation.AppearanceCell.Options.UseBackColor = true;
            this.colEquation.AppearanceCell.Options.UseFont = true;
            this.colEquation.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colEquation.AppearanceHeader.Options.UseFont = true;
            this.colEquation.Caption = "Equation";
            this.colEquation.FieldName = "Equation";
            this.colEquation.Name = "colEquation";
            this.colEquation.OptionsColumn.AllowEdit = false;
            this.colEquation.Width = 501;
            // 
            // colEquationText
            // 
            this.colEquationText.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colEquationText.AppearanceCell.Options.UseFont = true;
            this.colEquationText.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colEquationText.AppearanceHeader.Options.UseFont = true;
            this.colEquationText.Caption = "Equation";
            this.colEquationText.FieldName = "EquationText";
            this.colEquationText.Name = "colEquationText";
            this.colEquationText.Width = 762;
            // 
            // colUpdateStamp
            // 
            this.colUpdateStamp.Caption = "UpdateSamp";
            this.colUpdateStamp.FieldName = "UpdateStamp";
            this.colUpdateStamp.Name = "colUpdateStamp";
            // 
            // colUserName
            // 
            this.colUserName.Caption = "UserName";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            // 
            // colCFCUserID
            // 
            this.colCFCUserID.Caption = "CFCUserID";
            this.colCFCUserID.FieldName = "CFCUserID";
            this.colCFCUserID.Name = "colCFCUserID";
            this.colCFCUserID.Width = 70;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceCell.Options.UseTextOptions = true;
            this.colDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colDelete.AppearanceHeader.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.repDelete1;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 2;
            this.colDelete.Width = 116;
            // 
            // repDelete1
            // 
            this.repDelete1.AutoHeight = false;
            editorButtonImageOptions2.Image = global::CFCAIM.Properties.Resources.delete;
            this.repDelete1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repDelete1.Name = "repDelete1";
            this.repDelete1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repDelete1.Click += new System.EventHandler(this.repDelete1_Click);
            // 
            // SampleQuality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(966, 593);
            this.Controls.Add(this.grdQualityControl);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SampleQuality";
            this.Text = "Sample Quality Control Parameters";
            this.Load += new System.EventHandler(this.SampleQuality_Load);
            this.SizeChanged += new System.EventHandler(this.SampleQuality_SizeChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdQualityControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdQualityControlView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repDelete1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPreviewGridQualityControl;
        private DevExpress.XtraGrid.GridControl grdQualityControl;
        private DevExpress.XtraGrid.Views.Grid.GridView grdQualityControlView;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleQualityEquationID;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleQualityEquationName;
        private DevExpress.XtraGrid.Columns.GridColumn colEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colEquation;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCUserID;
        private System.Windows.Forms.Button btnAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repDelete1;
        private DevExpress.XtraGrid.Columns.GridColumn colEquationText;
    }
}