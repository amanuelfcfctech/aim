using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class AnalyzerInstrumentInfo : DataObjectBase
	{
		#region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@InstrumentID", SqlDbType.Int)]
		public Int32 InstrumentID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@InstrumentCode", SqlDbType.VarChar)]
		public String InstrumentCode { get; set; }
		
		[DataReaderColumnMapping(),
		SqlParameterMapping("@Description", SqlDbType.VarChar)]
		public String Description { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ArchivedFolder", SqlDbType.VarChar)]
		public String ArchivedFolder { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@ProximateFolder", SqlDbType.VarChar)]
		public String ProximateFolder { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SpectraFolder", SqlDbType.VarChar)]
		public String SpectraFolder { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@FileTypeID", SqlDbType.Int)]
		public FileTypeEnum FileTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public DataSourceTypeEnum DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsActive", SqlDbType.Int)]
		public Int32 IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }
		
		#endregion
	}
}
