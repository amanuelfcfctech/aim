using System;
using System.Data;
using Core.Library.Data;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class InternalImportValidateSampleInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping()]
        public Int32 InternalImportValidateSampleID { get; set; }               

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 Status { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 MessageStatus { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String Message { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String FilePathAndName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleDate { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalysisDate { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AlternateCode { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AnalyzerMaterialID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerMaterialCode { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerMaterialName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AnalyzerOwnerID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AnalyzerOwnerLocationID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AnalyzerSupplierID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerSupplierCode { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerSupplierName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AnalyzerSupplierLocationID { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerSupplierLocationCode { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerSupplierLocationName { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AIMMaterialID { get; set; }
        [DataReaderColumnMapping()]
        public Int32 AIMOwnerID { get; set; }
        [DataReaderColumnMapping()]
        public Int32 AIMOwnerLocationID { get; set; }
        [DataReaderColumnMapping()]
        public Int32 AIMSupplierID { get; set; }
        [DataReaderColumnMapping()]
        public Int32 AIMSupplierLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 XrefMaterialStatus { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 XrefSupplierStatus { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 XrefSupplierLocationStatus { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public List<NutriQuestXmlPropertyInfo> Property { get; set; }


        #endregion

    }
}
