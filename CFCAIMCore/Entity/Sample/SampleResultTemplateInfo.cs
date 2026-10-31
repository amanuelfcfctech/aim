using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleResultTemplateInfo : DataObjectBase
	{
		#region Properties			

		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleResultTemplateID", SqlDbType.Int)]
		public Int32 SampleResultTemplateID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleResultTemplateName", SqlDbType.NVarChar)]
		public String SampleResultTemplateName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
		public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]		
		public String DataSourceTypeName { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialID { get; set; }
		
		[DataReaderColumnMapping(isOptional: true)]
		public String AIMMaterialName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

        //Task1061 DB251022
        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CurrentIteration", SqlDbType.Int)]
        public Int32 CurrentIteration { get; set; }

        //Task1061 DB251022
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsGroupTemplate", SqlDbType.Bit)]
        public Boolean IsGroupTemplate { get; set; }

        //Task1061 DB251030
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SampleResultTemplateID_Default { get; set; }
        #endregion
    }
}
