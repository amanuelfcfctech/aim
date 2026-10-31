using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AnalyzerMaterialInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerMaterialID", SqlDbType.Int)]
        public Int32 AnalyzerMaterialID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerMaterialCode", SqlDbType.VarChar)]
        public String AnalyzerMaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerMaterialName", SqlDbType.VarChar)]
        public String AnalyzerMaterialName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceName { get; set; }
				
        #endregion
    }
}
