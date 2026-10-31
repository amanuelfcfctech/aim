using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleTestTypeResultInfo : DataObjectBase
    {
        #region Properties         

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTestTypeResultID", SqlDbType.Int)]
        public Int32 SampleTestTypeResultID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleID", SqlDbType.Int)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTestTypeDetailID", SqlDbType.Int)]
        public Int32 SampleTestTypeDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsIncluded", SqlDbType.Bit)]
        public Boolean IsIncluded { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleTestTypeName { get; set; }

        
        #endregion
    }
}
