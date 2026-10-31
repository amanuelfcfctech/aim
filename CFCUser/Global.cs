using CFCUser.Entity;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CFCUser
{
    public class Global
    {
        private static MainMenu _mainMenu;                              //Task991
        private static UserInfo _cfcLoginUser = new UserInfo();
        private static String _applicationName = String.Empty;
        private static String _companyName = String.Empty;

        //12/6/21
        private static Int32 _aimDatabaseID;
        private static String _cfcLoginUserName = String.Empty;

        //10/21/22
        //private static String 

        //todo String.Empty;
        private static String _programDatabaseVersion = "2026-08-18";  //CHANGE THIS DATE WITH EACH NEW VERSION    //Mark, when was this added?         -

        //10/21/22
        private static String _programAppVersion = String.Empty;
        private static Icon _aimIcon;

        #region variables
        public Icon AIMIcon
        {
            get
            {
                return _aimIcon;
            }
            set
            {
                _aimIcon = value;
            }
        }

        public String ProgramAppVersion
        {
            get
            {
                return _programAppVersion;
            }
            set
            {
                _programAppVersion = value;
            }
        }
        //10/21/22
        public String ProgramDatabaseVersion
        {
            get
            {
                return _programDatabaseVersion;
            }
            set
            {
                _programDatabaseVersion = value;
            }
        }

        public MainMenu MainMenu
        {
            get
            {
                return _mainMenu;
            }
            set
            {
                _mainMenu = value;
            }
        }

        public UserInfo CFCLoginUser
        {
            get
            {
                return _cfcLoginUser;
            }
            set
            {
                _cfcLoginUser = value;
            }
        }

        public String CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
            }
        }
        public String ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        //12/6/21
        public Int32 AIMDatabaseID
        {
            get
            {
                return _aimDatabaseID;
            }
            set
            {
                _aimDatabaseID = value;
            }
        }

        //12/6/21
        public String CFCLoginUserName
        {
            get
            {
                return _cfcLoginUserName;
            }
            set
            {
                _cfcLoginUserName = value;
            }
        }
        #endregion

        public static void open_form(Form myform)
        {
            try
            {
                //if (AIMGlobal.activeDataMap == 0)
                //{
                //    if (Convert.ToBoolean(CFCAIM.AIMGlobal.AimDatabaseExists()) == false)
                //    {
                //        DataMapping form = new DataMapping();
                //        form.MdiParent = _mainMenu;
                //        form.Show();
                //        form.Activate();

                //        return;
                //    }
                //    else
                //    {
                //        return;
                //    }
                //}

                Boolean formOpened = false;
                FormCollection openForms = Application.OpenForms;
                foreach (Form item in openForms)
                {
                    if (myform.Name == item.Name)
                    {
                        item.WindowState = FormWindowState.Maximized;

                        item.Activate();
                        formOpened = true;
                    }
                }
                if (formOpened == false)
                {
                    //Task991 SplashScreenShow();
                    //Task991  myform.MdiParent = _mainMenu;
                    myform.Show();
                    myform.WindowState = FormWindowState.Maximized;
                    //Task991 SplashScreenHide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
