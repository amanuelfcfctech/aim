using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Reflection;

namespace CFCAIMCore.DataExchange
{
	public class FossCSV
	{
        // DB240710 CFCUser.Global userGlobal = new CFCUser.Global();

        public ErrorInfo CSVRowProcess(
						FilesInfo file, 
						FilesCountInfo filesCount, 
						ref SampleInfo sample,
						Int32 aimOwnerID,
						Int32 aimOwnerLocationID,
						Int32 aimsupplierID,
						Int32 aimsupplierLocationID,
						Boolean isSampleAdded)
		{
			List<SampleResultInfo> sampleResult = new List<SampleResultInfo>();
			DateTime analysisDate = (DateTime)SqlDateTime.MinValue;
			ErrorInfo error = new ErrorInfo();
			Decimal resultValue = 0;
			Int32 sampleNumber = 0;
			Int32 nutrientColumn = 0;
			Int32 dataSourceTypeID = 0;
			Int32 columnNumber = 0;
			Int32 csvRow = 1;
						
			using (ReadWriteCSV.CsvFileReader reader = new ReadWriteCSV.CsvFileReader(file.ProcessPathAndName))
			{
				try
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
									
									//error.ErrorMessage = "Wrong format";
									AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Warning, "Wrong format, CSVRowProcess", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);

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
										
										//error.ErrorMessage = "Missing Sample";
										AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Warning, "Missing Sample, CSVRowProcess", (new StackTrace(true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);

										return error;
									}
									else
									{
										sample = new SampleInfo();
										sample.SampleID = 0;
										sample.SampleCode = row[4];
										sample.AnalyzerMaterialCode = row[1];
										sample.AIMSupplierID = aimsupplierID;
										sample.AIMSupplierLocationID = aimsupplierLocationID;
										sample.AIMOwnerID = aimOwnerID;
										sample.AIMOwnerLocationID = aimOwnerLocationID;
										sample.SampleDate = analysisDate;
										sample.AnalysisDate  = analysisDate;
                                            // DB240710 sample.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
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
                                    // DB240710 	sampleResult[nutrientColumn].CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                                    dataSourceTypeID = (Int32)DataSourceTypeEnum.Foss;
								sampleResult[nutrientColumn].DataSourceTypeID = dataSourceTypeID;
								sampleResult[nutrientColumn].Unit = "%";

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

				}
				catch (Exception ex)
				{
					AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Fatal, ex.Message, (new StackTrace(ex,true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
				}
				filesCount.SuccessCount += 1;
				error.ErrorNumber = 0;
				error.RecordID = sampleNumber;
				return error;
			}
		}
	}
}
