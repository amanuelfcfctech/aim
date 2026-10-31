namespace CFCFormsControlLibrary
{
    partial class MessageBoxCFC
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
            this.components = new System.ComponentModel.Container();
            this.lblMessage = new System.Windows.Forms.Label();
            this.tmrAnimate = new System.Windows.Forms.Timer(this.components);
            this.picMessage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(46, 12);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 23);
            this.lblMessage.TabIndex = 2;
            // 
            // tmrAnimate
            // 
            this.tmrAnimate.Interval = 20;
            this.tmrAnimate.Tick += new System.EventHandler(this.tmrAnimate_Tick);
            // 
            // picMessage
            // 
            this.picMessage.Location = new System.Drawing.Point(8, 8);
            this.picMessage.Name = "picMessage";
            this.picMessage.Size = new System.Drawing.Size(32, 32);
            this.picMessage.TabIndex = 3;
            this.picMessage.TabStop = false;
            // 
            // MessageBoxCFC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picMessage);
            this.Controls.Add(this.lblMessage);
            this.Name = "MessageBoxCFC";
            this.Size = new System.Drawing.Size(673, 49);
            this.Load += new System.EventHandler(this.MessageBoxCFC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Timer tmrAnimate;
    }
}
