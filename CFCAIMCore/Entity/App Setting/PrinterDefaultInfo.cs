using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class PrinterDefaultInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@PrinterDefaultID", SqlDbType.Int)]
        public Int32 PrinterDefaultID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@PrinterTypeID", SqlDbType.Int)]
        public Int32 PrinterTypeID { get; set; }
                
        [DataReaderColumnMapping(),
        SqlParameterMapping("@PrinterName", SqlDbType.VarChar)]
        public String PrinterName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String PrinterTypeName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        #endregion
    }
}
