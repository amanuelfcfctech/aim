using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AnalyzerSupplierLocationInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@AnalyzerSupplierLocationID", SqlDbType.Int)]
        public Int32 AnalyzerSupplierLocationID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AnalyzerSupplierID", SqlDbType.Int)]
        public Int32 AnalyzerSupplierID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AnalyzerSupplierLocationCode", SqlDbType.VarChar)]
        public String AnalyzerSupplierLocationCode { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AnalyzerSupplierLocationName", SqlDbType.VarChar)]
        public String AnalyzerSupplierLocationName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

       
        #endregion
    }
}

