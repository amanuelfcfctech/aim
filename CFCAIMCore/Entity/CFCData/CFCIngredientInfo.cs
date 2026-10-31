using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
	public class CFCIngredientInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 CFCIngredientID { get; set; }

        [DataReaderColumnMapping()]
        public String CFCIngredientCode { get; set; }

        [DataReaderColumnMapping()]
        public String CFCIngredientName { get; set; }

		[DataReaderColumnMapping()]
		public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(IsOptional=true)]
		//CFCIngredient column is "MathType", not "MathTypeID"
        //public Int32 MathTypeID { get; set; }
		public Int32 MathType { get; set; }


		[DataReaderColumnMapping()]
		public Int32 AimDatabaseID { get; set; }

		[DataReaderColumnMapping()]
		public String AimDatabaseName { get; set; }

        //9/5/17
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryTemplateDetailID { get; set; }

        //2/6/18
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }

        #endregion
    }
}

