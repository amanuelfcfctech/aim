namespace CFCAIM
{
    partial class DeveloperWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeveloperWindow));
			this.txtCode = new System.Windows.Forms.TextBox();
			this.btnEnter = new System.Windows.Forms.Button();
			this.tcDevTools = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.btnSwitchDatabase = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cboDatabase = new System.Windows.Forms.ComboBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.lblDatabaseName = new System.Windows.Forms.Label();
			this.tcDevTools.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtCode
			// 
			this.txtCode.Location = new System.Drawing.Point(0, 0);
			this.txtCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtCode.Name = "txtCode";
			this.txtCode.PasswordChar = '+';
			this.txtCode.Size = new System.Drawing.Size(189, 22);
			this.txtCode.TabIndex = 0;
			// 
			// btnEnter
			// 
			this.btnEnter.Location = new System.Drawing.Point(199, 0);
			this.btnEnter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnEnter.Name = "btnEnter";
			this.btnEnter.Size = new System.Drawing.Size(100, 28);
			this.btnEnter.TabIndex = 1;
			this.btnEnter.Text = "enter";
			this.btnEnter.UseVisualStyleBackColor = true;
			this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
			// 
			// tcDevTools
			// 
			this.tcDevTools.Controls.Add(this.tabPage1);
			this.tcDevTools.Controls.Add(this.tabPage2);
			this.tcDevTools.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tcDevTools.Enabled = false;
			this.tcDevTools.Location = new System.Drawing.Point(0, 36);
			this.tcDevTools.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tcDevTools.Name = "tcDevTools";
			this.tcDevTools.SelectedIndex = 0;
			this.tcDevTools.Size = new System.Drawing.Size(509, 250);
			this.tcDevTools.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.btnSwitchDatabase);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.cboDatabase);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage1.Size = new System.Drawing.Size(501, 221);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Switch Database";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// btnSwitchDatabase
			// 
			this.btnSwitchDatabase.Location = new System.Drawing.Point(144, 110);
			this.btnSwitchDatabase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnSwitchDatabase.Name = "btnSwitchDatabase";
			this.btnSwitchDatabase.Size = new System.Drawing.Size(344, 28);
			this.btnSwitchDatabase.TabIndex = 2;
			this.btnSwitchDatabase.Text = "Select (Click once and wait)";
			this.btnSwitchDatabase.UseVisualStyleBackColor = true;
			this.btnSwitchDatabase.Click += new System.EventHandler(this.btnSwitchDatabase_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 52);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select Database:";
			// 
			// cboDatabase
			// 
			this.cboDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDatabase.FormattingEnabled = true;
			this.cboDatabase.Items.AddRange(new object[] {
            "CFC Tech",
            "Farm Service Elevator",
            "Nikki",
            "New"});
			this.cboDatabase.Location = new System.Drawing.Point(144, 48);
			this.cboDatabase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboDatabase.Name = "cboDatabase";
			this.cboDatabase.Size = new System.Drawing.Size(343, 24);
			this.cboDatabase.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage2.Size = new System.Drawing.Size(501, 221);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// lblDatabaseName
			// 
			this.lblDatabaseName.AutoSize = true;
			this.lblDatabaseName.Location = new System.Drawing.Point(307, 6);
			this.lblDatabaseName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDatabaseName.Name = "lblDatabaseName";
			this.lblDatabaseName.Size = new System.Drawing.Size(66, 17);
			this.lblDatabaseName.TabIndex = 3;
			this.lblDatabaseName.Text = "loading...";
			this.lblDatabaseName.Visible = false;
			// 
			// DeveloperWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(509, 286);
			this.Controls.Add(this.lblDatabaseName);
			this.Controls.Add(this.tcDevTools);
			this.Controls.Add(this.btnEnter);
			this.Controls.Add(this.txtCode);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "DeveloperWindow";
			this.Text = "DeveloperWindow";
			this.Load += new System.EventHandler(this.DeveloperWindow_Load);
			this.tcDevTools.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.TabControl tcDevTools;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSwitchDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDatabase;
        private System.Windows.Forms.Label lblDatabaseName;
    }
}