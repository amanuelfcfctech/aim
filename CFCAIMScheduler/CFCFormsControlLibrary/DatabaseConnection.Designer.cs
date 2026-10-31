namespace CFCFormsControlLibrary
{
	partial class DatabaseConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseConnection));
            this.lblSearch = new System.Windows.Forms.Label();
            this.lueConnectName = new DevExpress.XtraEditors.LookUpEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbNonProduction = new System.Windows.Forms.RadioButton();
            this.rbProduction = new System.Windows.Forms.RadioButton();
            this.lblConnection = new System.Windows.Forms.Label();
            this.pnlSqlAuthentication = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtConnectName = new System.Windows.Forms.TextBox();
            this.lblConnectionName = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.chkSQLServerAuthentication = new System.Windows.Forms.CheckBox();
            this.lblCurrentDatabase = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlSaveDelete = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lueDatabase = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDatabase = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lueConnectName.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlSqlAuthentication.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlSaveDelete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDatabase.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(69, 81);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(130, 18);
            this.lblSearch.TabIndex = 83;
            this.lblSearch.Text = "Search Connections";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lueConnectName
            // 
            this.lueConnectName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lueConnectName.Location = new System.Drawing.Point(205, 79);
            this.lueConnectName.Name = "lueConnectName";
            this.lueConnectName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueConnectName.Properties.Appearance.Options.UseFont = true;
            this.lueConnectName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueConnectName.Size = new System.Drawing.Size(216, 22);
            this.lueConnectName.TabIndex = 74;
            this.lueConnectName.EditValueChanged += new System.EventHandler(this.lueConnectName_EditValueChanged);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Controls.Add(this.btnNew);
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(753, 58);
            this.panel4.TabIndex = 155;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(304, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(210, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Database Connection";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = ((System.Drawing.Image)(resources.GetObject("btnResize.Image")));
            this.btnResize.Location = new System.Drawing.Point(715, -1);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(33, 34);
            this.btnResize.TabIndex = 115;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.Location = new System.Drawing.Point(59, 5);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(50, 37);
            this.btnNew.TabIndex = 64;
            this.btnNew.TabStop = false;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(4, 5);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 37);
            this.btnExit.TabIndex = 64;
            this.btnExit.TabStop = false;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.lblConnection);
            this.groupBox1.Controls.Add(this.pnlSqlAuthentication);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.txtConnectName);
            this.groupBox1.Controls.Add(this.lblConnectionName);
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Controls.Add(this.chkSQLServerAuthentication);
            this.groupBox1.Controls.Add(this.lblCurrentDatabase);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(40, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(646, 278);
            this.groupBox1.TabIndex = 156;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Connection";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbNonProduction);
            this.panel1.Controls.Add(this.rbProduction);
            this.panel1.Location = new System.Drawing.Point(57, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 51);
            this.panel1.TabIndex = 118;
            // 
            // rbNonProduction
            // 
            this.rbNonProduction.AutoSize = true;
            this.rbNonProduction.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNonProduction.Location = new System.Drawing.Point(5, 26);
            this.rbNonProduction.Name = "rbNonProduction";
            this.rbNonProduction.Size = new System.Drawing.Size(177, 19);
            this.rbNonProduction.TabIndex = 1;
            this.rbNonProduction.Text = "Non-Production Connection";
            this.rbNonProduction.UseVisualStyleBackColor = true;
            // 
            // rbProduction
            // 
            this.rbProduction.AutoSize = true;
            this.rbProduction.Checked = true;
            this.rbProduction.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbProduction.Location = new System.Drawing.Point(5, 4);
            this.rbProduction.Name = "rbProduction";
            this.rbProduction.Size = new System.Drawing.Size(150, 19);
            this.rbProduction.TabIndex = 0;
            this.rbProduction.TabStop = true;
            this.rbProduction.Text = "Production Connection";
            this.rbProduction.UseVisualStyleBackColor = true;
            // 
            // lblConnection
            // 
            this.lblConnection.BackColor = System.Drawing.Color.Transparent;
            this.lblConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConnection.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnection.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblConnection.Location = new System.Drawing.Point(461, 31);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(156, 18);
            this.lblConnection.TabIndex = 117;
            this.lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConnection.Visible = false;
            // 
            // pnlSqlAuthentication
            // 
            this.pnlSqlAuthentication.Controls.Add(this.txtPassword);
            this.pnlSqlAuthentication.Controls.Add(this.txtUserName);
            this.pnlSqlAuthentication.Controls.Add(this.lblPassword);
            this.pnlSqlAuthentication.Controls.Add(this.lblUserName);
            this.pnlSqlAuthentication.Location = new System.Drawing.Point(83, 192);
            this.pnlSqlAuthentication.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSqlAuthentication.Name = "pnlSqlAuthentication";
            this.pnlSqlAuthentication.Size = new System.Drawing.Size(398, 72);
            this.pnlSqlAuthentication.TabIndex = 86;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(133, 37);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(217, 21);
            this.txtPassword.TabIndex = 7;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(133, 13);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(217, 21);
            this.txtUserName.TabIndex = 6;
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(42, 37);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(82, 18);
            this.lblPassword.TabIndex = 59;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(42, 13);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(82, 18);
            this.lblUserName.TabIndex = 58;
            this.lblUserName.Text = "User Name";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.Location = new System.Drawing.Point(461, 55);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(156, 29);
            this.btnTest.TabIndex = 87;
            this.btnTest.Text = "Test Connection";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtConnectName
            // 
            this.txtConnectName.Location = new System.Drawing.Point(214, 31);
            this.txtConnectName.Margin = new System.Windows.Forms.Padding(2);
            this.txtConnectName.Name = "txtConnectName";
            this.txtConnectName.Size = new System.Drawing.Size(217, 21);
            this.txtConnectName.TabIndex = 83;
            // 
            // lblConnectionName
            // 
            this.lblConnectionName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnectionName.BackColor = System.Drawing.Color.Transparent;
            this.lblConnectionName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionName.Location = new System.Drawing.Point(41, 31);
            this.lblConnectionName.Name = "lblConnectionName";
            this.lblConnectionName.Size = new System.Drawing.Size(166, 18);
            this.lblConnectionName.TabIndex = 89;
            this.lblConnectionName.Text = "Database Connection Name";
            this.lblConnectionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(214, 55);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(2);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(217, 21);
            this.txtServerName.TabIndex = 84;
            // 
            // chkSQLServerAuthentication
            // 
            this.chkSQLServerAuthentication.AutoSize = true;
            this.chkSQLServerAuthentication.Checked = true;
            this.chkSQLServerAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSQLServerAuthentication.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSQLServerAuthentication.Location = new System.Drawing.Point(62, 176);
            this.chkSQLServerAuthentication.Margin = new System.Windows.Forms.Padding(2);
            this.chkSQLServerAuthentication.Name = "chkSQLServerAuthentication";
            this.chkSQLServerAuthentication.Size = new System.Drawing.Size(167, 19);
            this.chkSQLServerAuthentication.TabIndex = 85;
            this.chkSQLServerAuthentication.Text = "SQL Server Authentication";
            this.chkSQLServerAuthentication.UseVisualStyleBackColor = true;
            this.chkSQLServerAuthentication.CheckedChanged += new System.EventHandler(this.chkSQLServerAuthentication_CheckedChanged);
            // 
            // lblCurrentDatabase
            // 
            this.lblCurrentDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentDatabase.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentDatabase.Location = new System.Drawing.Point(113, 55);
            this.lblCurrentDatabase.Name = "lblCurrentDatabase";
            this.lblCurrentDatabase.Size = new System.Drawing.Size(96, 18);
            this.lblCurrentDatabase.TabIndex = 88;
            this.lblCurrentDatabase.Text = "Server Name";
            this.lblCurrentDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlSaveDelete);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(40, 423);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(646, 102);
            this.groupBox2.TabIndex = 157;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database Name";
            // 
            // pnlSaveDelete
            // 
            this.pnlSaveDelete.Controls.Add(this.btnDelete);
            this.pnlSaveDelete.Controls.Add(this.btnSave);
            this.pnlSaveDelete.Controls.Add(this.lueDatabase);
            this.pnlSaveDelete.Controls.Add(this.lblDatabase);
            this.pnlSaveDelete.Location = new System.Drawing.Point(33, 18);
            this.pnlSaveDelete.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSaveDelete.Name = "pnlSaveDelete";
            this.pnlSaveDelete.Size = new System.Drawing.Size(584, 67);
            this.pnlSaveDelete.TabIndex = 81;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(520, 19);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(53, 37);
            this.btnDelete.TabIndex = 64;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(447, 19);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(53, 37);
            this.btnSave.TabIndex = 63;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lueDatabase
            // 
            this.lueDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lueDatabase.Location = new System.Drawing.Point(190, 24);
            this.lueDatabase.Name = "lueDatabase";
            this.lueDatabase.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueDatabase.Properties.Appearance.Options.UseFont = true;
            this.lueDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDatabase.Size = new System.Drawing.Size(216, 22);
            this.lueDatabase.TabIndex = 10;
            // 
            // lblDatabase
            // 
            this.lblDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDatabase.BackColor = System.Drawing.Color.Transparent;
            this.lblDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.Location = new System.Drawing.Point(64, 28);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(120, 18);
            this.lblDatabase.TabIndex = 62;
            this.lblDatabase.Text = "Database Name";
            this.lblDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DatabaseConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 540);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lueConnectName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DatabaseConnection";
            this.Text = "Database Connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DatabaseConnection_FormClosing);
            this.Load += new System.EventHandler(this.DatabaseConnection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lueConnectName.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSqlAuthentication.ResumeLayout(false);
            this.pnlSqlAuthentication.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.pnlSaveDelete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueDatabase.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label lblSearch;
		public DevExpress.XtraEditors.LookUpEdit lueConnectName;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnlSqlAuthentication;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtConnectName;
        private System.Windows.Forms.Label lblConnectionName;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.CheckBox chkSQLServerAuthentication;
        private System.Windows.Forms.Label lblCurrentDatabase;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlSaveDelete;
        public DevExpress.XtraEditors.LookUpEdit lueDatabase;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbNonProduction;
        private System.Windows.Forms.RadioButton rbProduction;
    }
}