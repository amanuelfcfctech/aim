using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using Core.Library.DevExpress;
using System.Drawing;
using DevExpress.XtraEditors;

namespace CFCAIM.Scheduler
{
    public partial class SchedulerWizard : Form
    {
        List<DayInfo> _dayInfo = new List<DayInfo>();
        List<WeekInfo> _weekInfo = new List<WeekInfo>();
        List<MonthInfo> _monthInfo = new List<MonthInfo>();

        Int32 _currentPage = 0;

        public SchedulerWizard()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }

        private void SchedulerWizard_Load(object sender, EventArgs e)
        {
            checkedListBoxControl1.Visible = true;            
            DataBindLueMonths();
            DataBindMonthlyWeekDay();
            DataBindImportTemplates();
            ClearFields();
            SetToolTips();
            ShowHideTabs(_currentPage);
            EnableDisableCheckBoxList();
                        
            tabControl1.TabPages.Remove(tabPage5);
            tabControl1.TabPages.Remove(tabPage6);
            tabControl1.TabPages.Remove(tabPage7);

            if (_currentPage == 0)
            {             
                btnBack.TabStop = false;
                btnCancel.TabStop = false;
                SendKeys.Send("{TAB}");
            }
            else
            {
                btnBack.TabStop = true;
                btnCancel.TabStop = true;
            }
        }                       

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnCancel.Visible = true;
            btnNext.Text = "Next";

            this.Icon = (Icon)Properties.Resources.CFCAIM;
            ToolTip ttip = new ToolTip();
            ttip.SetToolTip(btnNext, "Advance to next screen");

            switch (tabControl1.SelectedIndex)
            {
                case 0:  //Event name and description
                    btnNext.Enabled = false;
                    if (txtEventName.TextLength > 0)
                    {
                        btnNext.Enabled = true;
                    }                    
                    break;                

                case 1:  //Schedule
                    btnNext.Enabled = false;
                    if (deFromDate.Text.Length > 0)
                    {
                        btnNext.Enabled = true;
                    }
                    else
                    {
                        deFromDate.ShowPopup();
                    }
                    break;

                case 2:  //Event Type (currently import or export)
                    btnNext.Enabled = true;                    
                    break;

                case 3:  //Select File
                    btnNext.Enabled = false;
                    lblUserDefined.Visible = false;
                    lblSystem.Visible = false;
                    lblExport.Visible = false;

                    lueUserDefinedImportTemplates.Visible = false;
                    lueSystemImports.Visible = false;
                    lueExports.Visible = false;

                    if (rbImport.Checked == true)
                    {
                        if (rbUserDefined.Checked == true)
                        {
                            lblUserDefined.Visible = true;
                            lueUserDefinedImportTemplates.Visible = true;
                            lueUserDefinedImportTemplates.ShowPopup();
                        }
                        else
                        {
                            lblSystem.Location = new Point(114, 65);
                            lueSystemImports.Location = new Point(275, 62);
                            lblSystem.Visible = true;
                            lueSystemImports.Visible = true;
                            lueSystemImports.ShowPopup();
                        }
                    }
                    else
                    {
                        lblExport.Location = new Point(114, 65);
                        lueExports.Location = new Point(275, 62);
                        lblExport.Visible = true;
                        lueExports.Visible = true;
                        lueExports.ShowPopup();
                    }                   
                    break;                            

                //case 4:  //Conditions
                //    btnNext.Enabled = true;                    
                //    break;

                //case 5:  //Settings
                //    btnNext.Enabled = true;                    
                //    break;

                //case 6:  //Security Options
                //    btnNext.Enabled = true;
                //    btnNext.Text = "Finish";
                //    break;

                default:
                    break;
            }
            CheckList();

        }

        private void SaveEvent()
        {
            try
            {
                Int32 eventID = 0;
                Int32 monthID = 0;
                Int32 eventMonthlyID = 0;
                Boolean specificDays = false;
                Boolean specificWeek = false;
                Int32 recurEvery = 0;
                Int32 intTag = 0;


                EventsInfo def = new EventsInfo();
                def.EventID = 0;
                def.EventStatus = true;
                def.EventName = txtEventName.Text.ToString();
                def.EventDescription = txtEventDescription.Text.ToString();

                //Changed this -- added RecurEvery to daily and weekly tables, and removed from Events -- may change this back
                //def.RecurEvery = 0;
                //if (rbDaily.Checked == true || rbWeekly.Checked == true)
                //{
                //    def.RecurEvery = Convert.ToInt32(txtRecurEvery.Text);
                //}

                //----------------------------------------------------------
                //DAILY, WEEKLY, MONTHLY OR ONETIME                
                if (rbDaily.Checked == true)
                {
                    def.EventRunTypeID = (Int32)EnumEventRunType.Daily;
                }
                else if (rbWeekly.Checked == true)
                {
                    def.EventRunTypeID = (Int32)EnumEventRunType.Weekly;
                }
                else if (rbMonthly.Checked == true)
                {
                    def.EventRunTypeID = (Int32)EnumEventRunType.Monthly;
                }
                else
                {
                    def.EventRunTypeID = (Int32)EnumEventRunType.OneTime;
                }

                //----------------------------------------------------------
                //IMPORT-USERDEFINED, IMPORT-SYSTEM OR EXPORT
                if (rbImport.Checked == true && rbUserDefined.Checked == true)
                {
                    def.EventTypeID = (Int32)EnumEventType.ImportUserDefined;
                }
                else if (rbImport.Checked == true && rbSystem.Checked == true)
                {
                    def.EventTypeID = (Int32)EnumEventType.ImportSystem;
                }
                else
                {
                    def.EventTypeID = (Int32)EnumEventType.Export;
                }

                //----------------------------------------------------------
                //USERDEFINEDDATASOURCEDEFINITIONID, IMPORTCONFIGID OR SAMPLEEXPORTTEMPLATEID                
                if (lueUserDefinedImportTemplates.Visible == true)
                {
                    def.TemplateID = (Int32)lueUserDefinedImportTemplates.EditValue;
                }
                if (lueSystemImports.Visible == true)
                {
                    def.TemplateID = (Int32)lueSystemImports.EditValue;
                }
                if (lueExports.Visible == true)
                {
                    def.TemplateID = (Int32)lueExports.EditValue;
                }


                //----------------------------------------------------------    
                //STARTDATE            
                String txtdt = "";
                String amPM = "";
                txtdt = deFromDate.DateTime.ToString("yyyy-MM-dd ");
                txtdt += teFromTime.Time.ToString("HH:mm:ss");
                amPM = ".000";
                if (teFromTime.Time.ToString("tt") == "PM")
                {
                    amPM = ".999";
                }
                txtdt += amPM;
                def.StartDateTime = Convert.ToDateTime(txtdt);

                //----------------------------------------------------------
                //SAVE EVENT
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SaveEvent(ref def);
                if (error.ErrorNumber > 0)
                {
                    return;
                }


                //Save Event-Type Details
                if (error.RecordID > 0)
                {
                    try
                    {
                        eventID = error.RecordID;

                        //--------------------------------------------------------------------
                        //SAVE DAILY DETAILS
                        if (rbDaily.Checked == true)
                        {
                            recurEvery = Convert.ToInt32(txtRecurEvery.Text);
                            error = AIMManager.SaveEventDaily(0, eventID, (Int32)EnumEventRunType.Daily, recurEvery);
                        }

                        //--------------------------------------------------------------------
                        //SAVE WEEKLY DETAILS
                        if (rbWeekly.Checked == true)
                        {
                            recurEvery = Convert.ToInt32(txtRecurEvery.Text);

                            //For each day checked for this weekly event, add a record to the EventWeekly table (for the eventID)
                            //So if event should be run mon, wed and fri, the EventWeekly table will have 3 records for the one eventID
                            foreach (var ctrl in panelWeekDays.Controls)
                            {
                                if (ctrl is CheckBox && ((CheckBox)ctrl).Checked == true)
                                {
                                    //The Tag has values 1-7 (for Sun, Mon, Tues .... Sat)
                                    intTag = Convert.ToInt32(Convert.ToString(((CheckBox)ctrl).Tag));
                                    error = AIMManager.SaveEventWeekly(0, eventID, intTag, recurEvery);
                                }
                            }
                        }

                        //--------------------------------------------------------------------
                        //SAVE MONTHLY DETAILS
                        if (rbMonthly.Checked == true)
                        {
                            monthID = (Int32)lueMonths.EditValue;

                            if (rbMonthDays.Checked == true)
                            {
                                specificDays = true;
                            }
                            else
                            {
                                specificWeek = true;
                            }



                            error = AIMManager.SaveEventMonthly(0, eventID, monthID, specificDays, specificWeek);
                            if (error.RecordID > 0)
                            {
                                try
                                {
                                    eventMonthlyID = error.RecordID;

                                    if (rbMonthDays.Checked == true)
                                    {
                                        foreach (var ctrl in panelMonthDays.Controls)
                                        {
                                            if (ctrl is CheckBox && ((CheckBox)ctrl).Checked == true)
                                            {
                                                //The Tag has values 1-32 (for each of the month, plus 32 for lastday)                                                                                   
                                                intTag = Convert.ToInt32(Convert.ToString(((CheckBox)ctrl).Tag));
                                                error = AIMManager.SaveEventMonthlySpecificDays(0, eventMonthlyID, intTag);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var ctrl in panelMonthWeekNumber.Controls)
                                        {
                                            if (ctrl is CheckBox && ((CheckBox)ctrl).Checked == true)
                                            {
                                                //The Tag has values 1-5 (for First,Second,Third,Fourth and Last)                                                                              
                                                intTag = Convert.ToInt32(Convert.ToString(((CheckBox)ctrl).Tag));
                                                error = AIMManager.SaveEventMonthlySpecificWeekNumber(0, eventMonthlyID, intTag);
                                            }
                                        }


                                        foreach (var ctrl in panelMonthWeekDays.Controls)
                                        {
                                            if (ctrl is CheckBox && ((CheckBox)ctrl).Checked == true)
                                            {
                                                //The Tag has values 1-7 (for Sun, Mon, Tues .... Sat)                                                                              
                                                intTag = Convert.ToInt32(Convert.ToString(((CheckBox)ctrl).Tag));
                                                error = AIMManager.SaveEventMonthlySpecificWeekDays(0, eventMonthlyID, intTag);
                                            }
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //EVENT NAME TAB 
        #region Event Name tab
        private void txtEventName_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            if (txtEventName.Text.Length > 0)
            {
                btnNext.Enabled = true;
            }
        }
        private void txtEventName_Leave(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            if (txtEventName.Text.Length > 0)
            {
                btnNext.Enabled = true;
            }

        }
        #endregion

        //SCHEDULE TAB 
        #region Schedule tab
        private void deFromDate_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            if (deFromDate.Text.Length > 0)
            {
                btnNext.Enabled = true;
            }
        }
        private void rbOneTime_CheckedChanged(object sender, EventArgs e)
        {
            //txtRecurEvery.Text = "";
            gbRecurEvery.Visible = false;
            panelWeekDays.Visible = false;
            gpMonthly.Visible = false;

            btnNext.Enabled = false;
            if (rbOneTime.Checked == true)
            {
                btnNext.Enabled = true;
            }

        }
        private void txtRecurEvery_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;

            if (txtRecurEvery.Text == "0")
            {
                txtRecurEvery.Text = "1";
            }

            if (rbDaily.Checked == true && txtRecurEvery.Text.Length > 0)
            {
                btnNext.Enabled = validDay();
            }

            if (rbWeekly.Checked == true)
            {
                btnNext.Enabled = validWeek();
            }

        }
        private void CheckRecurEvery()
        {
            if (txtRecurEvery.Text.Length == 0)
            {
                txtRecurEvery.Text = "1";
            }

            if (txtRecurEvery.Text == "0")
            {
                txtRecurEvery.Text = "1";
            }
        }
        #endregion

        //EVENT TYPE TAB 
        #region Event Type tab
        private void rbExport_CheckedChanged(object sender, EventArgs e)
        {
            gbImport.Visible = true;
            if (rbExport.Checked == true)
            {
                gbImport.Visible = false;
            }

        }
        private void rbImport_CheckedChanged(object sender, EventArgs e)
        {
            if (rbImport.Checked == true)
            {
                gbImport.Visible = true;
            }
        }
        #endregion

        //SELECT FILES TAB
        #region Select Files tab
        private void DataBindImportTemplates()
        {


            //----------------------------------------------------------------------------------
            // IMPORTS-USER-DEFINED
            List<UserDefinedDataSourceDefinitionInfo> templateName = new List<UserDefinedDataSourceDefinitionInfo>();
            templateName = AIMManager.GetAllUserDefinedDataSourceDefinition_ForScheduler();
            templateName.Insert
            (
                0,
                new UserDefinedDataSourceDefinitionInfo
                {
                    UserDefinedDataSourceDefinitionID = 0,
                    ImportFileName = "--Select--"
                }
            );
            lueUserDefinedImportTemplates.Properties.DataSource = templateName;
            XtraEditors.LoadLookUpEdit(ref lueUserDefinedImportTemplates, "UserDefinedDataSourceDefinitionID", "ImportFileName");
            if (templateName.Count == 2)
            {
                lueUserDefinedImportTemplates.EditValue = 1;
            }
            else
            {
                lueUserDefinedImportTemplates.EditValue = 0;
            }


            //----------------------------------------------------------------------------------
            // IMPORTS-SYSTEM
            List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
            importConfig = AIMManager.GetImportConfig();
            importConfig.Insert(0, new ImportConfigInfo
            {
                ImportConfigID = 0,
                Name = "---Select---"
            }
            );
            lueSystemImports.Properties.DataSource = importConfig;
            XtraEditors.LoadLookUpEdit(ref lueSystemImports, "ImportConfigID", "Name");
            if (importConfig.Count == 2)
            {
                lueSystemImports.EditValue = 1;
            }
            else
            {
                lueSystemImports.EditValue = 0;
            }

            //----------------------------------------------------------------------------------
            // EXPORTS
            List<SampleExportTemplateInfo> exportTemplate = new List<SampleExportTemplateInfo>();
            exportTemplate = AIMManager.GetSampleExportTemplate();
            exportTemplate.Insert
                (
                    0,
                    new SampleExportTemplateInfo
                    {
                        SampleExportTemplateID = 0,
                        TemplateName = "--Select--"
                    }
                );
            lueExports.Properties.DataSource = exportTemplate;
            XtraEditors.LoadLookUpEdit(ref lueExports, "SampleExportTemplateID", "TemplateName", "ExportPath");
            lueExports.Properties.Columns["ExportPath"].Visible = false;


        }
        private void lueUserDefinedImportTemplates_EditValueChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            if ((Int32)lueUserDefinedImportTemplates.EditValue != 0)
            {
                btnNext.Enabled = true;
                btnNext.Text = "Finish";
            }
        }
        private void lueSystemImports_EditValueChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            if ((Int32)lueSystemImports.EditValue != 0)
            {
                btnNext.Enabled = true;
                btnNext.Text = "Finish";
            }
        }
        private void lueExports_EditValueChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            if ((Int32)lueExports.EditValue != 0)
            {
                btnNext.Enabled = true;
                btnNext.Text = "Finish";
            }
        }
        #endregion

        //DAILY 
        #region Daily

        private void rbDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDaily.Checked == true)
            {                
                lblRecurOn.Text = "days";
                gbRecurEvery.Visible = true;
                panelWeekDays.Visible = false;
                gpMonthly.Visible = false;
                CheckRecurEvery();

                //btnNext.Enabled = false;
                //if (txtRecurEvery.Text.Length > 0 && deFromDate.Text.Length > 0)
                //{
                //    btnNext.Enabled = true;
                //}
                //{
                //    btnNext.Enabled = false;
                //}
                btnNext.Enabled = validDay();
            }
        }
        private Boolean validDay()
        {
            Boolean rtn = false;
            if ((deFromDate.Text.Length > 0) && (rbDaily.Checked == true && txtRecurEvery.Text.Length > 0 && txtRecurEvery.Text != "0"))
            {
                rtn = true;
            }

            return rtn;
        }
        #endregion

        //WEEKLY
        #region Weekly

        private void DataBindMonthlyWeekDay()
        {
            //////_weekInfo = AIMManager.GetWeek();
            //////lueMonthlyWeekDay.Properties.DataSource = _weekInfo;
            //////XtraEditors.LoadLookUpEdit(ref lueMonthlyWeekDay, "WeekID", "WeekName");
            //////lueMonthlyWeekDay.Properties.Columns[1].Caption = "";
        }
        private void rbWeekly_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            if (rbWeekly.Checked == true)
            {                
                lblRecurOn.Text = "weeks on";
                gbRecurEvery.Visible = true;
                panelWeekDays.Visible = true;
                gpMonthly.Visible = false;
                CheckRecurEvery();
                //if (txtRecurEvery.Text.Length == 0)
                //{
                //    txtRecurEvery.Text = "1";
                //}

                //if (txtRecurEvery.Text == "0")
                //{
                //    txtRecurEvery.Text = "1";
                //}

                btnNext.Enabled = validWeek();                               
            }
        }
        private void gpWeekly_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnNext.Enabled = validWeek();
        }
        private Boolean validWeek()
        {
            Boolean rtn = false;
            if ((deFromDate.Text.Length > 0) && 
                (rbWeekly.Checked == true && txtRecurEvery.Text.Length > 0 && txtRecurEvery.Text != "0") && 
                (chkOnSunday.Checked == true || chkOnMonday.Checked == true || chkOnTuesday.Checked == true || 
                    chkOnWednesday.Checked == true || chkOnThursday.Checked == true || chkOnFriday.Checked == true || chkOnSaturday.Checked == true))
            {
                rtn = true;
            }

            return rtn;
        }


        #region Weekdays

        private void chkOnSunday_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnNext.Enabled = validWeek();
        }

        private void chkOnMonday_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnNext.Enabled = validWeek();
        }

        private void chkOnTuesday_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnNext.Enabled = validWeek();
        }

        private void chkOnWednesday_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnNext.Enabled = validWeek();
        }

        private void chkOnThursday_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnNext.Enabled = validWeek();
        }

        private void chkOnFriday_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnNext.Enabled = validWeek();
        }

        private void chkOnSaturday_CheckedChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnNext.Enabled = validWeek();
        }
        #endregion

        #endregion

        //MONTHLY 
        #region Monthly

        private void DataBindLueMonths()
        {
            _monthInfo = AIMManager.GetMonth();
            lueMonths.Properties.DataSource = _monthInfo;
            XtraEditors.LoadLookUpEdit(ref lueMonths, "MonthID", "MonthName");
            lueMonths.Properties.Columns[1].Caption = "";
        }
        private void rbMonthly_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonthly.Checked == true)
            {          
                gpMonthly.Location = new Point(262, 92);
                gpMonthly.Visible = true;
                panelWeekDays.Visible = false;
                gbRecurEvery.Visible = false;                
            }            
        }
        private void rbMonthDays_CheckedChanged(object sender, EventArgs e)
        {
            panelMonthWeekNumber.Visible = false;
            panelMonthWeekDays.Visible = false;
            panelMonthDays.Enabled = true;
        }
        private void rbMonthOn_CheckedChanged(object sender, EventArgs e)
        {
            panelMonthWeekNumber.Visible = true;
            panelMonthWeekDays.Visible = true;
            panelMonthDays.Enabled = false;
        }
        private Boolean validMonth()
        {
            Boolean rtn = false;

            if (rbMonthDays.Checked == true)
            {
                if (checkMonthlyDays() == true && (Int32)lueMonths.EditValue != 0)
                {
                    rtn = true;
                }
            }
            else
            {
                if (checkMonthlyWeekNumber() == true && checkMonthlyWeekDays() == true && (Int32)lueMonths.EditValue != 0)
                {
                    rtn = true;
                }               
            }           

            return rtn;
        }
        private Boolean checkMonthlyDays()
        {
            Boolean rtn = false;

            foreach (var ctrl in panelMonthDays.Controls)
            {
                if (ctrl is CheckBox && ((CheckBox)ctrl).Checked == true)
                {
                    rtn = true;
                }
            }
            return rtn;
        }
        private Boolean checkMonthlyWeekNumber()
        {
            Boolean rtn = false;

            foreach (var ctrl in panelMonthWeekNumber.Controls)
            {
                if (ctrl is CheckBox && ((CheckBox)ctrl).Checked == true)
                {
                    rtn = true;
                }
            }
            return rtn;
        }
        private Boolean checkMonthlyWeekDays()
        {
            Boolean rtn = false;

            foreach (var ctrl in panelMonthWeekDays.Controls)
            {
                if (ctrl is CheckBox && ((CheckBox)ctrl).Checked == true)
                {
                    rtn = true;
                }
            }
            return rtn;
        }
        private void panelMonthDays_MouseLeave(object sender, EventArgs e)
        {
            btnNext.Enabled = validMonth();
        }
        private void lueMonths_TextChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = validMonth();
        }
        private void panelMonthWeekNumber_MouseLeave(object sender, EventArgs e)
        {
            btnNext.Enabled = validMonth();
        }

        private void panelMonthWeekDays_MouseLeave(object sender, EventArgs e)
        {
            btnNext.Enabled = validMonth();
        }
        #endregion


        private void CheckList()
        {
            CheckedListBoxControl box = checkedListBoxControl1;
            box.SelectedIndex = tabControl1.SelectedIndex;

            if (box.GetItemCheckState(box.SelectedIndex) == CheckState.Checked)
            {
                box.SetItemCheckState(tabControl1.SelectedIndex, System.Windows.Forms.CheckState.Unchecked);
            }
            else
            {
                box.SetItemCheckState(tabControl1.SelectedIndex - 1, System.Windows.Forms.CheckState.Checked);
            }

            EnableDisableCheckBoxList();
        }
        private void EnableDisableCheckBoxList()
        {
            for (int i = 0; i < checkedListBoxControl1.ItemCount; i++)
            {
                object item = checkedListBoxControl1.GetItem(i);

                if (i == 0)
                {
                    ((DevExpress.XtraEditors.Controls.CheckedListBoxItem)(item)).Enabled = true;
                }
                else
                {
                    if (((DevExpress.XtraEditors.Controls.CheckedListBoxItem)(item)).CheckState == CheckState.Unchecked)
                    {
                        ((DevExpress.XtraEditors.Controls.CheckedListBoxItem)(item)).Enabled = false;
                    }
                    else
                    {
                        ((DevExpress.XtraEditors.Controls.CheckedListBoxItem)(item)).Enabled = true;
                    }
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage == 3)  //Finish
            {
                SaveEvent();
                Close();
                SchedulerMain scheduler;
                AIMGlobal.open_form(scheduler = new SchedulerMain());

            }
            else
            {
                _currentPage++;
                ShowHideTabs(_currentPage);
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
                SchedulerMain scheduler;
                AIMGlobal.open_form(scheduler = new SchedulerMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentPage == 0)

                {
                    Close();
                    SchedulerMain scheduler;
                    AIMGlobal.open_form(scheduler = new SchedulerMain());
                }

                _currentPage--;
                ShowHideTabs(_currentPage);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        private void ShowHideTabs(Int32 currentPage)
        {

            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SelectedIndex = currentPage;
        }       

        private void ClearFields()
        {
            txtEventName.Text = "";
            txtEventDescription.Text = "";
            txtRecurEvery.Text = "";
            deFromDate.Text = "";
            chkOnSunday.Checked = false;
            chkOnMonday.Checked = false;
            chkOnTuesday.Checked = false;
            chkOnWednesday.Checked = false;
            chkOnThursday.Checked = false;
            chkOnFriday.Checked = false;
            chkOnSaturday.Checked = false;
            lueMonths.EditValue = 0;
            
            lueUserDefinedImportTemplates.EditValue = 0;
            lueSystemImports.EditValue = 0;
            lueExports.EditValue = 0;
            foreach (var ctrl in panelMonthDays.Controls)
            {
                if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Checked = false;
                }
            }
            foreach (var ctrl in panelMonthWeekDays.Controls)
            {
                if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Checked = false;
                }
            }
            foreach (var ctrl in panelMonthWeekNumber.Controls)
            {
                if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Checked = false;
                }
            }
        }        

        private void SetToolTips()
        {
            try
            {
                this.Icon = (Icon)Properties.Resources.CFCAIM;
                ToolTip ttip = new ToolTip();
                ttip.SetToolTip(btnBack, "Return to previous screen");
                ttip.SetToolTip(btnCancel, "Cancel Wizard");
                ttip.SetToolTip(btnNext, "Advance to next screen");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //CURRENT NOT USED (on hidden tab pages) 
        #region Currently Not Used
        private void rbLogged_CheckedChanged(object sender, EventArgs e)
        {
            chkStorePassword.Enabled = false;
            if (rbLogged.Checked == true)
            {
                chkStorePassword.Enabled = true;
            }
        }

        private void rbUserMustBeLoggedOn_CheckedChanged(object sender, EventArgs e)
        {
            chkStorePassword.Enabled = false;
            if (rbLogged.Checked == true)
            {
                chkStorePassword.Enabled = true;
            }
        }

        private void chkACPower_CheckedChanged(object sender, EventArgs e)
        {
            chkBatteryPower.Enabled = false;
            if (chkACPower.Checked == true)
            {
                chkBatteryPower.Enabled = true;
            }
        }

        private void chkStartTask_CheckedChanged(object sender, EventArgs e)
        {
            chkStop.Enabled = false;
            chkRestart.Enabled = false;
            if (chkStartTask.Checked == true)
            {
                chkStop.Enabled = true;
                chkRestart.Enabled = true;
            }
        }

        private void chkStop_CheckedChanged(object sender, EventArgs e)
        {
            chkRestart.Enabled = false;
            if (chkStop.Checked == true)
            {
                chkRestart.Enabled = true;
            }
        }

        #endregion

        
    }
}
