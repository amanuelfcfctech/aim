using Core.Library.Data;
using System;
using System.Data;

namespace CFCAIMCore.Entity.Bruker
{
	public class BrukerSpectrumListInfo : DataObjectBase
	{
		[DataReaderColumnMapping()]
		public Int32 BrukerSpectrumID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@BrukerSampleID", SqlDbType.Int)]
		public Int32 BrukerSampleID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ValueProductInfo1", SqlDbType.NVarChar)]
		public String ValueProductInfo1 { get; set; }

		[DataReaderColumnMapping()]
		public DateTime CreateStamp { get; set; }
	}
}