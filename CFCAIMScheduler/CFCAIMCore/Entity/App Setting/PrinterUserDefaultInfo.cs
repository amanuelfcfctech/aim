using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class PrinterUserDefaultInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@PrinterUserDefaultID", SqlDbType.Int)]
        public Int32 PrinterUserDefaultID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@PrinterTypeID", SqlDbType.Int)]
        public Int32 PrinterTypeID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@PrinterName", SqlDbType.VarChar)]
        public String PrinterName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@UpdateCFCUserID", SqlDbType.Int)]
        public Int32 UpdateCFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }        
                
        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String PrinterTypeName { get; set; }


        #endregion
    }
}
