using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class CFCPlantInfo : DataObjectBase
    {
        #region Properties


		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@AimDatabaseID", SqlDbType.Int)]
		public Int32 AimDatabaseID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCPlantID", SqlDbType.Int)]
		public Int32 CFCPlantID { get; set; }	
		
        [DataReaderColumnMapping()]
        public String CFCPlantCode { get; set; }
		       
		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCPlantName", SqlDbType.VarChar)]
		public String CFCPlantName { get; set; }	
		
		[DataReaderColumnMapping()]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AimDatabaseName { get; set; }

		#endregion
    }
}
