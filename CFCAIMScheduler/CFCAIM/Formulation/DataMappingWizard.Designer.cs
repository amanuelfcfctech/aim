namespace CFCAIM.Maintenance
{
	partial class DataMappingWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataMappingWizard));
            this.pnlAdd = new System.Windows.Forms.Panel();
            this.lblHeading = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblFormulationPath = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblNewDefaultForProposed = new System.Windows.Forms.Label();
            this.lueNewDefaultForProposed = new DevExpress.XtraEditors.LookUpEdit();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lueDataSourceTypeNew = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDataSourceTypeNew = new System.Windows.Forms.Label();
            this.lblPriorityNew = new System.Windows.Forms.Label();
            this.luePriorityNew = new DevExpress.XtraEditors.LookUpEdit();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.lueDatabaseNameNew = new DevExpress.XtraEditors.LookUpEdit();
            this.lbCopyFrom = new System.Windows.Forms.Label();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.lueDatabaseName = new DevExpress.XtraEditors.LookUpEdit();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlNext = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.fbdSelectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlAdd.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueNewDefaultForProposed.Properties)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceTypeNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePriorityNew.Properties)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDatabaseNameNew.Properties)).BeginInit();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDatabaseName.Properties)).BeginInit();
            this.pnlNext.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAdd
            // 
            this.pnlAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAdd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAdd.Controls.Add(this.lblHeading);
            this.pnlAdd.Controls.Add(this.btnBack);
            this.pnlAdd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAdd.Location = new System.Drawing.Point(2, 2);
            this.pnlAdd.Margin = new System.Windows.Forms.Padding(2);
            this.pnlAdd.Name = "pnlAdd";
            this.pnlAdd.Size = new System.Drawing.Size(683, 77);
            this.pnlAdd.TabIndex = 167;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblHeading.Location = new System.Drawing.Point(19, 12);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(113, 15);
            this.lblHeading.TabIndex = 111;
            this.lblHeading.Text = "Add New Data Map";
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Arial", 9F);
            this.btnBack.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnBack.Location = new System.Drawing.Point(19, 31);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(39, 35);
            this.btnBack.TabIndex = 139;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.ItemSize = new System.Drawing.Size(71, 24);
            this.tabControl1.Location = new System.Drawing.Point(1, 82);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 281);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 175;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtDatabaseName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(670, 249);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(194, 38);
            this.txtDatabaseName.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(210, 20);
            this.txtDatabaseName.TabIndex = 0;
            this.txtDatabaseName.TextChanged += new System.EventHandler(this.txtDatabaseName_TextChanged);
            this.txtDatabaseName.Leave += new System.EventHandler(this.txtDatabaseName_Leave);
            this.txtDatabaseName.Validated += new System.EventHandler(this.txtDatabaseName_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F);
            this.label2.Location = new System.Drawing.Point(45, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 15);
            this.label2.TabIndex = 110;
            this.label2.Text = "Enter Data Map Name";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtPath);
            this.tabPage2.Controls.Add(this.lblFormulationPath);
            this.tabPage2.Controls.Add(this.btnBrowse);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(670, 249);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.Font = new System.Drawing.Font("Arial", 9F);
            this.txtPath.Location = new System.Drawing.Point(146, 38);
            this.txtPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(393, 21);
            this.txtPath.TabIndex = 35;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            this.txtPath.Leave += new System.EventHandler(this.txtPath_Leave);
            // 
            // lblFormulationPath
            // 
            this.lblFormulationPath.AutoSize = true;
            this.lblFormulationPath.Font = new System.Drawing.Font("Arial", 9F);
            this.lblFormulationPath.Location = new System.Drawing.Point(40, 41);
            this.lblFormulationPath.Name = "lblFormulationPath";
            this.lblFormulationPath.Size = new System.Drawing.Size(101, 15);
            this.lblFormulationPath.TabIndex = 33;
            this.lblFormulationPath.Text = "Formulation Path";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Image = global::CFCAIM.Properties.Resources.browse;
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(545, 30);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(88, 33);
            this.btnBrowse.TabIndex = 34;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblNewDefaultForProposed);
            this.tabPage3.Controls.Add(this.lueNewDefaultForProposed);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(670, 249);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblNewDefaultForProposed
            // 
            this.lblNewDefaultForProposed.AutoSize = true;
            this.lblNewDefaultForProposed.Font = new System.Drawing.Font("Arial", 9F);
            this.lblNewDefaultForProposed.Location = new System.Drawing.Point(39, 41);
            this.lblNewDefaultForProposed.Name = "lblNewDefaultForProposed";
            this.lblNewDefaultForProposed.Size = new System.Drawing.Size(210, 15);
            this.lblNewDefaultForProposed.TabIndex = 55;
            this.lblNewDefaultForProposed.Text = "Default For Proposed Nutrient Values";
            // 
            // lueNewDefaultForProposed
            // 
            this.lueNewDefaultForProposed.Location = new System.Drawing.Point(253, 38);
            this.lueNewDefaultForProposed.Name = "lueNewDefaultForProposed";
            this.lueNewDefaultForProposed.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueNewDefaultForProposed.Properties.Appearance.Options.UseFont = true;
            this.lueNewDefaultForProposed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueNewDefaultForProposed.Size = new System.Drawing.Size(319, 22);
            this.lueNewDefaultForProposed.TabIndex = 54;
            this.lueNewDefaultForProposed.EditValueChanged += new System.EventHandler(this.lueNewDefaultForProposed_EditValueChanged);
            this.lueNewDefaultForProposed.Leave += new System.EventHandler(this.lueNewDefaultForProposed_Leave);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lueDataSourceTypeNew);
            this.tabPage4.Controls.Add(this.lblDataSourceTypeNew);
            this.tabPage4.Controls.Add(this.lblPriorityNew);
            this.tabPage4.Controls.Add(this.luePriorityNew);
            this.tabPage4.Location = new System.Drawing.Point(4, 28);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(670, 249);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lueDataSourceTypeNew
            // 
            this.lueDataSourceTypeNew.Location = new System.Drawing.Point(470, 38);
            this.lueDataSourceTypeNew.Margin = new System.Windows.Forms.Padding(4);
            this.lueDataSourceTypeNew.Name = "lueDataSourceTypeNew";
            this.lueDataSourceTypeNew.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDataSourceTypeNew.Properties.Appearance.Options.UseFont = true;
            this.lueDataSourceTypeNew.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDataSourceTypeNew.Size = new System.Drawing.Size(142, 22);
            this.lueDataSourceTypeNew.TabIndex = 74;
            this.lueDataSourceTypeNew.Visible = false;
            // 
            // lblDataSourceTypeNew
            // 
            this.lblDataSourceTypeNew.AutoSize = true;
            this.lblDataSourceTypeNew.Font = new System.Drawing.Font("Arial", 9F);
            this.lblDataSourceTypeNew.Location = new System.Drawing.Point(391, 42);
            this.lblDataSourceTypeNew.Name = "lblDataSourceTypeNew";
            this.lblDataSourceTypeNew.Size = new System.Drawing.Size(75, 15);
            this.lblDataSourceTypeNew.TabIndex = 75;
            this.lblDataSourceTypeNew.Text = "Data Source";
            this.lblDataSourceTypeNew.Visible = false;
            // 
            // lblPriorityNew
            // 
            this.lblPriorityNew.AutoSize = true;
            this.lblPriorityNew.Font = new System.Drawing.Font("Arial", 9F);
            this.lblPriorityNew.Location = new System.Drawing.Point(42, 41);
            this.lblPriorityNew.Name = "lblPriorityNew";
            this.lblPriorityNew.Size = new System.Drawing.Size(132, 15);
            this.lblPriorityNew.TabIndex = 71;
            this.lblPriorityNew.Text = "AIM Priority Used Value";
            // 
            // luePriorityNew
            // 
            this.luePriorityNew.Location = new System.Drawing.Point(178, 38);
            this.luePriorityNew.Name = "luePriorityNew";
            this.luePriorityNew.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.luePriorityNew.Properties.Appearance.Options.UseFont = true;
            this.luePriorityNew.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luePriorityNew.Size = new System.Drawing.Size(180, 22);
            this.luePriorityNew.TabIndex = 70;
            this.luePriorityNew.EditValueChanged += new System.EventHandler(this.luePriorityNew_EditValueChanged);
            this.luePriorityNew.Leave += new System.EventHandler(this.luePriorityNew_Leave);
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 28);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage5.Size = new System.Drawing.Size(670, 249);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.lueDatabaseNameNew);
            this.tabPage6.Controls.Add(this.lbCopyFrom);
            this.tabPage6.Location = new System.Drawing.Point(4, 28);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage6.Size = new System.Drawing.Size(670, 249);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // lueDatabaseNameNew
            // 
            this.lueDatabaseNameNew.Location = new System.Drawing.Point(124, 38);
            this.lueDatabaseNameNew.Margin = new System.Windows.Forms.Padding(2);
            this.lueDatabaseNameNew.Name = "lueDatabaseNameNew";
            this.lueDatabaseNameNew.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDatabaseNameNew.Properties.Appearance.Options.UseFont = true;
            this.lueDatabaseNameNew.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueDatabaseNameNew.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueDatabaseNameNew.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDatabaseNameNew.Size = new System.Drawing.Size(310, 22);
            this.lueDatabaseNameNew.TabIndex = 69;
            // 
            // lbCopyFrom
            // 
            this.lbCopyFrom.AutoSize = true;
            this.lbCopyFrom.Font = new System.Drawing.Font("Arial", 9F);
            this.lbCopyFrom.Location = new System.Drawing.Point(45, 41);
            this.lbCopyFrom.Name = "lbCopyFrom";
            this.lbCopyFrom.Size = new System.Drawing.Size(67, 15);
            this.lbCopyFrom.TabIndex = 68;
            this.lbCopyFrom.Text = "Copy From";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.lueDatabaseName);
            this.tabPage8.Controls.Add(this.label7);
            this.tabPage8.Location = new System.Drawing.Point(4, 28);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage8.Size = new System.Drawing.Size(670, 249);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "tabPage8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // lueDatabaseName
            // 
            this.lueDatabaseName.Location = new System.Drawing.Point(177, 38);
            this.lueDatabaseName.Margin = new System.Windows.Forms.Padding(2);
            this.lueDatabaseName.Name = "lueDatabaseName";
            this.lueDatabaseName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lueDatabaseName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDatabaseName.Properties.Appearance.Options.UseBackColor = true;
            this.lueDatabaseName.Properties.Appearance.Options.UseFont = true;
            this.lueDatabaseName.Properties.Appearance.Options.UseTextOptions = true;
            this.lueDatabaseName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lueDatabaseName.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueDatabaseName.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueDatabaseName.Properties.AppearanceDropDown.Options.UseTextOptions = true;
            this.lueDatabaseName.Properties.AppearanceDropDown.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lueDatabaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDatabaseName.Size = new System.Drawing.Size(282, 22);
            this.lueDatabaseName.TabIndex = 112;
            this.lueDatabaseName.EditValueChanged += new System.EventHandler(this.lueDatabaseName_EditValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F);
            this.label7.Location = new System.Drawing.Point(40, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 15);
            this.label7.TabIndex = 111;
            this.label7.Text = "Select Data Map Name";
            // 
            // pnlNext
            // 
            this.pnlNext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNext.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlNext.Controls.Add(this.btnCancel);
            this.pnlNext.Controls.Add(this.btnNext);
            this.pnlNext.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlNext.Location = new System.Drawing.Point(4, 370);
            this.pnlNext.Margin = new System.Windows.Forms.Padding(2);
            this.pnlNext.Name = "pnlNext";
            this.pnlNext.Size = new System.Drawing.Size(646, 47);
            this.pnlNext.TabIndex = 176;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Arial", 9F);
            this.btnCancel.Location = new System.Drawing.Point(567, 14);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 22);
            this.btnCancel.TabIndex = 61;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Font = new System.Drawing.Font("Arial", 9F);
            this.btnNext.Location = new System.Drawing.Point(483, 14);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 22);
            this.btnNext.TabIndex = 60;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // DataMappingWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(687, 426);
            this.ControlBox = false;
            this.Controls.Add(this.pnlNext);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pnlAdd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataMappingWizard";
            this.Text = "Data Mapping Wizard";
            this.Load += new System.EventHandler(this.DataMappingWizard_Load);
            this.pnlAdd.ResumeLayout(false);
            this.pnlAdd.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueNewDefaultForProposed.Properties)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceTypeNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luePriorityNew.Properties)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDatabaseNameNew.Properties)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDatabaseName.Properties)).EndInit();
            this.pnlNext.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlAdd;
		private System.Windows.Forms.Label lblHeading;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox txtDatabaseName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel pnlNext;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnNext;
		private DevExpress.XtraEditors.LookUpEdit lueDatabaseName;
		private System.Windows.Forms.Label lblFormulationPath;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.FolderBrowserDialog fbdSelectFolder;
		private System.Windows.Forms.Label lblNewDefaultForProposed;
		private DevExpress.XtraEditors.LookUpEdit lueNewDefaultForProposed;
		private System.Windows.Forms.Label lblPriorityNew;
		private DevExpress.XtraEditors.LookUpEdit luePriorityNew;
		private DevExpress.XtraEditors.LookUpEdit lueDatabaseNameNew;
		private System.Windows.Forms.Label lbCopyFrom;
		private System.Windows.Forms.TextBox txtPath;
		private DevExpress.XtraEditors.LookUpEdit lueDataSourceTypeNew;
		private System.Windows.Forms.Label lblDataSourceTypeNew;
	}
}