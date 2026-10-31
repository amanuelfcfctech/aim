using System;
using System.Collections.Generic;
using System.Data;
using Core.Library.Data;

namespace CFCUser.Entity
{
	[Serializable()]
	public class UserMenuAccessInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(IsOptional = true)]
		public Int32 UserMenuAccessID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 MenuItemsID { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 UpdateUserID { get; set; }		

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsNotAllowed", SqlDbType.Bit)]
		public Boolean IsNotAllowed { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String Description { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 ParentNo { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Level1 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Level2 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Level3 { get; set; }

		//Currently not used
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Level4 { get; set; }

		//Currently not used
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Level5 { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SortOrder { get; set; }





		#endregion
	}
}













































































