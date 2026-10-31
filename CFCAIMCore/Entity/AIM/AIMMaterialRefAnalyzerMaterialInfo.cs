using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMMaterialRefAnalyzerMaterialInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialRefAnalyzerMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialRefAnalyzerMaterialID { get; set; }		
		
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerMaterialID", SqlDbType.Int)]
		public Int32 AnalyzerMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerMaterialCode { get; set; }

		[DataReaderColumnMapping()]
		public String AIMMaterialCode { get; set; }

		[DataReaderColumnMapping()]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String MaterialTypeName { get; set; }		

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerMaterialName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMLocationName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String DataSourceTypeName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MaterialTypeID { get; set; }

		

        #endregion


    }
}