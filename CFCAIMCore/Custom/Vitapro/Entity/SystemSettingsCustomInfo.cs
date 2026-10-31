using Core.Library.Data;
using System;
using System.Data;

//9/16/20
//namespace CFCAIMCustom.Entity.Vitapro
namespace CFCAIM.Custom.Vitapro.Entity

{
    [Serializable()]
    public class SystemSettingsCustomInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@SystemSettingsCustomID", SqlDbType.Int)]
        public Int32 SystemSettingsCustomID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientID1_ForDisplay", SqlDbType.Int)]
        public Int32 AIMNutrientID1_ForDisplay { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientID2_ForDisplay", SqlDbType.Int)]
        public Int32 AIMNutrientID2_ForDisplay { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientID3_ForDisplay", SqlDbType.Int)]
        public Int32 AIMNutrientID3_ForDisplay { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientID4_ForDisplay", SqlDbType.Int)]
        public Int32 AIMNutrientID4_ForDisplay { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String AIMNutrientName1 { get; set; }
        [DataReaderColumnMapping(IsOptional = true)]
        public String AIMNutrientName2 { get; set; }
        [DataReaderColumnMapping(IsOptional = true)]
        public String AIMNutrientName3 { get; set; }
        [DataReaderColumnMapping(IsOptional = true)]
        public String AIMNutrientName4 { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        
    }
}
