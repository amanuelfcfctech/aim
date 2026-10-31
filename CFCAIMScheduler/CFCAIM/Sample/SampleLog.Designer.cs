namespace CFCAIM
{
	partial class SampleLog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleLog));
			this.grdLog = new DevExpress.XtraGrid.GridControl();
			this.grdViewLog = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colSampleLogID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colSampleID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colLogCreateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colLogUserName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colLogMessage = new DevExpress.XtraGrid.Columns.GridColumn();
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnGraph = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.grdLog)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdViewLog)).BeginInit();
			this.pnlTopBtns.SuspendLayout();
			this.SuspendLayout();
			// 
			// grdLog
			// 
			this.grdLog.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdLog.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
			this.grdLog.Location = new System.Drawing.Point(0, 62);
			this.grdLog.MainView = this.grdViewLog;
			this.grdLog.Margin = new System.Windows.Forms.Padding(4);
			this.grdLog.Name = "grdLog";
			this.grdLog.Size = new System.Drawing.Size(985, 487);
			this.grdLog.TabIndex = 1;
			this.grdLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewLog});
			// 
			// grdViewLog
			// 
			this.grdViewLog.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
			this.grdViewLog.AppearancePrint.EvenRow.Options.UseBackColor = true;
			this.grdViewLog.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
			this.grdViewLog.AppearancePrint.OddRow.Options.UseBackColor = true;
			this.grdViewLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSampleLogID,
            this.colSampleID,
            this.colLogCreateStamp,
            this.colLogUserName,
            this.colLogMessage});
			this.grdViewLog.GridControl = this.grdLog;
			this.grdViewLog.Name = "grdViewLog";
			this.grdViewLog.OptionsPrint.EnableAppearanceEvenRow = true;
			this.grdViewLog.OptionsPrint.EnableAppearanceOddRow = true;
			// 
			// colSampleLogID
			// 
			this.colSampleLogID.Caption = "Sample Log ID";
			this.colSampleLogID.FieldName = "SampleLogID";
			this.colSampleLogID.Name = "colSampleLogID";
			this.colSampleLogID.OptionsColumn.AllowEdit = false;
			// 
			// colSampleID
			// 
			this.colSampleID.Caption = "Sample ID";
			this.colSampleID.FieldName = "SampleID";
			this.colSampleID.Name = "colSampleID";
			this.colSampleID.Visible = true;
			this.colSampleID.VisibleIndex = 0;
			this.colSampleID.Width = 303;
			// 
			// colLogCreateStamp
			// 
			this.colLogCreateStamp.Caption = "Create Stamp";
			this.colLogCreateStamp.DisplayFormat.FormatString = "g";
			this.colLogCreateStamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.colLogCreateStamp.FieldName = "CreateStamp";
			this.colLogCreateStamp.Name = "colLogCreateStamp";
			this.colLogCreateStamp.OptionsColumn.AllowEdit = false;
			this.colLogCreateStamp.Visible = true;
			this.colLogCreateStamp.VisibleIndex = 3;
			this.colLogCreateStamp.Width = 178;
			// 
			// colLogUserName
			// 
			this.colLogUserName.Caption = "UserName";
			this.colLogUserName.FieldName = "UserName";
			this.colLogUserName.Name = "colLogUserName";
			this.colLogUserName.OptionsColumn.AllowEdit = false;
			this.colLogUserName.Visible = true;
			this.colLogUserName.VisibleIndex = 2;
			this.colLogUserName.Width = 161;
			// 
			// colLogMessage
			// 
			this.colLogMessage.Caption = "Log Message";
			this.colLogMessage.FieldName = "LogMessage";
			this.colLogMessage.Name = "colLogMessage";
			this.colLogMessage.OptionsColumn.AllowEdit = false;
			this.colLogMessage.Visible = true;
			this.colLogMessage.VisibleIndex = 1;
			this.colLogMessage.Width = 570;
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnGraph);
			this.pnlTopBtns.Controls.Add(this.btnRefresh);
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Location = new System.Drawing.Point(0, 0);
			this.pnlTopBtns.Margin = new System.Windows.Forms.Padding(4);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(245, 60);
			this.pnlTopBtns.TabIndex = 29;
			// 
			// btnGraph
			// 
			this.btnGraph.Image = global::CFCAIM.Properties.Resources.preview_32x32;
			this.btnGraph.Location = new System.Drawing.Point(165, 9);
			this.btnGraph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnGraph.Name = "btnGraph";
			this.btnGraph.Size = new System.Drawing.Size(71, 46);
			this.btnGraph.TabIndex = 28;
			this.btnGraph.UseVisualStyleBackColor = true;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
			this.btnRefresh.Location = new System.Drawing.Point(89, 10);
			this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(71, 46);
			this.btnRefresh.TabIndex = 18;
			this.btnRefresh.UseVisualStyleBackColor = true;
			// 
			// btnExit
			// 
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(20, 10);
			this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(67, 46);
			this.btnExit.TabIndex = 4;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// SampleLog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(985, 549);
			this.Controls.Add(this.pnlTopBtns);
			this.Controls.Add(this.grdLog);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SampleLog";
			this.Text = "Sample Log";
			this.Load += new System.EventHandler(this.SampleLog_Load);
			((System.ComponentModel.ISupportInitialize)(this.grdLog)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdViewLog)).EndInit();
			this.pnlTopBtns.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl grdLog;
		private DevExpress.XtraGrid.Views.Grid.GridView grdViewLog;
		private DevExpress.XtraGrid.Columns.GridColumn colSampleLogID;
		private DevExpress.XtraGrid.Columns.GridColumn colSampleID;
		private DevExpress.XtraGrid.Columns.GridColumn colLogCreateStamp;
		private DevExpress.XtraGrid.Columns.GridColumn colLogUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colLogMessage;
		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnGraph;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;
	}
}