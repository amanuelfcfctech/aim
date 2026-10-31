namespace CFCAIM.Imports
{
    partial class ImportHelpType
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportHelpType));
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(12, 212);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(171, 14);
			this.label2.TabIndex = 30;
			this.label2.Text = "Add New Sample and Results:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 14);
			this.label1.TabIndex = 29;
			this.label1.Text = "Update Results Only:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(12, 318);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(252, 14);
			this.label6.TabIndex = 33;
			this.label6.Text = "Add New Sample/Results OR Update Results:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(25, 10);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(131, 24);
			this.label7.TabIndex = 35;
			this.label7.Text = "Import Types";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Font = new System.Drawing.Font("Arial", 8F);
			this.textBox1.Location = new System.Drawing.Point(41, 33);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(355, 37);
			this.textBox1.TabIndex = 37;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "Only the sample results will be imported.  If the sample does not exist in the sy" +
    "stem, the results will not be imported.";
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Font = new System.Drawing.Font("Arial", 8F);
			this.textBox2.Location = new System.Drawing.Point(41, 229);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(355, 73);
			this.textBox2.TabIndex = 38;
			this.textBox2.TabStop = false;
			this.textBox2.Text = resources.GetString("textBox2.Text");
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.Color.WhiteSmoke;
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Font = new System.Drawing.Font("Arial", 8F);
			this.textBox3.Location = new System.Drawing.Point(41, 335);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(355, 51);
			this.textBox3.TabIndex = 39;
			this.textBox3.TabStop = false;
			this.textBox3.Text = resources.GetString("textBox3.Text");
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.textBox6);
			this.panel1.Controls.Add(this.textBox5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.textBox4);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textBox3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.textBox2);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Location = new System.Drawing.Point(44, 49);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(422, 462);
			this.panel1.TabIndex = 40;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(12, 392);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(101, 14);
			this.label5.TabIndex = 44;
			this.label5.Text = "Composite Batch";
			// 
			// textBox6
			// 
			this.textBox6.BackColor = System.Drawing.Color.WhiteSmoke;
			this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox6.Font = new System.Drawing.Font("Arial", 8F);
			this.textBox6.Location = new System.Drawing.Point(41, 409);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new System.Drawing.Size(355, 50);
			this.textBox6.TabIndex = 45;
			this.textBox6.TabStop = false;
			this.textBox6.Text = "For samples from Romer Labs, when the sample column is blank on the import file, " +
    "this indicates a composite batch. (The import contains samples for one material," +
    " for all venders.)";
			this.textBox6.UseWaitCursor = true;
			// 
			// textBox5
			// 
			this.textBox5.BackColor = System.Drawing.Color.WhiteSmoke;
			this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox5.Font = new System.Drawing.Font("Arial", 8F);
			this.textBox5.Location = new System.Drawing.Point(41, 176);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(355, 23);
			this.textBox5.TabIndex = 43;
			this.textBox5.TabStop = false;
			this.textBox5.Text = "For existing samples, only the sample header fields will be updated.";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
			this.label4.Location = new System.Drawing.Point(12, 158);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(220, 16);
			this.label4.TabIndex = 42;
			this.label4.Text = "Update Sample Header Only:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(12, 81);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(194, 14);
			this.label3.TabIndex = 40;
			this.label3.Text = "Update Results and Header Fields:";
			// 
			// textBox4
			// 
			this.textBox4.BackColor = System.Drawing.Color.WhiteSmoke;
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox4.Font = new System.Drawing.Font("Arial", 8F);
			this.textBox4.Location = new System.Drawing.Point(41, 98);
			this.textBox4.Multiline = true;
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(355, 50);
			this.textBox4.TabIndex = 41;
			this.textBox4.TabStop = false;
			this.textBox4.Text = "For existing samples, the results will be imported, as well as changes to specifi" +
    "c header fields.  AIM required fields (Material, Owner, Owner Location, Supplier" +
    ", Supplier Location) cannot be changed.";
			this.textBox4.UseWaitCursor = true;
			// 
			// ImportHelpType
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(501, 533);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label7);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportHelpType";
			this.Text = "Help";
			this.Leave += new System.EventHandler(this.ImportHelpType_Leave);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox6;
    }
}