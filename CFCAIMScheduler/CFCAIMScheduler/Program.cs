using CFCAIM;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.SQLDatabaseConnection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCAIMScheduler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            
            List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
            Int32 databaseID = 0;
            String startDate;
            String currentDateTime;
            String dayOfWeek;
            String monthName;
            String lastRunDate;
            EventsInfo def = new EventsInfo();
            List<EventsInfo> events = new List<EventsInfo>();
            List<EventDayInfo> eventDaily = new List<EventDayInfo>();
            List<EventWeeklyInfo> eventWeekly = new List<EventWeeklyInfo>();
            List<EventMonthlyInfo> eventMonthly = new List<EventMonthlyInfo>();
            WeekInfo week = new WeekInfo();
            Int32 i = 0;
            DateTime rtnDate;
            Boolean runThisWeek = false;
            Boolean runThisDay = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists(SQLDatabaseConnection.CFCAIMDatabaseFile) == false)
            {
                Application.Run(new DatabaseConnection());
            }

            //Use XML to get database					
            if (File.Exists(SQLDatabaseConnection.CFCAIMDatabaseFile))
            {
                SQLDatabaseConnection.LoadConnection(ref databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
                databaseID = SQLDatabaseConnection.GetDefaultConnection(ref databases);
                AIMManager.IsMultipleDatabase = true;
                AIMManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(ref databases, databaseID);
            }
            else
            {
                Application.Exit();
            }
                        
           
            //Get all "enabled" Events table
            events = AIMManager.GetEvents(true);
            if (events != null)
            {
                foreach (var item in events)
                {
                    startDate = item.StartDateTime.ToString("M/dd/yyyy HH:mm");
                    currentDateTime = DateTime.Now.ToString("M/dd/yyyy HH:mm");
                    dayOfWeek = DateTime.Now.DayOfWeek.ToString();
                    monthName = DateTime.Now.Month.ToString();
                    lastRunDate = Convert.ToString(item.LastRunDateTime);
                    if (lastRunDate == "1/1/0001 12:00:00 AM")
                    {
                        lastRunDate = "Never";
                    }
                    

                    def = events[i];
                    switch (item.EventRunTypeID)
                    {

                        //------------------------------------------------------------------------------
                        //Daily
                        case 1:
                            eventDaily = AIMManager.GetEventDay(item.EventID);
                            if (eventDaily != null)
                            {
                                if (eventDaily[0].RecurEvery > 1)
                                {
                                    runThisDay = false;
                                    if (lastRunDate == "Never")
                                    {
                                        runThisDay = true;
                                    }
                                    else
                                    {
                                       
                                        rtnDate = Convert.ToDateTime(lastRunDate).AddDays(eventDaily[0].RecurEvery);
                                        if (rtnDate == Convert.ToDateTime(currentDateTime))
                                        {
                                            runThisDay = true;
                                        }
                                    }
                                }
                                if (eventDaily[0].RecurEvery == 1 || runThisDay == true)
                                {
                                    CheckRunTime(item.StartDateTime, currentDateTime, lastRunDate, def);
                                }
                            }
                            break;

                        //------------------------------------------------------------------------------
                        //Weekly
                        case 2:                           
                            eventWeekly = AIMManager.GetEventWeekly(item.EventID);
                            if (eventWeekly != null)
                            {
                                foreach (var wk in eventWeekly)
                                {
                                    
                                    if (wk.WeekDayName == dayOfWeek)
                                    {
                                        if (wk.RecurEvery > 1)
                                        {
                                            runThisWeek = false;
                                            if (lastRunDate == "Never")
                                            {
                                                runThisWeek = true;
                                            }
                                            else
                                            {
                                                rtnDate = Convert.ToDateTime(lastRunDate).AddDays(wk.RecurEvery * 7);
                                                if (rtnDate == Convert.ToDateTime(currentDateTime))
                                                {
                                                    runThisWeek = true;
                                                }                                                
                                            }                                            
                                        }
                                    }

                                    if (wk.RecurEvery == 1 || runThisWeek == true)
                                    {
                                        CheckRunTime(item.StartDateTime, currentDateTime, lastRunDate, def);                                     
                                    }
                                    
                                }
                            }                            
                            break;

                        //------------------------------------------------------------------------------
                        //Monthly
                        case 3:
                            eventMonthly = AIMManager.GetEventMonthly(item.EventID);
                            if (eventMonthly != null)
                            {
                                foreach (var mon in eventMonthly)
                                {
                                    if (mon.MonthName == "All" || mon.MonthID == Convert.ToInt32(monthName))        //All months or current month              
                                    {
                                        if (mon.SpecificDays == true)                                               //Specific DATES in the month
                                        {
                                            if (mon.DayID == DateTime.Now.Day)
                                            {
                                                CheckRunTime(item.StartDateTime, currentDateTime, lastRunDate, def);
                                            }
                                        }
                                        else                                                                        //Specific DAYS of the month
                                        {
                                            if (mon.WeekNumberName == "Last")
                                            {
                                                rtnDate = GetLastDayOfTheMonth(Convert.ToDateTime(currentDateTime), (DayOfWeek)mon.WeekID - 1);     //subtracting 1 because DayOfWeek enum starts at 0                                                                                           
                                            }
                                            else
                                            {                                            
                                                rtnDate = NthOf(Convert.ToDateTime(currentDateTime), mon.WeekNumberID, (DayOfWeek)mon.WeekID - 1);  //subtracting 1 because DayOfWeek enum starts at 0                                                                                           
                                            }
                                            if (rtnDate == Convert.ToDateTime(currentDateTime).Date)
                                            {
                                                CheckRunTime(item.StartDateTime, currentDateTime, lastRunDate, def);
                                            }
                                        }                                                                               
                                    }
                                }
                            }
                            break;

                        //------------------------------------------------------------------------------
                        //One-Time
                        case 4:
                            break;
                        default:
                            break;
                    }
                    i++;
                }
            }         
            Application.Exit();
        }


        public static DateTime NthOf(this DateTime CurDate, int Occurrence, DayOfWeek Day)
        {
     
            var fday = new DateTime(CurDate.Year, CurDate.Month, 1);            

            var fOc = fday.DayOfWeek == Day ? fday : fday.AddDays(Day - fday.DayOfWeek);
            // CurDate = 2011.10.1 Occurance = 1, Day = Friday >> 2011.09.30 FIX. 
            if (fOc.Month < CurDate.Month) Occurrence = Occurrence + 1;
            return fOc.AddDays(7 * (Occurrence - 1));
        }

        public static DateTime GetLastDayOfTheMonth(DateTime date, DayOfWeek Day)
        {           
            var lastDayOfMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            while (lastDayOfMonth.DayOfWeek != Day)
                lastDayOfMonth = lastDayOfMonth.AddDays(-1);

            return lastDayOfMonth;
        }


        public static int GetWeekNumber(DateTime now)
        {
            //this returns the week# for the year
            CultureInfo ci = CultureInfo.CurrentCulture;
            int weekNumber = ci.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);

            //this returns the week# for the month
            DateTime dt = DateTime.Now;//yourdatetime
            int weekOfMonth = (dt.Day + ((int)dt.DayOfWeek) + 1) / 7;        //dt.DayOfWeek starts on monday, we're starting on Sunday                   

            return weekOfMonth;
        }

        public static Int32 CheckRunTime(DateTime startDate, String now, String lastRunDate, EventsInfo def)
        {
            Int32 rtn = 0;
            if (startDate == Convert.ToDateTime(now) || (startDate < Convert.ToDateTime(now) && lastRunDate == "Never"))
            {
                RunEvent(ref def);                
            }
            else if (startDate < Convert.ToDateTime(now))
            {
                if (startDate > Convert.ToDateTime(lastRunDate).Date)
                {
                    RunEvent(ref def);                    
                }
            }
            return rtn;
        }

        public static Int32 RunEvent(ref EventsInfo def)
        {
            Int32 rtn = 0;
        
            //Run event

            UpdateLastRunDate(ref def);
            return rtn;
        }

        public static Int32 UpdateLastRunDate(ref EventsInfo def)
        {
            Int32 rtn = 0;
            def.LastRunDateTime = DateTime.Now;
            AIMManager.SaveEvent(ref def);
            return rtn;
        }
    }
}
