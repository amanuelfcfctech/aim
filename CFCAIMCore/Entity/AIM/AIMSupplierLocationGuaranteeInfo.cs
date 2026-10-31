using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AIMSupplierLocationGuaranteeInfo : DataObjectBase
    {
		[DataReaderColumnMapping(ColumnName="AIMSourceLocationGuaranteeID"),							  
		SqlParameterMapping("@AIMSourceLocationGuaranteeID", SqlDbType.Int)]
		public Int32 AIMSupplierLocationGuaranteeID { get; set; }					

        [DataReaderColumnMapping(ColumnName="AIMSourceLocationID"),
		SqlParameterMapping("@AIMSourceLocationID", SqlDbType.Int)]
        public Int32 AIMSupplierLocationID { get; set; }

		[DataReaderColumnMapping(ColumnName = "AIMSourceID"),
		SqlParameterMapping("@AIMSourceID", SqlDbType.Int)]
        public Int32 AIMSupplierID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMNutrientID", SqlDbType.Int)]
        public Int32 AIMNutrientID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@AIMMaterialID", SqlDbType.Int)]
        public Int32 AIMMaterialID { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Min", SqlDbType.Decimal)]
        public Double Min { get; set; }

        [DataReaderColumnMapping(),
		SqlParameterMapping("@Max", SqlDbType.Decimal)]
        public Double Max { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@Target", SqlDbType.Decimal)]
		public Double Target { get; set; }

		[DataReaderColumnMapping(),
		SqlParameterMapping("@IsActive", SqlDbType.Bit)]
		public Boolean IsActive { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("@CFCUserID", SqlDbType.VarChar)]
        public Int32 CFCUserID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        //7/28/20
        [DataReaderColumnMapping(isOptional: true)]
        public String SourceName { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String SourceLocationName { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AIMMaterialName { get; set; }
        [DataReaderColumnMapping(isOptional: true)]
        public String AIMNutrientName { get; set; }

        //11/21/22
        [DataReaderColumnMapping(),
        SqlParameterMapping("@Moisture", SqlDbType.Decimal)]
        public Double Moisture { get; set; }

    }
}
