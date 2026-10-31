using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCIngredientRefAIMMaterialInfo : DataObjectBase
    {
        #region Properties

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCIngredientRefAIMMaterialID", SqlDbType.Int)]
		public Int32 CFCIngredientRefAIMMaterialID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCIngredientID", SqlDbType.Int)]
        public Int32 CFCIngredientID { get; set; }

        [DataReaderColumnMapping()]
        public String CFCIngredientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCIngredientName { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@AnalyzerMaterialID", SqlDbType.Int)]		//why does this say "Analyzer" ?
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }
               

        //2/17/25
        //Status
        [DataReaderColumnMapping(isOptional: true)]
        public String StatusDisplay
        { get; set; }
        #endregion
    }
}


