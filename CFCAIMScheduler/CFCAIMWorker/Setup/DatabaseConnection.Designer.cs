
namespace CFCAIMWorker
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
			this.panel4 = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.btnExit = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.pnlSaveDelete = new System.Windows.Forms.Panel();
			this.lueDatabase = new DevExpress.XtraEditors.LookUpEdit();
			this.lblDatabase = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtID = new System.Windows.Forms.TextBox();
			this.rbAIM = new System.Windows.Forms.RadioButton();
			this.rbNir = new System.Windows.Forms.RadioButton();
			this.rgType = new DevExpress.XtraEditors.RadioGroup();
			this.lblConnection = new System.Windows.Forms.Label();
			this.lblCurrentDatabase = new System.Windows.Forms.Label();
			this.chkSQLServerAuthentication = new System.Windows.Forms.CheckBox();
			this.btnTest = new System.Windows.Forms.Button();
			this.pnlSqlAuthentication = new System.Windows.Forms.Panel();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.lblUserName = new System.Windows.Forms.Label();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.panel4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.pnlSaveDelete.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueDatabase.Properties)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rgType.Properties)).BeginInit();
			this.pnlSqlAuthentication.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.BackColor = System.Drawing.Color.DarkBlue;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.label10);
			this.panel4.Controls.Add(this.btnExit);
			this.panel4.Location = new System.Drawing.Point(1, 0);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(743, 52);
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
			// btnExit
			// 
			this.btnExit.BackColor = System.Drawing.SystemColors.Control;
			this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(17, 5);
			this.btnExit.Margin = new System.Windows.Forms.Padding(2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(50, 37);
			this.btnExit.TabIndex = 64;
			this.btnExit.TabStop = false;
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.pnlSaveDelete);
			this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(31, 316);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox2.Size = new System.Drawing.Size(681, 106);
			this.groupBox2.TabIndex = 159;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Database Name";
			this.groupBox2.Visible = false;
			// 
			// pnlSaveDelete
			// 
			this.pnlSaveDelete.Controls.Add(this.lueDatabase);
			this.pnlSaveDelete.Controls.Add(this.lblDatabase);
			this.pnlSaveDelete.Controls.Add(this.btnSave);
			this.pnlSaveDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlSaveDelete.Location = new System.Drawing.Point(34, 20);
			this.pnlSaveDelete.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSaveDelete.Name = "pnlSaveDelete";
			this.pnlSaveDelete.Size = new System.Drawing.Size(604, 63);
			this.pnlSaveDelete.TabIndex = 9;
			// 
			// lueDatabase
			// 
			this.lueDatabase.Location = new System.Drawing.Point(183, 20);
			this.lueDatabase.Name = "lueDatabase";
			this.lueDatabase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lueDatabase.Size = new System.Drawing.Size(240, 20);
			this.lueDatabase.TabIndex = 63;
			// 
			// lblDatabase
			// 
			this.lblDatabase.BackColor = System.Drawing.Color.Transparent;
			this.lblDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDatabase.Location = new System.Drawing.Point(44, 22);
			this.lblDatabase.Name = "lblDatabase";
			this.lblDatabase.Size = new System.Drawing.Size(122, 18);
			this.lblDatabase.TabIndex = 62;
			this.lblDatabase.Text = "Database Name";
			this.lblDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Image = global::CFCAIMWorker.Properties.Resources.save_32x32;
			this.btnSave.Location = new System.Drawing.Point(456, 12);
			this.btnSave.Margin = new System.Windows.Forms.Padding(2);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(53, 37);
			this.btnSave.TabIndex = 11;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtID);
			this.groupBox1.Controls.Add(this.rbAIM);
			this.groupBox1.Controls.Add(this.rbNir);
			this.groupBox1.Controls.Add(this.rgType);
			this.groupBox1.Controls.Add(this.lblConnection);
			this.groupBox1.Controls.Add(this.lblCurrentDatabase);
			this.groupBox1.Controls.Add(this.chkSQLServerAuthentication);
			this.groupBox1.Controls.Add(this.btnTest);
			this.groupBox1.Controls.Add(this.pnlSqlAuthentication);
			this.groupBox1.Controls.Add(this.txtServerName);
			this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(31, 79);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox1.Size = new System.Drawing.Size(681, 197);
			this.groupBox1.TabIndex = 158;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "AIM Connection";
			// 
			// txtID
			// 
			this.txtID.Location = new System.Drawing.Point(512, 112);
			this.txtID.Name = "txtID";
			this.txtID.Size = new System.Drawing.Size(96, 21);
			this.txtID.TabIndex = 120;
			// 
			// rbAIM
			// 
			this.rbAIM.AutoSize = true;
			this.rbAIM.Location = new System.Drawing.Point(311, 27);
			this.rbAIM.Name = "rbAIM";
			this.rbAIM.Size = new System.Drawing.Size(46, 19);
			this.rbAIM.TabIndex = 119;
			this.rbAIM.TabStop = true;
			this.rbAIM.Text = "AIM";
			this.rbAIM.UseVisualStyleBackColor = true;
			this.rbAIM.CheckedChanged += new System.EventHandler(this.rbAIM_CheckedChanged);
			// 
			// rbNir
			// 
			this.rbNir.AutoSize = true;
			this.rbNir.Location = new System.Drawing.Point(156, 27);
			this.rbNir.Name = "rbNir";
			this.rbNir.Size = new System.Drawing.Size(44, 19);
			this.rbNir.TabIndex = 118;
			this.rbNir.TabStop = true;
			this.rbNir.Text = "NIR";
			this.rbNir.UseVisualStyleBackColor = true;
			this.rbNir.CheckedChanged += new System.EventHandler(this.rbNir_CheckedChanged);
			// 
			// rgType
			// 
			this.rgType.Location = new System.Drawing.Point(99, 16);
			this.rgType.Name = "rgType";
			this.rgType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.rgType.Properties.Appearance.Options.UseBackColor = true;
			this.rgType.Size = new System.Drawing.Size(371, 37);
			this.rgType.TabIndex = 117;
			// 
			// lblConnection
			// 
			this.lblConnection.BackColor = System.Drawing.Color.Transparent;
			this.lblConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblConnection.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblConnection.ForeColor = System.Drawing.Color.DodgerBlue;
			this.lblConnection.Location = new System.Drawing.Point(510, 28);
			this.lblConnection.Name = "lblConnection";
			this.lblConnection.Size = new System.Drawing.Size(156, 18);
			this.lblConnection.TabIndex = 116;
			this.lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblConnection.Visible = false;
			// 
			// lblCurrentDatabase
			// 
			this.lblCurrentDatabase.BackColor = System.Drawing.Color.Transparent;
			this.lblCurrentDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCurrentDatabase.Location = new System.Drawing.Point(104, 59);
			this.lblCurrentDatabase.Name = "lblCurrentDatabase";
			this.lblCurrentDatabase.Size = new System.Drawing.Size(96, 18);
			this.lblCurrentDatabase.TabIndex = 48;
			this.lblCurrentDatabase.Text = "Server Name";
			this.lblCurrentDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chkSQLServerAuthentication
			// 
			this.chkSQLServerAuthentication.AutoSize = true;
			this.chkSQLServerAuthentication.Checked = true;
			this.chkSQLServerAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSQLServerAuthentication.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkSQLServerAuthentication.Location = new System.Drawing.Point(46, 97);
			this.chkSQLServerAuthentication.Margin = new System.Windows.Forms.Padding(2);
			this.chkSQLServerAuthentication.Name = "chkSQLServerAuthentication";
			this.chkSQLServerAuthentication.Size = new System.Drawing.Size(167, 19);
			this.chkSQLServerAuthentication.TabIndex = 4;
			this.chkSQLServerAuthentication.Text = "SQL Server Authentication";
			this.chkSQLServerAuthentication.UseVisualStyleBackColor = true;
			// 
			// btnTest
			// 
			this.btnTest.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTest.Location = new System.Drawing.Point(510, 52);
			this.btnTest.Margin = new System.Windows.Forms.Padding(2);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(155, 32);
			this.btnTest.TabIndex = 8;
			this.btnTest.Text = "Test Connection";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// pnlSqlAuthentication
			// 
			this.pnlSqlAuthentication.Controls.Add(this.txtPassword);
			this.pnlSqlAuthentication.Controls.Add(this.txtUserName);
			this.pnlSqlAuthentication.Controls.Add(this.lblPassword);
			this.pnlSqlAuthentication.Controls.Add(this.lblUserName);
			this.pnlSqlAuthentication.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pnlSqlAuthentication.Location = new System.Drawing.Point(107, 120);
			this.pnlSqlAuthentication.Margin = new System.Windows.Forms.Padding(2);
			this.pnlSqlAuthentication.Name = "pnlSqlAuthentication";
			this.pnlSqlAuthentication.Size = new System.Drawing.Size(368, 62);
			this.pnlSqlAuthentication.TabIndex = 5;
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword.Location = new System.Drawing.Point(123, 32);
			this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(240, 21);
			this.txtPassword.TabIndex = 7;
			// 
			// txtUserName
			// 
			this.txtUserName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUserName.Location = new System.Drawing.Point(123, 10);
			this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(240, 21);
			this.txtUserName.TabIndex = 6;
			// 
			// lblPassword
			// 
			this.lblPassword.BackColor = System.Drawing.Color.Transparent;
			this.lblPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPassword.Location = new System.Drawing.Point(10, 32);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(96, 18);
			this.lblPassword.TabIndex = 59;
			this.lblPassword.Text = "Password";
			this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblUserName
			// 
			this.lblUserName.BackColor = System.Drawing.Color.Transparent;
			this.lblUserName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUserName.Location = new System.Drawing.Point(10, 10);
			this.lblUserName.Name = "lblUserName";
			this.lblUserName.Size = new System.Drawing.Size(96, 18);
			this.lblUserName.TabIndex = 58;
			this.lblUserName.Text = "User Name";
			this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtServerName
			// 
			this.txtServerName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtServerName.Location = new System.Drawing.Point(217, 58);
			this.txtServerName.Margin = new System.Windows.Forms.Padding(2);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(240, 21);
			this.txtServerName.TabIndex = 3;
			// 
			// DatabaseConnection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(742, 489);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.panel4);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DatabaseConnection";
			this.Text = "DatabaseConnection";
			this.Load += new System.EventHandler(this.DatabaseConnection_Load);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.pnlSaveDelete.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lueDatabase.Properties)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rgType.Properties)).EndInit();
			this.pnlSqlAuthentication.ResumeLayout(false);
			this.pnlSqlAuthentication.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel pnlSaveDelete;
		private System.Windows.Forms.Label lblDatabase;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblConnection;
		private System.Windows.Forms.Label lblCurrentDatabase;
		private System.Windows.Forms.CheckBox chkSQLServerAuthentication;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Panel pnlSqlAuthentication;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Label lblUserName;
		private System.Windows.Forms.TextBox txtServerName;
		private DevExpress.XtraEditors.LookUpEdit lueDatabase;
		private System.Windows.Forms.RadioButton rbAIM;
		private DevExpress.XtraEditors.RadioGroup rgType;
		private System.Windows.Forms.RadioButton rbNir;
		private System.Windows.Forms.TextBox txtID;
	}
}