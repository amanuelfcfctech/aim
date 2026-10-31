using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class UserAccessInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 UserAccessID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 PermissionID { get; set; }
				
		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 UpdateUserID { get; set; }
			
		#endregion
	}
}