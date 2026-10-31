using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Foss
{
    public class FossSpectrumReceiveInfo
    {
		#region Properties
		public String Token { get; set; }

		[DataReaderColumnMapping()]
		public Int32 FossSpectrumReceiveID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FossSpectrumID", SqlDbType.Int)]
		public Int32 FossSpectrumID { get; set; }
		#endregion
	}
}
