using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class MonthInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@MonthID", SqlDbType.Int)]
        public Int32 MonthID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@MonthName", SqlDbType.NChar)]
        public String MonthName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}
