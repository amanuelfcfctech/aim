using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AnalyzerAnalyseInfo : DataObjectBase
    {
        #region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerAnalyseID", SqlDbType.Int)]
        public Int32 AnalyzerAnalyseID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@Code", SqlDbType.VarChar)]
        public String Code { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@Name", SqlDbType.VarChar)]
        public String Name { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@Description", SqlDbType.VarChar)]
        public String Description { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        #endregion
    }
}
