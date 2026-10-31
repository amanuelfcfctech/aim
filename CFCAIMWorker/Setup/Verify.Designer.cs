
namespace CFCAIMWorker.Setup
{
	partial class Verify
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Verify));
			this.panel4 = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.btnExit = new System.Windows.Forms.Button();
			this.rbAIM = new System.Windows.Forms.RadioButton();
			this.rbNIR = new System.Windows.Forms.RadioButton();
			this.btnVerify = new System.Windows.Forms.Button();
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.rbPNE = new System.Windows.Forms.RadioButton();
			this.bgwVerify = new System.ComponentModel.BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlCount = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtLastCount = new System.Windows.Forms.TextBox();
			this.txtCurrentCount = new System.Windows.Forms.TextBox();
			this.btnFixMissingResult = new System.Windows.Forms.Button();
			this.panel4.SuspendLayout();
			this.pnlCount.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.BackColor = System.Drawing.Color.DarkBlue;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.label10);
			this.panel4.Controls.Add(this.btnExit);
			this.panel4.Location = new System.Drawing.Point(1, 1);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(798, 52);
			this.panel4.TabIndex = 156;
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
			this.label10.Size = new System.Drawing.Size(210, 23);
			this.label10.TabIndex = 149;
			this.label10.Text = "Database Connection";
			// 
			// btnExit
			// 
			this.btnExit.BackColor = System.Drawing.SystemColors.Control;
			this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(17, 5);
			this.btnExit.Margin = new System.Windows.Forms.Padding(2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(50, 37);
			this.btnExit.TabIndex = 64;
			this.btnExit.TabStop = false;
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// rbAIM
			// 
			this.rbAIM.AutoSize = true;
			this.rbAIM.Location = new System.Drawing.Point(387, 69);
			this.rbAIM.Name = "rbAIM";
			this.rbAIM.Size = new System.Drawing.Size(44, 17);
			this.rbAIM.TabIndex = 159;
			this.rbAIM.Text = "AIM";
			this.rbAIM.UseVisualStyleBackColor = true;
			this.rbAIM.CheckedChanged += new System.EventHandler(this.rbAIM_CheckedChanged);
			// 
			// rbNIR
			// 
			this.rbNIR.AutoSize = true;
			this.rbNIR.Checked = true;
			this.rbNIR.Location = new System.Drawing.Point(232, 69);
			this.rbNIR.Name = "rbNIR";
			this.rbNIR.Size = new System.Drawing.Size(44, 17);
			this.rbNIR.TabIndex = 158;
			this.rbNIR.TabStop = true;
			this.rbNIR.Text = "NIR";
			this.rbNIR.UseVisualStyleBackColor = true;
			this.rbNIR.CheckedChanged += new System.EventHandler(this.rbNIR_CheckedChanged);
			// 
			// btnVerify
			// 
			this.btnVerify.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnVerify.Location = new System.Drawing.Point(276, 102);
			this.btnVerify.Margin = new System.Windows.Forms.Padding(2);
			this.btnVerify.Name = "btnVerify";
			this.btnVerify.Size = new System.Drawing.Size(155, 32);
			this.btnVerify.TabIndex = 160;
			this.btnVerify.Text = "Verify API";
			this.btnVerify.UseVisualStyleBackColor = true;
			this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.Location = new System.Drawing.Point(20, 188);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtMessage.Size = new System.Drawing.Size(751, 237);
			this.txtMessage.TabIndex = 161;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// rbPNE
			// 
			this.rbPNE.AutoSize = true;
			this.rbPNE.Location = new System.Drawing.Point(519, 69);
			this.rbPNE.Name = "rbPNE";
			this.rbPNE.Size = new System.Drawing.Size(47, 17);
			this.rbPNE.TabIndex = 163;
			this.rbPNE.Text = "PNE";
			this.rbPNE.UseVisualStyleBackColor = true;
			this.rbPNE.CheckedChanged += new System.EventHandler(this.rbPNE_CheckedChanged);
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(457, 102);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(155, 32);
			this.btnCancel.TabIndex = 166;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Visible = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// pnlCount
			// 
			this.pnlCount.Controls.Add(this.label2);
			this.pnlCount.Controls.Add(this.label1);
			this.pnlCount.Controls.Add(this.txtLastCount);
			this.pnlCount.Controls.Add(this.txtCurrentCount);
			this.pnlCount.Location = new System.Drawing.Point(118, 150);
			this.pnlCount.Name = "pnlCount";
			this.pnlCount.Size = new System.Drawing.Size(578, 31);
			this.pnlCount.TabIndex = 167;
			this.pnlCount.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(336, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 13);
			this.label2.TabIndex = 168;
			this.label2.Text = "Total Count";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(111, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 13);
			this.label1.TabIndex = 167;
			this.label1.Text = "Current Count";
			// 
			// txtLastCount
			// 
			this.txtLastCount.Location = new System.Drawing.Point(401, 5);
			this.txtLastCount.Name = "txtLastCount";
			this.txtLastCount.Size = new System.Drawing.Size(100, 20);
			this.txtLastCount.TabIndex = 166;
			// 
			// txtCurrentCount
			// 
			this.txtCurrentCount.Location = new System.Drawing.Point(193, 5);
			this.txtCurrentCount.Name = "txtCurrentCount";
			this.txtCurrentCount.Size = new System.Drawing.Size(100, 20);
			this.txtCurrentCount.TabIndex = 165;
			// 
			// btnFixMissingResult
			// 
			this.btnFixMissingResult.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFixMissingResult.Location = new System.Drawing.Point(95, 102);
			this.btnFixMissingResult.Margin = new System.Windows.Forms.Padding(2);
			this.btnFixMissingResult.Name = "btnFixMissingResult";
			this.btnFixMissingResult.Size = new System.Drawing.Size(155, 32);
			this.btnFixMissingResult.TabIndex = 168;
			this.btnFixMissingResult.Text = "Fix Missing Result";
			this.btnFixMissingResult.UseVisualStyleBackColor = true;
			this.btnFixMissingResult.Visible = false;
			this.btnFixMissingResult.Click += new System.EventHandler(this.btnFixMissingResult_Click);
			// 
			// Verify
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.btnFixMissingResult);
			this.Controls.Add(this.pnlCount);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.rbPNE);
			this.Controls.Add(this.txtMessage);
			this.Controls.Add(this.btnVerify);
			this.Controls.Add(this.rbAIM);
			this.Controls.Add(this.rbNIR);
			this.Controls.Add(this.panel4);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Verify";
			this.Text = "Vilify";
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.pnlCount.ResumeLayout(false);
			this.pnlCount.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.RadioButton rbAIM;
		private System.Windows.Forms.RadioButton rbNIR;
		private System.Windows.Forms.Button btnVerify;
		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.RadioButton rbPNE;
		private System.ComponentModel.BackgroundWorker bgwVerify;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Panel pnlCount;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtLastCount;
		private System.Windows.Forms.TextBox txtCurrentCount;
		private System.Windows.Forms.Button btnFixMissingResult;
	}
}