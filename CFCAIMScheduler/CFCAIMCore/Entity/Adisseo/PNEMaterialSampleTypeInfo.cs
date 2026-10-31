using Core.Library.Data;
using System;
using System.Data;


namespace CFCAIMCore.Entity.Adisseo
{
	[Serializable()]
	public class PNEMaterialSampleTypeInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@PNEMaterialSampleTypeID", SqlDbType.Int)]
		public Int32 PNEMaterialSampleTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@PNEMaterialSampleTypeCode", SqlDbType.VarChar)]
		public String PNEMaterialSampleTypeCode { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }
	}
}
