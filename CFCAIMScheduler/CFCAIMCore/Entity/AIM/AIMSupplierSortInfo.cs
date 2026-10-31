using System;
using System.Data;
using Core.Library.Data;

// * * * 
//Separate AIMSupplier entity -- to sort by SupplierName in lookupedits 
//(because in devexpress, columns are shown in the order they are within the collection)
// * * * 

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AIMSupplierSortInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(ColumnName = "AIMSourceID")]
        public Int32 AIMSupplierID { get; set; }               
        
        [DataReaderColumnMapping(ColumnName = "SourceName", IsOptional = true)]
        public String SupplierName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String AIMSupplierCode { get; set; }

        [DataReaderColumnMapping()]
        public Boolean IsActive { get; set; }

        public String IsActiveDisplay
        {
            get
            {
                if (IsActive == true)
                {
                    return "true";
                }
                else
                {
                    return "";
                }
            }
        }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryTemplateDetailID { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }


        #endregion
    }
}

