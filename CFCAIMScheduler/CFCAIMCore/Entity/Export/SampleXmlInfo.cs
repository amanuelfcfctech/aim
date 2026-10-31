using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleXmlInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping()]
		public String SampleCode { get; set; }

		[DataReaderColumnMapping()]
		public DateTime SampleDate { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AIMOwnerID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AIMSupplierID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AIMSupplierLocationID { get; set; }

		[DataReaderColumnMapping()]
		public String BillOfLading { get; set; }

		[DataReaderColumnMapping()]
		public String PoNumber { get; set; }

		#endregion
	}
}
