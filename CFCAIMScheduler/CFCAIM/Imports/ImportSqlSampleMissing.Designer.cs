namespace CFCAIM.Imports
{
	partial class ImportSqlSampleMissing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSqlSampleMissing));
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.grdMissingSamples = new DevExpress.XtraGrid.GridControl();
            this.grdMissingSamplesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMissingSampleCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMissingSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMissingSamplesView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Location = new System.Drawing.Point(1, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1020, 51);
            this.panel4.TabIndex = 154;
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Location = new System.Drawing.Point(5, 4);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(113, 40);
            this.pnlTopBtns.TabIndex = 150;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(0, -2);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 39);
            this.btnExit.TabIndex = 89;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(55, -2);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 39);
            this.btnRefresh.TabIndex = 88;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(246, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(282, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Sample Results Not Imported";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(979, 2);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 112;
            this.btnResize.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // grdMissingSamples
            // 
            this.grdMissingSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMissingSamples.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdMissingSamples.Location = new System.Drawing.Point(1, 128);
            this.grdMissingSamples.MainView = this.grdMissingSamplesView;
            this.grdMissingSamples.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdMissingSamples.Name = "grdMissingSamples";
            this.grdMissingSamples.Size = new System.Drawing.Size(1018, 356);
            this.grdMissingSamples.TabIndex = 155;
            this.grdMissingSamples.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdMissingSamplesView});
            // 
            // grdMissingSamplesView
            // 
            this.grdMissingSamplesView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdMissingSamplesView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdMissingSamplesView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdMissingSamplesView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdMissingSamplesView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMissingSampleCode,
            this.colCreatedStamp});
            this.grdMissingSamplesView.GridControl = this.grdMissingSamples;
            this.grdMissingSamplesView.Name = "grdMissingSamplesView";
            this.grdMissingSamplesView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdMissingSamplesView.OptionsPrint.EnableAppearanceOddRow = true;
            // 
            // colMissingSampleCode
            // 
            this.colMissingSampleCode.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colMissingSampleCode.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMissingSampleCode.AppearanceCell.Options.UseBackColor = true;
            this.colMissingSampleCode.AppearanceCell.Options.UseFont = true;
            this.colMissingSampleCode.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colMissingSampleCode.AppearanceHeader.Options.UseFont = true;
            this.colMissingSampleCode.Caption = "Sample Code";
            this.colMissingSampleCode.FieldName = "MissingSampleCode";
            this.colMissingSampleCode.Name = "colMissingSampleCode";
            this.colMissingSampleCode.OptionsColumn.AllowEdit = false;
            this.colMissingSampleCode.Visible = true;
            this.colMissingSampleCode.VisibleIndex = 0;
            this.colMissingSampleCode.Width = 367;
            // 
            // colCreatedStamp
            // 
            this.colCreatedStamp.AppearanceCell.BackColor = System.Drawing.Color.WhiteSmoke;
            this.colCreatedStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colCreatedStamp.AppearanceCell.Options.UseBackColor = true;
            this.colCreatedStamp.AppearanceCell.Options.UseFont = true;
            this.colCreatedStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colCreatedStamp.AppearanceHeader.Options.UseFont = true;
            this.colCreatedStamp.Caption = "Created Stamp";
            this.colCreatedStamp.FieldName = "CreatedStamp";
            this.colCreatedStamp.Name = "colCreatedStamp";
            this.colCreatedStamp.OptionsColumn.AllowEdit = false;
            this.colCreatedStamp.Visible = true;
            this.colCreatedStamp.VisibleIndex = 1;
            this.colCreatedStamp.Width = 370;
            // 
            // ImportSqlSampleMissing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1022, 490);
            this.Controls.Add(this.grdMissingSamples);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ImportSqlSampleMissing";
            this.Text = "Sample Results Not Imported";
            this.Load += new System.EventHandler(this.ImportSqlSampleMissing_Load);
            this.SizeChanged += new System.EventHandler(this.ImportSqlSampleMissing_SizeChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMissingSamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdMissingSamplesView)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnResize;
		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnRefresh;
		private DevExpress.XtraGrid.GridControl grdMissingSamples;
		private DevExpress.XtraGrid.Views.Grid.GridView grdMissingSamplesView;
		private DevExpress.XtraGrid.Columns.GridColumn colMissingSampleCode;
		private DevExpress.XtraGrid.Columns.GridColumn colCreatedStamp;
	}
}