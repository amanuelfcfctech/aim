using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCProductArchiveSpecInfo : DataObjectBase
    {

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCProductArchiveSpecID", SqlDbType.Int)]
        public Int32 CFCProductArchiveSpecID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCProductArchiveID", SqlDbType.Int)]
        public Int32 CFCProductArchiveID { get; set; }

        //3/15/21 - commented
        //public Int32 CFCProductID { get; set; }        

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCNutrientID", SqlDbType.Int)]
        public Int32 CFCNutrientID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@NutrientMin", SqlDbType.Decimal)]
        public Double NutrientMin { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@NutrientMax", SqlDbType.Decimal)]
        public Double NutrientMax { get; set; }

        //3/15/21        
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMNutrientID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientCode { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientName { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Decimal NutrientValue { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }


    }
}
