using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using CFCAIMCore.DataExchange;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CFCAIMCore.DataExchange
{
	public class DataExchangeManager
	{
		public static ErrorInfo CFCProcess()
		{
			ErrorInfo error = new ErrorInfo();
			try
			{
							
			}
			catch (Exception ex)
			{
				error.ErrorMessage = ex.Message;
				error.ErrorNumber = 1;
			}
			return error;
		}


		public static ErrorInfo APIProcess(DataSourceTypeEnum dataSource)
		{
			ErrorInfo error = new ErrorInfo();

			try
			{
				switch (dataSource)
				{
					case DataSourceTypeEnum.Foss:
						break;
					case DataSourceTypeEnum.Adisseo:
						AdisseoAPI.ValidateAnalysisCode();
						break;
					case DataSourceTypeEnum.Evonik:
						break;
					case DataSourceTypeEnum.Bruker:
						break;
					default:
						AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Warning, "DataSource needs to be updated in CSVFileProcess function to process the files. Contact CTCTech", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
						error.ErrorNumber = 1;
						break;
				}
			}
			catch (Exception ex)
			{
				AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Fatal, ex.Message, (new StackTrace(ex,true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
				error.ErrorNumber = 1;
			}
			return error;
		}

		public static ErrorInfo CSVProcess(FilesInfo file,
										ref FilesCountInfo filesCount,
											DataSourceTypeEnum dataSource,
											String backup)
		{			

			Int32 aimOwnerID = 0;
			Int32 aimOwnerLocationID = 0;
			Int32 sourceID = 0;
			Int32 sourceLocationID = 0;

			return CSVProcess(file,
							ref filesCount,
								dataSource,
								backup,
								aimOwnerID,
								aimOwnerLocationID,
								sourceID,
								sourceLocationID,
								false);
		}
		public static ErrorInfo CSVProcess(
					FilesInfo file,
				ref FilesCountInfo filesCount,
					DataSourceTypeEnum dataSource,
					String backupPath,
					Int32 aimOwnerID,
					Int32 aimOwnerLocationID,
					Int32 sourceID,
					Int32 sourceLocationID,
					Boolean isSampleAdded)
		{
			ErrorInfo error = new ErrorInfo();
			AdisseoCSV adisseoCSV = new AdisseoCSV();
			EvonikCSV evonikCSV = new EvonikCSV();
			FossCSV fossCSV = new FossCSV();
			SampleInfo sample = new SampleInfo();
			Boolean isGoodFile = false;

			try
			{
				
				switch (dataSource)
				{
					case DataSourceTypeEnum.Foss:
						error = fossCSV.CSVRowProcess(
														file,
														filesCount,
													ref sample,
														aimOwnerID,
														aimOwnerLocationID,
														sourceID,
														sourceLocationID,
														isSampleAdded);
						isGoodFile = true;

						if (error.ErrorNumber > 0)
						{
							isGoodFile = false;
						}
						break;
					case DataSourceTypeEnum.Adisseo:
						isGoodFile = adisseoCSV.CSVRowProcess(
														file,
														filesCount,
													ref sample,
														aimOwnerID,
														aimOwnerLocationID,
														sourceID,
														sourceLocationID,
														isSampleAdded);
						break;
					case DataSourceTypeEnum.Evonik:
						isGoodFile = evonikCSV.CSVRowProcess(file, ref filesCount, sample);
						break;
					default:
						AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Warning, "DataSource needs to be updated in CSVFileProcess function to process the files", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
						error.ErrorNumber = 1;
						break;
				}

				if (isGoodFile == true)
				{
					file.ProcessStatus = "Success";
					file.BackupPath = backupPath;
					MoveFileToFolder(file, sample.SampleID);
				}
				else
				{
					file.ProcessStatus = "Error, not processed";
					file.BackupPath = file.ProcessPath + @"\Error";

					//cm
					//MoveFileToFolder(file, sample.SampleID);
					MoveFileToFolder(file);
					
				}
			}
			catch (Exception ex)
			{
				AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Fatal, ex.Message + file.ProcessPathAndName + " will NOT be processed", (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);				
				error.ErrorNumber = 1;
			}
			return error;
		}

		public static ErrorInfo MoveFileToFolder(FilesInfo file)
		{
			return MoveFileToFolder(file, 0);
		}

		public static ErrorInfo MoveFileToFolder(FilesInfo file, Int32 sampleID)
		{
			ErrorInfo error = new ErrorInfo();
			Int32 fileCount = 1;

			try
			{				

				file.NewFileName = String.Empty;

				if (Directory.Exists(file.BackupPath) == false)
				{
					Directory.CreateDirectory(file.BackupPath);
				}

				while (fileCount > 0)
				{
					if (File.Exists(file.BackupPathAndName) == false)
					{
						File.Move(file.ProcessPathAndName, file.BackupPathAndName);
						fileCount = 0;
					}
					else
					{
						if (file.NewFileName.Length > 0)
						{
							fileCount += 1;
							file.NewFileName = fileCount.ToString() + file.NewFileName.Substring(file.NewFileName.IndexOf("_"));
						}
						else
						{
							//for new files
							if (sampleID == 0)
							{
								file.NewFileName = fileCount + "_" + file.FileName;
							}
							else
							{
								file.NewFileName = fileCount + "_" + sampleID + "_" + file.FileName;
							}
						}
					}
				}
			}

			catch (Exception ex)
			{
				AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Fatal, ex.Message + " Unable to move file", (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);		
				error.ErrorNumber = 1;
			}
			return error;
		}

		public static Boolean ProcessBrukerResult(BrukerSQLInfo bruker, SampleInfo sample, Int32 dataSourceTypeID, Int32 aimDatabaseID)
		{
			return BrukerSQL.ProcessBrukerResult(bruker, sample, dataSourceTypeID, aimDatabaseID);
		}
	}
}
