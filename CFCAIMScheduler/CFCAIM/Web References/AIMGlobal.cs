using CFCAIM.Maintenance;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using CFCAIMCore.Entity;
using System.Runtime.InteropServices;
using System.IO;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

namespace CFCAIM
{
    public class AIMGlobal
	{
		//private static DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(CFCAIM.WaitForm1);
		public static MathTypeEnum AnalysisMath = MathTypeEnum.Mean;
                
		private static MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];

		//------------------------------------------------------------------
		// import the necessary API function so .NET can
		// marshall parameters appropriately
		[DllImport("user32.dll")]
		static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

		// constants for the mouse_input() API function
		private const int MOUSEEVENTF_MOVE = 0x0001;
		private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
		private const int MOUSEEVENTF_LEFTUP = 0x0004;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
		private const int MOUSEEVENTF_RIGHTUP = 0x0010;
		private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
		private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
		private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

		// simulates movement of the mouse.  parameters specify changes
		// in relative position.  positive values indicate movement
		// right or down
		public static void Move(int xDelta, int yDelta)
		{
			mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
		}


		// simulates movement of the mouse.  parameters specify an
		// absolute location, with the top left corner being the
		// origin
		public static void MoveTo(int x, int y)
		{
			mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, 0);
		}


		// simulates a click-and-release action of the left mouse
		// button at its current position
		public static void LeftClick()
		{
			mouse_event(MOUSEEVENTF_LEFTDOWN, Control.MousePosition.X, Control.MousePosition.Y, 0, 0);
			mouse_event(MOUSEEVENTF_LEFTUP, Control.MousePosition.X, Control.MousePosition.Y, 0, 0);
		}
        //------------------------------------------------------------------

        //5/29/19
        public static void focusedRowGreen(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                
                //e.Appearance.BackColor = Color.Green;
                //e.Appearance.BackColor = Color.SeaGreen;
                //e.Appearance.BackColor = Color.SlateBlue;
                e.Appearance.BackColor = Color.Teal;

                e.Appearance.ForeColor = Color.White;
            }
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
                
        //4/12/22 - this sorts alphanumeric column the way that makes the most sense!  
        // For this to work, set the GridColumn.SortMode property to ColumnSortMode.Custom and then add the columns to the function below
        
        public static void sortGridColumn(object sender, CustomColumnSortEventArgs e)
        {            
            int value1, value2;

            //--------------------------------
            if (e.Value1 == null)
            {
                value1 = 0;
            }
            else
            {
                int.TryParse(e.Value1.ToString(), out value1);
            }

            //--------------------------------
            if (e.Value2 == null)
            {
                value2 = 0;
            }
            else
            {
                int.TryParse(e.Value2.ToString(), out value2);
            }

            //////{
            //////    if (int.TryParse(e.Value1.ToString(), out value1) && int.TryParse(e.Value2.ToString(), out value2))
            //////    {
                    e.Handled = true;
                    e.Result = value1.CompareTo(value2);
            //////    }
            //////}
           
        }


        #region Splash Screen Functions                             
        public static void SplashScreenShow()
		{
			_mainMenu.SplashScreenShow();
		}

		public static void SplashScreenHide()
		{
			_mainMenu.SplashScreenHide();
		}
        #endregion
                

		public static DataMapping dataMapping;		
		public static Instrument instrument;
		public static PrinterSetup printerS;		
		public static ExportBatchPending exportbatchpending;
		public static AIMOwnerLocationInfo aimOwnerLocation;
		public static Boolean aimDatabaseExists = true;        
        public static Boolean saveNewViewTemplate = false;
		public static Boolean firstTimeReloadData = true;

        //4/5/18 - used on SimpleReports screen -- sent to ReportTemplate for Sample Custom Report template set-up
        public static Boolean saveReportTemplateAs = false;
        public static Stream streamNew;
        public static Int32 templateReportID = 0;
        public static DateTime templateFromDate;
        public static DateTime templateToDate;
        public static Int32 templateMaterialType = 0;
        public static Int32 templateOwnerLocationID = 0;
        public static Int32 templateID = 0;

        //9/1/23 - used on SimpleReports screen - used to link with ReportNotes table
        public static Int32 reportID = 0;
        public static Int32 materialTypeID = 0;

        //4/20/21 - used on SimpleReports screen
        public static String sortNutrientsBy = String.Empty;

        //5/21/19
        public static Int32 templateAIMMaterialID = 0;
        public static Int32 templateAIMOwnerID = 0;
        public static Int32 templateAIMOwnerLocationID = 0;
        public static Int32 templateAIMSupplierID = 0;
        public static Int32 templateAIMSupplierLocationID = 0;
        public static Int32 templateMaterialTypeID = 0;


        //4/12/18 - used on SimpleReports screen -to determine is template is for a grid or pivot (or neither)
        public static Int32 templateType = 0;
                
        public static Int32 exportbatchAIMOwnerLocationID = 0;
		public static String exportbatchCFCPlantName = "";
		public static Boolean exportbatchMissingIngredientOrNutrient = false;
        public static Int32 exportbatchIngredientGroupID = 0;                             //10/16/23
        public static String exportbatchIngredientGroupName = "";                         //1/9/24
        public static String exportbatchPlantSingleOrGroup = "";                          //1/9/24    
        public static Int32 exportbatchPlantGroupID = 0;                                  //1/12/24
        public static String exportbatchPlantGroupName = "";                              //1/12/24


        //used for system/internal imports.
        public static List<InternalImportValidateSampleInfo> internalImportValidateSampleHeader = new List<InternalImportValidateSampleInfo>();
		public static List<SystemImportCFCTechXMLSampleInfo> systemImportCFCTechXMLSampleHeader = new List<SystemImportCFCTechXMLSampleInfo>();
		public static List<FilesInfo> internalFiles = new List<FilesInfo>();
		public static Int32 importError = 0;
		public static Int32 lastValidateID = 0; //used on import screen when multiple files are selected to be imported        
		public static String sqlDatabaseConnectionName = "";
		public static Int32 importConfigID = 0;

		public static String sqlNewOrEdit = "";
        public static String systemTemplateName = "";
        public static Int32 systemTemplateDataSourceTypeID = 0;

		//used for INTERNAL Import Update of cross-references
		public static Int32 internalDataSourceTypeID = 0;
		public static String internalDataSourceName = "";
		public static String internalRequiredField = "";
		public static Int32 internalDataSourceDefinitionID = 0;
		public static Int32 internalImportError = 0;
		public static Int32 importDataSourceTypeID = 0;
		public static String importDataBindDataSourceType = "";	   

		//used for user-defined imports.  When there is an error, user can click on button and specific maintenance screen will pop up
		public static Int32 userdefinedImportError = 0;
		public static Boolean stopPreviewingImport = false;
		public static Int32 importValidateError = 0;
		public static Boolean generateSampleCode = false;
		public static Boolean sampleCodeMissing = false;
		public static String lblWizardCaption = "";
		public static Boolean ImportMappingTabControlVisible = false;		
        public static Boolean sampleCodeMapped = true;
        public static Boolean useAnotherFieldAsSampleCode = false;              //4/12/21

        //8/19/22
        public static Boolean romerImport = false;  



        //12/23/20 - used on cross-ref screen (from import)
        public static Int32 userDefinedDefID = 0;

        //8/5/19
        public static String txtProgressMessage = "";

		//used for user-defined imports -- under hyperlink on import screen
		public static String importMessage1;
		public static String importMessage2;
		public static String importMessage3;
		public static String importMessage4;
		public static String importMessage5;
		
		//user-defined imports		
		public static Int32 importMissingField = 0;
		public static Int32 importTotalRecordCount = 0;			//used on progress bar
		public static Int32 importIncrement = 0;                //used on progress bar	
		public static Int32 importCounter = 0;                  //used on progress bar
		public static Int32 importTextCounter = 0;				//used on progress bar

		//used on ImportDataUserDefined and ExternalAllCrossRefUpdate forms -- to get records where cross-references are missing -- just for the import batch, not all missing cross-references
		public static List<ImportHeaderInfo> crossRef = new List<ImportHeaderInfo>();

        //11/19/21
        //used on user-defined imports (portrait)
        public static Boolean noGrouping = false;     //used on portrait imports (for the imports without any grouping and system-generated sample codes)
		
		//used for SampleExports 
		public static Int32 exportFileVal = 0;

		//5/11/17 - used on MathType form
		public static Int32 mathTypeID = 0;
		public static String txtMathTypeName = "";
		public static String txtMathTypeEquation = "";
		public static Boolean testNewMathTypeEquation = false;

        //1/3/18 - used on SampleControlEquation form
        public static Int32 equationID = 0;
        public static String txtEquationName = "";
        public static String txtQualityControlEquation = "";
        public static Boolean testNewEquation = false;
        public static Boolean newEquation = false;


        //used for EXTERNAL Advance Update of cross-references
        public static Int32 analyzerDataSourceTypeID = 0;
		public static String analyzerDataSourceName = "";
		public static String analyzerRequiredField = "";
		public static Int32 analyzerDataSourceDefinitionID = 0;

        //analysis drilldown        
		public static List<AnalysisInfo> sampleDetail;
        public static List<AnalysisProductInfo> sampleDetailProduct;        //3/1/21

        //12/20/22 - commented, no longer necessary
        ////public static Boolean FromAnalysisProduct = false;                  //3/1/21

        //sample warning messages drilldown
        public static Int32 sampleResultPredictID = 0;
		public static Int32 analyzerNutrientID = 0;
		public static Boolean sampleResultsIsActive = true;

		//data mapping wizard
		public static Boolean DataMappingTabControlVisible = false;
		public static Int32 dataMapID = 0;
		public static Boolean dataMapForm = false;

		//used for FORMULATION cross-reference screen
		public static String formulationCrossReference = "";

		//used on the Analysis form - when the formulation xref screen is called (from a hyperlink on the grid) so that the ownerlocation tab opens automatically
		public static Boolean ownerLocationTab = false;

		//used on the Sample form - when the aim maintenance screen is called (from the owner, owner-loc, supplier & supplier-loc hyperlink labels) so that the correct tab opens automatically
		public static Boolean aimMaintenance = false;
		public static Boolean aimOwnerTab = false;
		public static Boolean aimOwnerLocationTab = false;
		public static Boolean aimSupplierTab = false;
		public static Boolean aimSupplierLocationTab = false;

        //5/26/20
        public static Boolean aimSupplierGuaranteesTab = false;

        //7/30/20
        public static Boolean aimMaterialStockStatusTab = false;
        public static Boolean aimUnitTypeTab = false;
        public static Boolean aimWarningLimitsTab = false;

        
        public static Boolean aimSpecificIngredientsTab = false;                        //11/21/22
        public static Boolean aimSpecificProductsTab = false;                           //5/18/23
        public static Boolean aimOwnerLocationGroupTab = false;                         //11/22/22
        public static Boolean aimSupplierGroupTab = false;                              //8/30/23
        public static Boolean aimSupplierLocationGroupTab = false;                      //8/30/23                                

        //used on user-defined import screen, when the analyzer maintenance screen is called (when hyperlink is clicked) so that the correct tab opens automatically
        public static Boolean analyzerMaterialTab = false;
        public static Boolean analyzerOwnerTab = false;
        public static Boolean analyzerOwnerLocationTab = false;
        public static Boolean analyzerSupplierTab = false;
        public static Boolean analyzerSupplierLocationTab = false;

        //7/30/20
        public static Boolean analyzerNutrientTab = false;
        public static Boolean analyzerEquationParameterTab = false;
                
        //12/21/20
        public static Boolean analyzerSupplierLocationCombinedTab = false;

        //used on Mass Entry tab (Sample form) - when mass-entry hyperlink is clicked, set taborder form tab = Mass Entry
        public static Boolean massEntryTab = false;
		public static Int32 massEntryTemplateID = 0;

        //used on Quality Control tab (Sample form) - when mass-entry hyperlink is clicked, set taborder form tab = Quality Control
        public static Boolean qualityControlMassEntryTab = false;
        public static Int32 qualityControlMassEntryID = 0;

        //used on the Analysis cross-reference form - under material/nutrient code hyperlinks so the correct tab opens automatically
        public static Boolean aimMaterialTab = false;
		public static Boolean aimNutrientTab = false;

        //used on Sample, AIMMaterialRefAnalyzerMaterial hyperlink        
        public static Int32 PNEDataSourceTypeID = 0;                                    //1/19/24


        //used on Labs tab (Sample form) - when lab list hyperlink is clicked, set taborder form tab = Lab List
        public static Boolean testTypeTab = false;
        public static Int32 testTypeHeaderID = 0;
        public static Boolean testTypeGroupTab = false;                                 //10/4/23
        public static Int32 testTypeGroupID = 0;                                        //10/4/23
        
        public static String aimMaterialName = "";
		public static String aimNutrientName = "";
		public static String sampleIDFrom = "";
		public static String sampleIDTo = "";
		
		public static String youtubeVideoKey { get; set; }
		public static List<AnalysisInfo> analysisDataList { get; set; }
        public static List<AnalysisInfo> analysisDataListFiltered { get; set; }         //10/5/23
        public static List<AnalysisInfo> analysisWarningList { get; set; }
		public static Analysis analysisForm { get; set; }           
        public static List<SampleResultInfo> sampleResult { get; set; }

        public static Int32 chartAimNutrientID = 0;
		public static Int32 chartAimMaterialID = 0;
		public static DateTime chartBegDate;
		public static DateTime chartEndDate;
		public static AnalysisQueryEnum chartAnalysisQueryType = AnalysisQueryEnum.NoGroup;
		public static Int32 chartQueryTemplateID = 0;

        //1/4/23
        public static String chartArchiveVersion = "";        

        public static OutlierTypeEnum chartOutlierType = OutlierTypeEnum.NoOutlierExcluded;
		public static Boolean chartScreen = false;

        //8/3/17 - used with Analysis and ExportBatchPending screens
        public static Boolean exportComingFromAnalysis = false;

        //8/20/18 - used with User-Defined Import (when user is actually previewing import data) and on User Defined Import main form (under x-ref button).
        public static Boolean dataSourceXRefComingFromPreviewImportScreen = false;


        //used with Analysis and Charts screens
        public static Boolean comingFromAnalysis = false;
        public static Boolean analysisAllRecords = true;
        public static Boolean analysisOutliersOnly = false;
        public static Boolean analysisExcludeExtremeOutliers = false;  
		public static Boolean analysisExcludeMildOutliers = false;
		public static Boolean analysisIncludeExtremeOutliers = true;
		public static Boolean analysisIncludeMildOutliers = true;

        //12/23/22 added for AnalysisProducts and Chart screens
        public static Int32 fromAnalysisIngredientOrProduct = (Int32)EnumIngredientOrProduct.AnalysisIngredient;   //default to ingredient
        public static Int32 groupMaterials = (Int32)EnumTolerance.None;
        public static Int32 nutrientToleranceHeaderID = 0;


        //2/4/19
        public static List<ChartSampleListInfo> chartSampleList;

        //2/10/20
        public static Boolean mainMenuCustomToolStrip = false;

        //2/25/21
        public static String lastImportSampleCode = String.Empty; //used on import screen when multiple files are selected to be imported       

        public static Int32 lastImportHeaderID = 0; //used on import screen when multiple files are selected to be imported
		public static Boolean showAutoGenSampleMsg = true; //used on import screen when multiple files are selected to be imported (so message doesn't show for every import file, just the first one)               
		public static List<ImportDetailInfo> importDetailList { get; set; }		

		public static String GetAIMSyncDate()
		{
			DateTime minDate = new DateTime(2000, 1, 1);

            //9/9/20 - change to use AIMCoreGlobal
            //if (AIMGlobal.aimDatabase == null || AIMGlobal.aimDatabase.SyncTime < minDate)
            if (CFCAIMCore.Global.aimDatabase == null || CFCAIMCore.Global.aimDatabase.SyncTime < minDate)

            {
				return "Never sync or having problem getting info.";
			}

            //9/9/20 - change to use AIMCoreGlobal
            //if (AIMGlobal.aimDatabase.SyncProgressPercentage == 100 || AIMGlobal.aimDatabase.SyncProgressPercentage == 0)
            if (CFCAIMCore.Global.aimDatabase.SyncProgressPercentage == 100 || CFCAIMCore.Global.aimDatabase.SyncProgressPercentage == 0)

            {
                //9/9/20 - change to use AIMCoreGlobal
                //return "Last Sync with AIM on " + AIMGlobal.aimDatabase.SyncTime;
                return "Last Sync with AIM on " + CFCAIMCore.Global.aimDatabase.SyncTime;
            }
			else
			{
				return "Synchronizing right now!";
			}
		}

		public static String AddSpaceBetweenWords(String s)
		{
			return Regex.Replace(s, @"(\B[A-Z]+?(?=[A-Z][^A-Z])|\B[A-Z]+?(?=[^A-Z]))", " $1");
		}

		public static String RemoveSpaceBetweenWords(String s)
		{
			return Regex.Replace(s, " ", "");
		}

		public static String appName = "CFC AIMS";
		public static String versionFileLink = "https://www.dropbox.com/s/x3138uzavqs1ei4/configWeb.xml?dl=1";
		public static String appVersion = "14.9.3.0";
		public static String updateID = "5";
        public static Boolean groupByLoadNumber = false;        //2/20/17 - for guarantee report
        public static Int32 numberOfDecimals = 8;               //11/21/22 - for guarantee report
        public static String resultOrAdjustedValue = "";        //1/18/27 - for guarantee report
        public static Boolean checkOutOfRange = false;          //1/18/27 - for guarantee report


        //pivot report
        public static String analyzerMaterialName;
		public static String fromDate;
		public static String toDate;
		public static Boolean showDate = true;
		public static String dryMatterText;

        //1/18/21
        public static Boolean showMean = true;
        public static Boolean showMedian = true;
        public static Boolean showStdDev = true;
        public static Boolean showCVpercent = true;
        public static Boolean showCount = true;
        public static Boolean showSum = true;
        public static Boolean showMeanMinus = true;
        public static Boolean showMeanPlus = true;
        public static Boolean showMin = true;
        public static Boolean showMax = true;
        public static Int32 groupbyOption = 1;

        //1/20/21
        public static Boolean showMeanGroup = true;
        public static Boolean showMedianGroup = true;
        public static Boolean showStdDevGroup = true;
        public static Boolean showCVpercentGroup = true;
        public static Boolean showCountGroup = true;
        public static Boolean showSumGroup = true;
        public static Boolean showMeanMinusGroup = true;
        public static Boolean showMeanPlusGroup = true;
        public static Boolean showMinGroup = true;
        public static Boolean showMaxGroup = true;

        //1/25/21 -- used when opening Sample screen from Analysis
        public static String sampleCode = "";
        public static Boolean fromAnalysisScreen = false;

        //6/30/21 -- used when printing Label from sample screen
        public static Boolean line1Bold = false;
        public static Boolean line2Bold = false;
        public static Boolean line3Bold = false;
        public static Boolean line4Bold = false;
        public static Boolean line5Bold = false;



        //sample comparison report
        public static String dataSourceName1;
		public static String dataSourceName2;

		public static DateTime ConvertStringToDate(String date)
		{
			// Date strings are interpreted according to the current culture.
			// If the culture is en-US, 1/8/2008 is interpreted as "January 8, 2008",
			// but if the user's computer is fr-FR, 1/8/2008 is interpreted as "August 1, 2008"						

			DateTime dt = Convert.ToDateTime(date);

			// Specify exactly how to interpret the string.
			//////IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
			IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

			// Alternate choice: If the string has been input by an end user, you might 
			// want to format it according to the current culture:
			// IFormatProvider culture = System.Threading.Thread.CurrentThread.CurrentCulture;
			DateTime dt2 = DateTime.Parse(date, culture, System.Globalization.DateTimeStyles.AssumeLocal);

			return dt2;
		}                
        public static Int32 initialCFCLoad = 0;       
        public static void SyncDatabase(String path, Int32 databaseNameID, Int32 progressBarID, String syncType)
        {
            try
            {

                //syncType:  
                //	"L" for load (full sync, including archive tables)
                //	"Q" for quick-load (ingredient/plant/nutrient only) 
                //	"U" for update (update happens on Export to formulation)

                //Call sync program --> sync is vb6 program; 
                //7 parameters (customer dbase, SQL connection, new aimDatabaseID, "L" for load, userID, ProgressBarID, InitialLoad 1(true) or 0(false) )									
                AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
                String arg = String.Empty;
                String separator = "~~";
                String sqlConn = String.Empty;
                String filePath = Application.StartupPath + @"\\CFC5AIM.exe";

                //sqlConn = "Provider=SQLNCLI11;" + CFCAIMCore.Data.AIMDataAccess._database.ConnectionString;
                sqlConn = "Provider=SQLOLEDB;" + CFCAIMCore.Data.AIMDataAccess._database.ConnectionString;

                sqlConn = sqlConn.Replace("Initial Catalog", "Database");
                aimDatabase = AIMManager.GetAIMDatabase(databaseNameID);
                arg = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}", separator, path, sqlConn, aimDatabase.AIMDatabaseID, syncType, aimDatabase.CFCUserID.ToString(), progressBarID);

                //the "hidden" option below wasn't working -- tried writing arguments to a .ini file 
                //instead of sending them in call to program.  Didn't fix.  Instead made main-menu on vb6 program invisible.
                ////String argumentPath = Application.StartupPath + @"\\CFCLoadArguments.ini";
                ////System.IO.File.WriteAllText(argumentPath, arg);	

                if (File.Exists(filePath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(filePath, arg);
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(startInfo);
                    ErrorInfo error = new ErrorInfo();
                    String msg = "";
                    error = AIMManager.GenerateAIMProductFromCFC();

                    if (error.ErrorNumber != 0)
                    {
                        msg = "When syncing AIM with Formulation, there was an error generating AIM products.";
                        msg += Environment.NewLine + Environment.NewLine;
                        msg += "Please contact IT.";
                        MessageBox.Show(msg, "Error...", MessageBoxButtons.OK);
                    }
                    error = AIMManager.GenerateAIMProductXRefFromCFC();
                    if (error.ErrorNumber != 0)
                    {
                        msg = "When syncing AIM with Formulation, there was an error generating AIM product cross-references.";
                        msg += Environment.NewLine + Environment.NewLine;
                        msg += "Please contact IT.";
                        MessageBox.Show(msg, "Error...", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("CFC5AIM.exe is missing. Contact IT.", "Error...", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //11/17/20 - (moved back from CFCAIMCore)
        public static Int32 activeDataMap = 0;

        //10/25/21
        public static Boolean blnChangeAnalysisDate = false;

        //4/15/22
        public static Int32 orphanCrossRefReport = 0;
        //Currently, these are the values this global can be assigned:
        //AnalyzerIngredient = 1,
        //AnalyzerProduct = 2,
        //FormulationIngredient = 3,
        //FormulationProduct = 4

        //4/26/22
        public static Int32 sampleActivityReportOption = 1;
        //1 = SampleActivityReport
        //2 = SampleActivityQualityControlReport

        //6/3/22 - used in Advanced Reports (SampleResultsComparisonReport, DataSourceComparisonReport)
        public static Boolean useResultAnalysisDate = false;

        //6/22/22 - used in Advanced Reports (DataSourceComparisonReport)
        public static Int32 resultCount = 0;

        public static void open_form(Form myform)
        {
            try
            {
                if (AIMGlobal.activeDataMap == 0)               
                {
                    if (Convert.ToBoolean(CFCAIM.AIMGlobal.AimDatabaseExists()) == false)
                    {
                        DataMapping form = new DataMapping();
                        form.MdiParent = _mainMenu;
                        form.Show();

                        //10/6/21 - commented, not needed                                        
                        //form.WindowState = FormWindowState.Normal;                      
                        form.Activate();

                        return;
                    }
                    else
                    {
                        return;
                    }
                }

                //4/22/22 - uncommented this, this IS necessary and NOT causing resize problems (?) 
                //10/6/21 - removed, not necessary -- and causing resize problems
                Boolean formOpened = false;
                FormCollection openForms = Application.OpenForms;
                foreach (Form item in openForms)
                {
                    if (myform.Name == item.Name)
                    {
                        //4/22/22
                        //item.WindowState = FormWindowState.Normal;
                        item.WindowState = FormWindowState.Maximized;

                        item.Activate();
                        formOpened = true;
                    }
                }
                if (formOpened == false)
                {
                    SplashScreenShow();
                    myform.MdiParent = _mainMenu;
                    myform.Show();
                    myform.WindowState = FormWindowState.Maximized;
                    SplashScreenHide();
                }
                //4/22/22 - commented
                //SplashScreenShow();
                //myform.MdiParent = _mainMenu;
                //myform.Show();
                //myform.WindowState = FormWindowState.Maximized;
                //SplashScreenHide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //8/22/23 - used on SimpleReports screen, Submitted Samples Report
        public static Boolean useSampleDate = false;

        //8/23/23 - used on Sample screen, for re-order TabControl pages (to skip any events on the TabControl when form is loaded)
        public static Boolean loadingSampleScreen = true;

        //2/23/24 - used on Sample screen, when viewing further analysis (to bring up the current sample code on further analysis screen)
        public static String sampleCodeFurtherAnalysis = "";        
             

        //9/9/20 - commented, not referenced
        //public static void ExportToConcept5()
        //{
        //}

        //-----------------------------------------------------------------------------------------------------------------------
        //EVERYTHING BELOW THIS LINE WAS MOVED TO CFCAIMCore.Global 9/9/20              

        //CM200909
        //public static void previewGridResults(DevExpress.XtraPrinting.IPrintable grid, string formName)
        //{
        //previewResultsFormName = formName;
        //PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
        //link.Component = grid;
        //link.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
        //link.Margins.Bottom = 50;
        //link.Margins.Left = 25;
        //link.Margins.Right = 25;
        //link.Margins.Top = 100;
        //link.CreateDocument();
        ////grid.PropertyEditorControl.BackColor = Color.AliceBlue;
        ////link.Component.PropertyEditorControl.BackColor = Color.AliceBlue;
        //link.ShowPreview();
        //}

        //MW200908 Moved to CFCAIMCore public static Int32 exportbatchmaintenanceHeaderID = 0;
        //CM200909 public static String exportbatchmaintenanceHeaderName = "";
        //MW200908 public static Int32 exportbatchTargetID = 0;
        //CM200909 public static Int32 importProgressBarCounter = 0;
        //CM200909 public static DateTime ExportStartDate { get; set; }
        //CM200909 public static DateTime ExportEndDate { get; set; }
        //CM200909 public static AIMDatabaseInfo aimDatabase { get; set; }   
        //MW200908 public static Int32 activeDataMap = 0;
        //MW200908 public static Int32 sampleID;

        //CM200909 public static string previewResultsFormName = "";

        //CM200909
        //public static void printGridResults(DevExpress.XtraPrinting.IPrintable grid, string formName)
        //{
        //	previewResultsFormName = formName;
        //	PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
        //	link.Component = grid;
        //	link.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
        //	link.Margins.Bottom = 50;
        //	link.Margins.Left = 25;
        //	link.Margins.Right = 25;
        //	link.Margins.Top = 100;
        //	link.CreateDocument();

        //	//grid.PropertyEditorControl.BackColor = Color.AliceBlue;
        //	//link.Component.PropertyEditorControl.BackColor = Color.AliceBlue;

        //	link.Print();
        //}

        //CM200909
        //      //4/12/18
        //      public static void previewPivotResults(DevExpress.XtraPrinting.IPrintable pivot, string formName)
        //      {
        //          previewResultsFormName = formName;
        //          using (PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem()))
        //          {
        //              link.Component = pivot;
        //              link.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
        //              link.Margins.Bottom = 50;
        //              link.Margins.Left = 25;
        //              link.Margins.Right = 25;
        //              link.Margins.Top = 100;
        //              link.CreateDocument();
        //              link.ShowPreview();
        //              //This was sent from DevExpress - works great until adding the line to add header
        //              //PrintingSystem printingSystem = new PrintingSystem();                
        //              //printableLink.Landscape = true;
        //              //printableLink.PrintingSystem = printingSystem;
        //              //printableLink.Component = pivot;                
        //              //printableLink.CreateDocument();
        //              //printableLink.PrintingSystem.SaveDocument("myDocument.prnx");
        //              //printableLink.ShowPreview();            
        //          }
        //      }

        //CM200909
        //public static void previewChartResults(DevExpress.XtraPrinting.IPrintable chartName, string formName)
        //{
        //	previewResultsFormName = formName;
        //	PrintableComponentLink chart = new PrintableComponentLink(new PrintingSystem());
        //	chart.Component = chartName;            
        //	chart.Landscape = true;
        //	chart.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
        //	chart.Margins.Bottom = 50;
        //	chart.Margins.Left = 25;
        //	chart.Margins.Right = 25;
        //	chart.Margins.Top = 100;

        //          //3/22/19 -- this allows a 4-page chart to print on 1 page
        //          chart.PrintingSystem.Document.AutoFitToPagesWidth = 1;

        //          chart.CreateDocument();
        //	chart.ShowPreview();
        //}        

        //CM200909
        //public static void printChartResults(DevExpress.XtraPrinting.IPrintable chartName, string formName)
        //{
        //	previewResultsFormName = formName;
        //	PrintableComponentLink chart = new PrintableComponentLink(new PrintingSystem());
        //	chart.Component = chartName;
        //	chart.Landscape = true;
        //	chart.CreateMarginalHeaderArea += new CreateAreaEventHandler(createHeader);
        //	chart.Margins.Bottom = 50;
        //	chart.Margins.Left = 25;
        //	chart.Margins.Right = 25;
        //	chart.Margins.Top = 100;

        //          //3/22/19 -- this allows a 4-page chart to print on 1 page
        //          chart.PrintingSystem.Document.AutoFitToPagesWidth = 1;

        //          chart.CreateDocument();
        //	chart.Print();
        //}

        //CM200909
        //      //5/22/19
        //      public static Boolean skipCompanyDateHeading = false;                       

        //CM200909
        //      public static void createHeader(object sender, CreateAreaEventArgs e)       
        //      {
        //          try
        //          {
        //              //added for landscape charts
        //              Int32 startRow = 722;
        //              Int32 startDesc = 802;
        //              if (e.Graph.ClientPageSize.Width > 1000)
        //              {
        //                  startRow = 1022;
        //                  startDesc = 1102;
        //              }
        //              //------------------------------------

        //              BrickGraphics gr = e.Graph;
        //              TextBrick textbrick;
        //              String format = "";

        //              //5/22/19
        //              if (skipCompanyDateHeading == false)
        //              {
        //                  //Company and Date
        //                  string[] rows = { "Company:", "Date:" },
        //                           desc = { AIMGlobal.CompanyName().ToString(),
        //                                  DateTime.Now.ToString("M/d/yy h:mm tt"),
        //                              };
        //                  gr.Font = new Font("Arial", 8, FontStyle.Regular);
        //                  for (int i = 0; i < 2; i++)
        //                  {
        //                      textbrick = gr.DrawString(rows[i], Color.Black, new RectangleF(startRow, 20 * i + 2, 116, 28), BorderSide.None);
        //                      textbrick = gr.DrawString(desc[i], Color.Black, new RectangleF(startDesc, 20 * i + 2, gr.ClientPageSize.Width - 380, 28), BorderSide.None);
        //                  }
        //              }

        //              //5/22/19
        //              skipCompanyDateHeading = false;

        //              //UserName
        //              format = "Printed By:   {0}";
        //              PageInfoBrick userName = e.Graph.DrawPageInfo(PageInfo.UserName, format, Color.Black, new RectangleF(0, 42, 238, 20), BorderSide.None);
        //              userName.Alignment = BrickAlignment.Far;

        //              //PageNumber
        //              format = "Page:           {0} of {1}";
        //              PageInfoBrick pageNumber = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, format, Color.Black, new RectangleF(0, 62, 238, 20), BorderSide.None);
        //              pageNumber.Alignment = BrickAlignment.Far;

        //              //Report Title
        //              gr.Font = new Font("Arial", 14, FontStyle.Bold);
        //              textbrick = gr.DrawString(previewResultsFormName.ToString(), Color.Black, new RectangleF(10, 10, 600, 28), BorderSide.None);
        //              ////DateTime
        //              //PageInfoBrick dateTime = e.Graph.DrawPageInfo(PageInfo.DateTime, "Date: {0:M/d/yy h:mm tt}", Color.Black,
        //              //									new RectangleF(0, 0, 200, 20), BorderSide.None);

        //              //dateTime.LineAlignment = BrickAlignment.Center;
        //              //dateTime.Alignment = BrickAlignment.Far;
        //              //dateTime.AutoWidth = true;

        //              ////UserName
        //              //PageInfoBrick userName = e.Graph.DrawPageInfo(PageInfo.UserName, "Printed By: {0}", Color.Black,
        //              //												new RectangleF(0, 0, 100, 20), BorderSide.None);
        //              //userName.LineAlignment = BrickAlignment.Near;
        //              //userName.Alignment = BrickAlignment.Far;
        //              //userName.AutoWidth = true;

        //              ////PageNumber
        //              //PageInfoBrick pageNumber = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page: {0} of {1}", Color.Black,
        //              //												new RectangleF(0, 0, 100, 20), BorderSide.None);
        //              //pageNumber.LineAlignment = BrickAlignment.Far;
        //              //pageNumber.Alignment = BrickAlignment.Far;
        //              //pageNumber.AutoWidth = true;

        //              ////CompanyName
        //              //TextBrick companyName = e.Graph.DrawString(AIMGlobal.CompanyName(), Color.Black,
        //              //													new RectangleF(0, 0, 100, 20), BorderSide.None);				

        //              ////PageHeading
        //              //TextBrick pageHeader = e.Graph.DrawString(previewResultsFormName.ToString(), Color.Black,
        //              //													new RectangleF(0, 0, 400, 28), BorderSide.None);				
        //              //pageHeader.Location = new Point(800, 100);				
        //              //pageHeader.Font = new Font("Arial", 14);								

        //          }
        //          catch (Exception ex)
        //          {
        //              MessageBox.Show(ex.Message);
        //          }
        //      }


        //CM200910 
        //public static String CompanyName()
        //{
        //    LicensingConfigInfo _licenseConfig = new LicensingConfigInfo();
        //    _licenseConfig = CFCAIMCore.Manager.AIMManager.GetCompanyName(1);
        //    if (_licenseConfig == null)
        //    {
        //        return "";
        //    }
        //    else if (_licenseConfig.CompanyName.Length > 0)
        //    {
        //        return _licenseConfig.CompanyName;
        //    }
        //    else
        //        return "";
        //}

    }
}