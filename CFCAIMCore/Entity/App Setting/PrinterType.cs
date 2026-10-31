using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class PrinterTypeInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@PrinterTypeID", SqlDbType.Int)]
		public Int32 PrinterTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@PrinterTypeName", SqlDbType.VarChar)]
        public String PrinterTypeName { get; set; }

		[DataReaderColumnMapping(IsOptional = true),
		SqlParameterMapping("@PrinterName", SqlDbType.VarChar)]
        public String PrinterName { get; set; }
        
        [DataReaderColumnMapping()]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }
        #endregion
    }
}
