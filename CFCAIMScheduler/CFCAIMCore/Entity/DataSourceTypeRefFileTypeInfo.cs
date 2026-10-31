using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class DataSourceTypeRefFileTypeInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeRefFileTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeRefFileTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FileTypeID", SqlDbType.Int)]
		public Int32 FileTypeID { get; set; }			
		
		[DataReaderColumnMapping()]
		public String DataSourceTypeName { get; set; }

		[DataReaderColumnMapping()]
		public String FileTypeName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }		

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsSystem", SqlDbType.Bit)]
		public Boolean IsSystem { get; set; }

		#endregion
	}
}