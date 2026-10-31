using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class GroundUnGroundInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 GroundUnGroundID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String GroundUnGroundString { get; set; }

        //1/18/24
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean isDefault { get; set; }

        //1/18/24
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean isPNE { get; set; }

        #endregion
    }
}