using System;
using System.Data;
using Core.Library.Data;

namespace CFCUser.Entity
{
	[Serializable()]
	public class PermissionInfo : DataObjectBase
	{
		#region Properties
		
		[DataReaderColumnMapping(),
		SqlParameterMapping("@PermissionID", SqlDbType.Int)]
		public Int32 PermissionID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Description", SqlDbType.Int)]
		public String Description { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Int)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }				

		#endregion
	}
}


