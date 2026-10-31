using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class WeekInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@WeekID", SqlDbType.Int)]
        public Int32 WeekID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@WeekName", SqlDbType.NChar)]
        public String WeekName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}
