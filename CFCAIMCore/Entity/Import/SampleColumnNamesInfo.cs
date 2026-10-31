using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleImportColumnNamesInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(isOptional: true)]
		public String MapColumnName { get; set; }		

		[DataReaderColumnMapping(isOptional: true)]
		public String SampleColumnValue { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMFieldName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 MapColumnID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@MaxLength", SqlDbType.Int)]
		public Int32 MaxLength { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@DataType", SqlDbType.NVarChar)]
		public String DataType { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@IsDetail", SqlDbType.Bit)]
		public Boolean IsDetail { get; set; }

        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@CanAlter", SqlDbType.Bit)]
        //public Boolean CanAlter { get; set; }


        #endregion
    }
}