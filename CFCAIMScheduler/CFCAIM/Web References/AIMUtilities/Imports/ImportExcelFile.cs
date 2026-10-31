using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Data.Objects;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;

namespace CFCAIM.AIMUtilities.Imports
{
	public class ImportExcelFile 
	{        
        //header fields
        public static String _material = "";
		public static String _owner = "";
		public static String _ownerLocation = "";
		public static String _supplier = "";
		public static String _supplierLocation = "";
		public static String _sampleCode = "";
		public static String _alternateCode = "";
		public static String _sampleDate = "";
		public static String _invoiceNumber = "";
		public static String _poNumber = "";
		public static String _ticketNumber = "";
		public static String _vehicleNumber = "";	          
        public static String _billOfLading = "";
        public static String _contractNumber = "";
        public static String _loadNumber = "";
        public static String _lotNumber = "";
        public static String _notes = "";

        //detail fields
        public static String _analyzerNutrientCode = "";
        public static String _unitType = "";
        public static String _resultValueString = "";
        public static String _analyzerDataSourceTypeName = "";

        //aim required fields
        public static Int32 _aimMaterialID = 0;
        public static Int32 _aimOwnerID = 0;
        public static Int32 _aimOwnerLocationID = 0;
        public static Int32 _aimSupplierID = 0;
        public static Int32 _aimSupplierLocationID = 0;

        public static String _saveSampleCode = "";        
        public static String _headerMessage = "";
        public static String _dateForGeneratedSampleCode = DateTime.Now.ToString("yyyyMMdd");   //fields used for generated sample-code	

        public static DateTime _dtSampleDate = DateTime.Today;

        public static Boolean _firstRec = true;                                                 //used for auto-generated sample codes			      
        public static Boolean _blnWriteHeader = false;
        public static Boolean _blnWriteDetail = false;
        public static Boolean _sampleCodeExists = false;       
        public static Int32 _existingSampleID = 0;

        public static Int32 _saveHeaderID = 0;
        public static Int32 _existingHeaderID = 0;
        public static Int32 _headerID;
        public static Int32 _headerMessageStatus = 0;
        public static Int32 _sampleCounter = 0;                                                 //fields used for generated sample-code	       
        public static Int32 _detailID = 1;
        public static Int32 _dateFormatID = 0;
        public static Int32 _importFileTypeID = 0;
        public static Int32 _sheetNumber = 1;
        public static List<ImportHeaderInfo> _workHeader = new List<ImportHeaderInfo>();
        public bool WorkerReportsProgress { get; set; }

        //11/18/21 - commented, replaced with ColumnToUseForGrouping below
        ////public static String _useAsSampleBreak = "";

        //11/19/21
        public static Boolean _useThisFieldAsSampleCode = false;

        public static Int32 _detailBeginRow = 1;        
        
        public static List<ImportHeaderInfo> ExcelImportSamples(String fromScreen, Int32 defID, Int32 createOrMerge, Boolean allowDuplicates, Int32 lastHeaderID, String orientation, String filename, Int32 headerRow, DevExpress.XtraEditors.LookUpEdit lueMaterial, DevExpress.XtraEditors.LookUpEdit lueOwner, DevExpress.XtraEditors.LookUpEdit lueOwnerLocation, DevExpress.XtraEditors.LookUpEdit lueSource, DevExpress.XtraEditors.LookUpEdit lueSourceLocation, Int32 beginDetailColumn, String excelFileExtension, BackgroundWorker backgroundPreview, String useThisOwner, String useThisOwnerLocation, String useThisSupplier, String useThisSupplierLocation)
        {
            using (BackgroundWorker backgroundWorker = new BackgroundWorker())
            {
                return ExcelImportSamples(
                                        fromScreen,
                                        defID,
                                        createOrMerge,
                                        allowDuplicates,
                                        lastHeaderID,
                                        orientation,
                                        filename,
                                        headerRow,                                        
                                        lueMaterial,
                                        lueOwner,
                                        lueOwnerLocation,
                                        lueSource,
                                        lueSourceLocation,                                       
                                        beginDetailColumn,
                                        excelFileExtension,
                                        backgroundWorker,
                                        backgroundPreview                                                                                    
                                        , useThisOwner                       //12/22/20
                                        , useThisOwnerLocation               //12/22/20
                                        , useThisSupplier                    //12/22/20
                                        , useThisSupplierLocation            //12/22/20
                                        );
            }
        }
        public static List<ImportHeaderInfo> ExcelImportSamples(
                                String fromScreen, Int32 defID,
                                Int32 importType,
                                Boolean allowDuplicates,
                                Int32 lastHeaderID,
                                String orientation,
                                String filename,
                                Int32 headerRow,
                                DevExpress.XtraEditors.LookUpEdit lueMaterial,
                                DevExpress.XtraEditors.LookUpEdit lueOwner,
                                DevExpress.XtraEditors.LookUpEdit lueOwnerLocation,
                                DevExpress.XtraEditors.LookUpEdit lueSource,
                                DevExpress.XtraEditors.LookUpEdit lueSourceLocation,                               
                                Int32 beginDetailColumn,
                                String excelFileExtension,
                                BackgroundWorker backgroundWorker,
                                BackgroundWorker backgroundPreview
                                , String useThisOwner                       //12/22/20
                                , String useThisOwnerLocation               //12/22/20
                                , String useThisSupplier                    //12/22/20
                                , String useThisSupplierLocation            //12/22/20
                                )
        {

            List<ImportHeaderInfo> importHeader = new List<ImportHeaderInfo>();            
            using (SpreadsheetControl spreadsheetControl = new SpreadsheetControl())
            {
                
                Int32 loopCount = 0;                                        //2/3/21    
                Int32 blankLines = 0;                                       //9/24/21 - see notes about blank lines under Portrait section below
                String ColumnToUseForGrouping = "";                         //11/18/21
                Boolean blnDetailAlreadyWritten = false;                    //11/18/21                
                String lastProcessedSampleCode = "";                        //11/18/21 - used in portrait exports, where there may be multiple lines for the same sample-code, but the lines are not in order.
                                                                            //           They could show up anywhere in the import file.


                

                //-------------------------------------------------------------------
                //SEE IF FILE IS OPEN -- SHOULD BE CLOSED; OTHERWISE, IMPORT FILE WON'T MOVE TO ARCHIVE				
                if (IsFileOpen(filename) == true)
                {
                    AIMGlobal.SplashScreenHide();
                    MessageBox.Show("Excel file must be closed before importing.  Please close the file and try again.");
                    return importHeader;
                }

                //-------------------------------------------------------------------
                //GET VALUES FROM DEFINITION
                UserDefinedDataSourceDefinitionInfo def = new UserDefinedDataSourceDefinitionInfo();
                def = AIMManager.GetDataSourceUserDefinition(defID);
                if (def != null)
                {
                    _dateFormatID = def.DateFormatID;
                    _importFileTypeID = def.ImportFileTypeID;
                    _sheetNumber = def.ExcelSheetNumber;
                    _detailBeginRow = def.DetailBeginRow;
                    
                    if (def.FileHasHeader == true && def.HeaderBeginRow > 1)    //9/24/21
                    {                                                           //9/24/21
                        blankLines = def.HeaderBeginRow - 1;                    //9/24/21
                    }                                                           //9/24/21

                    if (headerRow == 0)
                    {
                        _detailBeginRow--;
                    }
                    
                }

                //-------------------------------------------------------------------
                //LOAD WORKBOOK FROM FILE

                CellRange range;
                Int32 lastColumn = 0;
                Int32 lastRow = 0;
                spreadsheetControl.AllowDrop = false;               //added this to keep from getting 'DragDrop registration did not succeed' error
                                  
                if (excelFileExtension.ToUpper() == ".XLS")

                {
                    spreadsheetControl.LoadDocument(filename, DocumentFormat.Xls);
                }
                else
                {
                    spreadsheetControl.LoadDocument(filename, DocumentFormat.Xlsx);
                }
                Worksheet worksheet = spreadsheetControl.Document.Worksheets[_sheetNumber - 1];  //if the data is on the excel sheet 1, the code sees that as sheet 0	
                range = worksheet.GetDataRange();
                lastRow = range.RowCount;
                lastColumn = range.ColumnCount;
                               
                try
                {
                    //-----------------------------------------------------------------------------
                    //* * * * This function is used on both the ImportMapping and ImportDataUserDefined screens * * *																		

                    AIMGlobal.SplashScreenShow();                   						                                        
                    List<ImportDetailInfo> workDetail = new List<ImportDetailInfo>();
                    ErrorInfo error = new ErrorInfo();

                    Boolean isDetail = false;
                    String str = "";
                    String fileTypeName = "";
                    String mapColumnName = "";                    
                    String detailMessage = "";
                    String orignalMapName = "";
                    Int32 rowCount = 0;
                    Int32 colCount = 0;
                    Int32 landscapeBeginDetailColumn = 0;
                    Int32 headerPointerForDetail = 0;	                    //portrait only										
                    Int32 pointer = 0;                   

                    //Int32 arrayCount = 0;                                 //11/18/21 - commented, not used?!

                    _headerID = lastHeaderID + 1;
                                       

                    //-----------------------------------------------------------------------------
                    //INITIALIZE GLOBAL VARIABLES
                    InitializeGlobals();

                    //-----------------------------------------------------------------------------
                    //FOR AUTO-GENERATED SAMPLES, GET LAST SAMPLE# USED (FOR DATA-SOURCE) AND SET COUNTER                    
                    _sampleCounter = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeCounter(defID);
                    
                    //-------------------------------------------------------------------
                    //GET ROW and COLUMN COUNTS
                    rowCount = range.RowCount;
                    colCount = range.ColumnCount;                    
                    AIMGlobal.importTotalRecordCount = rowCount;            //this is used on the progress bar      

                    //-----------------------------------------------------------------------------
                    //GET FIELD TEMPLATE 
                    List<UserDefinedDataSourceTemplateInfo> fieldTemplate = new List<UserDefinedDataSourceTemplateInfo>();
                    fieldTemplate = AIMManager.GetUserDefinedDataSourceTemplate(defID);

                    //-----------------------------------------------------------------------------
                    //GET COLUMN NAME TO USE AS BREAK IN SAMPLES (PORTRAIT)
                    if (orientation == "Portrait")
                    {
                        foreach (var item in fieldTemplate)
                        {
                            if (item.UseAsSampleCode == true)
                            {
                                //11/18/21 
                                //_useAsSampleBreak = item.MapColumnName;
                                ColumnToUseForGrouping = item.MapColumnName;

                                break;
                            }
                        }
                    }

                    //-----------------------------------------------------------------------------
                    //VALIDATE MAPPING                       
                    DevExpress.XtraEditors.LookUpEdit[] lueName = { lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation };
                    string[] colName = { "Material", "Owner", "OwnerLocation", "Supplier", "SupplierLocation" };
                    Int32 validate = 0;
                    validate = ValidateExcel(defID, worksheet, fieldTemplate, lueName, colName, colCount, filename, true);   
                    if (validate != 0)
                    {
                        AIMGlobal.importValidateError = validate;
                        AIMGlobal.SplashScreenHide();
                        return importHeader;
                    }

                    //-----------------------------------------------------------------------------
                    //BUILD IMPORT-POINTER ARRAY TO CHECK ORDER OF COLUMNS	
                    if (orientation == "Landscape")
                    {
                        landscapeBeginDetailColumn = beginDetailColumn - 1;
                    }                    
                    List<FileTypeInfo> fileType = new List<FileTypeInfo>();
                    fileType = AIMManager.GetFileType(_importFileTypeID);
                    fileTypeName = fileType[0].Name;

                    //-----------------------------------------------------------------------------
                    //ASSIGN IMPORT POINTERS
                    #region Assign Import Pointers
                    Boolean headerFieldsExist = false;
                    String excelField = "";
                    Int32 xPointer = 0;
                    Int32 templateCount = 0;
                    Int32 headerTemplateCount = 0;

                    //12/20/21 commented
                    //Boolean ExcelMatchesMap = true;

                    Boolean HeaderColumnFound = false;

                    Boolean incrementCounter = false;                   //11/18/21
                    
                    //11/24/21 - initialize these
                    _sampleCodeExists = false;
                    _existingSampleID = 0;


                    if (orientation == "Landscape")
                    {
                        templateCount = beginDetailColumn - 1;
                        headerTemplateCount = fieldTemplate.Count;
                    }
                    else
                    {
                        templateCount = colCount;
                    }

                    #region Landscape pointer
                    //--------------------------- LANDSCAPE import-pointer					
                    if (orientation == "Landscape")
                    {
                        for (int i = 0; i < headerTemplateCount; i++)
                        {
                            HeaderColumnFound = false;
                            if (worksheet[headerRow, i].Value != null)                      //--> there is something in the title field for the column
                            {
                                for (int ii = 0; ii < fieldTemplate.Count; ii++)
                                {
                                    if (HeaderColumnFound == false)
                                    {                                        
                                        //12/20/21
                                        //excelField = worksheet[headerRow, i].Value.ToString().ToUpper();
                                        excelField = worksheet[headerRow, i].Value.ToString().ToUpper().Trim();

                                        if (excelField.Length > 30)                         //Source Column Name cannot be longer than 30 chars
                                        {
                                            excelField = excelField.Substring(0, 30);                                            
                                        }
                                        if (fieldTemplate[ii].SourceColumnName.ToUpper() == excelField)
                                        {
                                            fieldTemplate[ii].ImportPointer = i;
                                            xPointer = i;
                                            HeaderColumnFound = true;
                                            headerFieldsExist = true;
                                        }

                                        //12/20/21
                                        //else if (ii == fieldTemplate.Count - 1)
                                        //{
                                        //    ExcelMatchesMap = false;
                                        //}
                                        
                                    }
                                }
                            }
                            else
                            {
                                fieldTemplate[i - 1].ImportPointer = fieldTemplate[i - 1].SourceColumnNumber;
                            }
                        }
                    }
                    #endregion

                    #region Portrait pointer
                    //--------------------------- PORTRAIT import-pointer
                    else
                    {
                        //11/18/21
                        //for (int i = 1; i <= templateCount; i++)
                        for (int i = 0; i <= templateCount; i++)
                        {
                            if (worksheet[headerRow, i].Value != null)                      //--> there is something in the title field for the column
                            {
                                for (int ii = 0; ii < fieldTemplate.Count; ii++)
                                {
                                    excelField = worksheet[headerRow, i].Value.ToString().ToUpper().Trim();
                                    if (excelField.Length > 30)                             //Source Column Name cannot be longer than 30 chars
                                    {
                                        excelField = excelField.Substring(0, 30);
                                    }

                                    if (fieldTemplate[ii].SourceColumnName.ToUpper() == excelField)
                                    {
                                        fieldTemplate[ii].ImportPointer = i;
                                        xPointer = i;
                                        headerFieldsExist = true;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    //12/20/21 - commented
                    //if (ExcelMatchesMap == false)
                    //{
                    //    AIMGlobal.SplashScreenHide();
                    //    MessageBox.Show("Spreadsheet header columns do not match the mapping. Please fix spreadsheet and/or mapping and try again.");
                    //    return importHeader;
                    //}
                    //else 
                    if (headerFieldsExist == false)
                    {
                        AIMGlobal.SplashScreenHide();
                        MessageBox.Show("Spreadsheet header row is blank or is missing a column. Please fix spreadsheet and/or mapping and try again.");
                        return importHeader;
                    }

                    //--------------------------- Get LAST ImportPointer value
                    xPointer++;
                    if (colCount > templateCount)
                    {
                        for (int i = templateCount; i < colCount; i++)
                        {
                            fieldTemplate.Add(new UserDefinedDataSourceTemplateInfo
                            {
                                UserDefinedDataSourceDefinitionID = defID,
                                UserDefinedDataSourceTemplateID = 0,
                                SourceColumnName = worksheet[headerRow, i].Value.ToString().ToUpper(),
                                SourceColumnNumber = i,
                                ImportPointer = i,
                                MapColumnID = i,
                                MapColumnName = "ResultValue",
                                IsManualEntry = false,
                                IsDetail = true
                            }
                            );
                            xPointer++;
                        }
                    }
                    #endregion

                    //-----------------------------------------------------------------------------
                    //MAIN LOOP
                    #region Main Loop
                    switch (orientation)
                    {
                        //-------------------------------------------------------------------------
                        //L A N D S C A P E 
                        case "Landscape":
                            #region Landscape

                            //-------------------------------------------------------			
                            //LOOP THROUGH EXCEL ROWS - put in work entity 
                            #region loop thru import file rows				
                            rowCount = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetRowCountForMapPreview(fromScreen, rowCount);

                            //2/3/21 - added loopCount (needed for import files that have several heading lines)
                            //for (int i = headerRow + _detailBeginRow; i < rowCount; i++)
                            if (headerRow == 0)
                            {
                                loopCount = headerRow + _detailBeginRow;
                            }
                            else
                            {
                                loopCount = _detailBeginRow;
                            }                           
                            for (int i = loopCount; i < rowCount; i++)

                                {                                                                
                                CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);
                                                                
                                AIMGlobal.txtProgressMessage = "Reading import data ...";                                                       //8/5/19
                                CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundPreview);                           //8/5/19

                                //---------------------------------------------------
                                //CHECK TO SEE IF HEADER SHOULD BE WRITTEN                        
                                _blnWriteHeader = true;
                                if (_saveSampleCode == "")
                                {
                                    _blnWriteHeader = false;
                                }

                                //---------------------------------------------------
                                //IF AUTO-GENERATE SAMPLE-CODE, INCREMENT COUNTER
                                IncrementAutoSampleNumber();

                                //---------------------------------------------------
                                //LOOP THROUGH FIELD TEMPLATE COLUMNS												
                                for (int j = 0; j < fieldTemplate.Count; j++)
                                {
                                    try
                                    {
                                        isDetail = false;
                                        mapColumnName = "";
                                        str = "";
                                        pointer = fieldTemplate[j].ImportPointer;
                                        isDetail = fieldTemplate[j].IsDetail;
                                        if (fieldTemplate[j].MapColumnName != null)
                                        {
                                            mapColumnName = fieldTemplate[j].MapColumnName.ToString();

                                            //---------------------------------------------------------------------------------------------------------
                                            //get ORIGINAL mapColumnName
                                            orignalMapName = "";
                                            orignalMapName = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetOriginalColumnMapName(defID, mapColumnName);
                                            mapColumnName = orignalMapName;
                                            //---------------------------------------------------------------------------------------------------------

                                        }
                                        if (worksheet[i, pointer].Value != null)
                                        {
                                            str = worksheet[i, pointer].Value.ToString();
                                        }

                                        //-------------------------------------------
                                        //ADD HEADER (Landscape)
                                        #region add header
                                        if (_blnWriteHeader == true)
                                        {
                                            //12/22/20
                                            //SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation);
                                            SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);

                                            //Set sample-code
                                            //_sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode);
                                            _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, "", false);
                                                                                        
                                            //Make sure sample-code doesn't already exist on SAMPLE TABLE and on CURRENT entity
                                            //_existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode, def.DataSourceTypeID);
                                            _existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode);

                                            //Make sure sample-code doesn't already exist on CURRENT entity                                           

                                            //11/18/21
                                            //_sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID,_workHeader);
                                            _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader, "SampleCode");

                                            //Set ExistingHeaderID column
                                            if (_sampleCodeExists == true)
                                            {
                                                _existingHeaderID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSampleCodeAlreadyExists(_sampleCode, def.DataSourceTypeID);
                                            }                      
                                                                                                              
                                            AddHeader(_headerID, filename);
                                            IncrementCountersAndIDs(orientation);
                                            InitializeAIMRequiredFields();                                            
                                        }
                                        #endregion

                                        
                                        _saveSampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSaveSampleCode(isDetail, _dateForGeneratedSampleCode, _sampleCounter, _saveSampleCode);

                                                                                
                                        SetVariables(mapColumnName, str, isDetail, _dateFormatID);              //_sampleCode and _saveSampleCode get set in here)  

                                        //-------------------------------------------
                                        //ADD DETAIL
                                        #region add detail
                                        if (isDetail == true)
                                        {

                                            //Check for numeric result; if not numeric, skip this nutrient
                                            if (CFCAIM.AIMUtilities.Imports.SharedFunctions.IsNumeric(str) == true)
                                            {
                                                SetVariables(mapColumnName, str, isDetail, _dateFormatID);      //_sampleCode and _saveSampleCode get set in here)

                                                //for landscape, use header row for nutrient name/code

                                                if (j >= fieldTemplate.Count)
                                                {
                                                    _analyzerNutrientCode = "";
                                                }
                                                else
                                                {
                                                    _analyzerNutrientCode = worksheet[headerRow, pointer].Value.ToString();
                                                }
                                                workDetail.Add(new ImportDetailInfo
                                                {
                                                    ImportDetailID = _detailID,
                                                    ImportHeaderID = _headerID,
                                                    AnalyzerNutrientCode = _analyzerNutrientCode,
                                                    ResultValueString = _resultValueString,
                                                    UnitType = _unitType,
                                                    DetailMessage = detailMessage               //this field isn't used on import
                                                }
                                                );
                                                _detailID++;
                                            }
                                        }
                                        #endregion
                                    }

                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }

                                //arrayCount++;                                                 //11/18/21 - commented, not used?!
                                str = "";

                                //---------------------------------------------------
                                //SEE IF USER CANCELLED PROCESS
                                if (AIMGlobal.stopPreviewingImport == true)
                                {
                                    AIMGlobal.stopPreviewingImport = false;
                                    break;
                                }
                            }
                            #endregion

                            //-------------------------------------------------------
                            if (AIMGlobal.generateSampleCode == true)
                            {
                                _sampleCounter++;
                                _sampleCode = _dateForGeneratedSampleCode + _sampleCounter.ToString();
                            }
                            

                            //-------------------------------------------------------
                            //ADD LAST HEADER
                            #region add last header  
                            _sampleCodeExists = false;
                            _existingHeaderID = 0;

                            //---------------------------------------------------
                            //IF AUTO-GENERATE SAMPLE-CODE, INCREMENT COUNTER
                            IncrementAutoSampleNumber();

                            //12/22/20
                            //SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation);
                            SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);

                            //Set sample-code
                            //_sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode);
                            _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, "", false);
                                                        
                            //2/15/21
                            //Make sure sample-code doesn't already exists on Sample table
                            //_existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode, def.DataSourceTypeID);
                            _existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode);

                            //Make sure sample-code doesn't already exist of CURRENT entity                            

                            //11/18/21
                            //_sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader);
                            _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader, "SampleCode");


                            //Set ExistingHeaderID Field
                            if (_sampleCodeExists == true)
                            {                                
                                _existingHeaderID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSampleCodeAlreadyExists(_sampleCode, def.DataSourceTypeID);
                            }

                            AddHeader(_headerID, filename);
                            InitializeAIMRequiredFields();
                            #endregion

                            //-------------------------------------------------------
                            //ADD ALL DETAIL RECORDS FROM WORKDETAIL (FOR ALL HEADER RECORDS, NOT JUST THE LAST ONE)                    
                            #region add all detail records

                           
                            Int32 arrayID = 0;
                            var sampleHeader = from g in _workHeader
                                                select g.ImportHeaderID;
                            foreach (var item in sampleHeader)
                            {
                                Int32 hdrID = 0;
                                hdrID = item;
                                arrayID++;
                                var sampleDetail = from d in workDetail
                                                    where d.ImportHeaderID == hdrID
                                                    select d;
                                foreach (var detailItem in sampleDetail)
                                {
                                    _detailID = detailItem.ImportDetailID;
                                    _saveHeaderID = hdrID;
                                    _analyzerNutrientCode = detailItem.AnalyzerNutrientCode;
                                    _resultValueString = detailItem.ResultValueString;
                                    _unitType = detailItem.UnitType;
                                    _analyzerDataSourceTypeName = detailItem.AnalyzerDataSourceTypeName;

                                    //11/24/21
                                    //AddResults(arrayID - 1, orientation);
                                    AddResults(arrayID - 1, orientation, "");

                                }
                            }
                           
                            #endregion

                            _saveHeaderID = _headerID;
                            break;
                        #endregion

                        //-------------------------------------------------------------------------
                        //P O R T R A I T
                        case "Portrait":
                            #region Portrait
                            
                            //---------------------------------------------------------------------
                            //LOOP THROUGH IMPORT FILE ROWS - put in work entity 		
                            #region loop thru import file rows		
                            rowCount = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetRowCountForMapPreview(fromScreen, rowCount);

                            //              EXCEL WORKSHEET
                            //---------------------------------------------
                            //      A       B           C           D
                            // 1
                            // 2    Mill#   Sample#     Material    Nutrient
                            // 3    10      12-0907     Corn        Protein
                            // 4    10      12-0907     Corn        Fat
                            //--------------------------------------------- 

                            // If first line of Excel is blank, DevExpress rowcount completely IGNORES that row. 
                            // So instead of 4 rows, DevExpress sees 3 rows. Added 9/24/21 code below to work around this.

                            
                            //9/24/21
                            Int32 beginRowToProcess = headerRow;
                            Int32 beginDetailRowToProcess = _detailBeginRow;
                            if (blankLines > 0)
                            {
                                beginRowToProcess = beginRowToProcess + blankLines;
                                rowCount = rowCount + blankLines;
                                beginDetailRowToProcess = beginRowToProcess - 1;
                            }
                            else
                            {
                                beginRowToProcess = headerRow + _detailBeginRow;
                            }

                            //11/18/21                            
                            if (AIMGlobal.generateSampleCode == true)
                            {
                                rowCount--;
                            }

                            for (Int32 n = beginRowToProcess; n <= rowCount; n++)                            
                            {
                                CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);                                                                
                                AIMGlobal.txtProgressMessage = "Reading import data ...";                                                       //8/5/19
                                CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundPreview);                           //8/5/19                                                               

                                //-----------------------------------------------------------------
                                //CHECK TO SEE IF HEADER SHOULD BE WRITTEN                        
                                _blnWriteHeader = false;

                                //11/26/21
                                if (AIMGlobal.noGrouping == true && _firstRec == false)
                                {
                                    _blnWriteHeader = true;
                                }
                                
                                //11/18/21 - changed variable name
                                //_blnWriteHeader = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleBreakColumn(_useAsSampleBreak, _workHeader, _saveSampleCode);
                                _blnWriteHeader = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleBreakColumn(ColumnToUseForGrouping, _workHeader, _saveSampleCode);
                                                                
                                //-----------------------------------------------------------------
                                //Check to see if detail should be written
                                _blnWriteDetail = false;
                                if (_saveSampleCode != "")
                                {
                                    _blnWriteDetail = true;
                                }

                                //-----------------------------------------------------------------
                                //LOOP THROUGH FIELD TEMPLATE COLUMNS
                                #region loop thru template columns
                                for (int j = 0; j < fieldTemplate.Count; j++)
                                {
                                    try
                                    {
                                        str = "";
                                        isDetail = false;
                                        mapColumnName = "";
                                        pointer = fieldTemplate[j].ImportPointer;
                                        isDetail = fieldTemplate[j].IsDetail;                                                                              
                                        if (fieldTemplate[j].MapColumnName != null)
                                        {
                                            mapColumnName = fieldTemplate[j].MapColumnName.ToString();

                                            //------------------------------------------------------
                                            //get ORIGINAL mapColumnName
                                            orignalMapName = "";
                                            orignalMapName = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetOriginalColumnMapName(defID, mapColumnName);
                                            mapColumnName = orignalMapName;
                                            //------------------------------------------------------
                                        }

                                        //get value for that field
                                        if (worksheet[n, pointer].Value != null)
                                        {
                                            str = worksheet[n, pointer].Value.ToString();
                                        }
                                                                                
                                        _useThisFieldAsSampleCode = fieldTemplate[j].UseAsSampleCode;                                        

                                        //3/9/21 ---------------------------------------------------
                                        //Added for portrait imports where user wants system to generate sample-codes, but also wants to group the samples by another field (and not the sys-gen sample code).
                                        //If the user selects system-generate sample-codes and also groups by another field, the AIMGlobal.sampleCodeMapped field is set to FALSE.
                                        if (_useThisFieldAsSampleCode == true)
                                        {
                                            AIMGlobal.sampleCodeMapped = false;

                                            //11/18/21
                                            ColumnToUseForGrouping = mapColumnName;
                                        }                                                                               

                                        //----------------------------------------------------------
                                        //ADD HEADER (Portrait)
                                        #region add header
                                        if (_blnWriteHeader == true)
                                        {
                                            //12/22/20
                                            //SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation);
                                            SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);

                                            //4/12/21 -----------------------------------------------
                                            //Added for portrait imports where user wants system to generate sample-codes, but also wants to group the samples by another field (and not the sys-gen sample code).                                    
                                            if (AIMGlobal.useAnotherFieldAsSampleCode == true && AIMGlobal.generateSampleCode == true)
                                            {
                                                _sampleCode = "";
                                            }
                                            
                                            //Set sample-code
                                            //_sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode);
                                            _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, _saveSampleCode, _useThisFieldAsSampleCode);
                                            
                                            //Make sure sample-code doesn't already exist on SAMPLE TABLE                                                                                        
                                            //Have gone back and forth, thinking DataSourceTypeID needs to be added to this function (ran into issues at CSA)                                             
                                            _existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode);

                                            //Make sure sample-code doesn't already exist on CURRENT entity

                                            //11/18/21
                                            //_sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader);
                                            _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader, "SampleCode");

                                            //Set ExistingHeaderID column                                                 
                                            if (_sampleCodeExists == true)              
                                            {                                                
                                                _existingHeaderID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSampleCodeAlreadyExists(_sampleCode, def.DataSourceTypeID);
                                                CFCAIM.AIMUtilities.Imports.SharedFunctions.SetExistingHeaderFieldsBeforeUpdate(_sampleCode, def.DataSourceTypeID);
                                            }

                                            AddHeader(_headerID, filename);
                                            if (_sampleCodeExists == true)              //7/30/19 - added for UpdateHeaderOnly option
                                            {                                                
                                                CFCAIM.AIMUtilities.Imports.SharedFunctions.InitializeHeaderVariablesAfterUpdate();
                                            }                                                
                                            
                                            IncrementCountersAndIDs(orientation);
                                            InitializeAIMRequiredFields();
                                            if (_firstRec == false)
                                            {
                                                headerPointerForDetail++;
                                            }
                                            _firstRec = false;

                                            blnDetailAlreadyWritten = false;            //11/18/21

                                            //11/18/21 - for samples that have system-generated sample codes                                                  
                                            ////if (ColumnToUseForGrouping == "")
                                            if (AIMGlobal.generateSampleCode == true)
                                            {
                                                incrementCounter = true;
                                            }

                                        }

                                        //11/18/21 -------------------------------------------------
                                        // Check _workheader to see if header already exists.
                                        // If it does, get the header-pointer for the detail.
                                        // Add detail to sub-entity from variables.                                        
                                        else
                                        {
                                            if (isDetail == true)
                                            {                                                
                                                if (_saveSampleCode != lastProcessedSampleCode && lastProcessedSampleCode != "")
                                                {
                                                    if (AIMGlobal.sampleCodeMapped == true)
                                                    {
                                                        ColumnToUseForGrouping = "SampleCode";
                                                    }

                                                    //See if sample-code exists (sending 0 as existing-sampleID because it is not needed in this case)                                                                                                                                                              
                                                    _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_saveSampleCode, 0, _workHeader, ColumnToUseForGrouping);

                                                    if (_sampleCodeExists == true && _workHeader.Count > 0)
                                                    {
                                                        //Get pointer                                                            
                                                        int ptr = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetPointerForExistingSamples(_saveSampleCode, _workHeader, ColumnToUseForGrouping);

                                                        //Set the detail variables and add the results
                                                        SetVariables(mapColumnName, str, isDetail, _dateFormatID);      //_sampleCode and _saveSampleCode get set in here)
                                                        AddResults(ptr, orientation, ColumnToUseForGrouping);

                                                        blnDetailAlreadyWritten = true;
                                                    }
                                                }
                                                
                                            }
                                        }
                                        //----------------------------------------------------------


                                        #endregion

                                        //----------------------------------------------------------
                                        //ADD DETAIL
                                        #region add detail

                                        //if (_blnWriteDetail == true)                                      //11/18/21
                                        if (_blnWriteDetail == true && blnDetailAlreadyWritten == false)    //11/18/21

                                        {
                                            //11/26/21
                                            if (AIMGlobal.sampleCodeMapped == true || AIMGlobal.noGrouping == true)
                                            {
                                                ColumnToUseForGrouping = "SampleCode";
                                            }


                                            //bypass writing detail for HeaderOnly imports
                                            if (importType != (Int32)EnumImportType.UpdateHeaderOnly)       
                                            {
                                                //11/24/21
                                                //AddResults(headerPointerForDetail, orientation);
                                                AddResults(headerPointerForDetail, orientation, ColumnToUseForGrouping);

                                                _detailID++;
                                            }
                                            _blnWriteDetail = false;                                            
                                        }
                                        #endregion
                                        //----------------------------------------------------------

                                        //11/18/21
                                        if (_firstRec == true)
                                        {
                                            incrementCounter = true;
                                        }

                                        //----------------------------------------------------------                                        
                                        lastProcessedSampleCode = _saveSampleCode;
                                        
                                        if (AIMGlobal.sampleCodeMapped == true)
                                        {                                           
                                            _saveSampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSaveSampleCode(isDetail, _dateForGeneratedSampleCode, _sampleCounter, _saveSampleCode);                                            
                                        }
                                                                                
                                        else if (AIMGlobal.noGrouping == true)
                                        {
                                            if (incrementCounter == true)
                                            {
                                                _sampleCounter++;
                                            }
                                            _saveSampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSaveSampleCode(isDetail, _dateForGeneratedSampleCode, _sampleCounter, _saveSampleCode);
                                            incrementCounter = false;
                                        }


                                        //11/18/21
                                        //else if (AIMGlobal.sampleCodeMapped == false && ColumnToUseForGrouping == "" && incrementCounter == true)
                                        else if (AIMGlobal.sampleCodeMapped == false && AIMGlobal.generateSampleCode == true && incrementCounter == true)

                                        {
                                            if (_firstRec == false)
                                            {
                                                _sampleCounter++;
                                            }
                                            incrementCounter = false;
                                        }

                                        //----------------------------------------------------------
                                        SetVariables(mapColumnName, str, isDetail, _dateFormatID);      //_sampleCode and _saveSampleCode get set in here)       

                                    }

                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }

                                    str = "";
                                }
                                
                                
                                #endregion
                                //------------------------------------------------------------------
                                //SEE IF USER CANCELLED PROCESS							
                                if (AIMGlobal.stopPreviewingImport == true)
                                {
                                    AIMGlobal.stopPreviewingImport = false;
                                    break;
                                }
                            }
                            #endregion

                            //----------------------------------------------------------------------
                            //ADD LAST HEADER   
                            #region add last header                                                                         
                            _blnWriteHeader = true;
                            

                            //11/18/21
                            //if (_saveSampleCode == "")
                            //{
                            //    _blnWriteHeader = false;
                            //}
                            if (AIMGlobal.useAnotherFieldAsSampleCode == true)
                            {                              
                                _blnWriteHeader = false;
                            }

                            //---------------------------------------------------
                            //IF AUTO-GENERATE SAMPLE-CODE, INCREMENT COUNTER
                            IncrementAutoSampleNumber();

                            _blnWriteDetail = false;

                            //11/18/21
                            //if (_saveSampleCode != "")
                            if (_saveSampleCode != "" && blnDetailAlreadyWritten == false)
                            {
                                _blnWriteDetail = true;
                            }
                            if (_blnWriteHeader == true)
                            {
                                //12/22/20
                                //SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation);
                                SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);

                                //Set sample-code
                                //_sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode);
                                _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, _saveSampleCode, _useThisFieldAsSampleCode);
                                                               
                                //Make sure sample-code doesn't already exist on SAMPLE TABLE
                                //Have gone back and forth, thinking DataSourceTypeID needs to be added to this function (ran into issues at CSA)
                                _existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode);

                                //Make sure sample-code doesn't already exist on CURRENT entity                                
                                                                
                                //11/18/21
                                //_sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader);
                                _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader, "SampleCode");

                                //Set ExistingHeaderID field                              
                                if (_sampleCodeExists == true)
                                {                                   
                                    _existingHeaderID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSampleCodeAlreadyExists(_sampleCode, def.DataSourceTypeID);
                                }

                                //11/19/21 - empty header line was getting written for imports that update only the header fields and no detail and imports with sample-code mapped
                                //AddHeader(_headerID, filename);
                                if (_sampleCodeExists == true && _sampleCode == "")
                                {}
                                else
                                {
                                    AddHeader(_headerID, filename);
                                }

                                

                                IncrementCountersAndIDs(orientation);
                                InitializeAIMRequiredFields();
                                if (headerPointerForDetail != 0)
                                {
                                    headerPointerForDetail++;
                                }
                            }
                            #endregion

                            //----------------------------------------------------------------------
                            //ADD LAST DETAIL RECORD TO HEADER-DETAIL ENTITY                     
                            #region add last detail
                            if (importType != (Int32)EnumImportType.UpdateHeaderOnly)       //bypass writing detail for HeaderOnly imports  
                            { 
                                if (_blnWriteDetail == true)                                                           
                                {
                                    //11/24/21
                                    //AddResults(headerPointerForDetail, orientation);
                                    AddResults(headerPointerForDetail, orientation,ColumnToUseForGrouping);

                                }
                            }
                            #endregion

                            break;
                        #endregion

                        default:
                            break;
                    }
                    #endregion

                    //-----------------------------------------------------------------------------
                    //MOVE WORK TO IMPORT HEADER ENTITY
                    importHeader = CFCAIM.AIMUtilities.Imports.SharedFunctions.CopyWorkToImportHeaderEntity(_workHeader, fromScreen, defID, importType, allowDuplicates, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, false, backgroundWorker, backgroundPreview);                    

                    AIMGlobal.SplashScreenHide();
                    AIMGlobal.lastImportHeaderID = _saveHeaderID;

                    //2/25/21 - get last samplecode from last import record (need this if multiple files are imported)                                
                    if (importHeader.Count > 0)
                    {
                        String lastsampleCode = "";
                        lastsampleCode = importHeader[importHeader.Count - 1].SampleCode;
                        if (lastsampleCode.Length > 8)
                        {
                            AIMGlobal.lastImportSampleCode = lastsampleCode.Substring(8, lastsampleCode.Length - 8);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                spreadsheetControl.Dispose();
            }
            return importHeader;
        }

        public static Int32 AddHeader(Int32 headerID, String fileName)
        {
            _workHeader.Add(new ImportHeaderInfo
            {
                ImportHeaderID = headerID,
                SampleCode = _sampleCode,
                AlternateCode = _alternateCode,
                SampleDateString = _sampleDate,
                InvoiceNumber = _invoiceNumber,
                PONumber = _poNumber,
                TicketNumber = _ticketNumber,
                VehicleNumber = _vehicleNumber,
                BillOfLading = _billOfLading,
                ContractNumber = _contractNumber,
                LoadNumber = _loadNumber,
                LotNumber = _lotNumber,

                MaterialName = _material,
                OwnerName = _owner,
                OwnerLocationName = _ownerLocation,
                SourceName = _supplier,
                SourceLocationName = _supplierLocation,

                MaterialID = _aimMaterialID,
                OwnerID = _aimOwnerID,
                OwnerLocationID = _aimOwnerLocationID,
                SourceID = _aimSupplierID,
                SourceLocationID = _aimSupplierLocationID,

                Message = _headerMessage,
                MessageStatus = _headerMessageStatus,
                ImportSourceFileName = fileName,
                SampleCodeExists = _sampleCodeExists,

                ExistingImportHeaderID = _existingHeaderID,
                Notes = _notes
            }
            );

            return 0;

        }

        //11/24/21
        //public static Int32 AddResults(Int32 pointer, String orientation)
        public static Int32 AddResults(Int32 pointer, String orientation, String ColumnToUseForGrouping)
        {            
            Int32 hdrID = 0;
           
            //11/24/21
            //hdrID = _workHeader[hdrPointer].ImportHeaderID;
            hdrID = _workHeader[pointer].ImportHeaderID;

            if (orientation == "Portrait")
            {
                //11/24/21
                //3/22/18 - if sample already exists on current entity, reset the ImportHeaderID and array pointer
                //11/18/21
                //if (CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnCurrentEntity(_sampleCode, _workHeader) == true)
                ////////if (CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnCurrentEntity(_sampleCode, _workHeader, "SampleCode") == true)
                ////////{                     
                ////////    for (int i = 0; i < _workHeader.Count; i++)
                ////////    {
                ////////        if (sampleFound == false)
                ////////        {
                ////////            if (_sampleCode == _workHeader[i].SampleCode)
                ////////            {
                ////////                hdrPointer = i;
                ////////                sampleFound = true;
                ////////            }
                ////////        }
                ////////    }                    
                ////////}
                Int32 ptr = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetPointerForResults(_saveSampleCode, _workHeader, ColumnToUseForGrouping);
                hdrID = ptr;
                pointer = ptr;
            }

            //////////11/24/21 - moved above check for portrait
            //////////hdrID = _workHeader[hdrPointer].ImportHeaderID;
            ////////hdrID = _workHeader[pointer].ImportHeaderID;

            //----------------------------------------------------------------------

            //11/24/21
            //if (_workHeader[pointer].DetailImport == null)
            //{
            //    _workHeader[pointer].DetailImport = new List<ImportDetailInfo>();
            //}
            if (_workHeader[pointer].DetailImport == null)
            {
                _workHeader[pointer].DetailImport = new List<ImportDetailInfo>();
            }
                        
            _workHeader[pointer].DetailImport.Add(new ImportDetailInfo
            {
                ImportDetailID = _detailID,

                //11/18/21
                //ImportHeaderID = _saveHeaderID,
                ImportHeaderID = hdrID,

                AnalyzerNutrientCode = _analyzerNutrientCode,
                ResultValueString = _resultValueString,
                UnitType = _unitType
            }
            );
            return 0;
            

        }
        public static Int32 InitializeGlobals()
        {
            _material = "";
            _owner = "";
            _ownerLocation = "";
            _supplier = "";
            _supplierLocation = "";
            _sampleCode = "";
            _alternateCode = "";
            _sampleDate = "";
            _dtSampleDate = DateTime.Today;
            _invoiceNumber = "";
            _poNumber = "";
            _ticketNumber = "";
            _vehicleNumber = "";
            _billOfLading = "";
            _contractNumber = "";
            _loadNumber = "";
            _lotNumber = "";

            _saveSampleCode = "";
            _headerMessageStatus = 0;
            _headerMessage = "";

            _detailID = 1;
            _analyzerNutrientCode = "";
            _unitType = "";
            _resultValueString = "";
            _analyzerDataSourceTypeName = "";

            _aimMaterialID = 0;
            _aimOwnerID = 0;
            _aimOwnerLocationID = 0;
            _aimSupplierID = 0;
            _aimSupplierLocationID = 0;
            _sampleCounter = 0;	  //fields used for generated sample-code	
            _dateForGeneratedSampleCode = DateTime.Now.ToString("yyyyMMdd");
            _saveHeaderID = 0;
            _blnWriteHeader = false;
            _blnWriteDetail = false;

            _workHeader = new List<ImportHeaderInfo>();

            _firstRec = true; //used for auto-generated sample codes
            _dateFormatID = 0;
            _sampleCodeExists = false;
            _notes = "";

            return 0;
        }        
        public static Int32 IncrementAutoSampleNumber()
        {
            if (AIMGlobal.generateSampleCode == true)
            {
                if (_firstRec == false)
                {
                    _sampleCounter++;
                }
            }
            return 0;
        }

        //12/22/20
        //public static String SetRequiredVariables(String fromScreen, DevExpress.XtraEditors.LookUpEdit lueMaterial, DevExpress.XtraEditors.LookUpEdit lueOwner, DevExpress.XtraEditors.LookUpEdit lueOwnerLocation, DevExpress.XtraEditors.LookUpEdit lueSource, DevExpress.XtraEditors.LookUpEdit lueSourceLocation)
        public static String SetRequiredVariables(String fromScreen, DevExpress.XtraEditors.LookUpEdit lueMaterial, DevExpress.XtraEditors.LookUpEdit lueOwner, DevExpress.XtraEditors.LookUpEdit lueOwnerLocation, DevExpress.XtraEditors.LookUpEdit lueSource, DevExpress.XtraEditors.LookUpEdit lueSourceLocation, String useThisOwner, String useThisOwnerLocation, String useThisSupplier, String useThisSupplierLocation)
        {
            //Required fields
            if (fromScreen == "Import")
            {
                if (lueMaterial.Enabled == true)
                {
                    _aimMaterialID = (Int32)lueMaterial.EditValue;
                    _material = lueMaterial.Text.ToString();
                }
                if (lueOwner.Enabled == true)
                {
                    _aimOwnerID = (Int32)lueOwner.EditValue;
                    _owner = lueOwner.Text.ToString();
                }
                if (lueOwnerLocation.Enabled == true)
                {
                    _aimOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                    _ownerLocation = lueOwnerLocation.Text.ToString();
                }
                if (lueSource.Enabled == true)
                {
                    _aimSupplierID = (Int32)lueSource.EditValue;
                    _supplier = lueSource.Text.ToString();
                }
                if (lueSourceLocation.Enabled == true)
                {
                    _aimSupplierLocationID = (Int32)lueSourceLocation.EditValue;
                    _supplierLocation = lueSourceLocation.Text.ToString();
                }
            }

            //12/22/20
            if (fromScreen == "Map")
            {
                if (useThisOwner != "")
                {
                    _owner = useThisOwner;
                }
                if (useThisOwnerLocation != "")
                {
                    _ownerLocation = useThisOwnerLocation;
                }
                if (useThisSupplier != "")
                {
                    _supplier = useThisSupplier;
                }
                if (useThisSupplierLocation != "")
                {
                    _supplierLocation = useThisSupplierLocation;
                }
                
            }


            CheckSupplierAndSupplierLocFields();

            return "";
        }
        public static Int32 CheckSupplierAndSupplierLocFields()
        {
            if (_supplier == "")
            {
                _supplier = _supplierLocation;
            }
            if (_supplierLocation == "")
            {
                _supplierLocation = _supplier;
            }

            if (_supplier.Length > 0 && _aimSupplierID == 0)
            {
                _aimSupplierID = _aimSupplierLocationID;
            }
            if (_supplierLocation.Length > 0 && _aimSupplierLocationID == 0)
            {
                _aimSupplierLocationID = _aimSupplierID;
            }


            return 0;
        }
        public static Int32 IncrementCountersAndIDs(String orientation)
        {
            _blnWriteHeader = false;
            _saveHeaderID = _headerID;
            _headerID++;
            _detailID = 1;

            //Can set _firstRec here for landscape but, for portrait, it needs to be set at a different spot
            if (orientation == "Landscape")
            {
                _firstRec = false;
            }

            return 0;
        }
        public static Int32 InitializeAIMRequiredFields()
        {
            _material = "";
            _owner = "";
            _ownerLocation = "";
            _supplier = "";
            _supplierLocation = "";

            _aimMaterialID = 0;
            _aimOwnerID = 0;
            _aimOwnerLocationID = 0;
            _aimSupplierID = 0;
            _aimSupplierLocationID = 0;

            return 0;
        }
        
        
        public static String SetVariables(String mapColumnName, String str, Boolean isDetail, Int32 dateFormatID)       

        {
            if (isDetail == true)
            {
                switch (mapColumnName)
                {
                    case "AnalyzerNutrientCode":
                        _analyzerNutrientCode = str;
                        break;
                    case "ResultValue":
                        _resultValueString = str;
                        break;
                    case "UnitType":
                        _unitType = str;
                        break;
                    default:
                        break;
                }

            }
            else
            {
                switch (mapColumnName)
                {
                    case "SampleCode":                                                
                        _sampleCode = str.Trim();
                        _saveSampleCode = _sampleCode;                        
                        break;

                    case "SampleDate":
                        DateTime dt;

                        if (CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckOLEDate(str) == true)
                        {
                            dt = Convert.ToDateTime(CFCAIM.AIMUtilities.Imports.SharedFunctions.ConvertOLEStringToDate(str));
                            _sampleDate = dt.ToString("yyyy-MM-dd");
                        }
                        else if (str != null)
                        {                            
                            dt = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckDateFormat(str, dateFormatID);
                            _sampleDate = dt.ToString("yyyy-MM-dd");
                        }
                        break;

                    case "AlternateCode":
                        _alternateCode = str.ToString();                     
                        break;
                    case "InvoiceNumber":
                        _invoiceNumber = str;
                        break;
                    case "PONumber":
                        _poNumber = str;
                        break;
                    case "TicketNumber":
                        _ticketNumber = str;
                        break;
                    case "VehicleID":
                        _vehicleNumber = str;
                        break;
                    case "BillOfLading":
                        _billOfLading = str;
                        break;
                    case "ContractNumber":
                        _contractNumber = str;
                        break;
                    case "LoadNumber":
                        _loadNumber = str;
                        break;
                    case "LotNumber":
                        _lotNumber = str;
                        break;
                    case "Material":
                        _material = str;
                        break;
                    case "Notes":
                        _notes = str;
                        break;
                    case "Owner":
                        _owner = str;
                        break;
                    case "OwnerLocation":
                        _ownerLocation = str;
                        break;
                    case "Supplier":
                        _supplier = str;
                        break;

                    case "SupplierLocation":
                    case "SupplierLocationCombined":            //12/22/20 - added supplierlocationcombined
                        _supplierLocation = str;
                        break;                        
                    default:
                        break;
                }
            }

            //11/19/21            
            if (_useThisFieldAsSampleCode == true)
            {
                _sampleCode = str.Trim();
                _saveSampleCode = _sampleCode;
            }

            return "";
        }
        public static Int32 ValidateExcel(Int32 defID, Worksheet worksheet, List<UserDefinedDataSourceTemplateInfo> userTemplate, DevExpress.XtraEditors.LookUpEdit[] lueName, string[] colName, Int32 excelColCount, String filename, Boolean showAutoGenSampleMsg)
        {
            String orientation = "";
            Int32 validate;            

            //3/4/21 - removed unnecessary assignment
            //UserDefinedDataSourceDefinitionInfo def = new UserDefinedDataSourceDefinitionInfo();
            UserDefinedDataSourceDefinitionInfo def;

            def = AIMManager.GetDataSourceUserDefinition(defID);
            if (def != null)
            {
                orientation = def.OrientationDisplay;
            }            
            validate = CFCAIM.AIMUtilities.Imports.SharedFunctions.ValidateMap(defID, userTemplate, lueName, colName, filename, orientation);
            if (validate != 0)
            {
                return -1;
            }

            //--------------------------------------------------------------                                
            //11/18/21 - commented - beginDetail is not doing anything. And beginHeader is not doing anything either, plus it is checked in SetHeaderBeginRow function.
            //Int32 beginDetail;
            //Int32 beginHeader;
            //beginDetail = def.NutrientDetailBeginColumn;                
            //if (def.HeaderBeginRow > 0)                                 
            //{
            //    beginHeader = def.HeaderBeginRow - 1;                
            //}

            return 0;

        }
        public static Boolean IsFileOpen(string path)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                return false;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);        
                return true;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }                
    }
}
