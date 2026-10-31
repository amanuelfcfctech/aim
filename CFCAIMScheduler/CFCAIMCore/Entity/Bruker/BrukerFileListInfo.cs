using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.Bruker
{
	public class BrukerFileListInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@Path", SqlDbType.NVarChar)]
		public String Path { get; set; }

		[DataReaderColumnMapping(columnName: "File_Name"),
		SqlParameterMapping("@FileName", SqlDbType.NVarChar)]
		public String FileName { get; set; }

		[DataReaderColumnMapping(columnName: "Name_Part1"),
		SqlParameterMapping("@NamePart1", SqlDbType.NVarChar)]
		public String NamePart1 { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Extension", SqlDbType.Int)]
		public String Extension { get; set; }

		[DataReaderColumnMapping(columnName: "Date_Time_Entry"),
		SqlParameterMapping("@DateTimeEntry", SqlDbType.DateTime)]
		public DateTime DateTimeEntry { get; set; }

		[DataReaderColumnMapping(columnName: "GMT_Entry"),
		SqlParameterMapping("@GMTEntry", SqlDbType.Int)]
		public Int32 GMTEntry { get; set; }

		[DataReaderColumnMapping(columnName: "Date_Time_Last_Change"),
		SqlParameterMapping("@DateTimeLastChange", SqlDbType.DateTime)]
		public DateTime DateTimeLastChange { get; set; }

		[DataReaderColumnMapping(columnName: "GMT_Last_Change"),
		SqlParameterMapping("@GMTLastChange", SqlDbType.Int)]
		public String GMTLastChange { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Spectrum", SqlDbType.VarBinary)]
		public Byte[] Spectrum { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DBCodePage", SqlDbType.Int)]
		public Int32 DBCodePage { get; set; }
		#endregion

	}
}
