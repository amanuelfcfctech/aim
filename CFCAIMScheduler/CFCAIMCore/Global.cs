using System;
using System.Collections.Generic;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;

namespace CFCAIMCore
{
    public class Global
    {
        //9/14/20
        public static String PDFPathAndFileName = String.Empty;


        ////private static MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];       
        ////private static Form _mainMenu;              


        //-------------------------------------------------------------------------------------------------
        //9/8/20 - FROM HERE DOWN, COPIED FROM CFCAIM.AIMGlobal

        ////public static Form MainMenu
        ////{
        ////    set 
        ////    {
        ////        _mainMenu = value;
        ////    }
        ////}

        ////public static void open_form(Form myform)
        ////{
        ////    try
        ////    {
        ////        if (Global.activeDataMap == 0)
        ////        {
        ////            if (Convert.ToBoolean(AimDatabaseExists()) == false)
        ////            {
        ////                DataMapping form = new DataMapping();
        ////                form.MdiParent = _mainMenu;                     
        ////                form.Show();
        ////                form.WindowState = FormWindowState.Normal;
        ////                form.Activate();

        ////                return;
        ////            }
        ////            else
        ////            {
        ////                MessageBox.Show("Please select an Active Formulation Connection on the Main Menu.", "Message...");
        ////                return;
        ////            }
        ////        }
        ////        FormCollection openForms = Application.OpenForms;
        ////        Boolean formOpened = false;
        ////        foreach (Form item in openForms)
        ////        {
        ////            if (myform.Name == item.Name)
        ////            {
        ////                item.WindowState = FormWindowState.Normal;
        ////                item.Activate();
        ////                formOpened = true;
        ////            }
        ////        }
        ////        if (formOpened == false)
        ////        {
        ////            SplashScreenShow();
        ////            myform.MdiParent = _mainMenu;                   
        ////            myform.Show();
        ////            SplashScreenHide();
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        MessageBox.Show(ex.Message);
        ////    }
        ////}

        ////public static Int32 activeDataMap = 0;

        public static AIMDatabaseInfo aimDatabase { get; set; }
        public static Int32 exportbatchTargetID = 0;
        
        public static Int32 sampleID;
        public static Int32 exportbatchmaintenanceHeaderID = 0;
        public static string previewResultsFormName = "";
        public static DateTime ExportStartDate { get; set; }
        public static DateTime ExportEndDate { get; set; }
        public static String exportbatchmaintenanceHeaderName = "";
        public static Int32 importProgressBarCounter = 0;

        //10/6/21 - for resize issue on DataMapping screen
        public static Int32 selectedDataMapID = 0;

        public static void previewGridResults(DevExpress.XtraPrinting.IPrintable grid, string formName)
        {
            previewResultsFormName = formName;
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = grid;
            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
            link.Margins.Bottom = 50;
            link.Margins.Left = 25;
            link.Margins.Right = 25;
            link.Margins.Top = 100;
            link.CreateDocument();

            //grid.PropertyEditorControl.BackColor = Color.AliceBlue;
            //link.Component.PropertyEditorControl.BackColor = Color.AliceBlue;


            link.ShowPreview();
        }

        //5/22/19
        public static Boolean skipCompanyDateHeading = false;

        public static void createHeader(object sender, CreateAreaEventArgs e)
        {
            try
            {
                //added for landscape charts
                Int32 startRow = 722;
                Int32 startDesc = 802;
                if (e.Graph.ClientPageSize.Width > 1000)
                {
                    startRow = 1022;
                    startDesc = 1102;
                }
                //------------------------------------

                BrickGraphics gr = e.Graph;
                TextBrick textbrick;
                String format = "";

                //5/22/19
                if (skipCompanyDateHeading == false)
                {
                    //Company and Date
                    string[] rows = { "Company:", "Date:" },
                             desc = { CompanyName().ToString(),
                                    DateTime.Now.ToString("M/d/yy h:mm tt"),
                                };
                    gr.Font = new Font("Arial", 8, FontStyle.Regular);
                    for (int i = 0; i < 2; i++)
                    {
                        textbrick = gr.DrawString(rows[i], Color.Black, new RectangleF(startRow, 20 * i + 2, 116, 28), BorderSide.None);
                        textbrick = gr.DrawString(desc[i], Color.Black, new RectangleF(startDesc, 20 * i + 2, gr.ClientPageSize.Width - 380, 28), BorderSide.None);
                    }
                }

                //5/22/19
                skipCompanyDateHeading = false;

                //UserName
                format = "Printed By:   {0}";
                PageInfoBrick userName = e.Graph.DrawPageInfo(PageInfo.UserName, format, Color.Black, new RectangleF(0, 42, 238, 20), BorderSide.None);
                userName.Alignment = BrickAlignment.Far;

                //PageNumber
                format = "Page:           {0} of {1}";
                PageInfoBrick pageNumber = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, format, Color.Black, new RectangleF(0, 62, 238, 20), BorderSide.None);
                pageNumber.Alignment = BrickAlignment.Far;

                //Report Title
                gr.Font = new Font("Arial", 14, FontStyle.Bold);
                textbrick = gr.DrawString(previewResultsFormName.ToString(), Color.Black, new RectangleF(10, 10, 600, 28), BorderSide.None);
                ////DateTime
                //PageInfoBrick dateTime = e.Graph.DrawPageInfo(PageInfo.DateTime, "Date: {0:M/d/yy h:mm tt}", Color.Black,
                //									new RectangleF(0, 0, 200, 20), BorderSide.None);

                //dateTime.LineAlignment = BrickAlignment.Center;
                //dateTime.Alignment = BrickAlignment.Far;
                //dateTime.AutoWidth = true;

                ////UserName
                //PageInfoBrick userName = e.Graph.DrawPageInfo(PageInfo.UserName, "Printed By: {0}", Color.Black,
                //												new RectangleF(0, 0, 100, 20), BorderSide.None);
                //userName.LineAlignment = BrickAlignment.Near;
                //userName.Alignment = BrickAlignment.Far;
                //userName.AutoWidth = true;

                ////PageNumber
                //PageInfoBrick pageNumber = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page: {0} of {1}", Color.Black,
                //												new RectangleF(0, 0, 100, 20), BorderSide.None);
                //pageNumber.LineAlignment = BrickAlignment.Far;
                //pageNumber.Alignment = BrickAlignment.Far;
                //pageNumber.AutoWidth = true;

                ////CompanyName
                //TextBrick companyName = e.Graph.DrawString(AIMGlobal.CompanyName(), Color.Black,
                //													new RectangleF(0, 0, 100, 20), BorderSide.None);				

                ////PageHeading
                //TextBrick pageHeader = e.Graph.DrawString(previewResultsFormName.ToString(), Color.Black,
                //													new RectangleF(0, 0, 400, 28), BorderSide.None);				
                //pageHeader.Location = new Point(800, 100);				
                //pageHeader.Font = new Font("Arial", 14);								

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static String CompanyName()
        {
            LicensingConfigInfo _licenseConfig = new LicensingConfigInfo();
            _licenseConfig = CFCAIMCore.Manager.AIMManager.GetCompanyName(1);
            if (_licenseConfig == null)
            {
                return "";
            }
            else if (_licenseConfig.CompanyName.Length > 0)
            {
                return _licenseConfig.CompanyName;
            }
            else
                return "";
        }

        public static List<ExportBatchMaintenanceReportInfo> DataSetExportBatchDetail;  //this was copied from CFCAIM.Report.ReportFunctions

        public static List<ExportBatchLogInfo> DataSetExportLog; //TASK951 6/3/24  'carol??


        #region Splash Screen Functions
        public static void SplashScreenShow()
        {
            ////_mainMenu.SplashScreenShow();            //11/6/20 - prob
        }

        public static void SplashScreenHide()
        {
            ////_mainMenu.SplashScreenHide();           //11/6/20 - prob
        }
        #endregion

        public static Guid ConvertIntToGuid(Int32 val)
        {
            byte[] bytes = new byte[32];
            BitConverter.GetBytes(val).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
        public static Int32 CheckUserPermissions(Int32 cfcUserID, Int32 permissionID)
        {
            Int32 rtn = 0;
            String msg = "";
            List<UserAccessInfo> userAccess = new List<UserAccessInfo>();
            userAccess = AIMManager.GetUserAccess(cfcUserID, permissionID);
            if (userAccess.Count == 0)
            {
                if (permissionID == (Int32)EnumPermissionTable.AnalysisExport)
                {
                    msg = "You do not have permission to export analysis data.";
                    MessageBox.Show(msg, "Warning...");
                }
                else if (permissionID == (Int32)EnumPermissionTable.FormulationOptions)
                { }
                else if (permissionID == (Int32)EnumPermissionTable.AllowChangeSampleMaterial)      //4/1/19
                { }
                else
                {
                    msg = "You do not have permission to access this form.";
                    MessageBox.Show(msg, "Warning...");
                }

                rtn = -1;
            }
            return rtn;
        }

        public static Boolean AimDatabaseExists()
        {
            Boolean rtn = false;
            List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
            aimDatabase = AIMManager.GetAIMDatabase(true);
            if (aimDatabase.Count != 0)
            {
                rtn = true;
            }
            return rtn;
        }

        
        public static void printGridResults(DevExpress.XtraPrinting.IPrintable grid, string formName)
        {
            previewResultsFormName = formName;
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = grid;
            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
            link.Margins.Bottom = 50;
            link.Margins.Left = 25;
            link.Margins.Right = 25;
            link.Margins.Top = 100;
            link.CreateDocument();

            //grid.PropertyEditorControl.BackColor = Color.AliceBlue;
            //link.Component.PropertyEditorControl.BackColor = Color.AliceBlue;

            link.Print();
        }

        //4/12/18
        public static void previewPivotResults(DevExpress.XtraPrinting.IPrintable pivot, string formName)
        {
            previewResultsFormName = formName;
            using (PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem()))
            {
                link.Component = pivot;
                link.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
                link.Margins.Bottom = 50;
                link.Margins.Left = 25;
                link.Margins.Right = 25;
                link.Margins.Top = 100;
                link.CreateDocument();
                link.ShowPreview();
                //This was sent from DevExpress - works great until adding the line to add header
                //PrintingSystem printingSystem = new PrintingSystem();                
                //printableLink.Landscape = true;
                //printableLink.PrintingSystem = printingSystem;
                //printableLink.Component = pivot;                
                //printableLink.CreateDocument();
                //printableLink.PrintingSystem.SaveDocument("myDocument.prnx");
                //printableLink.ShowPreview();            
            }
        }

        public static void previewChartResults(DevExpress.XtraPrinting.IPrintable chartName, string formName)
        {
            previewResultsFormName = formName;
            PrintableComponentLink chart = new PrintableComponentLink(new PrintingSystem());
            chart.Component = chartName;
            chart.Landscape = true;
            chart.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
            chart.Margins.Bottom = 50;
            chart.Margins.Left = 25;
            chart.Margins.Right = 25;
            chart.Margins.Top = 100;

            //3/22/19 -- this allows a 4-page chart to print on 1 page
            chart.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            chart.CreateDocument();
            chart.ShowPreview();
        }

        public static void printChartResults(DevExpress.XtraPrinting.IPrintable chartName, string formName)
        {
            previewResultsFormName = formName;
            PrintableComponentLink chart = new PrintableComponentLink(new PrintingSystem());
            chart.Component = chartName;
            chart.Landscape = true;
            chart.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
            chart.Margins.Bottom = 50;
            chart.Margins.Left = 25;
            chart.Margins.Right = 25;
            chart.Margins.Top = 100;

            //3/22/19 -- this allows a 4-page chart to print on 1 page
            chart.PrintingSystem.Document.AutoFitToPagesWidth = 1;

            chart.CreateDocument();
            chart.Print();
        }
    }//MW200908 
}//CFCAIMCore.Global
