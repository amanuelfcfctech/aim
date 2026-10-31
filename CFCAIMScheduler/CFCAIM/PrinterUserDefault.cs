using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class PrinterUserDefault : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
        PrinterSetup printerSetup;

        public PrinterUserDefault()
        {
            InitializeComponent();
        }

        private void PrinterUserDefault_Load(Object sender, EventArgs e)
        {
            DataBindlue();
            DataBind();
            SetToolTips();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void DataBind()
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdPrinterView.FocusedRowHandle.ToString(), out rowNum);
            grdPrinter.DataSource = AIMManager.GetPrinterUserDefaultAll();
            grdPrinterView.FocusedRowHandle = rowNum;
        }

        private void DataBindlue()
        {

            //--------------------------------------------------------------
            //CFCUSERS
            List<CFCUserInfo> users = new List<CFCUserInfo>();
            users = AIMManager.GetCFCUsers(true);
            users.Insert
            (
                0,
                new CFCUserInfo
                {
                    CFCUserID = 0,
                    UserName = "--Select--"
                }
            );
            lueUserName.Properties.DataSource = users;
            XtraEditors.LoadLookUpEdit(ref lueUserName, "CFCUserID", "UserName");


            //--------------------------------------------------------------
            //PRINTER TYPE
            List<PrinterTypeInfo> printerType = new List<PrinterTypeInfo>();
            printerType = AIMManager.GetPrinterTypeFromDefaultPrinters();
            printerType.Insert
            (
                0,
                new PrinterTypeInfo
                {
                    PrinterTypeID = 0,
                    PrinterTypeName = "--Select--"
                }
            );
            luePrinterType.Properties.DataSource = printerType;
            XtraEditors.LoadLookUpEdit(ref luePrinterType, "PrinterTypeID", "PrinterTypeName");

            //--------------------------------------------------------------
            //INSTALLED PRINTERS
            List<String> systemPrinter = new List<String>();
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                systemPrinter.Add(printer);
            }
            systemPrinter.Insert
            (               
                0, "Default"
            );
            luePrinter.Properties.DataSource = systemPrinter;
            luePrinter.EditValue = 0;

            //8/23/18
            //luePrinter.Text = "--Select--";
            luePrinter.Text = "Default";


            //8/23/18
            ////--------------------------------------------------------------
            ////LOADING THIS HERE BECAUSE 'SELECT' SHOULD NOT BE INCLUDED IN GRID DROPDOWN
            //List<String> installed = new List<String>();
            //foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            //{
            //    installed.Add(printer);
            //}
            //repluePrinter.DataSource = installed;
            repluePrinter.DataSource = systemPrinter;

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
            ttip.SetToolTip(btnAdd, "Add Printer");
            ttip.SetToolTip(btnPreviewResults, "Preview Grid Results");

        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void btnPreviewResults_Click(Object sender, EventArgs e)
        {
            try
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grdPrinter, "User Printers ");
                CFCAIMCore.Global.previewGridResults(grdPrinter, "User Printers ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(Object sender, EventArgs e)
        {
            Close();
            PrinterSetup printSetup;
            AIMGlobal.open_form(printSetup = new PrinterSetup());

        }

        private void PrinterUserDefault_StyleChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnAdd_Click(Object sender, EventArgs e)
        {
            try
            {
                PrinterUserDefaultInfo printerUserDefault = new PrinterUserDefaultInfo();

                if ((Int32)lueUserName.EditValue == 0)
                {
                    MessageBox.Show("Please select a User Name.", "Warning...", MessageBoxButtons.OK);
                    lueUserName.Focus();
                    lueUserName.ShowPopup();
                    return;
                }

                if ((Int32)luePrinterType.EditValue == 0)
                {
                    MessageBox.Show("Please select a Printer Type.", "Warning...", MessageBoxButtons.OK);
                    luePrinterType.Focus();
                    luePrinterType.ShowPopup();
                    return;
                }

                //8/23/18 - commented
                //if (luePrinter.Text.ToString() == "--Select--" || luePrinter.Text.ToString().Length == 0)
                //{
                //    MessageBox.Show("Please select a Printer.", "Warning...", MessageBoxButtons.OK);
                //    luePrinter.Focus();
                //    luePrinter.ShowPopup();
                //    return;
                //}

                //Check for duplicates
                PrinterUserDefaultInfo printerType = AIMManager.GetUserPrinterDefault((Int32)lueUserName.EditValue, (Int32)luePrinterType.EditValue);
                if (printerType != null)
                {
                    MessageBox.Show("This user already has this printer type assigned to a printer.", "Warning...", MessageBoxButtons.OK);
                    return;
                }

                printerUserDefault.PrinterUserDefaultID = 0;
                printerUserDefault.CFCUserID = (Int32)lueUserName.EditValue;
                printerUserDefault.PrinterTypeID = (Int32)luePrinterType.EditValue;
                printerUserDefault.PrinterName = luePrinter.Text.ToString();
                printerUserDefault.UpdateCFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SaveUserPrinterDefault(ref printerUserDefault);
                if (error.ErrorNumber > 0)
                {
                    return;
                }

                lueUserName.EditValue = 0;
                luePrinterType.EditValue = 0;
                luePrinter.EditValue = 0;

                //8/23/18
                luePrinter.Text = "Default";

                DataBind();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void grdPrinterView_ValidateRow(Object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                PrinterUserDefaultInfo printerUserDefault = new PrinterUserDefaultInfo();
                printerUserDefault = (PrinterUserDefaultInfo)grdPrinterView.GetFocusedRow();
                printerUserDefault.UpdateCFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                if (printerUserDefault != null)
                {
                    AIMManager.SaveUserPrinterDefault(ref printerUserDefault);
                }
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repbtnDelete_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                PrinterUserDefaultInfo printerUserDefault = new PrinterUserDefaultInfo();
                printerUserDefault = (PrinterUserDefaultInfo)grdPrinterView.GetFocusedRow();
                printerUserDefault.UpdateCFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeletePrinterUserDefault(printerUserDefault.PrinterUserDefaultID);
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddPrinterType_Click(Object sender, EventArgs e)
        {
            try
            {                
                AIMGlobal.open_form(printerSetup = new PrinterSetup());                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
    }
}
