using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Bruker
{
    public class BrukerCFCLogResultInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@Path", SqlDbType.NVarChar)]
		public String Path { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@FileName", SqlDbType.NVarChar)]
		public String FileName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataBlock", SqlDbType.NVarChar)]
		public String DataBlock { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@MethodID", SqlDbType.Int)]
		public Int32 MethodID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DateTimeAnalysis", SqlDbType.DateTime)]
		public DateTime DateTimeAnalysis { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@GMT", SqlDbType.Int)]
		public Int32 GMT { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ComponentName", SqlDbType.NVarChar)]
		public String ComponentName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsError", SqlDbType.Bit)]
		public Boolean IsError { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ErrorMessage", SqlDbType.VarChar)]
		public String ErrorMessage { get; set; }

		[DataReaderColumnMapping()]
		public DateTime CreateStamp { get; set; }
		#endregion
	}
}
