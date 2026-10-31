using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    public class TableColumnInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 TableID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String TableName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ColumnID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ColumnName { get; set; }
        #endregion
    }
}
