using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.ComponentModel;
using System.Linq;


namespace CFCAIM.AIMUtilities.Imports
{
    class ImportTextFile
	{
		
        //TODO - the stp that populates this field should be changed to use table instead of hard-coded values in stp
		public static SampleImportColumnNamesInfo _mapColumns = new SampleImportColumnNamesInfo();

        //changed fields to local "globals"
        public static Int32 _aimMaterialID = 0;
        public static Int32 _aimOwnerID = 0;
        public static Int32 _aimOwnerLocationID = 0;
        public static Int32 _aimSupplierID = 0;
        public static Int32 _aimSupplierLocationID = 0;

        //header fields for portrait
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
        public static Int32 _headerMessageStatus = 0;
        public static String _headerMessage = "";	 
        public static Boolean _blnWriteHeader = false;
        public static Boolean _blnWriteDetail = false;        
        public static Int32 _detailID = 0;
        public static Int32 _saveHeaderID = 0;        
        public static Int32 _headerPointerForDetail = 0;      
        public static String _saveSampleCode = "";
		public static Boolean _sampleCodeExists = false;
		public static Int32 _existingHeaderID = 0;

        //2/18/19 added to be same as CSV
        public static String _billOfLading = "";
        public static String _contractNumber = "";
        public static String _loadNumber = "";
        public static String _lotNumber = "";
        public static Int32 _dateFormatID = 0;
        //-----------------------------------

        //fields for generated sample-code
        public static Boolean _firstRec = true;
        public static Int32 _sampleCounter = 0;
        public static String _dateForGeneratedSampleCode = DateTime.Now.ToString("yyyyMMdd");

        public static List<ImportHeaderInfo> _workHeader = new List<ImportHeaderInfo>();
                
  
        //2/19/18 - added to be the same as CSV import
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

            _aimMaterialID = 0;
            _aimOwnerID = 0;
            _aimOwnerLocationID = 0;
            _aimSupplierID = 0;
            _aimSupplierLocationID = 0;

            _sampleCounter = 0;   //fields used for generated sample-code	
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

            //_analyzerNutrientCode = "";
            //_unitType = "";
            //_resultValueString = "";

            _sampleCodeExists = false;
            _headerPointerForDetail = 0;

            return 0;
        }

    }
}
