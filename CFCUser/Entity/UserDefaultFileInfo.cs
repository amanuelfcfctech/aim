//12/6/21

using Core.Library.Data;
using System;

namespace CFCUser.Entity
{
    [Serializable()]
    public class UserDefaultFileInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public string UserName { get; set; }

        [DataReaderColumnMapping()]
        public string ConnectName { get; set; }

        #endregion
    }
}


