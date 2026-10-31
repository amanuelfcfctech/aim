using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class DateFormatInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@DateFormatID", SqlDbType.Int)]
		public Int32 DateFormatID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DateFormat", SqlDbType.NVarChar)]
		public String DateFormat { get; set; }

		[DataReaderColumnMapping()]
		public Int32 SortOrder { get; set; }

		[DataReaderColumnMapping()]
		public String FormatParameter { get; set; }

		[DataReaderColumnMapping()]
		public String Culture { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCUserID { get; set; }

		#endregion
	}
}

