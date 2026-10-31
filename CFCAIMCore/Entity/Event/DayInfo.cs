using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Event
{
    [Serializable()]
    public class DayInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DayID", SqlDbType.Int)]
        public Int32 DayID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DayNumber", SqlDbType.NChar)]
        public String DayNumber { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        #endregion
    }
}
