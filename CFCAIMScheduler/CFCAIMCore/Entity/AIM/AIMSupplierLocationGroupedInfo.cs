using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]

    //7/26/19
    public class AIMSupplierLocationGroupedInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(ColumnName = "ID")]
        public Int32 ID { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String AIMSupplierLocationCode { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String SupplierLocationName { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public Boolean IsActive { get; set; }

        
        #endregion
    }
}

