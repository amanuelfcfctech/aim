namespace CFCAIM
{
	partial class UtilitiesCFCTech
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UtilitiesCFCTech));
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnExit = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblDatabase = new System.Windows.Forms.Label();
			this.lueConnectName = new DevExpress.XtraEditors.LookUpEdit();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();
			this.txtScript = new System.Windows.Forms.TextBox();
			this.btnRun = new System.Windows.Forms.Button();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtResults = new System.Windows.Forms.TextBox();
			this.grdResults = new DevExpress.XtraGrid.GridControl();
			this.grdResultsView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.btnResize = new System.Windows.Forms.Button();
			this.pnlTopBtns.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueConnectName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdResults)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grdResultsView)).BeginInit();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Location = new System.Drawing.Point(4, 5);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(51, 37);
			this.pnlTopBtns.TabIndex = 105;
			// 
			// btnExit
			// 
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(0, 0);
			this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(50, 37);
			this.btnExit.TabIndex = 89;
			this.btnExit.TabStop = false;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.lblDatabase);
			this.panel1.Controls.Add(this.lueConnectName);
			this.panel1.Controls.Add(this.btnClear);
			this.panel1.Controls.Add(this.btnLogin);
			this.panel1.Controls.Add(this.txtScript);
			this.panel1.Controls.Add(this.btnRun);
			this.panel1.Controls.Add(this.txtPassword);
			this.panel1.Controls.Add(this.lblPassword);
			this.panel1.Location = new System.Drawing.Point(10, 80);
			this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(836, 306);
			this.panel1.TabIndex = 1;
			this.panel1.TabStop = true;
			// 
			// lblDatabase
			// 
			this.lblDatabase.BackColor = System.Drawing.Color.Transparent;
			this.lblDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDatabase.Location = new System.Drawing.Point(336, 15);
			this.lblDatabase.Name = "lblDatabase";
			this.lblDatabase.Size = new System.Drawing.Size(113, 18);
			this.lblDatabase.TabIndex = 157;
			this.lblDatabase.Text = "Select Database";
			this.lblDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblDatabase.Visible = false;
			// 
			// lueConnectName
			// 
			this.lueConnectName.Location = new System.Drawing.Point(454, 14);
			this.lueConnectName.Name = "lueConnectName";
			this.lueConnectName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lueConnectName.Properties.Appearance.Options.UseFont = true;
			this.lueConnectName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lueConnectName.Size = new System.Drawing.Size(149, 22);
			this.lueConnectName.TabIndex = 156;
			this.lueConnectName.Visible = false;
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClear.Location = new System.Drawing.Point(652, 9);
			this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(80, 28);
			this.btnClear.TabIndex = 34;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Visible = false;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnLogin
			// 
			this.btnLogin.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLogin.Location = new System.Drawing.Point(225, 9);
			this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(80, 28);
			this.btnLogin.TabIndex = 33;
			this.btnLogin.Text = "Log In";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// txtScript
			// 
			this.txtScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtScript.Location = new System.Drawing.Point(11, 43);
			this.txtScript.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtScript.Multiline = true;
			this.txtScript.Name = "txtScript";
			this.txtScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtScript.Size = new System.Drawing.Size(820, 256);
			this.txtScript.TabIndex = 2;
			// 
			// btnRun
			// 
			this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRun.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRun.Location = new System.Drawing.Point(737, 9);
			this.btnRun.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(80, 28);
			this.btnRun.TabIndex = 3;
			this.btnRun.Text = "Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Visible = false;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Arial", 9F);
			this.txtPassword.Location = new System.Drawing.Point(110, 11);
			this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtPassword.MaxLength = 10;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(112, 21);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Font = new System.Drawing.Font("Arial", 9F);
			this.lblPassword.Location = new System.Drawing.Point(16, 14);
			this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(94, 15);
			this.lblPassword.TabIndex = 32;
			this.lblPassword.Text = "Enter password";
			// 
			// txtResults
			// 
			this.txtResults.Location = new System.Drawing.Point(690, 57);
			this.txtResults.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtResults.Multiline = true;
			this.txtResults.Name = "txtResults";
			this.txtResults.Size = new System.Drawing.Size(83, 14);
			this.txtResults.TabIndex = 107;
			this.txtResults.TabStop = false;
			this.txtResults.Visible = false;
			// 
			// grdResults
			// 
			this.grdResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdResults.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.grdResults.Location = new System.Drawing.Point(10, 392);
			this.grdResults.MainView = this.grdResultsView;
			this.grdResults.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.grdResults.Name = "grdResults";
			this.grdResults.Size = new System.Drawing.Size(836, 160);
			this.grdResults.TabIndex = 108;
			this.grdResults.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdResultsView});
			// 
			// grdResultsView
			// 
			this.grdResultsView.DetailHeight = 284;
			this.grdResultsView.GridControl = this.grdResults;
			this.grdResultsView.Name = "grdResultsView";
			this.grdResultsView.OptionsCustomization.AllowRowSizing = true;
			this.grdResultsView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
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
			this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(854, 52);
			this.panel4.TabIndex = 154;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.label10.ForeColor = System.Drawing.Color.White;
			this.label10.Location = new System.Drawing.Point(412, 10);
			this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(95, 23);
			this.label10.TabIndex = 149;
			this.label10.Text = "CFCTech";
			// 
			// btnResize
			// 
			this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
			this.btnResize.Location = new System.Drawing.Point(816, -1);
			this.btnResize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnResize.Name = "btnResize";
			this.btnResize.Size = new System.Drawing.Size(34, 34);
			this.btnResize.TabIndex = 112;
			this.btnResize.UseVisualStyleBackColor = true;
			this.btnResize.Visible = false;
			this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
			// 
			// UtilitiesCFCTech
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(854, 557);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.grdResults);
			this.Controls.Add(this.txtResults);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "UtilitiesCFCTech";
			this.Text = "CFCTech";
			this.Load += new System.EventHandler(this.UtilitiesCFCTech_Load);
			this.SizeChanged += new System.EventHandler(this.UtilitiesCFCTech_SizeChanged);
			this.pnlTopBtns.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueConnectName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdResults)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grdResultsView)).EndInit();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtScript;
		private System.Windows.Forms.TextBox txtResults;
		private DevExpress.XtraGrid.GridControl grdResults;
		private DevExpress.XtraGrid.Views.Grid.GridView grdResultsView;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.Button btnClear;
        public DevExpress.XtraEditors.LookUpEdit lueConnectName;
        private System.Windows.Forms.Label lblDatabase;
    }
}