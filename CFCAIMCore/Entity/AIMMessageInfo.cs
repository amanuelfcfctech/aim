using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMMessageInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMessageID", SqlDbType.Int)]
		public Int32 AIMMessageID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@TypeID", SqlDbType.Int)]
		public Int32 TypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Message", SqlDbType.NVarChar)]
		public String Message { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		#endregion
	}
}