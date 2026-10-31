using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;


namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class GuaranteeRefundHeaderInfo : DataObjectBase
	{
		#region Properties
	  
		[DataReaderColumnMapping(),
		SqlParameterMapping("@GuaranteeRefundHeaderID", SqlDbType.Int)]
		public Int32 GuaranteeRefundHeaderID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BatchID", SqlDbType.Int)]
		public Int32 BatchID { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@BatchSentDate", SqlDbType.DateTime)]
		public DateTime BatchSentDate { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMSupplierID", SqlDbType.Int)]
        public Int32 AIMSupplierID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMSupplierLocationID", SqlDbType.Int)]
        public Int32 AIMSupplierLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SourceName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SourceLocationName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }        	
        
        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        #endregion
    }
}
