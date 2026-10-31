using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class ImportConfigInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@ImportConfigID", SqlDbType.Int)]
		public Int32 ImportConfigID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Name", SqlDbType.VarChar)]
		public String Name { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProcessDirectory", SqlDbType.VarChar)]
		public String ProcessDirectory { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@BackUpDirectory", SqlDbType.VarChar)]
		public String BackUpDirectory { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@FileTypeID", SqlDbType.Int)]
		//public FileTypeEnum FileTypeID { get; set; }
        public Int32 FileTypeID { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@DelimiterTypeID", SqlDbType.Int)]
		public Int32 DelimiterTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		//public DataSourceTypeEnum DataSourceTypeID { get; set; }
        public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@AIMDatabaseID", SqlDbType.Int)]
		public Int32 AIMDatabaseID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping()]
		public String UserName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsCreateSample", SqlDbType.Bit)]
		public Boolean IsCreateSample { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExcelSheetNumber", SqlDbType.Int)]
		public Int32 ExcelSheetNumber { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ServerName", SqlDbType.VarChar)]
		public String ServerName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SqlUserName", SqlDbType.VarChar)]
		public String SqlUserName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SqlPassword", SqlDbType.VarChar)]
		public String SqlPassword { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsSqlAuthentication", SqlDbType.Bit)]
		public Boolean IsSqlAuthentication { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DatabaseName", SqlDbType.VarChar)]
		public String DatabaseName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ConnectName", SqlDbType.VarChar)]
		public String ConnectName { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SelectRequiredAimFields", SqlDbType.Bit)]
		public Boolean SelectRequiredAimFields { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@WebServiceID", SqlDbType.Int)]
		public Int32 WebServiceID { get; set; }

		#endregion
	}
}
