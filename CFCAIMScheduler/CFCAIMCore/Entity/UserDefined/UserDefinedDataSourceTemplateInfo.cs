using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class UserDefinedDataSourceTemplateInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@UserDefinedDataSourceTemplateID", SqlDbType.Int)]
		public Int32 UserDefinedDataSourceTemplateID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@UserDefinedDataSourceDefinitionID", SqlDbType.Int)]
		public Int32 UserDefinedDataSourceDefinitionID { get; set; }			
				
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SourceColumnName", SqlDbType.NVarChar)]
		public String SourceColumnName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SourceColumnNumber", SqlDbType.Int)]
		public Int32 SourceColumnNumber { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MapColumnID", SqlDbType.Int)]
		public Int32 MapColumnID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MapColumnName", SqlDbType.NVarChar)]
		public String MapColumnName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsManualEntry", SqlDbType.Bit)]
		public Boolean IsManualEntry { get; set; }				

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		public Int32 ImportPointer { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsDetail", SqlDbType.Bit)]
		public Boolean IsDetail { get; set; }

        //12/6/18
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@UseAsSampleCode", SqlDbType.Bit)]
        public Boolean UseAsSampleCode { get; set; }


        #endregion
    }
}
