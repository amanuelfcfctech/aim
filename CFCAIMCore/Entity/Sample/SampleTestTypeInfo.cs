using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleTestTypeInfo : DataObjectBase
    {
        #region Properties                

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTestTypeID", SqlDbType.Int)]
        public Int32 SampleTestTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleTestTypeName", SqlDbType.NVarChar)]
        public String SampleTestTypeName { get; set; }
       
        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        #endregion
    }
}
