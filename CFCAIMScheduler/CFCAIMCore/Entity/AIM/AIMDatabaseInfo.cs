using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AIMDatabaseInfo : DataObjectBase
    {
        #region Properties
				
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMDatabaseID", SqlDbType.Int)]
		public Int32 AIMDatabaseID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Name", SqlDbType.NVarChar)]
		public String Name { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCPath", SqlDbType.NVarChar)]
		public String CFCPath { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DefaultForPropose", SqlDbType.Int)]
		public MathTypeEnum DefaultForPropose { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }
		
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMPriorityUsedValueID", SqlDbType.Int)]
		public Int32 AIMPriorityUsedValueID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		//10/17/18
		//[DataReaderColumnMapping(isOptional: true)]
		//public DateTime SyncTime { get; set; }
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SyncTime", SqlDbType.Int)]
		public DateTime SyncTime { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 SyncProgressPercentage { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }


		//[DataReaderColumnMapping(isOptional: true)]
		//public String Name { get; set; }

		//[DataReaderColumnMapping(isOptional: true)]
		//public String CFCPath { get; set; }

		//[DataReaderColumnMapping(isOptional: true)]
		//public MathTypeEnum DefaultForPropose { get; set; }			

		//[DataReaderColumnMapping(isOptional: true)]
		//public Boolean IsActive { get; set; }

		//[DataReaderColumnMapping(isOptional: true)]
		//public Int32 CFCUserID { get; set; }
		
		//[DataReaderColumnMapping(isOptional: true)]
		//public Int32 AIMPriorityUsedValueID { get; set; }

		//[DataReaderColumnMapping(isOptional: true)]
		//public Int32 DataSourceTypeID { get; set; }
		

        #endregion
    }
}
