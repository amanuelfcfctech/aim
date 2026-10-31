using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class QueryTemplateInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@QueryTemplateID", SqlDbType.Int)]
		public Int32 QueryTemplateID { get; set; }
				
		[DataReaderColumnMapping(),
		SqlParameterMapping("@ExportTargetID", SqlDbType.Int)]
		public ExportTargetEnum ExportTargetID { get; set; }
				
		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMOwnerLocationID", SqlDbType.Int)]
		public Int32 AIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Name", SqlDbType.VarChar)]
        public String Name { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsAnalysis", SqlDbType.Bit)]
		public Boolean IsAnalysis { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }

		public String Created
		{
			get
			{
				if (IsAnalysis == true)
				{
					return "";
				}				
				else
				{
					return "Created on Reports screen";
				}
			}
		}

		//6/22/22
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@HideInAnalysis", SqlDbType.Bit)]
		public Boolean HideInAnalysis { get; set; }

		//10/31/22
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IngredientOrProduct", SqlDbType.VarChar)]
		public String IngredientOrProduct { get; set; }

		#endregion
	}
}


