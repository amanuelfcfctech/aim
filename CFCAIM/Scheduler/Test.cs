using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CFCAIM.Maintenance
{
	public partial class Test : Form
	{
		public Test()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
			Setup();
		}

		void Setup()
		{


			/*
			 http://www.codeproject.com/Answers/207268/how-to-dock-button-so-that-it-can-adjust-with-the#answer1
			 http://www.codeproject.com/Answers/150899/Zom-Out-malfunctions-when-Screen-resolution-change#answer1
			 * 
			 * 
			 You always need to use Docking technique if you want to make consistent form Layout. 
			 You will also need plenty of Panels. I'll show this all in code, without Designer, 
			 so you would see how it is really done:

using System;
using System.Drawing;
using System.Windows.Forms;
 
public partial class FormMain : Form {
    
	public FormMain() {
		InitializeComponent();
		Setup();
	} //FormMain

	void Setup() {
		Panel left = new Panel();
		this.Padding = new Padding(10);
		left.BackColor = Color.Yellow;
		left.Dock = DockStyle.Left;
			 
		Panel right = new Panel();
		right.BackColor = Color.Wheat;
		right.Dock = DockStyle.Fill;
		right.Padding = new Padding(4);
			 
		Button button = new Button();
		button.Dock = DockStyle.Bottom;
		button.Text = "&Click it!";
		button.Click += delegate(object sender, EventArgs eventArgs) {MessageBox.Show("Clicked!");	};
		button.BackColor = Color.LightGray;
			 
		ListBox lb = new ListBox();
		lb.Dock = DockStyle.Fill;
		right.Controls.AddRange(new Control[] { lb, button, });
		this.Controls.Add(right);
		this.Controls.Add(left);
	} //Setup

	[STAThread]
	static void Main() {
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new FormMain());
	} //Main (entry point)

} //class FormMain

 I used colors to show what control goes where. IUsing colors on temporary bases is also a good tool 
 during design phase. It is especially useful to design proper padding. Later on, you will remove the colors.

 For further design directions, please see my past solutions:
	GUI Apperance - C#.Net[^],
	Zom Out malfunctions when Screen resolution changes[^],
	Code behind class implementation[^].

			 
			*/


			//Panel left = new Panel();
			//this.Padding = new Padding(10);
			//left.BackColor = Color.Yellow;
			//left.Dock = DockStyle.Left;

			Panel right = new Panel();
			right.BackColor = Color.Wheat;
			right.Dock = DockStyle.Fill;
			right.Padding = new Padding(4);

			Button button = new Button();
			button.Dock = DockStyle.Bottom;
			button.Text = "&Click it!";
			button.Click += delegate(object sender, EventArgs eventArgs)
			{
				MessageBox.Show("Clicked!");
			};
			button.BackColor = Color.LightGray;

			ListBox lb = new ListBox();
			lb.Dock = DockStyle.Fill;
			right.Controls.AddRange(new Control[] { lb, button, });
			this.Controls.Add(right);
			//this.Controls.Add(left);


			panel1.Dock = DockStyle.Fill;

		
		}
	}
}
