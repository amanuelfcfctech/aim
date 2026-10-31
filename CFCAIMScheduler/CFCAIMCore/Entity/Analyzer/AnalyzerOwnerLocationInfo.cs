using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalyzerOwnerLocationInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("AnalyzerOwnerLocationID", SqlDbType.Int)]
		public Int32 AnalyzerOwnerLocationID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("AnalyzerOwnerID", SqlDbType.Int)]
		public Int32 AnalyzerOwnerID { get; set; }

		[DataReaderColumnMapping(),

        //1/17/18
        //SqlParameterMapping("AnalyzerOwnerLocationCode", SqlDbType.NChar)]
        SqlParameterMapping("AnalyzerOwnerLocationCode", SqlDbType.NVarChar)]

		public String AnalyzerOwnerLocationCode { get; set; }

		[DataReaderColumnMapping(),

        //1/17/18
		//SqlParameterMapping("AnalyzerOwnerLocationName", SqlDbType.NChar)]
        SqlParameterMapping("AnalyzerOwnerLocationName", SqlDbType.NVarChar)]

		public String AnalyzerOwnerLocationName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Int)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceUserName { get; set; }

		//4/25/17
		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerOwnerCode { get; set; }

		#endregion
	}
}
