namespace CFCLicensing
{
    partial class AuthorizationCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizationCode));
            this.fraExtendOrConvert = new System.Windows.Forms.GroupBox();
            this.txtInstallCode = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtActivationCode = new System.Windows.Forms.TextBox();
            this.lblActivationCode = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblLicensedInformation = new System.Windows.Forms.Label();
            this.lblLicensedVersion = new System.Windows.Forms.Label();
            this.fraExtendOrConvert.SuspendLayout();
            this.SuspendLayout();
            // 
            // fraExtendOrConvert
            // 
            this.fraExtendOrConvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(240)))), ((int)(((byte)(223)))));
            this.fraExtendOrConvert.Controls.Add(this.lblLicensedInformation);
            this.fraExtendOrConvert.Controls.Add(this.lblLicensedVersion);
            this.fraExtendOrConvert.Controls.Add(this.txtInstallCode);
            this.fraExtendOrConvert.Controls.Add(this.btnOk);
            this.fraExtendOrConvert.Controls.Add(this.txtCompanyName);
            this.fraExtendOrConvert.Controls.Add(this.lblCompanyName);
            this.fraExtendOrConvert.Controls.Add(this.btnExit);
            this.fraExtendOrConvert.Controls.Add(this.btnSubmit);
            this.fraExtendOrConvert.Controls.Add(this.txtActivationCode);
            this.fraExtendOrConvert.Controls.Add(this.lblActivationCode);
            this.fraExtendOrConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fraExtendOrConvert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.fraExtendOrConvert.Location = new System.Drawing.Point(3, 32);
            this.fraExtendOrConvert.Margin = new System.Windows.Forms.Padding(2);
            this.fraExtendOrConvert.Name = "fraExtendOrConvert";
            this.fraExtendOrConvert.Padding = new System.Windows.Forms.Padding(2);
            this.fraExtendOrConvert.Size = new System.Drawing.Size(681, 169);
            this.fraExtendOrConvert.TabIndex = 13;
            this.fraExtendOrConvert.TabStop = false;
            // 
            // txtInstallCode
            // 
            this.txtInstallCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(240)))), ((int)(((byte)(223)))));
            this.txtInstallCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInstallCode.Location = new System.Drawing.Point(3, 85);
            this.txtInstallCode.Name = "txtInstallCode";
            this.txtInstallCode.ReadOnly = true;
            this.txtInstallCode.Size = new System.Drawing.Size(678, 15);
            this.txtInstallCode.TabIndex = 18;
            this.txtInstallCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInstallCode.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(220)))), ((int)(((byte)(197)))));
            this.btnOk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOk.Image = global::CFCLicensing.Properties.Resources.check_mark;
            this.btnOk.Location = new System.Drawing.Point(536, 16);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(52, 37);
            this.btnOk.TabIndex = 17;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.BackColor = System.Drawing.SystemColors.Window;
            this.txtCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCompanyName.Location = new System.Drawing.Point(240, 18);
            this.txtCompanyName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(290, 23);
            this.txtCompanyName.TabIndex = 15;
            this.txtCompanyName.Visible = false;
            this.txtCompanyName.Leave += new System.EventHandler(this.txtCompanyName_Leave);
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.BackColor = System.Drawing.Color.Transparent;
            this.lblCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.Black;
            this.lblCompanyName.Location = new System.Drawing.Point(99, 17);
            this.lblCompanyName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(134, 19);
            this.lblCompanyName.TabIndex = 14;
            this.lblCompanyName.Text = "Company Name:";
            this.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblCompanyName.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExit.Image = global::CFCLicensing.Properties.Resources.exit;
            this.btnExit.Location = new System.Drawing.Point(472, 116);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(52, 37);
            this.btnExit.TabIndex = 12;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(220)))), ((int)(((byte)(197)))));
            this.btnSubmit.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSubmit.Image = global::CFCLicensing.Properties.Resources.check_mark;
            this.btnSubmit.Location = new System.Drawing.Point(411, 116);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(52, 37);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtActivationCode
            // 
            this.txtActivationCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtActivationCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActivationCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtActivationCode.Location = new System.Drawing.Point(240, 118);
            this.txtActivationCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtActivationCode.Name = "txtActivationCode";
            this.txtActivationCode.Size = new System.Drawing.Size(168, 23);
            this.txtActivationCode.TabIndex = 10;
            this.txtActivationCode.Visible = false;
            // 
            // lblActivationCode
            // 
            this.lblActivationCode.BackColor = System.Drawing.Color.Transparent;
            this.lblActivationCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActivationCode.ForeColor = System.Drawing.Color.Black;
            this.lblActivationCode.Location = new System.Drawing.Point(99, 122);
            this.lblActivationCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblActivationCode.Name = "lblActivationCode";
            this.lblActivationCode.Size = new System.Drawing.Size(134, 17);
            this.lblActivationCode.TabIndex = 9;
            this.lblActivationCode.Text = "Activation Code:";
            this.lblActivationCode.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblActivationCode.Visible = false;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMessage.Location = new System.Drawing.Point(2, 7);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(679, 22);
            this.lblMessage.TabIndex = 14;
            this.lblMessage.Text = "Please enter a company name.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblLicensedInformation
            // 
            this.lblLicensedInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblLicensedInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicensedInformation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLicensedInformation.Location = new System.Drawing.Point(6, 64);
            this.lblLicensedInformation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLicensedInformation.Name = "lblLicensedInformation";
            this.lblLicensedInformation.Size = new System.Drawing.Size(675, 21);
            this.lblLicensedInformation.TabIndex = 16;
            this.lblLicensedInformation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblLicensedInformation.Visible = false;
            // 
            // lblLicensedVersion
            // 
            this.lblLicensedVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblLicensedVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicensedVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLicensedVersion.Location = new System.Drawing.Point(8, 16);
            this.lblLicensedVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLicensedVersion.Name = "lblLicensedVersion";
            this.lblLicensedVersion.Size = new System.Drawing.Size(670, 46);
            this.lblLicensedVersion.TabIndex = 15;
            this.lblLicensedVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblLicensedVersion.Visible = false;
            // 
            // AuthorizationCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(240)))), ((int)(((byte)(223)))));
            this.ClientSize = new System.Drawing.Size(688, 207);
            this.ControlBox = false;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.fraExtendOrConvert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthorizationCode";
            this.Text = "Authorization Code";
            this.Load += new System.EventHandler(this.AuthorizationCode_Load);
            this.fraExtendOrConvert.ResumeLayout(false);
            this.fraExtendOrConvert.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox fraExtendOrConvert;
        public System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Button btnSubmit;
        public System.Windows.Forms.TextBox txtActivationCode;
        public System.Windows.Forms.Label lblActivationCode;
        public System.Windows.Forms.Label lblMessage;
		public System.Windows.Forms.TextBox txtCompanyName;
        public System.Windows.Forms.Label lblCompanyName;
		private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtInstallCode;
        public System.Windows.Forms.Label lblLicensedInformation;
        public System.Windows.Forms.Label lblLicensedVersion;
    }
}