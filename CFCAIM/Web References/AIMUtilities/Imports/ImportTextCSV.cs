using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace CFCAIM.AIMUtilities.Imports
{
    class ImportTextCSVFile
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

        //8/15/22
        public static String _sqlResultIDInfo = "";

        //aim required fields
        public static Int32 _aimMaterialID = 0;
        public static Int32 _aimOwnerID = 0;
        public static Int32 _aimOwnerLocationID = 0;
        public static Int32 _aimSupplierID = 0;
        public static Int32 _aimSupplierLocationID = 0;

        //3/23/18
        public static String _lastSampleCode = "";      //this keeps track of the sample-code currently being processed in the loop (USED FOR PORTRAIT ONLY).

        public static String _saveSampleCode = "";      //this is the sample-code that gets saved until all the detail columns for the row are read -- then the header (with this samplecode) is written.
        public static String _headerMessage = "";                             
        public static String _dateForGeneratedSampleCode = DateTime.Now.ToString("yyyyMMdd");

        public static DateTime _dtSampleDate = DateTime.Today;
        public static Char _delimiterField = ',';

        public static Boolean _firstRec = true;         //used for auto-generated sample code
        public static Boolean _blnWriteHeader = false;
        public static Boolean _blnWriteDetail = false;              
        public static Boolean _sampleCodeExists = false;                
        public static Int32 _existingSampleID = 0;

        public static Int32 _saveHeaderID = 0;        
        public static Int32 _existingHeaderID = 0;
        public static Int32 _headerID;
        public static Int32 _headerMessageStatus = 0;
        public static Int32 _sampleCounter = 0;	        //used for auto-generated sample code	
        public static Int32 _detailID = 1;
        public static Int32 _dateFormatID = 0;
        public static Int32 _headerPointerForDetail = 0;
        public static Int32 _importFileTypeID = 0;
        public static Int32 _decimalSeparatorID = 2;
        public static Int32 _detailBeginRow = 1;

        //11/19/21
        public static Boolean _useThisFieldAsSampleCode = false;

        public static List<ImportHeaderInfo> _workHeader = new List<ImportHeaderInfo>();               

        public bool WorkerReportsProgress { get; set; }

        public static List<ImportHeaderInfo> TextCSVImportSamples(String fromScreen, Int32 defID, Int32 createOrMerge, Boolean allowDuplicates, Int32 lastHeaderID, String orientation, String filename, Int32 headerRow, DevExpress.XtraEditors.LookUpEdit lueMaterial, DevExpress.XtraEditors.LookUpEdit lueOwner, DevExpress.XtraEditors.LookUpEdit lueOwnerLocation, DevExpress.XtraEditors.LookUpEdit lueSource, DevExpress.XtraEditors.LookUpEdit lueSourceLocation, Int32 beginDetailColumn, BackgroundWorker backgroundPreview, String useThisOwner, String useThisOwnerLocation, String useThisSupplier, String useThisSupplierLocation)
        {
            using (BackgroundWorker backgroundWorker = new BackgroundWorker())
            {
                return TextCSVImportSamples(
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
                                        backgroundWorker, 
                                        backgroundPreview                    //8/5/19
                                        , useThisOwner                       //12/22/20
                                        , useThisOwnerLocation               //12/22/20
                                        , useThisSupplier                    //12/22/20
                                        , useThisSupplierLocation            //12/22/20
                                        );           
            }
        }
        public static List<ImportHeaderInfo> TextCSVImportSamples(
                                String fromScreen, 
                                Int32 defID,
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
                                BackgroundWorker backgroundWorker, 
                                BackgroundWorker backgroundPreview          //8/5/19
                                , String useThisOwner                       //12/22/20
                                , String useThisOwnerLocation               //12/22/20
                                , String useThisSupplier                    //12/22/20
                                , String useThisSupplierLocation            //12/22/20
                                )
        {

            //* * * * This function is used on both the ImportMapping and ImportDataUserDefined screens * * *			
            AIMGlobal.SplashScreenShow();

            //-------------------------------------------------------------------

            List<ImportHeaderInfo> importHeader = new List<ImportHeaderInfo>();
            List<ImportDetailInfo> workDetail = new List<ImportDetailInfo>();
            ErrorInfo error = new ErrorInfo();

            Boolean isDetail = false;
            String str = "";
            String fileTypeName = "";            
            String mapColumnName = "";
            String msg = "";
            String detailMessage = "";            
            Int32 colCount = 0;
            Int32 rowCount = 0;
            Int32 landscapeBeginDetailColumn = 0;            
            Int32 arrayCount = 1;                   //landscape
            Int32 headerPointerForDetail = 0;       //portrait only		         				
            Int32 pointer = 0;
            String orignalMapName = "";
            String useAsSampleBreak = "";

            //Boolean useThisFieldAsSampleCode = false;

            String ColumnToUseForGrouping = "";                         //11/18/21
            Boolean blnDetailAlreadyWritten = false;                    //11/18/21                
            String lastProcessedSampleCode = "";                        //11/18/21 - used in portrait exports, where there may be multiple lines for the same sample-code, but the lines are not in order.
                                                                        //           They could show up anywhere in the import file.

            Boolean incrementCounter = false;                           //11/18/21

            //11/24/21 - initialize these
            _sampleCodeExists = false;
            _existingSampleID = 0;

            //-----------------------
            _headerID = lastHeaderID + 1;

            //-------------------------------------------------------------------
            //INITIALIZE GLOBAL VARIABLES
            InitializeGlobals();

            //-------------------------------------------------------------------
            //GET VALUES FROM DEFINITION
            UserDefinedDataSourceDefinitionInfo def = new UserDefinedDataSourceDefinitionInfo();
            def = AIMManager.GetDataSourceUserDefinition(defID);
            if (def != null)
            {
                _dateFormatID = def.DateFormatID;
                _delimiterField = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetDelimiterField(defID);
                _importFileTypeID = def.ImportFileTypeID;
                _decimalSeparatorID = def.DecimalSeparatorID;   //4/2/19
            }                       

            //-------------------------------------------------------------------
            //FOR AUTO-GENERATED SAMPLES, GET LAST SAMPLE# USED (FOR DATA-SOURCE) AND SET COUNTER
            _sampleCounter = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeCounter(defID);

            //-------------------------------------------------------------------
            //GET COLUMN COUNT	                                  
            TextFieldParser csvParserColCount = new TextFieldParser(filename);
            csvParserColCount.SetDelimiters(new string[] { _delimiterField.ToString() });
            csvParserColCount.HasFieldsEnclosedInQuotes = true;
            if (!csvParserColCount.EndOfData)
            {
                string[] flds = csvParserColCount.ReadFields();
                colCount = flds.Count();
            }
            csvParserColCount.Close();


            //-------------------------------------------------------------------
            //GET ROW COUNT AND POPULATE FIELD ARRAY	                                                                                    
            List<String[]> dataList = new List<string[]>();
            TextFieldParser csvParser = new TextFieldParser(filename);            
            csvParser.SetDelimiters(new string[] { _delimiterField.ToString() });
            csvParser.HasFieldsEnclosedInQuotes = true;
            string[] fieldHeaders = new string[colCount];
            while (!csvParser.EndOfData)
            {
                dataList.Add(csvParser.ReadFields());
                rowCount++;
            }
            csvParser.Close();            
            rowCount = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetRowCountForMapPreview(fromScreen, rowCount);


            //-------------------------------------------------------------------
            //GET FIELD TEMPLATE 
            List<UserDefinedDataSourceTemplateInfo> fieldTemplate = new List<UserDefinedDataSourceTemplateInfo>();
            fieldTemplate = AIMManager.GetUserDefinedDataSourceTemplate(defID);

            //-------------------------------------------------------------------
            //GET COLUMN NAME TO USE AS BREAK IN SAMPLES (PORTRAIT)
            if (orientation == "Portrait")
            {
                foreach (var item in fieldTemplate)
                {
                    if (item.UseAsSampleCode == true)
                    {
                        useAsSampleBreak = item.MapColumnName;
                        break;
                    }
                }
            }           
            

            //-------------------------------------------------------------------			
            //VALIDATE MAPPING
            DevExpress.XtraEditors.LookUpEdit[] lueName = { lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation };
            string[] colName = { "Material", "Owner", "OwnerLocation", "Supplier", "SupplierLocation" };
            Int32 validate = 0;
            validate = CFCAIM.AIMUtilities.Imports.SharedFunctions.ValidateMap(defID, fieldTemplate, lueName, colName, filename, orientation);
            if (validate != 0)
            {
                AIMGlobal.importValidateError = validate;
                return importHeader;
            }

            //-------------------------------------------------------------------            
            //BUILD IMPORT-POINTER ARRAY TO CHECK ORDER OF COLUMNS
            if (orientation == "Landscape")
            {
                landscapeBeginDetailColumn = beginDetailColumn - 1;               
            }           
            List<FileTypeInfo> fileType = new List<FileTypeInfo>();
            fileType = AIMManager.GetFileType(_importFileTypeID);
            fileTypeName = fileType[0].Name;

            //8/22/22
            //msg = CFCAIM.AIMUtilities.Imports.SharedFunctions.BuildImportPointer(defID, orientation, fileTypeName, fieldTemplate, dataList, headerRow, landscapeBeginDetailColumn, colCount);
            msg = CFCAIM.AIMUtilities.Imports.SharedFunctions.BuildImportPointer(defID, orientation, fileTypeName, fieldTemplate, dataList, headerRow, landscapeBeginDetailColumn, colCount, 0);

            if (msg != "")
            {
                AIMGlobal.SplashScreenHide();

                //10/26/21 - Using "System.Windows.MessageBox" instead of "System.Windows.Forms.MessageBox" caused resize issues for select customers
                //System.Windows.MessageBox.Show(msg);
                System.Windows.Forms.MessageBox.Show(msg);
               
                return importHeader;
            }

            switch (orientation)
            {
                //-----------------------------------------------------------
                //L A N D S C A P E
                case "Landscape":
                    #region Landscape

                    //-------------------------------------------------------
                    //LOOP THROUGH IMPORT FILE ROWS - put in work entity  
                    #region loop thru import file rows                  

                    //8/23/19 - added BeginDetailRow column
                    //for (int i = headerRow + 1; i <= rowCount; i++)
                    for (int i = headerRow + _detailBeginRow; i <= rowCount; i++)
                        {                       
                        CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);
                        
                        AIMGlobal.txtProgressMessage = "Reading import data ...";                                                               //8/5/19                                 
                        CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundPreview);                                   //8/5/19

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
                        #region loop thru field template columns       
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
                                if (dataList[arrayCount][pointer] != null)
                                {
                                    str = dataList[arrayCount][pointer];
                                }

                                //-------------------------------------------
                                //ADD HEADER (Landscape)
                                #region add header
                                if (_blnWriteHeader == true)
                                {
                                    //12/23/20
                                    //SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation);
                                    SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);

                                    //Set sample-code                                    
                                    _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, "", false);

                                    //Make sure sample-code doesn't already exist on SAMPLE TABLE
                                                                        
                                    //2/15/21
                                    //_existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode, def.DataSourceTypeID);
                                    _existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode);


                                    //Make sure sample-code doesn't already exist on CURRENT entity

                                    //11/18/21
                                    //_sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader);
                                    _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader, "SampleCode");

                                    //Set Existing HeaderID
                                    if (_sampleCodeExists == true)
                                    {
                                        //2/15/21
                                        //_existingHeaderID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSampleCodeAlreadyExists(_sampleCode);
                                        _existingHeaderID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSampleCodeAlreadyExists(_sampleCode, def.DataSourceTypeID);
                                    } 
                                                                       
                                    AddHeader(_headerID, filename);
                                    IncrementCountersAndIDs(orientation);
                                    InitializeAIMRequiredFields();
                                }
                                #endregion
                                                                
                                _saveSampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSaveSampleCode(isDetail, _dateForGeneratedSampleCode, _sampleCounter, _saveSampleCode);                                

                                SetVariables(mapColumnName, str, isDetail, _dateFormatID);

                                //-------------------------------------------
                                //ADD DETAIL
                                #region add detail
                                if (isDetail == true)
                                {

                                    //Check for numeric result; if not numeric, skip this nutrient
                                    if (CFCAIM.AIMUtilities.Imports.SharedFunctions.IsNumeric(str) == true)
                                    {                 
                                        SetVariables(mapColumnName, str, isDetail, _dateFormatID);

                                        //for landscape, use header row for nutrient name/code
                                        if (j >= fieldTemplate.Count)
                                        {
                                            _analyzerNutrientCode = "";
                                        }
                                        else
                                        {
                                            _analyzerNutrientCode = dataList[0][pointer];
                                        }
                                        workDetail.Add(new ImportDetailInfo
                                        {
                                            ImportDetailID = _detailID,
                                            ImportHeaderID = _headerID,
                                            AnalyzerNutrientCode = _analyzerNutrientCode,
                                            ResultValueString = _resultValueString,
                                            UnitType = _unitType,
                                            DetailMessage = detailMessage                   //this field isn't used on import
                                        }
                                        );
                                        _detailID++;
                                    }
                                }
                                #endregion
                            }

                            catch (Exception ex)
                            {
                                //10/26/21 - Using "System.Windows.MessageBox" instead of "System.Windows.Forms.MessageBox" caused resize issues for select customers
                                //System.Windows.MessageBox.Show(ex.Message);
                                System.Windows.Forms.MessageBox.Show(msg);                                
                            }
                        }
                        #endregion

                        arrayCount++;
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

                    //12/23/20
                    //SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation);
                    SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);

                    //Set sample-code                    
                    _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, "", false);

                    //Make sure sample-code doesn't already exist on SAMPLE TABLE
                                        
                    //2/15/21
                    //_existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode, def.DataSourceTypeID);
                    _existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode);

                    //Make sure sample-code doesn't already exist on CURRENT entity                    

                    //11/18/21
                    //_sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader);
                    _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader, "SampleCode");

                    //Set ExistingHeaderID column
                    if (_sampleCodeExists == true)
                    {
                        //2/15/21
                        //_existingHeaderID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSampleCodeAlreadyExists(_sampleCode);
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
                            ////AddResults(arrayID - 1, orientation);
                            AddResults(arrayID - 1, orientation, "");

                        }
                    }
                    #endregion

                    _saveHeaderID = _headerID;
                    break;
                    #endregion

                //-----------------------------------------------------------
                //P O R T R A I T
                case "Portrait":
                    #region Portrait

                    //-------------------------------------------------------
                    //LOOP THROUGH IMPORT FILE ROWS - put in work entity    
                    #region loop thru import file rows                                                                           

                    for (int n = headerRow; n < rowCount; n++)                    
                    {
                        CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);                                                
                        AIMGlobal.txtProgressMessage = "Reading import data ...";                                                               
                        CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundPreview);

                        //11/26/21
                        if (AIMGlobal.noGrouping == true && _firstRec == false)
                        {
                            _blnWriteHeader = true;
                        }

                        //8/16/22 - for Romer imports (that are not composite batches) use Alternate Code as the sample-break column.

                        //---------------------------------------------------
                        //CHECK TO SEE IF HEADER SHOULD BE WRITTEN                        
                        _blnWriteHeader = false;                                              
                        _blnWriteHeader = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleBreakColumn(useAsSampleBreak, _workHeader, _saveSampleCode);

                        //---------------------------------------------------
                        //IF AUTO-GENERATE SAMPLE-CODE, INCREMENT COUNTER      

                        //11/18/21 - commented
                        ////4/12/21
                        //IncrementAutoSampleNumber();                                               
                        ////if (_blnWriteHeader == true)
                        ////{
                        ////    IncrementAutoSampleNumber();
                        ////}

                        //---------------------------------------------------
                        //Check to see if detail should be written
                        _blnWriteDetail = false;    
                        
                        //11/19/21 - commented
                        //if (AIMGlobal.generateSampleCode == true)                                                  
                        //{                                       
                        //    if (_sampleCode != _saveSampleCode && _saveSampleCode != "")
                        //    {
                        //        _blnWriteDetail = true;
                        //    }                                
                        //}                       
                        //else
                        //{
                        //    if (_saveSampleCode != "")                                  
                            //{
                            //    _blnWriteDetail = true;
                            //}
                        //}  
                        if (_saveSampleCode != "")
                        {
                            _blnWriteDetail = true;
                        }

                        //---------------------------------------------------
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
                                if (dataList[n][pointer] != null)
                                {
                                    str = dataList[n][pointer];
                                }

                                //REMOVE ANY SPECIAL CHARS FROM BEGINNING/ENDING THAT NIR MACHINE MAY HAVE INSERTED                                
                                char[] specialChar = { '\"' };
                                str = str.TrimEnd(specialChar);
                                str = str.TrimStart(specialChar);    
                                
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
                                    //12/23/20
                                    //SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation);
                                    SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);                                                                       

                                    //4/12/21 -----------------------------------------------
                                    //Added for portrait imports where user wants system to generate sample-codes, but also wants to group the samples by another field (and not the sys-gen sample code).                                    
                                    if (AIMGlobal.useAnotherFieldAsSampleCode == true && AIMGlobal.generateSampleCode == true)
                                    {
                                        _sampleCode = "";
                                    }

                                    //----------------------------------------------------------
                                    //Set sample-code                                            

                                    //8/16/22
                                    //_sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, _saveSampleCode, _useThisFieldAsSampleCode);
                                    _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCodeTxtCSVPortrait(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, _saveSampleCode, ColumnToUseForGrouping);

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
                                    if (_sampleCodeExists == true)                          //7/30/19 - added for UpdateHeaderOnly option
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
                                    blnDetailAlreadyWritten = false;       //11/18/21     

                                    //11/18/21 - for samples that have system-generated sample codes                                    
                                    ////if (ColumnToUseForGrouping == "")
                                    if (AIMGlobal.generateSampleCode == true)
                                    {
                                        incrementCounter = true;
                                    }
                                }

                                //11/18/21 -------------------------------------------------------                                        
                                // check _workheader to see if header already exists
                                // if it does, get the header-pointer for the detail
                                // add detail to sub-entity from variables                                        
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
                                                SetVariables(mapColumnName, str, isDetail, _dateFormatID);
                                                AddResults(ptr, orientation, ColumnToUseForGrouping);

                                                blnDetailAlreadyWritten = true;
                                            }
                                        }
                                    }
                                    //---------------------------------------------------------------
                                }

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

                                        //8/15/22
                                        _sqlResultIDInfo = "";
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

                                //11/18/21
                                lastProcessedSampleCode = _saveSampleCode;                               

                                //----------------------------------------------------------                                                                
                                if (AIMGlobal.sampleCodeMapped == true)
                                {
                                    _saveSampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSaveSampleCode(isDetail, _dateForGeneratedSampleCode, _sampleCounter, _saveSampleCode);
                                }

                                //11/26/21
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
                                else if (AIMGlobal.sampleCodeMapped == false && AIMGlobal.generateSampleCode == true && incrementCounter == true)
                                {
                                    if (_firstRec == false)
                                    {
                                        _sampleCounter++;
                                    }
                                    incrementCounter = false;
                                }

                                SetVariables(mapColumnName, str, isDetail, _dateFormatID);          //(if mapColumnName = SampleCode, _saveSampleCode gets set in here)
                            }

                            catch (Exception ex)
                            {                                
                                System.Windows.Forms.MessageBox.Show(msg);
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
                    _blnWriteHeader = false;

                    //3/8/21 ---------------- this wasn't added to the Excel portrait imports (and I wonder if it should be)                  
                    if (useAsSampleBreak != "")
                    {
                        _blnWriteHeader = CFCAIM.AIMUtilities.Imports.SharedFunctions.WriteHeaderCheck(useAsSampleBreak, _workHeader, _saveSampleCode);
                    }
                    else
                    //-----------------------

                    {
                        var samp = from g in _workHeader
                                   where g.SampleCode == _saveSampleCode
                                   select new { g.SampleCode };
                        if (samp.Count() == 0 && _saveSampleCode != "")
                        {
                            _blnWriteHeader = true;
                        }
                    }


                    //---------------------------------------------------
                    //IF AUTO-GENERATE SAMPLE-CODE, INCREMENT COUNTER

                    //8/16/22
                    //IncrementAutoSampleNumber();
                    if (incrementCounter == true)
                    {
                        IncrementAutoSampleNumber();
                    }

                    _blnWriteDetail = false;

                    //11/18/21
                    //if (_saveSampleCode != "")
                    if (_saveSampleCode != "" && blnDetailAlreadyWritten == false)
                    {
                        _blnWriteDetail = true;
                    }

                    if (_blnWriteHeader == true)
                    {
                        //12/23/20
                        //SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation);
                        SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);

                        //Set sample-code                        

                        //8/16/22
                        //_sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCode(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, _saveSampleCode, _useThisFieldAsSampleCode);
                        _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCodeTxtCSVPortrait(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, _saveSampleCode, ColumnToUseForGrouping);

                        //Make sure sample-code doesn't already exist on SAMPLE TABLE

                        //2/15/21
                        //_existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode, def.DataSourceTypeID);
                        _existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_sampleCode);

                        //Make sure sample-code doesn't already exist on CURRENT entity        
                                                
                        //11/18/21
                        //_sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader);                        
                        _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckForExistingSampleCode(_sampleCode, _existingSampleID, _workHeader, ColumnToUseForGrouping);

                        //Set ExistingHeaderID column
                        _existingHeaderID = 0;
                        if (_sampleCodeExists == true)
                        {                            
                            _existingHeaderID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSampleCodeAlreadyExists(_sampleCode, def.DataSourceTypeID);
                                                        
                            //2/15/21
                            //CFCAIM.AIMUtilities.Imports.SharedFunctions.SetExistingHeaderFieldsBeforeUpdate(_sampleCode);
                            CFCAIM.AIMUtilities.Imports.SharedFunctions.SetExistingHeaderFieldsBeforeUpdate(_sampleCode, def.DataSourceTypeID);
                        }

                        
                        AddHeader(_headerID, filename);

                        //7/30/19 - added for UpdateHeaderOnly option                   
                        if (_sampleCodeExists == true)
                        {                           
                            CFCAIM.AIMUtilities.Imports.SharedFunctions.InitializeHeaderVariablesAfterUpdate();
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
                      
                    //11/18/21
                    //if (_saveSampleCode != "")
                    if (_saveSampleCode != "" && blnDetailAlreadyWritten == false)

                    {
                        _blnWriteDetail = true;
                    }
                    
                    if (_blnWriteDetail == true)
                    {         
                        //11/24/21
                        //AddResults(headerPointerForDetail, orientation);
                        AddResults(headerPointerForDetail, orientation, ColumnToUseForGrouping);
                    }
                    #endregion

                    break;
                    #endregion
                    
                default:
                    break;
            }

            //CHECK TO SEE IF DATASOURCE IS MAPPED
            Boolean dataSourceIsMapped = false;
            UserDefinedDataSourceDefinitionInfo dataSource = new UserDefinedDataSourceDefinitionInfo();
            dataSource = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataSource.DataSourceIsMapped == true)
            {
                dataSourceIsMapped = true;
            }

            //-----------------------------------------------------------------------------
            //MOVE WORK TO IMPORT HEADER ENTITY
            importHeader = CFCAIM.AIMUtilities.Imports.SharedFunctions.CopyWorkToImportHeaderEntity(_workHeader, fromScreen, defID, importType, allowDuplicates, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, dataSourceIsMapped, backgroundWorker, backgroundPreview);

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
                MaterialName = _material,
                OwnerName = _owner,
                OwnerLocationName = _ownerLocation,
                SourceName = _supplier,
                SourceLocationName = _supplierLocation,

                BillOfLading = _billOfLading,
                ContractNumber = _contractNumber,
                LoadNumber = _loadNumber,
                LotNumber = _lotNumber,

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
                ExistingSampleID = _existingSampleID,                
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


            //-------------------------------------------------------
            if (orientation == "Portrait")
            {
                ////////////If sample already exists on current entity, reset the ImportHeaderID and array pointer
                ////////////11/24/21 - uncommented
                //////////////11/18/21 - commented                  
                //////////////if (CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnCurrentEntity(_sampleCode, _workHeader) == true)
                //////////    if (CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnCurrentEntity(_saveSampleCode, _workHeader, ColumnToUseForGrouping) == true)
                //////////    ////if (CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnCurrentEntity(_sampleCode, _workHeader, "SampleCode") == true)
                //////////    {
                //////////        for (int i = 0; i < _workHeader.Count; i++)
                //////////        {
                //////////            if (sampleFound == false)
                //////////            {
                //////////                if (_sampleCode == _workHeader[i].SampleCode)
                //////////                {
                //////////                    hdrPointer = i;
                //////////                    sampleFound = true;
                //////////                }
                //////////            }
                //////////        }
                //////////    }

                Int32 ptr = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetPointerForResults(_saveSampleCode, _workHeader, ColumnToUseForGrouping);
                hdrID = ptr;
                pointer = ptr;
            }
            

            //11/24/21
            //if (_workHeader[hdrPointer].DetailImport == null)
            //{
            //    _workHeader[hdrPointer].DetailImport = new List<ImportDetailInfo>();
            //}
            if (_workHeader[pointer].DetailImport == null)
            {
                _workHeader[pointer].DetailImport = new List<ImportDetailInfo>();
            }

            //11/24/21
            //_workHeader[hdrPointer].DetailImport.Add(new ImportDetailInfo
            _workHeader[pointer].DetailImport.Add(new ImportDetailInfo

            {
                ImportDetailID = _detailID,
                ImportHeaderID = hdrID,
                AnalyzerNutrientCode = _analyzerNutrientCode,
                ResultValueString = _resultValueString,
                AnalyzerDataSourceTypeName = _analyzerDataSourceTypeName,
                UnitType = _unitType,
                SQLResultIDInfo = _sqlResultIDInfo              //added 8/15/22
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

            //8/15/22
            _sqlResultIDInfo = "";

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
            _delimiterField = ',';
            _decimalSeparatorID = 2;        //4/2/19
            _headerPointerForDetail = 0;
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

        //12/23/20
        //public static String SetRequiredVariables(String fromScreen,DevExpress.XtraEditors.LookUpEdit lueMaterial,DevExpress.XtraEditors.LookUpEdit lueOwner,DevExpress.XtraEditors.LookUpEdit lueOwnerLocation,DevExpress.XtraEditors.LookUpEdit lueSource, DevExpress.XtraEditors.LookUpEdit lueSourceLocation)
        public static String SetRequiredVariables(String fromScreen, DevExpress.XtraEditors.LookUpEdit lueMaterial, DevExpress.XtraEditors.LookUpEdit lueOwner, DevExpress.XtraEditors.LookUpEdit lueOwnerLocation, DevExpress.XtraEditors.LookUpEdit lueSource, DevExpress.XtraEditors.LookUpEdit lueSourceLocation, String useThisOwner, String useThisOwnerLocation, String useThisSupplier, String useThisSupplierLocation)
        {
            //For the required fields, if user chose to "Select On Import", this function sets the required fields from the lookup dropdowns.
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

            //12/23/20
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
                _aimSupplierID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSupplierAndSupplierLocationID("Supplier", _supplier, _supplier.Length);
            }

            if (_supplierLocation.Length > 0 && _aimSupplierLocationID == 0)
            {
                _aimSupplierLocationID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetSupplierAndSupplierLocationID("SupplierLocation", _supplierLocation, _supplierLocation.Length);
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
            //This function uses the mapped fields to set the sample and detail fields

            if (isDetail == true)
            {
                switch (mapColumnName)
                {
                    case "AnalyzerNutrientCode":
                        _analyzerNutrientCode = str;
                        break;
                    case "ResultValue":


                        //--------------------------------------------------------------
                        //5/31/19     
                        //4/2/19
                        //if (_decimalSeparatorID != (Int32)EnumDecimalSeparator.Period)
                        //{
                        //    str = str.Replace(",", ".");
                        //}
                       
                       
                        CultureInfo ci = CultureInfo.CurrentCulture;
                        String decSep = ci.NumberFormat.NumberDecimalSeparator;
                        //String decSepTemplate = "";
                        //if (_decimalSeparatorID == (Int32)EnumDecimalSeparator.Period)
                        //{
                        //    decSepTemplate = ".";
                        //}
                        //else
                        //{
                        //    decSepTemplate = ",";
                        //}                        
                        //if (decSep != decSepTemplate)
                        //{
                        //    str = str.Replace(decSepTemplate, decSep);
                        //}
                        //--------------------------------------------------------------

                        //String decSepSystem = "";
                        //3/4/21 - removed unnecessary assignment
                        //DecimalSeparatorInfo dec = new DecimalSeparatorInfo();
                        DecimalSeparatorInfo dec;

                        String decSepSystem = ci.NumberFormat.NumberDecimalSeparator;
                        dec = AIMManager.GetDecimalSeparator(_decimalSeparatorID);                       
                        if (dec.Name != decSep)
                        {
                            str = str.Replace(dec.Name, decSepSystem);
                        }
                        //--------------------------------------------------------------

                        _resultValueString = str;
                        break;
                    case "UnitType":
                        _unitType = str;
                        break;
                    case "DataSource":
                        _analyzerDataSourceTypeName = str;
                        break;

                    //8/15/22
                    case "SQLResultIDInfo":
                        _sqlResultIDInfo = str;                                               
                        break;

                    default:
                        break;
                }

            }
            else
            {
                switch (mapColumnName)
                {
                    case "Material":
                        _material = str;
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

                    case "SampleCode":                                             
                        _sampleCode = str.Trim();
                        _lastSampleCode = _saveSampleCode;
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
                    case "Notes":
                        _notes = str;
                        break;

                    default:
                        break;
                }
            }

            //11/19/21            
            if (_useThisFieldAsSampleCode == true)
            {
                _sampleCode = str.Trim();
                _lastSampleCode = _saveSampleCode;
                _saveSampleCode = _sampleCode;
            }

            return "";
        }

    }
}
