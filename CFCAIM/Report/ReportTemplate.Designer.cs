namespace CFCAIM.Report
{
    partial class ReportTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportTemplate));
            this.pnlNewViewTemplate = new System.Windows.Forms.Panel();
            this.btnCancelViewTemplateSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSaveViewTemplate = new System.Windows.Forms.Button();
            this.txtNewViewTemplateName = new System.Windows.Forms.TextBox();
            this.pnlExistingViewTemplate = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.lueViewTemplateInTab = new DevExpress.XtraEditors.LookUpEdit();
            this.pnlViewButtons = new System.Windows.Forms.Panel();
            this.btnUpdateCurrentViewTemplate = new System.Windows.Forms.Button();
            this.btnDeleteViewTemplate = new System.Windows.Forms.Button();
            this.btnNewViewTemplate = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlNewViewTemplate.SuspendLayout();
            this.pnlExistingViewTemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueViewTemplateInTab.Properties)).BeginInit();
            this.pnlViewButtons.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlTopBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNewViewTemplate
            // 
            this.pnlNewViewTemplate.Controls.Add(this.btnCancelViewTemplateSave);
            this.pnlNewViewTemplate.Controls.Add(this.label9);
            this.pnlNewViewTemplate.Controls.Add(this.btnSaveViewTemplate);
            this.pnlNewViewTemplate.Controls.Add(this.txtNewViewTemplateName);
            this.pnlNewViewTemplate.Location = new System.Drawing.Point(339, 123);
            this.pnlNewViewTemplate.Name = "pnlNewViewTemplate";
            this.pnlNewViewTemplate.Size = new System.Drawing.Size(357, 56);
            this.pnlNewViewTemplate.TabIndex = 150;
            this.pnlNewViewTemplate.Visible = false;
            // 
            // btnCancelViewTemplateSave
            // 
            this.btnCancelViewTemplateSave.Font = new System.Drawing.Font("Arial", 7.2F);
            this.btnCancelViewTemplateSave.Location = new System.Drawing.Point(295, 18);
            this.btnCancelViewTemplateSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelViewTemplateSave.Name = "btnCancelViewTemplateSave";
            this.btnCancelViewTemplateSave.Size = new System.Drawing.Size(51, 24);
            this.btnCancelViewTemplateSave.TabIndex = 141;
            this.btnCancelViewTemplateSave.Text = "Cancel";
            this.btnCancelViewTemplateSave.UseVisualStyleBackColor = true;
            this.btnCancelViewTemplateSave.Click += new System.EventHandler(this.btnCancelViewTemplateSave_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 7.2F);
            this.label9.Location = new System.Drawing.Point(3, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 13);
            this.label9.TabIndex = 140;
            this.label9.Text = "New Template Name";
            // 
            // btnSaveViewTemplate
            // 
            this.btnSaveViewTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveViewTemplate.Image")));
            this.btnSaveViewTemplate.Location = new System.Drawing.Point(209, 10);
            this.btnSaveViewTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveViewTemplate.Name = "btnSaveViewTemplate";
            this.btnSaveViewTemplate.Size = new System.Drawing.Size(51, 37);
            this.btnSaveViewTemplate.TabIndex = 140;
            this.btnSaveViewTemplate.UseVisualStyleBackColor = true;
            this.btnSaveViewTemplate.Click += new System.EventHandler(this.btnSaveViewTemplate_Click);
            // 
            // txtNewViewTemplateName
            // 
            this.txtNewViewTemplateName.Font = new System.Drawing.Font("Arial", 9F);
            this.txtNewViewTemplateName.Location = new System.Drawing.Point(5, 18);
            this.txtNewViewTemplateName.Name = "txtNewViewTemplateName";
            this.txtNewViewTemplateName.Size = new System.Drawing.Size(199, 21);
            this.txtNewViewTemplateName.TabIndex = 138;
            // 
            // pnlExistingViewTemplate
            // 
            this.pnlExistingViewTemplate.Controls.Add(this.label24);
            this.pnlExistingViewTemplate.Controls.Add(this.lueViewTemplateInTab);
            this.pnlExistingViewTemplate.Location = new System.Drawing.Point(44, 140);
            this.pnlExistingViewTemplate.Name = "pnlExistingViewTemplate";
            this.pnlExistingViewTemplate.Size = new System.Drawing.Size(269, 26);
            this.pnlExistingViewTemplate.TabIndex = 149;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Arial", 7.2F);
            this.label24.Location = new System.Drawing.Point(2, 6);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(52, 13);
            this.label24.TabIndex = 142;
            this.label24.Text = "Template";
            // 
            // lueViewTemplateInTab
            // 
            this.lueViewTemplateInTab.Location = new System.Drawing.Point(66, 2);
            this.lueViewTemplateInTab.Name = "lueViewTemplateInTab";
            this.lueViewTemplateInTab.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueViewTemplateInTab.Properties.Appearance.Options.UseFont = true;
            this.lueViewTemplateInTab.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueViewTemplateInTab.Properties.DropDownRows = 12;
            this.lueViewTemplateInTab.Size = new System.Drawing.Size(199, 22);
            this.lueViewTemplateInTab.TabIndex = 143;
            this.lueViewTemplateInTab.EditValueChanged += new System.EventHandler(this.lueViewTemplateInTab_EditValueChanged);
            // 
            // pnlViewButtons
            // 
            this.pnlViewButtons.Controls.Add(this.btnUpdateCurrentViewTemplate);
            this.pnlViewButtons.Controls.Add(this.btnDeleteViewTemplate);
            this.pnlViewButtons.Controls.Add(this.btnNewViewTemplate);
            this.pnlViewButtons.Location = new System.Drawing.Point(42, 83);
            this.pnlViewButtons.Name = "pnlViewButtons";
            this.pnlViewButtons.Size = new System.Drawing.Size(197, 47);
            this.pnlViewButtons.TabIndex = 148;
            // 
            // btnUpdateCurrentViewTemplate
            // 
            this.btnUpdateCurrentViewTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateCurrentViewTemplate.Image")));
            this.btnUpdateCurrentViewTemplate.Location = new System.Drawing.Point(70, 3);
            this.btnUpdateCurrentViewTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateCurrentViewTemplate.Name = "btnUpdateCurrentViewTemplate";
            this.btnUpdateCurrentViewTemplate.Size = new System.Drawing.Size(54, 41);
            this.btnUpdateCurrentViewTemplate.TabIndex = 146;
            this.btnUpdateCurrentViewTemplate.UseVisualStyleBackColor = true;
            this.btnUpdateCurrentViewTemplate.Click += new System.EventHandler(this.btnUpdateCurrentViewTemplate_Click);
            // 
            // btnDeleteViewTemplate
            // 
            this.btnDeleteViewTemplate.Image = global::CFCAIM.Properties.Resources.trash_can;
            this.btnDeleteViewTemplate.Location = new System.Drawing.Point(136, 3);
            this.btnDeleteViewTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteViewTemplate.Name = "btnDeleteViewTemplate";
            this.btnDeleteViewTemplate.Size = new System.Drawing.Size(54, 41);
            this.btnDeleteViewTemplate.TabIndex = 144;
            this.btnDeleteViewTemplate.UseVisualStyleBackColor = true;
            this.btnDeleteViewTemplate.Click += new System.EventHandler(this.btnDeleteViewTemplate_Click);
            // 
            // btnNewViewTemplate
            // 
            this.btnNewViewTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnNewViewTemplate.Image")));
            this.btnNewViewTemplate.Location = new System.Drawing.Point(4, 3);
            this.btnNewViewTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewViewTemplate.Name = "btnNewViewTemplate";
            this.btnNewViewTemplate.Size = new System.Drawing.Size(54, 41);
            this.btnNewViewTemplate.TabIndex = 147;
            this.btnNewViewTemplate.UseVisualStyleBackColor = true;
            this.btnNewViewTemplate.Click += new System.EventHandler(this.btnNewViewTemplate_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Location = new System.Drawing.Point(2, 2);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(716, 51);
            this.panel4.TabIndex = 155;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(439, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(175, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Report Templates";
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(3, 3);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(282, 38);
            this.pnlTopBtns.TabIndex = 104;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(1, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 41);
            this.btnExit.TabIndex = 89;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ReportTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(717, 376);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pnlNewViewTemplate);
            this.Controls.Add(this.pnlExistingViewTemplate);
            this.Controls.Add(this.pnlViewButtons);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportTemplate";
            this.Text = "Report Templates";
            this.Load += new System.EventHandler(this.ReportTemplate_Load);
            this.pnlNewViewTemplate.ResumeLayout(false);
            this.pnlNewViewTemplate.PerformLayout();
            this.pnlExistingViewTemplate.ResumeLayout(false);
            this.pnlExistingViewTemplate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueViewTemplateInTab.Properties)).EndInit();
            this.pnlViewButtons.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlTopBtns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlNewViewTemplate;
        private System.Windows.Forms.Button btnCancelViewTemplateSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSaveViewTemplate;
        private System.Windows.Forms.TextBox txtNewViewTemplateName;
        private System.Windows.Forms.Panel pnlExistingViewTemplate;
        private System.Windows.Forms.Label label24;
        private DevExpress.XtraEditors.LookUpEdit lueViewTemplateInTab;
        private System.Windows.Forms.Panel pnlViewButtons;
        private System.Windows.Forms.Button btnUpdateCurrentViewTemplate;
        private System.Windows.Forms.Button btnDeleteViewTemplate;
        private System.Windows.Forms.Button btnNewViewTemplate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlTopBtns;
        private System.Windows.Forms.Button btnExit;
    }
}