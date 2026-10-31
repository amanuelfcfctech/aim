using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SystemImportCFCTechXMLSampleInfo : DataObjectBase
	{

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SystemImportCFCTechXMLSampleID", SqlDbType.Int)]
		public Int32 SystemImportCFCTechXMLSampleID { get; set; }

		public Int32 AnalyzerMaterialID { get; set; }
		public Int32 AnalyzerOwnerID { get; set; }
		public Int32 AnalyzerOwnerLocationID { get; set; }
		public Int32 AnalyzerSupplierID { get; set; }
		public Int32 AnalyzerSupplierLocationID { get; set; }

		
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerMaterialCode { get; set; }		
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerOwnerCode { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerOwnerName { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerOwnerLocationCode { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerOwnerLocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerSupplierCode { get; set; }		
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerSupplierName { get; set; }				
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerSupplierLocationCode { get; set; }		
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerSupplierLocationName { get; set; }


		//Sample Code
		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		//Alternate Code
		[DataReaderColumnMapping(isOptional: true)]
		public String AlternateCode { get; set; }

		//Sample Date
		[DataReaderColumnMapping(isOptional: true)]
		public String SampleDate { get; set; }

		//Analysis Date
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalysisDate { get; set; }

		public Int32 AIMMaterialID { get; set; }
		public Int32 AIMOwnerID { get; set; }
		public Int32 AIMOwnerLocationID { get; set; }
		public Int32 AIMSupplierID { get; set; }
		public Int32 AIMSupplierLocationID { get; set; }

		public Int32 MaterialTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String Message { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MessageStatus { get; set; }
		//(using enum SampleImportStatus)
		// 2=Reject
		// 3=Warning

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Status { get; set; }

		public String StatusDisplay
		{
			get
			{
				if (Status == 1)
				{
					return "Accept";
				}
				else if (Status == 2)
				{
					return "Reject";
				}
				else
				{
					return "";
				}
			}
		}

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefMaterialStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefOwnerStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefOwnerLocationStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefSupplierStatus { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 XrefSupplierLocationStatus { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String FilePathAndName { get; set; }

        //1/22/18
        [DataReaderColumnMapping(isOptional: true)]
        public String ContractNumber { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String PoNumber { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String InvoiceNumber { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String TicketNumber { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String LoadNumber { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String BillOfLoad { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String VehicleNumber { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String LotNumber { get; set; }

        //DETAIL - sample results
        [DataReaderColumnMapping(isOptional: true)]
		public List<CFCTechXmlResultsInfo> Results { get; set; }


	}
}
