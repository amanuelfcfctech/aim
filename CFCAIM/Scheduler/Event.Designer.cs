namespace CFCAIM.Scheduler
{
	partial class Event
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Event));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbWeekly = new System.Windows.Forms.RadioButton();
            this.rbMonthly = new System.Windows.Forms.RadioButton();
            this.rbDaily = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlMonthly = new System.Windows.Forms.Panel();
            this.cmbDay = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkOnAllWeek = new System.Windows.Forms.CheckBox();
            this.chkOnSaturday = new System.Windows.Forms.CheckBox();
            this.chkOnFriday = new System.Windows.Forms.CheckBox();
            this.chkOnThursday = new System.Windows.Forms.CheckBox();
            this.chkOnWednesday = new System.Windows.Forms.CheckBox();
            this.chkOnTuesday = new System.Windows.Forms.CheckBox();
            this.chkOnMonday = new System.Windows.Forms.CheckBox();
            this.chkOnSunday = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbOn = new System.Windows.Forms.RadioButton();
            this.rbDays = new System.Windows.Forms.RadioButton();
            this.chkAllMonths = new System.Windows.Forms.CheckBox();
            this.chkDecember = new System.Windows.Forms.CheckBox();
            this.chkNovember = new System.Windows.Forms.CheckBox();
            this.chkOctober = new System.Windows.Forms.CheckBox();
            this.chkSeptember = new System.Windows.Forms.CheckBox();
            this.chkAugust = new System.Windows.Forms.CheckBox();
            this.chkJuly = new System.Windows.Forms.CheckBox();
            this.chkJune = new System.Windows.Forms.CheckBox();
            this.chkMay = new System.Windows.Forms.CheckBox();
            this.chkApril = new System.Windows.Forms.CheckBox();
            this.chkMarch = new System.Windows.Forms.CheckBox();
            this.chkFebruary = new System.Windows.Forms.CheckBox();
            this.chkJanuary = new System.Windows.Forms.CheckBox();
            this.pnlWeekly = new System.Windows.Forms.Panel();
            this.chkSaturday = new System.Windows.Forms.CheckBox();
            this.chkFriday = new System.Windows.Forms.CheckBox();
            this.chkThursday = new System.Windows.Forms.CheckBox();
            this.chkWednesday = new System.Windows.Forms.CheckBox();
            this.chkTuesday = new System.Windows.Forms.CheckBox();
            this.chkMonday = new System.Windows.Forms.CheckBox();
            this.chkSunday = new System.Windows.Forms.CheckBox();
            this.lblWeek = new System.Windows.Forms.Label();
            this.txtWeeklyRecur = new System.Windows.Forms.TextBox();
            this.lblWeeklyRecur = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.pnlDaily = new System.Windows.Forms.Panel();
            this.lblDays = new System.Windows.Forms.Label();
            this.txtDailyRecur = new System.Windows.Forms.TextBox();
            this.lblDailyRecur = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDay.Properties)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlWeekly.SuspendLayout();
            this.pnlDaily.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbWeekly);
            this.panel1.Controls.Add(this.rbMonthly);
            this.panel1.Controls.Add(this.rbDaily);
            this.panel1.Location = new System.Drawing.Point(15, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 529);
            this.panel1.TabIndex = 0;
            // 
            // rbWeekly
            // 
            this.rbWeekly.AutoSize = true;
            this.rbWeekly.Location = new System.Drawing.Point(17, 57);
            this.rbWeekly.Name = "rbWeekly";
            this.rbWeekly.Size = new System.Drawing.Size(75, 21);
            this.rbWeekly.TabIndex = 2;
            this.rbWeekly.TabStop = true;
            this.rbWeekly.Text = "Weekly";
            this.rbWeekly.UseVisualStyleBackColor = true;
            this.rbWeekly.CheckedChanged += new System.EventHandler(this.rbWeekly_CheckedChanged);
            // 
            // rbMonthly
            // 
            this.rbMonthly.AutoSize = true;
            this.rbMonthly.Location = new System.Drawing.Point(17, 96);
            this.rbMonthly.Name = "rbMonthly";
            this.rbMonthly.Size = new System.Drawing.Size(78, 21);
            this.rbMonthly.TabIndex = 1;
            this.rbMonthly.TabStop = true;
            this.rbMonthly.Text = "Monthly";
            this.rbMonthly.UseVisualStyleBackColor = true;
            this.rbMonthly.CheckedChanged += new System.EventHandler(this.rbMonthly_CheckedChanged);
            // 
            // rbDaily
            // 
            this.rbDaily.AutoSize = true;
            this.rbDaily.Location = new System.Drawing.Point(17, 19);
            this.rbDaily.Name = "rbDaily";
            this.rbDaily.Size = new System.Drawing.Size(60, 21);
            this.rbDaily.TabIndex = 0;
            this.rbDaily.TabStop = true;
            this.rbDaily.Text = "Daily";
            this.rbDaily.UseVisualStyleBackColor = true;
            this.rbDaily.CheckedChanged += new System.EventHandler(this.rbDaily_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnlMonthly);
            this.panel2.Controls.Add(this.pnlWeekly);
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Controls.Add(this.dtpStartDate);
            this.panel2.Controls.Add(this.lblStartDate);
            this.panel2.Location = new System.Drawing.Point(151, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(991, 529);
            this.panel2.TabIndex = 1;
            // 
            // pnlMonthly
            // 
            this.pnlMonthly.Controls.Add(this.cmbDay);
            this.pnlMonthly.Controls.Add(this.panel4);
            this.pnlMonthly.Controls.Add(this.panel3);
            this.pnlMonthly.Controls.Add(this.chkAllMonths);
            this.pnlMonthly.Controls.Add(this.chkDecember);
            this.pnlMonthly.Controls.Add(this.chkNovember);
            this.pnlMonthly.Controls.Add(this.chkOctober);
            this.pnlMonthly.Controls.Add(this.chkSeptember);
            this.pnlMonthly.Controls.Add(this.chkAugust);
            this.pnlMonthly.Controls.Add(this.chkJuly);
            this.pnlMonthly.Controls.Add(this.chkJune);
            this.pnlMonthly.Controls.Add(this.chkMay);
            this.pnlMonthly.Controls.Add(this.chkApril);
            this.pnlMonthly.Controls.Add(this.chkMarch);
            this.pnlMonthly.Controls.Add(this.chkFebruary);
            this.pnlMonthly.Controls.Add(this.chkJanuary);
            this.pnlMonthly.Location = new System.Drawing.Point(8, 57);
            this.pnlMonthly.Name = "pnlMonthly";
            this.pnlMonthly.Size = new System.Drawing.Size(969, 274);
            this.pnlMonthly.TabIndex = 42;
            // 
            // cmbDay
            // 
            this.cmbDay.EditValue = "";
            this.cmbDay.Location = new System.Drawing.Point(117, 105);
            this.cmbDay.Name = "cmbDay";
            this.cmbDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDay.Size = new System.Drawing.Size(154, 22);
            this.cmbDay.TabIndex = 51;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chkOnAllWeek);
            this.panel4.Controls.Add(this.chkOnSaturday);
            this.panel4.Controls.Add(this.chkOnFriday);
            this.panel4.Controls.Add(this.chkOnThursday);
            this.panel4.Controls.Add(this.chkOnWednesday);
            this.panel4.Controls.Add(this.chkOnTuesday);
            this.panel4.Controls.Add(this.chkOnMonday);
            this.panel4.Controls.Add(this.chkOnSunday);
            this.panel4.Location = new System.Drawing.Point(109, 144);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(801, 72);
            this.panel4.TabIndex = 50;
            // 
            // chkOnAllWeek
            // 
            this.chkOnAllWeek.AutoSize = true;
            this.chkOnAllWeek.Location = new System.Drawing.Point(8, 37);
            this.chkOnAllWeek.Name = "chkOnAllWeek";
            this.chkOnAllWeek.Size = new System.Drawing.Size(85, 21);
            this.chkOnAllWeek.TabIndex = 40;
            this.chkOnAllWeek.Text = "All Week";
            this.chkOnAllWeek.UseVisualStyleBackColor = true;
            // 
            // chkOnSaturday
            // 
            this.chkOnSaturday.AutoSize = true;
            this.chkOnSaturday.Location = new System.Drawing.Point(691, 37);
            this.chkOnSaturday.Name = "chkOnSaturday";
            this.chkOnSaturday.Size = new System.Drawing.Size(87, 21);
            this.chkOnSaturday.TabIndex = 39;
            this.chkOnSaturday.Text = "Saturday";
            this.chkOnSaturday.UseVisualStyleBackColor = true;
            // 
            // chkOnFriday
            // 
            this.chkOnFriday.AutoSize = true;
            this.chkOnFriday.Location = new System.Drawing.Point(602, 37);
            this.chkOnFriday.Name = "chkOnFriday";
            this.chkOnFriday.Size = new System.Drawing.Size(69, 21);
            this.chkOnFriday.TabIndex = 38;
            this.chkOnFriday.Text = "Friday";
            this.chkOnFriday.UseVisualStyleBackColor = true;
            // 
            // chkOnThursday
            // 
            this.chkOnThursday.AutoSize = true;
            this.chkOnThursday.Location = new System.Drawing.Point(506, 37);
            this.chkOnThursday.Name = "chkOnThursday";
            this.chkOnThursday.Size = new System.Drawing.Size(90, 21);
            this.chkOnThursday.TabIndex = 37;
            this.chkOnThursday.Text = "Thursday";
            this.chkOnThursday.UseVisualStyleBackColor = true;
            // 
            // chkOnWednesday
            // 
            this.chkOnWednesday.AutoSize = true;
            this.chkOnWednesday.Location = new System.Drawing.Point(389, 37);
            this.chkOnWednesday.Name = "chkOnWednesday";
            this.chkOnWednesday.Size = new System.Drawing.Size(105, 21);
            this.chkOnWednesday.TabIndex = 36;
            this.chkOnWednesday.Text = "Wednesday";
            this.chkOnWednesday.UseVisualStyleBackColor = true;
            // 
            // chkOnTuesday
            // 
            this.chkOnTuesday.AutoSize = true;
            this.chkOnTuesday.Location = new System.Drawing.Point(293, 37);
            this.chkOnTuesday.Name = "chkOnTuesday";
            this.chkOnTuesday.Size = new System.Drawing.Size(85, 21);
            this.chkOnTuesday.TabIndex = 35;
            this.chkOnTuesday.Text = "Tuesday";
            this.chkOnTuesday.UseVisualStyleBackColor = true;
            // 
            // chkOnMonday
            // 
            this.chkOnMonday.AutoSize = true;
            this.chkOnMonday.Location = new System.Drawing.Point(199, 37);
            this.chkOnMonday.Name = "chkOnMonday";
            this.chkOnMonday.Size = new System.Drawing.Size(80, 21);
            this.chkOnMonday.TabIndex = 34;
            this.chkOnMonday.Text = "Monday";
            this.chkOnMonday.UseVisualStyleBackColor = true;
            // 
            // chkOnSunday
            // 
            this.chkOnSunday.AutoSize = true;
            this.chkOnSunday.Location = new System.Drawing.Point(103, 37);
            this.chkOnSunday.Name = "chkOnSunday";
            this.chkOnSunday.Size = new System.Drawing.Size(78, 21);
            this.chkOnSunday.TabIndex = 33;
            this.chkOnSunday.Text = "Sunday";
            this.chkOnSunday.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbOn);
            this.panel3.Controls.Add(this.rbDays);
            this.panel3.Location = new System.Drawing.Point(8, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(95, 97);
            this.panel3.TabIndex = 49;
            // 
            // rbOn
            // 
            this.rbOn.AutoSize = true;
            this.rbOn.Location = new System.Drawing.Point(17, 57);
            this.rbOn.Name = "rbOn";
            this.rbOn.Size = new System.Drawing.Size(48, 21);
            this.rbOn.TabIndex = 2;
            this.rbOn.TabStop = true;
            this.rbOn.Text = "On";
            this.rbOn.UseVisualStyleBackColor = true;
            // 
            // rbDays
            // 
            this.rbDays.AutoSize = true;
            this.rbDays.Location = new System.Drawing.Point(17, 19);
            this.rbDays.Name = "rbDays";
            this.rbDays.Size = new System.Drawing.Size(61, 21);
            this.rbDays.TabIndex = 0;
            this.rbDays.TabStop = true;
            this.rbDays.Text = "Days";
            this.rbDays.UseVisualStyleBackColor = true;
            // 
            // chkAllMonths
            // 
            this.chkAllMonths.AutoSize = true;
            this.chkAllMonths.Location = new System.Drawing.Point(15, 9);
            this.chkAllMonths.Name = "chkAllMonths";
            this.chkAllMonths.Size = new System.Drawing.Size(88, 21);
            this.chkAllMonths.TabIndex = 48;
            this.chkAllMonths.Text = "All Month";
            this.chkAllMonths.UseVisualStyleBackColor = true;
            // 
            // chkDecember
            // 
            this.chkDecember.AutoSize = true;
            this.chkDecember.Location = new System.Drawing.Point(645, 51);
            this.chkDecember.Name = "chkDecember";
            this.chkDecember.Size = new System.Drawing.Size(95, 21);
            this.chkDecember.TabIndex = 47;
            this.chkDecember.Text = "December";
            this.chkDecember.UseVisualStyleBackColor = true;
            // 
            // chkNovember
            // 
            this.chkNovember.AutoSize = true;
            this.chkNovember.Location = new System.Drawing.Point(534, 51);
            this.chkNovember.Name = "chkNovember";
            this.chkNovember.Size = new System.Drawing.Size(95, 21);
            this.chkNovember.TabIndex = 46;
            this.chkNovember.Text = "November";
            this.chkNovember.UseVisualStyleBackColor = true;
            // 
            // chkOctober
            // 
            this.chkOctober.AutoSize = true;
            this.chkOctober.Location = new System.Drawing.Point(434, 51);
            this.chkOctober.Name = "chkOctober";
            this.chkOctober.Size = new System.Drawing.Size(81, 21);
            this.chkOctober.TabIndex = 45;
            this.chkOctober.Text = "October";
            this.chkOctober.UseVisualStyleBackColor = true;
            // 
            // chkSeptember
            // 
            this.chkSeptember.AutoSize = true;
            this.chkSeptember.Location = new System.Drawing.Point(327, 51);
            this.chkSeptember.Name = "chkSeptember";
            this.chkSeptember.Size = new System.Drawing.Size(99, 21);
            this.chkSeptember.TabIndex = 44;
            this.chkSeptember.Text = "September";
            this.chkSeptember.UseVisualStyleBackColor = true;
            // 
            // chkAugust
            // 
            this.chkAugust.AutoSize = true;
            this.chkAugust.Location = new System.Drawing.Point(226, 51);
            this.chkAugust.Name = "chkAugust";
            this.chkAugust.Size = new System.Drawing.Size(74, 21);
            this.chkAugust.TabIndex = 43;
            this.chkAugust.Text = "August";
            this.chkAugust.UseVisualStyleBackColor = true;
            // 
            // chkJuly
            // 
            this.chkJuly.AutoSize = true;
            this.chkJuly.Location = new System.Drawing.Point(125, 51);
            this.chkJuly.Name = "chkJuly";
            this.chkJuly.Size = new System.Drawing.Size(55, 21);
            this.chkJuly.TabIndex = 42;
            this.chkJuly.Text = "July";
            this.chkJuly.UseVisualStyleBackColor = true;
            // 
            // chkJune
            // 
            this.chkJune.AutoSize = true;
            this.chkJune.Location = new System.Drawing.Point(645, 8);
            this.chkJune.Name = "chkJune";
            this.chkJune.Size = new System.Drawing.Size(61, 21);
            this.chkJune.TabIndex = 41;
            this.chkJune.Text = "June";
            this.chkJune.UseVisualStyleBackColor = true;
            // 
            // chkMay
            // 
            this.chkMay.AutoSize = true;
            this.chkMay.Location = new System.Drawing.Point(534, 8);
            this.chkMay.Name = "chkMay";
            this.chkMay.Size = new System.Drawing.Size(56, 21);
            this.chkMay.TabIndex = 40;
            this.chkMay.Text = "May";
            this.chkMay.UseVisualStyleBackColor = true;
            // 
            // chkApril
            // 
            this.chkApril.AutoSize = true;
            this.chkApril.Location = new System.Drawing.Point(434, 8);
            this.chkApril.Name = "chkApril";
            this.chkApril.Size = new System.Drawing.Size(58, 21);
            this.chkApril.TabIndex = 36;
            this.chkApril.Text = "April";
            this.chkApril.UseVisualStyleBackColor = true;
            // 
            // chkMarch
            // 
            this.chkMarch.AutoSize = true;
            this.chkMarch.Location = new System.Drawing.Point(327, 8);
            this.chkMarch.Name = "chkMarch";
            this.chkMarch.Size = new System.Drawing.Size(69, 21);
            this.chkMarch.TabIndex = 35;
            this.chkMarch.Text = "March";
            this.chkMarch.UseVisualStyleBackColor = true;
            // 
            // chkFebruary
            // 
            this.chkFebruary.AutoSize = true;
            this.chkFebruary.Location = new System.Drawing.Point(226, 8);
            this.chkFebruary.Name = "chkFebruary";
            this.chkFebruary.Size = new System.Drawing.Size(87, 21);
            this.chkFebruary.TabIndex = 34;
            this.chkFebruary.Text = "February";
            this.chkFebruary.UseVisualStyleBackColor = true;
            // 
            // chkJanuary
            // 
            this.chkJanuary.AutoSize = true;
            this.chkJanuary.Location = new System.Drawing.Point(125, 8);
            this.chkJanuary.Name = "chkJanuary";
            this.chkJanuary.Size = new System.Drawing.Size(81, 21);
            this.chkJanuary.TabIndex = 33;
            this.chkJanuary.Text = "January";
            this.chkJanuary.UseVisualStyleBackColor = true;
            // 
            // pnlWeekly
            // 
            this.pnlWeekly.Controls.Add(this.chkSaturday);
            this.pnlWeekly.Controls.Add(this.chkFriday);
            this.pnlWeekly.Controls.Add(this.chkThursday);
            this.pnlWeekly.Controls.Add(this.chkWednesday);
            this.pnlWeekly.Controls.Add(this.chkTuesday);
            this.pnlWeekly.Controls.Add(this.chkMonday);
            this.pnlWeekly.Controls.Add(this.chkSunday);
            this.pnlWeekly.Controls.Add(this.lblWeek);
            this.pnlWeekly.Controls.Add(this.txtWeeklyRecur);
            this.pnlWeekly.Controls.Add(this.lblWeeklyRecur);
            this.pnlWeekly.Location = new System.Drawing.Point(8, 60);
            this.pnlWeekly.Name = "pnlWeekly";
            this.pnlWeekly.Size = new System.Drawing.Size(975, 289);
            this.pnlWeekly.TabIndex = 34;
            // 
            // chkSaturday
            // 
            this.chkSaturday.AutoSize = true;
            this.chkSaturday.Location = new System.Drawing.Point(215, 102);
            this.chkSaturday.Name = "chkSaturday";
            this.chkSaturday.Size = new System.Drawing.Size(87, 21);
            this.chkSaturday.TabIndex = 39;
            this.chkSaturday.Text = "Saturday";
            this.chkSaturday.UseVisualStyleBackColor = true;
            // 
            // chkFriday
            // 
            this.chkFriday.AutoSize = true;
            this.chkFriday.Location = new System.Drawing.Point(121, 102);
            this.chkFriday.Name = "chkFriday";
            this.chkFriday.Size = new System.Drawing.Size(69, 21);
            this.chkFriday.TabIndex = 38;
            this.chkFriday.Text = "Friday";
            this.chkFriday.UseVisualStyleBackColor = true;
            // 
            // chkThursday
            // 
            this.chkThursday.AutoSize = true;
            this.chkThursday.Location = new System.Drawing.Point(25, 102);
            this.chkThursday.Name = "chkThursday";
            this.chkThursday.Size = new System.Drawing.Size(90, 21);
            this.chkThursday.TabIndex = 37;
            this.chkThursday.Text = "Thursday";
            this.chkThursday.UseVisualStyleBackColor = true;
            // 
            // chkWednesday
            // 
            this.chkWednesday.AutoSize = true;
            this.chkWednesday.Location = new System.Drawing.Point(311, 60);
            this.chkWednesday.Name = "chkWednesday";
            this.chkWednesday.Size = new System.Drawing.Size(105, 21);
            this.chkWednesday.TabIndex = 36;
            this.chkWednesday.Text = "Wednesday";
            this.chkWednesday.UseVisualStyleBackColor = true;
            // 
            // chkTuesday
            // 
            this.chkTuesday.AutoSize = true;
            this.chkTuesday.Location = new System.Drawing.Point(215, 60);
            this.chkTuesday.Name = "chkTuesday";
            this.chkTuesday.Size = new System.Drawing.Size(85, 21);
            this.chkTuesday.TabIndex = 35;
            this.chkTuesday.Text = "Tuesday";
            this.chkTuesday.UseVisualStyleBackColor = true;
            // 
            // chkMonday
            // 
            this.chkMonday.AutoSize = true;
            this.chkMonday.Location = new System.Drawing.Point(121, 60);
            this.chkMonday.Name = "chkMonday";
            this.chkMonday.Size = new System.Drawing.Size(80, 21);
            this.chkMonday.TabIndex = 34;
            this.chkMonday.Text = "Monday";
            this.chkMonday.UseVisualStyleBackColor = true;
            // 
            // chkSunday
            // 
            this.chkSunday.AutoSize = true;
            this.chkSunday.Location = new System.Drawing.Point(25, 60);
            this.chkSunday.Name = "chkSunday";
            this.chkSunday.Size = new System.Drawing.Size(78, 21);
            this.chkSunday.TabIndex = 33;
            this.chkSunday.Text = "Sunday";
            this.chkSunday.UseVisualStyleBackColor = true;
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeek.Location = new System.Drawing.Point(262, 15);
            this.lblWeek.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(75, 18);
            this.lblWeek.TabIndex = 32;
            this.lblWeek.Text = "weeks on";
            // 
            // txtWeeklyRecur
            // 
            this.txtWeeklyRecur.Location = new System.Drawing.Point(143, 13);
            this.txtWeeklyRecur.Name = "txtWeeklyRecur";
            this.txtWeeklyRecur.Size = new System.Drawing.Size(112, 22);
            this.txtWeeklyRecur.TabIndex = 31;
            // 
            // lblWeeklyRecur
            // 
            this.lblWeeklyRecur.AutoSize = true;
            this.lblWeeklyRecur.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeeklyRecur.Location = new System.Drawing.Point(38, 17);
            this.lblWeeklyRecur.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeeklyRecur.Name = "lblWeeklyRecur";
            this.lblWeeklyRecur.Size = new System.Drawing.Size(96, 18);
            this.lblWeeklyRecur.TabIndex = 30;
            this.lblWeeklyRecur.Text = "Recur Every";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(195, 18);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(135, 20);
            this.listBox1.TabIndex = 32;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(92, 15);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(96, 25);
            this.dtpStartDate.TabIndex = 31;
            this.dtpStartDate.Value = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(38, 18);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(42, 18);
            this.lblStartDate.TabIndex = 30;
            this.lblStartDate.Text = "Start";
            // 
            // pnlDaily
            // 
            this.pnlDaily.Controls.Add(this.lblDays);
            this.pnlDaily.Controls.Add(this.txtDailyRecur);
            this.pnlDaily.Controls.Add(this.lblDailyRecur);
            this.pnlDaily.Location = new System.Drawing.Point(162, 79);
            this.pnlDaily.Name = "pnlDaily";
            this.pnlDaily.Size = new System.Drawing.Size(930, 103);
            this.pnlDaily.TabIndex = 2;
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDays.Location = new System.Drawing.Point(262, 15);
            this.lblDays.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(42, 18);
            this.lblDays.TabIndex = 32;
            this.lblDays.Text = "Days";
            // 
            // txtDailyRecur
            // 
            this.txtDailyRecur.Location = new System.Drawing.Point(143, 13);
            this.txtDailyRecur.Name = "txtDailyRecur";
            this.txtDailyRecur.Size = new System.Drawing.Size(112, 22);
            this.txtDailyRecur.TabIndex = 31;
            // 
            // lblDailyRecur
            // 
            this.lblDailyRecur.AutoSize = true;
            this.lblDailyRecur.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDailyRecur.Location = new System.Drawing.Point(38, 17);
            this.lblDailyRecur.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDailyRecur.Name = "lblDailyRecur";
            this.lblDailyRecur.Size = new System.Drawing.Size(96, 18);
            this.lblDailyRecur.TabIndex = 30;
            this.lblDailyRecur.Text = "Recur Every";
            // 
            // Event
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 563);
            this.Controls.Add(this.pnlDaily);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Event";
            this.Text = "Event";
            this.Load += new System.EventHandler(this.Event_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlMonthly.ResumeLayout(false);
            this.pnlMonthly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDay.Properties)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlWeekly.ResumeLayout(false);
            this.pnlWeekly.PerformLayout();
            this.pnlDaily.ResumeLayout(false);
            this.pnlDaily.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton rbWeekly;
		private System.Windows.Forms.RadioButton rbMonthly;
		private System.Windows.Forms.RadioButton rbDaily;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.DateTimePicker dtpStartDate;
		private System.Windows.Forms.Label lblStartDate;
		private System.Windows.Forms.Panel pnlDaily;
		private System.Windows.Forms.Label lblDailyRecur;
		private System.Windows.Forms.Label lblDays;
		private System.Windows.Forms.TextBox txtDailyRecur;
		private System.Windows.Forms.Panel pnlMonthly;
		private DevExpress.XtraEditors.CheckedComboBoxEdit cmbDay;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.CheckBox chkOnAllWeek;
		private System.Windows.Forms.CheckBox chkOnSaturday;
		private System.Windows.Forms.CheckBox chkOnFriday;
		private System.Windows.Forms.CheckBox chkOnThursday;
		private System.Windows.Forms.CheckBox chkOnWednesday;
		private System.Windows.Forms.CheckBox chkOnTuesday;
		private System.Windows.Forms.CheckBox chkOnMonday;
		private System.Windows.Forms.CheckBox chkOnSunday;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.RadioButton rbOn;
		private System.Windows.Forms.RadioButton rbDays;
		private System.Windows.Forms.CheckBox chkAllMonths;
		private System.Windows.Forms.CheckBox chkDecember;
		private System.Windows.Forms.CheckBox chkNovember;
		private System.Windows.Forms.CheckBox chkOctober;
		private System.Windows.Forms.CheckBox chkSeptember;
		private System.Windows.Forms.CheckBox chkAugust;
		private System.Windows.Forms.CheckBox chkJuly;
		private System.Windows.Forms.CheckBox chkJune;
		private System.Windows.Forms.CheckBox chkMay;
		private System.Windows.Forms.CheckBox chkApril;
		private System.Windows.Forms.CheckBox chkMarch;
		private System.Windows.Forms.CheckBox chkFebruary;
		private System.Windows.Forms.CheckBox chkJanuary;
		private System.Windows.Forms.Panel pnlWeekly;
		private System.Windows.Forms.CheckBox chkSaturday;
		private System.Windows.Forms.CheckBox chkFriday;
		private System.Windows.Forms.CheckBox chkThursday;
		private System.Windows.Forms.CheckBox chkWednesday;
		private System.Windows.Forms.CheckBox chkTuesday;
		private System.Windows.Forms.CheckBox chkMonday;
		private System.Windows.Forms.CheckBox chkSunday;
		private System.Windows.Forms.Label lblWeek;
		private System.Windows.Forms.TextBox txtWeeklyRecur;
		private System.Windows.Forms.Label lblWeeklyRecur;
	}
}