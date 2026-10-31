using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class EventWeeklyInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventWeeklyID", SqlDbType.Int)]
        public Int32 EventWeeklyID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventID", SqlDbType.Int)]
        public Int32 EventID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@WeekID", SqlDbType.Int)]
        public Int32 WeekID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@RecurEvery", SqlDbType.Int)]
        public Int32 RecurEvery { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String WeekDayName { get; set; }

        #endregion

    }
}
