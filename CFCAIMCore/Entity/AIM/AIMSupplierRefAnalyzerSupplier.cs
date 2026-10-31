using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMSupplierRefAnalyzerSupplierInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMSupplierRefAnalyzerSupplierID", SqlDbType.Int)]
		public Int32 AIMSupplierRefAnalyzerSupplierID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMSupplierID", SqlDbType.Int)]
		public Int32 AIMSupplierID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerSupplierID", SqlDbType.Int)]
		public Int32 AnalyzerSupplierID { get; set; }

		[DataReaderColumnMapping(isOptional:true)]
		public String AnalyzerSupplierCode { get; set; }

		[DataReaderColumnMapping()]
		public String AnalyzerSupplierName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }
			
		[DataReaderColumnMapping(isOptional: true)]
		public String DataSourceTypeName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }


		#endregion
	}
}