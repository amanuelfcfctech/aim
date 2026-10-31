using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    public class FileColumnTypeInfo
    {
        #region Properties
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ColumnNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ColumnName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String InfoType { get; set; }
        #endregion
    }
}
