using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;

namespace CFCAIM.DataExchange
{
	public class EvonikCSV
	{
		CFCUser.Global userGlobal = new CFCUser.Global();

		public Boolean CSVRowProcess(
									FilesInfo file,
									ref FilesCountInfo filesCount, 
									SampleInfo sample)
		{
			DateTime analysisDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
			List<SampleResultInfo> sampleResult = new List<SampleResultInfo>();
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
								if (row[3] != "RELEASE_DATE" || row[5] != "CUSTOMER_DESCRIPTION")
								{
									filesCount.WrongFormatCount += 1;
									return false;
								}
							}
							else if (columnNumber > 9)
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
								DateTime.TryParse(row[3], out analysisDate);
								String sampleString = row[5].Split(' ')[0];
								Int32.TryParse(sampleString, out sampleNumber);
								//if (cbCreateSample.Checked == false)
								//{
								sample = AIMManager.GetSample(sampleNumber);
								//}

								if (sample == null)
								{
									//if (cbCreateSample.Checked == false)
									//{
									filesCount.SampleMismatchCount += 1;
									return false;
									//}
									//else
									//{
									//}
								}
							}
							else if (columnNumber > 9)
							{
								try
								{
									Decimal.TryParse(column, out resultValue);
									sampleResult[nutrientColumn].SampleID = sampleNumber;
									sampleResult[nutrientColumn].ResultValue = resultValue;
									sampleResult[nutrientColumn].AnalysisDate = analysisDate;
									sampleResult[nutrientColumn].CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
									sampleResult[nutrientColumn].DataSourceTypeID = (Int32)DataSourceTypeEnum.Evonik;

									//9/9/20 - change to use AIMCoreGlobal
									//sampleResult[nutrientColumn].AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
									sampleResult[nutrientColumn].AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                                    sampleResult[nutrientColumn].SqlResultID = String.Empty;

									//8/17/22
									//AIMManager.InsertSampleResult(sampleResult[nutrientColumn], null);
									AIMManager.InsertSampleResult(sampleResult[nutrientColumn], null, 0);

									nutrientColumn += 1;
								}
								catch (Exception e)
								{
									MessageBox.Show(e + " i m inside the row " + row[0] + " and column: " + column + " col number: " + columnNumber);
								}
							}
						}
						columnNumber += 1;
					}
					columnNumber = 0;
					nutrientColumn = 0;
					csvRow += 1;
				}
				filesCount.SuccessCount += 1;
				return true;
			}
		}
	}
}
