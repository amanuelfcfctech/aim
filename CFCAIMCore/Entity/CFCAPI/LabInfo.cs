using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.CFCAPI
{
    public class LabInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 LabID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CustomerID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@LabName", SqlDbType.VarChar)]
        public String LabName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@PNEKey", SqlDbType.VarChar)]
        public String PNEKey { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsActive", SqlDbType.Int)]
        public Int32 IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}
