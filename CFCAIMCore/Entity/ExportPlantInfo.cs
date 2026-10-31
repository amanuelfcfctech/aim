using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ExportPlantInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ExportPlantID", SqlDbType.Int)]
        public Int32 ExportPlantID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ExportPlantParentID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 ExportPlantGroupID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ExportPlantGroupCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCPlantID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }


        //Is this needed?
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerOwnerLocationCode { get; set; }

        //Is this needed?
        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceTypeName { get; set; }


        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMOwnerLocationCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMOwnerLocationName { get; set; }

        #endregion
    }
}
