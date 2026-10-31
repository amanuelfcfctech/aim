using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleQualityInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleQualityID", SqlDbType.Int)]
        public Int32 SampleQualityID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleQualityParameterID", SqlDbType.Int)]
        public Int32 SampleQualityParameterID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleID", SqlDbType.Int)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ResultValueNumeric", SqlDbType.Decimal)]
        public Decimal ResultValueNumeric { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ResultValueString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleQualityParameterName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceTypeName { get; set; }

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


        #endregion
    }
}
