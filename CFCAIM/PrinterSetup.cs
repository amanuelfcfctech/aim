using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Manager;
using CFCAIMCore.Entity;
using Core.Library.DevExpress;
using System.Drawing.Printing;
using System.Linq;
using CFCAIMCore.Data;


namespace CFCAIM
{
    public partial class PrinterSetup : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
        PrinterUserDefault printerUserDefault;


        public PrinterSetup()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }

        private void PrinterSetup_Load(object sender, EventArgs e)
        {
            try
            {
                DataBindlue();
                DataBind();
                SetToolTips();
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataBind()
        {
            //6/15/18
            //grdPrinter.DataSource = AIMManager.GetPrinterType();
            //List<String> systemPrinter = new List<String>();
            //foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            //{
            //    systemPrinter.Add(printer);
            //}
            //repluePrinter.DataSource = systemPrinter;
            //repluePrinter.NullText = "--Select--";

            Int32 rowNum = 0;
            Int32.TryParse(grdPrinterView.FocusedRowHandle.ToString(), out rowNum);
            grdPrinter.DataSource = AIMManager.GetPrinterDefaultAll();
            grdPrinterView.FocusedRowHandle = rowNum;

        }

        private void DataBindlue()
        {

            //--------------------------------------------------------------
            //PRINTER TYPE
            List<PrinterTypeInfo> printerType = new List<PrinterTypeInfo>();
            printerType = AIMManager.GetPrinterType();
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
                //8/23/18
                //0, "--Select--"
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


        private void grdPrinterView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                PrinterDefaultInfo printerDefalt = new PrinterDefaultInfo();
                printerDefalt = (PrinterDefaultInfo)grdPrinterView.GetFocusedRow();
                printerDefalt.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                if (printerDefalt != null)
                {
                    AIMManager.SavePrinterDefault(ref printerDefalt);
                }
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                PrinterDefaultInfo printerDefault = new PrinterDefaultInfo();
                printerDefault = (PrinterDefaultInfo)grdPrinterView.GetFocusedRow();

                //Before deleting, check user-printers.  If a user-printer exists for the printer TYPE being deleted, 
                //  pop msg and do not delete printer. (Force user to delete user-printer first.)
                PrinterUserDefaultInfo user = new PrinterUserDefaultInfo();
                user = AIMManager.GetUserPrinterDefault(printerDefault.PrinterTypeID);
                if (user != null)
                {
                    String msg = "";
                    msg = "User(s) are currently set up with this printer type.";
                    msg += Environment.NewLine + Environment.NewLine + "Before this printer can be deleted, ";
                    msg += Environment.NewLine + "         you must first delete the User Printers with this printer type. ";
                    msg += Environment.NewLine + Environment.NewLine + "Delete the user printers by clicking the 'User Printers ...' button above.";
                    MessageBox.Show(msg, "Warning...");
                    return;
                }


                printerDefault.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeletePrinterDefault(printerDefault.PrinterDefaultID);
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                PrinterDefaultInfo printerDefault = new PrinterDefaultInfo();


                if ((Int32)luePrinterType.EditValue == 0)
                {
                    MessageBox.Show("Please enter a Printer Type.", "Warning...", MessageBoxButtons.OK);
                    luePrinterType.Focus();
                    return;
                }

                //8/23/18 - commented
                //if (luePrinter.Text.ToString() == "--Select--" || luePrinter.Text.ToString().Length == 0)
                //{
                //	MessageBox.Show("Please select a Printer.", "Warning...", MessageBoxButtons.OK);
                //	luePrinter.ShowPopup();
                //	return;
                //}

                //Check for duplicates
                PrinterDefaultInfo printerType = AIMManager.GetPrinterDefault((Int32)luePrinterType.EditValue);
                if (printerType != null)
                {
                    MessageBox.Show("This printer type has already been assigned a printer.", "Warning...", MessageBoxButtons.OK);
                    return;
                }
                printerDefault.PrinterDefaultID = 0;
                printerDefault.PrinterTypeID = (Int32)luePrinterType.EditValue;
                printerDefault.PrinterName = luePrinter.Text.ToString();
                printerDefault.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SavePrinterDefault(ref printerDefault);
                if (error.ErrorNumber > 0)
                {
                    return;
                }

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

        public static String GetPrinter(PrinterTypeEnum num, Int32 cfcUserID)
        {
            //6/12/23            
            Int32 pos = 0;
            Int32 posSpace = 0;                             //11/19/24 
            String leftChars = String.Empty;
            String rightChars = String.Empty;
            String printerNameNew = String.Empty;
            String printerName = String.Empty;
            String printerNamePart = String.Empty;           //11/19/24
            Boolean isFound = true;                         //Task1102 DB251103

            List<String> systemPrinters = new List<String>();
            foreach (string sprinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                systemPrinters.Add(sprinter);
            }

            PrinterTypeInfo printerType = AIMManager.GetOnePrinterType((Int32)num, cfcUserID);
            if (printerType != null)
            {
                //6/12/23
                printerName = printerType.PrinterName;

                if (printerName.ToUpper() == "DEFAULT")
                {
                    isFound = false;
                    for (int i = 0; i < systemPrinters.Count - 1; i++)         //Task1102 DB251002 Start
                    {
                        PrinterSettings defaultPrinter = new PrinterSettings();
                        printerName = AIMDataAccess.DefaultPrinterName();
                        if (systemPrinters[i].Contains(printerName))
                        {
                            isFound = true;
                            return systemPrinters[i];
                        }
                    }                                                           //Task1102 DB251002 End
                }
                else
                {
                    if (printerType.PrinterName.Contains("redirected"))
                    {
                        pos = printerType.PrinterName.IndexOf("redirected");
                        leftChars = printerType.PrinterName.Substring(0, pos);

                        //11/19/24
                        //get position of first space before "redirected"
                        posSpace = leftChars.LastIndexOf(" ", leftChars.Length);

                        //get printer name (without "redirected")
                        printerNamePart = printerType.PrinterName.Substring(0, posSpace);

                        //11/19/24                    
                        //loop thru user printers and if printer has "redirected" on it and the printer-name = default printer name, update it to the default (with redirect#)
                        List<PrinterUserDefaultInfo> userPrinters = new List<PrinterUserDefaultInfo>();
                        userPrinters = AIMManager.GetPrinterList((Int32)num);
                        for (int i = 0; i < userPrinters.Count; i++)
                        {
                            if (userPrinters[i].PrinterName.Contains("redirected"))
                            {
                                if (userPrinters[i].PrinterName.Contains(printerNamePart))
                                {
                                    PrinterUserDefaultInfo printerUserDefault = new PrinterUserDefaultInfo();
                                    printerUserDefault.PrinterUserDefaultID = userPrinters[i].PrinterUserDefaultID;
                                    printerUserDefault.CFCUserID = userPrinters[i].CFCUserID;
                                    printerUserDefault.PrinterTypeID = userPrinters[i].PrinterTypeID;
                                    printerUserDefault.PrinterName = printerName;
                                    printerUserDefault.UpdateCFCUserID = cfcUserID;

                                    if (printerUserDefault != null)
                                    {
                                        AIMManager.SaveUserPrinterDefault(ref printerUserDefault);
                                    }
                                }
                            }
                        }
                        isFound = false;
                        for (int i = 0; i < systemPrinters.Count; i++)                    //Task1102 DB251103
                        {
                            if (systemPrinters[i].Contains("redirected"))
                            {
                                if (systemPrinters[i].Contains(printerNamePart))
                                {
                                    isFound = true;
                                    return systemPrinters[i];
                                }
                            }
                        }

                    }
                    else
                    {
                        isFound = false;
                        for (int i = 0; i < systemPrinters.Count; i++)
                        {
                            if (systemPrinters[i].Contains(printerName))
                            {
                                isFound = true;
                                return systemPrinters[i];
                            }
                        }
                    }

                    if (isFound == false)
                    {
                        return AIMDataAccess.DefaultPrinterName();
                    }
                }

                //Task1102 DB251006  
                //---------------------------------------------------------------
                //6/12/23 
                ////CHECK FOR REDIRECTED 
                //if (printerType.PrinterName.Contains("redirected"))
                //{                                      
                //    pos = printerType.PrinterName.IndexOf("redirected");
                //    leftChars = printerType.PrinterName.Substring(0, pos);

                //    //11/19/24
                //    //get position of first space before "redirected"
                //    posSpace = leftChars.LastIndexOf(" ", leftChars.Length);

                //    //get printer name (without "redirected")
                //    printerNamePart = printerType.PrinterName.Substring(0, posSpace);

                //    //11/19/24                    
                //    //loop thru user printers and if printer has "redirected" on it and the printer-name = default printer name, update it to the default (with redirect#)
                //    List<PrinterUserDefaultInfo> userPrinters = new List<PrinterUserDefaultInfo>();
                //    userPrinters = AIMManager.GetPrinterList((Int32)num);                    
                //    for (int i = 0; i < userPrinters.Count; i++)
                //    {
                //        if (userPrinters[i].PrinterName.Contains("redirected"))
                //        {
                //            if (userPrinters[i].PrinterName.Contains(printerNamePart))
                //            {
                //                PrinterUserDefaultInfo printerUserDefault = new PrinterUserDefaultInfo();
                //                printerUserDefault.PrinterUserDefaultID = userPrinters[i].PrinterUserDefaultID;
                //                printerUserDefault.CFCUserID = userPrinters[i].CFCUserID;
                //                printerUserDefault.PrinterTypeID = userPrinters[i].PrinterTypeID;
                //                printerUserDefault.PrinterName = printerName;
                //                printerUserDefault.UpdateCFCUserID = cfcUserID;
                //                if (printerUserDefault != null)
                //                {
                //                    AIMManager.SaveUserPrinterDefault(ref printerUserDefault);
                //                }
                //            }
                //        }
                //    }

                //    //11/19/24
                //    //rightChars = printerType.PrinterName.Substring(pos + 10);
                //    //printerName = leftChars + "redirected" + rightChars;
                //    /////printerName = printerNameNew;
                //}
                //---------------------------------------------------------------

                //6/12/23
                //if (systemPrinters.Contains(printerType.PrinterName))
                //Task1102 DB250930 if (systemPrinters.Contains(printerName))

                //Task1102 DB251006 if (systemPrinters.Contains(printerNamePart))
                //Task1102 DB251006 {
                //Task1102 DB251006     return printerName;
                //Task1102 DB251006 }
                //Task1102 DB251006 else
                //Task1102 DB251006 {
                return String.Empty;
                //Task1102 DB251006 }
            }
            else
            {
                MessageBox.Show("The " + num.ToString() + " printer needs to be added under Setup > System Settings > Printer Setup.", "Warning...", MessageBoxButtons.OK);
                return String.Empty;
            }
        }

        private void btnPreviewResults_Click(object sender, EventArgs e)
        {

            try
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grdPrinter, "Printer Setup");
                CFCAIMCore.Global.previewGridResults(grdPrinter, "Printer Setup");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            ttip.SetToolTip(btnAddUserPrinter, "Add User Printer ...");
        }

        private void luePrinter_Enter(object sender, EventArgs e)
        {
            luePrinter.SelectAll();
        }

        private void luePrinter_Click(object sender, EventArgs e)
        {
            luePrinter.SelectAll();
        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void PrinterSetup_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
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

        private void luePrinterType_Enter(Object sender, EventArgs e)
        {
            luePrinterType.SelectAll();
        }

        private void luePrinterType_Click(Object sender, EventArgs e)
        {
            luePrinterType.SelectAll();
        }

        private void btnAddUserPrinter_Click(Object sender, EventArgs e)
        {
            try
            {
                Close();
                AIMGlobal.open_form(printerUserDefault = new PrinterUserDefault());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }
    }
}
