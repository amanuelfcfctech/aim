namespace CFCAIM
{
	partial class SampleAIMSync
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleAIMSync));
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnRunUpdate = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.btnResize = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.pbRebuild = new System.Windows.Forms.ProgressBar();
			this.bgw_Rebuild = new System.ComponentModel.BackgroundWorker();
			this.bgw_ProgressBar = new System.ComponentModel.BackgroundWorker();
			this.txtCounter = new System.Windows.Forms.TextBox();
			this.bgw_Equations = new System.ComponentModel.BackgroundWorker();
			this.bgw_Equations_ProgressBar = new System.ComponentModel.BackgroundWorker();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnRebuildHelp = new System.Windows.Forms.Button();
			this.deFromDate = new DevExpress.XtraEditors.DateEdit();
			this.chkAll = new System.Windows.Forms.CheckBox();
			this.chkDate = new System.Windows.Forms.CheckBox();
			this.pnlTopBtns.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Location = new System.Drawing.Point(2, 5);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(54, 37);
			this.pnlTopBtns.TabIndex = 30;
			// 
			// btnExit
			// 
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(0, 0);
			this.btnExit.Margin = new System.Windows.Forms.Padding(2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(54, 37);
			this.btnExit.TabIndex = 26;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnRunUpdate
			// 
			this.btnRunUpdate.BackColor = System.Drawing.SystemColors.Control;
			this.btnRunUpdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRunUpdate.Location = new System.Drawing.Point(44, 150);
			this.btnRunUpdate.Margin = new System.Windows.Forms.Padding(2);
			this.btnRunUpdate.Name = "btnRunUpdate";
			this.btnRunUpdate.Size = new System.Drawing.Size(131, 33);
			this.btnRunUpdate.TabIndex = 31;
			this.btnRunUpdate.Text = "Rebuild Sample AIM";
			this.btnRunUpdate.UseVisualStyleBackColor = false;
			this.btnRunUpdate.Click += new System.EventHandler(this.btnRunUpdate_Click);
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.BackColor = System.Drawing.Color.DarkBlue;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.btnResize);
			this.panel4.Controls.Add(this.label7);
			this.panel4.Controls.Add(this.pnlTopBtns);
			this.panel4.Location = new System.Drawing.Point(1, 1);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(669, 51);
			this.panel4.TabIndex = 152;
			// 
			// btnResize
			// 
			this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
			this.btnResize.Location = new System.Drawing.Point(631, -1);
			this.btnResize.Margin = new System.Windows.Forms.Padding(2);
			this.btnResize.Name = "btnResize";
			this.btnResize.Size = new System.Drawing.Size(34, 34);
			this.btnResize.TabIndex = 151;
			this.btnResize.UseVisualStyleBackColor = true;
			this.btnResize.Visible = false;
			this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.label7.ForeColor = System.Drawing.Color.White;
			this.label7.Location = new System.Drawing.Point(236, 14);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(251, 23);
			this.label7.TabIndex = 149;
			this.label7.Text = "Rebuild SampleAIM Table";
			// 
			// pbRebuild
			// 
			this.pbRebuild.Location = new System.Drawing.Point(380, 102);
			this.pbRebuild.Name = "pbRebuild";
			this.pbRebuild.Size = new System.Drawing.Size(260, 21);
			this.pbRebuild.Step = 1;
			this.pbRebuild.TabIndex = 157;
			this.pbRebuild.Visible = false;
			// 
			// bgw_Rebuild
			// 
			this.bgw_Rebuild.WorkerReportsProgress = true;
			this.bgw_Rebuild.WorkerSupportsCancellation = true;
			this.bgw_Rebuild.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRebuild_DoWork);
			this.bgw_Rebuild.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwRebuild_RunWorkerCompleted);
			// 
			// bgw_ProgressBar
			// 
			this.bgw_ProgressBar.WorkerReportsProgress = true;
			this.bgw_ProgressBar.WorkerSupportsCancellation = true;
			this.bgw_ProgressBar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwProgressBar_DoWork);
			this.bgw_ProgressBar.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwProgressBar_ProgressChanged);
			this.bgw_ProgressBar.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwProgressBar_RunWorkerCompleted);
			// 
			// txtCounter
			// 
			this.txtCounter.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCounter.Location = new System.Drawing.Point(380, 79);
			this.txtCounter.Margin = new System.Windows.Forms.Padding(2);
			this.txtCounter.Name = "txtCounter";
			this.txtCounter.ReadOnly = true;
			this.txtCounter.Size = new System.Drawing.Size(260, 18);
			this.txtCounter.TabIndex = 158;
			this.txtCounter.Visible = false;
			// 
			// bgw_Equations
			// 
			this.bgw_Equations.WorkerReportsProgress = true;
			this.bgw_Equations.WorkerSupportsCancellation = true;
			this.bgw_Equations.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_Equations_DoWork);
			this.bgw_Equations.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_Equations_RunWorkerCompleted);
			// 
			// bgw_Equations_ProgressBar
			// 
			this.bgw_Equations_ProgressBar.WorkerReportsProgress = true;
			this.bgw_Equations_ProgressBar.WorkerSupportsCancellation = true;
			this.bgw_Equations_ProgressBar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_Equations_ProgressBar_DoWork);
			this.bgw_Equations_ProgressBar.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgw_Equations_ProgressBar_ProgressChanged);
			this.bgw_Equations_ProgressBar.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_Equations_ProgressBar_RunWorkerCompleted);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.btnRebuildHelp);
			this.panel1.Controls.Add(this.deFromDate);
			this.panel1.Controls.Add(this.chkAll);
			this.panel1.Controls.Add(this.chkDate);
			this.panel1.Location = new System.Drawing.Point(30, 79);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(271, 66);
			this.panel1.TabIndex = 159;
			// 
			// btnRebuildHelp
			// 
			this.btnRebuildHelp.Image = global::CFCAIM.Properties.Resources.question_16x16;
			this.btnRebuildHelp.Location = new System.Drawing.Point(234, 5);
			this.btnRebuildHelp.Margin = new System.Windows.Forms.Padding(2);
			this.btnRebuildHelp.Name = "btnRebuildHelp";
			this.btnRebuildHelp.Size = new System.Drawing.Size(26, 23);
			this.btnRebuildHelp.TabIndex = 166;
			this.btnRebuildHelp.UseVisualStyleBackColor = true;
			this.btnRebuildHelp.Click += new System.EventHandler(this.btnRebuildHelp_Click);
			// 
			// deFromDate
			// 
			this.deFromDate.EditValue = new System.DateTime(((long)(0)));
			this.deFromDate.Location = new System.Drawing.Point(112, 6);
			this.deFromDate.Name = "deFromDate";
			this.deFromDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.deFromDate.Properties.Appearance.Options.UseFont = true;
			this.deFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.deFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.deFromDate.Properties.NullDate = new System.DateTime(((long)(0)));
			this.deFromDate.Size = new System.Drawing.Size(104, 22);
			this.deFromDate.TabIndex = 3;
			this.deFromDate.Visible = false;
			// 
			// chkAll
			// 
			this.chkAll.AutoSize = true;
			this.chkAll.Location = new System.Drawing.Point(12, 38);
			this.chkAll.Name = "chkAll";
			this.chkAll.Size = new System.Drawing.Size(76, 17);
			this.chkAll.TabIndex = 1;
			this.chkAll.Text = "Rebuild All";
			this.chkAll.UseVisualStyleBackColor = true;
			this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
			// 
			// chkDate
			// 
			this.chkDate.AutoSize = true;
			this.chkDate.Location = new System.Drawing.Point(12, 9);
			this.chkDate.Name = "chkDate";
			this.chkDate.Size = new System.Drawing.Size(94, 17);
			this.chkDate.TabIndex = 0;
			this.chkDate.Text = "Rebuild From: ";
			this.chkDate.UseVisualStyleBackColor = true;
			this.chkDate.CheckedChanged += new System.EventHandler(this.chkDate_CheckedChanged);
			// 
			// SampleAIMSync
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(669, 317);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.txtCounter);
			this.Controls.Add(this.pbRebuild);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.btnRunUpdate);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SampleAIMSync";
			this.Text = "Rebuild SampleAIM Table";
			this.Load += new System.EventHandler(this.SampleAIMSync_Load);
			this.SizeChanged += new System.EventHandler(this.SampleAIMSync_SizeChanged);
			this.pnlTopBtns.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRunUpdate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.ProgressBar pbRebuild;
        private System.ComponentModel.BackgroundWorker bgw_Rebuild;
        private System.ComponentModel.BackgroundWorker bgw_ProgressBar;
        private System.Windows.Forms.TextBox txtCounter;
        private System.ComponentModel.BackgroundWorker bgw_Equations;
        private System.ComponentModel.BackgroundWorker bgw_Equations_ProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkDate;
        private DevExpress.XtraEditors.DateEdit deFromDate;
        private System.Windows.Forms.Button btnRebuildHelp;
    }
}