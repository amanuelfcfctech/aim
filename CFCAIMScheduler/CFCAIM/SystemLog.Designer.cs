namespace CFCAIM
{
	partial class SystemLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemLog));
            this.grdSystemLog = new DevExpress.XtraGrid.GridControl();
            this.grdSystemLogView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSystemLogID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateStamp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMessage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnPreview = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSystemLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSystemLogView)).BeginInit();
            this.pnlTopBtns.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdSystemLog
            // 
            this.grdSystemLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSystemLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSystemLog.Location = new System.Drawing.Point(1, 90);
            this.grdSystemLog.MainView = this.grdSystemLogView;
            this.grdSystemLog.Name = "grdSystemLog";
            this.grdSystemLog.Size = new System.Drawing.Size(965, 529);
            this.grdSystemLog.TabIndex = 30;
            this.grdSystemLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdSystemLogView});
            // 
            // grdSystemLogView
            // 
            this.grdSystemLogView.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.grdSystemLogView.AppearancePrint.EvenRow.Options.UseBackColor = true;
            this.grdSystemLogView.AppearancePrint.OddRow.BackColor = System.Drawing.Color.White;
            this.grdSystemLogView.AppearancePrint.OddRow.Options.UseBackColor = true;
            this.grdSystemLogView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSystemLogID,
            this.colTypeID,
            this.colUpdateStamp,
            this.colUserName,
            this.colMessage});
            this.grdSystemLogView.DetailHeight = 280;
            this.grdSystemLogView.GridControl = this.grdSystemLog;
            this.grdSystemLogView.Name = "grdSystemLogView";
            this.grdSystemLogView.OptionsPrint.EnableAppearanceEvenRow = true;
            this.grdSystemLogView.OptionsPrint.EnableAppearanceOddRow = true;
            // 
            // colSystemLogID
            // 
            this.colSystemLogID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colSystemLogID.AppearanceCell.Options.UseFont = true;
            this.colSystemLogID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colSystemLogID.AppearanceHeader.Options.UseFont = true;
            this.colSystemLogID.Caption = "System Log ID";
            this.colSystemLogID.FieldName = "SystemLogID";
            this.colSystemLogID.MinWidth = 16;
            this.colSystemLogID.Name = "colSystemLogID";
            this.colSystemLogID.OptionsColumn.AllowEdit = false;
            this.colSystemLogID.OptionsColumn.ShowInCustomizationForm = false;
            this.colSystemLogID.Width = 60;
            // 
            // colTypeID
            // 
            this.colTypeID.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colTypeID.AppearanceCell.Options.UseFont = true;
            this.colTypeID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colTypeID.AppearanceHeader.Options.UseFont = true;
            this.colTypeID.Caption = "Type ID";
            this.colTypeID.FieldName = "TypeID";
            this.colTypeID.MinWidth = 16;
            this.colTypeID.Name = "colTypeID";
            this.colTypeID.OptionsColumn.AllowEdit = false;
            this.colTypeID.OptionsColumn.ShowInCustomizationForm = false;
            this.colTypeID.Width = 242;
            // 
            // colUpdateStamp
            // 
            this.colUpdateStamp.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colUpdateStamp.AppearanceCell.Options.UseFont = true;
            this.colUpdateStamp.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colUpdateStamp.AppearanceHeader.Options.UseFont = true;
            this.colUpdateStamp.Caption = "UpdateStamp";
            this.colUpdateStamp.DisplayFormat.FormatString = "g";
            this.colUpdateStamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colUpdateStamp.FieldName = "UpdateStamp";
            this.colUpdateStamp.MinWidth = 16;
            this.colUpdateStamp.Name = "colUpdateStamp";
            this.colUpdateStamp.OptionsColumn.AllowEdit = false;
            this.colUpdateStamp.Visible = true;
            this.colUpdateStamp.VisibleIndex = 2;
            this.colUpdateStamp.Width = 142;
            // 
            // colUserName
            // 
            this.colUserName.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colUserName.AppearanceCell.Options.UseFont = true;
            this.colUserName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colUserName.AppearanceHeader.Options.UseFont = true;
            this.colUserName.Caption = "UserName";
            this.colUserName.FieldName = "UserName";
            this.colUserName.MinWidth = 16;
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 1;
            this.colUserName.Width = 129;
            // 
            // colMessage
            // 
            this.colMessage.AppearanceCell.Font = new System.Drawing.Font("Arial", 9F);
            this.colMessage.AppearanceCell.Options.UseFont = true;
            this.colMessage.AppearanceHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.colMessage.AppearanceHeader.Options.UseFont = true;
            this.colMessage.Caption = "Log Message";
            this.colMessage.FieldName = "Message";
            this.colMessage.MinWidth = 16;
            this.colMessage.Name = "colMessage";
            this.colMessage.OptionsColumn.AllowEdit = false;
            this.colMessage.Visible = true;
            this.colMessage.VisibleIndex = 0;
            this.colMessage.Width = 456;
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTopBtns.BackColor = System.Drawing.Color.DarkBlue;
            this.pnlTopBtns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTopBtns.Controls.Add(this.btnPreview);
            this.pnlTopBtns.Controls.Add(this.label10);
            this.pnlTopBtns.Controls.Add(this.btnResize);
            this.pnlTopBtns.Controls.Add(this.panel1);
            this.pnlTopBtns.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBtns.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(968, 51);
            this.pnlTopBtns.TabIndex = 155;
            // 
            // btnPreview
            // 
            this.btnPreview.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnPreview.Location = new System.Drawing.Point(121, 6);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(57, 37);
            this.btnPreview.TabIndex = 150;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(431, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "System Log";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(930, -1);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 112;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(3, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(115, 38);
            this.panel1.TabIndex = 46;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 37;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(58, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 37);
            this.button2.TabIndex = 38;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // SystemLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(966, 621);
            this.Controls.Add(this.pnlTopBtns);
            this.Controls.Add(this.grdSystemLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SystemLog";
            this.Text = "System Log";
            this.Load += new System.EventHandler(this.SystemLog_Load);
            this.SizeChanged += new System.EventHandler(this.SystemLog_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.grdSystemLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSystemLogView)).EndInit();
            this.pnlTopBtns.ResumeLayout(false);
            this.pnlTopBtns.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraGrid.GridControl grdSystemLog;
		private DevExpress.XtraGrid.Views.Grid.GridView grdSystemLogView;
		private DevExpress.XtraGrid.Columns.GridColumn colSystemLogID;
		private DevExpress.XtraGrid.Columns.GridColumn colTypeID;
		private DevExpress.XtraGrid.Columns.GridColumn colUpdateStamp;
		private DevExpress.XtraGrid.Columns.GridColumn colUserName;
		private DevExpress.XtraGrid.Columns.GridColumn colMessage;
		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnResize;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnPreview;
    }
}