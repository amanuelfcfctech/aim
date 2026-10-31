using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.CFCAPI
{
	public class APILogInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 APILogID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@APISampleID", SqlDbType.Int)]
		public Int32 APISampleID { get; set; }
				
		[DataReaderColumnMapping()]
		public DateTime CreateStamp { get; set; }
		#endregion
	}
}
