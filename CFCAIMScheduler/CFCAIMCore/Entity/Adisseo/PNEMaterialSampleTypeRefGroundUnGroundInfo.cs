using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Adisseo
{
	[Serializable()]
	public class PNEMaterialSampleTypeRefGroundUnGroundInfo : DataObjectBase
	{
		[DataReaderColumnMapping(),
		SqlParameterMapping("@PNEMaterialSampleTypeRefGroundUnGroundID", SqlDbType.Int)]
		public Int32 PNEMaterialSampleTypeRefGroundUnGroundID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@PNEMaterialSampleTypeID", SqlDbType.Int)]
		public Int32 PNEMaterialSampleTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@GroundUnGroundID", SqlDbType.Int)]
		public Int32 GroundUnGroundID { get; set; }
		
		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String PNEMaterialSampleTypeCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String GroundUnGroundString { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

	}
}
