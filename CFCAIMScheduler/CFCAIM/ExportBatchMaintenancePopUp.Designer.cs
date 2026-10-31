namespace CFCAIM
{
	partial class ExportBatchMaintenancePopUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportBatchMaintenancePopUp));
            this.btnExit = new System.Windows.Forms.Button();
            this.lblHeaderName = new System.Windows.Forms.Label();
            this.txtBatchName = new System.Windows.Forms.TextBox();
            this.btnAddBatch = new System.Windows.Forms.Button();
            this.rdbSpecificOverlay = new System.Windows.Forms.RadioButton();
            this.rdbDefaultOverlay = new System.Windows.Forms.RadioButton();
            this.rdbBase = new System.Windows.Forms.RadioButton();
            this.grpExportTarget = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbPlantGroup = new System.Windows.Forms.RadioButton();
            this.rbPlantSingle = new System.Windows.Forms.RadioButton();
            this.txtPlantGroupName = new System.Windows.Forms.TextBox();
            this.txtCFCPlantName = new System.Windows.Forms.TextBox();
            this.lblExpand = new System.Windows.Forms.Label();
            this.lblSpecificOverlay = new System.Windows.Forms.Label();
            this.lblBase = new System.Windows.Forms.Label();
            this.lblDefaultOverlay = new System.Windows.Forms.Label();
            this.chkExpand = new System.Windows.Forms.CheckBox();
            this.txtExpandName = new System.Windows.Forms.TextBox();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.rbYes = new System.Windows.Forms.RadioButton();
            this.lueAIMOwnerLocation = new DevExpress.XtraEditors.LookUpEdit();
            this.grpExportTarget.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMOwnerLocation.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(12, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 37);
            this.btnExit.TabIndex = 20;
            this.btnExit.TabStop = false;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblHeaderName
            // 
            this.lblHeaderName.AutoSize = true;
            this.lblHeaderName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderName.Location = new System.Drawing.Point(61, 75);
            this.lblHeaderName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeaderName.Name = "lblHeaderName";
            this.lblHeaderName.Size = new System.Drawing.Size(107, 15);
            this.lblHeaderName.TabIndex = 29;
            this.lblHeaderName.Text = "Enter Batch Name";
            // 
            // txtBatchName
            // 
            this.txtBatchName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchName.Location = new System.Drawing.Point(180, 74);
            this.txtBatchName.Name = "txtBatchName";
            this.txtBatchName.Size = new System.Drawing.Size(234, 21);
            this.txtBatchName.TabIndex = 31;
            // 
            // btnAddBatch
            // 
            this.btnAddBatch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddBatch.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBatch.Image")));
            this.btnAddBatch.Location = new System.Drawing.Point(575, 11);
            this.btnAddBatch.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddBatch.Name = "btnAddBatch";
            this.btnAddBatch.Size = new System.Drawing.Size(57, 37);
            this.btnAddBatch.TabIndex = 34;
            this.btnAddBatch.UseVisualStyleBackColor = true;
            this.btnAddBatch.Click += new System.EventHandler(this.btnAddBatch_Click);
            // 
            // rdbSpecificOverlay
            // 
            this.rdbSpecificOverlay.AutoSize = true;
            this.rdbSpecificOverlay.Enabled = false;
            this.rdbSpecificOverlay.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbSpecificOverlay.Location = new System.Drawing.Point(34, 66);
            this.rdbSpecificOverlay.Margin = new System.Windows.Forms.Padding(2);
            this.rdbSpecificOverlay.Name = "rdbSpecificOverlay";
            this.rdbSpecificOverlay.Size = new System.Drawing.Size(14, 13);
            this.rdbSpecificOverlay.TabIndex = 141;
            this.rdbSpecificOverlay.UseVisualStyleBackColor = true;
            // 
            // rdbDefaultOverlay
            // 
            this.rdbDefaultOverlay.AutoSize = true;
            this.rdbDefaultOverlay.Enabled = false;
            this.rdbDefaultOverlay.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDefaultOverlay.Location = new System.Drawing.Point(34, 45);
            this.rdbDefaultOverlay.Margin = new System.Windows.Forms.Padding(2);
            this.rdbDefaultOverlay.Name = "rdbDefaultOverlay";
            this.rdbDefaultOverlay.Size = new System.Drawing.Size(14, 13);
            this.rdbDefaultOverlay.TabIndex = 140;
            this.rdbDefaultOverlay.UseVisualStyleBackColor = true;
            // 
            // rdbBase
            // 
            this.rdbBase.AutoSize = true;
            this.rdbBase.Checked = true;
            this.rdbBase.Enabled = false;
            this.rdbBase.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbBase.Location = new System.Drawing.Point(34, 24);
            this.rdbBase.Margin = new System.Windows.Forms.Padding(2);
            this.rdbBase.Name = "rdbBase";
            this.rdbBase.Size = new System.Drawing.Size(14, 13);
            this.rdbBase.TabIndex = 139;
            this.rdbBase.TabStop = true;
            this.rdbBase.UseVisualStyleBackColor = true;
            // 
            // grpExportTarget
            // 
            this.grpExportTarget.Controls.Add(this.panel1);
            this.grpExportTarget.Controls.Add(this.txtPlantGroupName);
            this.grpExportTarget.Controls.Add(this.txtCFCPlantName);
            this.grpExportTarget.Controls.Add(this.lblExpand);
            this.grpExportTarget.Controls.Add(this.lblSpecificOverlay);
            this.grpExportTarget.Controls.Add(this.lblBase);
            this.grpExportTarget.Controls.Add(this.lblDefaultOverlay);
            this.grpExportTarget.Controls.Add(this.chkExpand);
            this.grpExportTarget.Controls.Add(this.txtExpandName);
            this.grpExportTarget.Controls.Add(this.rdbSpecificOverlay);
            this.grpExportTarget.Controls.Add(this.rdbDefaultOverlay);
            this.grpExportTarget.Controls.Add(this.rdbBase);
            this.grpExportTarget.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpExportTarget.Location = new System.Drawing.Point(181, 122);
            this.grpExportTarget.Margin = new System.Windows.Forms.Padding(2);
            this.grpExportTarget.Name = "grpExportTarget";
            this.grpExportTarget.Padding = new System.Windows.Forms.Padding(2);
            this.grpExportTarget.Size = new System.Drawing.Size(396, 169);
            this.grpExportTarget.TabIndex = 139;
            this.grpExportTarget.TabStop = false;
            this.grpExportTarget.Text = "Export Target";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbPlantGroup);
            this.panel1.Controls.Add(this.rbPlantSingle);
            this.panel1.Location = new System.Drawing.Point(135, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(89, 41);
            this.panel1.TabIndex = 187;
            // 
            // rbPlantGroup
            // 
            this.rbPlantGroup.AutoSize = true;
            this.rbPlantGroup.Enabled = false;
            this.rbPlantGroup.Font = new System.Drawing.Font("Arial", 7.2F);
            this.rbPlantGroup.Location = new System.Drawing.Point(3, 22);
            this.rbPlantGroup.Margin = new System.Windows.Forms.Padding(2);
            this.rbPlantGroup.Name = "rbPlantGroup";
            this.rbPlantGroup.Size = new System.Drawing.Size(82, 17);
            this.rbPlantGroup.TabIndex = 186;
            this.rbPlantGroup.Text = "Plant Group";
            this.rbPlantGroup.UseVisualStyleBackColor = true;
            // 
            // rbPlantSingle
            // 
            this.rbPlantSingle.AutoSize = true;
            this.rbPlantSingle.Checked = true;
            this.rbPlantSingle.Enabled = false;
            this.rbPlantSingle.Font = new System.Drawing.Font("Arial", 7.2F);
            this.rbPlantSingle.Location = new System.Drawing.Point(3, 3);
            this.rbPlantSingle.Margin = new System.Windows.Forms.Padding(2);
            this.rbPlantSingle.Name = "rbPlantSingle";
            this.rbPlantSingle.Size = new System.Drawing.Size(84, 17);
            this.rbPlantSingle.TabIndex = 185;
            this.rbPlantSingle.TabStop = true;
            this.rbPlantSingle.Text = "Single Plant";
            this.rbPlantSingle.UseVisualStyleBackColor = true;
            // 
            // txtPlantGroupName
            // 
            this.txtPlantGroupName.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlantGroupName.Location = new System.Drawing.Point(226, 102);
            this.txtPlantGroupName.Margin = new System.Windows.Forms.Padding(2);
            this.txtPlantGroupName.Name = "txtPlantGroupName";
            this.txtPlantGroupName.ReadOnly = true;
            this.txtPlantGroupName.Size = new System.Drawing.Size(162, 19);
            this.txtPlantGroupName.TabIndex = 185;
            // 
            // txtCFCPlantName
            // 
            this.txtCFCPlantName.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCFCPlantName.Location = new System.Drawing.Point(226, 82);
            this.txtCFCPlantName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCFCPlantName.Name = "txtCFCPlantName";
            this.txtCFCPlantName.ReadOnly = true;
            this.txtCFCPlantName.Size = new System.Drawing.Size(162, 19);
            this.txtCFCPlantName.TabIndex = 182;
            // 
            // lblExpand
            // 
            this.lblExpand.AutoSize = true;
            this.lblExpand.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpand.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblExpand.Location = new System.Drawing.Point(31, 141);
            this.lblExpand.Name = "lblExpand";
            this.lblExpand.Size = new System.Drawing.Size(46, 14);
            this.lblExpand.TabIndex = 181;
            this.lblExpand.Text = "Expand";
            // 
            // lblSpecificOverlay
            // 
            this.lblSpecificOverlay.AutoSize = true;
            this.lblSpecificOverlay.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpecificOverlay.Location = new System.Drawing.Point(53, 66);
            this.lblSpecificOverlay.Name = "lblSpecificOverlay";
            this.lblSpecificOverlay.Size = new System.Drawing.Size(106, 14);
            this.lblSpecificOverlay.TabIndex = 180;
            this.lblSpecificOverlay.Text = "Specific Overlay For";
            // 
            // lblBase
            // 
            this.lblBase.AutoSize = true;
            this.lblBase.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBase.Location = new System.Drawing.Point(53, 23);
            this.lblBase.Name = "lblBase";
            this.lblBase.Size = new System.Drawing.Size(32, 14);
            this.lblBase.TabIndex = 179;
            this.lblBase.Text = "Base";
            // 
            // lblDefaultOverlay
            // 
            this.lblDefaultOverlay.AutoSize = true;
            this.lblDefaultOverlay.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultOverlay.Location = new System.Drawing.Point(53, 44);
            this.lblDefaultOverlay.Name = "lblDefaultOverlay";
            this.lblDefaultOverlay.Size = new System.Drawing.Size(82, 14);
            this.lblDefaultOverlay.TabIndex = 178;
            this.lblDefaultOverlay.Text = "Default Overlay";
            // 
            // chkExpand
            // 
            this.chkExpand.AutoSize = true;
            this.chkExpand.Enabled = false;
            this.chkExpand.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold);
            this.chkExpand.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.chkExpand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkExpand.Location = new System.Drawing.Point(15, 141);
            this.chkExpand.Name = "chkExpand";
            this.chkExpand.Size = new System.Drawing.Size(15, 14);
            this.chkExpand.TabIndex = 176;
            this.chkExpand.UseVisualStyleBackColor = true;
            // 
            // txtExpandName
            // 
            this.txtExpandName.Enabled = false;
            this.txtExpandName.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpandName.Location = new System.Drawing.Point(88, 140);
            this.txtExpandName.Margin = new System.Windows.Forms.Padding(2);
            this.txtExpandName.Name = "txtExpandName";
            this.txtExpandName.ReadOnly = true;
            this.txtExpandName.Size = new System.Drawing.Size(162, 19);
            this.txtExpandName.TabIndex = 143;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FloralWhite;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFilter.Controls.Add(this.label1);
            this.pnlFilter.Controls.Add(this.rbNo);
            this.pnlFilter.Controls.Add(this.rbYes);
            this.pnlFilter.Location = new System.Drawing.Point(181, 27);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(141, 41);
            this.pnlFilter.TabIndex = 169;
            this.pnlFilter.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 14);
            this.label1.TabIndex = 143;
            this.label1.Text = "Use Filtered Data";
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Checked = true;
            this.rbNo.Font = new System.Drawing.Font("Arial", 7.8F);
            this.rbNo.Location = new System.Drawing.Point(81, 16);
            this.rbNo.Margin = new System.Windows.Forms.Padding(2);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(38, 18);
            this.rbNo.TabIndex = 142;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "No";
            this.rbNo.UseVisualStyleBackColor = true;
            // 
            // rbYes
            // 
            this.rbYes.AutoSize = true;
            this.rbYes.Font = new System.Drawing.Font("Arial", 7.8F);
            this.rbYes.Location = new System.Drawing.Point(33, 16);
            this.rbYes.Margin = new System.Windows.Forms.Padding(2);
            this.rbYes.Name = "rbYes";
            this.rbYes.Size = new System.Drawing.Size(44, 18);
            this.rbYes.TabIndex = 141;
            this.rbYes.Text = "Yes";
            this.rbYes.UseVisualStyleBackColor = true;
            // 
            // lueAIMOwnerLocation
            // 
            this.lueAIMOwnerLocation.Location = new System.Drawing.Point(24, 185);
            this.lueAIMOwnerLocation.Name = "lueAIMOwnerLocation";
            this.lueAIMOwnerLocation.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lueAIMOwnerLocation.Properties.Appearance.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueAIMOwnerLocation.Properties.Appearance.Options.UseBackColor = true;
            this.lueAIMOwnerLocation.Properties.Appearance.Options.UseFont = true;
            this.lueAIMOwnerLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueAIMOwnerLocation.Properties.ReadOnly = true;
            this.lueAIMOwnerLocation.Size = new System.Drawing.Size(12, 20);
            this.lueAIMOwnerLocation.TabIndex = 170;
            this.lueAIMOwnerLocation.Visible = false;
            // 
            // ExportBatchMaintenancePopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(643, 316);
            this.Controls.Add(this.lueAIMOwnerLocation);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.grpExportTarget);
            this.Controls.Add(this.btnAddBatch);
            this.Controls.Add(this.txtBatchName);
            this.Controls.Add(this.lblHeaderName);
            this.Controls.Add(this.btnExit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportBatchMaintenancePopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export Batch Header Name";
            this.Load += new System.EventHandler(this.ExportBatchMaintenancePopUp_Load);
            this.grpExportTarget.ResumeLayout(false);
            this.grpExportTarget.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueAIMOwnerLocation.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lblHeaderName;
		private System.Windows.Forms.TextBox txtBatchName;
		private System.Windows.Forms.Button btnAddBatch;
		private System.Windows.Forms.RadioButton rdbSpecificOverlay;
		private System.Windows.Forms.RadioButton rdbDefaultOverlay;
		private System.Windows.Forms.RadioButton rdbBase;
		private System.Windows.Forms.GroupBox grpExportTarget;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.RadioButton rbNo;
        private System.Windows.Forms.RadioButton rbYes;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lueAIMOwnerLocation;
        private System.Windows.Forms.TextBox txtExpandName;
        private System.Windows.Forms.CheckBox chkExpand;
        private System.Windows.Forms.Label lblExpand;
        private System.Windows.Forms.Label lblSpecificOverlay;
        private System.Windows.Forms.Label lblBase;
        private System.Windows.Forms.Label lblDefaultOverlay;
        private System.Windows.Forms.TextBox txtCFCPlantName;
        private System.Windows.Forms.TextBox txtPlantGroupName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbPlantGroup;
        private System.Windows.Forms.RadioButton rbPlantSingle;
    }
}