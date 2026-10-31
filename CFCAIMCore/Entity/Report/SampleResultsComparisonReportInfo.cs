using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleResultsComparisonReportInfo : DataObjectBase
	{		
		#region Properties
		
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping(ColumnName = "AIMSourceID", IsOptional = true)]
		public Int32 AIMSupplierID { get; set; }

		[DataReaderColumnMapping(ColumnName = "AIMSourceLocationID", IsOptional = true)]
		public Int32 AIMSupplierLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMNutrientID_1 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMNutrientID_2 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMNutrientID_3 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double Val1 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Double Val2 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String OwnerName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String LocationName { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceName", IsOptional = true)]
		public String SupplierName { get; set; }

		[DataReaderColumnMapping(ColumnName = "SourceLocationName", IsOptional = true)]
		public String SupplierLocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName { get; set; }				

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String TicketNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCPlantID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCPlantCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 CFCPlantName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 QueryTemplateID { get; set; }			
		
		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceName1 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceName2 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean UseAnalysisDate { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime AnalysisDate { get; set; }

		#endregion
	}
}