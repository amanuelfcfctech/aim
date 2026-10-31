using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class CFCNutrientAIMNutrientRefInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
		public Int32 CFCNutrientAIMNutrientRefID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 CFCNutrientID { get; set; }

        [DataReaderColumnMapping()]
        public String CFCNutrientCode {get; set; }

        [DataReaderColumnMapping()]
        public String CFCNutrientName {get; set; }

		[DataReaderColumnMapping()]
		public Int32 AIMNutrientID {get; set; }

		[DataReaderColumnMapping()]
		public String AIMNutrientCode {get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String AIMNutrientName {get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }
        #endregion
    }
}


