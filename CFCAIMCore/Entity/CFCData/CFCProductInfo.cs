using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class CFCProductInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 CFCProductID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCPlantID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantCode { get; set; }

        [DataReaderColumnMapping()]
        public String CFCProductCode { get; set; }

        [DataReaderColumnMapping()]
        public String CFCProductName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Int32 AimDatabaseID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AimDatabaseName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String CFCPlantName { get; set; }

        
			

        #endregion
    }
}

