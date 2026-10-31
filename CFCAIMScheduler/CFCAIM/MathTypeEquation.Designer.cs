namespace CFCAIM
{
	partial class MathTypeEquation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MathTypeEquation));
            this.gbEquation = new System.Windows.Forms.GroupBox();
            this.txtBuild = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.gbTest = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblMean = new System.Windows.Forms.Label();
            this.txtEval = new System.Windows.Forms.TextBox();
            this.txtMean = new System.Windows.Forms.TextBox();
            this.lblSD = new System.Windows.Forms.Label();
            this.txtSD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnParenFront = new System.Windows.Forms.Button();
            this.btnDivide = new System.Windows.Forms.Button();
            this.btnMean = new System.Windows.Forms.Button();
            this.btnParenBack = new System.Windows.Forms.Button();
            this.btnMultiply = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnSD = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnResize = new System.Windows.Forms.Button();
            this.gbEquation.SuspendLayout();
            this.gbTest.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEquation
            // 
            this.gbEquation.Controls.Add(this.txtBuild);
            this.gbEquation.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gbEquation.Location = new System.Drawing.Point(24, 111);
            this.gbEquation.Margin = new System.Windows.Forms.Padding(2);
            this.gbEquation.Name = "gbEquation";
            this.gbEquation.Padding = new System.Windows.Forms.Padding(2);
            this.gbEquation.Size = new System.Drawing.Size(421, 75);
            this.gbEquation.TabIndex = 79;
            this.gbEquation.TabStop = false;
            this.gbEquation.Text = "Edit Equation";
            // 
            // txtBuild
            // 
            this.txtBuild.Font = new System.Drawing.Font("Arial", 9F);
            this.txtBuild.Location = new System.Drawing.Point(94, 32);
            this.txtBuild.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuild.MaxLength = 200;
            this.txtBuild.Name = "txtBuild";
            this.txtBuild.Size = new System.Drawing.Size(300, 21);
            this.txtBuild.TabIndex = 65;
            this.txtBuild.TextChanged += new System.EventHandler(this.txtBuild_TextChanged);
            this.txtBuild.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuild_KeyDown);
            this.txtBuild.Leave += new System.EventHandler(this.txtBuild_Leave);
            this.txtBuild.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtBuild_MouseDown);
            this.txtBuild.MouseLeave += new System.EventHandler(this.txtBuild_MouseLeave);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::CFCAIM.Properties.Resources.save;
            this.btnSave.Location = new System.Drawing.Point(95, 24);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 35);
            this.btnSave.TabIndex = 80;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblResult.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblResult.Location = new System.Drawing.Point(352, 23);
            this.lblResult.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 15);
            this.lblResult.TabIndex = 79;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(29, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 82;
            this.label2.Text = "Math Type Name";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Arial", 9F);
            this.txtName.Location = new System.Drawing.Point(140, 68);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.MaxLength = 30;
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(182, 14);
            this.txtName.TabIndex = 81;
            // 
            // gbTest
            // 
            this.gbTest.Controls.Add(this.lblResult);
            this.gbTest.Controls.Add(this.btnTest);
            this.gbTest.Controls.Add(this.lblMean);
            this.gbTest.Controls.Add(this.txtEval);
            this.gbTest.Controls.Add(this.txtMean);
            this.gbTest.Controls.Add(this.lblSD);
            this.gbTest.Controls.Add(this.txtSD);
            this.gbTest.Controls.Add(this.label1);
            this.gbTest.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gbTest.Location = new System.Drawing.Point(24, 199);
            this.gbTest.Margin = new System.Windows.Forms.Padding(2);
            this.gbTest.Name = "gbTest";
            this.gbTest.Padding = new System.Windows.Forms.Padding(2);
            this.gbTest.Size = new System.Drawing.Size(616, 75);
            this.gbTest.TabIndex = 80;
            this.gbTest.TabStop = false;
            this.gbTest.Text = "Test Equation";
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTest.Image = global::CFCAIM.Properties.Resources.preview_32x32;
            this.btnTest.Location = new System.Drawing.Point(95, 24);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(40, 35);
            this.btnTest.TabIndex = 75;
            this.btnTest.TabStop = false;
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblMean
            // 
            this.lblMean.AutoSize = true;
            this.lblMean.Font = new System.Drawing.Font("Arial", 8F);
            this.lblMean.Location = new System.Drawing.Point(150, 24);
            this.lblMean.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMean.Name = "lblMean";
            this.lblMean.Size = new System.Drawing.Size(33, 14);
            this.lblMean.TabIndex = 62;
            this.lblMean.Text = "Mean";
            // 
            // txtEval
            // 
            this.txtEval.Font = new System.Drawing.Font("Arial", 9F);
            this.txtEval.Location = new System.Drawing.Point(350, 44);
            this.txtEval.Margin = new System.Windows.Forms.Padding(2);
            this.txtEval.MaxLength = 200;
            this.txtEval.Name = "txtEval";
            this.txtEval.ReadOnly = true;
            this.txtEval.Size = new System.Drawing.Size(258, 21);
            this.txtEval.TabIndex = 77;
            // 
            // txtMean
            // 
            this.txtMean.Font = new System.Drawing.Font("Arial", 9F);
            this.txtMean.Location = new System.Drawing.Point(189, 20);
            this.txtMean.Margin = new System.Windows.Forms.Padding(2);
            this.txtMean.MaxLength = 30;
            this.txtMean.Name = "txtMean";
            this.txtMean.Size = new System.Drawing.Size(56, 21);
            this.txtMean.TabIndex = 61;
            this.txtMean.Text = "0";
            this.txtMean.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSD
            // 
            this.lblSD.AutoSize = true;
            this.lblSD.Font = new System.Drawing.Font("Arial", 8F);
            this.lblSD.Location = new System.Drawing.Point(150, 46);
            this.lblSD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSD.Name = "lblSD";
            this.lblSD.Size = new System.Drawing.Size(21, 14);
            this.lblSD.TabIndex = 64;
            this.lblSD.Text = "SD";
            // 
            // txtSD
            // 
            this.txtSD.Font = new System.Drawing.Font("Arial", 9F);
            this.txtSD.Location = new System.Drawing.Point(189, 43);
            this.txtSD.Margin = new System.Windows.Forms.Padding(2);
            this.txtSD.MaxLength = 30;
            this.txtSD.Name = "txtSD";
            this.txtSD.Size = new System.Drawing.Size(56, 21);
            this.txtSD.TabIndex = 63;
            this.txtSD.Text = "0";
            this.txtSD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8F);
            this.label1.Location = new System.Drawing.Point(300, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 14);
            this.label1.TabIndex = 78;
            this.label1.Text = "Result";
            // 
            // btnParenFront
            // 
            this.btnParenFront.Location = new System.Drawing.Point(4, 10);
            this.btnParenFront.Margin = new System.Windows.Forms.Padding(2);
            this.btnParenFront.Name = "btnParenFront";
            this.btnParenFront.Size = new System.Drawing.Size(22, 21);
            this.btnParenFront.TabIndex = 81;
            this.btnParenFront.TabStop = false;
            this.btnParenFront.Text = "(";
            this.btnParenFront.UseVisualStyleBackColor = true;
            this.btnParenFront.Click += new System.EventHandler(this.btnParenFront_Click);
            // 
            // btnDivide
            // 
            this.btnDivide.Location = new System.Drawing.Point(137, 10);
            this.btnDivide.Margin = new System.Windows.Forms.Padding(2);
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(22, 21);
            this.btnDivide.TabIndex = 86;
            this.btnDivide.TabStop = false;
            this.btnDivide.Text = "/";
            this.btnDivide.UseVisualStyleBackColor = true;
            this.btnDivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // btnMean
            // 
            this.btnMean.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMean.Location = new System.Drawing.Point(4, 32);
            this.btnMean.Margin = new System.Windows.Forms.Padding(2);
            this.btnMean.Name = "btnMean";
            this.btnMean.Size = new System.Drawing.Size(49, 21);
            this.btnMean.TabIndex = 87;
            this.btnMean.TabStop = false;
            this.btnMean.Text = "MEAN";
            this.btnMean.UseVisualStyleBackColor = true;
            this.btnMean.Click += new System.EventHandler(this.btnMean_Click);
            // 
            // btnParenBack
            // 
            this.btnParenBack.Location = new System.Drawing.Point(31, 10);
            this.btnParenBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnParenBack.Name = "btnParenBack";
            this.btnParenBack.Size = new System.Drawing.Size(22, 21);
            this.btnParenBack.TabIndex = 82;
            this.btnParenBack.TabStop = false;
            this.btnParenBack.Text = ")";
            this.btnParenBack.UseVisualStyleBackColor = true;
            this.btnParenBack.Click += new System.EventHandler(this.btnParenBack_Click);
            // 
            // btnMultiply
            // 
            this.btnMultiply.Location = new System.Drawing.Point(110, 10);
            this.btnMultiply.Margin = new System.Windows.Forms.Padding(2);
            this.btnMultiply.Name = "btnMultiply";
            this.btnMultiply.Size = new System.Drawing.Size(22, 21);
            this.btnMultiply.TabIndex = 85;
            this.btnMultiply.TabStop = false;
            this.btnMultiply.Text = "x";
            this.btnMultiply.UseVisualStyleBackColor = true;
            this.btnMultiply.Click += new System.EventHandler(this.btnMultiply_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(57, 10);
            this.btnPlus.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(22, 21);
            this.btnPlus.TabIndex = 83;
            this.btnPlus.TabStop = false;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnSD
            // 
            this.btnSD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSD.Location = new System.Drawing.Point(57, 32);
            this.btnSD.Margin = new System.Windows.Forms.Padding(2);
            this.btnSD.Name = "btnSD";
            this.btnSD.Size = new System.Drawing.Size(49, 21);
            this.btnSD.TabIndex = 88;
            this.btnSD.TabStop = false;
            this.btnSD.Text = "SD";
            this.btnSD.UseVisualStyleBackColor = true;
            this.btnSD.Click += new System.EventHandler(this.btnSD_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Image = global::CFCAIM.Properties.Resources.Undo_icon;
            this.btnRemove.Location = new System.Drawing.Point(110, 32);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(22, 21);
            this.btnRemove.TabIndex = 89;
            this.btnRemove.TabStop = false;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(83, 10);
            this.btnMinus.Margin = new System.Windows.Forms.Padding(2);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(22, 21);
            this.btnMinus.TabIndex = 84;
            this.btnMinus.TabStop = false;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(2, 5);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(50, 37);
            this.btnExit.TabIndex = 90;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnParenFront);
            this.groupBox1.Controls.Add(this.btnParenBack);
            this.groupBox1.Controls.Add(this.btnMultiply);
            this.groupBox1.Controls.Add(this.btnMinus);
            this.groupBox1.Controls.Add(this.btnPlus);
            this.groupBox1.Controls.Add(this.btnMean);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnSD);
            this.groupBox1.Controls.Add(this.btnDivide);
            this.groupBox1.Location = new System.Drawing.Point(471, 111);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(170, 75);
            this.groupBox1.TabIndex = 91;
            this.groupBox1.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(111, 53);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(49, 19);
            this.btnClear.TabIndex = 90;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(25, 286);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(616, 75);
            this.groupBox2.TabIndex = 92;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Save Equation";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.DarkBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.btnResize);
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Location = new System.Drawing.Point(1, 1);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(656, 52);
            this.panel4.TabIndex = 157;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(203, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(238, 23);
            this.label10.TabIndex = 149;
            this.label10.Text = "Edit Math Type Equation";
            // 
            // btnResize
            // 
            this.btnResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResize.Image = global::CFCAIM.Properties.Resources.back_icon;
            this.btnResize.Location = new System.Drawing.Point(617, 0);
            this.btnResize.Margin = new System.Windows.Forms.Padding(2);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(34, 34);
            this.btnResize.TabIndex = 112;
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Visible = false;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // MathTypeEquation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 374);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.gbEquation);
            this.Controls.Add(this.gbTest);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MathTypeEquation";
            this.Text = "Edit Math Type Equation";
            this.Load += new System.EventHandler(this.MathTypeEquation_Load);
            this.SizeChanged += new System.EventHandler(this.MathTypeEquation_SizeChanged);
            this.gbEquation.ResumeLayout(false);
            this.gbEquation.PerformLayout();
            this.gbTest.ResumeLayout(false);
            this.gbTest.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gbEquation;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.TextBox txtBuild;
		private System.Windows.Forms.GroupBox gbTest;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Label lblMean;
		private System.Windows.Forms.TextBox txtEval;
		private System.Windows.Forms.TextBox txtMean;
		private System.Windows.Forms.Label lblSD;
		private System.Windows.Forms.TextBox txtSD;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnParenFront;
		private System.Windows.Forms.Button btnDivide;
		private System.Windows.Forms.Button btnMean;
		private System.Windows.Forms.Button btnParenBack;
		private System.Windows.Forms.Button btnMultiply;
		private System.Windows.Forms.Button btnPlus;
		private System.Windows.Forms.Button btnSD;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnMinus;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnResize;
	}
}