using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleQualityParameterInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleQualityParameterID", SqlDbType.Int)]
        public Int32 SampleQualityParameterID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleQualityParameterName", SqlDbType.NVarChar)]
        public String SampleQualityParameterName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        
        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceUserName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ParameterIsNumeric", SqlDbType.Bit)]
        public Boolean ParameterIsNumeric { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        //4/25/22 - added for Butterball
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@UnitTypeID", SqlDbType.Int)]
        public Int32 UnitTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserTypeDescription { get; set; }


        #endregion
    }
}
