using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class DataSourceDetailInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceDetailID", SqlDbType.Int)]
		public Int32 DataSourceDetailID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceHeaderID", SqlDbType.Int)]
		public Int32 DataSourceHeaderID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String Name { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerDataSourceTypeID", SqlDbType.Int)]
		public Int32 AnalyzerDataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerDataSourceTypeName { get; set; }			

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		#endregion
	}
}


