using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System.Drawing;

namespace CFCAIM.Scheduler
{
	public partial class SchedulerMain : Form
	{
        CFCUser.Global userGlobal = new CFCUser.Global();
        SchedulerWizard schedulerWizard;

        public SchedulerMain()
		{
			InitializeComponent();
		}     	


        private void SchedulerMain_Load(object sender, EventArgs e)
        {
            ClearTxtfields();
            DataBindGridLue();
            DataBindGrid();
            SetToolTips();            
            grdSchedulerView.FocusedRowHandle = 0;            
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void ClearTxtfields()
        {
            txtEventName.Text = "";
            txtDescription.Text = "";
            txtFileName.Text = "";
            txtRunType.Text = "";
            txtType.Text = "";
            txtStartDate.Text = "";           
            txtLastRunDate.Text = "";
            txtDay_Recurs.Text = "";
            txtWeek_Recurs.Text = "";
            txtWeek_DaysOfWeek.Text = "";
            txtUserName.Text = "";
            txtUpdateStamp.Text = "";
        }

        private void DataBindGrid()
        {
            List<EventsInfo> events = new List<EventsInfo>();
            events = AIMManager.GetEvents();

            Int32 currentRowNum = 0;
            currentRowNum = grdSchedulerView.FocusedRowHandle;
            grdScheduler.DataSource = events;
            grdSchedulerView.FocusedRowHandle = currentRowNum;
            
        }

        private void DataBindGridLue()
        {
            List<EventRunTypeInfo> runtype = new List<EventRunTypeInfo>();
            runtype = AIMManager.GetEventRunType();
            runtype.Insert
            (
                0,
                new EventRunTypeInfo
                {
                    EventRunTypeID = 0,
                    EventRunTypeName = "--Select--"
                }
            );
            replueEventRunType.DataSource = runtype;
            XtraEditors.LoadGridLookUpEdit(ref replueEventRunType, "EventRunTypeID", "EventRunTypeName");

            //---------------------------------------------------
            List<EventTypeInfo> type = new List<EventTypeInfo>();
            type = AIMManager.GetEventType();
            type.Insert
            (
                0,
                new EventTypeInfo
                {
                    EventTypeID = 0,
                    EventTypeName = "--Select--"
                }
            );
            replueEventType.DataSource = type;
            XtraEditors.LoadGridLookUpEdit(ref replueEventType, "EventTypeID", "EventTypeName");

            ////---------------------------------------------------
            //List<EventsInfo> status = new List<EventsInfo>();
            //status = AIMManager.GetEventType();
            //replueEventStatus.DataSource = status;
            //XtraEditors.LoadGridLookUpEdit(ref replueEventType, "EventStatusID", "EventStatusName");
        }

        //private List<KeyValuePair<string, bool>> GenerateYesNo()
        //{
        //    List<KeyValuePair<string, bool>> yesNoChoices = new List<KeyValuePair<string, bool>>();

        //    yesNoChoices.Add(new KeyValuePair<string, bool>("Yes", true));
        //    yesNoChoices.Add(new KeyValuePair<string, bool>("No", false));
        //    return yesNoChoices;
        //}
        private List<KeyValuePair<string, bool>> GenerateYesNo()
        {
            List<KeyValuePair<string, bool>> yesNoChoices = new List<KeyValuePair<string, bool>>();

            yesNoChoices.Add(new KeyValuePair<string, bool>("Yes", true));
            yesNoChoices.Add(new KeyValuePair<string, bool>("No", false));
            return yesNoChoices;
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Close();
            schedulerWizard = new SchedulerWizard();
            schedulerWizard.Show();
        }

        private void repbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DialogResult dr = new DialogResult();
                ErrorInfo result = new ErrorInfo();

                dr = MessageBox.Show("Delete this scheduled event?", "Warning...", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    return;
                }

                EventsInfo def = new EventsInfo();
                def = (EventsInfo)grdSchedulerView.GetFocusedRow();
                def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;                
                AIMManager.DeleteEvent(def.EventID);       //Deletes from Event table and any occurances in EventDaily, EventWeekly, EventMonthly 
                DataBindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdSchedulerView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                EventsInfo def = new EventsInfo();
                def = (EventsInfo)grdSchedulerView.GetFocusedRow();
                def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveEvent(ref def);
                DataBindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }               

        //Trying to get dropdown with just the values Enabled/Disabled to work.  No luck.
        //private void repcmbStatus_CustomItemDisplayText(object sender, DevExpress.XtraEditors.CustomItemDisplayTextEventArgs e)
        //{
        //    if (e.Value == "True")
        //    {
        //        e.DisplayText = "Enabled";
        //    }
        //    else
        //    {
        //        e.DisplayText = "Disabled";
        //    }
        //}
        //private void repcmbStatus_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        //{
        //    if (e.Value == "True")
        //    {
        //        e.DisplayText = "Enabled";
        //    }
        //    else
        //    {
        //        e.DisplayText = "Disabled";
        //    }
        //}

        private void repositoryItemCheckEdit1_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                EventsInfo def = new EventsInfo();
                def = (EventsInfo)grdSchedulerView.GetFocusedRow();
                def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                if (((DevExpress.XtraEditors.CheckEdit)sender).Checked == true)
                {
                    def.EventStatus = true;
                }
                else
                {
                    def.EventStatus = false;
                }

                AIMManager.SaveEvent(ref def);
                DataBindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetToolTips()
        {
            try
            {
                this.Icon = (Icon)Properties.Resources.CFCAIM;
                ToolTip ttip = new ToolTip();
                foreach (Control ctrl in pnlTopBtns.Controls)
                {
                    ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
                }                
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void SchedulerMain_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }
                             
        private void grdSchedulerView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ClearTxtfields();
            panelDay.Visible = false;
            panelWeek.Visible = false;
            EventsInfo eventDef = new EventsInfo();
            eventDef = (EventsInfo)grdSchedulerView.GetFocusedRow();
            txtEventName.Text = eventDef.EventName;
            txtFileName.Text = eventDef.FileName;
            txtDescription.Text = eventDef.EventDescription;           
            txtStartDate.Text = eventDef.StartDateTime.ToString();
            txtUserName.Text = eventDef.UserName;
            txtUpdateStamp.Text = eventDef.UpdateStamp.ToString();
            

            if (eventDef.LastRunDateTime.ToString() == "1/1/0001 12:00:00 AM")
            {
                txtLastRunDate.Text = "Never";
            }
            else
            {
                txtLastRunDate.Text = eventDef.LastRunDateTime.ToString();
            }            

            switch (eventDef.EventTypeID)
            {
                case 1:
                    txtType.Text = "Import - User-Defined";                    
                    break;
                case 2:
                    txtType.Text = "Import - System";                    
                    break;
                case 3:
                    txtType.Text = "Export";                    
                    break;
                default:
                    break;
            }

            switch (eventDef.EventRunTypeID)
            {
                case 1:     //daily
                    txtRunType.Text = "Daily";
                    panelDay.Visible = true;
                    List<EventDayInfo> day = new List<EventDayInfo>();
                    day = AIMManager.GetEventDay(eventDef.EventID);
                    if (day != null)
                    {
                        txtDay_Recurs.Text = day[0].RecurEvery.ToString();
                    }
                    break;

                case 2:     //weekly
                    panelWeek.Location = new Point(387, 32);
                    panelWeek.Height = 70;                 
                    txtRunType.Text = "Weekly";
                    panelWeek.Visible = true;
                    List<EventWeeklyInfo> week = new List<EventWeeklyInfo>();
                    week = AIMManager.GetEventWeekly(eventDef.EventID);
                    if (week != null)
                    {
                        txtWeek_Recurs.Text = week[0].RecurEvery.ToString();
                    }

                    foreach (var item in week)
                    {
                        txtWeek_DaysOfWeek.Text += item.WeekDayName + "   ";
                    }
                    break;

                case 3:     //monthly
                    txtRunType.Text = "Monthly";
                    break;

                case 4:     //one-time
                    txtRunType.Text = "One-Time";
                    break;

                default:
                    break;
            }
        
        }

        private void grdSchedulerView_GotFocus(object sender, EventArgs e)
        {

        }
    }
}
