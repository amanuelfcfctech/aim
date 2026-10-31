using System;
using System.Collections.Generic;
using System.Diagnostics;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;
using System.Data.SqlTypes;
using System.Reflection;

namespace CFCAIMCore.DataExchange
{
	public class AdisseoCSV
	{
		// DB 240710 Working on converting the project         CFCUser.Global _UserGlobal = new CFCUser.Global();		

		public Boolean CSVRowProcess(
						FilesInfo file,
						FilesCountInfo filesCount,
						ref SampleInfo sample,
						Int32 aimOwnerID,
						Int32 aimOwnerLocationID,
						Int32 aimsupplierID,
						Int32 aimsupplierLocationID,
						Boolean isSampleAdded)
		{
			DateTime analysisDate = (DateTime)SqlDateTime.MinValue;
			List<SampleResultInfo> sampleResult = new List<SampleResultInfo>();
			Decimal resultValue = 0;
			Int32 sampleNumber = 0;
			Int32 nutrientColumn = 0;
			Int32 columnNumber = 1;
			Int32 csvRow = 1;
			Int32 dataSourceTypeID = 0;

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
							if (columnNumber == 1)
							{
								//checking file format
								if (row[0] != "sample code" || row[2] != "material" || row[3] != "last result date")
								{
									filesCount.WrongFormatCount += 1;
									return false;
								}
							}
							else if (columnNumber > 12)
							{
								//making list of SampleResults (which can be Nutrients actually)
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
							if (columnNumber == 1)
							{
								DateTime.TryParse(row[3], out analysisDate);
								Int32.TryParse(row[0], out sampleNumber);
								sample = AIMManager.GetSample(sampleNumber);
								if (sample == null)
								{
									sample = AIMManager.GetSample(row[0]);
									if (sample != null)
									{
										sampleNumber = sample.SampleID;
									}
									else
									{
										if (isSampleAdded)
										{
											sample = new SampleInfo();
											sample.SampleID = 0;
											sample.SampleCode = row[0];
											sample.AnalyzerMaterialCode = row[2];
											sample.AIMSupplierID = aimsupplierID;
											sample.AIMOwnerID = aimOwnerID;
											sample.AIMSupplierLocationID = aimsupplierLocationID;
											sample.AIMOwnerLocationID = aimOwnerLocationID;
											sample.SampleDate = DateTime.Now;
											// DB240710 sample.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;
											sampleNumber = AIMManager.SaveSample(ref sample).RecordID;
											sample.SampleID = sampleNumber;
										}
										else
										{
											filesCount.SampleMismatchCount += 1;
											return false;
										}
									}
								}
							}
							else if (columnNumber > 12)
							{
								try
								{
									Decimal.TryParse(column, out resultValue);
									sampleResult[nutrientColumn].SampleID = sampleNumber;
									sampleResult[nutrientColumn].ResultValue = resultValue;
									sampleResult[nutrientColumn].AnalysisDate = analysisDate;
                                        // DB240710 sampleResult[nutrientColumn].CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;
                                    Int32.TryParse(DataSourceTypeEnum.Adisseo.ToString(), out dataSourceTypeID);
									sampleResult[nutrientColumn].DataSourceTypeID = dataSourceTypeID;
									
									//8/17/22
									//AIMManager.InsertSampleResult(sampleResult[nutrientColumn], null);
									AIMManager.InsertSampleResult(sampleResult[nutrientColumn], null, 0);

									nutrientColumn += 1;
								}
								catch (Exception e)
								{
									//MessageBox.Show(e + " i m inside the row " + row[0] + " and column: " + column + " col number: " + columnNumber);
									AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Warning, e + " i m inside the row " + row[0] + " and column: " + column + " col number: " + columnNumber, (new StackTrace(e,true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
								}
							}
						}
						columnNumber += 1;
					}
					columnNumber = 1;
					nutrientColumn = 0;
					csvRow += 1;
				}

				}
				catch (Exception ex)
				{
					AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Fatal, ex.Message, (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
				}
				filesCount.SuccessCount += 1;
				return true;
			}
		}
	}
}
