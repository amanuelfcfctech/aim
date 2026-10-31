using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;

namespace CFCAIM.DataExchange
{
	public class PertenCSV
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
					/*
					0 "Sample ID"
					1 "Product Name"
					2 "Product Type Name"
					3 "Data/Time of Analysis"
					4 "Parameter Name"
					5 "Moisture Basis"
					6 "ReportedResult"
					7 "Unit"			

					*/
					
					foreach (string column in row)
					{
						if (csvRow == 1)
						{							
							if (row[0] != "Sample ID" ||
								row[1] != "Product Name" ||
								row[2] != "Product Type Name" ||
								row[3] != "Data/Time of Analysis" ||
								row[4] != "Parameter Name" ||
								row[5] != "Moisture Basis" ||
								row[6] != "ReportedResult" ||
								row[7] != "Unit")
							{
								filesCount.WrongFormatCount += 1;
								error.ErrorNumber = 1;
								error.ErrorMessage = "Wrong format";
								return error;
							}									
						}
						else
						{										
							sample = null;
							Int32.TryParse(row[0], out sampleNumber);
							DateTime.TryParse(row[3], out analysisDate);
								
							if (isSampleAdded == false)
							{
								sample = AIMManager.GetSample(sampleNumber);
							}
							if (sample == null)
							{
								sample = AIMManager.GetSample(row[0]);
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
									sample.SampleCode = row[0];
									sample.DataSourceTypeID = (Int32)DataSourceTypeEnum.Perten;

									//TODO?? analyzer material code??
									sample.AnalyzerMaterialCode = row[1];

									sample.AIMSupplierID = aimsourceID;
									sample.AIMSupplierLocationID = aimsourceLocationID;
									sample.AIMOwnerID = aimOwnerID;
									sample.AIMOwnerLocationID = aimOwnerLocationID;

									sample.SampleDate = analysisDate;
									sample.AnalysisDate = analysisDate;
									sample.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

									//8/17/23
									sample.SampleTestTypeID = 0;

									sampleNumber = AIMManager.SaveSample(ref sample).RecordID;
									sample.SampleID = sampleNumber;
								}
							}
							
							
							sampleResult[nutrientColumn].SampleID = sampleNumber;
							Decimal.TryParse(row[6], out resultValue);
							sampleResult[nutrientColumn].ResultValue = resultValue;							
							sampleResult[nutrientColumn].AnalysisDate = analysisDate;
							sampleResult[nutrientColumn].CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							sampleResult[nutrientColumn].DataSourceTypeID = (Int32)DataSourceTypeEnum.Perten;
							sampleResult[nutrientColumn].Unit = row[7];
							sampleResult[nutrientColumn].AimDatabaseID = (Int32)_mainMenu.lueDataMap.EditValue;
                            sampleResult[nutrientColumn].SqlResultID = String.Empty;

							//8/17/22
                            //AIMManager.InsertSampleResult(sampleResult[nutrientColumn], row[4]);
							AIMManager.InsertSampleResult(sampleResult[nutrientColumn], row[4], 0);

							nutrientColumn += 1;
							
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
