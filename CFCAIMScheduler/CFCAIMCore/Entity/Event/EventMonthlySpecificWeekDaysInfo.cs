using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class EventMonthlySpecificWeekDaysInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventMonthlySpecificWeekDaysID", SqlDbType.Int)]
        public Int32 EventMonthlySpecificWeekDaysID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventMonthlyID", SqlDbType.Int)]
        public Int32 EventMonthlyID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@WeekID", SqlDbType.Int)]
        public Int32 WeekID { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}
