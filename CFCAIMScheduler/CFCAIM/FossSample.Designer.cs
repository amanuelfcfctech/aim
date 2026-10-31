namespace CFCAIM.Foss
{
	partial class FossSample
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FossSample));
			this.lblFossSpetrum = new System.Windows.Forms.Label();
			this.lblFossResult = new System.Windows.Forms.Label();
			this.txtFossSpetrum = new System.Windows.Forms.TextBox();
			this.txtFossResult = new System.Windows.Forms.TextBox();
			this.lblSampleName = new System.Windows.Forms.Label();
			this.btnProcess = new System.Windows.Forms.Button();
			this.txtSampleNumber = new System.Windows.Forms.TextBox();
			this.txtMaterial = new System.Windows.Forms.TextBox();
			this.lblMaterial = new System.Windows.Forms.Label();
			this.bgwProcessData = new System.ComponentModel.BackgroundWorker();
			this.prgProgress = new System.Windows.Forms.ProgressBar();
			this.bgwTimer = new System.ComponentModel.BackgroundWorker();
			this.label1 = new System.Windows.Forms.Label();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnProcessNIR = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblFossSpetrum
			// 
			this.lblFossSpetrum.AutoSize = true;
			this.lblFossSpetrum.Location = new System.Drawing.Point(310, 340);
			this.lblFossSpetrum.Name = "lblFossSpetrum";
			this.lblFossSpetrum.Size = new System.Drawing.Size(102, 17);
			this.lblFossSpetrum.TabIndex = 13;
			this.lblFossSpetrum.Text = "Foss Spectrum";
			// 
			// lblFossResult
			// 
			this.lblFossResult.AutoSize = true;
			this.lblFossResult.Location = new System.Drawing.Point(329, 308);
			this.lblFossResult.Name = "lblFossResult";
			this.lblFossResult.Size = new System.Drawing.Size(82, 17);
			this.lblFossResult.TabIndex = 12;
			this.lblFossResult.Text = "Foss Result";
			// 
			// txtFossSpetrum
			// 
			this.txtFossSpetrum.Location = new System.Drawing.Point(419, 336);
			this.txtFossSpetrum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtFossSpetrum.Name = "txtFossSpetrum";
			this.txtFossSpetrum.Size = new System.Drawing.Size(340, 22);
			this.txtFossSpetrum.TabIndex = 11;
			this.txtFossSpetrum.TabStop = false;
			this.txtFossSpetrum.Text = "c:\\Foss";
			// 
			// txtFossResult
			// 
			this.txtFossResult.Location = new System.Drawing.Point(419, 306);
			this.txtFossResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtFossResult.Name = "txtFossResult";
			this.txtFossResult.Size = new System.Drawing.Size(340, 22);
			this.txtFossResult.TabIndex = 10;
			this.txtFossResult.TabStop = false;
			this.txtFossResult.Text = "c:\\Foss";
			// 
			// lblSampleName
			// 
			this.lblSampleName.AutoSize = true;
			this.lblSampleName.Location = new System.Drawing.Point(301, 76);
			this.lblSampleName.Name = "lblSampleName";
			this.lblSampleName.Size = new System.Drawing.Size(109, 17);
			this.lblSampleName.TabIndex = 9;
			this.lblSampleName.Text = "Sample Number";
			// 
			// btnProcess
			// 
			this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnProcess.ForeColor = System.Drawing.Color.ForestGreen;
			this.btnProcess.Location = new System.Drawing.Point(437, 170);
			this.btnProcess.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(212, 50);
			this.btnProcess.TabIndex = 8;
			this.btnProcess.Text = "Process";
			this.btnProcess.UseVisualStyleBackColor = true;
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// txtSampleNumber
			// 
			this.txtSampleNumber.Location = new System.Drawing.Point(417, 74);
			this.txtSampleNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtSampleNumber.Name = "txtSampleNumber";
			this.txtSampleNumber.Size = new System.Drawing.Size(239, 22);
			this.txtSampleNumber.TabIndex = 7;
			this.txtSampleNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSampleNumber_KeyDown);
			this.txtSampleNumber.Validated += new System.EventHandler(this.txtSampleNumber_Validated);
			// 
			// txtMaterial
			// 
			this.txtMaterial.Location = new System.Drawing.Point(419, 108);
			this.txtMaterial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtMaterial.Name = "txtMaterial";
			this.txtMaterial.Size = new System.Drawing.Size(239, 22);
			this.txtMaterial.TabIndex = 14;
			this.txtMaterial.TabStop = false;
			// 
			// lblMaterial
			// 
			this.lblMaterial.AutoSize = true;
			this.lblMaterial.Location = new System.Drawing.Point(349, 110);
			this.lblMaterial.Name = "lblMaterial";
			this.lblMaterial.Size = new System.Drawing.Size(58, 17);
			this.lblMaterial.TabIndex = 15;
			this.lblMaterial.Text = "Material";
			// 
			// bgwProcessData
			// 
			this.bgwProcessData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwProcessData_DoWork);
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
			this.prgProgress.TabIndex = 16;
			// 
			// bgwTimer
			// 
			this.bgwTimer.WorkerReportsProgress = true;
			this.bgwTimer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTimer_DoWork);
			this.bgwTimer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwTimer_ProgressChanged);
			this.bgwTimer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwTimer_RunWorkerCompleted);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(778, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(116, 17);
			this.label1.TabIndex = 17;
			this.label1.Text = "Version 15.01.19";
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(8, 534);
			this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(68, 50);
			this.btnStart.TabIndex = 18;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(78, 534);
			this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(80, 50);
			this.btnStop.TabIndex = 19;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnProcessNIR
			// 
			this.btnProcessNIR.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnProcessNIR.ForeColor = System.Drawing.Color.ForestGreen;
			this.btnProcessNIR.Location = new System.Drawing.Point(100, 170);
			this.btnProcessNIR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnProcessNIR.Name = "btnProcessNIR";
			this.btnProcessNIR.Size = new System.Drawing.Size(212, 50);
			this.btnProcessNIR.TabIndex = 20;
			this.btnProcessNIR.Text = "Process NIR";
			this.btnProcessNIR.UseVisualStyleBackColor = true;
			this.btnProcessNIR.Click += new System.EventHandler(this.btnProcessNIR_Click);
			// 
			// FossSample
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(1047, 590);
			this.Controls.Add(this.btnProcessNIR);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.prgProgress);
			this.Controls.Add(this.lblMaterial);
			this.Controls.Add(this.txtMaterial);
			this.Controls.Add(this.lblFossSpetrum);
			this.Controls.Add(this.lblFossResult);
			this.Controls.Add(this.txtFossSpetrum);
			this.Controls.Add(this.txtFossResult);
			this.Controls.Add(this.lblSampleName);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.txtSampleNumber);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "FossSample";
			this.Text = "Foss Sample";
			this.Load += new System.EventHandler(this.FossSample_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblFossSpetrum;
		private System.Windows.Forms.Label lblFossResult;
		private System.Windows.Forms.TextBox txtFossSpetrum;
		private System.Windows.Forms.TextBox txtFossResult;
		private System.Windows.Forms.Label lblSampleName;
		private System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.TextBox txtSampleNumber;
		private System.Windows.Forms.TextBox txtMaterial;
		private System.Windows.Forms.Label lblMaterial;
		private System.ComponentModel.BackgroundWorker bgwProcessData;
        private System.Windows.Forms.ProgressBar prgProgress;
		private System.ComponentModel.BackgroundWorker bgwTimer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnProcessNIR;
	}
}