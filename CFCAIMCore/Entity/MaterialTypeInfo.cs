using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class MaterialTypeInfo : DataObjectBase
    {
        [DataReaderColumnMapping()]
        public Int32 MaterialTypeID { get; set; }

        [DataReaderColumnMapping()]
        public String MaterialTypeName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }
    }
}
