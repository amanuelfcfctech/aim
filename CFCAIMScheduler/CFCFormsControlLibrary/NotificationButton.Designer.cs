namespace CFCFormsControlLibrary
{
    partial class NotificationButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.picMessage = new System.Windows.Forms.PictureBox();
			this.lblNo = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picMessage)).BeginInit();
			this.SuspendLayout();
			// 
			// picMessage
			// 
			this.picMessage.Image = global::CFCFormsControlLibrary.Properties.Resources.NotificationGrey;
			this.picMessage.Location = new System.Drawing.Point(38, 0);
			this.picMessage.Name = "picMessage";
			this.picMessage.Size = new System.Drawing.Size(34, 30);
			this.picMessage.TabIndex = 0;
			this.picMessage.TabStop = false;
			// 
			// lblNo
			// 
			this.lblNo.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblNo.Location = new System.Drawing.Point(3, 0);
			this.lblNo.Name = "lblNo";
			this.lblNo.Size = new System.Drawing.Size(35, 30);
			this.lblNo.TabIndex = 1;
			this.lblNo.Text = "0";
			this.lblNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblNo.TextChanged += new System.EventHandler(this.lblNo_TextChanged);
			this.lblNo.Click += new System.EventHandler(this.lblNo_Click);
			// 
			// NotificationButton
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblNo);
			this.Controls.Add(this.picMessage);
			this.Name = "NotificationButton";
			this.Size = new System.Drawing.Size(75, 30);
			((System.ComponentModel.ISupportInitialize)(this.picMessage)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picMessage;
        private System.Windows.Forms.Label lblNo;
    }
}
