using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleResultTemplateGridInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleID", SqlDbType.Int)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UnitType { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ResultValueString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMNutrientID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AnalyzerNutrientID", SqlDbType.Int)]
        public Int32 AnalyzerNutrientID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CurrentValue", SqlDbType.Decimal)]
        public Decimal CurrentValue { get; set; }

        [DataReaderColumnMapping(isOptional: true),                         //Task1135 DB251209  
        SqlParameterMapping("@Min", SqlDbType.Decimal)]
        public Decimal Min { get; set; }

        [DataReaderColumnMapping(isOptional: true),                         //Task1135 DB251209
        SqlParameterMapping("@Max", SqlDbType.Decimal)]
        public Decimal Max { get; set; }

        [DataReaderColumnMapping(isOptional: true)]                         //Task1135 DB260417
        public Int32 MessageOutside { get; set; }

        [DataReaderColumnMapping(isOptional: true)]                         //Task1135 DB260417
        public Int32 MessageInside { get; set; }

        //Task1135 DB260418 - added a column to identify what kind of warning is coming from the data
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ResultValueFlag { get; set; }

        //Task1061 251030 All columns pulled from SampleResultTemplateDetailInfo
        //                The columns below were not used in the grid
        //                Added note: The list of properties IN the grid but seem to be absent from the entity:
        //                            SampleResultID, DataSourceTypeName, InstrumentDescription, IsActive, repositoryItemButtonEdit1

        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
        //public Decimal ResultValue { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String AnalyzerNutrientName { get; set; }

        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@SortOrder", SqlDbType.Int)]
        //public Int32 SortOrder { get; set; }

        //[DataReaderColumnMapping(isOptional: true),
        //SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        //public Int32 CFCUserID { get; set; }

        #endregion
    }
}
