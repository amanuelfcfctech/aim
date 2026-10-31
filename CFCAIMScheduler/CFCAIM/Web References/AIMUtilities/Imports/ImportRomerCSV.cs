using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

//8/30/22
using CsvHelper;
using System.IO;


namespace CFCAIM.AIMUtilities.Imports
{
    class ImportRomerCSV
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

        public static Int32 _uniqueSampleID = 0;

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

        public static List<ImportHeaderInfo> RomerImportSamples(String fromScreen, Int32 defID, Int32 createOrMerge, Boolean allowDuplicates, Int32 lastHeaderID, String orientation, String filename, Int32 headerRow, DevExpress.XtraEditors.LookUpEdit lueMaterial, DevExpress.XtraEditors.LookUpEdit lueOwner, DevExpress.XtraEditors.LookUpEdit lueOwnerLocation, DevExpress.XtraEditors.LookUpEdit lueSource, DevExpress.XtraEditors.LookUpEdit lueSourceLocation, Int32 beginDetailColumn, BackgroundWorker backgroundPreview, String useThisOwner, String useThisOwnerLocation, String useThisSupplier, String useThisSupplierLocation, Int32 detailBeginRow)
        {
            using (BackgroundWorker backgroundWorker = new BackgroundWorker())
            {
                return RomerImportSamples(
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
                                        , detailBeginRow                    //3/7/23
                                        );
            }
        }
        public static List<ImportHeaderInfo> RomerImportSamples(
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
                                , Int32 detailBeginRow                      //3/7/23
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
            Int32 colCount = 0;
            Int32 rowCount = 0;            
            Int32 headerPointerForDetail = 0;       //portrait only		         				
            Int32 pointer = 0;
            String orignalMapName = "";
            String useAsSampleBreak = "";
            String ColumnToUseForGrouping = "";                         //11/18/21
            Boolean blnDetailAlreadyWritten = false;                    //11/18/21                
            String lastProcessedSampleCode = "";                        //11/18/21 - used in portrait exports, where there may be multiple lines for the same sample-code, but the lines are not in order.
                                                                        //           They could show up anywhere in the import file.
            Boolean incrementCounter = false;                           //11/18/21


            // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
            // Romer sample must have 17 columns and the first column
            // When searching for the heading row, "Sample" in column 1 signifies the heading row.
            // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *


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

            ////-------------------------------------------------------------------
            ////GET COLUMN COUNT	                                  
            //5/2/23 
            //TextFieldParser csvParserColCount = new TextFieldParser(filename);
            //csvParserColCount.SetDelimiters(new string[] { _delimiterField.ToString() });
            //csvParserColCount.HasFieldsEnclosedInQuotes = true;
            //if (!csvParserColCount.EndOfData)
            //{
            //    string[] flds = csvParserColCount.ReadFields();
            //    colCount = flds.Count();
            //}
            //csvParserColCount.Close();
            colCount = 17;


            //-------------------------------------------------------------------
            //GET ROW COUNT AND POPULATE FIELD ARRAY	                                                                                    
            List<String[]> dataList = new List<string[]>();            
            TextFieldParser csvParser = new TextFieldParser(filename);
            csvParser.SetDelimiters(new string[] { _delimiterField.ToString() });
            csvParser.HasFieldsEnclosedInQuotes = true;            
            colCount++;
                                                                                            
            while (!csvParser.EndOfData)
            {
                dataList.Add(csvParser.ReadFields());                
                rowCount++;
            }
            csvParser.Close();
            rowCount = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetRowCountForMapPreview(fromScreen, rowCount);

            //-------------------------------------------------------------------
            //8/22/22            
            //CHANGE LAST FIELD TO UNIQUE ID FIELDS
            try
            {
                String strDate = "";
                Int32 cntr = 0;

                dataList[headerRow - 1][15] = "UniqueSampleID";                 
                for (int row = detailBeginRow - 1; row < rowCount; row++)
                {                               
                    
                    strDate = dataList[row][2].Substring(0, 10).Trim();                                      
                    if (dataList[row][0] == "")     //(Composite batches do not have a sample#)
                    {
                        //UniqueID = Cntr + Date + Reader Serial# -- at end of function, replace Cntr with SysGen sampleCode (need Cntr to "group" the composite samples, because the sampleCode is not known at time time)
                        cntr = cntr + 1;                        
                        dataList[row][15] = cntr + "_" + strDate + "_" + dataList[row][14];
                    }
                    else
                    {
                        //UniqueID = Sample + Date + Reader Serial#                        
                        dataList[row][15] = dataList[row][0] + "_" + strDate + "_" + dataList[row][14];
                    }                                                             
                }
            }
            catch (Exception ex)
            {
                msg = "The import pointer is incorrect. Make sure the header/detail begin rows on the import file match the template settings.";
                System.Windows.Forms.MessageBox.Show(msg);                
            }

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
            List<FileTypeInfo> fileType = new List<FileTypeInfo>();
            fileType = AIMManager.GetFileType(_importFileTypeID);
            fileTypeName = fileType[0].Name;

            msg = CFCAIM.AIMUtilities.Imports.SharedFunctions.BuildImportPointer(defID, orientation, fileTypeName, fieldTemplate, dataList, headerRow, 0, colCount, (Int32)EnumImportType.RomerResults);
            if (msg != "")
            {
                AIMGlobal.SplashScreenHide();
                System.Windows.Forms.MessageBox.Show(msg);
                return importHeader;
            }

            //-------------------------------------------------------
            //LOOP THROUGH IMPORT FILE ROWS - put in work entity    
            #region loop thru import file rows   
                        
            for (int n = headerRow; n < rowCount; n++)
            {
                CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);
                AIMGlobal.txtProgressMessage = "Reading import data ...";
                CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundPreview);

                if (AIMGlobal.noGrouping == true && _firstRec == false)               
                {
                    _blnWriteHeader = true;
                }

                //8/16/22 - Romer imports (that are not composite batches) use Alternate Code as the sample-break column.

                //---------------------------------------------------
                //CHECK TO SEE IF HEADER SHOULD BE WRITTEN                        
                _blnWriteHeader = false;
                _blnWriteHeader = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleBreakColumn(useAsSampleBreak, _workHeader, _saveSampleCode);

                //---------------------------------------------------
                //Check to see if detail should be written
                _blnWriteDetail = false;
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
                        
                        //Added for portrait imports where user wants system to generate sample-codes, but also wants to group the samples by another field (and not the sys-gen sample code).
                        //If the user selects system-generate sample-codes and also groups by another field, the AIMGlobal.sampleCodeMapped field is set to FALSE.
                        if (_useThisFieldAsSampleCode == true)
                        {
                            AIMGlobal.sampleCodeMapped = false;
                            ColumnToUseForGrouping = mapColumnName;
                        }

                        //----------------------------------------------------------
                        //ADD HEADER (Portrait)
                        #region add header
                        if (_blnWriteHeader == true)
                        {                            
                            SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);
                            
                            //----------------------------------------------------------
                            //Set sample-code                                                                        
                            _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCodeTxtCSVPortrait(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, _saveSampleCode, ColumnToUseForGrouping);

                            //Make sure sample-code doesn't already exist on SAMPLE TABLE                                                        
                            _existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.RomerSampleCodeAlreadyExistsOnTable(_saveSampleCode, (Int32)lueOwner.EditValue, (Int32)lueOwnerLocation.EditValue, (Int32)lueSource.EditValue, (Int32)lueSourceLocation.EditValue);

                            //Make sure sample-code doesn't already exist on CURRENT entity                                                              
                            _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.RomerCheckForExistingSampleCode(_saveSampleCode, (Int32)lueOwner.EditValue, (Int32)lueOwnerLocation.EditValue, (Int32)lueSource.EditValue, (Int32)lueSourceLocation.EditValue, _existingSampleID, _workHeader, ColumnToUseForGrouping);

                            //Set existing header field columns
                            if (_sampleCodeExists == true)
                            {                                
                                SetExistingHeaderFieldsBeforeUpdateUsingSampleID(_existingSampleID);
                            }
                       
                            //5/2/23 - set Owner variable for Map screen
                            if (fromScreen == "Map")
                            {
                                List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
                                owner = AIMManager.GetAIMOwner(true);
                                if (owner.Count == 1)
                                {
                                    SetVariables("Owner", owner[0].OwnerCode, false, 1);
                                }
                            }

                            AddHeader(_headerID, filename);
                            if (_sampleCodeExists == true)                          
                            {
                                InitHeaderVariablesAfterUpdate();
                            }

                            IncrementCountersAndIDs(orientation);
                            InitializeAIMRequiredFields();

                            if (_firstRec == false)
                            {
                                headerPointerForDetail++;
                            }
                            _firstRec = false;
                            blnDetailAlreadyWritten = false;                             
                            if (AIMGlobal.generateSampleCode == true)
                            {
                                incrementCounter = true;
                            }
                        }                       

                        #endregion

                        //----------------------------------------------------------
                        //ADD DETAIL
                        #region add detail
                        if (_blnWriteDetail == true && blnDetailAlreadyWritten == false)
                        {
                            if (AIMGlobal.sampleCodeMapped == true || AIMGlobal.noGrouping == true)
                            {                                
                                ColumnToUseForGrouping = "SampleCode";
                            }    

                            //8/23/22
                            //AddResults(headerPointerForDetail, orientation, ColumnToUseForGrouping);
                            AddResults(headerPointerForDetail, orientation, ColumnToUseForGrouping, def.IsCompositeBatch);

                            _detailID++;
                            _sqlResultIDInfo = "";                            
                            _blnWriteDetail = false;

                        }
                        #endregion
                        //----------------------------------------------------------
                        if (_firstRec == true)
                        {
                            incrementCounter = true;
                        }
                        lastProcessedSampleCode = _saveSampleCode;

                        //----------------------------------------------------------                                                                
                        if (AIMGlobal.noGrouping == true)
                        {
                            if (incrementCounter == true)
                            {
                                _sampleCounter++;
                            }
                            _saveSampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSaveSampleCode(isDetail, _dateForGeneratedSampleCode, _sampleCounter, _saveSampleCode);
                            incrementCounter = false;
                        }
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
            if (incrementCounter == true)
            {
                IncrementAutoSampleNumber();
            }

            _blnWriteDetail = false;

            if (_saveSampleCode != "" && blnDetailAlreadyWritten == false)
            {
                _blnWriteDetail = true;
            }

            if (_blnWriteHeader == true)
            {                
                SetRequiredVariables(fromScreen, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);

                //Set sample-code                        
                _sampleCode = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleCodeTxtCSVPortrait(_dateForGeneratedSampleCode, _sampleCounter, _sampleCode, _saveSampleCode, ColumnToUseForGrouping);

                //Make sure sample-code doesn't already exist on SAMPLE TABLE
                _existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.RomerSampleCodeAlreadyExistsOnTable(_saveSampleCode, (Int32)lueOwner.EditValue, (Int32)lueOwnerLocation.EditValue, (Int32)lueSource.EditValue, (Int32)lueSourceLocation.EditValue);

                //Make sure sample-code doesn't already exist on CURRENT entity        
                _sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.RomerCheckForExistingSampleCode(_saveSampleCode, (Int32)lueOwner.EditValue, (Int32)lueOwnerLocation.EditValue, (Int32)lueSource.EditValue, (Int32)lueSourceLocation.EditValue, _existingSampleID, _workHeader, ColumnToUseForGrouping);

                //Set existing header field columns                
                if (_sampleCodeExists == true)
                {   
                    SetExistingHeaderFieldsBeforeUpdateUsingSampleID(_existingSampleID);
                }

                //5/2/23 - set Owner variable for Map screen
                if (fromScreen == "Map")
                {
                    List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
                    owner = AIMManager.GetAIMOwner(true);
                    if (owner.Count == 1)
                    {
                        SetVariables("Owner", owner[0].OwnerCode, false, 1);
                    }
                }


                AddHeader(_headerID, filename);           
                if (_sampleCodeExists == true)
                {
                    InitHeaderVariablesAfterUpdate();
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
            
            if (_saveSampleCode != "" && blnDetailAlreadyWritten == false)
            {
                _blnWriteDetail = true;
            }

            if (_blnWriteDetail == true)
            {     
                //8/23/22
                //AddResults(headerPointerForDetail, orientation, ColumnToUseForGrouping);
                AddResults(headerPointerForDetail, orientation, ColumnToUseForGrouping, def.IsCompositeBatch);
            }                

            //-----------------------------------------------------------------------------
            //MOVE WORK TO IMPORT HEADER ENTITY
            importHeader = CFCAIM.AIMUtilities.Imports.SharedFunctions.CopyWorkToImportHeaderEntity(_workHeader, fromScreen, defID, importType, allowDuplicates, lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, false, backgroundWorker, backgroundPreview);

            AIMGlobal.SplashScreenHide();
            AIMGlobal.lastImportHeaderID = _saveHeaderID;

            //get last samplecode from last import record (need this if multiple files are imported)  
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
                Notes = _notes,

                UniqueSampleID = _saveSampleCode

            }
            );

            return 0;

        }


        //public static Int32 AddResults(Int32 pointer, String orientation, String ColumnToUseForGrouping)
        public static Int32 AddResults(Int32 pointer, String orientation, String ColumnToUseForGrouping, Boolean isComposite)
        {

            Int32 hdrID = 0;
            hdrID = _workHeader[pointer].ImportHeaderID;


            //---------------------------------------------------------------------
            if (orientation == "Portrait")
            {               
                Int32 ptr = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetPointerForResults(_saveSampleCode, _workHeader, ColumnToUseForGrouping);
                hdrID = ptr;
                pointer = ptr;
            }
            if (_workHeader[pointer].DetailImport == null)
            {
                _workHeader[pointer].DetailImport = new List<ImportDetailInfo>();
            }

            //---------------------------------------------------------------------
            //Replace Counter in UniqueID field with SysGen sampleID (The counter was needed to "group" the samples, but the samplecode was unknown at the time of grouping)    
            if (isComposite == true)
            {
                Int32 intIndex = 0;
                String sqlField = "";
                intIndex = _sqlResultIDInfo.IndexOf('_');
                sqlField = _sqlResultIDInfo;
                sqlField = sqlField.Substring(intIndex + 1, _sqlResultIDInfo.Length - (intIndex + 1));
                sqlField = _workHeader[pointer].SampleCode + "_" + sqlField;
                _sqlResultIDInfo = sqlField;
            }

            //---------------------------------------------------------------------
            _workHeader[pointer].DetailImport.Add(new ImportDetailInfo
            {
                ImportDetailID = _detailID,
                ImportHeaderID = hdrID,
                AnalyzerNutrientCode = _analyzerNutrientCode,
                ResultValueString = _resultValueString,
                AnalyzerDataSourceTypeName = _analyzerDataSourceTypeName,
                UnitType = _unitType,
                SQLResultIDInfo = _sqlResultIDInfo              
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

        
        public static String SetRequiredVariables(String fromScreen, DevExpress.XtraEditors.LookUpEdit lueMaterial, DevExpress.XtraEditors.LookUpEdit lueOwner, DevExpress.XtraEditors.LookUpEdit lueOwnerLocation, DevExpress.XtraEditors.LookUpEdit lueSource, DevExpress.XtraEditors.LookUpEdit lueSourceLocation, String useThisOwner, String useThisOwnerLocation, String useThisSupplier, String useThisSupplierLocation)
        {
            //For the required fields, if user chose to "Select On Import", this function sets the required fields from the lookup dropdowns.
            if (fromScreen == "Import")
            {

                //5/2/23 - material is mapped, so commented these lines
                //if (lueMaterial.Enabled == true)
                //{
                //    _aimMaterialID = (Int32)lueMaterial.EditValue;
                //    _material = lueMaterial.Text.ToString();
                //}
               
                if (lueOwner.Enabled == true)
                {
                    _aimOwnerID = (Int32)lueOwner.EditValue;
                    _owner = lueOwner.Text.ToString();
                }

                //5/2/23 - if lueOwner is not enabled, it means there is only ONE owner
                else
                {
                    List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
                    owner = AIMManager.GetAIMOwner(true);
                    if (owner.Count == 1)
                    {
                        _aimOwnerID = owner[0].AIMOwnerID;
                        _owner = owner[0].OwnerCode;
                    }
                }
                                
                //5/2/23 - ownerlocation is mapped, so commented these lines
                //if (lueOwnerLocation.Enabled == true)
                //{
                //    _aimOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                //    _ownerLocation = lueOwnerLocation.Text.ToString();
                //}

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

                        CultureInfo ci = CultureInfo.CurrentCulture;
                        String decSep = ci.NumberFormat.NumberDecimalSeparator;                        
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

                    //8/18/22
                    case "UniqueSampleID":
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

        //8/17/22
        public static String SetExistingHeaderFieldsBeforeUpdateUsingSampleID(Int32 sampleID)
        {
            String rtn = "";
            SampleInfo sample = new SampleInfo();
            sample = AIMManager.GetSample(sampleID);

            //------------------------------------------------------------------

            if (sample != null)
            {
                _sampleCode = sample.SampleCode;
                _sampleDate = sample.SampleDate.ToString();
                _alternateCode = sample.AlternateCode;
                _invoiceNumber = sample.InvoiceNumber;
                _poNumber = sample.PoNumber;
                _ticketNumber = sample.TicketNumber;
                _vehicleNumber = sample.VehicleNumber;
                _billOfLading = sample.BillOfLoad;
                _contractNumber = sample.ContractNumber;
                _loadNumber = sample.LoadNumber;
                _lotNumber = sample.LotNumber;
                _notes = sample.Notes;                
            }
            return rtn;
        }

        //8/17/22
        public static Int32 InitHeaderVariablesAfterUpdate()
        {
            _alternateCode = "";
            _sampleDate = "";
            _invoiceNumber = "";
            _poNumber = "";
            _ticketNumber = "";
            _vehicleNumber = "";
            _billOfLading = "";
            _contractNumber = "";
            _loadNumber = "";
            _lotNumber = "";
            _notes = "";

            return 0;
        }
    }

}
