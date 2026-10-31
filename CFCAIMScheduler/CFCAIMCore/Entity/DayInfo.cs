using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class DayInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@DayID", SqlDbType.Int)]
		public Int32 DayID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DayNumber", SqlDbType.VarChar)]
		public String DayNumber { get; set; }

		[DataReaderColumnMapping()]
		public String UpdateStamp { get; set; }		
		#endregion
	}
}

