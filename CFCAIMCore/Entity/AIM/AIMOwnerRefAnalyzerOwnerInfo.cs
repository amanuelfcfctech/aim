using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMOwnerRefAnalyzerOwnerInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerRefAnalyzerOwnerID", SqlDbType.Int)]
		public Int32 AIMOwnerRefAnalyzerOwnerID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerOwnerID", SqlDbType.Int)]
		public Int32 AnalyzerOwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerOwnerCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerOwnerName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMOwnerID", SqlDbType.Int)]
		public Int32 AIMOwnerID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String OwnerCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String OwnerName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceUserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 ImportHeaderID { get; set; }

        //4/18/18
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }


        #endregion
    }
}


