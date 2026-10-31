using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalyzerDataSourceTypeRefDataSourceTypeInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerDataSourceTypeRefDataSourceTypeID", SqlDbType.Int)]
		public Int32 AnalyzerDataSourceTypeRefDataSourceTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerDataSourceTypeID", SqlDbType.Int)]
		public Int32 AnalyzerDataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerDataSourceTypeName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public String Name { get; set; }

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


