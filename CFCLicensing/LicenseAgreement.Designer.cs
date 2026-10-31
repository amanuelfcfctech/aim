namespace CFCLicensing
{
	partial class LicenseAgreement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseAgreement));
            this.lblFooter = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdDoNotAccept = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFooter
            // 
            this.lblFooter.BackColor = System.Drawing.SystemColors.Control;
            this.lblFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFooter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFooter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFooter.Location = new System.Drawing.Point(0, 549);
            this.lblFooter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(776, 43);
            this.lblFooter.TabIndex = 6;
            this.lblFooter.Text = resources.GetString("lblFooter.Text");
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.SystemColors.Control;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(776, 69);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = resources.GetString("lblHeader.Text");
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 75;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "CFCFerBL.Reports.LicenseAgreement.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(11, 79);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(750, 407);
            this.reportViewer1.TabIndex = 6;
            // 
            // cmdPrint
            // 
            this.cmdPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdPrint.Image = global::CFCLicensing.Properties.Resources.printer;
            this.cmdPrint.Location = new System.Drawing.Point(590, 498);
            this.cmdPrint.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(52, 37);
            this.cmdPrint.TabIndex = 4;
            this.cmdPrint.UseVisualStyleBackColor = false;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdDoNotAccept
            // 
            this.cmdDoNotAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdDoNotAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDoNotAccept.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDoNotAccept.Image = global::CFCLicensing.Properties.Resources.DeleteBig;
            this.cmdDoNotAccept.Location = new System.Drawing.Point(347, 498);
            this.cmdDoNotAccept.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmdDoNotAccept.Name = "cmdDoNotAccept";
            this.cmdDoNotAccept.Size = new System.Drawing.Size(52, 37);
            this.cmdDoNotAccept.TabIndex = 2;
            this.cmdDoNotAccept.UseVisualStyleBackColor = false;
            this.cmdDoNotAccept.Click += new System.EventHandler(this.cmdDoNotAccept_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmdAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAccept.Image = global::CFCLicensing.Properties.Resources.check_mark;
            this.cmdAccept.Location = new System.Drawing.Point(95, 498);
            this.cmdAccept.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(52, 37);
            this.cmdAccept.TabIndex = 1;
            this.cmdAccept.UseVisualStyleBackColor = false;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // LicenseAgreement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 592);
            this.ControlBox = false;
            this.Controls.Add(this.lblFooter);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.cmdDoNotAccept);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.cmdAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimizeBox = false;
            this.Name = "LicenseAgreement";
            this.ShowIcon = false;
            this.Text = "SOFTWARE LICENSE AGREEMENT";
            this.Load += new System.EventHandler(this.LicenseAgreement_Load);
            this.ResumeLayout(false);

		}

		#endregion

        public System.Windows.Forms.Button cmdPrint;
		public System.Windows.Forms.Button cmdDoNotAccept;
		public System.Windows.Forms.Button cmdAccept;
		public System.Windows.Forms.Label lblFooter;
		public System.Windows.Forms.Label lblHeader;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
	}
}