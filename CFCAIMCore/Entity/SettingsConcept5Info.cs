using System;


namespace CFCAIMCore.Entity
{
	public class SettingsConcept5Info
	{
		public Int32 SettingsConcept5ID { get; set; }
		public String FormulationPath { get; set; }
		public String DatabaseConnection { get; set; }
		public Int32 SyncTypeNum { get; set; }
		public String SyncType(String desc)
		{
			if (SyncTypeNum == 1)
			{
				desc = "Quick";
			}
			else if (SyncTypeNum == 2)
			{
				desc = "Full";
			}						
			else
			{
				desc = "";
			}
			return desc;
		}
	}

}

