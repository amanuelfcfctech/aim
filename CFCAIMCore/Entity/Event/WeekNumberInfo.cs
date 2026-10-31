using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class WeekNumberInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@WeekNumberID", SqlDbType.Int)]
        public Int32 WeekNumberID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@WeekNumberName", SqlDbType.NChar)]
        public String WeekNumberName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}
