using System;
using System.Linq;
using System.ComponentModel;
using CFCAIMCore.Entity;
using System.IO;
using CFCAIMCore.Manager;
using System.Windows;
using System.Windows.Forms;

namespace CFCAIM.DataExchange
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
				
		#region XLSXProcess
		public static ErrorInfo XLSXProcess(
					FilesInfo file,
					ref FilesCountInfo filesCount,
					DataSourceTypeEnum dataSource,
					String backupPath,					
					Boolean isSampleAdded,
					Int32 sheetNumber,                    
					String excelFileExtension,
					BackgroundWorker backgroundWorker)
		{
			ErrorInfo error = new ErrorInfo();
			SampleInfo sample = new SampleInfo();
			Boolean isGoodFile = false;
			
			try
			{
				switch (dataSource)
				{					
					case DataSourceTypeEnum.Evonik:						
						isGoodFile = Import.ImportExcel.Landscape(file, ref filesCount, sample, (Int32)dataSource, sheetNumber, 5, 11, excelFileExtension, backgroundWorker);
						break;

					case DataSourceTypeEnum.Specman:
						isGoodFile = Import.ImportExcel.Landscape(file, ref filesCount, sample, (Int32)dataSource, sheetNumber, 6, 10, excelFileExtension, backgroundWorker);
						break;

					case DataSourceTypeEnum.ABVista:						
						isGoodFile = Import.ImportExcel.Portrait(file, ref filesCount, sample, (Int32)dataSource, sheetNumber, excelFileExtension, backgroundWorker);
						break;

					default:
						error.ErrorMessage = "DataSource needs to be updated in XLSXFileProcess function to process the files. Contact IT.";
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
					Int32 sampleID = 0;
					if (sample != null)
					{
						sampleID = sample.SampleID;
					}					
					MoveFileToFolder(file, sampleID);

				}
			}
			catch (Exception ex)
			{
				error.ErrorMessage = ex.Message + " The file " + file.ProcessPathAndName + " will NOT be processed. Processing other files, if there are any.";
				error.ErrorNumber = 1;
			}
			return error;
		}
		#endregion			
	
		#region APIProcess
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
						error.ErrorMessage = "DataSource needs to be updated in CSVFileProcess function to process the files. Contact IT.";
						error.ErrorNumber = 1;
						break;
				}
			}
			catch (Exception ex)
			{
				error.ErrorMessage = ex.Message;
				error.ErrorNumber = 1;
			}
			return error;
		}
		#endregion

		#region SQLDatabaseProcess
		public static ErrorInfo SqlDatabaseProcess(
			DataSourceTypeEnum dataSourceTypeID,
			DateTime startDate,
			DateTime endDate,
			String sampleCode,			
			Int32 sqlLogID,
			String connectionName,
			ref FilesCountInfo filesCount)
		{
			ErrorInfo error = new ErrorInfo();											
			SampleInfo sample = new SampleInfo();
			Boolean isGoodFile = false;					

			try
			{
				switch (dataSourceTypeID)
				{					
					case DataSourceTypeEnum.Perten:						
                        isGoodFile = Import.ImportSqlDatabase.SqlDatabase(dataSourceTypeID, startDate, endDate, sampleCode, sqlLogID, connectionName, ref filesCount);
                        break;

                    case DataSourceTypeEnum.Bruker:
                        isGoodFile = Import.ImportSqlDatabase.SqlDatabase(dataSourceTypeID, startDate, endDate, sampleCode, sqlLogID, connectionName, ref filesCount);
                        break;

                    default:
						error.ErrorMessage = "DataSource needs to be updated in DatabaseProcess function to process the files. Contact IT.";
						error.ErrorNumber = 1;
						break;
				}

				if (isGoodFile == true)
				{										
				}
				else
				{
					AIMGlobal.importError = -1;
				}
			}
			catch (Exception ex)
			{
				error.ErrorMessage = ex.Message;
				error.ErrorNumber = 1;
			}
			return error;
		
		}
		#endregion

		#region CSVProcess
		public static ErrorInfo CSVProcess(FilesInfo file,
										ref FilesCountInfo filesCount,
											DataSourceTypeEnum dataSource,
											String backup)
		{

			Int32 aimOwnerID = 0;
			Int32 aimOwnerLocationID = 0;
			Int32 aimsourceID = 0;
			Int32 aimsourceLocationID = 0;
			Int32 delimiterTypeID = 0;

			return CSVProcess(file,
							ref filesCount,
								dataSource,
								backup,							
								aimOwnerID,
								aimOwnerLocationID,
								aimsourceID,
								aimsourceLocationID,
								false,
								delimiterTypeID);
		}
		public static ErrorInfo CSVProcess(
					FilesInfo file,
				    ref FilesCountInfo filesCount,
					DataSourceTypeEnum dataSource,
					String backupPath,
					Int32 aimOwnerID,
					Int32 aimOwnerLocationID,
					Int32 aimsourceID,
					Int32 aimsourceLocationID,
					Boolean isSampleAdded,
					Int32 delimiterTypeID)
		{
			ErrorInfo error = new ErrorInfo();

            FossCSV fossCSV = new FossCSV();
            CFCTechCSV cfcTechCSV = new CFCTechCSV();
            AdisseoCSV adisseoCSV = new AdisseoCSV();
            PertenCSV pertenCSV = new PertenCSV();
            EvonikCSV evonikCSV = new EvonikCSV();			
			LabAdvantageCSV labAdvantageCSV = new LabAdvantageCSV();

			//8/12/22
			RomerLabsCSV romerLabsCSV = new RomerLabsCSV();

			SampleInfo sample = new SampleInfo();
			Boolean isGoodFile = false;

			try
			{
				switch (dataSource)
				{

//----------------------------------------------------------------------------------------------------------
//FOSS - CSV (landscape) 
					case DataSourceTypeEnum.Foss:
						error = fossCSV.CSVRowProcess(file,	filesCount,	ref sample,	aimOwnerID,	aimOwnerLocationID,
														aimsourceID, aimsourceLocationID, isSampleAdded, delimiterTypeID);
						isGoodFile = true;
						if (error.ErrorNumber > 0)
						{
							isGoodFile = false;
						}
						break;

//--------------------------------------------------------------------------------------------------------
//CFCTECH - CSV (landscape) 
                    case DataSourceTypeEnum.CFCTech:
                        error = cfcTechCSV.CSVRowProcess(file, filesCount, ref sample);
                        isGoodFile = true;
                        if (error.ErrorNumber > 0)
                        {
                            isGoodFile = false;
                        }
                        break;

//--------------------------------------------------------------------------------------------------------
//ADISSEO - CSV (landscape) 
                    case DataSourceTypeEnum.Adisseo:
						isGoodFile = adisseoCSV.CSVRowProcess(file,	filesCount, ref sample,	aimOwnerID,	aimOwnerLocationID,	
                                                        aimsourceID, aimsourceLocationID, isSampleAdded, delimiterTypeID);
						break;

//--------------------------------------------------------------------------------------------------------
//PERTEN - CSV - (portrait)
                    //6/27/17
                    //case DataSourceTypeEnum.Perten:
                    //    error = pertenCSV.CSVRowProcess(file, filesCount, ref sample, aimOwnerID, aimOwnerLocationID,
                    //                                    aimsourceID, aimsourceLocationID, isSampleAdded, delimiterTypeID);
                    //    isGoodFile = true;

                    //    if (error.ErrorNumber > 0)
                    //    {
                    //        isGoodFile = false;
                    //    }
                    //    break;


//--------------------------------------------------------------------------------------------------------
//EVONIK - CSV (landscape) 
                    case DataSourceTypeEnum.Evonik:
						isGoodFile = evonikCSV.CSVRowProcess(file, ref filesCount, sample);
						break;

//--------------------------------------------------------------------------------------------------------
//LAB ADVANTAGE - CSV (landscape) 
                    case DataSourceTypeEnum.LabAdvantage:
						error = labAdvantageCSV.CSVRowProcess(file,	filesCount,	ref sample,	aimOwnerID,	aimOwnerLocationID,
														aimsourceID, aimsourceLocationID, isSampleAdded, delimiterTypeID);
						isGoodFile = true;
						if (error.ErrorNumber > 0)
						{
							isGoodFile = false;
						}
						break;

//--------------------------------------------------------------------------------------------------------
//8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left this code here. Could be used in future.
//ROMER LABS - CSV (portrait) 
					case DataSourceTypeEnum.Romer:
						//error = romerLabsCSV.CSVRowProcess(file, filesCount, ref sample, headerRow, detailRow, aimOwnerID, aimOwnerLocationID,
						//								aimsourceID, aimsourceLocationID, isSampleAdded, delimiterTypeID);
						//isGoodFile = true;
						//if (error.ErrorNumber > 0)
						//{
						//	isGoodFile = false;
						//}
						//break;
						error.ErrorMessage = "DataSource needs to be updated in CSVFileProcess function to process the files. Contact IT.";
						System.Windows.MessageBox.Show(error.ErrorMessage, "Error...");
						error.ErrorNumber = 1;
						break;				


				//--------------------------------------------------------------------------------------------------------
				default:
						error.ErrorMessage = "DataSource needs to be updated in CSVFileProcess function to process the files. Contact IT.";
						error.ErrorNumber = 1;
						break;
						}

//--------------------------------------------------------------------------------------------------------
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

					Int32 sampleID = 0;
					if (sample != null)
					{
						sampleID = sample.SampleID;

						//MoveFileToFolder(file, sample.SampleID);
						MoveFileToFolder(file, sampleID);
					}

					//4/20/21 - moved above
					////MoveFileToFolder(file, sample.SampleID);
					//MoveFileToFolder(file, sampleID);

				}
			}
			catch (Exception ex)
			{
				error.ErrorMessage = ex.Message + " The file " + file.ProcessPathAndName + " will NOT be processed. Processing other files, if there are any.";
				error.ErrorNumber = 1;
			}
			return error;
		}
		#endregion		

		#region XMLProcess
		public static ErrorInfo XMLProcess(
			DataSourceTypeEnum dataSource,
			String backupPath,			
			FilesInfo file,
			ref FilesCountInfo filesCount,
			Int32 aimOwnerID, 
			Int32 aimOwnerLocationID,
			String comingFrom,
			String filePathAndName, 			
			BackgroundWorker backgroundWorker,
            Int32 batchID
            )
		{
			ErrorInfo error = new ErrorInfo();
			Boolean isGoodFile = false;
				
			
			try
			{
				switch (dataSource)
				{
					case DataSourceTypeEnum.NutriQuest:                    
                        isGoodFile = NutriQuestXML.ImportXML(file.ProcessPathAndName, ref filesCount, aimOwnerID, aimOwnerLocationID, comingFrom, filePathAndName, backgroundWorker, batchID);
						break;
					
					case DataSourceTypeEnum.CFCTech:
						isGoodFile = CFCTechXML.ImportXML(file.ProcessPathAndName, ref filesCount, comingFrom, filePathAndName, backgroundWorker, batchID);
						break;

					default:
						error.ErrorMessage = "DataSource needs to be updated in XMLProcess function to process the files. Contact IT.";
						error.ErrorNumber = 1;
						break;
				}

				//Check for sample errors
				Int32 errorCount = 0;				
				foreach (SystemImportCFCTechXMLSampleInfo item in CFCAIM.AIMGlobal.systemImportCFCTechXMLSampleHeader)
				{										
					if (item.Status == (Int32)AcceptReject.Reject)
					{
						errorCount++;
					}
				}
				
				if (isGoodFile == true)
				{
                    //1/31/19
					if (AIMGlobal.importError != 0)
					{
                        file.ProcessStatus = "Error, not processed";
                        file.BackupPath = file.ProcessPath + @"\Error";                        
                        MoveFileToFolder(file);
                    }

					else
					{
						if (comingFrom == "Import")
						{
							var query = (
								from f in CFCAIM.AIMGlobal.systemImportCFCTechXMLSampleHeader
								where f.Status == (Int32)AcceptReject.Accept
								group f by f.FilePathAndName into fileGroup
								select new { filepath = fileGroup.Key, Names = fileGroup }).
									OrderBy(letter => letter.filepath);

							foreach (var f in query)
							{
								file.ProcessStatus = "Success";
								file.BackupPath = backupPath;
								MoveFilesToBackup(file, f.filepath, 0);
							}
						}
						else
						{

							file.ProcessStatus = "Success";
							file.BackupPath = backupPath;
							MoveFileToFolder(file);
						}
																		
					}
					
				}
				else
				{
					file.ProcessStatus = "Error, not processed";
					file.BackupPath = file.ProcessPath + @"\Error";
					MoveFileToFolder(file);
				}

			}
			catch (Exception ex)
			{
				error.ErrorMessage = ex.Message;
				error.ErrorNumber = 1;
			}
			return error;
		}
		#endregion

		public static ErrorInfo MoveFilesToBackup(FilesInfo file, String processPathAndName, Int32 sampleID)
		{
			ErrorInfo error = new ErrorInfo();
			Int32 fileCount = 1;
			String processName = "";		

			try
			{
				Int32 lastPos = processPathAndName.LastIndexOf("\\");
				Int32 stringLen = processPathAndName.Length;
				Int32 len = stringLen - lastPos;
				processName = processPathAndName.Substring(lastPos, len);

				


				file.NewFileName = String.Empty;

				if (Directory.Exists(file.BackupPath) == false)
				{
					Directory.CreateDirectory(file.BackupPath);
				}

				while (fileCount > 0)
				{
					if (File.Exists(file.BackupPathAndName) == false)
					{
						//File.Move(processPathAndName, file.BackupPathAndName);
						File.Move(processPathAndName, file.BackupPathAndName + "\\" + processName);
						
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
								if (file.FileName != null)
								{
									file.NewFileName = fileCount + "_" + file.FileName;
								}

							}
							else
							{
								if (file.FileName != null)
								{
									file.NewFileName = fileCount + "_" + sampleID + "_" + file.FileName;
								}

							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				error.ErrorMessage = ex + ". Unable to move file.";
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
								if (file.FileName != null)
								{
									file.NewFileName = fileCount + "_" + file.FileName;
								}
								
							}
							else
							{
								if (file.FileName != null)
								{
									file.NewFileName = fileCount + "_" + sampleID + "_" + file.FileName;
								}
								
							}
						}
					}
				}
			}

			catch (Exception ex)
			{
				error.ErrorMessage = ex + ". Unable to move file.";
				error.ErrorNumber = 1;
			}
			return error;
		}
	}
}
