using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleQualityEquationBuildInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleQualityEquationBuildID", SqlDbType.Int)]
        public Int32 SampleQualityEquationBuildID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleQualityEquationID", SqlDbType.Int)]
        public Int32 SampleQualityEquationID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ValueString", SqlDbType.VarChar)]
        public String ValueString { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Status", SqlDbType.Bit)]
        public Boolean Status { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleQualityParameterID", SqlDbType.Int)]
        public Int32 SampleQualityParameterID { get; set; }

        #endregion
    }
}
