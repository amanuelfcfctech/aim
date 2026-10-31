using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class DelimiterTypeInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 DelimiterTypeID { get; set; }

		[DataReaderColumnMapping()]
		public String Name { get; set; }

		[DataReaderColumnMapping()]
		public String Description { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }
		#endregion
	}
}