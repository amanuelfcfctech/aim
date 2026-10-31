using System;
using System.Drawing;
using System.Windows.Forms;

namespace CFCFormsControlLibrary
{
	public class FormsControl
	{
		#region  local variables
		private static Boolean _isMessageDisplay = true;
		private static String _cfcDatabaseFile = String.Empty;
		private static Icon _formIcon;
        private static String _programVersion = String.Empty;
        private static String _databaseConnection = String.Empty;

        public static String ProgramVersion
        {
            get
            {
                return _programVersion;
            }
            set
            {
                _programVersion = value;
            }
        }
        public static String DatabaseConnection
        {
            get
            {
                return _databaseConnection;
            }
            set
            {
                _databaseConnection = value;
            }
        }
        public static Boolean IsMessageDisplay
		{
			get
			{
				return _isMessageDisplay;
			}

			set
			{
				_isMessageDisplay = value;
			}
		}
		public static String CFCDatabaseFile
		{
			get
			{
				return _cfcDatabaseFile;
			}

			set
			{
				_cfcDatabaseFile = value;
			}
		}
		public static Icon FormIcon
		{
			get
			{
				return _formIcon;
			}

			set
			{
				_formIcon = value;
			}
		}
		#endregion

		public static Boolean CloseAllForms()
		{
			DialogResult dr = DialogResult.Yes;
			Boolean isCancel = false;
			Int32 count = 0;

			for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
			{
				if (Application.OpenForms[i].Name != "MainMenu" && Application.OpenForms[i].Name.Length > 0)
				{
					count += 1;
				}
			}

			if (count > 0)
			{
				if (IsMessageDisplay == true)
				{
					dr = MessageBox.Show("There are multiple forms open.  Did you save all your work?", "Warning...", MessageBoxButtons.YesNo);
				}
				IsMessageDisplay = false;

				if (dr == DialogResult.No)
				{
					isCancel = true;
				}
				else
				{
					for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
					{
						if (Application.OpenForms[i].Name != "MainMenu")
						{
							Application.OpenForms[i].Close();
						}
					}
				}
			}
			return isCancel;
		}
	}
}
