
namespace CFCAIMWorker
{
	partial class Setting
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
			this.panel4 = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.btnExit = new System.Windows.Forms.Button();
			this.lblCurrentDatabase = new System.Windows.Forms.Label();
			this.txtAIMInstrumentKey = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtAIMAPIKey = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtAPIUrlTest = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtAPIUrl = new System.Windows.Forms.TextBox();
			this.chkDebugOn = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnFossErrorPath = new System.Windows.Forms.Button();
			this.btnFossArchivePath = new System.Windows.Forms.Button();
			this.btnFossNIRPath = new System.Windows.Forms.Button();
			this.btnFossCSVPath = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.txtFossErrorPath = new System.Windows.Forms.TextBox();
			this.txtFossArchivePath = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtFossNIRPath = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtFossCSVPath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.btnSave = new System.Windows.Forms.Button();
			this.chkTestUrlOn = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtID = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkTestPNEOn = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtPNEAPITestUrl = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.txtPNEAPIUrl = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.panel4.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
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
			this.panel4.Location = new System.Drawing.Point(0, -2);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(802, 53);
			this.panel4.TabIndex = 156;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.label10.ForeColor = System.Drawing.Color.White;
			this.label10.Location = new System.Drawing.Point(345, 10);
			this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(87, 23);
			this.label10.TabIndex = 149;
			this.label10.Text = "Settings";
			// 
			// btnExit
			// 
			this.btnExit.BackColor = System.Drawing.SystemColors.Control;
			this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(18, 5);
			this.btnExit.Margin = new System.Windows.Forms.Padding(2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(50, 37);
			this.btnExit.TabIndex = 20;
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// lblCurrentDatabase
			// 
			this.lblCurrentDatabase.BackColor = System.Drawing.Color.Transparent;
			this.lblCurrentDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCurrentDatabase.Location = new System.Drawing.Point(118, 65);
			this.lblCurrentDatabase.Name = "lblCurrentDatabase";
			this.lblCurrentDatabase.Size = new System.Drawing.Size(96, 18);
			this.lblCurrentDatabase.TabIndex = 158;
			this.lblCurrentDatabase.Text = "Debug";
			this.lblCurrentDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtAIMInstrumentKey
			// 
			this.txtAIMInstrumentKey.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAIMInstrumentKey.Location = new System.Drawing.Point(227, 238);
			this.txtAIMInstrumentKey.Margin = new System.Windows.Forms.Padding(2);
			this.txtAIMInstrumentKey.Name = "txtAIMInstrumentKey";
			this.txtAIMInstrumentKey.Size = new System.Drawing.Size(240, 21);
			this.txtAIMInstrumentKey.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(56, 238);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(158, 18);
			this.label2.TabIndex = 162;
			this.label2.Text = "AIM Instrument Key";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(67, 203);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(147, 18);
			this.label3.TabIndex = 164;
			this.label3.Text = "AIM API Key";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtAIMAPIKey
			// 
			this.txtAIMAPIKey.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAIMAPIKey.Location = new System.Drawing.Point(227, 203);
			this.txtAIMAPIKey.Margin = new System.Windows.Forms.Padding(2);
			this.txtAIMAPIKey.Name = "txtAIMAPIKey";
			this.txtAIMAPIKey.Size = new System.Drawing.Size(240, 21);
			this.txtAIMAPIKey.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(53, 168);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(161, 18);
			this.label4.TabIndex = 166;
			this.label4.Text = "API Url Test";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtAPIUrlTest
			// 
			this.txtAPIUrlTest.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAPIUrlTest.Location = new System.Drawing.Point(227, 168);
			this.txtAPIUrlTest.Margin = new System.Windows.Forms.Padding(2);
			this.txtAPIUrlTest.Name = "txtAPIUrlTest";
			this.txtAPIUrlTest.Size = new System.Drawing.Size(354, 21);
			this.txtAPIUrlTest.TabIndex = 4;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(50, 138);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(164, 18);
			this.label5.TabIndex = 168;
			this.label5.Text = "API Url Production";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtAPIUrl
			// 
			this.txtAPIUrl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAPIUrl.Location = new System.Drawing.Point(227, 138);
			this.txtAPIUrl.Margin = new System.Windows.Forms.Padding(2);
			this.txtAPIUrl.Name = "txtAPIUrl";
			this.txtAPIUrl.Size = new System.Drawing.Size(354, 21);
			this.txtAPIUrl.TabIndex = 3;
			// 
			// chkDebugOn
			// 
			this.chkDebugOn.AutoSize = true;
			this.chkDebugOn.Location = new System.Drawing.Point(227, 67);
			this.chkDebugOn.Name = "chkDebugOn";
			this.chkDebugOn.Size = new System.Drawing.Size(40, 17);
			this.chkDebugOn.TabIndex = 1;
			this.chkDebugOn.Text = "On";
			this.chkDebugOn.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnFossErrorPath);
			this.groupBox1.Controls.Add(this.btnFossArchivePath);
			this.groupBox1.Controls.Add(this.btnFossNIRPath);
			this.groupBox1.Controls.Add(this.btnFossCSVPath);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.txtFossErrorPath);
			this.groupBox1.Controls.Add(this.txtFossArchivePath);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.txtFossNIRPath);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.txtFossCSVPath);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(17, 439);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(757, 218);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Foss Setting";
			// 
			// btnFossErrorPath
			// 
			this.btnFossErrorPath.Location = new System.Drawing.Point(469, 149);
			this.btnFossErrorPath.Name = "btnFossErrorPath";
			this.btnFossErrorPath.Size = new System.Drawing.Size(95, 20);
			this.btnFossErrorPath.TabIndex = 14;
			this.btnFossErrorPath.Text = "Browse";
			this.btnFossErrorPath.UseVisualStyleBackColor = true;
			this.btnFossErrorPath.Click += new System.EventHandler(this.btnFossErrorPath_Click);
			// 
			// btnFossArchivePath
			// 
			this.btnFossArchivePath.Location = new System.Drawing.Point(469, 107);
			this.btnFossArchivePath.Name = "btnFossArchivePath";
			this.btnFossArchivePath.Size = new System.Drawing.Size(95, 20);
			this.btnFossArchivePath.TabIndex = 13;
			this.btnFossArchivePath.Text = "Browse";
			this.btnFossArchivePath.UseVisualStyleBackColor = true;
			this.btnFossArchivePath.Click += new System.EventHandler(this.btnFossArchivePath_Click);
			// 
			// btnFossNIRPath
			// 
			this.btnFossNIRPath.Location = new System.Drawing.Point(469, 72);
			this.btnFossNIRPath.Name = "btnFossNIRPath";
			this.btnFossNIRPath.Size = new System.Drawing.Size(95, 20);
			this.btnFossNIRPath.TabIndex = 12;
			this.btnFossNIRPath.Text = "Browse";
			this.btnFossNIRPath.UseVisualStyleBackColor = true;
			this.btnFossNIRPath.Click += new System.EventHandler(this.btnFossNIRPath_Click);
			// 
			// btnFossCSVPath
			// 
			this.btnFossCSVPath.Location = new System.Drawing.Point(469, 36);
			this.btnFossCSVPath.Name = "btnFossCSVPath";
			this.btnFossCSVPath.Size = new System.Drawing.Size(95, 20);
			this.btnFossCSVPath.TabIndex = 11;
			this.btnFossCSVPath.Text = "Browse";
			this.btnFossCSVPath.UseVisualStyleBackColor = true;
			this.btnFossCSVPath.Click += new System.EventHandler(this.btnFossCSVPath_Click);
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(101, 148);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(96, 18);
			this.label8.TabIndex = 168;
			this.label8.Text = "Error Path";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtFossErrorPath
			// 
			this.txtFossErrorPath.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFossErrorPath.Location = new System.Drawing.Point(210, 148);
			this.txtFossErrorPath.Margin = new System.Windows.Forms.Padding(2);
			this.txtFossErrorPath.Name = "txtFossErrorPath";
			this.txtFossErrorPath.Size = new System.Drawing.Size(240, 21);
			this.txtFossErrorPath.TabIndex = 10;
			// 
			// txtFossArchivePath
			// 
			this.txtFossArchivePath.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFossArchivePath.Location = new System.Drawing.Point(210, 106);
			this.txtFossArchivePath.Margin = new System.Windows.Forms.Padding(2);
			this.txtFossArchivePath.Name = "txtFossArchivePath";
			this.txtFossArchivePath.Size = new System.Drawing.Size(240, 21);
			this.txtFossArchivePath.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(101, 106);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 18);
			this.label7.TabIndex = 165;
			this.label7.Text = "Archive Path";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtFossNIRPath
			// 
			this.txtFossNIRPath.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFossNIRPath.Location = new System.Drawing.Point(210, 70);
			this.txtFossNIRPath.Margin = new System.Windows.Forms.Padding(2);
			this.txtFossNIRPath.Name = "txtFossNIRPath";
			this.txtFossNIRPath.Size = new System.Drawing.Size(240, 21);
			this.txtFossNIRPath.TabIndex = 8;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(101, 73);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 18);
			this.label6.TabIndex = 163;
			this.label6.Text = "NIR Path";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtFossCSVPath
			// 
			this.txtFossCSVPath.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFossCSVPath.Location = new System.Drawing.Point(210, 35);
			this.txtFossCSVPath.Margin = new System.Windows.Forms.Padding(2);
			this.txtFossCSVPath.Name = "txtFossCSVPath";
			this.txtFossCSVPath.Size = new System.Drawing.Size(240, 21);
			this.txtFossCSVPath.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(101, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 18);
			this.label1.TabIndex = 161;
			this.label1.Text = "CSV Path";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnSave
			// 
			this.btnSave.BackColor = System.Drawing.SystemColors.Control;
			this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Image = global::CFCAIMWorker.Properties.Resources.save_32x32;
			this.btnSave.Location = new System.Drawing.Point(639, 79);
			this.btnSave.Margin = new System.Windows.Forms.Padding(2);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(50, 37);
			this.btnSave.TabIndex = 19;
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// chkTestUrlOn
			// 
			this.chkTestUrlOn.AutoSize = true;
			this.chkTestUrlOn.Location = new System.Drawing.Point(227, 100);
			this.chkTestUrlOn.Name = "chkTestUrlOn";
			this.chkTestUrlOn.Size = new System.Drawing.Size(40, 17);
			this.chkTestUrlOn.TabIndex = 2;
			this.chkTestUrlOn.Text = "On";
			this.chkTestUrlOn.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(118, 98);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(96, 18);
			this.label9.TabIndex = 172;
			this.label9.Text = "Test Url";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtID
			// 
			this.txtID.Location = new System.Drawing.Point(639, 136);
			this.txtID.Name = "txtID";
			this.txtID.Size = new System.Drawing.Size(96, 20);
			this.txtID.TabIndex = 174;
			this.txtID.Visible = false;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkTestPNEOn);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.txtPNEAPITestUrl);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.txtPNEAPIUrl);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Location = new System.Drawing.Point(20, 284);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(757, 126);
			this.groupBox2.TabIndex = 175;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "PNE API Setting";
			// 
			// chkTestPNEOn
			// 
			this.chkTestPNEOn.AutoSize = true;
			this.chkTestPNEOn.Location = new System.Drawing.Point(210, 15);
			this.chkTestPNEOn.Name = "chkTestPNEOn";
			this.chkTestPNEOn.Size = new System.Drawing.Size(40, 17);
			this.chkTestPNEOn.TabIndex = 173;
			this.chkTestPNEOn.Text = "On";
			this.chkTestPNEOn.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.BackColor = System.Drawing.Color.Transparent;
			this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(101, 13);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(96, 18);
			this.label11.TabIndex = 174;
			this.label11.Text = "Test Url";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtPNEAPITestUrl
			// 
			this.txtPNEAPITestUrl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPNEAPITestUrl.Location = new System.Drawing.Point(210, 82);
			this.txtPNEAPITestUrl.Margin = new System.Windows.Forms.Padding(2);
			this.txtPNEAPITestUrl.Name = "txtPNEAPITestUrl";
			this.txtPNEAPITestUrl.Size = new System.Drawing.Size(399, 21);
			this.txtPNEAPITestUrl.TabIndex = 8;
			// 
			// label13
			// 
			this.label13.BackColor = System.Drawing.Color.Transparent;
			this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(75, 83);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(122, 18);
			this.label13.TabIndex = 163;
			this.label13.Text = "PNE API Url Test";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtPNEAPIUrl
			// 
			this.txtPNEAPIUrl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPNEAPIUrl.Location = new System.Drawing.Point(210, 45);
			this.txtPNEAPIUrl.Margin = new System.Windows.Forms.Padding(2);
			this.txtPNEAPIUrl.Name = "txtPNEAPIUrl";
			this.txtPNEAPIUrl.Size = new System.Drawing.Size(399, 21);
			this.txtPNEAPIUrl.TabIndex = 7;
			// 
			// label14
			// 
			this.label14.BackColor = System.Drawing.Color.Transparent;
			this.label14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(50, 44);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(147, 18);
			this.label14.TabIndex = 161;
			this.label14.Text = "PNE API Url Production";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Setting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 669);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.txtID);
			this.Controls.Add(this.chkTestUrlOn);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.chkDebugOn);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtAPIUrl);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtAPIUrlTest);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtAIMAPIKey);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtAIMInstrumentKey);
			this.Controls.Add(this.lblCurrentDatabase);
			this.Controls.Add(this.panel4);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Setting";
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.Setting_Load);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lblCurrentDatabase;
		private System.Windows.Forms.TextBox txtAIMInstrumentKey;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtAIMAPIKey;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtAPIUrlTest;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtAPIUrl;
		private System.Windows.Forms.CheckBox chkDebugOn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtFossCSVPath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtFossErrorPath;
		private System.Windows.Forms.TextBox txtFossArchivePath;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtFossNIRPath;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnFossNIRPath;
		private System.Windows.Forms.Button btnFossCSVPath;
		private System.Windows.Forms.Button btnFossErrorPath;
		private System.Windows.Forms.Button btnFossArchivePath;
		private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.CheckBox chkTestUrlOn;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkTestPNEOn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPNEAPITestUrl;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPNEAPIUrl;
        private System.Windows.Forms.Label label14;
    }
}