using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Data;
using System.Reflection;
using Core.Library.Data;

namespace CFCAIM
{
	public sealed class Test<T>
	{
		public static void Load(ref CheckedComboBoxEdit cmbDay, IList<T> eventDay, IList day, String display, String value)
		{
			String checkValues = String.Empty;
			T retVal = default(T);
			System.Collections.Hashtable _hashtable = null;

			_hashtable = AttributeUtility.GetPropertyList(typeof(T), typeof(DataReaderColumnMappingAttribute));
			Type x = typeof(T);
			retVal = (T)(Activator.CreateInstance(x));
			cmbDay.Properties.DataSource = day;
			cmbDay.Properties.DisplayMember = display;
			cmbDay.Properties.ValueMember = value;
			foreach (T item in eventDay)
			{
				//item
				if ("" == "")
				{
					
				}
			}

			for (int i = 0; i < eventDay.Count; i++)
			{
				if (checkValues != String.Empty)
				{
					checkValues += ",";
				}
				//				checkValues += String.Format("{0}", eventDay[i].DayID);
			}

			if (checkValues != String.Empty)
			{
				cmbDay.SetEditValue(checkValues);
			}
		}
	}
}
