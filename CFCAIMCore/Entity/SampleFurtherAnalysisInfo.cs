using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleFurtherAnalysisInfo : DataObjectBase
    {

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleFurtherAnalysisID", SqlDbType.Int)]
        public Int32 SampleFurtherAnalysisID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleID", SqlDbType.NVarChar)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@DataSourceTypeID", SqlDbType.Int)]
        public Int32 DataSourceTypeID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMessageID", SqlDbType.Int)]
		public Int32 AIMMessageID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String Message { get; set; }

		[DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ProcessDate", SqlDbType.DateTime)]
        public DateTime ProcessDate { get; set; }      

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UpdateName { get; set; }

        //7/11/23
        [DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }

        //7/11/23
        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleDate { get; set; }

        //7/11/23
        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceTypeName { get; set; }

        //7/11/23
        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        //7/11/23
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean UpdateProcessDate { get; set; }


    }
}
