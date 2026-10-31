using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleCustomReportInfo
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleCreatedStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ContractNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String PoNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String InvoiceNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String TicketNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String LoadNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String BillOfLoad { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String VehicleNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String LotNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AlternateCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String Notes { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String MaterialTypeName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMOwnerCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMOwnerName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMOwnerLocationCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMOwnerLocationName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierLocationCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierLocationName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerNutrientCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AnalyzerNutrientName { get; set; }               
                
        [DataReaderColumnMapping(isOptional: true)]
        public String Unit { get; set; }

        [DataReaderColumnMapping(isOptional: true)]        
        public Decimal ResultValue { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime ResultAnalysisDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String DataSourceTypeCode { get; set; }

        //----------------------------------------------------------

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCProductCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String CFCProductName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime ProductionDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ProductionVersion { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal NutrientMin { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal NutrientMax { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Decimal NutrientValue { get; set; }

        //----------------------------------------------------------

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime FromDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime ToDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String FromDateString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ToDateString { get; set; }

        
        #endregion
    }
}