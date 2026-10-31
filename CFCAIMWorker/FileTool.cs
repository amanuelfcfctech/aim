using CFCAIMCore.Entity;
using System;
using System.IO;

namespace CFCAIMWorker
{
	public class FileTool
    {

        #region Globals
        String _strFolderMoveError = "";
        String _strArchivePath = String.Empty;
        #endregion

        #region Globals
        public static ErrorInfo MoveFile(FileInfo fromFile, String toPath)
        {
            ErrorInfo error = new ErrorInfo();
            String newFile = String.Empty;
            Int32 fileCount = 1;

            try
            {
                error.ErrorMessage = "Successful move file.";
                error.ErrorNumber = 0;

                if (Directory.Exists(toPath) == false)
                {
                    Directory.CreateDirectory(toPath);
                }

                newFile = toPath + "\\" + fromFile.Name;
                if (File.Exists(newFile) == false)
                {
                    File.Move(fromFile.FullName, newFile);
                    fileCount = 0;
                }
                else
                {
                    newFile = toPath + "\\" + fileCount.ToString() + "_" + fromFile.Name;
                    while (File.Exists(newFile) == true)
                    {
                        fileCount += 1;
                        newFile = toPath + "\\" + fileCount.ToString() + "_" + fromFile.Name;
                    }
                    File.Move(fromFile.FullName, newFile);
                }
            }
            catch (Exception ex)
            {
                error.ErrorMessage = ex + ". Unable to move file.";
                error.ErrorNumber = 1;
            }
            return error;
        }
        #endregion
    }
}
