using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity.CFCAPI
{
    public class InstrumentInfo : DataObjectBase
    {
		#region Properties
		[DataReaderColumnMapping()]
		public Int32 InstrumentID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@LabID", SqlDbType.Int)]
		public String LabID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@InstrumentSerial", SqlDbType.NVarChar)]
		public String InstrumentSerial { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@InstrumentTypeID", SqlDbType.Int)]
		public String InstrumentTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMInstrumentKey", SqlDbType.VarChar)]
		public String AIMInstrumentKey { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Int)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@PNEKey", SqlDbType.NVarChar)]
		public String PNEKey { get; set; }

		#endregion
	}
}
