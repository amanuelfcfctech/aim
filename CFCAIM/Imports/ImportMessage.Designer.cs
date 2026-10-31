namespace CFCAIM.Imports
{
	partial class ImportMessage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportMessage));
			this.richMessage1 = new System.Windows.Forms.RichTextBox();
			this.richMessage2 = new System.Windows.Forms.RichTextBox();
			this.richMessage3 = new System.Windows.Forms.RichTextBox();
			this.richMessage4 = new System.Windows.Forms.RichTextBox();
			this.richMessage5 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richMessage1
			// 
			this.richMessage1.BackColor = System.Drawing.SystemColors.HighlightText;
			this.richMessage1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richMessage1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richMessage1.Location = new System.Drawing.Point(12, 12);
			this.richMessage1.Name = "richMessage1";
			this.richMessage1.ReadOnly = true;
			this.richMessage1.Size = new System.Drawing.Size(449, 68);
			this.richMessage1.TabIndex = 3;
			this.richMessage1.Text = "";
			// 
			// richMessage2
			// 
			this.richMessage2.BackColor = System.Drawing.SystemColors.HighlightText;
			this.richMessage2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richMessage2.Font = new System.Drawing.Font("Arial", 9F);
			this.richMessage2.Location = new System.Drawing.Point(12, 81);
			this.richMessage2.Name = "richMessage2";
			this.richMessage2.ReadOnly = true;
			this.richMessage2.Size = new System.Drawing.Size(449, 68);
			this.richMessage2.TabIndex = 4;
			this.richMessage2.Text = "";
			// 
			// richMessage3
			// 
			this.richMessage3.BackColor = System.Drawing.SystemColors.HighlightText;
			this.richMessage3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richMessage3.Font = new System.Drawing.Font("Arial", 9F);
			this.richMessage3.Location = new System.Drawing.Point(12, 150);
			this.richMessage3.Name = "richMessage3";
			this.richMessage3.ReadOnly = true;
			this.richMessage3.Size = new System.Drawing.Size(449, 68);
			this.richMessage3.TabIndex = 5;
			this.richMessage3.Text = "";
			// 
			// richMessage4
			// 
			this.richMessage4.BackColor = System.Drawing.SystemColors.HighlightText;
			this.richMessage4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richMessage4.Font = new System.Drawing.Font("Arial", 9F);
			this.richMessage4.Location = new System.Drawing.Point(12, 219);
			this.richMessage4.Name = "richMessage4";
			this.richMessage4.ReadOnly = true;
			this.richMessage4.Size = new System.Drawing.Size(449, 68);
			this.richMessage4.TabIndex = 6;
			this.richMessage4.Text = "";
			// 
			// richMessage5
			// 
			this.richMessage5.BackColor = System.Drawing.SystemColors.HighlightText;
			this.richMessage5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richMessage5.Font = new System.Drawing.Font("Arial", 9F);
			this.richMessage5.Location = new System.Drawing.Point(12, 288);
			this.richMessage5.Name = "richMessage5";
			this.richMessage5.ReadOnly = true;
			this.richMessage5.Size = new System.Drawing.Size(449, 68);
			this.richMessage5.TabIndex = 7;
			this.richMessage5.Text = "";
			// 
			// ImportMessage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(467, 365);
			this.Controls.Add(this.richMessage5);
			this.Controls.Add(this.richMessage4);
			this.Controls.Add(this.richMessage3);
			this.Controls.Add(this.richMessage2);
			this.Controls.Add(this.richMessage1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportMessage";
			this.Text = "ImportMessage";
			this.Load += new System.EventHandler(this.ImportMessage_Load);
			this.Leave += new System.EventHandler(this.ImportMessage_Leave);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox richMessage1;
		private System.Windows.Forms.RichTextBox richMessage2;
		private System.Windows.Forms.RichTextBox richMessage3;
		private System.Windows.Forms.RichTextBox richMessage4;
		private System.Windows.Forms.RichTextBox richMessage5;

	}
}