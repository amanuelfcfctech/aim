using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

namespace CFCAIM.AIMUtilities
{
	public class GlobalFunctions
	{
        public static Guid ConvertIntToGuid(Int32 val)
        {
            byte[] bytes = new byte[32];
            BitConverter.GetBytes(val).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
		public static Int32 CheckUserPermissions(Int32 cfcUserID, Int32 permissionID)
		{
			Int32 rtn = 0;
			String msg = "";
			List<UserAccessInfo> userAccess = new List<UserAccessInfo>();
			userAccess = AIMManager.GetUserAccess(cfcUserID, permissionID);
			if (userAccess.Count == 0)
			{
				if (permissionID == (Int32)EnumPermissionTable.AnalysisExport)
				{
					msg = "You do not have permission to export analysis data.";
                    MessageBox.Show(msg, "Warning...");
                }
                else if (permissionID == (Int32)EnumPermissionTable.FormulationOptions)
                { }
                else if (permissionID == (Int32)EnumPermissionTable.AllowChangeSampleMaterial)      //4/1/19
                { }
                else
				{
					msg = "You do not have permission to access this form.";
                    MessageBox.Show(msg, "Warning...");
                }
				
				rtn = -1;
			}
			return rtn;
		}		
	}
}
