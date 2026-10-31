using System;


namespace CFCAIMCore.Entity
{
	public class SettingInfo
	{
		public Int32 SettingID { get; set; }
		public Boolean IsDebug { get; set; }
		public Boolean IsTestUrl { get; set; }
		public String APIUrl { get; set; }
		public String APIUrlTest { get; set; }
		public String AIMAPIKey { get; set; }
		public String AIMInstrumentKey { get; set; }
		public String FossCSVPath { get; set; }
		public String FossNIRPath { get; set; }
		public String FossArchivePath { get; set; }
		public String FossErrorPath { get; set; }
		public Boolean IsPNETestUrl { get; set; }
		public String PNEAPIUrl { get; set; }
		public String PNEAPITestUrl { get; set; }
	}
	
}

