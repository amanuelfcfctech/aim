using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;

namespace CFCAIM.Scheduler
{
	public partial class Event : Form
	{
		public Event()
		{
			InitializeComponent();
		}

		private void Event_Load(object sender, EventArgs e)
		{
			List<DayInfo> day = new List<DayInfo>();
			List<EventDayInfo> eventDay = new List<EventDayInfo>();
			String checkValues = String.Empty;

			day = AIMManager.GetDay();
			eventDay = AIMManager.GetEventDay(1);
			Test<EventDayInfo>.Load(ref cmbDay, eventDay, day,"Day", "DayID");
		}

		private void rbDaily_CheckedChanged(object sender, EventArgs e)
		{
			pnlWeekly.Visible = false;
			pnlDaily.Visible = true;
			pnlMonthly.Visible = false;
		}

		private void rbWeekly_CheckedChanged(object sender, EventArgs e)
		{
			pnlWeekly.Visible = true;
			pnlDaily.Visible = false;
			pnlMonthly.Visible = false;
		}

		private void rbMonthly_CheckedChanged(object sender, EventArgs e)
		{
			pnlWeekly.Visible = false;
			pnlDaily.Visible = false;
			pnlMonthly.Visible = true;
		}

//		private static void load(ref CheckedComboBoxEdit cmbDay, IList eventDay, IList day)
//		{
//			String checkValues = String.Empty;

//			cmbDay.Properties.DataSource = day;
//			cmbDay.Properties.DisplayMember = "Day";
//			cmbDay.Properties.ValueMember = "DayID";
//			for (int i = 0; i < eventDay.Count; i++)
//			{
//				if (checkValues != String.Empty)
//				{
//					checkValues += ",";
//				}
////				checkValues += String.Format("{0}", eventDay[i].DayID);
//			}

//			if (checkValues != String.Empty)
//			{
//				cmbDay.SetEditValue(checkValues);
//			}
//		}
	}
}
