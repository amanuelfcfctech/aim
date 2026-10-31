using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class DataSourceTypeInfo : DataObjectBase
    {
        #region Properties

		[DataReaderColumnMapping(isOptional: true),				
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@Name", SqlDbType.NVarChar)]
		public String Name { get; set; }

		//4/5/17
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeCode", SqlDbType.NVarChar)]
		public String DataSourceTypeCode { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsFurtherAnalyses", SqlDbType.Bit)]
		public Boolean IsFurtherAnalyses { get; set; }

		//4/28/17
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsSQLDatabase", SqlDbType.Bit)]
		public Boolean IsSQLDatabase { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsUserDefined", SqlDbType.Bit)]
		public Boolean IsUserDefined { get; set; }

		public String TypeDisplay
		{
			get
			{
				if (Name == "--Select--")
				{
					return "";
				}
				else if (IsUserDefined == true)
				{
					return "User-Defined";
				}
				
				else
				{
					return "System";
				}
				
			}
		}

		//6/1/22 - used on advanced reports
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 OrderBy { get; set; }

		//5/31/22 - used on advanced reports
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 QueryTemplateDetailID { get; set; }

		//5/31/22 - used on advanced reports
		[DataReaderColumnMapping(isOptional: true)]
		public Int32 QueryOperationID { get; set; }

        public String ErrorMessage { get; set; }

        #endregion
    }
}