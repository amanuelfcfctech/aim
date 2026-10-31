namespace CFCAIM
{
    partial class PrinterUserDefault
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrinterUserDefault));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnResize = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.lueUserName = new DevExpress.XtraEditors.LookUpEdit();
            this.lblUserName = new System.Windows.Forms.Label();
            this.luePrinterType = new DevExpress.XtraEditors.LookUpEdit();
            this.luePrinter = new DevExpress.XtraEditors.LookUpEdit();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.lblPrinterType = new System.Windows.Forms.Label();
            this.btnPreviewResults = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grdPrinter = new DevExpress.XtraGrid.GridControl();
            this.grdPrinterView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrinterName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrinter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repbtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repluePrinter = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            this.pnlAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePrinterType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePrinter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPrinter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPrinterView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repluePrinter)).BeginInit();
            this.SuspendLayout();
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
            this.panel4.Location = new System.Drawing.Point(1, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(984, 51);
            this.panel4.TabIndex = 152;
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(946, -1);
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
            this.label7.Size = new System.Drawing.Size(134, 23);
            this.label7.TabIndex = 149;
            this.label7.Text = "User Printers";
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(3, 3);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(117, 39);
            this.pnlTopBtns.TabIndex = 28;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(59, 2);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 37);
            this.btnRefresh.TabIndex = 25;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.PrinterUserDefault_Load);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(2, 2);
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
            this.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAdd.Controls.Add(this.lueUserName);
            this.pnlAdd.Controls.Add(this.lblUserName);
            this.pnlAdd.Controls.Add(this.luePrinterType);
            this.pnlAdd.Controls.Add(this.luePrinter);
            this.pnlAdd.Controls.Add(this.lblPrinter);
            this.pnlAdd.Controls.Add(this.lblPrinterType);
            this.pnlAdd.Controls.Add(this.btnPreviewResults);
            this.pnlAdd.Controls.Add(this.btnAdd);
            this.pnlAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAdd.Location = new System.Drawing.Point(11, 66);
            this.pnlAdd.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(962, 46);
            this.pnlAdd.TabIndex = 154;
            // 
            // lueUserName
            // 
            this.lueUserName.Location = new System.Drawing.Point(100, 12);
            this.lueUserName.Margin = new System.Windows.Forms.Padding(2);
            this.lueUserName.Name = "lueUserName";
            this.lueUserName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueUserName.Properties.Appearance.Options.UseFont = true;
            this.lueUserName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueUserName.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.lueUserName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueUserName.Size = new System.Drawing.Size(150, 22);
            this.lueUserName.TabIndex = 68;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Arial", 9F);
            this.lblUserName.Location = new System.Drawing.Point(26, 15);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(71, 15);
            this.lblUserName.TabIndex = 67;
            this.lblUserName.Text = "User Name";
            // 
            // luePrinterType
            // 
            this.luePrinterType.Location = new System.Drawing.Point(354, 12);
            this.luePrinterType.Margin = new System.Windows.Forms.Padding(2);
            this.luePrinterType.Name = "luePrinterType";
            this.luePrinterType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luePrinterType.Properties.Appearance.Options.UseFont = true;
            this.luePrinterType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePrinterType.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.luePrinterType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.luePrinterType.Size = new System.Drawing.Size(123, 22);
            this.luePrinterType.TabIndex = 66;
            // 
            // luePrinter
            // 
            this.luePrinter.Location = new System.Drawing.Point(565, 12);
            this.luePrinter.Margin = new System.Windows.Forms.Padding(2);
            this.luePrinter.Name = "luePrinter";
            this.luePrinter.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.luePrinter.Properties.Appearance.Options.UseFont = true;
            this.luePrinter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePrinter.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.luePrinter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.luePrinter.Size = new System.Drawing.Size(260, 22);
            this.luePrinter.TabIndex = 64;
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinter.Location = new System.Drawing.Point(516, 15);
            this.lblPrinter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(46, 15);
            this.lblPrinter.TabIndex = 65;
            this.lblPrinter.Text = "Printer ";
            // 
            // lblPrinterType
            // 
            this.lblPrinterType.AutoSize = true;
            this.lblPrinterType.Font = new System.Drawing.Font("Arial", 9F);
            this.lblPrinterType.Location = new System.Drawing.Point(279, 15);
            this.lblPrinterType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPrinterType.Name = "lblPrinterType";
            this.lblPrinterType.Size = new System.Drawing.Size(71, 15);
            this.lblPrinterType.TabIndex = 62;
            this.lblPrinterType.Text = "Printer Type";
            // 
            // btnPreviewResults
            // 
            this.btnPreviewResults.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewResults.Location = new System.Drawing.Point(896, 3);
            this.btnPreviewResults.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewResults.Name = "btnPreviewResults";
            this.btnPreviewResults.Size = new System.Drawing.Size(57, 37);
            this.btnPreviewResults.TabIndex = 58;
            this.btnPreviewResults.UseVisualStyleBackColor = true;
            this.btnPreviewResults.Click += new System.EventHandler(this.btnPreviewResults_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::CFCAIM.Properties.Resources.saveto_32x32;
            this.btnAdd.Location = new System.Drawing.Point(834, 3);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 37);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grdPrinter
            // 
            this.grdPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPrinter.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdPrinter.Location = new System.Drawing.Point(11, 114);
            this.grdPrinter.MainView = this.grdPrinterView;
            this.grdPrinter.Name = "grdPrinter";
            this.grdPrinter.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repluePrinter,
            this.repbtnDelete});
            this.grdPrinter.Size = new System.Drawing.Size(962, 357);
            this.grdPrinter.TabIndex = 153;
            this.grdPrinter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdPrinterView});
            // 
            // grdPrinterView
            // 
            this.grdPrinterView.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.grdPrinterView.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.grdPrinterView.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.grdPrinterView.Appearance.DetailTip.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.DetailTip.Options.UseFont = true;
            this.grdPrinterView.Appearance.Empty.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.Empty.Options.UseFont = true;
            this.grdPrinterView.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.EvenRow.Options.UseFont = true;
            this.grdPrinterView.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.FilterCloseButton.Options.UseFont = true;
            this.grdPrinterView.Appearance.FilterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.FilterPanel.Options.UseFont = true;
            this.grdPrinterView.Appearance.FixedLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.FixedLine.Options.UseFont = true;
            this.grdPrinterView.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.FocusedCell.Options.UseFont = true;
            this.grdPrinterView.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.FocusedRow.Options.UseFont = true;
            this.grdPrinterView.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.FooterPanel.Options.UseFont = true;
            this.grdPrinterView.Appearance.GroupButton.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.GroupButton.Options.UseFont = true;
            this.grdPrinterView.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.GroupFooter.Options.UseFont = true;
            this.grdPrinterView.Appearance.GroupPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.GroupPanel.Options.UseFont = true;
            this.grdPrinterView.Appearance.GroupRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.GroupRow.Options.UseFont = true;
            this.grdPrinterView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.HeaderPanel.Options.UseFont = true;
            this.grdPrinterView.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.HideSelectionRow.Options.UseFont = true;
            this.grdPrinterView.Appearance.HorzLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.HorzLine.Options.UseFont = true;
            this.grdPrinterView.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.OddRow.Options.UseFont = true;
            this.grdPrinterView.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.Preview.Options.UseFont = true;
            this.grdPrinterView.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.Row.Options.UseFont = true;
            this.grdPrinterView.Appearance.RowSeparator.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.RowSeparator.Options.UseFont = true;
            this.grdPrinterView.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.SelectedRow.Options.UseFont = true;
            this.grdPrinterView.Appearance.TopNewRow.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.TopNewRow.Options.UseFont = true;
            this.grdPrinterView.Appearance.VertLine.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.VertLine.Options.UseFont = true;
            this.grdPrinterView.Appearance.ViewCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.grdPrinterView.Appearance.ViewCaption.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdPrinterView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdPrinterView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdPrinterView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdPrinterView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdPrinterView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPrinterView.AppearancePrint.Row.Options.UseFont = true;
            this.grdPrinterView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserName,
            this.colPrinterName,
            this.colPrinter,
            this.colUpdateStamp,
            this.colDelete});
            this.grdPrinterView.GridControl = this.grdPrinter;
            this.grdPrinterView.Name = "grdPrinterView";
            this.grdPrinterView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdPrinterView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdPrinterView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdPrinterView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdPrinterView_ValidateRow);
            // 
            // colUserName
            // 
            this.colUserName.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUserName.AppearanceCell.Options.UseBackColor = true;
            this.colUserName.AppearanceCell.Options.UseFont = true;
            this.colUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUserName.AppearanceHeader.Options.UseFont = true;
            this.colUserName.Caption = "User Name";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.OptionsColumn.ReadOnly = true;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 0;
            this.colUserName.Width = 104;
            // 
            // colPrinterName
            // 
            this.colPrinterName.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colPrinterName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPrinterName.AppearanceCell.Options.UseBackColor = true;
            this.colPrinterName.AppearanceCell.Options.UseFont = true;
            this.colPrinterName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPrinterName.AppearanceHeader.Options.UseFont = true;
            this.colPrinterName.Caption = "Printer Type";
            this.colPrinterName.FieldName = "PrinterTypeName";
            this.colPrinterName.Name = "colPrinterName";
            this.colPrinterName.OptionsColumn.AllowEdit = false;
            this.colPrinterName.OptionsColumn.ReadOnly = true;
            this.colPrinterName.Visible = true;
            this.colPrinterName.VisibleIndex = 1;
            this.colPrinterName.Width = 153;
            // 
            // colPrinter
            // 
            this.colPrinter.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colPrinter.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPrinter.AppearanceCell.Options.UseBackColor = true;
            this.colPrinter.AppearanceCell.Options.UseFont = true;
            this.colPrinter.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colPrinter.AppearanceHeader.Options.UseFont = true;
            this.colPrinter.Caption = "Printer";
            this.colPrinter.FieldName = "PrinterName";
            this.colPrinter.Name = "colPrinter";
            this.colPrinter.OptionsColumn.AllowEdit = false;
            this.colPrinter.Visible = true;
            this.colPrinter.VisibleIndex = 2;
            this.colPrinter.Width = 245;
            // 
            // colUpdateStamp
            // 
            this.colUpdateStamp.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colUpdateStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdateStamp.AppearanceCell.Options.UseBackColor = true;
            this.colUpdateStamp.AppearanceCell.Options.UseFont = true;
            this.colUpdateStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colUpdateStamp.AppearanceHeader.Options.UseFont = true;
            this.colUpdateStamp.Caption = "Update Stamp";
            this.colUpdateStamp.DisplayFormat.FormatString = "g";
            this.colUpdateStamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colUpdateStamp.FieldName = "UpdateStamp";
            this.colUpdateStamp.Name = "colUpdateStamp";
            this.colUpdateStamp.OptionsColumn.AllowEdit = false;
            this.colUpdateStamp.OptionsColumn.ReadOnly = true;
            this.colUpdateStamp.Visible = true;
            this.colUpdateStamp.VisibleIndex = 3;
            this.colUpdateStamp.Width = 112;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDelete.AppearanceCell.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDelete.AppearanceHeader.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.repbtnDelete;
            this.colDelete.Name = "colDelete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 4;
            // 
            // repbtnDelete
            // 
            this.repbtnDelete.AutoHeight = false;
            editorButtonImageOptions1.Image = global::CFCAIM.Properties.Resources.delete;
            this.repbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Delete record", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repbtnDelete.Name = "repbtnDelete";
            this.repbtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repbtnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repbtnDelete_ButtonClick);
            // 
            // repluePrinter
            // 
            this.repluePrinter.AutoHeight = false;
            this.repluePrinter.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repluePrinter.Name = "repluePrinter";
            // 
            // PrinterUserDefault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(984, 474);
            this.Controls.Add(this.pnlAdd);
            this.Controls.Add(this.grdPrinter);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrinterUserDefault";
            this.Text = "User Printers";
            this.Load += new System.EventHandler(this.PrinterUserDefault_Load);
            this.StyleChanged += new System.EventHandler(this.PrinterUserDefault_StyleChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            this.pnlAdd.ResumeLayout(false);
            this.pnlAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePrinterType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePrinter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPrinter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPrinterView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repbtnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repluePrinter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlAdd;
        private DevExpress.XtraEditors.LookUpEdit lueUserName;
        private System.Windows.Forms.Label lblUserName;
        private DevExpress.XtraEditors.LookUpEdit luePrinterType;
        private DevExpress.XtraEditors.LookUpEdit luePrinter;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.Label lblPrinterType;
        private System.Windows.Forms.Button btnPreviewResults;
        private System.Windows.Forms.Button btnAdd;
        private DevExpress.XtraGrid.GridControl grdPrinter;
        private DevExpress.XtraGrid.Views.Grid.GridView grdPrinterView;
        private DevExpress.XtraGrid.Columns.GridColumn colPrinterName;
        private DevExpress.XtraGrid.Columns.GridColumn colPrinter;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repluePrinter;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repbtnDelete;
    }
}