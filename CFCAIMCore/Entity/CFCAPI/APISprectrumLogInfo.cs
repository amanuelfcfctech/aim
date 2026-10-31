using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.CFCAPI
{
	public class APISprectrumLogInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 APISprectrumLogID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@APISpectrumID", SqlDbType.Int)]
		public Int32 APISpectrumID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@APINIRType", SqlDbType.Int)]
		public Int32 APINIRType { get; set; }

		[DataReaderColumnMapping()]
		public DateTime CreateStamp { get; set; }
		#endregion
	}
}

