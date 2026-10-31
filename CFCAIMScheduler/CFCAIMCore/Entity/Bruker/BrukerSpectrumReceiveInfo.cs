using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Bruker
{
    public class BrukerSpectrumReceiveInfo
    {
		#region Properties
		public String Token { get; set; }

		[DataReaderColumnMapping()]
		public Int32 BrukerSpectrumReceiveID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BrukerSpectrumID", SqlDbType.Int)]
		public Int32 BrukerSpectrumID { get; set; }
		#endregion
	}
}
