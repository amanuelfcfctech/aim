namespace CFCAIM.Chart
{
    partial class ChartsSampleList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartsSampleList));
            this.grdSampleList = new DevExpress.XtraGrid.GridControl();
            this.grdSampleListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSampleID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.replnkSampleID = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colSampleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSampleDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMMaterialCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMMaterialName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMNutrientCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIMNutrientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResultValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnResize = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSampleList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSampleListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replnkSampleID)).BeginInit();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdSampleList
            // 
            this.grdSampleList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSampleList.Location = new System.Drawing.Point(0, 49);
            this.grdSampleList.MainView = this.grdSampleListView;
            this.grdSampleList.Name = "grdSampleList";
            this.grdSampleList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.replnkSampleID});
            this.grdSampleList.Size = new System.Drawing.Size(909, 482);
            this.grdSampleList.TabIndex = 0;
            this.grdSampleList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdSampleListView});
            this.grdSampleList.Load += new System.EventHandler(this.grdSampleList_Load);
            // 
            // grdSampleListView
            // 
            this.grdSampleListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSampleID,
            this.colSampleCode,
            this.colSampleDate,
            this.colAIMMaterialCode,
            this.colAIMMaterialName,
            this.colAIMNutrientCode,
            this.colAIMNutrientName,
            this.colResultValue});
            this.grdSampleListView.GridControl = this.grdSampleList;
            this.grdSampleListView.Name = "grdSampleListView";
            // 
            // colSampleID
            // 
            this.colSampleID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSampleID.AppearanceCell.Options.UseFont = true;
            this.colSampleID.AppearanceCell.Options.UseTextOptions = true;
            this.colSampleID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSampleID.AppearanceHeader.Options.UseFont = true;
            this.colSampleID.AppearanceHeader.Options.UseTextOptions = true;
            this.colSampleID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleID.Caption = "Sample ID";
            this.colSampleID.ColumnEdit = this.replnkSampleID;
            this.colSampleID.FieldName = "SampleID";
            this.colSampleID.Name = "colSampleID";
            this.colSampleID.OptionsColumn.ReadOnly = true;
            this.colSampleID.Visible = true;
            this.colSampleID.VisibleIndex = 7;
            this.colSampleID.Width = 128;
            // 
            // replnkSampleID
            // 
            this.replnkSampleID.AutoHeight = false;
            this.replnkSampleID.Name = "replnkSampleID";
            this.replnkSampleID.Click += new System.EventHandler(this.replnkSampleID_Click);
            // 
            // colSampleCode
            // 
            this.colSampleCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colSampleCode.AppearanceCell.Options.UseFont = true;
            this.colSampleCode.AppearanceCell.Options.UseTextOptions = true;
            this.colSampleCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSampleCode.AppearanceHeader.Options.UseFont = true;
            this.colSampleCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colSampleCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleCode.Caption = "Sample Code";
            this.colSampleCode.FieldName = "SampleCode";
            this.colSampleCode.Name = "colSampleCode";
            this.colSampleCode.OptionsColumn.AllowEdit = false;
            this.colSampleCode.OptionsColumn.ReadOnly = true;
            this.colSampleCode.Visible = true;
            this.colSampleCode.VisibleIndex = 5;
            this.colSampleCode.Width = 142;
            // 
            // colSampleDate
            // 
            this.colSampleDate.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSampleDate.AppearanceCell.Options.UseFont = true;
            this.colSampleDate.AppearanceCell.Options.UseTextOptions = true;
            this.colSampleDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleDate.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSampleDate.AppearanceHeader.Options.UseFont = true;
            this.colSampleDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colSampleDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSampleDate.Caption = "Sample Date";
            this.colSampleDate.FieldName = "SampleDate";
            this.colSampleDate.Name = "colSampleDate";
            this.colSampleDate.OptionsColumn.AllowEdit = false;
            this.colSampleDate.OptionsColumn.ReadOnly = true;
            this.colSampleDate.Visible = true;
            this.colSampleDate.VisibleIndex = 4;
            this.colSampleDate.Width = 122;
            // 
            // colAIMMaterialCode
            // 
            this.colAIMMaterialCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMMaterialCode.AppearanceCell.Options.UseFont = true;
            this.colAIMMaterialCode.AppearanceCell.Options.UseTextOptions = true;
            this.colAIMMaterialCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAIMMaterialCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colAIMMaterialCode.AppearanceHeader.Options.UseFont = true;
            this.colAIMMaterialCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colAIMMaterialCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAIMMaterialCode.Caption = "AIM Material Code";
            this.colAIMMaterialCode.FieldName = "AIMMaterialCode";
            this.colAIMMaterialCode.Name = "colAIMMaterialCode";
            this.colAIMMaterialCode.Visible = true;
            this.colAIMMaterialCode.VisibleIndex = 0;
            this.colAIMMaterialCode.Width = 127;
            // 
            // colAIMMaterialName
            // 
            this.colAIMMaterialName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMMaterialName.AppearanceCell.Options.UseFont = true;
            this.colAIMMaterialName.AppearanceCell.Options.UseTextOptions = true;
            this.colAIMMaterialName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAIMMaterialName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colAIMMaterialName.AppearanceHeader.Options.UseFont = true;
            this.colAIMMaterialName.AppearanceHeader.Options.UseTextOptions = true;
            this.colAIMMaterialName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAIMMaterialName.Caption = "AIM Material Name";
            this.colAIMMaterialName.FieldName = "AIMMaterialName";
            this.colAIMMaterialName.Name = "colAIMMaterialName";
            this.colAIMMaterialName.Visible = true;
            this.colAIMMaterialName.VisibleIndex = 1;
            this.colAIMMaterialName.Width = 157;
            // 
            // colAIMNutrientCode
            // 
            this.colAIMNutrientCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMNutrientCode.AppearanceCell.Options.UseFont = true;
            this.colAIMNutrientCode.AppearanceCell.Options.UseTextOptions = true;
            this.colAIMNutrientCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAIMNutrientCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colAIMNutrientCode.AppearanceHeader.Options.UseFont = true;
            this.colAIMNutrientCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colAIMNutrientCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAIMNutrientCode.Caption = "AIM Nutrient Code";
            this.colAIMNutrientCode.FieldName = "AIMNutrientCode";
            this.colAIMNutrientCode.Name = "colAIMNutrientCode";
            this.colAIMNutrientCode.Visible = true;
            this.colAIMNutrientCode.VisibleIndex = 2;
            this.colAIMNutrientCode.Width = 119;
            // 
            // colAIMNutrientName
            // 
            this.colAIMNutrientName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colAIMNutrientName.AppearanceCell.Options.UseFont = true;
            this.colAIMNutrientName.AppearanceCell.Options.UseTextOptions = true;
            this.colAIMNutrientName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAIMNutrientName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colAIMNutrientName.AppearanceHeader.Options.UseFont = true;
            this.colAIMNutrientName.AppearanceHeader.Options.UseTextOptions = true;
            this.colAIMNutrientName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAIMNutrientName.Caption = "AIM Nutrient Name";
            this.colAIMNutrientName.FieldName = "AIMNutrientName";
            this.colAIMNutrientName.Name = "colAIMNutrientName";
            this.colAIMNutrientName.Visible = true;
            this.colAIMNutrientName.VisibleIndex = 3;
            this.colAIMNutrientName.Width = 151;
            // 
            // colResultValue
            // 
            this.colResultValue.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colResultValue.AppearanceCell.Options.UseFont = true;
            this.colResultValue.AppearanceCell.Options.UseTextOptions = true;
            this.colResultValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colResultValue.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colResultValue.AppearanceHeader.Options.UseFont = true;
            this.colResultValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colResultValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colResultValue.Caption = "Result Value";
            this.colResultValue.FieldName = "ResultValue";
            this.colResultValue.Name = "colResultValue";
            this.colResultValue.Visible = true;
            this.colResultValue.VisibleIndex = 6;
            this.colResultValue.Width = 113;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Location = new System.Drawing.Point(0, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(909, 47);
            this.panel4.TabIndex = 154;
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(867, 2);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(35, 36);
            this.btnResize.TabIndex = 168;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(420, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 23);
            this.label5.TabIndex = 149;
            this.label5.Text = "Chart - Sample List";
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(4, 3);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(113, 40);
            this.pnlTopBtns.TabIndex = 123;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(0, -2);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 39);
            this.btnExit.TabIndex = 89;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ChartsSampleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(910, 533);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.grdSampleList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChartsSampleList";
            this.Text = "Chart - Sample List";
            this.SizeChanged += new System.EventHandler(this.ChartsSampleList_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.grdSampleList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSampleListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replnkSampleID)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdSampleList;
        private DevExpress.XtraGrid.Views.Grid.GridView grdSampleListView;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleID;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleCode;
        private DevExpress.XtraGrid.Columns.GridColumn colSampleDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit replnkSampleID;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMMaterialCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMMaterialName;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMNutrientCode;
        private DevExpress.XtraGrid.Columns.GridColumn colAIMNutrientName;
        private DevExpress.XtraGrid.Columns.GridColumn colResultValue;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnExit;
    }
}