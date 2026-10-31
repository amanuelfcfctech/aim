using Core.Library.Data;
using System;

namespace CFCAIMCore.Entity.API
{
    public class APIStatusInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(IsOptional = true)]
        public Int32 APIStatusID { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public Boolean IsAPIDown { get; set; }
        #endregion
    }
}
