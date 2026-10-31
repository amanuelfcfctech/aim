using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMSupplierLocationRefAnalyzerSupplierLocationInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMSupplierLocationRefAnalyzerSupplierLocationID", SqlDbType.Int)]
		public Int32 AIMSupplierLocationRefAnalyzerSupplierLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMSupplierLocationID", SqlDbType.Int)]
		public Int32 AIMSupplierLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMSupplierID", SqlDbType.Int)]
		public Int32 AIMSupplierID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierLocationCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String AIMSupplierLocationName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerSupplierLocationID", SqlDbType.Int)]
		public Int32 AnalyzerSupplierLocationID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerSupplierID", SqlDbType.Int)]
		public Int32 AnalyzerSupplierID { get; set; }

        //5/22/18
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerSupplierCode { get; set; }

        //4/2/18
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerSupplierName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerSupplierLocationCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerSupplierLocationName { get; set; }

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