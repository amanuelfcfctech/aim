using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    //Task1061 DB251022
    [Serializable()]
    public class SampleResultTemplateDetailGroupInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleResultTemplateDetailGroupID", SqlDbType.Int)]
        public Int32 SampleResultTemplateDetailGroupID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleResultTemplateID", SqlDbType.Int)]
        public Int32 SampleResultTemplateID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@NutrientGroupID", SqlDbType.Int)]
        public Int32 NutrientGroupID { get; set; }

        [DataReaderColumnMapping()]
        public String NutrientGroupName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@TestFrequency", SqlDbType.Int)]
        public Int32 TestFrequency { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@IsDateFrequency", SqlDbType.Bit)]
        public Boolean IsDateFrequency { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@StartDate", SqlDbType.DateTime)]
        public DateTime StartDate { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping()]
        public String UserName { get; set; }

        #endregion
    }
}
