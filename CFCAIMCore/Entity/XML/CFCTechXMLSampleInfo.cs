using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCTechXmlSampleInfo : DataObjectBase
    {        
        [XmlElement("SampleID")]
        public String SampleID { get; set; }
                
        [XmlElement("SampleDate")]
        public String SampleDate { get; set; }
                
        [XmlElement("AIMMaterialCode")]
        public String AIMMaterialCode { get; set; }
                
        [XmlElement("AIMOwnerCode")]
        public String AIMOwnerCode { get; set; }
                
        [XmlElement("AIMOwnerLocationCode")]
        public String AIMOwnerLocationCode { get; set; }
                
        [XmlElement("AIMSupplierCode")]
        public String AIMSupplierCode { get; set; }
                
        [XmlElement("AIMSupplierLocationCode")]
        public String AIMSupplierLocationCode { get; set; }
                
        [XmlElement("SampleCode")]
        public String SampleCode { get; set; }

        [XmlElement("AlternateCode")]
        public String AlternateCode { get; set; }

        [XmlElement("BillOfLoad")]
        public String BillOfLoad { get; set; }

        [XmlElement("ContractNumber")]
        public String ContractNumber { get; set; }

        [XmlElement("InvoiceNumber")]
        public String InvoiceNumber { get; set; }

        [XmlElement("LoadNumber")]
        public String LoadNumber { get; set; }

        [XmlElement("LotNumber")]
        public String LotNumber { get; set; }

        [XmlElement("PoNumber")]
        public String PoNumber { get; set; }

        [XmlElement("TicketNumber")]
        public String TicketNumber { get; set; }

        [XmlElement("VehicleNumber")]
        public String VehicleNumber { get; set; }

        [XmlElement("MaterialTypeID")]
        public String MaterialTypeID { get; set; }


        [XmlElement("Results")]
        public List<CFCTechXmlResultsInfo> Results { get; set; }




    }
}
