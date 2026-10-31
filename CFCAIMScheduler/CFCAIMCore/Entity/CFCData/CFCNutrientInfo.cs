using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class CFCNutrientInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
		public Int32 CFCNutrientID { get; set; }

        [DataReaderColumnMapping()]
		public String CFCNutrientCode { get; set; }

        [DataReaderColumnMapping()]
		public String CFCNutrientName { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping()]
        public Int32 MathTypeID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 AimDatabaseID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AimDatabaseName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean CreateNutrient { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public Boolean CreateXref { get; set; }

		[DataReaderColumnMapping()]
		public String Unit { get; set; }
        
        [DataReaderColumnMapping()]
        public Boolean IsComputed { get; set; }

        #endregion
    }
}

