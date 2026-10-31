using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class SampleAIMRefCFCUserInfo : DataObjectBase
    {
        #region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleAIMRefCFCUserID", SqlDbType.Int)]
		public Int32 SampleAIMRefCFCUserID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleAIMID", SqlDbType.Int)]
		public Int32 SampleAIMID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsUserFlag", SqlDbType.Bit)]
		public Boolean IsUserFlag { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }
        #endregion
    }
}

