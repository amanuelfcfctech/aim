using Core.Library.Data;
using System;

namespace CFCAIMCore.Entity
{
	[Serializable()]
    public class AIMConfigInfo : DataObjectBase
    {
        [DataReaderColumnMapping()]
        public Int32 AIMDatabaseID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsActive { get; set; }
    }
}
