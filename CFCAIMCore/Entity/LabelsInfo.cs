using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class LabelsInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(IsOptional = true)]
        public Int32 LabelsID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ControlID", SqlDbType.Int)]
        public Int32 ControlID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@AlternateNameID", SqlDbType.Int)]
        public Int32 AlternateNameID { get; set; }

        //LabelFieldFirst is the top line that defaults to Load#. User sets this on Sample > Settings > Labels tab
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelFieldFirst", SqlDbType.Bit)]
        public Boolean LabelFieldFirst { get; set; }

        //LabelFieldLast is the bottom line that defaults to Ticket#. User sets this on Sample > Settings > Labels tab
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelFieldLast", SqlDbType.Bit)]
        public Boolean LabelFieldLast { get; set; }

        //6/23/21 - defaults to SupplierName
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelFieldLine2", SqlDbType.Bit)]
        public Boolean LabelFieldLine2 { get; set; }

        //6/23/21 - defaults to SupplierLocation Name
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelFieldLine3", SqlDbType.Bit)]
        public Boolean LabelFieldLine3 { get; set; }

        //6/23/21 - defaults to AIMMaterialCode
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelFieldLine4", SqlDbType.Bit)]
        public Boolean LabelFieldLine4 { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@UpdateStamp", SqlDbType.DateTime)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String ControlName { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String AlternateName { get; set; }
        
        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(IsOptional = true)]
        public String UserControlName {get; set; }

        //6/29/21
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Line1Bold", SqlDbType.Bit)]
        public Boolean Line1Bold { get; set; }

        //6/29/21
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Line2Bold", SqlDbType.Bit)]
        public Boolean Line2Bold { get; set; }

        //6/29/21
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Line3Bold", SqlDbType.Bit)]
        public Boolean Line3Bold { get; set; }

        //6/29/21
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Line4Bold", SqlDbType.Bit)]
        public Boolean Line4Bold { get; set; }

        //6/29/21
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@Line5Bold", SqlDbType.Bit)]
        public Boolean Line5Bold { get; set; }

        //-------------------------------------------------------------------
        //5/2/23 - added product label fields
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelProdFieldLine1", SqlDbType.Bit)]
        public Boolean LabelProdFieldLine1 { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelProdFieldLine2", SqlDbType.Bit)]
        public Boolean LabelProdFieldLine2 { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelProdFieldLine3", SqlDbType.Bit)]
        public Boolean LabelProdFieldLine3 { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelProdFieldLine4", SqlDbType.Bit)]
        public Boolean LabelProdFieldLine4 { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@LabelProdFieldLine5", SqlDbType.Bit)]
        public Boolean LabelProdFieldLine5 { get; set; }

        //5/2/23 - added product yes/no fields
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ProdLine1Bold", SqlDbType.Bit)]
        public Boolean ProdLine1Bold { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ProdLine2Bold", SqlDbType.Bit)]
        public Boolean ProdLine2Bold { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ProdLine3Bold", SqlDbType.Bit)]
        public Boolean ProdLine3Bold { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ProdLine4Bold", SqlDbType.Bit)]
        public Boolean ProdLine4Bold { get; set; }
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@ProdLine5Bold", SqlDbType.Bit)]
        public Boolean ProdLine5Bold { get; set; }


        #endregion
    }
}