using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class EventsInfo : DataObjectBase
    {
        #region Properties

        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventID", SqlDbType.Int)]
        public Int32 EventID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventName", SqlDbType.NVarChar)]
        public String EventName { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventDescription", SqlDbType.VarChar)]
        public String EventDescription { get; set; }

        //Daily, Monthly, Weekly, One-Time
        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventRunTypeID", SqlDbType.Int)]
        public Int32 EventRunTypeID { get; set; }

        //Import or Export
        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventTypeID", SqlDbType.Int)]
        public Int32 EventTypeID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@StartDateTime", SqlDbType.DateTime)]
        public DateTime StartDateTime { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@LastRunDateTime", SqlDbType.DateTime)]
        public DateTime LastRunDateTime { get; set; }

        //UserDefinedDataSourceDefinitionID,         
        [DataReaderColumnMapping(isOptional: true),             
        SqlParameterMapping("@TemplateID", SqlDbType.Int)]                  
        public Int32 TemplateID { get; set; }

        //[DataReaderColumnMapping(),
        //SqlParameterMapping("@RecurEvery", SqlDbType.Int)]
        //public Int32 RecurEvery { get; set; }
                
        [DataReaderColumnMapping(isOptional: true),
        SqlParameterMapping("@EventStatus", SqlDbType.Bit)]
        public Boolean EventStatus { get; set; }

        public String EventStatusString
        {
            get
            {
                if (EventStatus == true)
                {
                    return "Enabled";
                }                
                else
                {
                    return "Disabled";
                }
            }
        }

        public String LastRunDateTimeString
        {
            get
            {
                if (LastRunDateTime.Date == Convert.ToDateTime("1/1/001 12:00:00 AM"))
                {
                    return "Never";
                }
                else
                {
                    return Convert.ToString(LastRunDateTime);
                }
            }
        }


        [DataReaderColumnMapping()]
        public DateTime UpdateStamp { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@CFCUserID", SqlDbType.Int)]
        public Int32 CFCUserID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String UserName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String EventTypeName { get; set; }
        
        [DataReaderColumnMapping(isOptional: true)]             //System import name
        public String SystemImportName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]             //User-Defined import name
        public String ImportFileName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]             //Export name
        public String ExportName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]             
        public String FileName { get; set; }

        #endregion
    }
}
