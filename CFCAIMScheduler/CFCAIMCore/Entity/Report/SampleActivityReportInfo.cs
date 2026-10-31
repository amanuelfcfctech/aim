using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleActivityReportInfo
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String OwnerLocationName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSourceID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMSourceLocationID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SupplierName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SupplierLocationName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime FromDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime ToDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String FromDateString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ToDateString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String IngredientName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ProductName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime CreatedStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CreatedStampGroup { get; set; }

        //4/26/22
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 MaterialTypeID { get; set; }

        //4/26/22 - added for Sample Activity Quality Control report
        [DataReaderColumnMapping(isOptional: true)]
        public String ParameterName { get; set; }

        //4/26/22 - added for Sample Activity Quality Control report
        [DataReaderColumnMapping(isOptional: true)]
        public Decimal ResultValue { get; set; }

        #endregion
    }
}