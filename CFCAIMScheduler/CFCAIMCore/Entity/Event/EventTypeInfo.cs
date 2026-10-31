using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class EventTypeInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventTypeID", SqlDbType.Int)]
        public Int32 EventTypeID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventTypeName", SqlDbType.VarChar)]
        public String EventTypeName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        public String EventTypeNameString
        {
            get
            {
                if (EventTypeName == "ImportUserDefined")
                {
                    return "Import - User-Defined";
                }
                else if (EventTypeName == "ImportSystem")
                {
                    return "Import - System";
                }
                else if (EventTypeName == "Export")
                {
                    return "Export";
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion
    }
}
