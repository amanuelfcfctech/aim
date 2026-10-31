using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
// cursor change 2026-10-01 - Option B: entity DTOs come from the single CFCAPICore store (was CFCAPI.Entity).
using CFCAPICore.Entity.Updater;
using Core.Library.SQLDatabaseConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using CFCAPICore.Entity;

namespace CFCAIMWorker
{
    public static class WorkerGlobal
    {
        #region Member Variables
        private static List<SQLDatabaseInfo> _databases = new List<SQLDatabaseInfo>();
        private static SystemOptionInfo _systemOptions = new SystemOptionInfo();
        private static APITokenInfo _apiToken = new APITokenInfo();
        private static String _programVersion = "2026-09-30";
        private static String _databaseXML = String.Empty;
        private static String _applicationName = "CFCAIMWorker";
        private static String _ProgramSettingsFilePath = String.Empty;

        public static List<SQLDatabaseInfo> Databases
        {
            get
            {
                return _databases;
            }
            set
            {
                _databases = value;
            }
        }
        public static SystemOptionInfo SystemOptions
        {
            get
            {
                return _systemOptions;
            }
            set
            {
                _systemOptions = value;
            }
        }
        public static APITokenInfo ApiToken
        {
            get
            {
                return _apiToken;
            }
            set
            {
                _apiToken = value;
            }
        }
        public static String ProgramVersion
        {
            get
            {
                return _programVersion;
            }
        }
        public static String DatabaseXML
        {
            get
            {
                return _databaseXML;
            }
            set
            {
                _databaseXML = value;
            }
        }
        public static String ApplicationName
        {
            get
            {
                return _applicationName;
            }
        }
        public static String CFCAIMWorkerUpdaterFilePath
        {
            get
            {
                return _ProgramSettingsFilePath;
            }
            set
            {
                _ProgramSettingsFilePath = value;
            }
        }
        public static String ApplicationDirectory
        {
            get
            {
                return Application.StartupPath;
            }
        }
        public static String ApplicationParentDirectory
        {
            get
            {
                return Path.GetDirectoryName(ApplicationDirectory);
            }
        }
        #endregion

        public static Boolean SetUpDatabaseConnection(String connetionName)
        {
            Boolean isError = false;
            SQLDatabaseInfo database = new SQLDatabaseInfo();
            List<SQLDatabaseInfo> databases = Databases;
            SQLDatabaseConnection.LoadConnection(ref databases, true, DatabaseXML);
            database = databases.FirstOrDefault(d => d.ConnectName == connetionName);

            if (database == null)
            {
                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "No database connection is setup");
                Library.WriteErrorLogFile("No database connection is setup");
                isError = true;
            }
            else
            {
                if (connetionName == "NIR")
                {
                    BrukerManager.CreateBrukerConnection(database.ServerName, database.Database, database.IsSQLAuthentication, database.User, database.Password);
                }
                else if (connetionName == "AIM")
                {
                    AIMManager.IsMultipleDatabase = true;
                    AIMManager.CreateConnection(database.ServerName, database.Database, database.IsSQLAuthentication, database.User, database.Password);
                }


                Library.WriteErrorLogFile("ServerName = " + database.ServerName);
                Library.WriteErrorLogFile("Database = " + database.Database);
                Library.WriteErrorLogFile("IsSQLAuthentication = " + database.IsSQLAuthentication.ToString());
                Library.WriteErrorLogFile("User = " + database.User);
            }

            return isError;
        }

        public static void UpdateSetting(String settingName, Object newValue)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(CFCAIMWorkerUpdaterFilePath);

            XmlNode node = doc.SelectSingleNode($"//setting[@name='{settingName}']/value");
            if (node != null)
            {
                // Convert any value to string, then lowercase
                node.InnerText = newValue?.ToString().ToLower() ?? "";
                doc.Save(CFCAIMWorkerUpdaterFilePath);
            }
        }
    }
}
