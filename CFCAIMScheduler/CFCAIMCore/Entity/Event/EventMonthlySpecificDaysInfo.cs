using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class EventMonthlySpecificDaysInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventMonthlySpecificDaysID", SqlDbType.Int)]
        public Int32 EventMonthlySpecificDaysID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventMonthlyID", SqlDbType.Int)]
        public Int32 EventMonthlyID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DayID", SqlDbType.Int)]
        public Int32 DayID { get; set; }
     
        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}
