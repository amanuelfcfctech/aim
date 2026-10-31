using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;
using System.IO;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class BrukerSQLInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleCode", SqlDbType.NVarChar)]
        public String SampleCode { get; set; }

        //Material
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Material", SqlDbType.NVarChar)]
        public String Material { get; set; }

        //SampleDate
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@SampleDate", SqlDbType.DateTime)]
        public DateTime SampleDate { get; set; }
        
        //Nutrient                                                              
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Nutrient", SqlDbType.NVarChar)]
        public String Nutrient { get; set; }

        //UnitType
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@UnitType", SqlDbType.NVarChar)]
        public String UnitType { get; set; }              
        
        //ResultValue
        [DataReaderColumnMapping,
        SqlParameterMapping("@ResultValue", SqlDbType.Decimal)]
        public Decimal ResultValue { get; set; }

        //Unique ID
        [DataReaderColumnMapping,
        SqlParameterMapping("@UniqueID", SqlDbType.NVarChar)]
        public String UniqueID { get; set; }

        [DataReaderColumnMapping]
        public String Path { get; set; }

        //Unique ID
        [DataReaderColumnMapping]
        public String FileName { get; set; }
        #endregion

    }
}

