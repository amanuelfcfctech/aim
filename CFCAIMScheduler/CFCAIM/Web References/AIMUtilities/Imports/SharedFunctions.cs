
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace CFCAIM.AIMUtilities.Imports
{

    class SharedFunctions
    {
        public static Int32 _aimMaterialID = 0;
        public static Int32 _aimOwnerID = 0;
        public static Int32 _aimOwnerLocationID = 0;
        public static Int32 _aimSupplierID = 0;
        public static Int32 _aimSupplierLocationID = 0;
        public static Int32 _headerSpecificMessageStatus = 0;   //0 this is used to go to specific x-ref screen for setup		
        public static Int32 _headerStatus = 0;                  // this is accept/reject
        public static String _headerMessage = "";
        public static Int32 _dataSourceTypeID = 0;

        //7/30/19 - added rest of header fields for UpdateHeaderOnly option
        public static String _alternateCode = "";

        //8/17/22
        public static String _sampleCode = "";

        //1/30/20
        //public static DateTime _sampleDate;
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

        public static Int32 _analyzerMaterialID = 0;
        public static Int32 _analyzerOwnerID = 0;
        public static Int32 _analyzerOwnerLocationID = 0;
        public static Int32 _analyzerSupplierID = 0;
        public static Int32 _analyzerSupplierLocationID = 0;

        //12/24/20
        public static Int32 _analyzerSupplierLocationCombinedID = 0;

        public static Int32 _activeMaterialStatus = 0;
        public static Int32 _activeOwnerStatus = 0;
        public static Int32 _activeOwnerLocationStatus = 0;
        public static Int32 _activeSupplierStatus = 0;
        public static Int32 _activeSupplierLocationStatus = 0;

        public static Int32 _xrefMaterialStatus = 0;
        public static Int32 _xrefOwnerStatus = 0;
        public static Int32 _xrefOwnerLocationStatus = 0;
        public static Int32 _xrefSupplierStatus = 0;
        public static Int32 _xrefSupplierLocationStatus = 0;

        public static Int32 _errorMaterial = 0;
        public static Int32 _errorOwner = 0;
        public static Int32 _errorOwnerLocation = 0;
        public static Int32 _errorSupplier = 0;
        public static Int32 _errorSupplierLocation = 0;

        public static String _material = "";
        public static String _owner = "";
        public static String _ownerLocation = "";
        public static String _supplier = "";
        public static String _supplierLocation = "";

        public static String _analyzerMaterialCode = "";
        public static String _analyzerOwnerCode = "";
        public static String _analyzerOwnerLocationCode = "";
        public static String _analyzerSupplierCode = "";
        public static String _analyzerSupplierLocationCode = "";


        public static List<ImportHeaderInfo> CopyWorkToImportHeaderEntity
                (
                List<ImportHeaderInfo>
                workHeader,
                String fromScreen,
                Int32 defID,
                Int32 importType,
                Boolean allowDuplicates,
                DevExpress.XtraEditors.LookUpEdit lueMaterial,
                DevExpress.XtraEditors.LookUpEdit lueOwner,
                DevExpress.XtraEditors.LookUpEdit lueOwnerLocation,
                DevExpress.XtraEditors.LookUpEdit lueSource,
                DevExpress.XtraEditors.LookUpEdit lueSourceLocation,
                Boolean datasourceIsMapped,
                BackgroundWorker backgroundPreview                                                                                            //8/5/19		


                )

        {
            using (BackgroundWorker backgroundWorker = new BackgroundWorker())
            {
                return CopyWorkToImportHeaderEntity(
                    workHeader,
                    fromScreen,
                    defID,
                    importType,
                    allowDuplicates,
                    lueMaterial,
                    lueOwner,
                    lueOwnerLocation,
                    lueSource,
                    lueSourceLocation,
                    datasourceIsMapped,
                    backgroundWorker,
                    backgroundPreview                                                                                                    //8/5/19


                    );
            }
        }
        public static List<ImportHeaderInfo> CopyWorkToImportHeaderEntity
            (
                List<ImportHeaderInfo> workHeader,
                String fromScreen,
                Int32 defID,
                Int32 importType,           //--> importType = Merge, Create, or MergeAndOrCreate
                Boolean allowDuplicates,
                DevExpress.XtraEditors.LookUpEdit lueMaterial,
                DevExpress.XtraEditors.LookUpEdit lueOwner,
                DevExpress.XtraEditors.LookUpEdit lueOwnerLocation,
                DevExpress.XtraEditors.LookUpEdit lueSource,
                DevExpress.XtraEditors.LookUpEdit lueSourceLocation,
                Boolean datasourceIsMapped,
                BackgroundWorker backgroundWorker,
                BackgroundWorker backgroundPreview                                                                                        //8/5/19

            )

        {

            CFCUser.Global userGlobal = new CFCUser.Global();

            List<ImportHeaderInfo> importHeader = new List<ImportHeaderInfo>();


            //header fields for portrait
            _material = "";
            _owner = "";
            _ownerLocation = "";
            _supplier = "";
            _supplierLocation = "";

            String sampleDate = "";
            DateTime dtSampleDate = DateTime.Today;
            _headerStatus = 0;                  // this is accept/reject
            _headerSpecificMessageStatus = 0;   // this is used to go to specific x-ref screen for setup			
            Boolean useCurrentDate = false;
            Boolean allowAdd = true;

            _xrefMaterialStatus = 0;
            _xrefOwnerStatus = 0;
            _xrefOwnerLocationStatus = 0;
            _xrefSupplierStatus = 0;
            _xrefSupplierLocationStatus = 0;

            _activeMaterialStatus = 0;
            _activeOwnerStatus = 0;
            _activeOwnerLocationStatus = 0;
            _activeSupplierStatus = 0;
            _activeSupplierLocationStatus = 0;

            _headerMessage = "";

            _aimMaterialID = 0;
            _aimOwnerID = 0;
            _aimOwnerLocationID = 0;
            _aimSupplierID = 0;
            _aimSupplierLocationID = 0;

            _analyzerMaterialID = 0;
            _analyzerOwnerID = 0;
            _analyzerOwnerLocationID = 0;
            _analyzerSupplierID = 0;
            _analyzerSupplierLocationID = 0;

            //12/24/20
            _analyzerSupplierLocationCombinedID = 0;

            _analyzerMaterialCode = "";
            _analyzerOwnerCode = "";
            _analyzerOwnerLocationCode = "";
            _analyzerSupplierCode = "";
            _analyzerSupplierLocationCode = "";

            String analyzerDataSourceCode = "";

            //detail fields
            Decimal resultValue = 0;
            Decimal valDecimal = 0;
            String detailMessage = "";
            Int32 detailMessageStatus = 0;
            String resultValueString = "";

            Int32 detailDataSourceCrossRefMissingCount = 0;
            Int32 analyzerDataSourceTypeID = 0;
            Boolean analyzerDataSourceXrefExists = false;

            //12/24/20
            Boolean blnCombined = false;


            //error counts (missing cross references)
            _errorMaterial = 0;
            _errorOwner = 0;
            _errorOwnerLocation = 0;
            _errorSupplier = 0;
            _errorSupplierLocation = 0;

            Int32 headerPointer = 0;
            _dataSourceTypeID = 0;


            /****************
			 * Status and Message fields on header entity:
			 * 
			  ImportHeaderInfo:				
				 Int32 		Status  	   (Unassigned = 0, Accept = 1, Reject = 2, Warning = 3) 
				 String 	StatusDisplay - displays either “Accept” (status = 1) or “Reject” (status = 2) on grid 
				 Int32 		MessageStatus - uses enum SampleImportSpecificMessageDetail (Date Error, MissingMaterial, MissingXRef etc.) 
				 String 	Message 

			   In code:
				 headerStatus 				 - uses enum SampleImportStatus (accepted, cancelled, unassigned, warning)
				 headerSpecificMessageStatus - is used to go to specific x-ref screen and uses enum SampleImportSpecificMessageDetail 
															(DateError, MissingMaterial, MissingOwnerCrossRef, Unassigned, etc.)

			  Status = 3 (Warning) currently shows up if there is an error on detail, but the sample can still go thru
			 * 
			 ****************/


            var samplesToValidate = from g in workHeader
                                    select g;

            //--------------------------------------------------------------------------------
            //TODO: make this change -- to no longer insert from workHeader into importheader.  
            // Just use work header and set status and index -- then add if statement to check status before
            // going through validating the sample. If status = 0, sample shouldn't have to be validated again.
            // Replace the "foreach" statement below with the commented "for" statement -- and then use the workheader index [i].
            //            for (int i  = 0; i  < workHeader.Count; i ++)
            //            {
            //                workHeader[i].
            //            }
            //--------------------------------------------------------------------------------

            AIMGlobal.txtProgressMessage = "Validating import data ...";                                                                        //8/5/19       

            //9/9/20 - change to use AIMCoreGlobal
            //AIMGlobal.importProgressBarCounter = 0;                                                                                             //8/5/19       
            CFCAIMCore.Global.importProgressBarCounter = 0;                                                                                             //8/5/19       

            foreach (var item in samplesToValidate)
            {
                if (backgroundWorker.IsBusy == true)
                {
                    backgroundWorker.ReportProgress(0);
                }


                CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundPreview);                                           //8/5/19


                //12/24/20
                //Check to see if this import uses "Combined" Source and Source-Location. (The boolean field is then used below when checking cross-references)
                UserDefinedDataSourceTemplateInfo temp = new UserDefinedDataSourceTemplateInfo();
                temp = AIMManager.GetUserDefinedDataSourceTemplateMapColumnNameTop(defID, "SupplierLocationCombined");
                if (temp != null)
                {
                    if ((Int32)lueSource.EditValue == 0 || (Int32)lueSourceLocation.EditValue == 0)
                    {
                        blnCombined = true;
                    }

                }

                _dataSourceTypeID = GetDataSourceTypeID(defID);

                //----------------------------------------------------- 
                //CHECK RADIO BUTTON, TO CREATE OR MERGE SAMPLES   1 = Create; 2 = Merge
                #region Check Create or Merge
                _headerMessage = "";
                _headerStatus = (Int32)SampleImportStatus.Unassigned;
                _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.Unassigned;
                switch (importType)
                {

                    case (Int32)EnumImportType.AddNewSamplesAndResults:

                        if (item.SampleCodeExists == true)
                        {
                            _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists;
                            _headerMessage = "Sample already exists";
                        }
                        else
                        {
                            _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
                            _headerMessage = "Create New Sample";
                        }
                        break;


                    case (Int32)EnumImportType.UpdateResultsOnly:
                    case (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields:      //4/1/19            
                    case (Int32)EnumImportType.UpdateHeaderOnly:            //7/29/19                                   

                        if (item.SampleCodeExists == false)
                        {
                            _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist;
                            _headerMessage = "No Sample exists";

                            //-----------------------------------------------------------------							
                            //Add record to SampleImportLog
                            SampleImportLogInfo log = new SampleImportLogInfo();
                            Int32 logID = 0;
                            log.SampleImportLogID = 0;
                            log.SampleCode = item.SampleCode;
                            log.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            logID = AIMManager.SaveSampleImportLog(ref log).RecordID;

                        }
                        else
                        {
                            if (importType == (Int32)EnumImportType.UpdateResultsOnly)
                            {
                                _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MergeResults;
                                _headerMessage = "Add results";
                            }
                            else if (importType == (Int32)EnumImportType.UpdateHeaderOnly)
                            {
                                _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.UpdateHeaderFields;
                                _headerMessage = "Update Header fields";
                            }
                            else
                            {
                                _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.AddResultsAndUpdateHeaderFields;
                                _headerMessage = "Add results and Update Header fields";
                            }


                        }
                        break;


                    case (Int32)EnumImportType.AddNewSamplesAndResultsORUpdateResults:
                    case (Int32)EnumImportType.RomerResults:                                    //8/17/22
                        {

                            if (item.SampleCodeExists == true)
                            {
                                _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MergeResults;
                                _headerMessage = "Add results";
                            }
                            else
                            {
                                _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
                                _headerMessage = "Create New Sample";
                            }
                        }
                        break;

                    default:
                        break;
                }
                #endregion

                //-----------------------------------------------------				
                //VALIDATE SAMPLE DATE STRING
                #region Validate sample date
                if (item.SampleDateString == null && item.SampleDate != null)
                {
                    item.SampleDateString = item.SampleDate.ToString();
                }


                useCurrentDate = false;
                if (item.SampleDateString == "")
                {
                    useCurrentDate = true;
                    dtSampleDate = DateTime.Now;
                }
                if (useCurrentDate == false)
                {
                    if (CheckDate(item.SampleDateString) == true)
                    {
                        if (item.SampleDateString == "1/1/1700 12:00:00 AM")
                        {
                            dtSampleDate = DateTime.Parse("1700-01-01");
                            _headerMessage = "Date Error";
                            _headerStatus = (Int32)SampleImportStatus.Cancelled;
                            _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.DateError;
                        }
                        else
                        {
                            DateTime dt = DateTime.Parse(item.SampleDateString);
                            sampleDate = dt.ToString("yyyy-MM-dd");
                            dtSampleDate = dt;
                        }
                    }
                    else
                    {
                        dtSampleDate = DateTime.Parse("1700-01-01");
                        _headerMessage = "Date Error";
                        _headerStatus = (Int32)SampleImportStatus.Cancelled;
                        _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.DateError;
                    }
                }

                #endregion

                //-----------------------------------------------------				
                #region Check Import Type (1=Merge, 2=Create, 3=Merge and/or Create, 4=ResultsAndHeaderFields, 5=UpdateHeaderOnly)
                //-----------------------------------------------------
                //UPDATE RESULTS ONLY 
                if (importType == (Int32)EnumImportType.UpdateResultsOnly || importType == (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields)
                {
                    //2/15/21
                    //SetHeaderFieldsForResults(item.SampleCode, importType);
                    SetHeaderFieldsForResults(item.SampleCode, importType, _dataSourceTypeID);
                }
                else if (importType == (Int32)EnumImportType.UpdateHeaderOnly)
                {

                    //2/15/21
                    //SetHeaderFieldsForHeaderUpdateOnly(item.SampleCode);
                    SetHeaderFieldsForHeaderUpdateOnly(item.SampleCode, _dataSourceTypeID);
                }


                //-----------------------------------------------------
                //CREATE NEW SAMPLE WITH RESULTS
                else if (importType == (Int32)EnumImportType.AddNewSamplesAndResults)
                {
                    //---------------------------------------------------------------------------------------------
                    //POPULATE ANALYZER TABLES (from Analyzer export data)

                    //12/24/20 -- if blnCombined = true, the Analyzer Source/SourceLocation name is in the item.SourceLocationName
                    //PopulateUserDefinedTables(item.MaterialName, item.OwnerName, item.OwnerLocationName, item.SourceName, item.SourceLocationName);
                    PopulateUserDefinedTables(item.MaterialName, item.OwnerName, item.OwnerLocationName, item.SourceName, item.SourceLocationName, blnCombined);


                    //--------------------------------------------------------------------------------------------
                    //CHECK CROSS-REFERENCES - send AIM fields to function to see if an x-ref exists for the AIM field

                    //12/24/20
                    if (blnCombined == true)
                    {
                        //SupplierLocCombined xref
                        CheckSupplierLocCombinedCrossReference(item.SourceID, _analyzerSupplierLocationCombinedID, lueSourceLocation);
                    }
                    else
                    {
                        //Supplier and SupplierLoc xref
                        CheckSupplierLocCrossReference(item.SourceLocationID, lueSourceLocation);
                        CheckSupplierCrossReference(item.SourceID, lueSource);
                    }
                    CheckOwnerLocCrossReference(item.OwnerLocationID, lueOwnerLocation);
                    CheckOwnerCrossReference(item.OwnerID, lueOwner);
                    CheckMaterialCrossReference(item.MaterialID, lueMaterial);

                    //--------------------------------------------------------------------------------------------
                    //SET HEADER-MESSAGE if AIM required fields are missing
                    SetHeaderMessage(item.MaterialName, item.OwnerName, item.OwnerLocationName, item.SourceName, item.SourceLocationName,
                        lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, fromScreen);

                    //--------------------------------------------------------------------------------------------
                    //CHECK Override if Material/Owner/OwnerLocation/Supplier/SupplierLocation are REJECTED					
                    CheckOverride();

                }

                //-----------------------------------------------------
                //UPDATE RESULTS IF SAMPLE EXISTS OR CREATE NEW SAMPLE WITH RESULTS    
                // (Romer samples will fall into this area)
                else
                {
                    if (item.SampleCodeExists == true)
                    {
                        //2/15/21
                        //SetHeaderFieldsForResults(item.SampleCode, importType);
                        SetHeaderFieldsForResults(item.SampleCode, importType, _dataSourceTypeID);
                    }
                    else
                    {
                        #region Populate user-defined tables

                        //12/24/20
                        //PopulateUserDefinedTables(item.MaterialName, item.OwnerName, item.OwnerLocationName, item.SourceName, item.SourceLocationName);
                        PopulateUserDefinedTables(item.MaterialName, item.OwnerName, item.OwnerLocationName, item.SourceName, item.SourceLocationName, blnCombined);

                        #endregion
                        #region Check Cross-References - send AIM fields to function to see if an x-ref exists for the AIM field

                        //12/24/20
                        if (blnCombined == true)
                        {
                            #region SupplierLocCombined xref
                            CheckSupplierLocCombinedCrossReference(item.SourceID, item.SourceLocationID, lueSourceLocation);
                            #endregion
                        }
                        else
                        {

                            CheckSupplierLocCrossReference(item.SourceLocationID, lueSourceLocation);
                            CheckSupplierCrossReference(item.SourceID, lueSource);
                        }
                        CheckOwnerLocCrossReference(item.OwnerLocationID, lueOwnerLocation);
                        CheckOwnerCrossReference(item.OwnerID, lueOwner);
                        CheckMaterialCrossReference(item.MaterialID, lueMaterial);
                        #endregion
                        #region Set Header-Message if AIM required fields are missing
                        SetHeaderMessage(item.MaterialName, item.OwnerName, item.OwnerLocationName, item.SourceName, item.SourceLocationName,
                            lueMaterial, lueOwner, lueOwnerLocation, lueSource, lueSourceLocation, fromScreen);
                        #endregion
                        #region Override if Material/Owner/OwnerLocation/Supplier/SupplierLocation are REJECTED					                        
                        CheckOverride();
                        #endregion

                    }
                }
                #endregion

                allowAdd = true;

                //-----------------------------------------------------
                //ON IMPORTS, CHECK TO SEE IF THERE ARE DUPLICATES FOR SAMPLE-CODE
                if (fromScreen == "Import")
                {
                    if (importType == (Int32)EnumImportType.AddNewSamplesAndResults && allowDuplicates == false)
                    {
                        Boolean exists = false;

                        //2/15/21 - added _dataSourceTypeID because, when the system creates sample codes, it uses the DataSourceTypeID 
                        //          and then finds the last samplecode and adds 1. So, when checking for existing sample codes below, 
                        //          datasourcetypeID is needed.
                        //exists = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsForDataSource(item.SampleCode);
                        exists = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsForDataSource(item.SampleCode, _dataSourceTypeID);

                        if (exists == true)
                        {
                            allowAdd = false;
                        }
                    }
                }

                //-----------------------------------------------------
                //Added the following so that if user selects "Add Results" and there is no existing sample,
                //		the owner, owner-location, supplier and supplier-location will show blanks; otherwise 
                //		the correct values will show up.
                                
                switch (importType)
                {
                    case (Int32)EnumImportType.UpdateResultsOnly:
                    case (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields:
                    case (Int32)EnumImportType.UpdateHeaderOnly:
                        _owner = "";
                        _ownerLocation = "";
                        _supplier = "";
                        _supplierLocation = "";

                        //12/22/20 - added check of OwnerName, OwnerlocationName, etc., below

                        if (item.OwnerID != 0 || item.OwnerName != "")
                            _owner = item.OwnerName;
                        if (item.OwnerLocationID != 0 || item.OwnerLocationName != "")
                            _ownerLocation = item.OwnerLocationName;
                        if (item.SourceID != 0 || item.SourceName != "")
                            _supplier = item.SourceName;
                        if (item.SourceLocationID != 0 || item.SourceLocationName != "")
                            _supplierLocation = item.SourceLocationName;
                        break;

                    case (Int32)EnumImportType.AddNewSamplesAndResultsORUpdateResults:

                    //8/17/22
                    case (Int32)EnumImportType.RomerResults:

                        if (item.SampleCodeExists == true)
                        {
                            if (item.OwnerName != "" || item.OwnerName != "")
                                _owner = item.OwnerName;
                            if (item.OwnerLocationName != "" || item.OwnerLocationName != "")
                                _ownerLocation = item.OwnerLocationName;
                            if (item.SourceName != "" || item.SourceName != "")
                                _supplier = item.SourceName;
                            if (item.SourceLocationName != "" || item.SourceLocationName != "")
                                _supplierLocation = item.SourceLocationName;
                        }
                        break;
                    default:
                        break;
                }

                //-----------------------------------------------------							
                if (allowAdd == true)
                {
                    //ADD HEADER RECORD	
                    #region Add Header Record
                    importHeader.Add(new ImportHeaderInfo
                    {
                        ImportHeaderID = item.ImportHeaderID,
                        SampleCode = item.SampleCode,
                        AlternateCode = item.AlternateCode,
                        SampleDate = dtSampleDate,
                        InvoiceNumber = item.InvoiceNumber,
                        PONumber = item.PONumber,
                        TicketNumber = item.TicketNumber,
                        VehicleNumber = item.VehicleNumber,
                        BillOfLading = item.BillOfLading,
                        ContractNumber = item.ContractNumber,
                        LoadNumber = item.LoadNumber,
                        LotNumber = item.LotNumber,
                        Notes = item.Notes,

                        MaterialName = item.MaterialName,
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
                        MessageStatus = _headerSpecificMessageStatus,
                        Status = _headerStatus,

                        ImportSourceFileName = item.ImportSourceFileName,
                        SampleCodeExists = item.SampleCodeExists,
                        ExistingCode = item.ExistingCode,
                        ExistingDate = item.ExistingDate,
                        ExistingImportHeaderID = item.ExistingImportHeaderID

                        ,
                        AnalyzerMaterialID = _analyzerMaterialID
                        ,
                        AnalyzerOwnerID = _analyzerOwnerID
                        ,
                        AnalyzerOwnerLocationID = _analyzerOwnerLocationID
                        ,
                        UserDefinedSupplierID = _analyzerSupplierID
                        ,
                        UserDefinedSupplierLocationID = _analyzerSupplierLocationID

                        //12/28/20
                        ,
                        SupplierLocationCombinedID = _analyzerSupplierLocationCombinedID

                        ,
                        XrefMaterialStatus = _xrefMaterialStatus
                        ,
                        XrefOwnerStatus = _xrefOwnerStatus
                        ,
                        XrefOwnerLocationStatus = _xrefOwnerLocationStatus
                        ,
                        XrefSupplierStatus = _xrefSupplierStatus
                        ,
                        XrefSupplierLocationStatus = _xrefSupplierLocationStatus

                        ,
                        ActiveMaterialStatus = _activeMaterialStatus
                        ,
                        ActiveOwnerStatus = _activeOwnerStatus
                        ,
                        ActiveOwnerLocationStatus = _activeOwnerLocationStatus
                        ,
                        ActiveSupplierStatus = _activeSupplierStatus
                        ,
                        ActiveSupplierLocationStatus = _activeSupplierLocationStatus

                        ,
                        MaterialCode = _analyzerMaterialCode
                        ,
                        OwnerCode = _analyzerOwnerLocationCode
                        ,
                        OwnerLocationCode = _analyzerOwnerLocationCode
                        ,
                        SourceCode = _analyzerSupplierCode
                        ,
                        SourceLocationCode = _analyzerSupplierLocationCode

                    });
                    #endregion

                    //--------------------------------------------------
                    //SET DETAIL POINTER AND MESSAGES
                    if (importHeader[headerPointer].DetailImport == null)
                    {
                        importHeader[headerPointer].DetailImport = new List<ImportDetailInfo>();
                    }

                    //VALIDATE and ADD DETAIL RECORD
                    #region Add Detail

                    if (item.DetailImport != null)
                    {
                        for (int i = 0; i < item.DetailImport.Count; i++)
                        {
                            //If datasource is mapped, make sure a DataSource DETAIL record exists for the Analyzer datasource
                            analyzerDataSourceTypeID = 0;
                            analyzerDataSourceXrefExists = false;
                            detailDataSourceCrossRefMissingCount = 0;

                            if (datasourceIsMapped == true)
                            {
                                ErrorInfo xref = AIMManager.GetDataSourceHeaderDetail(item.DetailImport[i].AnalyzerDataSourceTypeName);
                                if (xref.RecordID != 0)
                                {
                                    analyzerDataSourceTypeID = xref.RecordID;
                                    analyzerDataSourceXrefExists = true;
                                }
                                else
                                {
                                    //4/6/17 - insert datasource into AnalyzerDataSource table
                                    AnalyzerDataSourceTypeInfo data = new AnalyzerDataSourceTypeInfo();
                                    data.AnalyzerDataSourceTypeID = 0;
                                    data.AnalyzerDataSourceTypeName = item.DetailImport[i].AnalyzerDataSourceTypeName;

                                    analyzerDataSourceCode = item.DetailImport[i].AnalyzerDataSourceTypeName;
                                    if (analyzerDataSourceCode.Length > 20)
                                    {
                                        analyzerDataSourceCode = item.DetailImport[i].AnalyzerDataSourceTypeName.Substring(0, 20);
                                    }
                                    data.AnalyzerDataSourceTypeCode = analyzerDataSourceCode;

                                    data.DataSourceTypeID = _dataSourceTypeID;
                                    data.IsActive = true;
                                    data.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                                    ErrorInfo error = new ErrorInfo();
                                    error = AIMManager.SaveAnalyzerDataSource(ref data, false);
                                    analyzerDataSourceTypeID = error.RecordID;

                                    detailDataSourceCrossRefMissingCount++;
                                }

                            }

                            detailMessageStatus = 0;
                            detailMessage = "";

                            //----------------------------------------------
                            if (item.DetailImport[i].AnalyzerNutrientCode.ToString().Length == 0)
                            {
                                detailMessage = "Missing nutrient";
                                detailMessageStatus = (Int32)SampleImportStatus.Warning;
                            }


                            //----------------------------------------------
                            if (datasourceIsMapped == true && item.DetailImport[i].AnalyzerDataSourceTypeName.ToString().Length == 0)
                            {
                                detailMessage = "Missing DataSource";
                                detailMessageStatus = (Int32)SampleImportStatus.Warning;

                            }
                            else if (datasourceIsMapped == true && analyzerDataSourceXrefExists == false)
                            {
                                detailMessage = "Missing Cross-Reference";
                                detailMessageStatus = (Int32)SampleImportStatus.Warning;
                            }

                            //----------------------------------------------
                            if (item.DetailImport[i].ResultValueString == null && CheckDecimal(item.DetailImport[i].ResultValue.ToString()) == true)
                            {
                                Decimal.TryParse(item.DetailImport[i].ResultValueString, out valDecimal);
                                resultValue = valDecimal;
                                resultValueString = null;
                            }
                            else if (CheckDecimal(item.DetailImport[i].ResultValueString) == true)
                            {

                                Decimal.TryParse(item.DetailImport[i].ResultValueString, out valDecimal);
                                resultValue = valDecimal;
                                resultValueString = item.DetailImport[i].ResultValueString;
                            }
                            else
                            {
                                resultValue = 0;
                                resultValueString = "";
                                detailMessage = "Decimal value error";
                                detailMessageStatus = (Int32)SampleImportStatus.Warning;
                            }

                           
                            //----------------------------------------------
                            //ADD DETAIL
                            importHeader[headerPointer].DetailImport.Add(new ImportDetailInfo
                            {
                                ImportDetailID = item.DetailImport[i].ImportDetailID,
                                ImportHeaderID = item.DetailImport[i].ImportHeaderID,
                                AnalyzerNutrientCode = item.DetailImport[i].AnalyzerNutrientCode,
                                ResultValue = resultValue,
                                ResultValueString = resultValueString,
                                UnitType = item.DetailImport[i].UnitType,
                                DetailMessage = detailMessage,
                                DetailMessageStatus = detailMessageStatus,
                                AnalyzerDataSourceTypeName = item.DetailImport[i].AnalyzerDataSourceTypeName,
                                AnalyzerDataSourceTypeID = analyzerDataSourceTypeID,
                                XrefDatasourceExists = analyzerDataSourceXrefExists,

                                //8/15/22
                                SQLResultIDInfo = item.DetailImport[i].SQLResultIDInfo

                            });
                        }
                    }
                    #endregion

                    //-----------------------------------------------------
                    //Add detail-message count -- if there are messages on the detail (like 'decimal error' or 'missing nutrient')
                    //								then a "*" will show on the grid from the sample, indicating something wasn't
                    //								quite right with the detail.  But user can still import sample.
                    #region Add Detail-Message Count
                    var results = from g in importHeader[headerPointer].DetailImport
                                  where g.DetailMessageStatus > 0
                                  select new
                                  {
                                      g.ImportHeaderID

                                  };
                    //update count field on import header 
                    if (results.Count() > 0)
                    {
                        var sampleNumber = from g in importHeader
                                           where g.ImportHeaderID == item.ImportHeaderID
                                           select g;

                        foreach (var samp in sampleNumber)
                        {
                            samp.DetailMessageErrorTotalCount = results.Count();
                        }
                    }
                    #endregion

                    #region Check Detail datasource missing counts; update import-header
                    if (datasourceIsMapped == true)
                    {
                        if (detailDataSourceCrossRefMissingCount > 0)
                        {
                            var sampleNumber = from g in importHeader
                                               where g.ImportHeaderID == item.ImportHeaderID
                                               select g;

                            foreach (var samp in sampleNumber)
                            {
                                samp.DetailDataSourceCrossRefMissingCount = results.Count();
                                samp.Message = "Missing Cross-Reference";
                                samp.MessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingDatasourceCrossReference;
                                samp.Status = (Int32)SampleImportStatus.Cancelled;

                            }
                        }
                    }
                    #endregion


                    //-----------------------------------------------------

                    _xrefMaterialStatus = 0;
                    _xrefOwnerStatus = 0;
                    _xrefOwnerLocationStatus = 0;
                    _xrefSupplierStatus = 0;
                    _xrefSupplierLocationStatus = 0;

                    _activeMaterialStatus = 0;
                    _activeOwnerStatus = 0;
                    _activeOwnerLocationStatus = 0;
                    _activeSupplierStatus = 0;
                    _activeSupplierLocationStatus = 0;

                    headerPointer++;

                }

                if (AIMGlobal.stopPreviewingImport == true)
                {
                    AIMGlobal.stopPreviewingImport = false;
                    break;
                }
            }

            return importHeader;
        }

        public static Int32 CheckAimRequiredDropdowns(List<UserDefinedDataSourceTemplateInfo> userTemplate, DevExpress.XtraEditors.LookUpEdit[] lueName, string[] colName)
        {
            //3/2/21 commented, not used
            //Boolean supplierExists = false;
            //Boolean supplierLocationExists = false;

            Int32 rtn = 0;

            //IF AIM REQUIRED FIELD DROPDOWN VALUE = "USE MAPPING", MAKE SURE COLUMN EXISTS ON SPREADSHEET			
            for (int a = 1; a < 5; a++)  //--> changed this from 0 to 1 to skip the Material dropdown (because it is hidden right now. This could change later.)
            {
                if (lueName[a].Text == "Use Mapping")
                {
                    //search user template for MapColumnName = colName[a]
                    Boolean blnMapRequiredFieldsMissing = true;
                    for (int d = 0; d < userTemplate.Count; d++)
                    {

                        //if (userTemplate[d].MapColumnName == colName[a])
                        //{
                        //	blnMapRequiredFieldsMissing = false;
                        //	break;
                        //}
                        switch (colName[a])
                        {
                            case "Material":
                            case "Owner":
                            case "OwnerLocation":
                                if (userTemplate[d].MapColumnName == colName[a])
                                {
                                    blnMapRequiredFieldsMissing = false;
                                    break;
                                }
                                break;
                            case "Supplier":
                                if (userTemplate[d].MapColumnName == colName[a] || userTemplate[d].MapColumnName == "SupplierLocationCombined")
                                {
                                    blnMapRequiredFieldsMissing = false;
                                    //supplierExists = true;                //3/2/21 commented, not used
                                    break;
                                }

                                //12/23/20 - commented - why are we doing this?
                                //else if (supplierLocationExists == true)
                                //{
                                //	blnMapRequiredFieldsMissing = false;
                                //	break;
                                //}

                                break;
                            case "SupplierLocation":
                                if (userTemplate[d].MapColumnName == colName[a] || userTemplate[d].MapColumnName == "SupplierLocationCombined")
                                {
                                    blnMapRequiredFieldsMissing = false;
                                    //supplierLocationExists = true;        //3/2/21 commented, not used
                                    break;
                                }

                                //12/23/20 - commented - why are we doing this?
                                //else if (supplierExists == true)
                                //{
                                //    blnMapRequiredFieldsMissing = false;
                                //    break;
                                //}

                                break;
                            default:
                                break;
                        }
                    }



                    if (blnMapRequiredFieldsMissing == true)
                    {
                        rtn = -1;
                    }
                }
            }

            return rtn;
        }
        public static Int32 CheckOwnerMapping(Int32 defID)
        {
            Int32 rtn = 0;

            //IF AIM-OWNER IS MAPPED, AIM-OWNER-LOCATION MUST ALSO BE MAPPED

            //ErrorInfo owner = AIMManager.GetUserDefinedDataSourceTemplateCount(defID, "SampleOwner");
            ErrorInfo owner = AIMManager.GetUserDefinedDataSourceTemplateCount(defID, "Owner");

            if (owner.RecordID > 0)
            {
                ErrorInfo ownerloc = AIMManager.GetUserDefinedDataSourceTemplateCount(defID, "OwnerLocation");
                if (ownerloc.RecordID == 0)
                {
                    rtn = -1;
                }
            }
            return rtn;
        }
        public static Int32 CheckSupplierMapping(Int32 defID)
        {
            Int32 rtn = 0;

            //IF AIM-SUPPLIER IS MAPPED, AIM-SUPPLIER-LOCATION MUST ALSO BE MAPPED
            ErrorInfo source = AIMManager.GetUserDefinedDataSourceTemplateCount(defID, "Source");
            if (source.RecordID > 0)
            {
                ErrorInfo sourceloc = AIMManager.GetUserDefinedDataSourceTemplateCount(defID, "SourceLocation");
                if (sourceloc.RecordID == 0)
                {
                    rtn = -1;
                }
            }
            return rtn;
        }
        public static string SetRequiredField(DevExpress.XtraEditors.LookUpEdit lue, String val, String fromScreen)
        {

            String rtn = "";

            if (fromScreen == "Import")
            {
                if (lue.Enabled == true)
                {
                    rtn = lue.Text.ToString();
                }
                else if (val != "")
                {
                    rtn = val;
                }
                else
                {
                    rtn = "MISSING";
                }
            }
            else
            {
                if (val != "")
                {
                    rtn = val;
                }
                else
                {
                    rtn = "MISSING";
                }
            }
            return rtn;
        }
        public static Int32 CheckRequiredFieldForMapping(Int32 defID, String field, String val, Int32 saveID)
        {
            return 0;
        }
        public static String PopulateAnalyzerCodes(String field, Int32 ID)
        {
            String rtn = "";
            switch (field)
            {
                case "Material":
                    List<AnalyzerMaterialInfo> material = new List<AnalyzerMaterialInfo>();
                    material = AIMManager.GetAnalyzerMaterial_AnalyzerMaterialID(ID);
                    if (material != null)
                    {
                        if (material.Count != 0)
                        {
                            rtn = material[0].AnalyzerMaterialCode;
                        }
                    }
                    break;
                case "Owner":
                    List<AnalyzerOwnerInfo> owner = new List<AnalyzerOwnerInfo>();
                    owner = AIMManager.GetAnalyzerOwner_OwnerID(ID);

                    if (owner != null)
                    {
                        if (owner.Count != 0)
                        {
                            rtn = owner[0].AnalyzerOwnerCode;
                        }
                    }
                    break;
                case "OwnerLocation":
                    List<AnalyzerOwnerLocationInfo> ownerLocation = new List<AnalyzerOwnerLocationInfo>();
                    ownerLocation = AIMManager.GetAnalyzerOwnerLocation_OwnerLocationID(ID);
                    if (ownerLocation != null)
                    {
                        if (ownerLocation.Count != 0)
                        {
                            rtn = ownerLocation[0].AnalyzerOwnerLocationCode;
                        }
                    }
                    break;

                case "Source":
                    List<AnalyzerSupplierInfo> source = new List<AnalyzerSupplierInfo>();
                    source = AIMManager.GetAnalyzerSupplier_SupplierID(ID);

                    if (source != null)
                    {
                        if (source.Count != 0)
                        {
                            rtn = source[0].AnalyzerSupplierCode;
                        }
                    }
                    break;

                case "SourceLocation":
                    List<AnalyzerSupplierLocationInfo> supplierLocation = new List<AnalyzerSupplierLocationInfo>();
                    supplierLocation = AIMManager.GetAnalyzerSupplierLocation_SupplierLocationID(ID);
                    if (supplierLocation != null)
                    {
                        if (supplierLocation.Count != 0)
                        {
                            rtn = supplierLocation[0].AnalyzerSupplierLocationCode;
                        }
                    }
                    break;

                default:
                    break;
            }

            return rtn;
        }
        public static Int32 PopulateAnalyzerTables(Int32 dataSourceTypeID, String field, String val, Int32 saveID)
        {
            Int32 returnRecordID = 0;
            if ((val == "" && saveID == 0) || (val == "MISSING" && saveID == 0))
            {
                return 0;
            }

            ErrorInfo error = new ErrorInfo();
            switch (field)
            {
                case "Material":
                    AnalyzerMaterialInfo materialExists = new AnalyzerMaterialInfo();
                    DataSourceTypeEnum dataSource = (DataSourceTypeEnum)dataSourceTypeID;
                    materialExists = AIMManager.GetAnalyzerMaterial((Int32)dataSource, val);
                    if (materialExists == null)
                    {
                        AnalyzerMaterialInfo material = new AnalyzerMaterialInfo();
                        material.AnalyzerMaterialID = 0;
                        material.AnalyzerMaterialCode = val;
                        material.AnalyzerMaterialName = val;
                        material.DataSourceTypeID = dataSourceTypeID;
                        material.IsActive = true;
                        error = AIMManager.SaveAnalyzerMaterial(ref material, false);
                        returnRecordID = error.RecordID;
                    }
                    else
                    {
                        returnRecordID = materialExists.AnalyzerMaterialID;
                    }

                    break;
                case "Owner":
                    List<AnalyzerOwnerInfo> ownerExists = new List<AnalyzerOwnerInfo>();
                    ownerExists = AIMManager.GetAnalyzerOwner_DataSourceType_OwnerName(dataSourceTypeID, val);
                    if (ownerExists.Count == 0)
                    {
                        AnalyzerOwnerInfo owner = new AnalyzerOwnerInfo();
                        owner.AnalyzerOwnerID = 0;
                        owner.AnalyzerOwnerCode = val;
                        owner.AnalyzerOwnerName = val;
                        owner.DataSourceTypeID = dataSourceTypeID;
                        owner.IsActive = true;
                        error = AIMManager.SaveAnalyzerOwner(ref owner, false);

                        returnRecordID = error.RecordID;
                    }
                    else
                    {
                        returnRecordID = ownerExists[0].AnalyzerOwnerID;
                    }
                    break;
                case "OwnerLocation":
                    List<AnalyzerOwnerLocationInfo> ownerLocationExists = new List<AnalyzerOwnerLocationInfo>();
                    ownerLocationExists = AIMManager.GetAnalyzerOwnerLocationNameList(dataSourceTypeID, saveID, val);
                    if (ownerLocationExists.Count == 0)
                    {
                        AnalyzerOwnerLocationInfo ownerLocation = new AnalyzerOwnerLocationInfo();
                        ownerLocation.AnalyzerOwnerLocationID = 0;
                        ownerLocation.AnalyzerOwnerID = saveID;
                        ownerLocation.AnalyzerOwnerLocationCode = val;
                        ownerLocation.AnalyzerOwnerLocationName = val;
                        ownerLocation.DataSourceTypeID = dataSourceTypeID;
                        ownerLocation.IsActive = true;
                        error = AIMManager.SaveAnalyzerOwnerLocation(ref ownerLocation, false);
                        returnRecordID = error.RecordID;
                    }
                    else
                    {
                        returnRecordID = ownerLocationExists[0].AnalyzerOwnerLocationID;
                    }
                    break;

                case "Source":
                    List<AnalyzerSupplierInfo> supplierExists = new List<AnalyzerSupplierInfo>();
                    supplierExists = AIMManager.GetAnalyzerSupplier_DataSourceType_SupplierName(dataSourceTypeID, val);
                    if (supplierExists.Count == 0)
                    {
                        AnalyzerSupplierInfo supplier = new AnalyzerSupplierInfo();
                        supplier.AnalyzerSupplierID = 0;
                        supplier.AnalyzerSupplierCode = val;
                        supplier.AnalyzerSupplierName = val;
                        supplier.DataSourceTypeID = dataSourceTypeID;
                        supplier.IsActive = true;
                        error = AIMManager.SaveAnalyzerSupplier(ref supplier, false);
                        returnRecordID = error.RecordID;
                    }
                    else
                    {
                        returnRecordID = supplierExists[0].AnalyzerSupplierID;
                    }
                    break;
                case "SourceLocation":
                    List<AnalyzerSupplierLocationInfo> supplierLocationExists = new List<AnalyzerSupplierLocationInfo>();
                    supplierLocationExists = AIMManager.GetAnalyzerSupplierLocationNameList(dataSourceTypeID, saveID, val);

                    if (supplierLocationExists.Count == 0)
                    {
                        AnalyzerSupplierLocationInfo supplierLocation = new AnalyzerSupplierLocationInfo();
                        supplierLocation.AnalyzerSupplierLocationID = 0;
                        supplierLocation.AnalyzerSupplierID = saveID;
                        supplierLocation.AnalyzerSupplierLocationCode = val;
                        supplierLocation.AnalyzerSupplierLocationName = val;
                        supplierLocation.DataSourceTypeID = dataSourceTypeID;
                        supplierLocation.IsActive = true;
                        error = AIMManager.SaveAnalyzerSupplierLocation(ref supplierLocation, false);
                        returnRecordID = error.RecordID;
                    }
                    else
                    {
                        returnRecordID = supplierLocationExists[0].AnalyzerSupplierLocationID;
                    }
                    break;

                case "SupplierLocationCombined":
                    List<AnalyzerSupplierLocationCombinedInfo> supplierCombinedExists = new List<AnalyzerSupplierLocationCombinedInfo>();
                    supplierCombinedExists = AIMManager.GetAnalyzerSupplierLocationCombinedList(dataSourceTypeID, val);

                    if (supplierCombinedExists.Count == 0)
                    {
                        AnalyzerSupplierLocationCombinedInfo supplierCombined = new AnalyzerSupplierLocationCombinedInfo();
                        supplierCombined.AnalyzerSupplierLocationCombinedID = 0;
                        supplierCombined.AnalyzerSupplierLocationCombinedName = val;
                        supplierCombined.DataSourceTypeID = dataSourceTypeID;
                        supplierCombined.IsActive = true;
                        error = AIMManager.SaveAnalyzerSupplierLocationCombined(ref supplierCombined, false);
                        returnRecordID = error.RecordID;
                    }
                    else
                    {
                        returnRecordID = supplierCombinedExists[0].AnalyzerSupplierLocationCombinedID;
                    }
                    break;
                default:
                    break;
            }
            return returnRecordID;

        }

        //12/28/20 - added this just to return the AIMSupplierID for Combined Supplier Locations
        public static Int32 CheckCrossReferenceAndReturnCombinedSupplier(String field, Int32 dataSourceTypeID, Int32 ID)
        {
            Int32 returnRecordID = 0;

            ErrorInfo error = new ErrorInfo();
            List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> xref = new List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>();
            xref = AIMManager.GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(dataSourceTypeID, ID);
            if (xref != null && xref.Count != 0)
            {
                returnRecordID = xref[0].AIMSupplierID;
            }
            return returnRecordID;
        }

        public static Int32 CheckCrossReference(String field, Int32 dataSourceTypeID, Int32 ID, Int32 IDSecond)
        {

            Int32 returnRecordID = 0;

            ErrorInfo error = new ErrorInfo();
            switch (field)
            {
                case "Material":
                    List<AIMMaterialRefAnalyzerMaterialInfo> materialXRef = new List<AIMMaterialRefAnalyzerMaterialInfo>();
                    materialXRef = AIMManager.GetAIMMaterialRefAnalyzerMaterialList(ID);
                    if (materialXRef != null && materialXRef.Count != 0)
                    {
                        returnRecordID = materialXRef[0].AIMMaterialID;
                    }
                    break;
                case "Owner":
                    List<AIMOwnerRefAnalyzerOwnerInfo> ownerXref = new List<AIMOwnerRefAnalyzerOwnerInfo>();
                    ownerXref = AIMManager.GetAIMOwnerRefAnalyzerOwnerDataSourceTypeIDAnalyzerOwnerID(dataSourceTypeID, ID);
                    if (ownerXref != null && ownerXref.Count != 0)
                    {
                        returnRecordID = ownerXref[0].AIMOwnerID;
                    }
                    break;
                case "OwnerLocation":
                    List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> ownerLocationXref = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
                    ownerLocationXref = AIMManager.GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeIDAnalyzerOwnerLocationID(dataSourceTypeID, ID, IDSecond);
                    if (ownerLocationXref != null && ownerLocationXref.Count != 0)
                    {
                        returnRecordID = ownerLocationXref[0].AIMOwnerLocationID;
                    }
                    break;
                case "Source":
                    List<AIMSupplierRefAnalyzerSupplierInfo> supplierXref = new List<AIMSupplierRefAnalyzerSupplierInfo>();
                    supplierXref = AIMManager.GetAnalyzerCrossRefAimSupplierDataSourceTypeIDAnalyzerSupplierID(dataSourceTypeID, ID);
                    if (supplierXref != null && supplierXref.Count != 0)
                    {
                        returnRecordID = supplierXref[0].AIMSupplierID;
                    }
                    break;

                case "SourceLocation":
                    List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> supplierLocationXref = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
                    supplierLocationXref = AIMManager.GetAnalyzerCrossRefAimSupplierLocationDataSourceTypeIDAnalyzerSupplierLocationID(dataSourceTypeID, ID, IDSecond);
                    if (supplierLocationXref != null && supplierLocationXref.Count != 0)
                    {
                        returnRecordID = supplierLocationXref[0].AIMSupplierLocationID;
                    }
                    break;


                //12/24/20
                case "SupplierLocationCombined":
                    List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> xref = new List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>();
                    xref = AIMManager.GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(dataSourceTypeID, ID);
                    if (xref != null && xref.Count != 0)
                    {
                        returnRecordID = xref[0].AIMSupplierLocationID;
                    }
                    break;
                default:
                    break;
            }
            return returnRecordID;

        }


        public static Int32 AnalyzerCheckActive(String field, Int32 dataSourceTypeID, Int32 ID, Int32 IDSecond)
        {

            Int32 returnRecordID = 0;

            ErrorInfo error = new ErrorInfo();
            switch (field)
            {
                case "Material":
                    List<AnalyzerMaterialInfo> material = new List<AnalyzerMaterialInfo>();
                    material = AIMManager.GetAnalyzerMaterial_MaterialID_DataSourceTypeID_IsActive(ID, dataSourceTypeID, 1);
                    if (material != null && material.Count != 0)
                    {
                        returnRecordID = material[0].AnalyzerMaterialID;
                    }
                    break;
                case "Owner":
                    List<AnalyzerOwnerInfo> owner = new List<AnalyzerOwnerInfo>();
                    owner = AIMManager.GetAnalyzerOwner_OwnerID_DataSourceTypeID_IsActive(ID, dataSourceTypeID, 1);
                    if (owner != null && owner.Count != 0)
                    {
                        returnRecordID = owner[0].AnalyzerOwnerID;
                    }
                    break;
                case "OwnerLocation":
                    List<AnalyzerOwnerLocationInfo> ownerLocation = new List<AnalyzerOwnerLocationInfo>();
                    ownerLocation = AIMManager.GetAnalyzerOwnerLocation(ID, dataSourceTypeID, 1);

                    if (ownerLocation != null && ownerLocation.Count != 0)
                    {
                        returnRecordID = ownerLocation[0].AnalyzerOwnerLocationID;
                    }
                    break;
                case "Supplier":
                    List<AnalyzerSupplierInfo> supplier = new List<AnalyzerSupplierInfo>();
                    supplier = AIMManager.GetAnalyzerSupplier(ID, dataSourceTypeID, 1);

                    if (supplier != null && supplier.Count != 0)
                    {
                        returnRecordID = supplier[0].AnalyzerSupplierID;
                    }
                    break;
                case "SupplierLocation":
                    List<AnalyzerSupplierLocationInfo> supplierLocation = new List<AnalyzerSupplierLocationInfo>();
                    supplierLocation = AIMManager.GetAnalyzerSupplierLocation(ID, dataSourceTypeID, 1);

                    if (supplierLocation != null && supplierLocation.Count != 0)
                    {
                        returnRecordID = supplierLocation[0].AnalyzerSupplierLocationID;
                    }
                    break;

                //12/24/20
                case "SupplierLocationCombined":
                    List<AnalyzerSupplierLocationCombinedInfo> combined = new List<AnalyzerSupplierLocationCombinedInfo>();
                    combined = AIMManager.GetAnalyzerSupplierLocationCombined(ID, dataSourceTypeID, 1);

                    if (combined != null && combined.Count != 0)
                    {
                        returnRecordID = combined[0].AnalyzerSupplierLocationCombinedID;
                    }
                    break;
                default:
                    break;
            }
            return returnRecordID;

        }
        public static DateTime? ConvertOLEStringToDate(String date)
        {
            try
            {
                double dbt = double.Parse(date);
                DateTime dtconv = DateTime.FromOADate(dbt);
                String strDtConv = dtconv.ToString();
                DateTime dt = DateTime.Parse(strDtConv);
                return dt;
            }
            catch
            {
                return null;
            }

        }

        public static DateTime CheckDateFormat(String dateString, Int32 dateFormatID)
        {
            String format = "d";
            DateTime result;
            CultureInfo provider = CultureInfo.InvariantCulture;

            try
            {
                DateFormatInfo dateFormat = new DateFormatInfo();


                dateFormat = AIMManager.GetDateFormat(dateFormatID);
                if (dateFormat != null)
                {
                    if (dateFormat.FormatParameter != null)
                    {
                        format = dateFormat.FormatParameter;
                    }
                    if (dateFormat.Culture != null)
                    {
                        //6/20/19
                        if (dateFormat.Culture.ToString() != "NULL")
                        {
                            provider = new CultureInfo(dateFormat.Culture.ToString());
                        }

                    }
                }

                if (provider.Name != "")
                {
                    try
                    {
                        //4/26/17 - added try/catch here so that if a Euro-date format is selected as the dateformat, but
                        //			the import-date is in a format OTHER than Euro, a good date will be generated.
                        //			However, there may be some instances when the value won't be a good date, so then 
                        //			the date is set to the current date.
                        result = DateTime.ParseExact(dateString, format, provider);
                    }
                    catch (Exception)
                    {
                        DateTime valDateTime;
                        if (DateTime.TryParse(dateString, out valDateTime) == true)
                        {
                            result = Convert.ToDateTime(dateString);
                        }
                        else
                        {
                            result = DateTime.Today;
                        }
                    }
                }
                else
                {
                    try
                    {
                        result = Convert.ToDateTime(dateString);
                    }
                    catch (Exception)
                    {
                        result = DateTime.Today;
                    }

                }
                return result;


            }
            catch
            {
                return DateTime.Today;
            }
        }

        public static bool CheckOLEDate(String date)
        {
            try
            {
                double dbt = double.Parse(date);
                DateTime dtconv = DateTime.FromOADate(dbt);
                String strDtConv = dtconv.ToString();
                DateTime dt = DateTime.Parse(strDtConv);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckDecimal(String val)
        {
            try
            {
                decimal valDecimal = 0;
                Decimal.TryParse(val, out valDecimal);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static Int32 GetDataSourceTypeID(Int32 defID)
        {
            Int32 rtn = 0;

            try
            {
                UserDefinedDataSourceDefinitionInfo dataSource = new UserDefinedDataSourceDefinitionInfo();
                dataSource = AIMManager.GetDataSourceUserDefinition(defID);
                if (dataSource != null)
                {
                    rtn = dataSource.DataSourceTypeID;
                }
            }
            catch (Exception)
            {
                rtn = 0;
                throw;
            }

            return rtn;
        }

        //check here 2
        //11/18/21
        //public static Int32 CheckAutoGenerateSampleCode(List<UserDefinedDataSourceTemplateInfo> userTemplate)
        public static Int32 CheckAutoGenerateSampleCode(List<UserDefinedDataSourceTemplateInfo> userTemplate, Int32 defID)

        {
            AIMGlobal.sampleCodeMissing = false;
            AIMGlobal.sampleCodeMapped = true;
            Boolean blnSampleColumnMissing = true;
            List<AlternateNameInfo> AltName = new List<AlternateNameInfo>();        //1/28/20
            String strSampleCodeAlternateName = "";                                 //1/28/20
            Boolean altSampleCodeFound = false;                                     //1/28/20
            CFCUser.Global userGlobal = new CFCUser.Global();                       //11/18/21

            for (int i = 0; i < userTemplate.Count; i++)
            {
                //Map column = "SampleCode"

                //------------------------------------------------------
                //1/28/20 - get possible alternate name for "SampleCode"
                if (altSampleCodeFound == false)
                {
                    AltName = AIMManager.GetAlternateImportFieldName(userTemplate[i].UserDefinedDataSourceDefinitionID, "SampleCode");
                    if (AltName.Count > 0)
                    {
                        if (AltName[i].AlternateName != null)
                        {
                            strSampleCodeAlternateName = AltName[i].AlternateName;
                            altSampleCodeFound = true;
                        }
                    }
                }
                //------------------------------------------------------

                //1/28/20
                //if (userTemplate[i].MapColumnName == "SampleCode")
                if (userTemplate[i].MapColumnName == "SampleCode" || (altSampleCodeFound == true && userTemplate[i].MapColumnName == strSampleCodeAlternateName))

                {
                    blnSampleColumnMissing = false;
                    break;
                }

                //12/10/18
                if (userTemplate[i].UseAsSampleCode == true)
                {
                    AIMGlobal.sampleCodeMapped = false;
                    break;
                }

            }

            if (blnSampleColumnMissing == true && AIMGlobal.showAutoGenSampleMsg == true)
            {
                DialogResult dr;
                UserDefinedDataSourceDefinitionInfo definition = new UserDefinedDataSourceDefinitionInfo();
                definition = AIMManager.GetDataSourceUserDefinition(defID);

                //8/12/22
                if (definition.ImportType == (Int32)EnumImportType.RomerResults)

                {
                    //then skip check below
                    AIMGlobal.generateSampleCode = true;
                    AIMGlobal.sampleCodeMissing = true;
                }
                else
                {
                    AIMGlobal.showAutoGenSampleMsg = false;
                    AIMGlobal.sampleCodeMissing = true;
                    AIMGlobal.SplashScreenHide();

                    dr = MessageBox.Show("The Sample Code field is missing from the column mapping. Do you want the program to generate Sample Codes?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                    {
                        AIMGlobal.generateSampleCode = false;
                        AIMGlobal.SplashScreenShow();
                        return -1;
                    }
                }

                //-----------------------------------------------------------------------------
                //11/18/21 - update the user-defined definition field "SystemCreatedSampleCode" = Yes              
                //8/12/22
                if (definition.ImportType == (Int32)EnumImportType.RomerResults)
                {
                    //then skip check below
                }
                else
                {
                    if (definition.SystemCreatedSampleCode == false)
                    {
                        dr = MessageBox.Show("This template is set to NOT allow system-generated sample codes.  You must first change the template to allow system-generated codes, and then try again.", "Warning...", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                        AIMGlobal.showAutoGenSampleMsg = true;
                        AIMGlobal.SplashScreenShow();
                        return -1;
                    }
                }
                //-----------------------------------------------------------------------------


                AIMGlobal.generateSampleCode = true;

                //11/18/21
                AIMGlobal.sampleCodeMapped = false;


            }

            else if (blnSampleColumnMissing == true && AIMGlobal.showAutoGenSampleMsg == false)
            {
                AIMGlobal.sampleCodeMissing = true;
                if (AIMGlobal.generateSampleCode == true)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }

            }

            return 0;

        }

        //2/16/18 - created so all imports can call this one function
        public static String BuildImportPointerDeleteThisOneEventually(Int32 defID, String orientation, String importType, List<UserDefinedDataSourceTemplateInfo> userTemplate, string[] fieldHeaders, Int32 templateCount, Int32 columnCount, Int32 portraitHeaderRow, Int32 colCount)
        {
            Boolean HeaderColumnFound = false;
            String field = "";
            Boolean headerFieldsExist = false;
            Boolean TextMatchesMap = true;

            String rtn = "";

            //--------------------------- LANDSCAPE import-pointer	
            if (orientation == "Landscape")
            {
                for (int i = 1; i < columnCount; i++)
                {
                    HeaderColumnFound = false;
                    if (fieldHeaders[i] != null) //--> there is something in the title field for the column
                    {
                        for (int ii = 0; ii < userTemplate.Count; ii++)
                        {
                            if (HeaderColumnFound == false)
                            {
                                field = fieldHeaders[i].ToString().ToUpper();
                                if (userTemplate[ii].SourceColumnName.ToUpper() == field)
                                {
                                    userTemplate[ii].ImportPointer = i;
                                    HeaderColumnFound = true;
                                    headerFieldsExist = true;
                                }
                                else if (ii == userTemplate.Count - 1)
                                {
                                    TextMatchesMap = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        userTemplate[i - 1].ImportPointer = userTemplate[i - 1].SourceColumnNumber;
                    }
                }
            }

            //--------------------------- PORTRAIT import-pointer
            else
            {
                //2/16/18 - adding this here -- temporarily -- to allow the portrait CSV files to go thru okay.
                // Added the "headerFieldsExist" check for CSV landscape (just like Excel landscape) but 
                // I don't want to add the check to this section yet -- just want to fix the CSV landscape first.
                // So I'm forcing "headerFieldsExist" = true for CSV portrait.
                headerFieldsExist = true;

                if (portraitHeaderRow != 0)
                {
                    for (int i = 0; i < columnCount; i++)
                    {
                        if (fieldHeaders[i] != null) //--> there is something in the title field for the column
                        {
                            for (int ii = 0; ii < userTemplate.Count; ii++)
                            {
                                if (userTemplate[ii].SourceColumnName.ToUpper() == fieldHeaders[i].ToString().ToUpper())
                                {
                                    userTemplate[ii].ImportPointer = i;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < userTemplate.Count; i++)
                    {
                        userTemplate[i].ImportPointer = userTemplate[i].SourceColumnNumber - 1;
                    }
                }
            }

            if (headerFieldsExist == true && TextMatchesMap == true)
            {
                if (colCount > templateCount)
                {
                    for (int i = templateCount; i < colCount; i++)
                    {
                        userTemplate.Add(new UserDefinedDataSourceTemplateInfo
                        {
                            UserDefinedDataSourceDefinitionID = defID,
                            UserDefinedDataSourceTemplateID = 0,
                            SourceColumnName = fieldHeaders[i].ToString().ToUpper(),
                            SourceColumnNumber = i,
                            ImportPointer = i,
                            MapColumnID = i,
                            MapColumnName = "ResultValue",
                            IsManualEntry = false,
                            IsDetail = true
                        }
                        );
                    }
                }
            }

            if (headerFieldsExist == false)
            {
                rtn = importType + " header row is blank. Please fix " + importType + " file and/or mapping and try again.";
            }
            else if (TextMatchesMap == false)
            {
                rtn = importType + " header columns do not match the mapping. Please fix " + importType + " file and/or mapping and try again.";
            }

            return rtn;
        }

        //8/22/22 - added importType for Romer imports
        //2/26/18 - new and improved function -- delete one above eventually
        //public static String BuildImportPointer(Int32 defID, String orientation, String importType, List<UserDefinedDataSourceTemplateInfo> fieldTemplate, List<String[]> dataList, Int32 headerRow, Int32 landscapeBeginDetailColumn, Int32 landscapeDataColCount)
        public static String BuildImportPointer(Int32 defID, String orientation, String importFileType, List<UserDefinedDataSourceTemplateInfo> fieldTemplate, List<String[]> dataList, Int32 headerRow, Int32 landscapeBeginDetailColumn, Int32 landscapeDataColCount, Int32 importType)
        {
            Boolean HeaderColumnFound = false;
            String field = "";
            Boolean headerFieldsExist = false;
            Boolean TextMatchesMap = true;
            String rtn = "";

            switch (orientation)
            {

				#region Landscape
				case "Landscape":

                    if (landscapeBeginDetailColumn <= 0)
                    {
                        TextMatchesMap = false;
                    }
                    else
                    {
                        for (int i = 0; i < fieldTemplate.Count; i++)
                        {
                            HeaderColumnFound = false;
                            if (dataList[0] != null) //--> there is something in the title field for the column
                            {
                                for (int ii = 0; ii < fieldTemplate.Count; ii++)
                                {
                                    if (HeaderColumnFound == false)
                                    {
                                        field = dataList[0][i].ToString().ToUpper();

                                        //3/22/19 - Source Column Name cannot be longer than 30 chars
                                        if (field.Length > 30)
                                        {
                                            field = field.Substring(0, 30);
                                        }

                                        if (fieldTemplate[ii].SourceColumnName.ToUpper() == field)
                                        {
                                            fieldTemplate[ii].ImportPointer = i;
                                            HeaderColumnFound = true;
                                            headerFieldsExist = true;
                                        }
                                        else if (ii == fieldTemplate.Count - 1)
                                        {
                                            TextMatchesMap = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                fieldTemplate[i - 1].ImportPointer = fieldTemplate[i - 1].SourceColumnNumber;
                            }
                        }
                    }
                    break;
				#endregion

				#region Portrait
				case "Portrait":

                    if (headerRow != 0 && headerRow > dataList.Count)
                    {
                        TextMatchesMap = false;
                    }
                    

                    //8/30/22 - skipping this check for romer (where heading line starts on line 7, but line 6 is not acknowledged by TextParser
                    //else if (landscapeDataColCount < fieldTemplate.Count)
                    else if ((landscapeDataColCount < fieldTemplate.Count) && importType != (Int32)EnumImportType.RomerResults)
                    {
                        TextMatchesMap = false;
                    }


                    else
                    {
                        //----------------------------------------------------------------------
                        //Import has a header row (with column names)
                        if (headerRow != 0)
                        {
                            //5/2/23
                            if (headerRow == 1)
                            {

                                for (int i = 0; i < headerRow; i++)
                                {
                                    if (dataList[i] != null) //--> there is something in the title field for the column
                                    {
                                        for (int ii = 0; ii < fieldTemplate.Count; ii++)
                                        {
                                            if (fieldTemplate[ii].SourceColumnName.ToUpper() == dataList[i][ii].ToString().ToUpper())
                                            {
                                                fieldTemplate[ii].ImportPointer = ii;
                                            }
                                        }
                                    }
                                }
                            }

                            //5/2/23
                            else
                            {
                                for (int i = headerRow - 1; i < headerRow; i++)
                                {
                                    if (dataList[i] != null) //--> there is something in the title field for the column
                                    {
                                        for (int ii = 0; ii < fieldTemplate.Count; ii++)
                                        {
                                            if (fieldTemplate[ii].SourceColumnName.ToUpper() == dataList[i][ii].ToString().ToUpper())
                                            {
                                                fieldTemplate[ii].ImportPointer = ii;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        

                        //----------------------------------------------------------------------
                        //No header row
                        else
                        {
                            for (int i = 0; i < fieldTemplate.Count; i++)
                            {
                                fieldTemplate[i].ImportPointer = fieldTemplate[i].SourceColumnNumber - 1;
                            }
                        }
                    }
                    break;
                    #endregion
            }

            //-------------------------------------------------------------------------------------
            if (orientation == "Landscape")
            {
                if (headerFieldsExist == true && TextMatchesMap == true)
                {
                    if (landscapeDataColCount > landscapeBeginDetailColumn)
                    {
                        for (int i = landscapeBeginDetailColumn; i < landscapeDataColCount; i++)
                        {
                            fieldTemplate.Add(new UserDefinedDataSourceTemplateInfo
                            {
                                UserDefinedDataSourceDefinitionID = defID,
                                UserDefinedDataSourceTemplateID = 0,
                                SourceColumnName = dataList[0][i].ToString().ToUpper().Trim(),
                                SourceColumnNumber = i,
                                ImportPointer = i,
                                MapColumnID = i,
                                MapColumnName = "ResultValue",
                                IsManualEntry = false,
                                IsDetail = true
                            }
                            );
                        }
                    }
                }
            }

            //-------------------------------------------------------------------------------------
            if (headerFieldsExist == false && orientation == "Landscape")
            {
                rtn = "There is a problem with the " + importFileType + " header row. Landscape imports require header row. Please fix " + importFileType + " file and/or mapping and try again.";
            }
            else if (TextMatchesMap == false)
            {
                rtn = importFileType + " header columns do not match the mapping. ";
                rtn += Environment.NewLine + Environment.NewLine;
                rtn += "Make sure you have selected the correct orientation. ";
                rtn += Environment.NewLine + Environment.NewLine;
                rtn += "Please fix " + importFileType + " file and/or mapping and try again.";
            }

            return rtn;
        }

        
        public static Int32 ValidateMap(Int32 defID, List<UserDefinedDataSourceTemplateInfo> userTemplate, DevExpress.XtraEditors.LookUpEdit[] lueName, string[] colName, String fileName, String orientation)
        {
            Boolean blnMapColumnNameMissing = false;
            Boolean blnMaterialColumnMissing = true;
            Boolean blnCheckBoxSelected = false;
            String msg = "";

            List<AlternateNameInfo> AltName = new List<AlternateNameInfo>();        //1/28/20
            String strSampleCodeAlternateName = "";                                 //1/28/20
            Boolean altSampleCodeFound = false;                                     //1/28/20

            AIMGlobal.useAnotherFieldAsSampleCode = false;                                      //4/12/21

            if (orientation == "Portrait")
            {
                for (int i = 0; i < userTemplate.Count; i++)
                {
                    if (userTemplate[i].UseAsSampleCode == true)
                    {
                        blnCheckBoxSelected = true;
                        AIMGlobal.useAnotherFieldAsSampleCode = true;                           //4/12/21
                        break;
                    }
                }
            }

            //LOOP THRU USER TEMPLATE AND COMPARE WITH SPREADSHEET HEADERS
            for (int i = 0; i < userTemplate.Count; i++)
            {

                //Map column = "Material"
                if (userTemplate[i].MapColumnName == "Material")
                {
                    blnMaterialColumnMissing = false;
                }

                //Map column is blank				
                if ((userTemplate[i].MapColumnName == "" && userTemplate[i].UseAsSampleCode == false) || userTemplate[i].MapColumnName == "--Select--")
                {
                    blnMapColumnNameMissing = true;
                    break;
                }
            }

            //------------------------------------------------------
            //1/28/20 - get possible alternate name for "SampleCode"
            if (altSampleCodeFound == false)
            {
                AltName = AIMManager.GetAlternateImportFieldName(defID, "SampleCode");
                if (AltName.Count > 1)
                {

                    if (AltName[0].AlternateName != null)
                    {
                        strSampleCodeAlternateName = AltName[0].AlternateName;
                        altSampleCodeFound = true;
                    }
                }
            }
            if (altSampleCodeFound == false)
            {
                strSampleCodeAlternateName = "SampleCode";
            }

            //------------------------------------------------------

            //check for duplicate MapColumnNames for "SampleCode"

            //1/28/20 - changed to use either "SampleCode" or whatever is the alternate name for sample code (if one exists, and is found above.)
            //ErrorInfo outPut = AIMManager.GetUserDefinedDataSourceTemplateCount(defID, "SampleCode");
            ErrorInfo outPut = AIMManager.GetUserDefinedDataSourceTemplateCount(defID, strSampleCodeAlternateName);

            if (outPut.RecordID > 1)
            {
                AIMGlobal.SplashScreenHide();
                msg = "The mapping contains duplicate columns mapped to SampleCode.";
                msg += Environment.NewLine + Environment.NewLine;
                msg += "Please fix the mapping and try again.";
                MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
                return -1;
            }

            if (blnMapColumnNameMissing == true)
            {
                AIMGlobal.SplashScreenHide();
                if (orientation == "Portrait")
                {
                    if (blnCheckBoxSelected == false)
                    {
                        msg = "On the mapping, a column was not selected to group the samples.";
                        msg += Environment.NewLine + Environment.NewLine;
                        msg += Environment.NewLine + Environment.NewLine + "Please select a column (check box) to group the samples, and try again.";
                    }
                    else
                    {
                        msg = "A mapped column-name was not selected under the Column Map tab.";
                        msg += Environment.NewLine + Environment.NewLine + "Please either:  ";
                        msg += Environment.NewLine + Environment.NewLine;
                        msg += "     Select a column to be mapped, or " + Environment.NewLine;
                        msg += "     Delete the column.";
                    }
                    MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
                    return -1;

                }
                else
                {
                    msg = "A mapped column-name was not selected under the Column Map tab.";
                    msg += Environment.NewLine + Environment.NewLine + "Please either:  ";
                    msg += Environment.NewLine + Environment.NewLine;
                    msg += "     Select a column to be mapped, or " + Environment.NewLine;
                    msg += "     Delete the column.";
                    MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
                    return -1;
                }
            }

            Int32 intImportType = 0;
            UserDefinedDataSourceDefinitionInfo dataDef = new UserDefinedDataSourceDefinitionInfo();
            dataDef = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataDef != null)
            {
                intImportType = dataDef.ImportType;
            }
            else
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show("Error getting user-defined definition.", "Warning...", MessageBoxButtons.OK);
                return -1;
            }

            //4/3/18 -- if DataSourceIsMapped = 1, make sure there is a field on the template with the MapColumName = 'DataSource'
            if (dataDef.DataSourceIsMapped == true)
            {
                ErrorInfo map = AIMManager.GetUserDefinedDataSourceTemplateMapColumnName(defID, "DataSource");
                if (map.RecordID == -1)
                {
                    AIMGlobal.SplashScreenHide();
                    msg = "The 'Datasource is Mapped' option is selected for this template, but the Datasource column is not mapped under the Column Map tab.";
                    msg += Environment.NewLine + Environment.NewLine + "Please fix the mapping or set the 'Datasource is Mapped' option to No. ";
                    msg += Environment.NewLine + Environment.NewLine;
                    MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
                    return -1;
                }
            }

            //7/29/19
            ////4/1/19
            ////if (blnMaterialColumnMissing == true && intImportType != (Int32)EnumImportType.Merge)
            //if (blnMaterialColumnMissing == true && (intImportType != (Int32)EnumImportType.Merge && intImportType != (Int32)EnumImportType.ResultsAndHeaderFields))
            if (blnMaterialColumnMissing == true && (intImportType != (Int32)EnumImportType.UpdateResultsOnly && intImportType != (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields && intImportType != (Int32)EnumImportType.UpdateHeaderOnly))

            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show("Material has not been mapped. Import will not be allowed.", "Warning...", MessageBoxButtons.OK);
                return -1;
            }

            //-------------------------------------------------------------------								
            if (CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckAimRequiredDropdowns(userTemplate, lueName, colName) == -1)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show("AIM required fields designated as 'Use Mapping' under the Template tab are missing from the file. Please add the column(s), check the mapping, and try again.", "Warning...", MessageBoxButtons.OK);
                return -1;
            }

            //-------------------------------------------------------------------								        
            if (CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckOwnerMapping(defID) != 0)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show("AIM Owner Location must be mapped.", "Warning...", MessageBoxButtons.OK);
                return -1;
            }

            //-------------------------------------------------------------------					
            if (CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSupplierMapping(defID) != 0)
            {
                AIMGlobal.SplashScreenHide();
                MessageBox.Show("AIM Supplier Location must be mapped.", "Warning...", MessageBoxButtons.OK);
                return -1;
            }

            if (dataDef.SystemCreatedSampleCode == true)
            {
                AIMGlobal.generateSampleCode = true;

                //11/18/21
                AIMGlobal.sampleCodeMapped = false;

            }

            else
            {
                //11/18/21
                //if (CheckAutoGenerateSampleCode(userTemplate) == 0)
                if (CheckAutoGenerateSampleCode(userTemplate, defID) == 0)
                {
                    AIMGlobal.generateSampleCode = true;
                    if (AIMGlobal.sampleCodeMissing == false)
                    {
                        AIMGlobal.generateSampleCode = false;
                    }
                }
                else
                {
                    AIMGlobal.generateSampleCode = false;
                }
            }
            return 0;
        }
        public static Char GetDelimiterField(Int32 defID)
        {
            Char delimiterType = ',';
            UserDefinedDataSourceDefinitionInfo dataDef = new UserDefinedDataSourceDefinitionInfo();
            dataDef = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataDef != null)
            {
                if (dataDef.DelimiterTypeID == (Int32)DelimiterTypeEnum.Comma)
                    delimiterType = ',';
                else if (dataDef.DelimiterTypeID == (Int32)DelimiterTypeEnum.Semicolon)
                    delimiterType = ';';
                else if (dataDef.DelimiterTypeID == (Int32)DelimiterTypeEnum.Verticalbar)
                    delimiterType = '|';
                else if (dataDef.DelimiterTypeID == (Int32)DelimiterTypeEnum.Tab)
                    delimiterType = '\t';
                else
                    delimiterType = ' ';


            }
            return delimiterType;

        }


        public static Char GetDecimalSeparator(Int32 defID)
        {
            Char decSeparator = ' ';
            UserDefinedDataSourceDefinitionInfo dataDef = new UserDefinedDataSourceDefinitionInfo();
            dataDef = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataDef != null)
            {
                if (dataDef.ImportFileTypeID == (Int32)FileTypeEnum.CSV || dataDef.ImportFileTypeID == (Int32)FileTypeEnum.TXT)
                {

                    if (dataDef.DecimalSeparatorID == (Int32)DelimiterTypeEnum.Comma)
                        decSeparator = ',';
                    else
                    {
                        decSeparator = '.';
                    }

                }
            }
            return decSeparator;
        }


        public static String GetOrientation(Int32 defID)
        {
            String orientation = "";
            UserDefinedDataSourceDefinitionInfo dataDef = new UserDefinedDataSourceDefinitionInfo();
            dataDef = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataDef != null)
            {
                if (dataDef.Orientation == (Boolean)false)
                    orientation = "Portrait";
                if (dataDef.Orientation == (Boolean)true)
                    orientation = "Landscape";

            }
            return orientation;

        }


        public static String GetQualityControl(Int32 defID)
        {
            String qualityControl = "";
            UserDefinedDataSourceDefinitionInfo dataDef = new UserDefinedDataSourceDefinitionInfo();
            dataDef = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataDef != null)
            {
                if (dataDef.IsQualityControl == (Boolean)false)
                    qualityControl = "No";
                if (dataDef.IsQualityControl == (Boolean)true)
                    qualityControl = "Yes";

            }
            return qualityControl;

        }


        public static String GetImportType(Int32 defID)
        {
            String importType = "";
            UserDefinedDataSourceDefinitionInfo dataDef = new UserDefinedDataSourceDefinitionInfo();
            dataDef = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataDef != null)
            {
                if (dataDef.ImportType == (Int32)EnumImportType.UpdateResultsOnly)
                    importType = "Update Results Only";

                //4/1/19
                if (dataDef.ImportType == (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields)
                    importType = "Update Results and Header Fields";

                if (dataDef.ImportType == (Int32)EnumImportType.AddNewSamplesAndResults)
                    importType = "Add New Sample/Results";

                if (dataDef.ImportType == (Int32)EnumImportType.AddNewSamplesAndResultsORUpdateResults)
                    importType = "Add New Sample/Results OR Update Results";

                //7/29/19
                if (dataDef.ImportType == (Int32)EnumImportType.UpdateHeaderOnly)
                    importType = "Update Header Only";

            }
            return importType;

        }


        public static String GetHeaderExists(Int32 defID)
        {
            String headerExists = "";
            UserDefinedDataSourceDefinitionInfo dataDef = new UserDefinedDataSourceDefinitionInfo();
            dataDef = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataDef != null)
            {
                if (dataDef.FileHasHeader == (Boolean)true)
                    headerExists = "Yes";
                if (dataDef.FileHasHeader == (Boolean)false)
                    headerExists = "No";

            }
            return headerExists;

        }


        public static Int32 GetNutrientBeginColumn(Int32 defID)
        {
            Int32 col = 0;
            UserDefinedDataSourceDefinitionInfo dataDef = new UserDefinedDataSourceDefinitionInfo();
            dataDef = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataDef != null)
            {
                if (dataDef.NutrientDetailBeginColumn > 0)
                    col = dataDef.NutrientDetailBeginColumn;

            }
            return col;

        }


        //AUTO-GENERATED SAMPLES
        public static Int32 SampleCodeCounter(Int32 defID)
        {
            //Datasouce is needed because there may be multiple datasources on an import file.

            //------------------------------------------------------------------------------
            //IF SAMPLE-CODE DOES NOT EXIST ON FILE, GET LAST SAMPLE# USED (FOR DATA-SOURCE) AND SET COUNTER	

            Int32 dataSourceTypeID = 0;
            Int32 sampleCounter = 0;
            ErrorInfo samp = new ErrorInfo();
            UserDefinedDataSourceDefinitionInfo dataDef = new UserDefinedDataSourceDefinitionInfo();

            //------------------------------------------------------------------			
            dataDef = AIMManager.GetDataSourceUserDefinition(defID);
            if (dataDef != null)
            {
                dataSourceTypeID = dataDef.DataSourceTypeID;
            }

            //------------------------------------------------------------------			
            samp = AIMManager.GetNextGeneratedSampleNumber(dataSourceTypeID);
            if (samp.RecordID != 1)
            {
                //2/25/21 - added to get last samplecode from last import record (need this if multiple files are imported)              
                //sampleCounter = samp.RecordID + 1;
                if (AIMGlobal.lastImportSampleCode != "")
                {
                    Int32 code = 0;
                    Int32.TryParse(AIMGlobal.lastImportSampleCode, out code);
                    sampleCounter = code + 1;
                }
                else
                {
                    sampleCounter = samp.RecordID + 1;
                }

            }

            return sampleCounter;
        }
        public static Int32 CheckBackgroundWorker(BackgroundWorker backgroundWorker)
        {
            if (backgroundWorker.IsBusy == true)
            {
                backgroundWorker.ReportProgress(0);
            }
            return 0;
        }


        //2/15/21
        //public static Boolean SampleCodeAlreadyExistsForDataSource(String sampleCode)
        public static Boolean SampleCodeAlreadyExistsForDataSource(String sampleCode, Int32 datasourceTypeID)
        {
            Boolean rtn = false;
            SampleInfo sample = new SampleInfo();

            //12/29/17 - change to separate stp for imports (from one used on Sample screen, because that one uses number-of-days)
            //sample = AIMManager.GetSample(sampleCode);

            //------------------------------------------------------------------
            //7/13/21 - Question: Datasource on Sample is used with auto-generated samples, to get the next available sample# for the datasource,
            //			because there may be multiple datasources on an import file -- so shouldn't datasourcetypeid be included?

            //3/12/21 - changed this back, because datasourcetypeid on the sample header shouldn't really be used -- because
            //			one sample can have several datasourcetypes(which are on the AnalyzerNutrient and part of the results)
            //2/15/21
            //sample = AIMManager.GetSampleCodeForImports(sampleCode, datasourceTypeID);
            sample = AIMManager.GetSampleCodeForImports(sampleCode);
            //------------------------------------------------------------------

            if (sample != null)
            {
                rtn = true;
            }

            return rtn;
        }


        //2/15/21
        //public static Int32 SampleCodeAlreadyExistsOnTable(String sampleCode, Int32 datasourceTypeID)
        public static Int32 SampleCodeAlreadyExistsOnTable(String sampleCode)
        {
            Int32 rtn = 0;
            SampleInfo sample = new SampleInfo();

            //------------------------------------------------------------------
            //7/13/21 - Question: Datasource on Sample is used with auto-generated samples, to get the next available sample# for the datasource,
            //			because there may be multiple datasources on an import file -- so shouldn't datasourcetypeid be included?

            //3/12/21 - changed this back, because datasourcetypeid on the sample header shouldn't really be used -- because
            //			one sample can have several datasourcetypes(which are on the AnalyzerNutrient and part of the results)
            //2/15/21
            //sample = AIMManager.GetSampleCodeForImports(sampleCode, datasourceTypeID);
            sample = AIMManager.GetSampleCodeForImports(sampleCode);

            //------------------------------------------------------------------

            if (sample != null)
            {
                rtn = sample.SampleID;
            }
            return rtn;
        }

        //8/16/22
        public static Int32 RomerSampleCodeAlreadyExistsOnTable(String sampleCode, Int32 OwnerID, Int32 OwnerLocID, Int32 SupplierID, Int32 SupplierLocID)
        {
            Int32 rtn = 0;
            SampleInfo sample = new SampleInfo();
            sample = AIMManager.GetSampleCodeForRomerImports(sampleCode, OwnerID, OwnerLocID, SupplierID, SupplierLocID);
            if (sample != null)
            {
                rtn = sample.SampleID;
            }
            return rtn;
        }

        //11/24/21
        public static Int32 GetPointerForResults(String sampleCode, List<ImportHeaderInfo> workEntity, String ColumnToUseForGrouping)
        {
            Int32 rtn = 0;
            Int32 pointer = 0;
            Boolean sampleFound = false;

            if (ColumnToUseForGrouping == "SampleCode")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].SampleCode)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }
            else if (ColumnToUseForGrouping == "AlternateCode")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].AlternateCode)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }
            else if (ColumnToUseForGrouping == "BillOfLading")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].BillOfLading)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }
            else if (ColumnToUseForGrouping == "ContractNumber")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].ContractNumber)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }
            else if (ColumnToUseForGrouping == "InvoiceNumber")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].InvoiceNumber)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }
            else if (ColumnToUseForGrouping == "LoadNumber")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].LoadNumber)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }
            else if (ColumnToUseForGrouping == "LotNumber")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].LotNumber)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }
            else if (ColumnToUseForGrouping == "PONumber")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].PONumber)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }
            else if (ColumnToUseForGrouping == "TicketNumber")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].TicketNumber)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }
            else if (ColumnToUseForGrouping == "VehicleNumber")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].VehicleNumber)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }

            //8/18/22 - added for Romer imports
            else if (ColumnToUseForGrouping == "UniqueSampleID")
            {
                for (int i = 0; i < workEntity.Count; i++)
                {
                    if (sampleFound == false)
                    {
                        if (sampleCode == workEntity[i].UniqueSampleID)
                        {
                            pointer = i;
                        }
                    }
                }
                rtn = pointer;
            }

            return rtn;

        }


        //11/18/21
        public static Int32 GetPointerForExistingSamples(String sampleCode, List<ImportHeaderInfo> workEntity, String ColumnToUseForGrouping)
        {
            Int32 rtn = 0;

            try
            {

                if (ColumnToUseForGrouping == "SampleCode")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.SampleCode,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }
                else if (ColumnToUseForGrouping == "AlternateCode")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.AlternateCode,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }
                else if (ColumnToUseForGrouping == "BillOfLading")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.BillOfLading,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }
                else if (ColumnToUseForGrouping == "ContractNumber")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.ContractNumber,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }
                else if (ColumnToUseForGrouping == "InvoiceNumber")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.InvoiceNumber,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }
                else if (ColumnToUseForGrouping == "LoadNumber")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.LoadNumber,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }
                else if (ColumnToUseForGrouping == "LotNumber")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.LotNumber,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }
                else if (ColumnToUseForGrouping == "PONumber")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.PONumber,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }
                else if (ColumnToUseForGrouping == "TicketNumber")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.TicketNumber,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }
                else if (ColumnToUseForGrouping == "VehicleNumber")
                {
                    int ptr = workEntity.Select((item, index) => new
                    {
                        ItemName = item.VehicleNumber,
                        Position = index
                    }).Where(ii => ii.ItemName == sampleCode)
                    .First()
                    .Position;
                    rtn = ptr;
                }

                return rtn;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        //11/18/21
        //public static Boolean SampleCodeAlreadyExistsOnCurrentEntity(String sampleCode, List<ImportHeaderInfo> workEntity)
        //{
        //    Boolean rtn = false;
        //    var samp = from g in workEntity
        //               where g.SampleCode == sampleCode
        //               select new { g.SampleCode };
        //    if (samp.Count() != 0)
        //    {
        //        rtn = true;
        //    }

        //    return rtn;
        //}
        public static Boolean SampleCodeAlreadyExistsOnCurrentEntity(String sampleCode, List<ImportHeaderInfo> workEntity, String ColumnToUseForGrouping)
        {
            Boolean rtn = false;
            if (ColumnToUseForGrouping == "SampleCode")
            {
                var samp = from g in workEntity
                           where g.SampleCode == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "AlternateCode")
            {
                var samp = from g in workEntity
                           where g.AlternateCode == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "BillOfLading")
            {
                var samp = from g in workEntity
                           where g.BillOfLading == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "ContractNumber")
            {
                var samp = from g in workEntity
                           where g.ContractNumber == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "InvoiceNumber")
            {
                var samp = from g in workEntity
                           where g.InvoiceNumber == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "LoadNumber")
            {
                var samp = from g in workEntity
                           where g.LoadNumber == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "LotNumber")
            {
                var samp = from g in workEntity
                           where g.LotNumber == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "PONumber")
            {
                var samp = from g in workEntity
                           where g.PONumber == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "TicketNumber")
            {
                var samp = from g in workEntity
                           where g.TicketNumber == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "VehicleNumber")
            {
                var samp = from g in workEntity
                           where g.VehicleNumber == sampleCode
                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            return rtn;

        }

        //8/16/22
        public static Boolean RomerSampleCodeAlreadyExistsOnCurrentEntity(String sampleCode, Int32 ownerID, Int32 ownerLocID, Int32 supplierID, Int32 supplierLocID,
            List<ImportHeaderInfo> workEntity, String ColumnToUseForGrouping)
        {
            Boolean rtn = false;
            if (ColumnToUseForGrouping == "AlternateCode")
            {
                var samp = from g in workEntity
                           where g.AlternateCode == sampleCode &&
                                 g.OwnerID == ownerID &&
                                 g.OwnerLocationID == ownerLocID &&
                                 g.SourceID == supplierID &&
                                 g.SourceLocationID == supplierLocID

                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }
            else if (ColumnToUseForGrouping == "UniqueSampleID")
            {
                var samp = from g in workEntity
                           where g.UniqueSampleID == sampleCode &&
                                 g.OwnerID == ownerID &&
                                 g.OwnerLocationID == ownerLocID &&
                                 g.SourceID == supplierID &&
                                 g.SourceLocationID == supplierLocID

                           select new { g.SampleCode };
                if (samp.Count() != 0)
                {
                    rtn = true;
                }
            }

            return rtn;

        }

        public static Int32 SetSupplierAndSupplierLocationID(String type, String name, Int32 length)
        {
            Int32 rtn = 0;
            AIMSupplierInfo supplier = new AIMSupplierInfo();
            AIMSupplierLocationInfo supplierLoc = new AIMSupplierLocationInfo();
            if (type == "Supplier")
            {
                supplier = AIMManager.GetOneSupplier(name, length);
                if (supplier != null)
                {
                    rtn = supplier.AIMSupplierID;
                }

            }
            else if (type == "SupplierLocation")
            {
                supplierLoc = AIMManager.GetOneSupplierLocation(name, length);
                if (supplierLoc != null)
                {
                    rtn = supplierLoc.AIMSupplierLocationID;
                }
            }

            return rtn;
        }

        //2/15/21
        //public static Int32 SetSampleCodeAlreadyExists(String sampleCode)
        public static Int32 SetSampleCodeAlreadyExists(String sampleCode, Int32 datasourceTypeID)
        {
            Int32 rtn = 0;
            SampleInfo sample = new SampleInfo();

            //12/29/17 - change to separate stp for imports (from one used on Sample screen, because that one uses number-of-days)
            //sample = AIMManager.GetSample(sampleCode);

            //------------------------------------------------------------------
            //7/13/21 - Question: Datasource on Sample is used with auto-generated samples, to get the next available sample# for the datasource,
            //			because there may be multiple datasources on an import file -- so shouldn't datasourcetypeid be included?

            //3/12/21 - changed this back, because datasourcetypeid on the sample header shouldn't really be used -- because
            //			one sample can have several datasourcetypes(which are on the AnalyzerNutrient and part of the results)
            //2/15/21
            //sample = AIMManager.GetSampleCodeForImports(sampleCode, datasourceTypeID);
            sample = AIMManager.GetSampleCodeForImports(sampleCode);
            //------------------------------------------------------------------

            if (sample != null)
            {
                rtn = sample.SampleID;
            }
            return rtn;
        }

        //11/18/21
        //public static Boolean CheckForExistingSampleCode(String sampleCode, Int32 existingSampleID, List<ImportHeaderInfo> workHeader)
        //{
        //    //Make sure sample-code doesn't already exist on CURRENT entity
        //    Boolean sampleCodeExists = false;                                 
        //    if (existingSampleID != 0)
        //    {
        //        sampleCodeExists = true;
        //    }

        //    //Check CURRENT entity
        //    if (sampleCodeExists == false)
        //    {
        //        sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnCurrentEntity(sampleCode, workHeader);
        //    }
        //    return sampleCodeExists;
        //}
        public static Boolean CheckForExistingSampleCode(String sampleCode, Int32 existingSampleID, List<ImportHeaderInfo> workHeader, String ColumnToUseForGrouping)
        {
            //Make sure sample-code doesn't already exist on CURRENT entity
            Boolean sampleCodeExists = false;
            if (existingSampleID != 0)
            {
                sampleCodeExists = true;
            }

            //Check CURRENT entity
            if (sampleCodeExists == false)
            {
                sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnCurrentEntity(sampleCode, workHeader, ColumnToUseForGrouping);
            }
            return sampleCodeExists;
        }

        //8/16/22
        public static Boolean RomerCheckForExistingSampleCode(String sampleCode, Int32 ownerID, Int32 ownerLocID, Int32 supplierID, Int32 supplierLocID, Int32 existingSampleID, List<ImportHeaderInfo> workHeader, String ColumnToUseForGrouping)
        {
            //Make sure sample-code doesn't already exist on CURRENT entity
            Boolean sampleCodeExists = false;
            if (existingSampleID != 0)
            {
                sampleCodeExists = true;
            }

            //Check CURRENT entity
            if (sampleCodeExists == false)
            {
                sampleCodeExists = CFCAIM.AIMUtilities.Imports.SharedFunctions.RomerSampleCodeAlreadyExistsOnCurrentEntity(sampleCode, ownerID, ownerLocID, supplierID, supplierLocID, workHeader, ColumnToUseForGrouping);
            }
            return sampleCodeExists;
        }

        //check here 1
        public static String CheckSampleCode(String dt, Int32 counter, String sampleCode, String saveSampleCode, Boolean useFieldAsSampleCode)
        {
            String code = "";
            if (AIMGlobal.generateSampleCode == true)
            {
                if (sampleCode != "" && saveSampleCode != "" && useFieldAsSampleCode == false)          
                {
                    code = saveSampleCode;
                }
                else
                {
                    code = dt + counter.ToString();
                }
            }
            else if (AIMGlobal.generateSampleCode == false && AIMGlobal.sampleCodeMissing == true)
            {
                code = "Missing";
            }
            else
            {
                code = sampleCode;
            }

            return code;

        }

        //8/16/22              
        public static String CheckSampleCodeTxtCSVPortrait(String dt, Int32 counter, String sampleCode, String saveSampleCode, String test)

        {
            String code = "";
            if (AIMGlobal.generateSampleCode == true)
            {
                if (sampleCode != "" && saveSampleCode != "" && test == "")          
                {
                    code = saveSampleCode;
                }
                else
                {
                    code = dt + counter.ToString();
                }


            }
            else if (AIMGlobal.generateSampleCode == false && AIMGlobal.sampleCodeMissing == true)
            {
                code = "Missing";
            }
            else
            {
                code = sampleCode;
            }

            return code;

        }

        public static Boolean IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        //3/8/21
        public static Boolean WriteHeaderCheck(String UseThisFieldAsSampleCode, List<ImportHeaderInfo> workHeader, String saveSampleCode)
        {
            Boolean rtn = false;

            if (UseThisFieldAsSampleCode == "AlternateCode")
            {
                var samp = from g in workHeader
                           where g.AlternateCode == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }

            }
            if (UseThisFieldAsSampleCode == "BillOfLading")
            {
                var samp = from g in workHeader
                           where g.BillOfLading == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }
            }
            if (UseThisFieldAsSampleCode == "ContractNumber")
            {
                var samp = from g in workHeader
                           where g.ContractNumber == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }
            }
            if (UseThisFieldAsSampleCode == "InvoiceNumber")
            {
                var samp = from g in workHeader
                           where g.InvoiceNumber == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }
            }
            if (UseThisFieldAsSampleCode == "LoadNumber")
            {
                var samp = from g in workHeader
                           where g.LoadNumber == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }
            }
            if (UseThisFieldAsSampleCode == "LotNumber")
            {
                var samp = from g in workHeader
                           where g.LotNumber == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }
            }
            if (UseThisFieldAsSampleCode == "PONumber")
            {
                var samp = from g in workHeader
                           where g.PONumber == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }
            }
            if (UseThisFieldAsSampleCode == "TicketNumber")
            {
                var samp = from g in workHeader
                           where g.TicketNumber == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }
            }
            if (UseThisFieldAsSampleCode == "VehicleNumber")
            {
                var samp = from g in workHeader
                           where g.VehicleNumber == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }
            }

            //8/18/22 - added for Romer Imports
            if (UseThisFieldAsSampleCode == "UniqueSampleID")
            {
                var samp = from g in workHeader
                           where g.UniqueSampleID == saveSampleCode
                           select new { g.SampleCode };
                if (samp.Count() == 0 && saveSampleCode != "")
                {
                    rtn = true;
                }
            }

            return rtn;
        }


        public static String CheckSaveSampleCode(Boolean isDetail, String dt, Int32 counter, String saveSampleCode)

        {
            String code = "";

            if (isDetail == false && AIMGlobal.generateSampleCode == true && AIMGlobal.sampleCodeMapped == false)
            {
                code = dt + counter.ToString();
            }

            //3/9/21 ------------------------
            //Added for portrait imports where user wants system to generate sample-codes, but also wants to group the samples by another field (and not the sys-gen sample code).
            //Before this check, if the user selects system-generate sample-codes and also groups by another field, the AIMGlobal.sampleCodeMapped field is set to FALSE (so code will fall in here).
            else if (isDetail == false && AIMGlobal.generateSampleCode == true && AIMGlobal.sampleCodeMapped == false)
            {
                code = saveSampleCode;
            }
            //-------------------------------

            else if (isDetail == false && AIMGlobal.generateSampleCode == true)
            {
                code = dt + counter.ToString();
            }
            else if (isDetail == false && AIMGlobal.generateSampleCode == false && AIMGlobal.sampleCodeMissing == true)
            {
                code = "Missing";
            }
            else
            {
                code = saveSampleCode;
            }
            return code;
        }

        //1/6/20        
        public static String SaveImportXMLStream(GridView grdView, Int32 defID)
        {
            String code = "";

            //getting layout in stream
            Stream stream = new System.IO.MemoryStream();
            grdView.SaveLayoutToStream(stream);
            stream.Position = 0;

            //saving layout
            UserDefinedDataSourceDefinitionInfo definition = new UserDefinedDataSourceDefinitionInfo();
            definition = AIMManager.GetDataSourceUserDefinition(defID);

            StreamReader streamReader = new StreamReader(stream);
            definition.GridXMLStream = streamReader.ReadToEnd();
            stream.Dispose();

            ErrorInfo error = new ErrorInfo();
            error = AIMManager.SaveDataSourceUserDefinition(ref definition);
            if (error.ErrorNumber > 0)
            {
                code = "Error saving AIM grid template.";
            }
            return code;
        }

        //1/6/20        
        public static String GetImportXMLStream(GridView grdView, Int32 defID)
        {

            byte[] byteArray;
            UserDefinedDataSourceDefinitionInfo template = new UserDefinedDataSourceDefinitionInfo();
            template = AIMManager.GetDataSourceUserDefinition(defID);
            if (template == null || template.UserDefinedDataSourceDefinitionID == 0)
            {
                return "";
            }
            if (template.GridXMLStream == null)
            {
                return "";
            }

            if (template.GridXMLStream.Trim().Length == 0)
            {
                return "";
            }

            try
            {
                // convert string to stream
                byteArray = System.Text.Encoding.UTF8.GetBytes(template.GridXMLStream.Trim());
                MemoryStream stream = new MemoryStream(byteArray);
                grdView.RestoreLayoutFromStream(stream);
                return "";
            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        //12/23/20
        //public static String ChangeGridHeaderCaptions(GridView grdPreviewView)
        public static String ChangeGridHeaderCaptions(GridView grdPreviewView, Int32 defID, DevExpress.XtraEditors.LookUpEdit lueSupplier, DevExpress.XtraEditors.LookUpEdit lueSupplierLocation, String fromScreen)

        {
            String code = "";
            Boolean blnSupplierCombined = false;
            Boolean blnSupplier = false;
            Boolean blnSupplierLoc = false;

            //----------------------------------------------------------------------------------
            //1/6/20 - Update grid column headers with alternate names. 

            // ** There are 4 GridViews that need these labels:  
            //                      Two on the ImportMapping and 
            //                      Two on ImportDataUserDefined. 
            //On both forms, gridViews are named: grdPreview and grdPreviewHeaderView
            //----------------------------------------------------------------------------------

            //12/30/20 - (SupplierLocationCombined uses the SupplierLocation field on the Preview grid)

            //12/23/20    
            UserDefinedDataSourceTemplateInfo temp = new UserDefinedDataSourceTemplateInfo();
            temp = AIMManager.GetUserDefinedDataSourceTemplateMapColumnNameTop(defID, "SupplierLocationCombined");

            if (fromScreen == "Map")
            {
                if (temp != null && ((Int32)lueSupplier.EditValue == 1 || (Int32)lueSupplierLocation.EditValue == 1))
                {
                    blnSupplierCombined = true;

                    //Check for Supplier column
                    ErrorInfo mapSupplier = AIMManager.GetUserDefinedDataSourceTemplateMapColumnName(defID, "Supplier");
                    if (mapSupplier.RecordID > 0)
                    {
                        blnSupplier = true;
                    }

                    //Check for SupplierLocation column
                    ErrorInfo mapSupplierLoc = AIMManager.GetUserDefinedDataSourceTemplateMapColumnName(defID, "SupplierLocation");
                    if (mapSupplierLoc.RecordID > 0)
                    {
                        blnSupplierLoc = true;
                    }
                }
            }
            else
            {
                if (temp != null && ((Int32)lueSupplier.EditValue == 0 || (Int32)lueSupplierLocation.EditValue == 0))
                {
                    blnSupplierCombined = true;

                    //Check for Supplier column
                    ErrorInfo mapSupplier = AIMManager.GetUserDefinedDataSourceTemplateMapColumnName(defID, "Supplier");
                    if (mapSupplier.RecordID > 0)
                    {
                        blnSupplier = true;
                    }

                    //Check for SupplierLocation column
                    ErrorInfo mapSupplierLoc = AIMManager.GetUserDefinedDataSourceTemplateMapColumnName(defID, "SupplierLocation");
                    if (mapSupplierLoc.RecordID > 0)
                    {
                        blnSupplierLoc = true;
                    }
                }
            }

            List<ReportAlternateNamesInfo> _names = new List<ReportAlternateNamesInfo>();
            _names = AIMManager.GetReportAlternateNamesForImport();
            foreach (var item in _names)
            {
                if (item.ControlName == "lblBOL")
                {
                    grdPreviewView.Columns["BillOfLading"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblContractNumber")
                {
                    grdPreviewView.Columns["ContractNumber"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblInvoiceNumber")
                {
                    grdPreviewView.Columns["InvoiceNumber"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblLoadNumber")
                {
                    grdPreviewView.Columns["LoadNumber"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblLotNumber")
                {
                    grdPreviewView.Columns["LotNumber"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblPONumber")
                {
                    grdPreviewView.Columns["PONumber"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblTicketNumber")
                {
                    grdPreviewView.Columns["TicketNumber"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblVehicleID")
                {
                    grdPreviewView.Columns["VehicleID"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblMaterial")
                {
                    grdPreviewView.Columns["MaterialName"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblOwnerLocation")
                {
                    grdPreviewView.Columns["OwnerLocationName"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblOwner")
                {
                    grdPreviewView.Columns["OwnerName"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblSupplierLocation")
                {

                    //12/23/20
                    //grdPreviewView.Columns["SourceLocationName"].Caption = item.AlternateName;
                    grdPreviewView.Columns["SourceLocationName"].Visible = true;
                    if (blnSupplierCombined == true)
                    {
                        if (blnSupplierLoc == true)
                        {
                            grdPreviewView.Columns["SourceLocationName"].Caption = "Supplier and Location Combined";
                            grdPreviewView.Columns["SourceName"].Visible = false;
                        }
                        else if (blnSupplierLoc == false && blnSupplier == false)
                        {
                            grdPreviewView.Columns["SourceLocationName"].Caption = "Supplier and Location Combined";
                            grdPreviewView.Columns["SourceName"].Visible = false;
                        }

                    }
                    else
                    {
                        grdPreviewView.Columns["SourceLocationName"].Caption = item.AlternateName;
                    }
                }
                if (item.ControlName == "lblSupplier")
                {
                    //12/23/20
                    //grdPreviewView.Columns["SourceName"].Caption = item.AlternateName;
                    grdPreviewView.Columns["SourceName"].Visible = true;
                    if (blnSupplierCombined == true)
                    {
                        if (blnSupplier == true)
                        {
                            grdPreviewView.Columns["SourceName"].Caption = "Supplier and Location Combined";
                            grdPreviewView.Columns["SourceLocationName"].Visible = false;
                        }
                    }
                    else
                    {
                        grdPreviewView.Columns["SourceName"].Caption = item.AlternateName;
                    }
                }

                if (item.ControlName == "lblID")
                {
                    grdPreviewView.Columns["SampleCode"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblDate")
                {
                    grdPreviewView.Columns["SampleDate"].Caption = item.AlternateName;
                }

                if (item.ControlName == "lblAlternateCode")
                {
                    grdPreviewView.Columns["AlternateCode"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblDataSource")
                {
                    grdPreviewView.Columns["DataSourceTypeID"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblExpirationDate")
                {
                    grdPreviewView.Columns["ExpirationDate"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblFurtherAnalysis")
                {
                    grdPreviewView.Columns["FurtherAnalysisID"].Caption = item.AlternateName;
                }
                if (item.ControlName == "lblNotes")
                {
                    grdPreviewView.Columns["Notes"].Caption = item.AlternateName;
                }

            }
            //----------------------------------------------------------------------------------

            return code;
        }

        public static String ShowAdditionalMappedColumns(GridView grdPreviewView, Int32 defID)

        {
            String code = "";

            //Added VisibleIndex to make sure the required fields are the first columns in the grid ... 
            //and the Message, StatusImport and StatusDisplay are the last columns in the grid.

            grdPreviewView.Columns["SampleCode"].VisibleIndex = 0;
            grdPreviewView.Columns["SampleDate"].VisibleIndex = 1;
            grdPreviewView.Columns["MaterialName"].VisibleIndex = 2;
            grdPreviewView.Columns["OwnerName"].VisibleIndex = 3;
            grdPreviewView.Columns["OwnerLocationName"].VisibleIndex = 4;
            grdPreviewView.Columns["SourceName"].VisibleIndex = 5;
            grdPreviewView.Columns["SourceLocationName"].VisibleIndex = 6;
            grdPreviewView.Columns["UpdateOwnerSupplier"].VisibleIndex = 7;

            grdPreviewView.Columns["AlternateCode"].VisibleIndex = 8;
            grdPreviewView.Columns["BillOfLading"].VisibleIndex = 9;
            grdPreviewView.Columns["ContractNumber"].VisibleIndex = 10;
            grdPreviewView.Columns["InvoiceNumber"].VisibleIndex = 11;
            grdPreviewView.Columns["LoadNumber"].VisibleIndex = 12;
            grdPreviewView.Columns["LotNumber"].VisibleIndex = 13;
            grdPreviewView.Columns["Notes"].VisibleIndex = 14;
            grdPreviewView.Columns["PONumber"].VisibleIndex = 15;
            grdPreviewView.Columns["TicketNumber"].VisibleIndex = 16;
            grdPreviewView.Columns["VehicleID"].VisibleIndex = 17;
            grdPreviewView.Columns["ExpirationDate"].VisibleIndex = 18;

            grdPreviewView.Columns["Message"].VisibleIndex = 19;
            grdPreviewView.Columns["StatusDisplay"].VisibleIndex = 20;
            grdPreviewView.Columns["StatusImport"].VisibleIndex = 21;



            //Hide these columns unless they are mapped    
            grdPreviewView.Columns["SourceName"].Visible = false;           //CombinedSourceLocation uses the SourceLocationName column, so this is hidden unless "Combined" is not mapped
            grdPreviewView.Columns["UpdateOwnerSupplier"].Visible = false;  //this one is hidden until button on on PreviewImport tab is clicked
            grdPreviewView.Columns["AlternateCode"].Visible = false;
            grdPreviewView.Columns["BillOfLading"].Visible = false;
            grdPreviewView.Columns["ContractNumber"].Visible = false;
            grdPreviewView.Columns["InvoiceNumber"].Visible = false;
            grdPreviewView.Columns["LoadNumber"].Visible = false;
            grdPreviewView.Columns["LotNumber"].Visible = false;
            grdPreviewView.Columns["Notes"].Visible = false;
            grdPreviewView.Columns["PONumber"].Visible = false;
            grdPreviewView.Columns["TicketNumber"].Visible = false;
            grdPreviewView.Columns["VehicleID"].Visible = false;
            grdPreviewView.Columns["ExpirationDate"].Visible = false;

            //12/23/20    
            List<UserDefinedDataSourceTemplateInfo> temp = new List<UserDefinedDataSourceTemplateInfo>();
            temp = AIMManager.GetUserDefinedDataSourceTemplate(defID);
            foreach (var item in temp)
            {
                if (item.MapColumnName != "DO NOT USE" && item.MapColumnName != "SampleCode" && item.MapColumnName != "SampleDate" && item.MapColumnName != "Material" && item.MapColumnName != "Owner" && item.MapColumnName != "OwnerLocation" && item.MapColumnName != "SupplierLocation" && item.MapColumnName != "SupplierLocationCombined")
                {
                    //2/18/21
                    //grdPreviewView.Columns[item.MapColumnName].Visible = true;
                    if (grdPreviewView.Columns[item.MapColumnName] != null)
                    {
                        grdPreviewView.Columns[item.MapColumnName].Visible = true;
                    }

                }
            }

            UserDefinedDataSourceTemplateInfo tempcol = new UserDefinedDataSourceTemplateInfo();
            tempcol = AIMManager.GetUserDefinedDataSourceTemplateMapColumnNameTop(defID, "SupplierLocationCombined");
            if (tempcol == null)
            {
                grdPreviewView.Columns["SourceName"].Visible = true;
            }

            //----------------------------------------------------------------------------------

            //grdPreviewView.Columns["SampleCode"].VisibleIndex = 0;
            //grdPreviewView.Columns["SampleDate"].VisibleIndex = 1;
            //grdPreviewView.Columns["MaterialName"].VisibleIndex = 2;
            //grdPreviewView.Columns["OwnerName"].VisibleIndex = 3;
            //grdPreviewView.Columns["OwnerLocationName"].VisibleIndex = 4;            
            //grdPreviewView.Columns["SourceLocationName"].VisibleIndex = 6;
            //if (grdPreviewView.Columns["SourceName"].Visible == true) { grdPreviewView.Columns["SourceName"].VisibleIndex = 5; }
            //if (grdPreviewView.Columns["UpdateOwnerSupplier"].Visible == true) { grdPreviewView.Columns["UpdateOwnerSupplier"].VisibleIndex = 7; }
            //if (grdPreviewView.Columns["AlternateCode"].Visible == true) { grdPreviewView.Columns["AlternateCode"].VisibleIndex = 8; }
            //if (grdPreviewView.Columns["BillOfLading"].Visible == true) { grdPreviewView.Columns["BillOfLading"].VisibleIndex = 9; }
            //if (grdPreviewView.Columns["ContractNumber"].Visible == true) { grdPreviewView.Columns["ContractNumber"].VisibleIndex = 10; }
            //if (grdPreviewView.Columns["InvoiceNumber"].Visible == true) { grdPreviewView.Columns["InvoiceNumber"].VisibleIndex = 11; }
            //if (grdPreviewView.Columns["LoadNumber"].Visible == true) { grdPreviewView.Columns["LoadNumber"].VisibleIndex = 12; }
            //if (grdPreviewView.Columns["LotNumber"].Visible == true) { grdPreviewView.Columns["LotNumber"].VisibleIndex = 13; }
            //if (grdPreviewView.Columns["Notes"].Visible == true) { grdPreviewView.Columns["SourcNoteseName"].VisibleIndex = 14; }
            //if (grdPreviewView.Columns["PONumber"].Visible == true) { grdPreviewView.Columns["PONumber"].VisibleIndex = 15; }
            //if (grdPreviewView.Columns["TicketNumber"].Visible == true) { grdPreviewView.Columns["TicketNumber"].VisibleIndex = 16; }
            //if (grdPreviewView.Columns["VehicleID"].Visible == true) { grdPreviewView.Columns["VehicleID"].VisibleIndex = 17; }
            //if (grdPreviewView.Columns["ExpirationDate"].Visible == true) { grdPreviewView.Columns["ExpirationDate"].VisibleIndex = 18; }
            //grdPreviewView.Columns["Message"].VisibleIndex = 19;
            //grdPreviewView.Columns["StatusImport"].VisibleIndex = 20;
            //grdPreviewView.Columns["StatusDisplay"].VisibleIndex = 21;


            return code;
        }


        //12/13/18
        public static List<ImportHeaderInfo> PreviewImport(
            String fromString
            , Int32 defID
            , Int32 createOrMerge
            , Boolean allowDuplicates
            , Int32 lastHeaderID
            , String fileName
            , Int32 headerBeginRow
            , Int32 recCount
            , DevExpress.XtraEditors.LookUpEdit lueMaterial
            , DevExpress.XtraEditors.LookUpEdit lueOwner
            , DevExpress.XtraEditors.LookUpEdit lueOwnerLocation
            , DevExpress.XtraEditors.LookUpEdit lueSupplier
            , DevExpress.XtraEditors.LookUpEdit lueSupplierLocation
            , Int32 beginDetailColumn
            , String excelFileExtension
            , BackgroundWorker backgroundWorker
            , BackgroundWorker backgroundPreview                        //8/5/19

            , String useThisOwner                                       //12/22/20
            , String useThisOwnerLocation                               //12/22/20
            , String useThisSupplier                                    //12/22/20
            , String useThisSupplierLocation                            //12/22/20

            )
        {

            List<ImportHeaderInfo> importResults = new List<ImportHeaderInfo>();
            UserDefinedDataSourceDefinitionInfo selectedDefinition = new UserDefinedDataSourceDefinitionInfo();
            selectedDefinition = AIMManager.GetDataSourceUserDefinition(defID);

            //--------------------------------------------------------------------------------------
            //IMPORT FILE
            switch ((Int32)selectedDefinition.ImportFileTypeID)
            {

                //---------------------------- TXT/CSV ---------------------------------------------                    
                case (Int32)FileTypeEnum.CSV:
                case (Int32)FileTypeEnum.TXT:

                    // * * * * * * * * * * * * * * * * * * * * * * * 
                    // When changing CSV/TXT imports, NEED TO TEST:
                    // TXT/CSV - Landscape
                    // TXT/CSV - Portrait, with header
                    // TXT/CSV - Portrait, without header   
                    // TXT/CSV - Portrait, with sample-code mapped
                    // TXT/CSV - Portrait, with another field used for grouping
                    // TXT/CSV - Portrait, with no grouping
                    // * * * * * * * * * * * * * * * * * * * * * * *

                    importResults = ImportTextCSVFile.TextCSVImportSamples(fromString, defID, createOrMerge, allowDuplicates, lastHeaderID, selectedDefinition.OrientationDisplay, fileName, headerBeginRow, lueMaterial, lueOwner, lueOwnerLocation, lueSupplier, lueSupplierLocation, beginDetailColumn, backgroundPreview, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);
                    break;


                //---------------------------- EXCEL -----------------------------------------------
                case (Int32)FileTypeEnum.XLS:
                case (Int32)FileTypeEnum.XLSX:

                    // * * * * * * * * * * * * * * * * * * * * * * *
                    // When changing EXCEL imports, NEED TO TEST:
                    // XLSX - Landscape
                    // XLSX - Portrait, with header
                    // XLSX - Portrait, without header
                    // XLSX - Portrait, with sample-code mapped
                    // XLSX - Portrait, with another field used for grouping
                    // XLSX - Portrait, with no grouping
                    // * * * * * * * * * * * * * * * * * * * * * * *

                    excelFileExtension = Path.GetExtension(fileName);
                    importResults = ImportExcelFile.ExcelImportSamples(fromString, defID, createOrMerge, allowDuplicates, lastHeaderID, selectedDefinition.OrientationDisplay, fileName, headerBeginRow, lueMaterial, lueOwner, lueOwnerLocation, lueSupplier, lueSupplierLocation, beginDetailColumn, excelFileExtension, backgroundPreview, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);
                    break;

                default:
                    MessageBox.Show("This import file format is currently unavailable for User-Defined imports. Contact IT.", "Warning...", MessageBoxButtons.OK);
                    break;
            }

            return importResults;
        }

        //8/16/22
        public static List<ImportHeaderInfo> PreviewRomerImport(
            String fromString
            , Int32 defID
            , Int32 createOrMerge
            , Boolean allowDuplicates
            , Int32 lastHeaderID
            , String fileName
            , Int32 headerBeginRow
            , Int32 recCount
            , DevExpress.XtraEditors.LookUpEdit lueMaterial
            , DevExpress.XtraEditors.LookUpEdit lueOwner
            , DevExpress.XtraEditors.LookUpEdit lueOwnerLocation
            , DevExpress.XtraEditors.LookUpEdit lueSupplier
            , DevExpress.XtraEditors.LookUpEdit lueSupplierLocation
            , Int32 beginDetailColumn
            , String excelFileExtension
            , BackgroundWorker backgroundWorker
            , BackgroundWorker backgroundPreview
            , String useThisOwner
            , String useThisOwnerLocation
            , String useThisSupplier
            , String useThisSupplierLocation
            , Int32 detailBeginRow 

            )
        {

            List<ImportHeaderInfo> importResults = new List<ImportHeaderInfo>();
            UserDefinedDataSourceDefinitionInfo selectedDefinition = new UserDefinedDataSourceDefinitionInfo();
            selectedDefinition = AIMManager.GetDataSourceUserDefinition(defID);
            importResults = ImportRomerCSV.RomerImportSamples(fromString, defID, createOrMerge, allowDuplicates, lastHeaderID, selectedDefinition.OrientationDisplay, fileName, headerBeginRow, lueMaterial, lueOwner, lueOwnerLocation, lueSupplier, lueSupplierLocation, beginDetailColumn, backgroundPreview, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation, detailBeginRow);
            return importResults;
        }


        //12/13/18
        public static Int32 SetHeaderBeginRow(Int32 DefID)
        {
            UserDefinedDataSourceDefinitionInfo selectedDefinition = new UserDefinedDataSourceDefinitionInfo();
            selectedDefinition = AIMManager.GetDataSourceUserDefinition(DefID);                    


            Int32 headerBeginRow = selectedDefinition.HeaderBeginRow;

            //--------------------------------------------------
            //SET CSV/TEXT HEADER BEGIN ROW (PORTRAIT)            
            if ((Int32)selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.CSV
                || (Int32)selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.TXT)

            {
                if (selectedDefinition.FileHasHeader == false && selectedDefinition.Orientation == false)  // (portrait)
                {
                    headerBeginRow = 0;
                }
            }

            //--------------------------------------------------
            //SET EXCEL HEADER BEGIN 
            if ((Int32)selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLS
                || (Int32)selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLSX)
            {
                if (headerBeginRow > 0)
                {
                    headerBeginRow--;   //doing this because DevExpress Spreadsheet starts columns and rows at 0, not 1                       
                }
            }
            return headerBeginRow;

        }

        //8/15/22 - added IsComposite (to bypass message for Romer imports)
        //public static Boolean CheckSampleGrouping(Int32 templateCount, List<UserDefinedDataSourceTemplateInfo> def)       
        public static Boolean CheckSampleGrouping(Int32 templateCount, List<UserDefinedDataSourceTemplateInfo> def, Boolean isComposite)
        {

            //Portrait imports need field to use to group samples (otherwise, each line on the portrait import will show up as a single sample)

            Boolean rtn = false;
            Boolean checkmarkFound = false;
            Boolean sampleCodeIsMapped = false;

            if (templateCount != 0)
            {
                if (def != null)
                {
                    foreach (var item in def)
                    {
                        if (item.UseAsSampleCode == true)
                        {
                            //4/14/21
                            //checkmarkFound = true;
                            if (item.MapColumnName == "DO NOT USE") { }
                            else
                            {
                                checkmarkFound = true;
                            }

                            break;
                        }
                        if (item.MapColumnName == "SampleCode")
                        {
                            sampleCodeIsMapped = true;
                            break;
                        }

                    }
                }
            }

            //11/19/21
            AIMGlobal.noGrouping = false;         //used for samples where there is no field to group the samples and the sample-codes are sys-generated

            if (checkmarkFound == false && sampleCodeIsMapped == false)
            {
                //11/19/21           
                AIMGlobal.noGrouping = true;      //used for samples where there is no field to group the samples and the sample-codes are sys-generated                              

                //8/15/22 - added for Romer imports, to skip the message below
                if (isComposite == true)
                {
                    rtn = true;
                }
                else

                {
                    DialogResult dr = new DialogResult();
                    dr = MessageBox.Show("A field to use to group the samples has not been selected.  Continue?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.Yes)
                    {
                        rtn = true;
                    }
                }
            }
            else
            {
                rtn = true;
            }


            return rtn;
        }



        //12/11/18 - check for column name to use as sample break (portrait imports)
        public static Boolean CheckSampleBreakColumn(String colToUseAsSampleBreak, List<ImportHeaderInfo> workHdr, String saveSampleCode)
        {
            Boolean rtn = false;

            switch (colToUseAsSampleBreak)
            {
                case "AlternateCode":
                    var work1 = from g in workHdr
                                where g.AlternateCode == saveSampleCode
                                select new { g.SampleCode };
                    if (work1.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;


                case "BillOfLading":
                    var work2 = from g in workHdr
                                where g.BillOfLading == saveSampleCode
                                select new { g.BillOfLading };
                    if (work2.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }

                    break;

                case "ContractNumber":
                    var work3 = from g in workHdr
                                where g.ContractNumber == saveSampleCode
                                select new { g.ContractNumber };
                    if (work3.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "InvoiceNumber":
                    var work4 = from g in workHdr
                                where g.InvoiceNumber == saveSampleCode
                                select new { g.InvoiceNumber };
                    if (work4.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "LoadNumber":
                    var work5 = from g in workHdr
                                where g.LoadNumber == saveSampleCode
                                select new { g.LoadNumber };
                    if (work5.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "LotNumber":
                    var work6 = from g in workHdr
                                where g.LotNumber == saveSampleCode
                                select new { g.LotNumber };
                    if (work6.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "Material":
                    var work7 = from g in workHdr
                                where g.MaterialCode == saveSampleCode
                                select new { g.MaterialCode };
                    if (work7.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "Notes":
                    var work8 = from g in workHdr
                                where g.Notes == saveSampleCode
                                select new { g.Notes };
                    if (work8.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "OwnerCode":
                    var work9 = from g in workHdr
                                where g.OwnerCode == saveSampleCode
                                select new { g.OwnerCode };
                    if (work9.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "OwnerLocationCode":
                    var work10 = from g in workHdr
                                 where g.OwnerLocationCode == saveSampleCode
                                 select new { g.OwnerLocationCode };
                    if (work10.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "PONumber":
                    var work11 = from g in workHdr
                                 where g.PONumber == saveSampleCode
                                 select new { g.PONumber };
                    if (work11.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "SampleCode":
                    var work12 = from g in workHdr
                                 where g.SampleCode == saveSampleCode
                                 select new { g.SampleCode };
                    if (work12.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "SourceCode":
                    var work13 = from g in workHdr
                                 where g.SourceCode == saveSampleCode
                                 select new { g.SourceCode };
                    if (work13.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "SourceLocationCode":
                    var work14 = from g in workHdr
                                 where g.SourceLocationCode == saveSampleCode
                                 select new { g.SourceLocationCode };
                    if (work14.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "TicketNumber":
                    var work15 = from g in workHdr
                                 where g.TicketNumber == saveSampleCode
                                 select new { g.TicketNumber };
                    if (work15.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "VehicleID":
                    var work16 = from g in workHdr
                                 where g.VehicleNumber == saveSampleCode
                                 select new { g.VehicleNumber };
                    if (work16.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                case "UniqueSampleID":
                    var work17 = from g in workHdr
                                where g.UniqueSampleID == saveSampleCode
                                select new { g.UniqueSampleID };
                    if (work17.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
                default:
                    var work23 = from g in workHdr
                                 where g.SampleCode == saveSampleCode
                                 select new { g.SampleCode };
                    if (work23.Count() == 0 && saveSampleCode != "")
                    {
                        rtn = true;
                    }
                    break;
            }

            return rtn;
        }

        public static Int32 SetRowCountForMapPreview(String fromScreen, Int32 rowCount)
        {
            Int32 count = 0;

            //12/22/20
            //if (fromScreen == "Map" && rowCount > 99)
            if (fromScreen == "Map" && rowCount > 29)
            {
                //12/22/20
                //count = 100;
                count = 30;
            }
            else
            {
                count = rowCount;
            }
            return count;
        }


        public static String GetOriginalColumnMapName(Int32 defID, String alternateName)
        {
            String rtn = "";
            MapColumnNamesInfo map = new MapColumnNamesInfo();
            map = AIMManager.GetOriginalMapColumnName(defID, alternateName);
            if (map != null)
            {
                rtn = map.MapColumnName;
            }
            else
            {
                rtn = alternateName;
            }
            return rtn;
        }

        //2/15/21
        //public static Int32 SetHeaderFieldsForHeaderUpdateOnly(String sampleCode)
        public static Int32 SetHeaderFieldsForHeaderUpdateOnly(String sampleCode, Int32 datasourceTypeID)
        {
            Int32 rtn = 0;
            SampleInfo sample = new SampleInfo();

            //------------------------------------------------------------------
            //7/13/21 - Question: Datasource on Sample is used with auto-generated samples, to get the next available sample# for the datasource,
            //			because there may be multiple datasources on an import file -- so shouldn't datasourcetypeid be included?

            //3/12/21 - changed this back, because datasourcetypeid on the sample header shouldn't be used -- because
            //			one sample can have several datasourcetypes(which are on the AnalyzerNutrient and part of the results)
            //2/15/21
            //sample = AIMManager.GetSampleCodeForImports(sampleCode, datasourceTypeID);
            sample = AIMManager.GetSampleCodeForImports(sampleCode);

            //------------------------------------------------------------------

            if (sample != null)
            {
                _aimMaterialID = sample.AIMMaterialID;
                _aimOwnerID = sample.AIMOwnerID;
                _aimOwnerLocationID = sample.AIMOwnerLocationID;
                _aimSupplierID = sample.AIMSupplierID;
                _aimSupplierLocationID = sample.AIMSupplierLocationID;

                _alternateCode = sample.AlternateCode;

                //1/30/20
                //_sampleDate = sample.SampleDate;
                _sampleDate = sample.SampleDate.ToString();

                _invoiceNumber = sample.InvoiceNumber;
                _poNumber = sample.PoNumber;
                _ticketNumber = sample.TicketNumber;
                _vehicleNumber = sample.VehicleNumber;
                _billOfLading = sample.BillOfLoad;
                _contractNumber = sample.ContractNumber;
                _loadNumber = sample.LoadNumber;
                _lotNumber = sample.LotNumber;
                _notes = sample.Notes;
                _dataSourceTypeID = sample.DataSourceTypeID;

                _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.UpdateHeaderFields;
                _headerMessage = "Update header fields";

            }

            return rtn;
        }

        //2/15/21
        //public static Int32 SetHeaderFieldsForResults(String sampleCode, Int32 importType)
        public static Int32 SetHeaderFieldsForResults(String sampleCode, Int32 importType, Int32 datasourceTypeID)
        {
            Int32 rtn = 0;
            SampleInfo sample = new SampleInfo();

            //------------------------------------------------------------------
            //7/13/21 - Question: Datasource on Sample is used with auto-generated samples, to get the next available sample# for the datasource,
            //			because there may be multiple datasources on an import file -- so shouldn't datasourcetypeid be included?

            //3/12/21 - changed this back, because datasourcetypeid on the sample header shouldn't really be used -- because
            //			one sample can have several datasourcetypes(which are on the AnalyzerNutrient and part of the results)
            //2/15/21
            //sample = AIMManager.GetSampleCodeForImports(sampleCode, datasourceTypeID);
            sample = AIMManager.GetSampleCodeForImports(sampleCode);

            //------------------------------------------------------------------

            if (sample != null)
            {
                _aimMaterialID = sample.AIMMaterialID;
                _aimOwnerID = sample.AIMOwnerID;
                _aimOwnerLocationID = sample.AIMOwnerLocationID;
                _aimSupplierID = sample.AIMSupplierID;
                _aimSupplierLocationID = sample.AIMSupplierLocationID;

                if (importType == (Int32)EnumImportType.UpdateResultsOnly)
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MergeResults;
                    _headerMessage = "Add results";
                }
                else if (importType == (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields)
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.AddResultsAndUpdateHeaderFields;
                    _headerMessage = "Add results and update specific header fields";
                }
            }

            return rtn;
        }

        //12/28/20
        //public static Int32 PopulateUserDefinedTables(String materialName, String ownerName, String ownerLocName, String supplierName, String supplierLocName)
        public static Int32 PopulateUserDefinedTables(String materialName, String ownerName, String ownerLocName, String supplierName, String supplierLocName, Boolean blnCombined)
        {
            Int32 rtn = 0;


            if (ownerName.Length != 0)
                _owner = ownerName;
            if (ownerLocName.Length != 0)
                _ownerLocation = ownerLocName;
            if (supplierName.Length != 0)
                _supplier = supplierName;
            if (supplierLocName.Length != 0)
                _supplierLocation = supplierLocName;

            //12/28/20
            if (blnCombined == true)
            {
                _analyzerMaterialID = PopulateAnalyzerTables(_dataSourceTypeID, "Material", materialName, 0);
                _analyzerOwnerID = PopulateAnalyzerTables(_dataSourceTypeID, "Owner", ownerName, 0);
                _analyzerOwnerLocationID = PopulateAnalyzerTables(_dataSourceTypeID, "OwnerLocation", ownerLocName, _analyzerOwnerID);
                _analyzerSupplierLocationCombinedID = PopulateAnalyzerTables(_dataSourceTypeID, "SupplierLocationCombined", supplierLocName, 0);

                _analyzerMaterialCode = PopulateAnalyzerCodes("Material", _analyzerMaterialID);
                _analyzerOwnerCode = PopulateAnalyzerCodes("Owner", _analyzerOwnerID);
                _analyzerOwnerLocationCode = PopulateAnalyzerCodes("OwnerLocation", _analyzerOwnerLocationID);
            }
            else
            {
                _analyzerMaterialID = PopulateAnalyzerTables(_dataSourceTypeID, "Material", materialName, 0);
                _analyzerOwnerID = PopulateAnalyzerTables(_dataSourceTypeID, "Owner", ownerName, 0);
                _analyzerOwnerLocationID = PopulateAnalyzerTables(_dataSourceTypeID, "OwnerLocation", ownerLocName, _analyzerOwnerID);
                _analyzerSupplierID = PopulateAnalyzerTables(_dataSourceTypeID, "Source", supplierName, 0);
                _analyzerSupplierLocationID = PopulateAnalyzerTables(_dataSourceTypeID, "SourceLocation", supplierLocName, _analyzerSupplierID);

                _analyzerMaterialCode = PopulateAnalyzerCodes("Material", _analyzerMaterialID);
                _analyzerOwnerCode = PopulateAnalyzerCodes("Owner", _analyzerOwnerID);
                _analyzerOwnerLocationCode = PopulateAnalyzerCodes("OwnerLocation", _analyzerOwnerLocationID);
                _analyzerSupplierCode = PopulateAnalyzerCodes("Source", _analyzerSupplierID);
                _analyzerSupplierLocationCode = PopulateAnalyzerCodes("SourceLocation", _analyzerSupplierLocationID);
            }
            return rtn;
        }

        public static Int32 CheckOverride()
        {
            Int32 rtn = 0;
            if (_activeMaterialStatus == 1 ||
                        _activeOwnerStatus == 1 ||
                        _activeOwnerLocationStatus == 1 ||
                        _activeSupplierStatus == 1 ||
                        _activeSupplierLocationStatus == 1
                        )
            {
                _headerStatus = (Int32)SampleImportStatus.Ignore;

                if (_activeMaterialStatus == 1)
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveMaterial;
                    _headerMessage = "Sample will be ignored on import (Inactive Material).";
                }
                else if (_activeOwnerStatus == 1)
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveOwner;
                    _headerMessage = "Sample will be ignored on import (Inactive Owner).";
                }
                else if (_activeOwnerLocationStatus == 1)
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation;
                    _headerMessage = "Sample will be ignored on import (Inactive Owner Location).";
                }
                else if (_activeSupplierStatus == 1)
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveSupplier;
                    _headerMessage = "Sample will be ignored on import (Inactive Supplier).";
                }
                else if (_activeSupplierLocationStatus == 1)
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation;
                    _headerMessage = "Sample will be ignored on import (Inactive Supplier Location).";
                }
                else
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.Rejected;
                    _headerMessage = "Sample will be ignored on import.";
                }
            }

            return rtn;
        }

        public static Int32 SetHeaderMessage(String materialName, String ownerName, String ownerLocationName, String supplierName, String supplierLocationName,
            DevExpress.XtraEditors.LookUpEdit lueMaterial,
            DevExpress.XtraEditors.LookUpEdit lueOwner,
            DevExpress.XtraEditors.LookUpEdit lueOwnerLocation,
            DevExpress.XtraEditors.LookUpEdit lueSupplier,
            DevExpress.XtraEditors.LookUpEdit lueSupplierLocation,
            String fromScreen
            )
        {
            Int32 rtn = 0;

            CFCAIM.AIMGlobal.importMissingField = 0;

            _material = SetRequiredField(lueMaterial, materialName, fromScreen);
            _owner = SetRequiredField(lueOwner, ownerName, fromScreen);
            _ownerLocation = SetRequiredField(lueOwnerLocation, ownerLocationName, fromScreen);
            _supplier = SetRequiredField(lueSupplier, supplierName, fromScreen);
            _supplierLocation = SetRequiredField(lueSupplierLocation, supplierLocationName, fromScreen);
            if (_material == "MISSING" || _owner == "MISSING" || _ownerLocation == "MISSING" || _supplier == "MISSING" || _supplierLocation == "MISSING")
            {
                _headerMessage = "Missing Required field.";
                _headerStatus = (Int32)SampleImportStatus.Cancelled;

                if (_supplierLocation == "MISSING")
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocation;
                }
                else if (_supplier == "MISSING")
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingSupplier;
                }
                else if (_ownerLocation == "MISSING")
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocation;
                }
                else if (_owner == "MISSING")
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingOwner;
                }
                else if (_material == "MISSING")
                {
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingMaterial;
                }
            }
            return rtn;
        }

        //12/24/20
        public static Int32 CheckSupplierLocCombinedCrossReference(Int32 supplierID, Int32 supplierLocID, DevExpress.XtraEditors.LookUpEdit lueSupplierLoc)
        {
            Int32 rtn = 0;
            _aimSupplierLocationID = supplierLocID;
            if (lueSupplierLoc.Enabled == false)
            {
                //check to see if analyzerSupplierLocationCombinedID is active						
                _aimSupplierLocationID = AnalyzerCheckActive("SupplierLocationCombined", _dataSourceTypeID, _analyzerSupplierLocationCombinedID, _analyzerSupplierID);
                if (_aimSupplierLocationID == 0)
                {
                    _headerMessage = "Sample will be ignored on import (Inactive Supplier Location Combined)";
                    _activeSupplierLocationStatus = 1;
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation;
                    _headerStatus = (Int32)SampleImportStatus.Ignore;
                    _errorSupplierLocation++;
                }
                else
                {
                    _aimSupplierLocationID = CheckCrossReference("SupplierLocationCombined", _dataSourceTypeID, _analyzerSupplierLocationCombinedID, _analyzerSupplierID);

                    if (_aimSupplierLocationID == 0)
                    {
                        _headerMessage = "Missing Cross-Reference";
                        _xrefSupplierLocationStatus = 1;
                        _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocationCombinedXref;
                        _headerStatus = (Int32)SampleImportStatus.Cancelled;
                        _errorSupplierLocation++;
                    }
                    else
                    {
                        _aimSupplierID = CheckCrossReferenceAndReturnCombinedSupplier("SupplierLocationCombined", _dataSourceTypeID, _analyzerSupplierLocationCombinedID);
                    }
                }
            }
            return rtn;
        }

        public static Int32 CheckSupplierLocCrossReference(Int32 supplierLocID, DevExpress.XtraEditors.LookUpEdit lueSupplierLoc)
        {
            Int32 rtn = 0;
            _aimSupplierLocationID = supplierLocID;
            if (lueSupplierLoc.Enabled == false)
            {
                //check to see if analyzerSupplierLocationID is active						
                _aimSupplierLocationID = AnalyzerCheckActive("SupplierLocation", _dataSourceTypeID, _analyzerSupplierLocationID, _analyzerSupplierID);
                if (_aimSupplierLocationID == 0)
                {
                    _headerMessage = "Sample will be ignored on import (Inactive Supplier Location)";
                    _activeSupplierLocationStatus = 1;
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation;
                    _headerStatus = (Int32)SampleImportStatus.Ignore;
                    _errorSupplierLocation++;
                }
                else
                {
                    _aimSupplierLocationID = CheckCrossReference("SourceLocation", _dataSourceTypeID, _analyzerSupplierLocationID, _analyzerSupplierID);
                    if (_aimSupplierLocationID == 0)
                    {
                        _headerMessage = "Missing Cross-Reference";
                        _xrefSupplierLocationStatus = 1;
                        _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocationXref;
                        _headerStatus = (Int32)SampleImportStatus.Cancelled;
                        _errorSupplierLocation++;
                    }
                }
            }
            return rtn;
        }


        public static Int32 CheckSupplierCrossReference(Int32 supplierID, DevExpress.XtraEditors.LookUpEdit lueSupplier)
        {
            Int32 rtn = 0;
            //-------------------------------------------------------------------
            _aimSupplierID = supplierID;
            if (lueSupplier.Enabled == false)
            {
                // check to see if analyzerSupplierID is active
                _aimSupplierID = AnalyzerCheckActive("Supplier", _dataSourceTypeID, _analyzerSupplierID, 0);
                if (_aimSupplierID == 0)
                {
                    _headerMessage = "Sample will be ignored on import (Inactive Supplier).";
                    _activeSupplierStatus = 1;
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveSupplier;
                    _headerStatus = (Int32)SampleImportStatus.Ignore;
                    _errorSupplier++;
                }
                else
                {
                    //---------------------------------------
                    _aimSupplierID = CheckCrossReference("Source", _dataSourceTypeID, _analyzerSupplierID, 0);
                    if (_aimSupplierID == 0)
                    {
                        _headerMessage = "Missing Cross-Reference";
                        _xrefSupplierStatus = 1;
                        _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingSupplierXref;
                        _headerStatus = (Int32)SampleImportStatus.Cancelled;
                        _errorSupplier++;
                    }
                }

            }
            return rtn;
        }
        public static Int32 CheckOwnerLocCrossReference(Int32 ownerLocID, DevExpress.XtraEditors.LookUpEdit lueOwnerLocation)
        {
            Int32 rtn = 0;

            _aimOwnerLocationID = ownerLocID;
            if (lueOwnerLocation.Enabled == false)
            {
                //check to see if analyzerOwnerLocationID is active						
                _aimOwnerLocationID = AnalyzerCheckActive("OwnerLocation", _dataSourceTypeID, _analyzerOwnerLocationID, _analyzerOwnerID);
                if (_aimOwnerLocationID == 0)
                {
                    _headerMessage = "Sample will be ignored on import (Inactive Owner Location).";
                    _activeOwnerLocationStatus = 1;
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation;
                    _headerStatus = (Int32)SampleImportStatus.Ignore;
                    _errorOwnerLocation++;
                }
                else
                {
                    _aimOwnerLocationID = CheckCrossReference("OwnerLocation", _dataSourceTypeID, _analyzerOwnerLocationID, _analyzerOwnerID);
                    if (_aimOwnerLocationID == 0)
                    {
                        _headerMessage = "Missing Cross-Reference";
                        _xrefOwnerLocationStatus = 1;
                        _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocationXref;
                        _headerStatus = (Int32)SampleImportStatus.Cancelled;
                        _errorOwnerLocation++;
                    }
                }
            }
            return rtn;
        }
        public static Int32 CheckOwnerCrossReference(Int32 ownerID, DevExpress.XtraEditors.LookUpEdit lueOwner)
        {
            Int32 rtn = 0;

            _aimOwnerID = ownerID;
            if (lueOwner.Enabled == false)
            {
                _aimOwnerID = AnalyzerCheckActive("Owner", _dataSourceTypeID, _analyzerOwnerID, 0);
                if (_aimOwnerID == 0)
                {
                    _headerMessage = "Sample will be ignored on import (Inactive Owner).";
                    _activeOwnerStatus = 1;
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveOwner;
                    _headerStatus = (Int32)SampleImportStatus.Ignore;
                    _errorOwner++;
                }
                else
                {
                    _aimOwnerID = CheckCrossReference("Owner", _dataSourceTypeID, _analyzerOwnerID, 0);
                    if (_aimOwnerID == 0)
                    {
                        _headerMessage = "Missing Cross-Reference";
                        _xrefOwnerStatus = 1;
                        _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingOwnerXref;
                        _headerStatus = (Int32)SampleImportStatus.Cancelled;
                        _errorOwner++;
                    }
                }
            }
            return rtn;
        }
        public static Int32 CheckMaterialCrossReference(Int32 materialID, DevExpress.XtraEditors.LookUpEdit lueMaterial)
        {
            Int32 rtn = 0;

            _aimMaterialID = materialID;
            if (lueMaterial.Enabled == false)
            {
                _aimMaterialID = AnalyzerCheckActive("Material", _dataSourceTypeID, _analyzerMaterialID, 0);
                if (_aimMaterialID == 0)
                {
                    _headerMessage = "Sample will be ignored on import (Inactive Material).";
                    _activeMaterialStatus = 1;
                    _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.InactiveMaterial;
                    _headerStatus = (Int32)SampleImportStatus.Ignore;
                    _errorMaterial++;
                }
                else
                {
                    _aimMaterialID = CheckCrossReference("Material", _dataSourceTypeID, _analyzerMaterialID, 0);
                    if (_aimMaterialID == 0)
                    {
                        _headerMessage = "Missing Cross-Reference";
                        _xrefMaterialStatus = 1;
                        _headerSpecificMessageStatus = (Int32)SampleImportSpecificMessageDetail.MissingMaterialXref;
                        _headerStatus = (Int32)SampleImportStatus.Cancelled;
                        _errorMaterial++;
                    }
                }
            }
            return rtn;

        }

        //1/30/20 - moved here from both TXT/CSV import and EXCEL import
        public static Int32 InitializeHeaderVariablesAfterUpdate()
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

        //1/30/20 - moved here from both TXT/CSV import and EXCEL import

        //2/15/21
        //public static String SetExistingHeaderFieldsBeforeUpdate(String sampleCode)
        public static String SetExistingHeaderFieldsBeforeUpdate(String sampleCode, Int32 datasourceTypeID)
        {
            String rtn = "";
            SampleInfo sample = new SampleInfo();

            //------------------------------------------------------------------
            //7/13/21 - Question: Datasource on Sample is used with auto-generated samples, to get the next available sample# for the datasource,
            //			because there may be multiple datasources on an import file -- so shouldn't datasourcetypeid be included?

            //3/12/21 - changed this back, because datasourcetypeid on the sample header shouldn't really be used -- because
            //			one sample can have several datasourcetypes(which are on the AnalyzerNutrient and part of the results)
            //2/15/21
            //sample = AIMManager.GetSampleCodeForImports(sampleCode, datasourceTypeID);
            sample = AIMManager.GetSampleCodeForImports(sampleCode);

            //------------------------------------------------------------------

            if (sample != null)
            {
                if (_sampleDate == "")
                {
                    _sampleDate = sample.SampleDate.ToString();
                }
                if (_alternateCode == "")
                {
                    _alternateCode = sample.AlternateCode;
                }
                if (_invoiceNumber == "")
                {
                    _invoiceNumber = sample.InvoiceNumber;
                }
                if (_poNumber == "")
                {
                    _poNumber = sample.PoNumber;
                }
                if (_ticketNumber == "")
                {
                    _ticketNumber = sample.TicketNumber;
                }
                if (_vehicleNumber == "")
                {
                    _vehicleNumber = sample.VehicleNumber;
                }
                if (_billOfLading == "")
                {
                    _billOfLading = sample.BillOfLoad;
                }
                if (_contractNumber == "")
                {
                    _contractNumber = sample.ContractNumber;
                }
                if (_loadNumber == "")
                {
                    _loadNumber = sample.LoadNumber;
                }
                if (_lotNumber == "")
                {
                    _lotNumber = sample.LotNumber;
                }
                if (_notes == "")
                {
                    _notes = sample.Notes;
                }
            }
            return rtn;
        }

    }
}
