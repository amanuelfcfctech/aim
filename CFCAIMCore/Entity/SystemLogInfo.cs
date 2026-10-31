using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SystemLogInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SystemLogID", SqlDbType.Int)]
		public Int32 SystemLogID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@TypeID", SqlDbType.Int)]
		public Int32 TypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Message", SqlDbType.NVarChar)]
		public String Message { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SourceLineNumber", SqlDbType.Int)]
		public Int32 SourceLineNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MethodName", SqlDbType.NVarChar)]
		public String MethodName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]		
		public String UserName { get; set; }

		#endregion
	}
}