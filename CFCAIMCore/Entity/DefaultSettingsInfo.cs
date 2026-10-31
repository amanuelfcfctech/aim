using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class DefaultSettingsInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@DefaultSettingsID", SqlDbType.Int)]
		public Int32 DefaultSettingsID { get; set; }		

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		#endregion
	}
}