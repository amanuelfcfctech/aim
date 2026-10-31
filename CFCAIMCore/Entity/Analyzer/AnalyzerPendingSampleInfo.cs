using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalyzerPendingSampleInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerPendingSampleID", SqlDbType.Int)]
		public Int32 AnalyzerPendingSampleID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleID", SqlDbType.Int)]
		public Int32 SampleID { get; set; }

		[DataReaderColumnMapping()]
		public DateTime CreatedStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SentStamp", SqlDbType.DateTime)]
		public DateTime SentStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@RequestCode", SqlDbType.NVarChar)]
		public String RequestCode { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ReceivedStamp", SqlDbType.DateTime)]
		public DateTime ReceivedStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SpectraName", SqlDbType.NVarChar)]
		public String SpectraName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@NIRAnalysis", SqlDbType.DateTime)]
		public DateTime NIRAnalysis { get; set; }

		//1/16/24
		[DataReaderColumnMapping(isOptional: true)]
		public String SampleCode { get; set; }

		#endregion
	}
}