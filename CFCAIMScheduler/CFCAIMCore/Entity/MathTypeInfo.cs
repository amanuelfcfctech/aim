using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class MathTypeInfo : DataObjectBase
    {
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("MathTypeID", SqlDbType.Int)]
		public Int32 MathTypeID { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("MathTypeName", SqlDbType.NVarChar)]
        public String MathTypeName { get; set; }

        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("IsActive", SqlDbType.Bit)]
        public Boolean IsActive { get; set; }

		[DataReaderColumnMapping(isOptional: true),
		SqlParameterMapping("IsSystem", SqlDbType.Bit)]
		public Boolean IsSystem { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("Equation", SqlDbType.VarChar)]
        public String Equation { get; set; }

        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
		public String UserName { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String EquationTest { get; set; }

		[DataReaderColumnMapping(isOptional: true)]		
		public Decimal Result { get; set; }
	}
}
