using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class AIMIngredient : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
		public Int32 AIMIngredientID { get; set; }

        [DataReaderColumnMapping()]
		public String AIMIngredientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String AIMIngredientName { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping()]
		public DateTime UpdateStamp { get; set; }

		[DataReaderColumnMapping()]
		public String FurterAnalysisCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }
        #endregion
    }
}
