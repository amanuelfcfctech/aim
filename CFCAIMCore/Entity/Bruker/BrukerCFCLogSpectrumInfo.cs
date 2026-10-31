using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Bruker
{
    public class BrukerCFCLogSpectrumInfo
    {
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@Path", SqlDbType.NVarChar)]
		public String Path { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@FileName", SqlDbType.NVarChar)]
		public String FileName { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsError { get; set; }

		[DataReaderColumnMapping()]
		public DateTime CreateStamp { get; set; }

		[DataReaderColumnMapping()]
		public String ErrorMessage { get; set; }

		#endregion

	}
}
