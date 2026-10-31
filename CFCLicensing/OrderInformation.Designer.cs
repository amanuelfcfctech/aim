namespace CFCLicensing
{
	partial class OrderInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderInformation));
            this.Picture1 = new System.Windows.Forms.PictureBox();
            this.lblLicensedInformation = new System.Windows.Forms.Label();
            this.lblLicensedVersion = new System.Windows.Forms.Label();
            this.cmdOrderOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).BeginInit();
            this.Picture1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Picture1
            // 
            this.Picture1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.Picture1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picture1.Controls.Add(this.lblLicensedInformation);
            this.Picture1.Controls.Add(this.lblLicensedVersion);
            this.Picture1.Location = new System.Drawing.Point(6, 6);
            this.Picture1.Margin = new System.Windows.Forms.Padding(2);
            this.Picture1.Name = "Picture1";
            this.Picture1.Size = new System.Drawing.Size(409, 228);
            this.Picture1.TabIndex = 1;
            this.Picture1.TabStop = false;
            // 
            // lblLicensedInformation
            // 
            this.lblLicensedInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblLicensedInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicensedInformation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLicensedInformation.Location = new System.Drawing.Point(4, 146);
            this.lblLicensedInformation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLicensedInformation.Name = "lblLicensedInformation";
            this.lblLicensedInformation.Size = new System.Drawing.Size(398, 55);
            this.lblLicensedInformation.TabIndex = 3;
            this.lblLicensedInformation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblLicensedVersion
            // 
            this.lblLicensedVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblLicensedVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicensedVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLicensedVersion.Location = new System.Drawing.Point(6, 8);
            this.lblLicensedVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLicensedVersion.Name = "lblLicensedVersion";
            this.lblLicensedVersion.Size = new System.Drawing.Size(393, 80);
            this.lblLicensedVersion.TabIndex = 1;
            this.lblLicensedVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmdOrderOK
            // 
            this.cmdOrderOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(240)))), ((int)(((byte)(223)))));
            this.cmdOrderOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOrderOK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdOrderOK.Image = global::CFCLicensing.Properties.Resources.check_mark;
            this.cmdOrderOK.Location = new System.Drawing.Point(180, 104);
            this.cmdOrderOK.Margin = new System.Windows.Forms.Padding(2);
            this.cmdOrderOK.Name = "cmdOrderOK";
            this.cmdOrderOK.Size = new System.Drawing.Size(52, 37);
            this.cmdOrderOK.TabIndex = 2;
            this.cmdOrderOK.UseVisualStyleBackColor = false;
            this.cmdOrderOK.Click += new System.EventHandler(this.cmdOrderOK_Click);
            // 
            // OrderInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(421, 241);
            this.Controls.Add(this.cmdOrderOK);
            this.Controls.Add(this.Picture1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "OrderInformation";
            this.Text = "OrderInformation";
            this.Load += new System.EventHandler(this.OrderInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).EndInit();
            this.Picture1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.PictureBox Picture1;
		public System.Windows.Forms.Button cmdOrderOK;
		public System.Windows.Forms.Label lblLicensedInformation;
		public System.Windows.Forms.Label lblLicensedVersion;

	}
}