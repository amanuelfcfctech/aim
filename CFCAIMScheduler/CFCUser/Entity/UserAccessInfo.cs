using System;
using System.Data;
using Core.Library.Data;

namespace CFCUser.Entity
{
	[Serializable()]
	public class UserAccessInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@UserAccessID", SqlDbType.Int)]
		public Int32 UserAccessID { get; set; }		

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@PermissionID", SqlDbType.Int)]
		public Int32 PermissionID { get; set; }
		
		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@UpdateUserID", SqlDbType.Int)]
		public Int32 UpdateUserID { get; set; }

		[DataReaderColumnMapping()]
		public String Description { get; set; }

		#endregion
	}
}


