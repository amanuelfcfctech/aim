using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AIMPriorityUsedValueInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMPriorityUsedValueID", SqlDbType.Int)]
		public Int32 AIMPriorityUsedValueID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Description", SqlDbType.VarChar)]
		public String Description { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }
		
		#endregion
	}
}
