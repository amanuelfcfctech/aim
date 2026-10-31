using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleImportUserDefinedMapSelectionInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleImportUserDefinedMapSelectionID", SqlDbType.Int)]
		public Int32 SampleImportUserDefinedMapSelectionID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Description", SqlDbType.NVarChar)]
		public String Description { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		#endregion
	}
}
