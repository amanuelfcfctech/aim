using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleResultTemplateDetailInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleResultTemplateDetailID", SqlDbType.Int)]
		public Int32 SampleResultTemplateDetailID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleResultTemplateID", SqlDbType.Int)]
		public Int32 SampleResultTemplateID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerNutrientID", SqlDbType.Int)]
		public Int32 AnalyzerNutrientID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
		public Decimal ResultValue { get; set; }

		//5/8/17
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CurrentValue", SqlDbType.Decimal)]
		public Decimal CurrentValue { get; set; }

		//5/15/17
		//[DataReaderColumnMapping(isOptional: true),
		//SqlParameterMapping("@ResultValueString", SqlDbType.NVarChar)]
		//public String ResultValueString { get; set; }
		[DataReaderColumnMapping(isOptional: true)]
		public String ResultValueString { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerNutrientCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UnitType { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SortOrder", SqlDbType.Int)]
		public Int32 SortOrder { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }
				
		#endregion
	}
}
