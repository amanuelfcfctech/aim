namespace CFCAIM.Imports
{
	partial class ImportSqlConnections
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportSqlConnections));
            this.panel4 = new System.Windows.Forms.Panel();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.lueConnectName = new DevExpress.XtraEditors.LookUpEdit();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlSQLConnection = new System.Windows.Forms.Panel();
            this.pnlDatabaseName = new System.Windows.Forms.GroupBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.btnSaveConnection = new System.Windows.Forms.Button();
            this.lueDatabase = new DevExpress.XtraEditors.LookUpEdit();
            this.lblConnectionName = new System.Windows.Forms.Label();
            this.txtConnectName = new System.Windows.Forms.TextBox();
            this.panelServer = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbNonProduction = new System.Windows.Forms.RadioButton();
            this.rbProduction = new System.Windows.Forms.RadioButton();
            this.lblConnection = new System.Windows.Forms.Label();
            this.pnlSqlAuthentication = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.lblCurrentDatabase = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.chkSQLServerAuthentication = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueConnectName.Properties)).BeginInit();
            this.pnlSQLConnection.SuspendLayout();
            this.pnlDatabaseName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDatabase.Properties)).BeginInit();
            this.panelServer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlSqlAuthentication.SuspendLayout();
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
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(681, 51);
            this.panel4.TabIndex = 154;
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(3, 3);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(56, 38);
            this.pnlTopBtns.TabIndex = 150;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 1;
            this.btnExit.TabStop = false;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(256, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(266, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "SQL Database Connections";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(642, -1);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 112;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // lueConnectName
            // 
            this.lueConnectName.Location = new System.Drawing.Point(167, 73);
            this.lueConnectName.Name = "lueConnectName";
            this.lueConnectName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueConnectName.Properties.Appearance.Options.UseFont = true;
            this.lueConnectName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueConnectName.Size = new System.Drawing.Size(159, 22);
            this.lueConnectName.TabIndex = 155;
            this.lueConnectName.EditValueChanged += new System.EventHandler(this.lueConnectName_EditValueChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(13, 74);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(150, 18);
            this.lblSearch.TabIndex = 156;
            this.lblSearch.Text = "Search AIM Connections";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.Location = new System.Drawing.Point(332, 63);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(54, 37);
            this.btnNew.TabIndex = 157;
            this.btnNew.TabStop = false;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // pnlSQLConnection
            // 
            this.pnlSQLConnection.Controls.Add(this.pnlDatabaseName);
            this.pnlSQLConnection.Controls.Add(this.panelServer);
            this.pnlSQLConnection.Location = new System.Drawing.Point(15, 118);
            this.pnlSQLConnection.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSQLConnection.Name = "pnlSQLConnection";
            this.pnlSQLConnection.Size = new System.Drawing.Size(646, 310);
            this.pnlSQLConnection.TabIndex = 159;
            this.pnlSQLConnection.Visible = false;
            // 
            // pnlDatabaseName
            // 
            this.pnlDatabaseName.Controls.Add(this.lblDatabase);
            this.pnlDatabaseName.Controls.Add(this.btnSaveConnection);
            this.pnlDatabaseName.Controls.Add(this.lueDatabase);
            this.pnlDatabaseName.Controls.Add(this.lblConnectionName);
            this.pnlDatabaseName.Controls.Add(this.txtConnectName);
            this.pnlDatabaseName.Font = new System.Drawing.Font("Arial", 9F);
            this.pnlDatabaseName.Location = new System.Drawing.Point(10, 229);
            this.pnlDatabaseName.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDatabaseName.Name = "pnlDatabaseName";
            this.pnlDatabaseName.Padding = new System.Windows.Forms.Padding(2);
            this.pnlDatabaseName.Size = new System.Drawing.Size(627, 74);
            this.pnlDatabaseName.TabIndex = 74;
            this.pnlDatabaseName.TabStop = false;
            this.pnlDatabaseName.Text = "Database Name";
            this.pnlDatabaseName.Visible = false;
            // 
            // lblDatabase
            // 
            this.lblDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDatabase.BackColor = System.Drawing.Color.Transparent;
            this.lblDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.Location = new System.Drawing.Point(97, 22);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(102, 18);
            this.lblDatabase.TabIndex = 66;
            this.lblDatabase.Text = "Database Name";
            this.lblDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSaveConnection
            // 
            this.btnSaveConnection.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConnection.Image = global::CFCAIM.Properties.Resources.save_32x32;
            this.btnSaveConnection.Location = new System.Drawing.Point(456, 23);
            this.btnSaveConnection.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveConnection.Name = "btnSaveConnection";
            this.btnSaveConnection.Size = new System.Drawing.Size(57, 37);
            this.btnSaveConnection.TabIndex = 160;
            this.btnSaveConnection.UseVisualStyleBackColor = true;
            this.btnSaveConnection.Visible = false;
            this.btnSaveConnection.Click += new System.EventHandler(this.btnSaveConnection_Click);
            // 
            // lueDatabase
            // 
            this.lueDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lueDatabase.Location = new System.Drawing.Point(211, 21);
            this.lueDatabase.Name = "lueDatabase";
            this.lueDatabase.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueDatabase.Properties.Appearance.Options.UseFont = true;
            this.lueDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDatabase.Properties.DropDownRows = 15;
            this.lueDatabase.Size = new System.Drawing.Size(230, 22);
            this.lueDatabase.TabIndex = 65;
            this.lueDatabase.EditValueChanged += new System.EventHandler(this.lueDatabase_EditValueChanged);
            // 
            // lblConnectionName
            // 
            this.lblConnectionName.BackColor = System.Drawing.Color.Transparent;
            this.lblConnectionName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionName.Location = new System.Drawing.Point(24, 44);
            this.lblConnectionName.Name = "lblConnectionName";
            this.lblConnectionName.Size = new System.Drawing.Size(175, 18);
            this.lblConnectionName.TabIndex = 72;
            this.lblConnectionName.Text = "Connection Name";
            this.lblConnectionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtConnectName
            // 
            this.txtConnectName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectName.Location = new System.Drawing.Point(211, 43);
            this.txtConnectName.Margin = new System.Windows.Forms.Padding(2);
            this.txtConnectName.Name = "txtConnectName";
            this.txtConnectName.Size = new System.Drawing.Size(231, 21);
            this.txtConnectName.TabIndex = 71;
            this.txtConnectName.TextChanged += new System.EventHandler(this.txtConnectName_TextChanged);
            // 
            // panelServer
            // 
            this.panelServer.Controls.Add(this.panel1);
            this.panelServer.Controls.Add(this.lblConnection);
            this.panelServer.Controls.Add(this.pnlSqlAuthentication);
            this.panelServer.Controls.Add(this.btnTestConnection);
            this.panelServer.Controls.Add(this.lblCurrentDatabase);
            this.panelServer.Controls.Add(this.txtServerName);
            this.panelServer.Controls.Add(this.chkSQLServerAuthentication);
            this.panelServer.Font = new System.Drawing.Font("Arial", 9F);
            this.panelServer.Location = new System.Drawing.Point(10, 1);
            this.panelServer.Margin = new System.Windows.Forms.Padding(2);
            this.panelServer.Name = "panelServer";
            this.panelServer.Padding = new System.Windows.Forms.Padding(2);
            this.panelServer.Size = new System.Drawing.Size(627, 220);
            this.panelServer.TabIndex = 73;
            this.panelServer.TabStop = false;
            this.panelServer.Text = "AIM Connection";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbNonProduction);
            this.panel1.Controls.Add(this.rbProduction);
            this.panel1.Location = new System.Drawing.Point(40, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 50);
            this.panel1.TabIndex = 119;
            // 
            // rbNonProduction
            // 
            this.rbNonProduction.AutoSize = true;
            this.rbNonProduction.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNonProduction.Location = new System.Drawing.Point(5, 25);
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
            this.lblConnection.Location = new System.Drawing.Point(456, 20);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(166, 18);
            this.lblConnection.TabIndex = 118;
            this.lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblConnection.Visible = false;
            // 
            // pnlSqlAuthentication
            // 
            this.pnlSqlAuthentication.Controls.Add(this.lblUserName);
            this.pnlSqlAuthentication.Controls.Add(this.lblPassword);
            this.pnlSqlAuthentication.Controls.Add(this.txtUserName);
            this.pnlSqlAuthentication.Controls.Add(this.txtPassword);
            this.pnlSqlAuthentication.Location = new System.Drawing.Point(97, 156);
            this.pnlSqlAuthentication.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSqlAuthentication.Name = "pnlSqlAuthentication";
            this.pnlSqlAuthentication.Size = new System.Drawing.Size(351, 50);
            this.pnlSqlAuthentication.TabIndex = 70;
            this.pnlSqlAuthentication.Visible = false;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(5, 4);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(95, 18);
            this.lblUserName.TabIndex = 62;
            this.lblUserName.Text = "User Name";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(5, 26);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(95, 18);
            this.lblPassword.TabIndex = 63;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(113, 4);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(231, 21);
            this.txtUserName.TabIndex = 60;
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(113, 26);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(231, 21);
            this.txtPassword.TabIndex = 61;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestConnection.Location = new System.Drawing.Point(456, 38);
            this.btnTestConnection.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(166, 32);
            this.btnTestConnection.TabIndex = 64;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // lblCurrentDatabase
            // 
            this.lblCurrentDatabase.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentDatabase.Location = new System.Drawing.Point(87, 20);
            this.lblCurrentDatabase.Name = "lblCurrentDatabase";
            this.lblCurrentDatabase.Size = new System.Drawing.Size(102, 18);
            this.lblCurrentDatabase.TabIndex = 58;
            this.lblCurrentDatabase.Text = "Server Name";
            this.lblCurrentDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServerName
            // 
            this.txtServerName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerName.Location = new System.Drawing.Point(209, 20);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(2);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(231, 21);
            this.txtServerName.TabIndex = 56;
            this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
            // 
            // chkSQLServerAuthentication
            // 
            this.chkSQLServerAuthentication.AutoSize = true;
            this.chkSQLServerAuthentication.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSQLServerAuthentication.Location = new System.Drawing.Point(39, 139);
            this.chkSQLServerAuthentication.Margin = new System.Windows.Forms.Padding(2);
            this.chkSQLServerAuthentication.Name = "chkSQLServerAuthentication";
            this.chkSQLServerAuthentication.Size = new System.Drawing.Size(167, 19);
            this.chkSQLServerAuthentication.TabIndex = 57;
            this.chkSQLServerAuthentication.Text = "SQL Server Authentication";
            this.chkSQLServerAuthentication.UseVisualStyleBackColor = true;
            this.chkSQLServerAuthentication.CheckedChanged += new System.EventHandler(this.chkSQLServerAuthentication_CheckedChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::CFCAIM.Properties.Resources.trash_can;
            this.btnDelete.Location = new System.Drawing.Point(390, 64);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(57, 37);
            this.btnDelete.TabIndex = 161;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ImportSqlConnections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(681, 444);
            this.ControlBox = false;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.pnlSQLConnection);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.lueConnectName);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportSqlConnections";
            this.Text = "SQL Database Connections";
            this.Load += new System.EventHandler(this.ImportSqlConnections_Load);
            this.SizeChanged += new System.EventHandler(this.ImportSqlConnections_SizeChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lueConnectName.Properties)).EndInit();
            this.pnlSQLConnection.ResumeLayout(false);
            this.pnlDatabaseName.ResumeLayout(false);
            this.pnlDatabaseName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDatabase.Properties)).EndInit();
            this.panelServer.ResumeLayout(false);
            this.panelServer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSqlAuthentication.ResumeLayout(false);
            this.pnlSqlAuthentication.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnResize;
		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnExit;
		public DevExpress.XtraEditors.LookUpEdit lueConnectName;
		private System.Windows.Forms.Label lblSearch;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Panel pnlSQLConnection;
		private System.Windows.Forms.GroupBox pnlDatabaseName;
		private System.Windows.Forms.Label lblDatabase;
		public DevExpress.XtraEditors.LookUpEdit lueDatabase;
		private System.Windows.Forms.Label lblConnectionName;
		private System.Windows.Forms.TextBox txtConnectName;
		private System.Windows.Forms.GroupBox panelServer;
		private System.Windows.Forms.Label lblConnection;
		private System.Windows.Forms.Panel pnlSqlAuthentication;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Button btnTestConnection;
		private System.Windows.Forms.Label lblCurrentDatabase;
		private System.Windows.Forms.TextBox txtServerName;
		private System.Windows.Forms.CheckBox chkSQLServerAuthentication;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSaveConnection;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbNonProduction;
        private System.Windows.Forms.RadioButton rbProduction;
    }
}