using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SystemMasterAndDatabaseFilesInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@DatabaseName", SqlDbType.VarChar)]
        public String DatabaseName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@LogicalName", SqlDbType.VarChar)]
        public String LogicalName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@PhysicalName", SqlDbType.VarChar)]
        public String PhysicalName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TypeOfFile", SqlDbType.VarChar)]
        public String TypeOfFile { get; set; }
       
        #endregion
    }
}

