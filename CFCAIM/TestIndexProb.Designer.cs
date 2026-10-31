namespace CFCAIM
{
	partial class TestIndexProb
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
			this.lueSampleNumber = new DevExpress.XtraEditors.LookUpEdit();
			this.btnLast = new System.Windows.Forms.Button();
			this.btnFirst = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnPrevious = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.lueSampleNumber.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// lueSampleNumber
			// 
			this.lueSampleNumber.Location = new System.Drawing.Point(292, 100);
			this.lueSampleNumber.Name = "lueSampleNumber";
			this.lueSampleNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lueSampleNumber.Size = new System.Drawing.Size(229, 22);
			this.lueSampleNumber.TabIndex = 0;
			// 
			// btnLast
			// 
			this.btnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnLast.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLast.Image = global::CFCAIM.Properties.Resources.last_32x32;
			this.btnLast.Location = new System.Drawing.Point(357, 172);
			this.btnLast.Margin = new System.Windows.Forms.Padding(0);
			this.btnLast.Name = "btnLast";
			this.btnLast.Size = new System.Drawing.Size(29, 30);
			this.btnLast.TabIndex = 52;
			this.btnLast.TabStop = false;
			this.btnLast.UseVisualStyleBackColor = true;
			this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
			// 
			// btnFirst
			// 
			this.btnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnFirst.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFirst.Image = global::CFCAIM.Properties.Resources.first_32x32;
			this.btnFirst.Location = new System.Drawing.Point(270, 172);
			this.btnFirst.Margin = new System.Windows.Forms.Padding(0);
			this.btnFirst.Name = "btnFirst";
			this.btnFirst.Size = new System.Drawing.Size(29, 30);
			this.btnFirst.TabIndex = 55;
			this.btnFirst.TabStop = false;
			this.btnFirst.UseVisualStyleBackColor = true;
			this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
			// 
			// btnNext
			// 
			this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnNext.Image = global::CFCAIM.Properties.Resources.next_32x32;
			this.btnNext.Location = new System.Drawing.Point(328, 172);
			this.btnNext.Margin = new System.Windows.Forms.Padding(0);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(29, 30);
			this.btnNext.TabIndex = 53;
			this.btnNext.TabStop = false;
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnPrevious
			// 
			this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnPrevious.Image = global::CFCAIM.Properties.Resources.prev_32x32;
			this.btnPrevious.Location = new System.Drawing.Point(299, 172);
			this.btnPrevious.Margin = new System.Windows.Forms.Padding(0);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(29, 30);
			this.btnPrevious.TabIndex = 54;
			this.btnPrevious.TabStop = false;
			this.btnPrevious.UseVisualStyleBackColor = true;
			this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
			// 
			// TestIndexProb
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(627, 374);
			this.Controls.Add(this.btnFirst);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnPrevious);
			this.Controls.Add(this.btnLast);
			this.Controls.Add(this.lueSampleNumber);
			this.Name = "TestIndexProb";
			this.Text = "TestIndexProb";
			this.Load += new System.EventHandler(this.TestIndexProb_Load);
			((System.ComponentModel.ISupportInitialize)(this.lueSampleNumber.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.LookUpEdit lueSampleNumber;
		private System.Windows.Forms.Button btnLast;
		private System.Windows.Forms.Button btnFirst;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnPrevious;
	}
}