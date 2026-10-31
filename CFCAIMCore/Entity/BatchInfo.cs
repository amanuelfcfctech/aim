using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	public class BatchInfo : DataObjectBase
	{
		#region Properties
		public String Token { get; set; }

		[DataReaderColumnMapping()]
		public Int32 BatchID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CustomerID", SqlDbType.Int)]
		public Int32 CustomerID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@InstrumentID", SqlDbType.Int)]
		public Int32 InstrumentID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime StartStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime EndStamp { get; set; }
		#endregion

	}
}
