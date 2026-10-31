using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class PlusMinusInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@PlusMinusID", SqlDbType.Int)]
        public Int32 PlusMinusID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@PlusMinusName", SqlDbType.VarChar)]
        public String PlusMinusName { get; set; }
                
        public String PlusMinusNameString
        {
            get
            {
                if (PlusMinusName == "Plus")
                {
                    return "+";
                }
                else if (PlusMinusName == "Minus")
                {
                    return "-";
                }                
                else
                {
                    return "";
                }
            }
        }

        //12/1/22
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@PlusMinusDescription", SqlDbType.VarChar)]
        public String PlusMinusDescription { get; set; }


        #endregion
    }
}
