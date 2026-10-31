using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class CFCProductArchiveInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping()]        
        public Int32 CFCProductArchiveID { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public String ProductionVersion { get; set; }

		[DataReaderColumnMapping(isOptional: true)]
		public DateTime ProductionDate { get; set; }
				
		[DataReaderColumnMapping(isOptional: true)]       
        public Int32 CFCProductID { get; set; }
                
        [DataReaderColumnMapping(isOptional: true)]        
        public String ArchiveVersion { get; set; }

        [DataReaderColumnMapping(isOptional: true)]		
        public DateTime ArchiveDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]        
        public String ArchiveType { get; set; }

        [DataReaderColumnMapping(isOptional: true)]        
        public String ArchiveUser { get; set; }		       

        [DataReaderColumnMapping(isOptional: true)]        
        public String ProductionUser { get; set; }

        [DataReaderColumnMapping(isOptional: true)]        
        public DateTime TrialDate { get; set; }

        [DataReaderColumnMapping(isOptional: true)]        
        public String TrialVersion { get; set; }

        [DataReaderColumnMapping(isOptional: true)]        
        public String TrialUser { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(isOptional: true)]        
        public Int32 CFCUserID { get; set; }
        
        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }


        //6/4/24
        [DataReaderColumnMapping(isOptional: true)]     
        public String CFCProductCode { get; set; }

        //6/4/24
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCProductName { get; set; }

        //6/4/24
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantCode { get; set; }

        //6/4/24
        [DataReaderColumnMapping(isOptional: true)]
        public String CFCPlantName { get; set; }

        #endregion
    }
}

