using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalyzerDataSourceTypeInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerDataSourceTypeID", SqlDbType.Int)]
		public Int32 AnalyzerDataSourceTypeID { get; set; }		

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerDataSourceTypeName", SqlDbType.VarChar)]
		public String AnalyzerDataSourceTypeName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerDataSourceTypeCode", SqlDbType.VarChar)]
		public String AnalyzerDataSourceTypeCode { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }		


		#endregion
	}
}

