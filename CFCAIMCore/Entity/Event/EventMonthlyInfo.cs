using System;
using System.Data;
using Core.Library.Data;

namespace CFCAIMCore.Entity
{
    [Serializable()]
    public class EventMonthlyInfo : DataObjectBase
    {
        #region Properties
        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventMonthlyID", SqlDbType.Int)]
        public Int32 EventMonthlyID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@EventID", SqlDbType.Int)]
        public Int32 EventID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@MonthID", SqlDbType.Int)]
        public Int32 MonthID { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SpecificDays", SqlDbType.Bit)]
        public Boolean SpecificDays { get; set; }

        [DataReaderColumnMapping(),
        SqlParameterMapping("@SpecificWeek", SqlDbType.Bit)]
        public Boolean SpecificWeek { get; set; }               

        [DataReaderColumnMapping(isOptional: true)]
        public DateTime UpdateStamp { get; set; }
        
        [DataReaderColumnMapping(isOptional: true)]
        public Int32 DayID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 WeekID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public Int32 WeekNumberID { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String MonthName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String WeekName { get; set; }

        [DataReaderColumnMapping(isOptional: true)]
        public String WeekNumberName { get; set; }

        

        
        
        //public String MonthName
        //{
        //    get
        //    {
        //        if (MonthID == 1)
        //        {
        //            return "January";
        //        }
        //        else if (MonthID == 2)
        //        {
        //            return "February";
        //        }
        //        else if (MonthID == 3)
        //        {
        //            return "March";
        //        }
        //        else if (MonthID == 4)
        //        {
        //            return "April";
        //        }
        //        else if (MonthID == 5)
        //        {
        //            return "May";
        //        }
        //        else if (MonthID == 6)
        //        {
        //            return "June";
        //        }
        //        else if (MonthID == 7)
        //        {
        //            return "July";
        //        }
        //        else if (MonthID == 8)
        //        {
        //            return "August";
        //        }
        //        else if (MonthID == 9)
        //        {
        //            return "September";
        //        }
        //        else if (MonthID == 10)
        //        {
        //            return "October";
        //        }
        //        else if (MonthID == 11)
        //        {
        //            return "November";
        //        }
        //        else
        //        {
        //            return "December";
        //        }
        //    }
        //}

        #endregion
    }
}
