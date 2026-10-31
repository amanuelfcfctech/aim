using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Foss
{
	//[Serializable()]
	public class FossSampleResultInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FossSampleResultID", SqlDbType.Int)]
		public Int32 FossSampleResultID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FossSampleID", SqlDbType.Int)]
		public Int32 FossSampleID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ResultName", SqlDbType.NVarChar)]
		public String ResultName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
		public Decimal ResultValue { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime CreateStamp { get; set; }
		#endregion
	}
}