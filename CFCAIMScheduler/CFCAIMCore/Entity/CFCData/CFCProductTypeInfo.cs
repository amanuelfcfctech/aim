using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCProductTypeInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 CFCProductTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ProductTypeName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMDatabaseID { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryTemplateDetailID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }


        #endregion
    }
}

