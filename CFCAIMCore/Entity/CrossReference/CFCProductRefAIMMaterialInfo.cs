using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCProductRefAIMMaterialInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
		SqlParameterMapping("@CFCProductRefAIMMaterialID", SqlDbType.Int)]
		public Int32 CFCProductRefAIMMaterialID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@CFCProductID", SqlDbType.Int)]
		public Int32 CFCProductID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCProductName {get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCProductCode {get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
		public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerMaterialCode { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerMaterialName { get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        //3/26/18
        [DataReaderColumnMapping(isOptional:true)]
        public Int32 AIMOwnerLocationID { get; set; }

        //3/26/18
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantCode { get; set; }

        //3/26/18
        [DataReaderColumnMapping(isOptional: true)]
        public String LocationCode { get; set; }


        //3/15/21 ---------------------------------        
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCPlantID { get; set; }        
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantName { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String ArchiveVersion { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String ProductionVersion { get; set; }
        //-----------------------------------------

        //4/12/22
        [DataReaderColumnMapping(isOptional: true)]
        public String LocationName { get; set; }
	
        //1/10/22
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 MaterialTypeID { get; set; }

        //4/28/22
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCProductArchiveID { get; set; }

        //AlreadyExists
        [DataReaderColumnMapping(isOptional: true)]
        public String AlreadyExists { get; set; }

        //Status
        [DataReaderColumnMapping(isOptional: true)]
        public String StatusDisplay
        {
            get {
                
                String result;

                if (AlreadyExists != null && LocationName != null)
                {
                    result = "Product is Cross-referenced in a Different Plant";
                }
                else if (LocationName == null)
                {
                    result = "Missing FRM Plant Cross-Reference";
                }
                else
                {
                    result = "";
                }

                return result;
            }
        }

        #endregion
    }
}


