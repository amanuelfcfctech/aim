using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleQualityControlMassEntryInfo : DataObjectBase
    {
        #region Properties			

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleQualityControlMassEntryID", SqlDbType.Int)]
        public Int32 SampleQualityControlMassEntryID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleQualityControlMassEntryName", SqlDbType.NVarChar)]
        public String SampleQualityControlMassEntryName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceTypeName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public List<SampleQualityControlMassEntryDetailInfo> MassEntryDetail { get; set; }

        #endregion
    }
}
