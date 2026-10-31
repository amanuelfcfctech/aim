using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleTestTypeGroupDetailInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTestTypeGroupDetailID", SqlDbType.Int)]
        public Int32 SampleTestTypeGroupDetailID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTestTypeGroupID", SqlDbType.Int)]
        public Int32 SampleTestTypeGroupID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleTestTypeGroupCode { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleTestTypeID", SqlDbType.Int)]
        public Int32 SampleTestTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleTestTypeName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SampleTestTypeDetailID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        //12/1/23
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SortOrder { get; set; }

        //12/1/23
        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceTypeName { get; set; }

        #endregion
    }
}
