using System;
using System.Data;
using Core.Library.Data;

namespace CFCUser.Entity
{
    public class RolesInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public string RoleName { get; set; }
        #endregion
    }
}
