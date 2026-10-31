//9/16/209
//namespace CFCAIMCustom.Vitapro
namespace CFCAIM.Custom.Vitapro.Forms

{
    partial class LineMaintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LineMaintenance));
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnPreviewGrid = new System.Windows.Forms.Button();
            this.btnResize = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grdLineMaintenance = new DevExpress.XtraGrid.GridControl();
            this.grdLineMaintenanceView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLineRefCFCPlantID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLineID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLineName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFCPlantName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replueCFCPlantName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colExtraSpace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLineMaintenance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLineMaintenanceView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCPlantName)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnPreviewGrid);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(850, 51);
            this.panel4.TabIndex = 159;
            // 
            // btnPreviewGrid
            // 
            this.btnPreviewGrid.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreviewGrid.Location = new System.Drawing.Point(122, 3);
            this.btnPreviewGrid.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreviewGrid.Name = "btnPreviewGrid";
            this.btnPreviewGrid.Size = new System.Drawing.Size(57, 39);
            this.btnPreviewGrid.TabIndex = 151;
            this.btnPreviewGrid.TabStop = false;
            this.btnPreviewGrid.UseVisualStyleBackColor = true;
            this.btnPreviewGrid.Click += new System.EventHandler(this.btnPreviewGrid_Click);
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(809, -1);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(36, 34);
            this.btnResize.TabIndex = 150;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(347, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(208, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Linea Mantenimiento";
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
            this.btnRefresh.Click += new System.EventHandler(this.Line_Load);
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
            // grdLineMaintenance
            // 
            this.grdLineMaintenance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdLineMaintenance.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdLineMaintenance.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdLineMaintenance.Location = new System.Drawing.Point(0, 93);
            this.grdLineMaintenance.MainView = this.grdLineMaintenanceView;
            this.grdLineMaintenance.Margin = new System.Windows.Forms.Padding(2);
            this.grdLineMaintenance.Name = "grdLineMaintenance";
            this.grdLineMaintenance.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replueCFCPlantName});
            this.grdLineMaintenance.Size = new System.Drawing.Size(847, 375);
            this.grdLineMaintenance.TabIndex = 160;
            this.grdLineMaintenance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdLineMaintenanceView});
            // 
            // grdLineMaintenanceView
            // 
            this.grdLineMaintenanceView.Appearance.Row.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.Appearance.Row.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdLineMaintenanceView.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdLineMaintenanceView.AppearancePrint.EvenRow.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.GroupRow.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.Lines.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.Lines.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdLineMaintenanceView.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdLineMaintenanceView.AppearancePrint.OddRow.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.Preview.Options.UseFont = true;
            this.grdLineMaintenanceView.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLineMaintenanceView.AppearancePrint.Row.Options.UseFont = true;
            this.grdLineMaintenanceView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLineRefCFCPlantID,
            this.gridColumn11,
            this.colLineID,
            this.colLineName,
            this.colCFCPlantName,
            this.gridColumn5,
            this.colExtraSpace,
            this.colIsActive,
            this.gridColumn1});
            this.grdLineMaintenanceView.GridControl = this.grdLineMaintenance;
            this.grdLineMaintenanceView.Name = "grdLineMaintenanceView";
            this.grdLineMaintenanceView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            this.grdLineMaintenanceView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdLineMaintenanceView.OptionsPrint.EnableAppearanceOddRow = true;
            this.grdLineMaintenanceView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.grdLineMaintenanceView_CustomDrawCell);
            this.grdLineMaintenanceView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.grdLineMaintenanceView_ValidateRow);
            // 
            // colLineRefCFCPlantID
            // 
            this.colLineRefCFCPlantID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colLineRefCFCPlantID.AppearanceCell.Options.UseFont = true;
            this.colLineRefCFCPlantID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colLineRefCFCPlantID.AppearanceHeader.Options.UseFont = true;
            this.colLineRefCFCPlantID.Caption = "LineRefCFCPlantID";
            this.colLineRefCFCPlantID.FieldName = "LineRefCFCPlantID";
            this.colLineRefCFCPlantID.Name = "colLineRefCFCPlantID";
            this.colLineRefCFCPlantID.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "CFCPlantID";
            this.gridColumn11.FieldName = "CFCPlantID";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.ReadOnly = true;
            this.gridColumn11.OptionsColumn.TabStop = false;
            // 
            // colLineID
            // 
            this.colLineID.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colLineID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colLineID.AppearanceCell.Options.UseBackColor = true;
            this.colLineID.AppearanceCell.Options.UseFont = true;
            this.colLineID.AppearanceCell.Options.UseTextOptions = true;
            this.colLineID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLineID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colLineID.AppearanceHeader.Options.UseFont = true;
            this.colLineID.AppearanceHeader.Options.UseTextOptions = true;
            this.colLineID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLineID.Caption = "Linea";
            this.colLineID.FieldName = "LineID";
            this.colLineID.Name = "colLineID";
            this.colLineID.OptionsColumn.ReadOnly = true;
            this.colLineID.Visible = true;
            this.colLineID.VisibleIndex = 0;
            this.colLineID.Width = 70;
            // 
            // colLineName
            // 
            this.colLineName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colLineName.AppearanceCell.Options.UseFont = true;
            this.colLineName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colLineName.AppearanceHeader.Options.UseFont = true;
            this.colLineName.AppearanceHeader.Options.UseTextOptions = true;
            this.colLineName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colLineName.Caption = "Nombre de linea";
            this.colLineName.FieldName = "LineName";
            this.colLineName.Name = "colLineName";
            this.colLineName.Visible = true;
            this.colLineName.VisibleIndex = 1;
            this.colLineName.Width = 161;
            // 
            // colCFCPlantName
            // 
            this.colCFCPlantName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCPlantName.AppearanceCell.Options.UseFont = true;
            this.colCFCPlantName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colCFCPlantName.AppearanceHeader.Options.UseFont = true;
            this.colCFCPlantName.Caption = "Planta de formulación";
            this.colCFCPlantName.ColumnEdit = this.replueCFCPlantName;
            this.colCFCPlantName.FieldName = "CFCPlantID";
            this.colCFCPlantName.Name = "colCFCPlantName";
            this.colCFCPlantName.Visible = true;
            this.colCFCPlantName.VisibleIndex = 2;
            this.colCFCPlantName.Width = 221;
            // 
            // replueCFCPlantName
            // 
            this.replueCFCPlantName.AutoHeight = false;
            this.replueCFCPlantName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.replueCFCPlantName.Name = "replueCFCPlantName";
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn5.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.Caption = "Actualizar sello";
            this.gridColumn5.DisplayFormat.FormatString = "g";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "UpdateStamp";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.OptionsColumn.TabStop = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 113;
            // 
            // colExtraSpace
            // 
            this.colExtraSpace.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colExtraSpace.AppearanceCell.Options.UseBackColor = true;
            this.colExtraSpace.Name = "colExtraSpace";
            this.colExtraSpace.OptionsColumn.AllowEdit = false;
            this.colExtraSpace.OptionsColumn.ReadOnly = true;
            this.colExtraSpace.OptionsColumn.TabStop = false;
            this.colExtraSpace.Visible = true;
            this.colExtraSpace.VisibleIndex = 4;
            this.colExtraSpace.Width = 181;
            // 
            // colIsActive
            // 
            this.colIsActive.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colIsActive.AppearanceCell.Options.UseFont = true;
            this.colIsActive.AppearanceCell.Options.UseTextOptions = true;
            this.colIsActive.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsActive.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F);
            this.colIsActive.AppearanceHeader.Options.UseFont = true;
            this.colIsActive.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsActive.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsActive.Caption = "Activo";
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 5;
            this.colIsActive.Width = 83;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "CFCPlantID";
            this.gridColumn1.FieldName = "CFCPlantID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // LineMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(848, 470);
            this.Controls.Add(this.grdLineMaintenance);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LineMaintenance";
            this.Text = "Linea Mantenimiento";
            this.Load += new System.EventHandler(this.Line_Load);
            this.Resize += new System.EventHandler(this.Line_Resize);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLineMaintenance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLineMaintenanceView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replueCFCPlantName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnPreviewGrid;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private DevExpress.XtraGrid.GridControl grdLineMaintenance;
        private DevExpress.XtraGrid.Views.Grid.GridView grdLineMaintenanceView;
        private DevExpress.XtraGrid.Columns.GridColumn colLineRefCFCPlantID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn colLineID;
        private DevExpress.XtraGrid.Columns.GridColumn colCFCPlantName;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit replueCFCPlantName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn colExtraSpace;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraGrid.Columns.GridColumn colLineName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}