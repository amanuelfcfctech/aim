using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalyzerSupplierLocationCombinedInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerSupplierLocationCombinedID", SqlDbType.Int)]
		public Int32 AnalyzerSupplierLocationCombinedID { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerSupplierLocationCombinedName", SqlDbType.VarChar)]
		public String AnalyzerSupplierLocationCombinedName { get; set; }

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

