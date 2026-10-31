namespace CFCAIM
{
	partial class DefaultSettings
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultSettings));
			this.pnlTopBtns = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.lueDataSourceType = new DevExpress.XtraEditors.LookUpEdit();
			this.btnSave = new System.Windows.Forms.Button();
			this.panel4 = new System.Windows.Forms.Panel();
			this.btnResize = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.pnlTopBtns.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).BeginInit();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTopBtns
			// 
			this.pnlTopBtns.Controls.Add(this.btnRefresh);
			this.pnlTopBtns.Controls.Add(this.btnExit);
			this.pnlTopBtns.Location = new System.Drawing.Point(3, 4);
			this.pnlTopBtns.Name = "pnlTopBtns";
			this.pnlTopBtns.Size = new System.Drawing.Size(122, 40);
			this.pnlTopBtns.TabIndex = 29;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
			this.btnRefresh.Location = new System.Drawing.Point(59, 1);
			this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(57, 37);
			this.btnRefresh.TabIndex = 25;
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.DefaultSettings_Load);
			// 
			// btnExit
			// 
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(2, 1);
			this.btnExit.Margin = new System.Windows.Forms.Padding(2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(54, 37);
			this.btnExit.TabIndex = 26;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 9F);
			this.label2.Location = new System.Drawing.Point(60, 101);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 15);
			this.label2.TabIndex = 63;
			this.label2.Text = "Data Source";
			// 
			// lueDataSourceType
			// 
			this.lueDataSourceType.Location = new System.Drawing.Point(151, 98);
			this.lueDataSourceType.Margin = new System.Windows.Forms.Padding(4);
			this.lueDataSourceType.Name = "lueDataSourceType";
			this.lueDataSourceType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.lueDataSourceType.Properties.Appearance.Options.UseFont = true;
			this.lueDataSourceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.lueDataSourceType.Size = new System.Drawing.Size(156, 22);
			this.lueDataSourceType.TabIndex = 32;
			this.lueDataSourceType.Click += new System.EventHandler(this.lueDataSourceType_Click);
			this.lueDataSourceType.Enter += new System.EventHandler(this.lueDataSourceType_Enter);
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.Location = new System.Drawing.Point(123, 5);
			this.btnSave.Margin = new System.Windows.Forms.Padding(2);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(57, 37);
			this.btnSave.TabIndex = 63;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.BackColor = System.Drawing.Color.DarkBlue;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel4.Controls.Add(this.btnResize);
			this.panel4.Controls.Add(this.btnSave);
			this.panel4.Controls.Add(this.label7);
			this.panel4.Controls.Add(this.pnlTopBtns);
			this.panel4.Location = new System.Drawing.Point(1, 1);
			this.panel4.Margin = new System.Windows.Forms.Padding(2);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(678, 51);
			this.panel4.TabIndex = 151;
			// 
			// btnResize
			// 
			this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
			this.btnResize.Location = new System.Drawing.Point(639, 0);
			this.btnResize.Margin = new System.Windows.Forms.Padding(2);
			this.btnResize.Name = "btnResize";
			this.btnResize.Size = new System.Drawing.Size(34, 34);
			this.btnResize.TabIndex = 150;
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
			this.label7.Location = new System.Drawing.Point(262, 14);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(158, 23);
			this.label7.TabIndex = 149;
			this.label7.Text = "Default Settings";
			// 
			// DefaultSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(679, 401);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lueDataSourceType);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "DefaultSettings";
			this.Text = "Default Settings";
			this.Load += new System.EventHandler(this.DefaultSettings_Load);
			this.SizeChanged += new System.EventHandler(this.DefaultSettings_SizeChanged);
			this.pnlTopBtns.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).EndInit();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.LookUpEdit lueDataSourceType;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnResize;
	}
}