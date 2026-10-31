using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ProductToleranceProductCodesInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

        #endregion
    }
}
