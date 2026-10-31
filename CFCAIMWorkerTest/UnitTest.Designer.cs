namespace CFCAIMWorkerTest
{
    partial class UnitTest
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
            this.btnRun = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblBrukerSpectrumSentError = new System.Windows.Forms.Label();
            this.lblBrukerSpectrumReceiveError = new System.Windows.Forms.Label();
            this.lblBrukerSpectrumError = new System.Windows.Forms.Label();
            this.lblBrukerSentLogError = new System.Windows.Forms.Label();
            this.lblBrukerSampleResultSentError = new System.Windows.Forms.Label();
            this.lblBrukerSampleResultReceiveError = new System.Windows.Forms.Label();
            this.lblBrukerSampleResultError = new System.Windows.Forms.Label();
            this.lblBrukerSampleError = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRunFoss = new System.Windows.Forms.Button();
            this.btnRunCFCAPIPne = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(59, 44);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(111, 47);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run Bruker Test";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(56, 94);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(83, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Click run to start";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Bruker Sample";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(285, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Bruker Sample Result";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Bruker Sample Result Receive";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(285, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Bruker Sample Result Sent";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(285, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "BrukerSentLog";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(285, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Bruker Spectrum";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(285, 171);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Bruker Spectrum Receive";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(285, 194);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Bruker Spectrum Sent";
            // 
            // lblBrukerSpectrumSentError
            // 
            this.lblBrukerSpectrumSentError.AutoSize = true;
            this.lblBrukerSpectrumSentError.Location = new System.Drawing.Point(440, 194);
            this.lblBrukerSpectrumSentError.Name = "lblBrukerSpectrumSentError";
            this.lblBrukerSpectrumSentError.Size = new System.Drawing.Size(38, 13);
            this.lblBrukerSpectrumSentError.TabIndex = 19;
            this.lblBrukerSpectrumSentError.Text = "[result]";
            // 
            // lblBrukerSpectrumReceiveError
            // 
            this.lblBrukerSpectrumReceiveError.AutoSize = true;
            this.lblBrukerSpectrumReceiveError.Location = new System.Drawing.Point(440, 171);
            this.lblBrukerSpectrumReceiveError.Name = "lblBrukerSpectrumReceiveError";
            this.lblBrukerSpectrumReceiveError.Size = new System.Drawing.Size(38, 13);
            this.lblBrukerSpectrumReceiveError.TabIndex = 18;
            this.lblBrukerSpectrumReceiveError.Text = "[result]";
            // 
            // lblBrukerSpectrumError
            // 
            this.lblBrukerSpectrumError.AutoSize = true;
            this.lblBrukerSpectrumError.Location = new System.Drawing.Point(440, 150);
            this.lblBrukerSpectrumError.Name = "lblBrukerSpectrumError";
            this.lblBrukerSpectrumError.Size = new System.Drawing.Size(38, 13);
            this.lblBrukerSpectrumError.TabIndex = 17;
            this.lblBrukerSpectrumError.Text = "[result]";
            // 
            // lblBrukerSentLogError
            // 
            this.lblBrukerSentLogError.AutoSize = true;
            this.lblBrukerSentLogError.Location = new System.Drawing.Point(440, 130);
            this.lblBrukerSentLogError.Name = "lblBrukerSentLogError";
            this.lblBrukerSentLogError.Size = new System.Drawing.Size(38, 13);
            this.lblBrukerSentLogError.TabIndex = 16;
            this.lblBrukerSentLogError.Text = "[result]";
            // 
            // lblBrukerSampleResultSentError
            // 
            this.lblBrukerSampleResultSentError.AutoSize = true;
            this.lblBrukerSampleResultSentError.Location = new System.Drawing.Point(440, 110);
            this.lblBrukerSampleResultSentError.Name = "lblBrukerSampleResultSentError";
            this.lblBrukerSampleResultSentError.Size = new System.Drawing.Size(38, 13);
            this.lblBrukerSampleResultSentError.TabIndex = 15;
            this.lblBrukerSampleResultSentError.Text = "[result]";
            // 
            // lblBrukerSampleResultReceiveError
            // 
            this.lblBrukerSampleResultReceiveError.AutoSize = true;
            this.lblBrukerSampleResultReceiveError.Location = new System.Drawing.Point(440, 90);
            this.lblBrukerSampleResultReceiveError.Name = "lblBrukerSampleResultReceiveError";
            this.lblBrukerSampleResultReceiveError.Size = new System.Drawing.Size(38, 13);
            this.lblBrukerSampleResultReceiveError.TabIndex = 14;
            this.lblBrukerSampleResultReceiveError.Text = "[result]";
            // 
            // lblBrukerSampleResultError
            // 
            this.lblBrukerSampleResultError.AutoSize = true;
            this.lblBrukerSampleResultError.Location = new System.Drawing.Point(440, 68);
            this.lblBrukerSampleResultError.Name = "lblBrukerSampleResultError";
            this.lblBrukerSampleResultError.Size = new System.Drawing.Size(38, 13);
            this.lblBrukerSampleResultError.TabIndex = 13;
            this.lblBrukerSampleResultError.Text = "[result]";
            // 
            // lblBrukerSampleError
            // 
            this.lblBrukerSampleError.AutoSize = true;
            this.lblBrukerSampleError.Location = new System.Drawing.Point(440, 47);
            this.lblBrukerSampleError.Name = "lblBrukerSampleError";
            this.lblBrukerSampleError.Size = new System.Drawing.Size(38, 13);
            this.lblBrukerSampleError.TabIndex = 12;
            this.lblBrukerSampleError.Text = "[result]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(303, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Bruker Test Results:";
            // 
            // btnRunFoss
            // 
            this.btnRunFoss.Location = new System.Drawing.Point(59, 259);
            this.btnRunFoss.Name = "btnRunFoss";
            this.btnRunFoss.Size = new System.Drawing.Size(111, 47);
            this.btnRunFoss.TabIndex = 21;
            this.btnRunFoss.Text = "Run Foss Test";
            this.btnRunFoss.UseVisualStyleBackColor = true;
            this.btnRunFoss.Click += new System.EventHandler(this.btnRunFoss_Click);
            // 
            // btnRunCFCAPIPne
            // 
            this.btnRunCFCAPIPne.Location = new System.Drawing.Point(59, 110);
            this.btnRunCFCAPIPne.Name = "btnRunCFCAPIPne";
            this.btnRunCFCAPIPne.Size = new System.Drawing.Size(111, 47);
            this.btnRunCFCAPIPne.TabIndex = 22;
            this.btnRunCFCAPIPne.Text = "Run CFCAPIPne Test";
            this.btnRunCFCAPIPne.UseVisualStyleBackColor = true;
            this.btnRunCFCAPIPne.Click += new System.EventHandler(this.btnRunCFCAPIPne_Click);
            // 
            // UnitTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 407);
            this.Controls.Add(this.btnRunCFCAPIPne);
            this.Controls.Add(this.btnRunFoss);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBrukerSpectrumSentError);
            this.Controls.Add(this.lblBrukerSpectrumReceiveError);
            this.Controls.Add(this.lblBrukerSpectrumError);
            this.Controls.Add(this.lblBrukerSentLogError);
            this.Controls.Add(this.lblBrukerSampleResultSentError);
            this.Controls.Add(this.lblBrukerSampleResultReceiveError);
            this.Controls.Add(this.lblBrukerSampleResultError);
            this.Controls.Add(this.lblBrukerSampleError);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRun);
            this.Name = "UnitTest";
            this.Text = "UnitTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblBrukerSpectrumSentError;
        private System.Windows.Forms.Label lblBrukerSpectrumReceiveError;
        private System.Windows.Forms.Label lblBrukerSpectrumError;
        private System.Windows.Forms.Label lblBrukerSentLogError;
        private System.Windows.Forms.Label lblBrukerSampleResultSentError;
        private System.Windows.Forms.Label lblBrukerSampleResultReceiveError;
        private System.Windows.Forms.Label lblBrukerSampleResultError;
        private System.Windows.Forms.Label lblBrukerSampleError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRunFoss;
        private System.Windows.Forms.Button btnRunCFCAPIPne;
    }
}