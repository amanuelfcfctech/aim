using System;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class ReportAlternateNamesInfo
    {
        #region Properties

        [DataReaderColumnMapping(isOptional: true)]
        public String ControlName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AlternateName { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String lblBOL { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String lblContractNumber { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String lblInvoiceNumber { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String lblLoadNumber { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String lblLotNumber { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String lblPONumber { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String lblTicketNumber { get; set; }

        //[DataReaderColumnMapping(isOptional: true)]
        //public String lblVehicleID { get; set; }


        #endregion
    }
}