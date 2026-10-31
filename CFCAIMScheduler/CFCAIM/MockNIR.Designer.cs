namespace CFCAIM.Maintenance
{
	partial class MockNIR
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
			this.label1 = new System.Windows.Forms.Label();
			this.lblMaterial = new System.Windows.Forms.Label();
			this.txtMaterial = new System.Windows.Forms.TextBox();
			this.lblFossSpetrum = new System.Windows.Forms.Label();
			this.lblFossResult = new System.Windows.Forms.Label();
			this.txtFossSpetrum = new System.Windows.Forms.TextBox();
			this.txtFossResult = new System.Windows.Forms.TextBox();
			this.lblSampleName = new System.Windows.Forms.Label();
			this.btnProcess = new System.Windows.Forms.Button();
			this.txtSampleNumber = new System.Windows.Forms.TextBox();
			this.bgwTimer = new System.ComponentModel.BackgroundWorker();
			this.prgProgress = new System.Windows.Forms.ProgressBar();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btnRestoreDatabase = new System.Windows.Forms.Button();
			this.txtDatabaseName = new System.Windows.Forms.TextBox();
			this.txtBackupPath = new System.Windows.Forms.TextBox();
			this.txtMachineName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(704, 124);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 17);
			this.label1.TabIndex = 27;
			this.label1.Text = "Version 16.01.21";
			// 
			// lblMaterial
			// 
			this.lblMaterial.AutoSize = true;
			this.lblMaterial.Location = new System.Drawing.Point(275, 170);
			this.lblMaterial.Name = "lblMaterial";
			this.lblMaterial.Size = new System.Drawing.Size(58, 17);
			this.lblMaterial.TabIndex = 26;
			this.lblMaterial.Text = "Material";
			// 
			// txtMaterial
			// 
			this.txtMaterial.Location = new System.Drawing.Point(345, 168);
			this.txtMaterial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtMaterial.Name = "txtMaterial";
			this.txtMaterial.Size = new System.Drawing.Size(239, 22);
			this.txtMaterial.TabIndex = 25;
			this.txtMaterial.TabStop = false;
			// 
			// lblFossSpetrum
			// 
			this.lblFossSpetrum.AutoSize = true;
			this.lblFossSpetrum.Location = new System.Drawing.Point(236, 348);
			this.lblFossSpetrum.Name = "lblFossSpetrum";
			this.lblFossSpetrum.Size = new System.Drawing.Size(102, 17);
			this.lblFossSpetrum.TabIndex = 24;
			this.lblFossSpetrum.Text = "Foss Spectrum";
			// 
			// lblFossResult
			// 
			this.lblFossResult.AutoSize = true;
			this.lblFossResult.Location = new System.Drawing.Point(255, 316);
			this.lblFossResult.Name = "lblFossResult";
			this.lblFossResult.Size = new System.Drawing.Size(82, 17);
			this.lblFossResult.TabIndex = 23;
			this.lblFossResult.Text = "Foss Result";
			// 
			// txtFossSpetrum
			// 
			this.txtFossSpetrum.Location = new System.Drawing.Point(345, 344);
			this.txtFossSpetrum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtFossSpetrum.Name = "txtFossSpetrum";
			this.txtFossSpetrum.ReadOnly = true;
			this.txtFossSpetrum.Size = new System.Drawing.Size(340, 22);
			this.txtFossSpetrum.TabIndex = 22;
			this.txtFossSpetrum.TabStop = false;
			this.txtFossSpetrum.Text = "C:\\AimDemo\\Result";
			// 
			// txtFossResult
			// 
			this.txtFossResult.Location = new System.Drawing.Point(345, 314);
			this.txtFossResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtFossResult.Name = "txtFossResult";
			this.txtFossResult.ReadOnly = true;
			this.txtFossResult.Size = new System.Drawing.Size(340, 22);
			this.txtFossResult.TabIndex = 21;
			this.txtFossResult.TabStop = false;
			this.txtFossResult.Text = "C:\\AimDemo\\Result";
			// 
			// lblSampleName
			// 
			this.lblSampleName.AutoSize = true;
			this.lblSampleName.Location = new System.Drawing.Point(227, 136);
			this.lblSampleName.Name = "lblSampleName";
			this.lblSampleName.Size = new System.Drawing.Size(109, 17);
			this.lblSampleName.TabIndex = 20;
			this.lblSampleName.Text = "Sample Number";
			// 
			// btnProcess
			// 
			this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnProcess.ForeColor = System.Drawing.Color.ForestGreen;
			this.btnProcess.Location = new System.Drawing.Point(363, 230);
			this.btnProcess.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(212, 50);
			this.btnProcess.TabIndex = 19;
			this.btnProcess.Text = "Process";
			this.btnProcess.UseVisualStyleBackColor = true;
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// txtSampleNumber
			// 
			this.txtSampleNumber.Location = new System.Drawing.Point(343, 134);
			this.txtSampleNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtSampleNumber.Name = "txtSampleNumber";
			this.txtSampleNumber.Size = new System.Drawing.Size(239, 22);
			this.txtSampleNumber.TabIndex = 18;
			this.txtSampleNumber.Validated += new System.EventHandler(this.txtSampleNumber_Validated);
			// 
			// bgwTimer
			// 
			this.bgwTimer.WorkerReportsProgress = true;
			this.bgwTimer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTimer_DoWork);
			this.bgwTimer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwTimer_ProgressChanged);
			this.bgwTimer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwTimer_RunWorkerCompleted);
			// 
			// prgProgress
			// 
			this.prgProgress.Dock = System.Windows.Forms.DockStyle.Top;
			this.prgProgress.Location = new System.Drawing.Point(0, 0);
			this.prgProgress.Margin = new System.Windows.Forms.Padding(4);
			this.prgProgress.MarqueeAnimationSpeed = 200;
			this.prgProgress.Maximum = 4;
			this.prgProgress.Name = "prgProgress";
			this.prgProgress.Size = new System.Drawing.Size(1047, 28);
			this.prgProgress.TabIndex = 28;
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(82, 482);
			this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(80, 50);
			this.btnStop.TabIndex = 30;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(12, 482);
			this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(68, 50);
			this.btnStart.TabIndex = 29;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(31, 454);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(111, 17);
			this.label2.TabIndex = 31;
			this.label2.Text = "CFC AIM Worker";
			// 
			// btnRestoreDatabase
			// 
			this.btnRestoreDatabase.Location = new System.Drawing.Point(869, 316);
			this.btnRestoreDatabase.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnRestoreDatabase.Name = "btnRestoreDatabase";
			this.btnRestoreDatabase.Size = new System.Drawing.Size(102, 50);
			this.btnRestoreDatabase.TabIndex = 32;
			this.btnRestoreDatabase.Text = "Restore Database";
			this.btnRestoreDatabase.UseVisualStyleBackColor = true;
			this.btnRestoreDatabase.Click += new System.EventHandler(this.btnRestoreDatabase_Click);
			// 
			// txtDatabaseName
			// 
			this.txtDatabaseName.Location = new System.Drawing.Point(807, 407);
			this.txtDatabaseName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtDatabaseName.Name = "txtDatabaseName";
			this.txtDatabaseName.ReadOnly = true;
			this.txtDatabaseName.Size = new System.Drawing.Size(225, 22);
			this.txtDatabaseName.TabIndex = 33;
			this.txtDatabaseName.TabStop = false;
			// 
			// txtBackupPath
			// 
			this.txtBackupPath.Location = new System.Drawing.Point(807, 433);
			this.txtBackupPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtBackupPath.Name = "txtBackupPath";
			this.txtBackupPath.ReadOnly = true;
			this.txtBackupPath.Size = new System.Drawing.Size(225, 22);
			this.txtBackupPath.TabIndex = 34;
			this.txtBackupPath.TabStop = false;
			// 
			// txtMachineName
			// 
			this.txtMachineName.Location = new System.Drawing.Point(807, 459);
			this.txtMachineName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtMachineName.Name = "txtMachineName";
			this.txtMachineName.ReadOnly = true;
			this.txtMachineName.Size = new System.Drawing.Size(225, 22);
			this.txtMachineName.TabIndex = 35;
			this.txtMachineName.TabStop = false;
			// 
			// MockNIR
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1047, 543);
			this.Controls.Add(this.txtMachineName);
			this.Controls.Add(this.txtBackupPath);
			this.Controls.Add(this.txtDatabaseName);
			this.Controls.Add(this.btnRestoreDatabase);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.prgProgress);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblMaterial);
			this.Controls.Add(this.txtMaterial);
			this.Controls.Add(this.lblFossSpetrum);
			this.Controls.Add(this.lblFossResult);
			this.Controls.Add(this.txtFossSpetrum);
			this.Controls.Add(this.txtFossResult);
			this.Controls.Add(this.lblSampleName);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.txtSampleNumber);
			this.Name = "MockNIR";
			this.Text = "Mock NIR";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblMaterial;
		private System.Windows.Forms.TextBox txtMaterial;
		private System.Windows.Forms.Label lblFossSpetrum;
		private System.Windows.Forms.Label lblFossResult;
		private System.Windows.Forms.TextBox txtFossSpetrum;
		private System.Windows.Forms.TextBox txtFossResult;
		private System.Windows.Forms.Label lblSampleName;
		private System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.TextBox txtSampleNumber;
		private System.ComponentModel.BackgroundWorker bgwTimer;
		private System.Windows.Forms.ProgressBar prgProgress;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnRestoreDatabase;
		private System.Windows.Forms.TextBox txtDatabaseName;
		private System.Windows.Forms.TextBox txtBackupPath;
		private System.Windows.Forms.TextBox txtMachineName;
	}
}