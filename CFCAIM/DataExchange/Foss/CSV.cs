using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;

namespace CFCAIM.DataExchange
{
	public class FossCSV
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];

		public ErrorInfo CSVRowProcess(
						FilesInfo file, 
						FilesCountInfo filesCount, 
						ref SampleInfo sample,
						Int32 aimOwnerID,
						Int32 aimOwnerLocationID,
						Int32 aimsourceID,
						Int32 aimsourceLocationID,
						Boolean isSampleAdded,
						Int32 delimiterTypeID)
		{
			List<SampleResultInfo> sampleResult = new List<SampleResultInfo>();
			DateTime analysisDate = (DateTime)SqlDateTime.MinValue;
			ErrorInfo error = new ErrorInfo();
			Decimal resultValue = 0;
			Int32 sampleNumber = 0;
			Int32 nutrientColumn = 0;
			Int32 columnNumber = 0;
			Int32 csvRow = 1;
						
			using (ReadWriteCSV.CsvFileReader reader = new ReadWriteCSV.CsvFileReader(file.ProcessPathAndName))
			{
				ReadWriteCSV.CsvRow row = new ReadWriteCSV.CsvRow();				
				while (reader.ReadRow(row))										
					{				
					foreach (string column in row)
					{
						if (csvRow == 1)
						{
							if (columnNumber == 0)
							{
								if (row[0] != "Analysis Time" || row[4] != "Sample Number")
								{
									filesCount.WrongFormatCount += 1;
									error.ErrorNumber = 1;
									error.ErrorMessage = "Wrong format";
									return error;
								}
							}
							else if (columnNumber > 7)
							{
								sampleResult.Add(
										new SampleResultInfo
										{
											AnalyzerNutrientCode = column,
											UpdateStamp = DateTime.Now,
											IsActive = true,
										}
										);
							}
						}
						else
						{
							if (columnNumber == 0)
							{
								sample = null;
								DateTime.TryParse(row[0], out analysisDate);
								Int32.TryParse(row[4], out sampleNumber);
								if (isSampleAdded == false)
								{
									sample = AIMManager.GetSample(sampleNumber);
								}
								if (sample == null)
								{
									sample = AIMManager.GetSample(row[4]);
								}

								if (sample == null)
								{
									if (isSampleAdded == false)
									{
										filesCount.SampleMismatchCount += 1;
										error.ErrorNumber = 2;
										error.ErrorMessage = "Missing Sample";
										return error;
									}
									else
									{
										sample = new SampleInfo();
										sample.SampleID = 0;
										sample.SampleCode = row[4];
										sample.DataSourceTypeID = (Int32)DataSourceTypeEnum.Foss;
										sample.AnalyzerMaterialCode = row[1];
										sample.AIMSupplierID = aimsourceID;
										sample.AIMSupplierLocationID = aimsourceLocationID;
										sample.AIMOwnerID = aimOwnerID;
										sample.AIMOwnerLocationID = aimOwnerLocationID;
										sample.SampleDate = analysisDate;
										sample.AnalysisDate  = analysisDate;
										sample.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

										//8/17/23
										sample.SampleTestTypeID = 0;

										sampleNumber = AIMManager.SaveSample(ref sample).RecordID;
										sample.SampleID = sampleNumber;
									}
								}
							}
							else if (columnNumber > 7)
							{
								Decimal.TryParse(column, out resultValue);
								sampleResult[nutrientColumn].SampleID = sampleNumber;
								sampleResult[nutrientColumn].ResultValue = resultValue;
								sampleResult[nutrientColumn].AnalysisDate = analysisDate;
								sampleResult[nutrientColumn].CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								sampleResult[nutrientColumn].DataSourceTypeID = (Int32)DataSourceTypeEnum.Foss;
								sampleResult[nutrientColumn].Unit = "%";
								sampleResult[nutrientColumn].AimDatabaseID = (Int32)_mainMenu.lueDataMap.EditValue;
                                sampleResult[nutrientColumn].SqlResultID = String.Empty;

								//8/17/22
                                //AIMManager.InsertSampleResult(sampleResult[nutrientColumn], null);
								AIMManager.InsertSampleResult(sampleResult[nutrientColumn], null, 0);

								nutrientColumn += 1;
							}
						}
						columnNumber += 1;
					}
					columnNumber = 0;
					nutrientColumn = 0;
					csvRow += 1;
				}
				filesCount.SuccessCount += 1;
				error.ErrorNumber = 0;
				error.RecordID = sampleNumber;
				return error;
			}
		}
	}
}
