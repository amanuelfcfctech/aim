using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class CFCPlantRefAIMOwnerLocationInfo : DataObjectBase
    {
        #region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCPlantRefAIMOwnerLocationID", SqlDbType.Int)]
		public Int32 CFCPlantRefAIMOwnerLocationID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCPlantID", SqlDbType.Int)]
		public Int32 CFCPlantID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantCode {get; set; }
		
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantName {get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMOwnerLocationID", SqlDbType.Int)]
		public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMOwnerID", SqlDbType.Int)]
        public Int32 AIMOwnerID { get; set; }      

        [DataReaderColumnMapping(isOptional: true)]
		public String LocationCode {get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String LocationName {get; set; }
					 
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
		public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}

