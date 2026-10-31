using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleImportRequiredFieldsInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping]
		public Boolean Material { get; set; }

		[DataReaderColumnMapping]
		public Boolean Source { get; set; }

		[DataReaderColumnMapping]
		public Boolean SourceLocation { get; set; }

		[DataReaderColumnMapping]
		public Boolean Owner { get; set; }

		[DataReaderColumnMapping]
		public Boolean OwnerLocation { get; set; }

		#endregion

	}
}
