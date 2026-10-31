using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalyzerCrossReferenceMassUpdateInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AnalyzerCrossReferenceMassUpdateID", SqlDbType.Int)]
		public Int32 AnalyzerCrossReferenceMassUpdateID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerID", SqlDbType.Int)]
		public Int32 AnalyzerID { get; set; }

		//used for owner-location and source-location -- to get ownerid and sourceid
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AnalyzerIDSecond { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AnalyzerName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMID", SqlDbType.Int)]
		public Int32 AIMID { get; set; }

		//used for owner-location and source-location -- to get ownerid and sourceid
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AIMIDSecond { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		#endregion
	}
}


