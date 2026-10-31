using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class InternalImportValidateSampleInfo : DataObjectBase
	{

		[DataReaderColumnMapping(),
		SqlParameterMapping("@InternalImportValidateSampleID", SqlDbType.Int)]
		public Int32 InternalImportValidateSampleID { get; set; }

		public Int32 AnalyzerMaterialID { get; set; }
		public Int32 AnalyzerOwnerID { get; set; }
		public Int32 AnalyzerOwnerLocationID { get; set; }
		public Int32 AnalyzerSupplierID { get; set; }
		public Int32 AnalyzerSupplierLocationID { get; set; }

		//Analyzer Material Code		
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerMaterialCode { get; set; }
		//Analyzer Material Name		
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerMaterialName { get; set; }

		//Analyzer Supplier	Code
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerSupplierCode { get; set; }
		//Analyzer Supplier	Name
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerSupplierName { get; set; }

		//Analyzer Supplier Location Code
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerSupplierLocationCode { get; set; }
		//Analyzer Supplier Location Name
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

		//DETAIL - sample results
		[DataReaderColumnMapping(isOptional: true)]
		public List<NutriQuestXmlPropertyInfo> Property { get; set; }


	}
}
