
namespace CFCAIMWorker
{
	partial class cmdTestPNEAPI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cmdTestPNEAPI));
            this.btnDatabaseConnection = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnTestPNEAPI = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnVilify = new System.Windows.Forms.Button();
            this.btnConcept5Settings = new System.Windows.Forms.Button();
            this.cmdSendSamplePNEAPI = new System.Windows.Forms.Button();
            this.txtSampleID = new System.Windows.Forms.TextBox();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDatabaseConnection
            // 
            this.btnDatabaseConnection.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatabaseConnection.Location = new System.Drawing.Point(249, 112);
            this.btnDatabaseConnection.Name = "btnDatabaseConnection";
            this.btnDatabaseConnection.Size = new System.Drawing.Size(281, 38);
            this.btnDatabaseConnection.TabIndex = 0;
            this.btnDatabaseConnection.Text = "Database Connection";
            this.btnDatabaseConnection.UseVisualStyleBackColor = true;
            this.btnDatabaseConnection.Click += new System.EventHandler(this.btnDatabaseConnection_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetting.Location = new System.Drawing.Point(249, 225);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(281, 34);
            this.btnSetting.TabIndex = 1;
            this.btnSetting.Text = "Settings";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(812, 52);
            this.panel4.TabIndex = 156;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(20, 5);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 37);
            this.btnExit.TabIndex = 150;
            this.btnExit.TabStop = false;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
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
            this.label10.Size = new System.Drawing.Size(222, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "CFC AIM Worker Setup";
            // 
            // btnTestPNEAPI
            // 
            this.btnTestPNEAPI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestPNEAPI.Location = new System.Drawing.Point(247, 365);
            this.btnTestPNEAPI.Name = "btnTestPNEAPI";
            this.btnTestPNEAPI.Size = new System.Drawing.Size(281, 34);
            this.btnTestPNEAPI.TabIndex = 157;
            this.btnTestPNEAPI.Text = "Test PNE API";
            this.btnTestPNEAPI.UseVisualStyleBackColor = true;
            this.btnTestPNEAPI.Click += new System.EventHandler(this.btnTestPNEAPI_Click);
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(310, 410);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(144, 20);
            this.txtCount.TabIndex = 158;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(310, 436);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(144, 20);
            this.txtName.TabIndex = 159;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(291, 65);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(47, 14);
            this.lblVersion.TabIndex = 160;
            this.lblVersion.Text = "Version";
            // 
            // btnVilify
            // 
            this.btnVilify.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVilify.Location = new System.Drawing.Point(249, 294);
            this.btnVilify.Name = "btnVilify";
            this.btnVilify.Size = new System.Drawing.Size(281, 34);
            this.btnVilify.TabIndex = 161;
            this.btnVilify.Text = "Verify API";
            this.btnVilify.UseVisualStyleBackColor = true;
            this.btnVilify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // btnConcept5Settings
            // 
            this.btnConcept5Settings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConcept5Settings.Location = new System.Drawing.Point(249, 187);
            this.btnConcept5Settings.Name = "btnConcept5Settings";
            this.btnConcept5Settings.Size = new System.Drawing.Size(281, 34);
            this.btnConcept5Settings.TabIndex = 162;
            this.btnConcept5Settings.Text = "Concept5 Settings";
            this.btnConcept5Settings.UseVisualStyleBackColor = true;
            this.btnConcept5Settings.Click += new System.EventHandler(this.btnConcept5Settings_Click);
            // 
            // cmdSendSamplePNEAPI
            // 
            this.cmdSendSamplePNEAPI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSendSamplePNEAPI.Location = new System.Drawing.Point(247, 473);
            this.cmdSendSamplePNEAPI.Name = "cmdSendSamplePNEAPI";
            this.cmdSendSamplePNEAPI.Size = new System.Drawing.Size(281, 34);
            this.cmdSendSamplePNEAPI.TabIndex = 163;
            this.cmdSendSamplePNEAPI.Text = "Send Sample PNE API";
            this.cmdSendSamplePNEAPI.UseVisualStyleBackColor = true;
            this.cmdSendSamplePNEAPI.Click += new System.EventHandler(this.cmdSendSamplePNEAPI_Click);
            // 
            // txtSampleID
            // 
            this.txtSampleID.Location = new System.Drawing.Point(310, 513);
            this.txtSampleID.Name = "txtSampleID";
            this.txtSampleID.Size = new System.Drawing.Size(144, 20);
            this.txtSampleID.TabIndex = 164;
            // 
            // cmdTestPNEAPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 585);
            this.Controls.Add(this.txtSampleID);
            this.Controls.Add(this.cmdSendSamplePNEAPI);
            this.Controls.Add(this.btnConcept5Settings);
            this.Controls.Add(this.btnVilify);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.btnTestPNEAPI);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnDatabaseConnection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "cmdTestPNEAPI";
            this.Text = "MainMenu";
            this.Load += new System.EventHandler(this.mainMenu_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnDatabaseConnection;
		private System.Windows.Forms.Button btnSetting;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnTestPNEAPI;
		private System.Windows.Forms.TextBox txtCount;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Button btnVilify;
		private System.Windows.Forms.Button btnConcept5Settings;
		private System.Windows.Forms.Button cmdSendSamplePNEAPI;
		private System.Windows.Forms.TextBox txtSampleID;
    }
}