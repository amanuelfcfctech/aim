using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCustom.Entity.Vitapro
{
    [Serializable()]
    public class ProductionStatusInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@ProductionStatusID", SqlDbType.Int)]
        public Int32 ProductionStatusID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ProductionStatusName", SqlDbType.VarChar)]
        public String ProductionStatusName { get; set; }
               
    }
}
