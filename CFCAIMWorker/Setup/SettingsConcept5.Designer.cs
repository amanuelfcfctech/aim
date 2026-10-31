namespace CFCAIMWorker.Setup
{
    partial class SettingsConcept5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsConcept5));
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtFormulationPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDatabaseConnection = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lueSyncType = new DevExpress.XtraEditors.LookUpEdit();
            this.btnBrowseFormulationPath = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueSyncType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Location = new System.Drawing.Point(-1, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(802, 53);
            this.panel4.TabIndex = 157;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::CFCAIMWorker.Properties.Resources.save_32x32;
            this.btnSave.Location = new System.Drawing.Point(72, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 37);
            this.btnSave.TabIndex = 171;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.label10.Size = new System.Drawing.Size(181, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Concept5 Settings";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(18, 5);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 37);
            this.btnExit.TabIndex = 20;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtFormulationPath
            // 
            this.txtFormulationPath.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormulationPath.Location = new System.Drawing.Point(224, 92);
            this.txtFormulationPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtFormulationPath.Name = "txtFormulationPath";
            this.txtFormulationPath.Size = new System.Drawing.Size(356, 21);
            this.txtFormulationPath.TabIndex = 162;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 18);
            this.label1.TabIndex = 164;
            this.label1.Text = "Formulation Path";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDatabaseConnection
            // 
            this.txtDatabaseConnection.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabaseConnection.Location = new System.Drawing.Point(224, 131);
            this.txtDatabaseConnection.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatabaseConnection.Name = "txtDatabaseConnection";
            this.txtDatabaseConnection.Size = new System.Drawing.Size(356, 21);
            this.txtDatabaseConnection.TabIndex = 165;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 18);
            this.label2.TabIndex = 166;
            this.label2.Text = "Database Connection";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(81, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 18);
            this.label3.TabIndex = 168;
            this.label3.Text = "Full or Quick Sync";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lueSyncType
            // 
            this.lueSyncType.Location = new System.Drawing.Point(224, 170);
            this.lueSyncType.Name = "lueSyncType";
            this.lueSyncType.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.lueSyncType.Properties.Appearance.Options.UseFont = true;
            this.lueSyncType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueSyncType.Size = new System.Drawing.Size(101, 22);
            this.lueSyncType.TabIndex = 169;
            // 
            // btnBrowseFormulationPath
            // 
            this.btnBrowseFormulationPath.Font = new System.Drawing.Font("Arial", 8F);
            this.btnBrowseFormulationPath.Image = global::CFCAIMWorker.Properties.Resources.browse;
            this.btnBrowseFormulationPath.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowseFormulationPath.Location = new System.Drawing.Point(583, 87);
            this.btnBrowseFormulationPath.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowseFormulationPath.Name = "btnBrowseFormulationPath";
            this.btnBrowseFormulationPath.Size = new System.Drawing.Size(89, 31);
            this.btnBrowseFormulationPath.TabIndex = 170;
            this.btnBrowseFormulationPath.Text = "Browse";
            this.btnBrowseFormulationPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowseFormulationPath.UseVisualStyleBackColor = true;
            this.btnBrowseFormulationPath.Click += new System.EventHandler(this.btnBrowseFormulationPath_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(19, 400);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(96, 20);
            this.txtID.TabIndex = 175;
            this.txtID.Visible = false;
            // 
            // SettingsConcept5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnBrowseFormulationPath);
            this.Controls.Add(this.lueSyncType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatabaseConnection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFormulationPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsConcept5";
            this.Text = "Concept5 Settings";
            this.Load += new System.EventHandler(this.SettingsConcept5_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueSyncType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtFormulationPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDatabaseConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.LookUpEdit lueSyncType;
        private System.Windows.Forms.Button btnBrowseFormulationPath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtID;
    }
}