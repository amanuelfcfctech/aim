using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMOwnerLocationRefAnalyzerOwnerLocationInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerLocationRefAnalyzerOwnerLocationID", SqlDbType.Int)]
		public Int32 AIMOwnerLocationRefAnalyzerOwnerLocationID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerOwnerLocationID", SqlDbType.Int)]
		public Int32 AnalyzerOwnerLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerOwnerID", SqlDbType.Int)]
		public Int32 AnalyzerOwnerID { get; set; }

        //4/2/18
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerOwnerName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerOwnerLocationCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerOwnerLocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerID", SqlDbType.Int)]
		public Int32 AIMOwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerLocationID", SqlDbType.Int)]
		public Int32 AIMOwnerLocationID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public String LocationCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String LocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceUserName { get; set; }

        //4/18/18
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

		//1/8/21
		[DataReaderColumnMapping(isOptional: true)]
		public String OwnerName { get; set; }

		#endregion
	}
}


