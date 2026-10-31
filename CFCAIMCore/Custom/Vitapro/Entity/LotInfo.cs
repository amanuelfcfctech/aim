using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data;

//9/16/20
//namespace CFCAIMCustom.Entity.Vitapro
namespace CFCAIM.Custom.Vitapro.Entity

{
    [Serializable()]
    public class LotInfo : DataObjectBase
    {
        [DataReaderColumnMapping(),
        SqlParameterMapping("@LotID", SqlDbType.Int)]
        public Int32 LotID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SampleID", SqlDbType.Int)]
        public Int32 SampleID { get; set; }
        
        //8/27/19 - used for mass export -- when user wants to export more than one batch at the same time. 
        [DataReaderColumnMapping(isOptional: true)]
        public Boolean Export { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@LotStatusID", SqlDbType.Int)]
        public Int32 LotStatusID { get; set; }               

        [DataReaderColumnMapping()]
        public DateTime LotStatusUpdate { get; set; }
                
        [DataReaderColumnMapping(),
        SqlParameterMapping("@ProductionStatusLine1", SqlDbType.Int)]
        public Int32 ProductionStatusLine1 { get; set; }              

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ProductionStatusLine2", SqlDbType.Int)]
        public Int32 ProductionStatusLine2 { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ProductionStatusLine3", SqlDbType.Int)]
        public Int32 ProductionStatusLine3 { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ProductionStatusLine4", SqlDbType.Int)]
        public Int32 ProductionStatusLine4 { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        //[DataReaderColumnMapping(),
        //SqlParameterMapping("@LotNumber", SqlDbType.NVarChar)]
        //public String LotNumber { get; set; }

        [DataReaderColumnMapping()]
        public String LotNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 AIMMaterialID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }
                
        //Using Sample user-defined field for Numero lote proveedor
        [DataReaderColumnMapping(isOptional: true)]
        public String InvoiceNumber { get; set; }

        //Using Sample user-defined field for Número guía de despacho 
        [DataReaderColumnMapping(isOptional: true)]
        public String ContractNumber { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime SampleDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String SampleDisplay
        {
            get
            {
                String rtn = "";
                if (LotStatusID == (Int32)EnumReceivingStatus.Pending)
                {
                    rtn = "Pending";
                }
                if (LotStatusID == (Int32)EnumReceivingStatus.Accept)
                {
                    rtn = "Accept";
                }
                if (LotStatusID == (Int32)EnumReceivingStatus.Reject)
                {
                    rtn = "Reject";
                }
                if (LotStatusID == (Int32)EnumReceivingStatus.Ignore)
                {
                    rtn = "Ignore";
                }
                return rtn;
            }
        }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@ExportStatus", SqlDbType.Bit)]
        public Boolean ExportStatus { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime ExpirationDate { get; set; }

        //[DataReaderColumnMapping(),
        //SqlParameterMapping("@AmountAvailable", SqlDbType.Decimal)]
        //public Decimal AmountAvailable { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@AmountAvailable", SqlDbType.Int)]
        public Int32 AmountAvailable { get; set; }


        [DataReaderColumnMapping(isOptional: true)]
        public String AmtAvailable { get; set; }
                
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@DepletedDate", SqlDbType.DateTime)]
        public DateTime DepletedDate { get; set; }


        [DataReaderColumnMapping(isOptional: true)]
        public Boolean Dot1 { get; set; }

        //8/26/19
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientAmount1", SqlDbType.Decimal)]
        public Decimal AIMNutrientAmount1 { get; set; }

        //9/27/19
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientAmount2", SqlDbType.Decimal)]
        public Decimal AIMNutrientAmount2 { get; set; }

        //9/27/19
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientAmount3", SqlDbType.Decimal)]
        public Decimal AIMNutrientAmount3 { get; set; }

        //9/27/19
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AIMNutrientAmount4", SqlDbType.Decimal)]
        public Decimal AIMNutrientAmount4 { get; set; }

        //9/27/19
        [DataReaderColumnMapping(isOptional: true),                     //--> Vitapro is using this field for Notes/Comments (on the Lot screen)
        SqlParameterMapping("@BillOfLoad", SqlDbType.NVarChar)]
        public String BillOfLoad { get; set; }

        //10/1/19
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ResultCount", SqlDbType.Int)]
        public Int32 ResultCount { get; set; }

        //9/2/20
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Concept5ExportStatus", SqlDbType.Bit)]
        public Boolean Concept5ExportStatus { get; set; }

    }
}
