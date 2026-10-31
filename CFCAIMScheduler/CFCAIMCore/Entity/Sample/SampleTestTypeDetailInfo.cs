using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleTestTypeDetailInfo : DataObjectBase
    {
        #region Properties         

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTestTypeDetailID", SqlDbType.Int)]
        public Int32 SampleTestTypeDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTestTypeID", SqlDbType.Int)]
        public Int32 SampleTestTypeID { get; set; }
        
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SortOrder", SqlDbType.Int)]
        public Int32 SortOrder { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }


        #endregion
    }
}
