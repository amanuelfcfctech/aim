using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class SampleHeaderInfo
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 SampleID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AlternateCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String Notes { get; set; }

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
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }

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
        public DateTime FromDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime ToDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String FromDateString { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String ToDateString { get; set; }




        #endregion
    }
}