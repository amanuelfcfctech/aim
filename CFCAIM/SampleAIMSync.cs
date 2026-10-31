using System;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Threading;

namespace CFCAIM
{
    public partial class SampleAIMSync : Form
	{

        CFCUser.Global userGlobal = new CFCUser.Global();
        private Int32 _progressBarID = 0;
        private DateTime _rebuildDateDt = DateTime.Now.AddDays(-365);
        private String _rebuildDate = "";
        HelpRebuild helpRebuild;

        public SampleAIMSync()
		{
			InitializeComponent();
		}

        private void SampleAIMSync_Load(Object sender, EventArgs e)
        {
            try
            {
                SetToolTips();
                               

                //if Rebuild is running, show progress bar and disable Rebuild button				
                ProgressBarInfo result = new ProgressBarInfo();                
                DateTime saveTime;
                result = AIMManager.GetProgressBarValue("Rebuild", userGlobal.CFCLoginUser.CFCUserID);
                if (result != null)
                {
                    //check to make sure process is still running - if updatestamp does not change, process is not running.
                    saveTime = result.UpdateStamp;
                    for (int i = 0; i < 500; i++) {}
                    result = AIMManager.GetProgressBarValue("Rebuild", userGlobal.CFCLoginUser.CFCUserID);
                    if (saveTime == result.UpdateStamp)
                    {
                        // Delete record from SQL ProgressBar table			
                        AIMManager.DeleteProgressBar(result.ProgressBarID);
                    }
                    else
                    {                        
                        pbRebuild.Visible = true;
                        txtCounter.Visible = true;
                        txtCounter.Text = "Rebuilding ...";
                        btnRunUpdate.Text = "Rebuilding ...";
                        btnRunUpdate.Enabled = false;                        
                    }
                }

                //6/27/19                
                deFromDate.EditValue = DateTime.Now.AddDays (-365); //--> sets date to one year back from today's date.
                btnRunUpdate.Enabled = true;

                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
        private void btnRunUpdate_Click(Object sender, EventArgs e)
        {
            
            DialogResult dr;
            ErrorInfo errorLog = new ErrorInfo();

            //6/27/19
            if (chkDate.Checked == false && chkAll.Checked == false)
            {
                MessageBox.Show("Please select either 'Rebuild From' or 'Rebuild All' ", "Warning...");                
                return;
            }

            if (chkDate.Checked == true && deFromDate.Text == "")
            {
                MessageBox.Show("Please enter a From date.", "Warning...");
                deFromDate.Focus();
                return;
            }

            if (chkAll.Checked == true)
            {
                //get oldest sample date
                errorLog = AIMManager.GetSampleOldestSampleDate();
                if (errorLog != null)
                {
                    _rebuildDateDt = Convert.ToDateTime(errorLog.ErrorMessage);
                }
                else
                {
                    _rebuildDateDt = DateTime.Now.AddDays(-365);
                }                
            }
            else
            {
                _rebuildDateDt = Convert.ToDateTime(deFromDate.EditValue);
            }

            _rebuildDate = _rebuildDateDt.Year + "-" + _rebuildDateDt.Month + "-" + _rebuildDateDt.Day;
            

            dr = MessageBox.Show("Rebuild Sample AIM table?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
            {                
                return;
            }

            //6/27/19
            btnRunUpdate.Enabled = false;            
            btnRunUpdate.Text = "Rebuilding ...";

            //5/24/19            
            errorLog = AIMManager.WriteToLog("START");

            pbRebuild.Visible = true;
            pbRebuild.Value = 0;
            txtCounter.Visible = true;
            txtCounter.Text = String.Empty;

            // Insert record into ProgressBar SQL table
            ProgressBarInfo progressBar = new ProgressBarInfo();
            progressBar.ProgressBarID = 0;
            progressBar.ProgressBarValue = 1;
            progressBar.ProgressBarDescription = "Rebuilding ...";
            progressBar.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            progressBar.ProgressBarType = (Int32)EnumProgressBarType.RebuildSampleAIM;
            _progressBarID = AIMManager.InsertProgressBar(ref progressBar).RecordID;

            //------------------------------
            //Background worker for EQUATIONS progress bar          
            if (!bgw_Equations_ProgressBar.IsBusy)
            {
                bgw_Equations_ProgressBar.RunWorkerAsync();
            }

            //------------------------------
            //Background worker for EQUATIONS stored procedure
            if (!bgw_Equations.IsBusy)
            {
                bgw_Equations.RunWorkerAsync();
            }

            //5/9/18 - moved to bgw_Equations_ProgressBar worker completed event
            //////------------------------------
            //////Background worker for REBUILD progress bar
            ////if (!bgwProgressBar.IsBusy)
            ////{
            ////    bgwProgressBar.RunWorkerAsync();
            ////}

            //////------------------------------
            //////Background worker for REBUILD stored procedure
            ////if (!bgwRebuild.IsBusy)
            ////{                
            ////    bgwRebuild.RunWorkerAsync();
            ////}                      

        }

        private void SetToolTips()
        {
            System.Drawing.Icon ico = Properties.Resources.CFCAIM;
            this.Icon = ico;
            ToolTip ttip = new ToolTip();
            foreach (Control ctrl in pnlTopBtns.Controls)
            {
                ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
            }
            ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
        }


        private void btnResize_Click(Object sender, EventArgs e)
        {           
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void SampleAIMSync_SizeChanged(Object sender, EventArgs e)
        {         
            btnResize.Visible = true;
        }

        //4/18/18        
        private void bgwRebuild_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {                
                //6/27/19
                //AIMManager.RePopulateSampleAimTable(_progressBarID, userGlobal.CFCLoginUser.CFCUserID);
                AIMManager.RePopulateSampleAimTable(_progressBarID, userGlobal.CFCLoginUser.CFCUserID, _rebuildDate);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //4/18/18
        private void bgwRebuild_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            
            ErrorInfo errorLog = new ErrorInfo();

            //4/8/22
            errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - Begin");

            bgw_Rebuild.Dispose();

            //4/8/22
            errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 1");

            //update system setting	
            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
            systemSettings = AIMManager.GetSystemSettings();

            //4/8/22
            errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 2");

            if (systemSettings != null)
            {

                //4/8/22
                errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 3");

                SystemSettingsInfo settings = new SystemSettingsInfo();
                settings.SystemSettingsID = systemSettings.SystemSettingsID;
                settings.AIMNutrientID = systemSettings.AIMNutrientID;
                settings.AllowAIMInactive = systemSettings.AllowAIMInactive;
                settings.ShowAnalyzerMaterials = systemSettings.ShowAnalyzerMaterials;
                settings.SampleAimNeedsRebuild = false;                
                settings.AutoGenerateLotNumber = systemSettings.AutoGenerateLotNumber;

                //12/21/18               
                settings.SampleNumberOfDays = systemSettings.SampleNumberOfDays;
                
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SaveSystemSettings(ref settings);

                //4/8/22
                errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 4");

                if (error.ErrorNumber > 0)
                {
                    //4/8/22
                    errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 4.5");

                    return;
                }

                //4/8/22
                //errorLog = AIMManager.WriteToLog("Back1 After system settings update");
                errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 5");

            }

            //4/8/22
            errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 6");

            // Delete record from SQL ProgressBar table			            
            AIMManager.DeleteProgressBar(_progressBarID);

            //4/8/22
            //errorLog = AIMManager.WriteToLog("Back1 DeleteProgressBar");
            errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 7");

            bgw_ProgressBar.Dispose();

            //4/8/22
            errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 8");

            bgw_Rebuild.Dispose();

            //4/8/22
            errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - 9");

            pbRebuild.Visible = false;
            txtCounter.Visible = false;
            btnRunUpdate.Text = "Rebuild Sample AIM";
            btnRunUpdate.Enabled = true;
            AIMGlobal.SplashScreenHide();
            MessageBox.Show("Rebuild Utility is complete.");

            //6/27/19
            btnRunUpdate.Enabled = true;

            //4/8/22
            //errorLog = AIMManager.WriteToLog("Back1 Complete-End");
            errorLog = AIMManager.WriteToLog("bgwRebuild_RunWorkerCompleted function - End");
        }
               

        private void bgwProgressBar_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Int32 count = 1;
                ProgressBarInfo progressBar = new ProgressBarInfo();
               
                progressBar = AIMManager.GetProgressBarValue(_progressBarID);
                if (progressBar == null)
                {
                    return;
                }
                
                while (progressBar != null)
                {
                    count++;
                    bgw_ProgressBar.ReportProgress(count);
                    Thread.Sleep(2000);
                    progressBar = AIMManager.GetProgressBarValue(_progressBarID);
                }
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwProgressBar_ProgressChanged(Object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            ProgressBarInfo progressBar = new ProgressBarInfo();

            try
            {
                if (pbRebuild.Value != pbRebuild.Maximum)
                {
                    progressBar = AIMManager.GetProgressBarValue(_progressBarID);
                    if (progressBar != null)                                            //12/28/21 - added check for null
                    {
                        pbRebuild.Value = progressBar.ProgressBarValue;
                        txtCounter.Text = progressBar.ProgressBarDescription;
                    }
                }
                else
                {
                    pbRebuild.Value = 0;
                    txtCounter.Text = String.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwProgressBar_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {            
            bgw_ProgressBar.Dispose();

            // Delete record from SQL ProgressBar table			
            AIMManager.DeleteProgressBar(_progressBarID);
        }

        //5/9/18 - this stored procedure needs to be run BEFORE the SampleAIM rebuild
        private void bgw_Equations_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                //11/4/22 - added rebuildDate
                //AIMManager.ReCalculateEquations(_progressBarID, userGlobal.CFCLoginUser.CFCUserID, CFCAIMCore.Global.aimDatabase.AIMDatabaseID);
                AIMManager.ReCalculateEquations(_progressBarID, userGlobal.CFCLoginUser.CFCUserID, CFCAIMCore.Global.aimDatabase.AIMDatabaseID, _rebuildDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgw_Equations_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            bgw_Equations.Dispose();                       		            
            AIMManager.DeleteProgressBar(_progressBarID);
            bgw_Equations_ProgressBar.Dispose();
            bgw_Equations.Dispose();
                       

        }

        private void bgw_Equations_ProgressBar_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Int32 count = 1;
                ProgressBarInfo progressBar = new ProgressBarInfo();

                progressBar = AIMManager.GetProgressBarValue(_progressBarID);
                if (progressBar == null)
                {
                    return;
                }

                while (progressBar != null)
                {
                    count++;
                    bgw_Equations_ProgressBar.ReportProgress(count);
                    Thread.Sleep(2000);
                    progressBar = AIMManager.GetProgressBarValue(_progressBarID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgw_Equations_ProgressBar_ProgressChanged(Object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            ProgressBarInfo progressBar = new ProgressBarInfo();

            try
            {
                if (pbRebuild.Value != pbRebuild.Maximum)
                {
                    progressBar = AIMManager.GetProgressBarValue(_progressBarID);
                    pbRebuild.Value = progressBar.ProgressBarValue;
                    txtCounter.Text = progressBar.ProgressBarDescription;

                }
                else
                {
                    pbRebuild.Value = 0;
                    txtCounter.Text = String.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgw_Equations_ProgressBar_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            ErrorInfo errorLog = new ErrorInfo();
            errorLog = AIMManager.WriteToLog("EquaWorker Complete-Start");

            bgw_Equations_ProgressBar.Dispose();

            // Delete record from SQL ProgressBar table			
            AIMManager.DeleteProgressBar(_progressBarID);

            errorLog = AIMManager.WriteToLog("EquaWorker after dispose bar");

            //5/9/18 - this is for the Sample AIM Rebuild (not the Equation Rebuild)
            // Insert record into ProgressBar SQL table
            ProgressBarInfo progressBar = new ProgressBarInfo();
            progressBar.ProgressBarID = 0;
            progressBar.ProgressBarValue = 1;
            progressBar.ProgressBarDescription = "Rebuilding Sample AIM ...";
            progressBar.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            progressBar.ProgressBarType = (Int32)EnumProgressBarType.RebuildSampleAIM;
            _progressBarID = AIMManager.InsertProgressBar(ref progressBar).RecordID;


            //5/9/18 -- moved here from btnRunUpdate click event
            //------------------------------
            //Background worker for REBUILD progress bar
            if (!bgw_ProgressBar.IsBusy)
            {
                bgw_ProgressBar.RunWorkerAsync();
            }
            errorLog = AIMManager.WriteToLog("EquaWorker after bgwProgressBar.RunWorker");

            //------------------------------
            //Background worker for REBUILD stored procedure
            if (!bgw_Rebuild.IsBusy)
            {
                bgw_Rebuild.RunWorkerAsync();
            }
            errorLog = AIMManager.WriteToLog("EquaWorker after bgwRebuild.RunWorker");

        }


        private void btnExit_Click(Object sender, EventArgs e)
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

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            deFromDate.Visible = false;
            if (chkDate.Checked == true)
            {
                deFromDate.Visible = true;
                chkAll.Checked = false;

                //11/4/22
                deFromDate.EditValue = DateTime.Now;
            }

            //11/4/22
            else
            {
                deFromDate.EditValue = DateTime.Now.AddDays(-365); //--> sets date to one year back from today's date.
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                chkDate.Checked = false;
            }
        }

        private void btnRebuildHelp_Click(object sender, EventArgs e)
        {
            try
            {
                helpRebuild = new HelpRebuild();
                helpRebuild.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
