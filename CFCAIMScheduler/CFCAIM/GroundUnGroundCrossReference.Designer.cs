namespace CFCAIM
{
    partial class GroundUnGroundCrossReference
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroundUnGroundCrossReference));
            this.grdGround = new DevExpress.XtraGrid.GridControl();
            this.grdGroundView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPNEMaterialSampleTypeRefGroundUnGroundID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPNEMaterialSampleTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroundUnGroundID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPNEMaterialSampleTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroundUnGroundString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeleteXRef = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDeleteGroundUnGroundXRef = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblSampleType = new System.Windows.Forms.Label();
            this.lueMaterialSampleType = new DevExpress.XtraEditors.LookUpEdit();
            this.lueGround = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGroundView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteGroundUnGroundXRef)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueMaterialSampleType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGround.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grdGround
            // 
            this.grdGround.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdGround.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdGround.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            gridLevelNode1.RelationName = "Level1";
            this.grdGround.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grdGround.Location = new System.Drawing.Point(1, 151);
            this.grdGround.MainView = this.grdGroundView;
            this.grdGround.Margin = new System.Windows.Forms.Padding(2);
            this.grdGround.MinimumSize = new System.Drawing.Size(266, 246);
            this.grdGround.Name = "grdGround";
            this.grdGround.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repbtnDeleteGroundUnGroundXRef});
            this.grdGround.Size = new System.Drawing.Size(886, 342);
            this.grdGround.TabIndex = 2;
            this.grdGround.TabStop = false;
            this.grdGround.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdGroundView});
            // 
            // grdGroundView
            // 
            this.grdGroundView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.grdGroundView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.grdGroundView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.grdGroundView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.DetailTip.Options.UseFont = true;
            this.grdGroundView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.Empty.Options.UseFont = true;
            this.grdGroundView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.EvenRow.Options.UseFont = true;
            this.grdGroundView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.FilterCloseButton.Options.UseFont = true;
            this.grdGroundView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.FilterPanel.Options.UseFont = true;
            this.grdGroundView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.FixedLine.Options.UseFont = true;
            this.grdGroundView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.FocusedCell.Options.UseFont = true;
            this.grdGroundView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.FocusedRow.Options.UseFont = true;
            this.grdGroundView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.FooterPanel.Options.UseFont = true;
            this.grdGroundView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.GroupButton.Options.UseFont = true;
            this.grdGroundView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.GroupFooter.Options.UseFont = true;
            this.grdGroundView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.GroupPanel.Options.UseFont = true;
            this.grdGroundView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.GroupRow.Options.UseFont = true;
            this.grdGroundView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdGroundView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.HideSelectionRow.Options.UseFont = true;
            this.grdGroundView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.HorzLine.Options.UseFont = true;
            this.grdGroundView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.OddRow.Options.UseFont = true;
            this.grdGroundView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.Preview.Options.UseFont = true;
            this.grdGroundView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.Row.Options.UseFont = true;
            this.grdGroundView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.RowSeparator.Options.UseFont = true;
            this.grdGroundView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.SelectedRow.Options.UseFont = true;
            this.grdGroundView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.TopNewRow.Options.UseFont = true;
            this.grdGroundView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.VertLine.Options.UseFont = true;
            this.grdGroundView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.Appearance.ViewCaption.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdGroundView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdGroundView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdGroundView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdGroundView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdGroundView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdGroundView.AppearancePrint.Row.Options.UseFont = true;
            this.grdGroundView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPNEMaterialSampleTypeRefGroundUnGroundID,
            this.colPNEMaterialSampleTypeID,
            this.colGroundUnGroundID,
            this.colPNEMaterialSampleTypeCode,
            this.colGroundUnGroundString,
            this.gridColumn15,
            this.gridColumn16,
            this.colDeleteXRef});
            this.grdGroundView.GridControl = this.grdGround;
            this.grdGroundView.Name = "grdGroundView";
            this.grdGroundView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdGroundView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdGroundView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdGroundView.OptionsView.EnableAppearanceEvenRow = true;
            // 
            // colPNEMaterialSampleTypeRefGroundUnGroundID
            // 
            this.colPNEMaterialSampleTypeRefGroundUnGroundID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPNEMaterialSampleTypeRefGroundUnGroundID.AppearanceCell.Options.UseFont = true;
            this.colPNEMaterialSampleTypeRefGroundUnGroundID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPNEMaterialSampleTypeRefGroundUnGroundID.AppearanceHeader.Options.UseFont = true;
            this.colPNEMaterialSampleTypeRefGroundUnGroundID.Caption = "PNEMaterialSampleTypeRefGroundUnGroundID";
            this.colPNEMaterialSampleTypeRefGroundUnGroundID.FieldName = "PNEMaterialSampleTypeRefGroundUnGroundID";
            this.colPNEMaterialSampleTypeRefGroundUnGroundID.Name = "colPNEMaterialSampleTypeRefGroundUnGroundID";
            this.colPNEMaterialSampleTypeRefGroundUnGroundID.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // colPNEMaterialSampleTypeID
            // 
            this.colPNEMaterialSampleTypeID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPNEMaterialSampleTypeID.AppearanceCell.Options.UseFont = true;
            this.colPNEMaterialSampleTypeID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPNEMaterialSampleTypeID.AppearanceHeader.Options.UseFont = true;
            this.colPNEMaterialSampleTypeID.Caption = "PNEMaterialSampleTypeID";
            this.colPNEMaterialSampleTypeID.FieldName = "PNEMaterialSampleTypeID";
            this.colPNEMaterialSampleTypeID.Name = "colPNEMaterialSampleTypeID";
            this.colPNEMaterialSampleTypeID.OptionsColumn.AllowEdit = false;
            this.colPNEMaterialSampleTypeID.Width = 199;
            // 
            // colGroundUnGroundID
            // 
            this.colGroundUnGroundID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colGroundUnGroundID.AppearanceCell.Options.UseFont = true;
            this.colGroundUnGroundID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colGroundUnGroundID.AppearanceHeader.Options.UseFont = true;
            this.colGroundUnGroundID.Caption = "GroundUnGroundID";
            this.colGroundUnGroundID.FieldName = "GroundUnGroundID";
            this.colGroundUnGroundID.Name = "colGroundUnGroundID";
            this.colGroundUnGroundID.OptionsColumn.AllowEdit = false;
            this.colGroundUnGroundID.Width = 183;
            // 
            // colPNEMaterialSampleTypeCode
            // 
            this.colPNEMaterialSampleTypeCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colPNEMaterialSampleTypeCode.AppearanceCell.Options.UseFont = true;
            this.colPNEMaterialSampleTypeCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colPNEMaterialSampleTypeCode.AppearanceHeader.Options.UseFont = true;
            this.colPNEMaterialSampleTypeCode.Caption = "PNE Material Sample Type Code";
            this.colPNEMaterialSampleTypeCode.FieldName = "PNEMaterialSampleTypeCode";
            this.colPNEMaterialSampleTypeCode.Name = "colPNEMaterialSampleTypeCode";
            this.colPNEMaterialSampleTypeCode.OptionsColumn.AllowEdit = false;
            this.colPNEMaterialSampleTypeCode.Visible = true;
            this.colPNEMaterialSampleTypeCode.VisibleIndex = 0;
            this.colPNEMaterialSampleTypeCode.Width = 192;
            // 
            // colGroundUnGroundString
            // 
            this.colGroundUnGroundString.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colGroundUnGroundString.AppearanceCell.Options.UseFont = true;
            this.colGroundUnGroundString.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colGroundUnGroundString.AppearanceHeader.Options.UseFont = true;
            this.colGroundUnGroundString.Caption = "Ground/Whole Description";
            this.colGroundUnGroundString.FieldName = "GroundUnGroundString";
            this.colGroundUnGroundString.Name = "colGroundUnGroundString";
            this.colGroundUnGroundString.OptionsColumn.AllowEdit = false;
            this.colGroundUnGroundString.Visible = true;
            this.colGroundUnGroundString.VisibleIndex = 1;
            this.colGroundUnGroundString.Width = 247;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn15.AppearanceCell.Options.UseFont = true;
            this.gridColumn15.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn15.AppearanceHeader.Options.UseFont = true;
            this.gridColumn15.Caption = "User Name";
            this.gridColumn15.FieldName = "UserName";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 2;
            this.gridColumn15.Width = 191;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn16.AppearanceCell.Options.UseFont = true;
            this.gridColumn16.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn16.AppearanceHeader.Options.UseFont = true;
            this.gridColumn16.Caption = "Update Stamp";
            this.gridColumn16.DisplayFormat.FormatString = "g";
            this.gridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn16.FieldName = "UpdateStamp";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 3;
            this.gridColumn16.Width = 163;
            // 
            // colDeleteXRef
            // 
            this.colDeleteXRef.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDeleteXRef.AppearanceCell.Options.UseFont = true;
            this.colDeleteXRef.AppearanceCell.Options.UseTextOptions = true;
            this.colDeleteXRef.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeleteXRef.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDeleteXRef.AppearanceHeader.Options.UseFont = true;
            this.colDeleteXRef.AppearanceHeader.Options.UseTextOptions = true;
            this.colDeleteXRef.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeleteXRef.Caption = "Delete";
            this.colDeleteXRef.ColumnEdit = this.repbtnDeleteGroundUnGroundXRef;
            this.colDeleteXRef.FieldName = "colDelete";
            this.colDeleteXRef.Name = "colDeleteXRef";
            this.colDeleteXRef.OptionsColumn.ShowInCustomizationForm = false;
            this.colDeleteXRef.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.colDeleteXRef.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colDeleteXRef.Visible = true;
            this.colDeleteXRef.VisibleIndex = 4;
            this.colDeleteXRef.Width = 68;
            // 
            // repbtnDeleteGroundUnGroundXRef
            // 
            this.repbtnDeleteGroundUnGroundXRef.AutoHeight = false;
            editorButtonImageOptions1.Image = global::CFCAIM.Properties.Resources.delete;
            this.repbtnDeleteGroundUnGroundXRef.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDeleteGroundUnGroundXRef.Name = "repbtnDeleteGroundUnGroundXRef";
            this.repbtnDeleteGroundUnGroundXRef.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDeleteGroundUnGroundXRef.Click += new System.EventHandler(this.repbtnDeleteGroundUnGroundXRef_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(890, 51);
            this.panel4.TabIndex = 155;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(202, 52);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(119, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(284, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(306, 23);
            this.label3.TabIndex = 149;
            this.label3.Text = "Ground/Whole Cross Reference";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(849, -1);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(36, 35);
            this.btnResize.TabIndex = 0;
            this.btnResize.TabStop = false;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlTopBtns);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(118, 39);
            this.panel2.TabIndex = 52;
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(118, 39);
            this.pnlTopBtns.TabIndex = 72;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(59, 1);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 37);
            this.btnRefresh.TabIndex = 32;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(2, 1);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 31;
            this.btnExit.TabStop = false;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreview.Location = new System.Drawing.Point(816, 99);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(57, 37);
            this.btnPreview.TabIndex = 156;
            this.btnPreview.TabStop = false;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAdd.Location = new System.Drawing.Point(758, 99);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 37);
            this.btnAdd.TabIndex = 159;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblSampleType
            // 
            this.lblSampleType.AutoSize = true;
            this.lblSampleType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleType.Location = new System.Drawing.Point(24, 84);
            this.lblSampleType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSampleType.Name = "lblSampleType";
            this.lblSampleType.Size = new System.Drawing.Size(152, 15);
            this.lblSampleType.TabIndex = 160;
            this.lblSampleType.Text = "PNE Material Sample Type";
            // 
            // lueMaterialSampleType
            // 
            this.lueMaterialSampleType.Location = new System.Drawing.Point(27, 102);
            this.lueMaterialSampleType.Name = "lueMaterialSampleType";
            this.lueMaterialSampleType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueMaterialSampleType.Properties.Appearance.Options.UseFont = true;
            this.lueMaterialSampleType.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueMaterialSampleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueMaterialSampleType.Properties.DropDownRows = 15;
            this.lueMaterialSampleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueMaterialSampleType.Size = new System.Drawing.Size(162, 22);
            this.lueMaterialSampleType.TabIndex = 161;
            // 
            // lueGround
            // 
            this.lueGround.Location = new System.Drawing.Point(260, 102);
            this.lueGround.Name = "lueGround";
            this.lueGround.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueGround.Properties.Appearance.Options.UseFont = true;
            this.lueGround.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lueGround.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueGround.Properties.DropDownRows = 15;
            this.lueGround.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueGround.Size = new System.Drawing.Size(162, 22);
            this.lueGround.TabIndex = 163;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(257, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 15);
            this.label1.TabIndex = 164;
            this.label1.Text = "Ground/Whole Description";
            // 
            // GroundUnGroundCrossReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(888, 495);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSampleType);
            this.Controls.Add(this.lueMaterialSampleType);
            this.Controls.Add(this.lueGround);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.grdGround);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GroundUnGroundCrossReference";
            this.Text = "Ground/Whole Cross Reference";
            this.Load += new System.EventHandler(this.GroundUnGroundCrossReference_Load);
            this.SizeChanged += new System.EventHandler(this.GroundUnGroundCrossReference_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.grdGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGroundView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDeleteGroundUnGroundXRef)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueMaterialSampleType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGround.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdGround;
        private DevExpress.XtraGrid.Views.Grid.GridView grdGroundView;
        private DevExpress.XtraGrid.Columns.GridColumn colPNEMaterialSampleTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colGroundUnGroundID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn colDeleteXRef;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDeleteGroundUnGroundXRef;
        private DevExpress.XtraGrid.Columns.GridColumn colPNEMaterialSampleTypeRefGroundUnGroundID;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colPNEMaterialSampleTypeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colGroundUnGroundString;
        private System.Windows.Forms.Label lblSampleType;
        private DevExpress.XtraEditors.LookUpEdit lueMaterialSampleType;
        private DevExpress.XtraEditors.LookUpEdit lueGround;
        private System.Windows.Forms.Label label1;
    }
}