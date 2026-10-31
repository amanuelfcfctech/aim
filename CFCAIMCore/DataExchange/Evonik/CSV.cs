using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;
using System.Reflection;

namespace CFCAIMCore.DataExchange
{
	public class EvonikCSV
	{
        // DB240710 CFCUser.Global userGlobal = new CFCUser.Global();

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
									Int32 cfcUserID = 0;
									Int32 dataSourceTypeID = 0;
									Decimal.TryParse(column, out resultValue);
									sampleResult[nutrientColumn].SampleID = sampleNumber;
									sampleResult[nutrientColumn].ResultValue = resultValue;
									sampleResult[nutrientColumn].AnalysisDate = analysisDate;
                                        // DB240710 Int32.TryParse(userGlobal.CFCLoginUser.CFCUserID.ToString(), out cfcUserID);
                                    sampleResult[nutrientColumn].CFCUserID = cfcUserID;
									Int32.TryParse(DataSourceTypeEnum.Evonik.ToString(), out dataSourceTypeID);
									sampleResult[nutrientColumn].DataSourceTypeID = dataSourceTypeID;  

									//8/17/22
									//AIMManager.InsertSampleResult(sampleResult[nutrientColumn], null);
									AIMManager.InsertSampleResult(sampleResult[nutrientColumn], null, 0);

									nutrientColumn += 1;
								}
								catch (Exception e)
								{
									//MessageBox.Show(e + " i m inside the row " + row[0] + " and column: " + column + " col number: " + columnNumber);
									AIMManager.InsertSystemLog((Int32)EnumSystemLogType.Warning, e + " i m inside the row " + row[0] + " and column: " + column + " col number: " + columnNumber, (new StackTrace(e, true)).GetFrame(0).GetFileLineNumber(), MethodInfo.GetCurrentMethod().Name, 0);
								}
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
				return true;
			}
		}
	}
}
