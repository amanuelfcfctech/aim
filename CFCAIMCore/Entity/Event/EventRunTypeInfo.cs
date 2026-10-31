using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class EventRunTypeInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventRunTypeID", SqlDbType.Int)]
        public Int32 EventRunTypeID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventRunTypeName", SqlDbType.VarChar)]
        public String EventRunTypeName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}
