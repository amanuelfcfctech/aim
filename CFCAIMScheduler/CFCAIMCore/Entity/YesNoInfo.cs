using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class YesNoInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 YesNoID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String YesNoString { get; set; }

        #endregion
    }
}