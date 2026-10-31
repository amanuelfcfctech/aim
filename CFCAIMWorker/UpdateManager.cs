using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCAPI;
// cursor change 2026-10-01 - Option B: entity DTOs come from the single CFCAPICore store (was CFCAPI.Entity).
using CFCAPICore.Entity.Updater;
using CFCAPICore.Entity;
using CFCAPICore.Entity.Updater;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CFCAIMWorker
{
    public static class UpdateManager
    {
        public static void UpdateCFCUpdaterIfAvailable()
        {
            String flagPath = Path.Combine(WorkerGlobal.ApplicationParentDirectory, "pending_update.flag");
            if (File.Exists(flagPath))
            {
                Int32 softwareID = 0;
                String version = String.Empty;
                String UpdaterDecompressedUpdatePath = String.Empty;
                String applicationDirectory = String.Empty;
                DateTime updateStamp = DateTime.MinValue;

                String[] lines = File.ReadAllLines(flagPath);
                Dictionary<String, String> values = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
                foreach (String line in lines)
                {
                    Int32 colonIndex = line.IndexOf(':');
                    if (colonIndex > 0)
                    {
                        String key = line.Substring(0, colonIndex).Trim();
                        String value = line.Substring(colonIndex + 1).Trim();
                        values[key] = value;
                    }
                }

                try
                {
                    softwareID = Int32.TryParse(values["SoftwareID"], out Int32 id) ? id : 0;
                    version = values["Version"];
                    UpdaterDecompressedUpdatePath = values["UpdaterDecompressedUpdatePath"];
                    applicationDirectory = values["ApplicationDirectory"];
                    updateStamp = DateTime.TryParse(values["UpdateStamp"], out DateTime dt) ? dt : DateTime.MinValue;

                }
                catch
                {
                    //TODO: Error handling
                }

                Boolean isValid = softwareID > 0 &&
                    !String.IsNullOrWhiteSpace(version) &&
                    !String.IsNullOrWhiteSpace(UpdaterDecompressedUpdatePath) &&
                    !String.IsNullOrWhiteSpace(applicationDirectory) &&
                    updateStamp > DateTime.MinValue &&
                    Directory.Exists(UpdaterDecompressedUpdatePath) &&
                    Directory.Exists(applicationDirectory);

                if (isValid)
                {
                    ExecuteUpdate(UpdaterDecompressedUpdatePath, applicationDirectory);
                    File.Delete(flagPath);

                    // Update our server's database with the customer's new Updater app version
                    CustomerProgramVersionInfo customerProgramVersion = new CustomerProgramVersionInfo
                    {
                        Token = APIGlobal.ApiToken.Token,
                        CustomerID = APIGlobal.ApiToken.CustomerID,
                        SoftwareID = softwareID,
                        Version = version,
                        ComputerName = Environment.MachineName,
                        FilePath = applicationDirectory,
                    };
                    UpdaterClient.PutCustomerVersion(customerProgramVersion);
                }
                else
                {
                    //TODO: Handle error
                }

            }
        }
        public static void ExecuteUpdate(String unzipDownloadFilePath, String applicationDirectory)
        {
            // Copy contents using VB method //TODO: Add safety net if file is not found
            new Computer().FileSystem.CopyDirectory(unzipDownloadFilePath, applicationDirectory, overwrite: true);

            // Delete the unzipped download file after update is complete
            Directory.Delete(unzipDownloadFilePath, recursive: true);
        }

        public static void UpdateProgramVersionRecord()
        {
            Library.WriteErrorLogFile($"Updating Program Version Record Code Version: {WorkerGlobal.ProgramVersion}, Settings Version: {WorkerGlobal.SystemOptions.ProgramVersion}, IsUpdating Setting: {WorkerGlobal.SystemOptions.IsUpdating}");

            if (WorkerGlobal.ProgramVersion != WorkerGlobal.SystemOptions.ProgramVersion || WorkerGlobal.SystemOptions.IsUpdating == true)
            {
                CustomerProgramVersionInfo customerProgramVersion = new CustomerProgramVersionInfo();
                ErrorInfo error = new ErrorInfo();
                Boolean isUpdateError = false;
                String errorLogMessage = String.Empty;
                Int32 actualProgramVersion = 0;
                Int32 expectedProgramVersion = 0;
                Int32.TryParse(WorkerGlobal.ProgramVersion, out actualProgramVersion);
                Int32.TryParse(WorkerGlobal.SystemOptions.ProgramVersion, out expectedProgramVersion);

                customerProgramVersion = new CustomerProgramVersionInfo
                {
                    Token = APIGlobal.ApiToken.Token,
                    CustomerID = APIGlobal.ApiToken.CustomerID,
                    SoftwareID = (Int32)SoftwareTypeEnum.CFCAIMWorker,
                    Version = WorkerGlobal.ProgramVersion,
                    ComputerName = Environment.MachineName,
                    FilePath = WorkerGlobal.ApplicationDirectory,
                    IsUpdateError = isUpdateError,
                    ErrorLogMessage = errorLogMessage
                };

                if (WorkerGlobal.SystemOptions.IsUpdating == true && actualProgramVersion < expectedProgramVersion)
                {
                    //Update Failed
                    isUpdateError = true;
                    errorLogMessage = "Updater Failed. IsUpdating is true and Expected version is later than the actual version";
                }
                else if (WorkerGlobal.SystemOptions.IsUpdating == true)
                {
                    //update success
                    UpdaterClient.PostCustomerProgramVersionUpdateRequestComplete(customerProgramVersion);
                }

                error = UpdaterClient.PutCustomerVersion(customerProgramVersion);
                Library.WriteErrorLogFile($"PutCustomerVersion() returned: {error.RecordID}, {error.ErrorMessage}");

                WorkerGlobal.SystemOptions.ProgramVersion = WorkerGlobal.ProgramVersion;
                WorkerGlobal.UpdateSetting("AppVersion", WorkerGlobal.SystemOptions.ProgramVersion);

                WorkerGlobal.SystemOptions.IsUpdating = false;
                WorkerGlobal.UpdateSetting("IsUpdating", WorkerGlobal.SystemOptions.IsUpdating);
            }
        }
        public static Boolean IsUpdateScheduled()
        {
            String scheduledUpdateDate = String.Empty;
            scheduledUpdateDate = WorkerGlobal.SystemOptions.ScheduledUpdateDate;
            Int32 scheduledUpdateDateValue = 0;
            Int32.TryParse(scheduledUpdateDate, out scheduledUpdateDateValue);
            Boolean isUpdateScheduled = (Int32.Parse(DateTime.Now.ToString("yyyyMMdd")) > scheduledUpdateDateValue);

            Library.WriteErrorLogFile($"scheduledUpdate: {scheduledUpdateDateValue}, result: {isUpdateScheduled}");
            return isUpdateScheduled;
        }
        public static void CheckSoftwareUpdate(ref Boolean isUpdateApproved, ref String updateFileName)
        {
            ProgramVersionInfo newProgramVersion = new ProgramVersionInfo();
            String scheduledUpdateDate = String.Empty;

            scheduledUpdateDate = DateTime.Now.AddDays(30).ToString("yyyyMMdd");
            newProgramVersion = UpdaterClient.GetUpdateVersion(APIGlobal.ApiToken.Token, (Int32)SoftwareTypeEnum.CFCAIMWorker, WorkerGlobal.ProgramVersion);
            Library.WriteErrorLogFile($"New Program Found?: {newProgramVersion.ProgramVersionID}, {newProgramVersion.ErrorMessage}");
            if (newProgramVersion == null || newProgramVersion.ProgramVersionID > 0 && newProgramVersion.FileName.Length == 0)
            {
                //TODO: Error
            }
            else if (newProgramVersion.ProgramVersionID > 0)
            {
                isUpdateApproved = UpdaterClient.GetIsUpdateApproved(
                    APIGlobal.ApiToken.Token,
                    APIGlobal.ApiToken.CustomerID,
                    (Int32)SoftwareTypeEnum.CFCAIMWorker,
                    Environment.MachineName,
                    WorkerGlobal.ApplicationDirectory
                    );

                Library.WriteErrorLogFile($"Is Update Request Approved: {isUpdateApproved}");
                if (isUpdateApproved)
                {
                    isUpdateApproved = true;
                    updateFileName = newProgramVersion.FileName;

                    WorkerGlobal.SystemOptions.ProgramVersion = newProgramVersion.Version;
                    WorkerGlobal.UpdateSetting("AppVersion", WorkerGlobal.SystemOptions.ProgramVersion);

                    WorkerGlobal.SystemOptions.IsUpdating = true;
                    WorkerGlobal.UpdateSetting("IsUpdating", WorkerGlobal.SystemOptions.IsUpdating);
                }
                else
                {
                    scheduledUpdateDate = DateTime.Now.AddDays(2).ToString("yyyyMMdd");
                }
            }

            WorkerGlobal.SystemOptions.ScheduledUpdateDate = scheduledUpdateDate;
            WorkerGlobal.UpdateSetting("ScheduledUpdateDate", WorkerGlobal.SystemOptions.ScheduledUpdateDate);
        }
        public static void LaunchCFCUpdaterAndExit(String updateFileName)
        {
            String apiKey = CFCAPIManager.Setting.AIMAPIKey;
            String applicationDirectory = WorkerGlobal.ApplicationDirectory;
            String applicationParentDirectory = Path.GetDirectoryName(applicationDirectory);
            String CFCUpdaterPath = Path.Combine(applicationParentDirectory, "CFCUpdater", "CFCUpdater.exe");

            String arguments = $"/source={WorkerGlobal.ApplicationName} " +
                               $"/applicationDirectory={applicationDirectory} " +
                               $"/applicationSoftwareID={(Int32)SoftwareTypeEnum.CFCAIMWorker} " +
                               $"/updateFileName={updateFileName} " +
                               $"/apiKey={apiKey} " +
                               $"/cfcTechAPIProdURL={APIGlobal.CFCTechAPIURL} " +
                               $"/cfcTechAPIMaintenanceProdURL={APIGlobal.CFCTechAPIMaintenanceURL}";

            Library.WriteErrorLogFile($"Launching Updater with Params: {WorkerGlobal.ApplicationName}, {applicationDirectory}, {(Int32)SoftwareTypeEnum.CFCAIMWorker}, {updateFileName}, {apiKey}");
            if (File.Exists(CFCUpdaterPath))
            {
                Process.Start(CFCUpdaterPath, arguments);
                Application.Exit();
            }
            else
            {
                Library.WriteErrorLogFile("Failed to locate CFCUpdater. Please Contact CFC Services.");
            }
        }
    }
}
