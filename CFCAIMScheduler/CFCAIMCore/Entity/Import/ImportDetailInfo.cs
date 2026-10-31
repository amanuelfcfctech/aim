using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ImportDetailInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ImportDetailID", SqlDbType.Int)]
		public Int32 ImportDetailID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ImportHeaderID", SqlDbType.Int)]
		public Int32 ImportHeaderID { get; set; }

		[DataReaderColumnMapping,
		SqlParameterMapping("@AnalyzerNutrientCode", SqlDbType.NVarChar)]
		public String AnalyzerNutrientCode { get; set; }

		[DataReaderColumnMapping,
		SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
		public Decimal ResultValue { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ResultValueString", SqlDbType.NVarChar)]
		public String ResultValueString { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UnitType", SqlDbType.NVarChar)]
		public String UnitType { get; set; }	
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DetailMessage", SqlDbType.NVarChar)]
		public String DetailMessage { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 DetailMessageStatus { get; set; }
		//(using enum SampleImportStatus)
		// 2=Reject
		// 3=Warning

		[DataReaderColumnMapping]
		public String Message { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Status", SqlDbType.Int)]
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

		//4/4/17
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerDataSourceTypeID", SqlDbType.Int)]
		public Int32 AnalyzerDataSourceTypeID { get; set; }

		//4/4/17
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerDataSourceTypeName { get; set; }

		//4/4/17
		[DataReaderColumnMapping(isOptional: true)]
		public Boolean XrefDatasourceExists { get; set; }

		//8/15/22
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SQLResultInfo", SqlDbType.NVarChar)]
		public String SQLResultIDInfo { get; set; }


		#endregion

	}
}
