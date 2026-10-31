using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace CFCAIM.AIMUtilities.Imports
{
    public class ImportCSVFile
	{		
        public static String _tableName = "Sample";
		public static Boolean _blnWriteHeader = false;
		public static Boolean _blnWriteDetail = false;
		public static String _saveSampleCode = "";
		public static Int32 _saveHeaderID = 0;
		public static Boolean _sampleCodeExists = false;
		public static Int32 _existingHeaderID = 0;

		public static String _material = "";
		public static String _owner = "";
		public static String _ownerLocation = "";
		public static String _supplier = "";
		public static String _supplierLocation = "";
		public static String _sampleCode = "";
		public static String _alternateCode = "";
		public static String _sampleDate = "";
		public static DateTime _dtSampleDate = DateTime.Today;
		public static String _invoiceNumber = "";
		public static String _poNumber = "";
		public static String _ticketNumber = "";
		public static String _vehicleNumber = "";

		public static Int32 _headerID; 
		public static Int32 _headerMessageStatus = 0;
		public static String _headerMessage = "";
		public static Int32 _sampleCounter = 0;	  //fields used for generated sample-code	
        public static String _dateForGeneratedSampleCode = DateTime.Now.ToString("yyyyMMdd");
        public static Boolean _firstRec = true; //used for auto-generated sample codes

		public static String _analyzerNutrientCode = "";
		public static String _unitType = "";
		public static String _resultValueString = "";
		public static Int32 _detailID = 1;

		public static Int32 _aimMaterialID = 0;
		public static Int32 _aimOwnerID = 0;
		public static Int32 _aimOwnerLocationID = 0;
		public static Int32 _aimSupplierID = 0;
		public static Int32 _aimSupplierLocationID = 0;
		public static String _billOfLading = "";
		public static String _contractNumber = "";
		public static String _loadNumber = "";
		public static String _lotNumber = "";
		public static Int32 _dateFormatID = 0;                              
        public static List<ImportHeaderInfo> _workHeader = new List<ImportHeaderInfo>();					
		public bool WorkerReportsProgress { get; set; }

        

        //2/1/18
        public static Int32 AddResults(Int32 pointer)
        {
            if (_workHeader[pointer].DetailImport == null)
            {
                _workHeader[pointer].DetailImport = new List<ImportDetailInfo>();
            }
            _workHeader[pointer].DetailImport.Add(new ImportDetailInfo
            {
                ImportDetailID = _detailID,
                ImportHeaderID = _saveHeaderID,
                AnalyzerNutrientCode = _analyzerNutrientCode,
                ResultValueString = _resultValueString,
                UnitType = _unitType               
                
            }
            );
            return 0;
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

				//6/6/17
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

				ExistingImportHeaderID = _existingHeaderID

			}
			);

			return 0;

		}

		public static String SetRequiredVariables(String fromScreen,
								DevExpress.XtraEditors.LookUpEdit lueMaterial,
								DevExpress.XtraEditors.LookUpEdit lueOwner,
								DevExpress.XtraEditors.LookUpEdit lueOwnerLocation,
								DevExpress.XtraEditors.LookUpEdit lueSource,
								DevExpress.XtraEditors.LookUpEdit lueSourceLocation)
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
			return 0;
		}
		public static Int32 IncrementCountersAndIDs()
		{
			_blnWriteHeader = false;
			_saveHeaderID = _headerID;
			//_headerID = _headerID + 1;
			_headerID++;
			_detailID = 1;
			_firstRec = false;

			return 0;
		}
		public static Int32 InitializeFunctionGlobals()
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

			_saveSampleCode = "";
			_headerMessageStatus = 0;
			_headerMessage = "";

			_detailID = 1;
			_analyzerNutrientCode = "";
			_unitType = "";
			_resultValueString = "";

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

			//4/20/17
			_dateFormatID = 0;

			//6/6/17
			_billOfLading = "";
			_contractNumber = "";
			_loadNumber = "";
			_lotNumber = "";

            _sampleCodeExists = false;

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
        public static Int32 InitializedAIMRequiredFields()
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
						_supplierLocation = str;
						break;
					default:
						break;
				}
			}
			return "";
		}

	}


}
