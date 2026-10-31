using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleQualityControlMassEntryDetailInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleQualityControlMassEntryDetailID", SqlDbType.Int)]
        public Int32 SampleQualityControlMassEntryDetailID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleQualityControlMassEntryID", SqlDbType.Int)]
        public Int32 SampleQualityControlMassEntryID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleQualityParameterID", SqlDbType.Int)]
        public Int32 SampleQualityParameterID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleQualityParameterName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SortOrder", SqlDbType.Int)]
        public Int32 SortOrder { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
        public Decimal ResultValue { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public String ResultValueString { get; set; }               

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
