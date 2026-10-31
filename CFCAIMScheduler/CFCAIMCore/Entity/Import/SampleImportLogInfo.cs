using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleImportLogInfo : DataObjectBase
	{
		#region Properties
		[DataReaderColumnMapping(),
		SqlParameterMapping("@SampleImportlogID", SqlDbType.Int)]
		public Int32 SampleImportLogID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleID", SqlDbType.Int)]
        public Int32 SampleID { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String SampleCode { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public DateTime SampleDate { get; set; }

        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@Message", SqlDbType.NVarChar)]
        //public String Message { get; set; }

        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@Status", SqlDbType.Int)]
        //public Int32 Status { get; set; }

        //public String StatusDisplay
        //{
        //    get
        //    {
        //        if (Status == 1)
        //        {
        //            return "Accept";
        //        }
        //        else if (Status == 2)
        //        {
        //            return "Reject";
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //}

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@UserDefinedDataSourceDefinitionID", SqlDbType.Int)]
        public Int32 UserDefinedDataSourceDefinitionID { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }
	   
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@BatchID", SqlDbType.Int)]
        public Int32 BatchID { get; set; }
	   
        [DataReaderColumnMapping(isOptional: true)]
        public List<SampleImportLogDetailInfo> DetailLog { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@SampleCode", SqlDbType.NVarChar)]
		public String SampleCode { get; set; }


		#endregion
	}
}
