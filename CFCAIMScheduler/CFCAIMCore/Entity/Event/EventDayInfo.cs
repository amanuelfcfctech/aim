using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class EventDayInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@EventDailyID", SqlDbType.Int)]
		public Int32 EventDailyID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@EventID", SqlDbType.Int)]
		public Int32 EventID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DayID", SqlDbType.Int)]
		public Int32 DayID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@RecurEvery", SqlDbType.Int)]
        public Int32 RecurEvery { get; set; }

        [DataReaderColumnMapping()]
		public String UpdateStamp { get; set; }		
		#endregion
	}
}
