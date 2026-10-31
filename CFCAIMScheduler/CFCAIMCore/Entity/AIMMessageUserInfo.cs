using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMMessageUserInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMessageUserID", SqlDbType.Int)]
		public Int32 AIMMessageUserID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMessageID", SqlDbType.Int)]
		public Int32 AIMMessageID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		#endregion
	}
}