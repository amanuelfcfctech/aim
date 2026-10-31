using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class UnitTypeInfo : DataObjectBase
	{
		[DataReaderColumnMapping()]
		public Int32 UnitTypeID { get; set; }

		[DataReaderColumnMapping()]
		public String UnitTypeCode { get; set; }
		
		[DataReaderColumnMapping()]
		public String UnitTypeDescription { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsActive { get; set; }
	}
}

