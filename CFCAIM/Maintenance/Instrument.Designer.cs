namespace CFCAIM.Maintenance
{
    partial class Instrument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Instrument));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tcoProcess = new System.Windows.Forms.TabControl();
            this.tpProcess = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lueDataSourceType = new DevExpress.XtraEditors.LookUpEdit();
            this.gbActive = new System.Windows.Forms.GroupBox();
            this.rbInactive = new System.Windows.Forms.RadioButton();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInstrumentDescription = new System.Windows.Forms.TextBox();
            this.lueFileType = new DevExpress.XtraEditors.LookUpEdit();
            this.lblFileType = new System.Windows.Forms.Label();
            this.lueInstrumentCode = new DevExpress.XtraEditors.LookUpEdit();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblTemplateName = new System.Windows.Forms.Label();
            this.tpNewSetting = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lueDataSourceTypeNew = new DevExpress.XtraEditors.LookUpEdit();
            this.txtInstrumentCodeNew = new System.Windows.Forms.TextBox();
            this.lueFileTypeNew = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblNewInstrumentDescription = new System.Windows.Forms.Label();
            this.txtInstrumentDescriptionNew = new System.Windows.Forms.TextBox();
            this.pnlTopBtns = new System.Windows.Forms.Panel();
            this.fbdSelectedDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.tcoProcess.SuspendLayout();
            this.tpProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).BeginInit();
            this.gbActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueFileType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInstrumentCode.Properties)).BeginInit();
            this.tpNewSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceTypeNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFileTypeNew.Properties)).BeginInit();
            this.pnlTopBtns.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(70, 0);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(71, 46);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.Instrument_Load);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(67, 46);
            this.btnExit.TabIndex = 0;
            this.btnExit.TabStop = false;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tcoProcess
            // 
            this.tcoProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcoProcess.Controls.Add(this.tpProcess);
            this.tcoProcess.Controls.Add(this.tpNewSetting);
            this.tcoProcess.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcoProcess.Location = new System.Drawing.Point(13, 87);
            this.tcoProcess.Margin = new System.Windows.Forms.Padding(4);
            this.tcoProcess.Name = "tcoProcess";
            this.tcoProcess.SelectedIndex = 0;
            this.tcoProcess.Size = new System.Drawing.Size(848, 354);
            this.tcoProcess.TabIndex = 88;
            this.tcoProcess.TabStop = false;
            // 
            // tpProcess
            // 
            this.tpProcess.Controls.Add(this.label6);
            this.tpProcess.Controls.Add(this.label3);
            this.tpProcess.Controls.Add(this.lueDataSourceType);
            this.tpProcess.Controls.Add(this.gbActive);
            this.tpProcess.Controls.Add(this.label4);
            this.tpProcess.Controls.Add(this.txtInstrumentDescription);
            this.tpProcess.Controls.Add(this.lueFileType);
            this.tpProcess.Controls.Add(this.lblFileType);
            this.tpProcess.Controls.Add(this.lueInstrumentCode);
            this.tpProcess.Controls.Add(this.btnDelete);
            this.tpProcess.Controls.Add(this.lblTemplateName);
            this.tpProcess.Location = new System.Drawing.Point(4, 25);
            this.tpProcess.Margin = new System.Windows.Forms.Padding(4);
            this.tpProcess.Name = "tpProcess";
            this.tpProcess.Padding = new System.Windows.Forms.Padding(4);
            this.tpProcess.Size = new System.Drawing.Size(840, 325);
            this.tpProcess.TabIndex = 0;
            this.tpProcess.Text = "Existing Instruments";
            this.tpProcess.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F);
            this.label6.Location = new System.Drawing.Point(164, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 17);
            this.label6.TabIndex = 63;
            this.label6.Text = "Data Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(199, 184);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 62;
            this.label3.Text = "Status ";
            // 
            // lueDataSourceType
            // 
            this.lueDataSourceType.Location = new System.Drawing.Point(263, 121);
            this.lueDataSourceType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueDataSourceType.Name = "lueDataSourceType";
            this.lueDataSourceType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueDataSourceType.Properties.Appearance.Options.UseFont = true;
            this.lueDataSourceType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDataSourceType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueDataSourceType.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDataSourceType.Properties.AppearanceFocused.Options.UseFont = true;
            this.lueDataSourceType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDataSourceType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueDataSourceType.Size = new System.Drawing.Size(208, 24);
            this.lueDataSourceType.TabIndex = 4;
            this.lueDataSourceType.Leave += new System.EventHandler(this.lueDataSourceType_Leave);
            // 
            // gbActive
            // 
            this.gbActive.Controls.Add(this.rbInactive);
            this.gbActive.Controls.Add(this.rbActive);
            this.gbActive.Location = new System.Drawing.Point(262, 167);
            this.gbActive.Name = "gbActive";
            this.gbActive.Size = new System.Drawing.Size(93, 63);
            this.gbActive.TabIndex = 61;
            this.gbActive.TabStop = false;
            this.gbActive.Leave += new System.EventHandler(this.gbActive_Leave);
            // 
            // rbInactive
            // 
            this.rbInactive.AutoSize = true;
            this.rbInactive.Location = new System.Drawing.Point(7, 35);
            this.rbInactive.Name = "rbInactive";
            this.rbInactive.Size = new System.Drawing.Size(77, 20);
            this.rbInactive.TabIndex = 1;
            this.rbInactive.Text = "Inactive";
            this.rbInactive.UseVisualStyleBackColor = true;
            this.rbInactive.Leave += new System.EventHandler(this.rbInactive_Leave);
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Location = new System.Drawing.Point(7, 15);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(67, 20);
            this.rbActive.TabIndex = 0;
            this.rbActive.Text = "Active";
            this.rbActive.UseVisualStyleBackColor = true;
            this.rbActive.Leave += new System.EventHandler(this.rbActive_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 16);
            this.label4.TabIndex = 60;
            this.label4.Text = "Instrument Description";
            // 
            // txtInstrumentDescription
            // 
            this.txtInstrumentDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstrumentDescription.Location = new System.Drawing.Point(263, 56);
            this.txtInstrumentDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtInstrumentDescription.Name = "txtInstrumentDescription";
            this.txtInstrumentDescription.Size = new System.Drawing.Size(388, 25);
            this.txtInstrumentDescription.TabIndex = 3;
            this.txtInstrumentDescription.Leave += new System.EventHandler(this.txtInstrumentDescription_Leave);
            // 
            // lueFileType
            // 
            this.lueFileType.Location = new System.Drawing.Point(263, 89);
            this.lueFileType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueFileType.Name = "lueFileType";
            this.lueFileType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueFileType.Properties.Appearance.Options.UseFont = true;
            this.lueFileType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9F);
            this.lueFileType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueFileType.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9F);
            this.lueFileType.Properties.AppearanceFocused.Options.UseFont = true;
            this.lueFileType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueFileType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueFileType.Size = new System.Drawing.Size(208, 24);
            this.lueFileType.TabIndex = 5;
            this.lueFileType.Leave += new System.EventHandler(this.lueFileType_Leave);
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileType.Location = new System.Drawing.Point(188, 93);
            this.lblFileType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(66, 17);
            this.lblFileType.TabIndex = 52;
            this.lblFileType.Text = "File Type";
            // 
            // lueInstrumentCode
            // 
            this.lueInstrumentCode.Location = new System.Drawing.Point(263, 23);
            this.lueInstrumentCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueInstrumentCode.Name = "lueInstrumentCode";
            this.lueInstrumentCode.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lueInstrumentCode.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueInstrumentCode.Properties.Appearance.Options.UseBackColor = true;
            this.lueInstrumentCode.Properties.Appearance.Options.UseFont = true;
            this.lueInstrumentCode.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9F);
            this.lueInstrumentCode.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueInstrumentCode.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9F);
            this.lueInstrumentCode.Properties.AppearanceFocused.Options.UseFont = true;
            this.lueInstrumentCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueInstrumentCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueInstrumentCode.Size = new System.Drawing.Size(388, 24);
            this.lueInstrumentCode.TabIndex = 2;
            this.lueInstrumentCode.EditValueChanged += new System.EventHandler(this.lueInstrumentCode_EditValueChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::CFCAIM.Properties.Resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(733, 21);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(71, 46);
            this.btnDelete.TabIndex = 50;
            this.btnDelete.TabStop = false;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblTemplateName
            // 
            this.lblTemplateName.AutoSize = true;
            this.lblTemplateName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemplateName.Location = new System.Drawing.Point(41, 27);
            this.lblTemplateName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTemplateName.Name = "lblTemplateName";
            this.lblTemplateName.Size = new System.Drawing.Size(213, 17);
            this.lblTemplateName.TabIndex = 45;
            this.lblTemplateName.Text = "Instrument Code/Serial Number";
            // 
            // tpNewSetting
            // 
            this.tpNewSetting.Controls.Add(this.label8);
            this.tpNewSetting.Controls.Add(this.label7);
            this.tpNewSetting.Controls.Add(this.lueDataSourceTypeNew);
            this.tpNewSetting.Controls.Add(this.txtInstrumentCodeNew);
            this.tpNewSetting.Controls.Add(this.lueFileTypeNew);
            this.tpNewSetting.Controls.Add(this.label1);
            this.tpNewSetting.Controls.Add(this.btnSave);
            this.tpNewSetting.Controls.Add(this.lblNewInstrumentDescription);
            this.tpNewSetting.Controls.Add(this.txtInstrumentDescriptionNew);
            this.tpNewSetting.Location = new System.Drawing.Point(4, 25);
            this.tpNewSetting.Margin = new System.Windows.Forms.Padding(4);
            this.tpNewSetting.Name = "tpNewSetting";
            this.tpNewSetting.Padding = new System.Windows.Forms.Padding(4);
            this.tpNewSetting.Size = new System.Drawing.Size(840, 325);
            this.tpNewSetting.TabIndex = 1;
            this.tpNewSetting.Text = "New Instrument";
            this.tpNewSetting.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F);
            this.label8.Location = new System.Drawing.Point(164, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 17);
            this.label8.TabIndex = 63;
            this.label8.Text = "Data Source";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(41, 27);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(213, 17);
            this.label7.TabIndex = 64;
            this.label7.Text = "Instrument Code/Serial Number";
            // 
            // lueDataSourceTypeNew
            // 
            this.lueDataSourceTypeNew.Location = new System.Drawing.Point(263, 121);
            this.lueDataSourceTypeNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueDataSourceTypeNew.Name = "lueDataSourceTypeNew";
            this.lueDataSourceTypeNew.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDataSourceTypeNew.Properties.Appearance.Options.UseFont = true;
            this.lueDataSourceTypeNew.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDataSourceTypeNew.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueDataSourceTypeNew.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9F);
            this.lueDataSourceTypeNew.Properties.AppearanceFocused.Options.UseFont = true;
            this.lueDataSourceTypeNew.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDataSourceTypeNew.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueDataSourceTypeNew.Size = new System.Drawing.Size(209, 24);
            this.lueDataSourceTypeNew.TabIndex = 14;
            // 
            // txtInstrumentCodeNew
            // 
            this.txtInstrumentCodeNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstrumentCodeNew.Location = new System.Drawing.Point(263, 23);
            this.txtInstrumentCodeNew.Margin = new System.Windows.Forms.Padding(4);
            this.txtInstrumentCodeNew.Name = "txtInstrumentCodeNew";
            this.txtInstrumentCodeNew.Size = new System.Drawing.Size(388, 25);
            this.txtInstrumentCodeNew.TabIndex = 12;
            // 
            // lueFileTypeNew
            // 
            this.lueFileTypeNew.Location = new System.Drawing.Point(263, 89);
            this.lueFileTypeNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lueFileTypeNew.Name = "lueFileTypeNew";
            this.lueFileTypeNew.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueFileTypeNew.Properties.Appearance.Options.UseFont = true;
            this.lueFileTypeNew.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9F);
            this.lueFileTypeNew.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lueFileTypeNew.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9F);
            this.lueFileTypeNew.Properties.AppearanceFocused.Options.UseFont = true;
            this.lueFileTypeNew.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueFileTypeNew.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueFileTypeNew.Size = new System.Drawing.Size(209, 24);
            this.lueFileTypeNew.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(188, 93);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 62;
            this.label1.Text = "File Type";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::CFCAIM.Properties.Resources.save;
            this.btnSave.Location = new System.Drawing.Point(733, 21);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 46);
            this.btnSave.TabIndex = 22;
            this.btnSave.TabStop = false;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblNewInstrumentDescription
            // 
            this.lblNewInstrumentDescription.AutoSize = true;
            this.lblNewInstrumentDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewInstrumentDescription.Location = new System.Drawing.Point(99, 60);
            this.lblNewInstrumentDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNewInstrumentDescription.Name = "lblNewInstrumentDescription";
            this.lblNewInstrumentDescription.Size = new System.Drawing.Size(155, 17);
            this.lblNewInstrumentDescription.TabIndex = 58;
            this.lblNewInstrumentDescription.Text = "Instrument Description";
            // 
            // txtInstrumentDescriptionNew
            // 
            this.txtInstrumentDescriptionNew.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstrumentDescriptionNew.Location = new System.Drawing.Point(263, 56);
            this.txtInstrumentDescriptionNew.Margin = new System.Windows.Forms.Padding(4);
            this.txtInstrumentDescriptionNew.Name = "txtInstrumentDescriptionNew";
            this.txtInstrumentDescriptionNew.Size = new System.Drawing.Size(388, 25);
            this.txtInstrumentDescriptionNew.TabIndex = 13;
            // 
            // pnlTopBtns
            // 
            this.pnlTopBtns.Controls.Add(this.btnRefresh);
            this.pnlTopBtns.Controls.Add(this.btnExit);
            this.pnlTopBtns.Location = new System.Drawing.Point(6, 6);
            this.pnlTopBtns.Margin = new System.Windows.Forms.Padding(4);
            this.pnlTopBtns.Name = "pnlTopBtns";
            this.pnlTopBtns.Size = new System.Drawing.Size(142, 47);
            this.pnlTopBtns.TabIndex = 101;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.pnlTopBtns);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Location = new System.Drawing.Point(1, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(948, 62);
            this.panel4.TabIndex = 154;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(360, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(239, 28);
            this.label10.TabIndex = 149;
            this.label10.Text = "Analyzer Instrument";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(902, 3);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(38, 33);
            this.btnResize.TabIndex = 112;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // Instrument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(950, 566);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.tcoProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Instrument";
            this.Text = "Analyzer Instrument";
            this.Load += new System.EventHandler(this.Instrument_Load);
            this.SizeChanged += new System.EventHandler(this.Instrument_SizeChanged);
            this.tcoProcess.ResumeLayout(false);
            this.tpProcess.ResumeLayout(false);
            this.tpProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceType.Properties)).EndInit();
            this.gbActive.ResumeLayout(false);
            this.gbActive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueFileType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueInstrumentCode.Properties)).EndInit();
            this.tpNewSetting.ResumeLayout(false);
            this.tpNewSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueDataSourceTypeNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueFileTypeNew.Properties)).EndInit();
            this.pnlTopBtns.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.TabControl tcoProcess;
		private System.Windows.Forms.TabPage tpProcess;
		private System.Windows.Forms.Label lblFileType;
		private DevExpress.XtraEditors.LookUpEdit lueInstrumentCode;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label lblTemplateName;
		private System.Windows.Forms.TabPage tpNewSetting;
		private DevExpress.XtraEditors.LookUpEdit lueFileTypeNew;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.LookUpEdit lueDataSourceTypeNew;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblNewInstrumentDescription;
		private System.Windows.Forms.TextBox txtInstrumentDescriptionNew;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtInstrumentDescription;
		private DevExpress.XtraEditors.LookUpEdit lueFileType;
		private DevExpress.XtraEditors.LookUpEdit lueDataSourceType;
		private System.Windows.Forms.Panel pnlTopBtns;
		private System.Windows.Forms.FolderBrowserDialog fbdSelectedDirectory;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtInstrumentCodeNew;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox gbActive;
		private System.Windows.Forms.RadioButton rbInactive;
		private System.Windows.Forms.RadioButton rbActive;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
    }
}