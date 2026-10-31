using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AnalyzerMaterialRefAnalyzerAnalyseInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
		public Int32 AnalyzerMaterialRefAnalyzerAnalyseID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AnalyzerMaterialID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AnalyzerAnalyseID { get; set; }

		[DataReaderColumnMapping()]
		public DataSourceTypeEnum DataSourceTypeID { get; set; }

		[DataReaderColumnMapping()]
		public String Code { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }
        #endregion
    }
}
