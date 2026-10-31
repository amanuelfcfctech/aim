using System;
using System.Data;
using Core.Library.Data;

// * * * 
//Separate AIMOwnerLocation entity -- to sort by OwnerLocationName in lookupedits 
//(because in devexpress, columns are shown in the order they are within the collection)
// * * * 

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class AIMOwnerLocationSortInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping()]
        public Int32 AIMOwnerID { get; set; }

        [DataReaderColumnMapping()]
        public Int32 AIMOwnerLocationID { get; set; }

        [DataReaderColumnMapping()]
        public String LocationName { get; set; }

        [DataReaderColumnMapping()]
        public String LocationCode { get; set; }               

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
        public String OwnerCode { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String OwnerName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Boolean IsFormulationPlant { get; set; }

        //8/23/17
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryTemplateDetailID { get; set; }

        //2/6/18
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 QueryOperationID { get; set; }

        #endregion
    }
}
