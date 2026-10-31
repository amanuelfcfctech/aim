using System;
using System.Data;
using Core.Library.Data;
using System.Data.SqlTypes;

namespace CFCAIMCore.Entity
{
	[Serializable()]
	public class SampleNumberOfDays : DataObjectBase
	{
		[DataReaderColumnMapping()]
		public Int32 SampleNumberOfDaysID { get; set; }

		[DataReaderColumnMapping()]
		public Int32 NumberOfDays { get; set; }

		public String DaysDisplay
		{
			get
			{
				if (NumberOfDays == 0)
				{
					return "--Select--";
				}
				if (NumberOfDays == 30)
				{
					return "30 Days";
				}
				else if (NumberOfDays == 60)
				{
					return "60 Days";
				}
				else if (NumberOfDays == 90)
				{
					return "90 Days";
				}
				else if (NumberOfDays == 120)
				{
					return "120 Days";
				}
				else if (NumberOfDays == 180)
				{
					return "180 Days";
				}
				else if (NumberOfDays == 365)
				{
					return "1 Year ";
				}
				else if (NumberOfDays == 730)
				{
					return "2 Years";
				}
				else if (NumberOfDays == 1095)
				{
					return "3+ Years";
				}				
				else
				{
					return "";
				}
			}
		}

	}
}
