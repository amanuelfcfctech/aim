using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleTableStructureInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(isOptional: true)]
		public String ColumnName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String DataType { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 Size { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String PrecisionScale { get; set; }

		#endregion
	}
}
