namespace CFCLicensing
{
	partial class EvaluationNotice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EvaluationNotice));
            this.cmdConvert = new System.Windows.Forms.Button();
            this.cmdOrderInformation = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.Line1 = new System.Windows.Forms.Label();
            this.lblDaysRemaining = new System.Windows.Forms.Label();
            this.lblSoftWarning = new System.Windows.Forms.Label();
            this.lblEvaluationPeriod = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblProgramName = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdConvert
            // 
            this.cmdConvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(220)))), ((int)(((byte)(197)))));
            this.cmdConvert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdConvert.Location = new System.Drawing.Point(271, 288);
            this.cmdConvert.Margin = new System.Windows.Forms.Padding(2);
            this.cmdConvert.Name = "cmdConvert";
            this.cmdConvert.Size = new System.Drawing.Size(162, 39);
            this.cmdConvert.TabIndex = 21;
            this.cmdConvert.Text = "Get Licence or Extend";
            this.cmdConvert.UseVisualStyleBackColor = false;
            this.cmdConvert.Click += new System.EventHandler(this.cmdConvert_Click);
            // 
            // cmdOrderInformation
            // 
            this.cmdOrderInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(220)))), ((int)(((byte)(197)))));
            this.cmdOrderInformation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdOrderInformation.Location = new System.Drawing.Point(42, 288);
            this.cmdOrderInformation.Margin = new System.Windows.Forms.Padding(2);
            this.cmdOrderInformation.Name = "cmdOrderInformation";
            this.cmdOrderInformation.Size = new System.Drawing.Size(162, 36);
            this.cmdOrderInformation.TabIndex = 18;
            this.cmdOrderInformation.Text = "Contact Us";
            this.cmdOrderInformation.UseVisualStyleBackColor = false;
            this.cmdOrderInformation.Click += new System.EventHandler(this.cmdOrderInformation_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(220)))), ((int)(((byte)(197)))));
            this.cmdAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdAccept.Image = global::CFCLicensing.Properties.Resources.check_mark;
            this.cmdAccept.Location = new System.Drawing.Point(197, 173);
            this.cmdAccept.Margin = new System.Windows.Forms.Padding(2);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(52, 37);
            this.cmdAccept.TabIndex = 14;
            this.cmdAccept.UseVisualStyleBackColor = false;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // Line1
            // 
            this.Line1.BackColor = System.Drawing.SystemColors.WindowText;
            this.Line1.Location = new System.Drawing.Point(8, 212);
            this.Line1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Line1.Name = "Line1";
            this.Line1.Size = new System.Drawing.Size(478, 2);
            this.Line1.TabIndex = 23;
            // 
            // lblDaysRemaining
            // 
            this.lblDaysRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblDaysRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaysRemaining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(120)))), ((int)(((byte)(117)))));
            this.lblDaysRemaining.Location = new System.Drawing.Point(2, 62);
            this.lblDaysRemaining.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDaysRemaining.Name = "lblDaysRemaining";
            this.lblDaysRemaining.Size = new System.Drawing.Size(490, 18);
            this.lblDaysRemaining.TabIndex = 24;
            this.lblDaysRemaining.Text = "99 Days Remaining in Evaluation Period";
            this.lblDaysRemaining.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSoftWarning
            // 
            this.lblSoftWarning.BackColor = System.Drawing.Color.Transparent;
            this.lblSoftWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSoftWarning.Location = new System.Drawing.Point(10, 225);
            this.lblSoftWarning.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSoftWarning.Name = "lblSoftWarning";
            this.lblSoftWarning.Size = new System.Drawing.Size(475, 58);
            this.lblSoftWarning.TabIndex = 19;
            this.lblSoftWarning.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblEvaluationPeriod
            // 
            this.lblEvaluationPeriod.BackColor = System.Drawing.Color.Transparent;
            this.lblEvaluationPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblEvaluationPeriod.Location = new System.Drawing.Point(40, 98);
            this.lblEvaluationPeriod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEvaluationPeriod.Name = "lblEvaluationPeriod";
            this.lblEvaluationPeriod.Size = new System.Drawing.Size(399, 63);
            this.lblEvaluationPeriod.TabIndex = 17;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(120)))), ((int)(((byte)(117)))));
            this.Label2.Location = new System.Drawing.Point(3, 41);
            this.Label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(489, 15);
            this.Label2.TabIndex = 16;
            this.Label2.Text = "This is a Fully Functional Copy for EVALUATION USE ONLY.";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProgramName
            // 
            this.lblProgramName.BackColor = System.Drawing.Color.Transparent;
            this.lblProgramName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(120)))), ((int)(((byte)(117)))));
            this.lblProgramName.Location = new System.Drawing.Point(15, 2);
            this.lblProgramName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProgramName.Name = "lblProgramName";
            this.lblProgramName.Size = new System.Drawing.Size(458, 34);
            this.lblProgramName.TabIndex = 15;
            this.lblProgramName.Text = "CFC Tech System";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(220)))), ((int)(((byte)(197)))));
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExit.Image = global::CFCLicensing.Properties.Resources.exit;
            this.btnExit.Location = new System.Drawing.Point(253, 173);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(52, 37);
            this.btnExit.TabIndex = 25;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // EvaluationNotice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 334);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cmdConvert);
            this.Controls.Add(this.cmdOrderInformation);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.Line1);
            this.Controls.Add(this.lblDaysRemaining);
            this.Controls.Add(this.lblSoftWarning);
            this.Controls.Add(this.lblEvaluationPeriod);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.lblProgramName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EvaluationNotice";
            this.ShowIcon = false;
            this.Text = "Evaluation Notice";
            this.Activated += new System.EventHandler(this.EvaluationNotice_Activated);
            this.Load += new System.EventHandler(this.EvaluationNotice_Load);
            this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Button cmdConvert;
		public System.Windows.Forms.Button cmdOrderInformation;
		public System.Windows.Forms.Button cmdAccept;
		public System.Windows.Forms.Label Line1;
		public System.Windows.Forms.Label lblDaysRemaining;
		public System.Windows.Forms.Label lblSoftWarning;
		public System.Windows.Forms.Label lblEvaluationPeriod;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label lblProgramName;
        public System.Windows.Forms.Button btnExit;
	}
}