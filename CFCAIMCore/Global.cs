using System;
using DevExpress.XtraPrinting.Preview;
using DevExpress.Drawing.Printing;
using DevExpress.XtraPrinting.Preview;
using System.Text;
using System.Xml;
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
        public static Form Form;
        //9/14/20
        public static String PDFPathAndFileName = String.Empty;
        private static PrintableComponentLink _activePreviewLink = null;   //1175FG 7/1/2026


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

        public static List<ExportBatchLogInfo> DataSetExportLog; //TASK951 6/3/24 cm


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
                //link.Landscape = true;
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
        #region Report Print Layout   //1175FG 7/1/2026

        public static ReportPrintLayoutInfo GetDefaultPrintLayout()
        {
            ReportPrintLayoutInfo layout = new ReportPrintLayoutInfo();
            layout.Landscape = true;
            layout.MarginLeft = 25;
            layout.MarginTop = 25;
            layout.MarginRight = 25;
            layout.MarginBottom = 25;
            return layout;
        }

        public static void ApplyPrintLayoutToLink(PrintableComponentLink link, ReportPrintLayoutInfo layout)
        {
            if (link == null)
            {
                return;
            }

            if (layout == null)
            {
                layout = GetDefaultPrintLayout();
            }

            link.Landscape = layout.Landscape;
            link.Margins.Left = layout.MarginLeft;
            link.Margins.Top = layout.MarginTop;
            link.Margins.Right = layout.MarginRight;
            link.Margins.Bottom = layout.MarginBottom;
        }

        public static ReportPrintLayoutInfo GetPrintLayoutFromLink(PrintableComponentLink link)   
        {
            if (link == null)
            {
                return GetDefaultPrintLayout();
            }

            PrintingSystemBase printingSystem = link.PrintingSystem as PrintingSystemBase;
            if (printingSystem != null && printingSystem.PageSettings != null)
            {
                ReportPrintLayoutInfo layout = new ReportPrintLayoutInfo();
                layout.Landscape = printingSystem.PageSettings.Landscape;
                layout.MarginLeft = printingSystem.PageSettings.LeftMargin;
                layout.MarginTop = printingSystem.PageSettings.TopMargin;
                layout.MarginRight = printingSystem.PageSettings.RightMargin;
                layout.MarginBottom = printingSystem.PageSettings.BottomMargin;
                return layout;
            }

            ReportPrintLayoutInfo fallbackLayout = new ReportPrintLayoutInfo();
            fallbackLayout.Landscape = link.Landscape;
            fallbackLayout.MarginLeft = Convert.ToInt32(link.Margins.Left);
            fallbackLayout.MarginTop = Convert.ToInt32(link.Margins.Top);
            fallbackLayout.MarginRight = Convert.ToInt32(link.Margins.Right);
            fallbackLayout.MarginBottom = Convert.ToInt32(link.Margins.Bottom);
            return fallbackLayout;
        }

        public static String SerializePrintLayout(ReportPrintLayoutInfo layout)
        {
            if (layout == null)
            {
                return String.Empty;
            }

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("PrintLayout");
            doc.AppendChild(root);

            XmlElement landscape = doc.CreateElement("Landscape");
            landscape.InnerText = layout.Landscape.ToString();
            root.AppendChild(landscape);

            XmlElement marginLeft = doc.CreateElement("MarginLeft");
            marginLeft.InnerText = layout.MarginLeft.ToString();
            root.AppendChild(marginLeft);

            XmlElement marginTop = doc.CreateElement("MarginTop");
            marginTop.InnerText = layout.MarginTop.ToString();
            root.AppendChild(marginTop);

            XmlElement marginRight = doc.CreateElement("MarginRight");
            marginRight.InnerText = layout.MarginRight.ToString();
            root.AppendChild(marginRight);

            XmlElement marginBottom = doc.CreateElement("MarginBottom");
            marginBottom.InnerText = layout.MarginBottom.ToString();
            root.AppendChild(marginBottom);

            return doc.OuterXml;
        }

        public static ReportPrintLayoutInfo DeserializePrintLayout(String xml)
        {
            if (String.IsNullOrWhiteSpace(xml))
            {
                return GetDefaultPrintLayout();
            }

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                ReportPrintLayoutInfo layout = new ReportPrintLayoutInfo();
                Boolean landscape = false;
                Int32 marginLeft = 25;
                Int32 marginTop = 25;
                Int32 marginRight = 25;
                Int32 marginBottom = 25;

                XmlNode landscapeNode = doc.SelectSingleNode("//Landscape");
                if (landscapeNode != null)
                {
                    Boolean.TryParse(landscapeNode.InnerText, out landscape);
                }

                XmlNode marginLeftNode = doc.SelectSingleNode("//MarginLeft");
                if (marginLeftNode != null)
                {
                    Int32.TryParse(marginLeftNode.InnerText, out marginLeft);
                }

                XmlNode marginTopNode = doc.SelectSingleNode("//MarginTop");
                if (marginTopNode != null)
                {
                    Int32.TryParse(marginTopNode.InnerText, out marginTop);
                }

                XmlNode marginRightNode = doc.SelectSingleNode("//MarginRight");
                if (marginRightNode != null)
                {
                    Int32.TryParse(marginRightNode.InnerText, out marginRight);
                }

                XmlNode marginBottomNode = doc.SelectSingleNode("//MarginBottom");
                if (marginBottomNode != null)
                {
                    Int32.TryParse(marginBottomNode.InnerText, out marginBottom);
                }

                layout.Landscape = landscape;
                layout.MarginLeft = marginLeft;
                layout.MarginTop = marginTop;
                layout.MarginRight = marginRight;
                layout.MarginBottom = marginBottom;
                return layout;
            }
            catch
            {
                return GetDefaultPrintLayout();
            }
        }

        private static void ShowPreviewNonModal(
            PrintableComponentLink link,
            String formName,
            Action<ReportPrintLayoutInfo> onPreviewClosed)
        {
            previewResultsFormName = formName;

            if (_activePreviewLink != null)
            {
                return;
            }

            _activePreviewLink = link;
            link.CreateDocument();

            PrintPreviewFormEx previewForm = new PrintPreviewFormEx();
            previewForm.PrintingSystem = link.PrintingSystem;

            previewForm.FormClosed += (sender, e) =>
            {
                ReportPrintLayoutInfo updatedLayout = GetPrintLayoutFromLink(link);

                if (onPreviewClosed != null)
                {
                    onPreviewClosed(updatedLayout);
                }

                link.Dispose();
                _activePreviewLink = null;
            };

            previewForm.Show();
        }

        public static void previewGridResults(
            DevExpress.XtraPrinting.IPrintable grid,
            String formName,
            ReportPrintLayoutInfo layout,
            Action<ReportPrintLayoutInfo> onPreviewClosed)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = grid;
            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
            ApplyPrintLayoutToLink(link, layout);
            ShowPreviewNonModal(link, formName, onPreviewClosed);
        }

        public static void previewPivotResults(
            DevExpress.XtraPrinting.IPrintable pivot,
            String formName,
            ReportPrintLayoutInfo layout,
            Action<ReportPrintLayoutInfo> onPreviewClosed)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = pivot;
            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
            ApplyPrintLayoutToLink(link, layout);
            ShowPreviewNonModal(link, formName, onPreviewClosed);
        }

        #endregion

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

        //task 1119 FG 7/21/2026 - highlight (select) the text of every dropdown on a form when the user clicks or tabs into it;
        //call once from the form constructor (after InitializeComponent) instead of adding individual Click/Enter handlers per dropdown.
        //Covers LookUpEdit, GridLookUpEdit and SearchLookUpEdit (all derive from LookUpEditBase).
        public static void EnableDropDownSelectAll(Control root)
        {
            foreach (Control control in root.Controls)
            {
                DevExpress.XtraEditors.LookUpEditBase lookUpEdit = control as DevExpress.XtraEditors.LookUpEditBase;
                if (lookUpEdit != null)
                {
                    lookUpEdit.Click += DropDown_SelectAll;
                    lookUpEdit.Enter += DropDown_SelectAll;
                }

                EnableDropDownSelectAll(control);
            }
        }
        private static void DropDown_SelectAll(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.LookUpEditBase lookUpEdit = (DevExpress.XtraEditors.LookUpEditBase)sender;
            if (lookUpEdit.Enabled)
            {
                lookUpEdit.SelectAll();
            }
        }
    }//MW200908 
}//CFCAIMCore.Global
