using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Bruker
{
    public class BrukerSampleResultReceiveInfo
    {
		#region Properties
		public String Token { get; set; }

		[DataReaderColumnMapping()]
		public Int32 BrukerSampleResultReceiveID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BrukerSampleResultID", SqlDbType.Int)]
		public Int32 BrukerSampleResultID { get; set; }
		#endregion
	}
}
