using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleExportTemplateInfo : DataObjectBase
	{
		#region Properties

		//-----------------------------------------------

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleExportTemplateID", SqlDbType.Int)]
		public Int32 SampleExportTemplateID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@TemplateName", SqlDbType.NVarChar)]
		public String TemplateName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@FileTypeID", SqlDbType.Int)]
		public Int32 FileTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DelimiterTypeID", SqlDbType.Int)]
		public Int32 DelimiterTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExportPath", SqlDbType.NVarChar)]
		public String ExportPath { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@ExportFileName", SqlDbType.NVarChar)]
		public String ExportFileName { get; set; }
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@UpdateStamp", SqlDbType.DateTime)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		#endregion
	}
}
