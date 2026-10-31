using System;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;

namespace CFCAIM.DataExchange
{
	public class LabAdvantageCSV
	{
		CFCUser.Global userGlobal = new CFCUser.Global();

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
			SampleResultInfo sampleResult = new SampleResultInfo();			
			DateTime analysisDate = (DateTime)SqlDateTime.MinValue;
			ErrorInfo error = new ErrorInfo();
			Decimal resultValue = 0;
			Int32 sampleNumber = 0;
			Int32 nutrientColumn = 0;
			Int32 columnNumber = 0;
			Int32 csvRow = 1;
			String nutrientCode = "";
			Decimal value = 0;
			String units = "";
			

			using (ReadWriteCSV.CsvFileReader reader = new ReadWriteCSV.CsvFileReader(file.ProcessPathAndName))
			{
				ReadWriteCSV.CsvRow row = new ReadWriteCSV.CsvRow();

				String delimiterType = ",";
				if (delimiterTypeID == (Int32)DelimiterTypeEnum.Semicolon)
				{
					delimiterType = ";" ;				
				}
				
				//while (reader.ReadRow(row, ';'))
				while (reader.ReadRow(row,Convert.ToChar(delimiterType)))

				{
					foreach (string column in row)
					{						

						if (columnNumber > 6)
						{
							if (nutrientColumn == 0)
							{
								nutrientCode = column;
							}
							else if (nutrientColumn == 1)
							{
								Decimal.TryParse(column, out resultValue);
								value = resultValue;
							}
							else if (nutrientColumn == 2)
							{
								units = column;
							}
							if (nutrientColumn == 2)
							{
								sampleResult.SampleID = sampleNumber;
								sampleResult.AnalyzerNutrientCode = nutrientCode;
								sampleResult.ResultValue = resultValue;
								sampleResult.Unit = units;
								sampleResult.UpdateStamp = DateTime.Now;
								sampleResult.IsActive = true;
								sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.LabAdvantage;

								//9/9/20 - change to use AIMCoreGlobal
								//sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
								sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;
								                                
                                sampleResult.SqlResultID = String.Empty;

								//8/17/22
                                //AIMManager.InsertSampleResult(sampleResult, null);
								AIMManager.InsertSampleResult(sampleResult, null, 0);

								nutrientColumn = 0;
								nutrientCode = "";
								resultValue = 0;
								units = "";
							}
							else
							{
								nutrientColumn += 1;
							}
						}

						else
						{
							if (columnNumber == 0)
							{
								sample = null;																							
								sample = AIMManager.GetSampleAlternateCode((row[0].Substring(0, 23)) );

								if (sample == null)
								{
									if (isSampleAdded == true)
									{
										filesCount.SampleMismatchCount += 1;
										error.ErrorNumber = 2;
										error.ErrorMessage = "Missing Sample";
										return error;
									}
									else
									{
										String analyzerMaterialCode = "Corn";
										String sampleCode = "";
										Int32 aimMaterialID = 1;
										aimsourceID = 1;
										aimsourceLocationID = 12;
										aimOwnerID = 1;
										aimOwnerLocationID = 1;
										String date = row[0].Substring(3, 2) + "/" + row[0].Substring(5, 2) + "/" + row[0].Substring(7, 2);
										analysisDate = AIMGlobal.ConvertStringToDate(date);															

										sample = new SampleInfo();											
										sample.SampleID = 0;
										sample.SampleCode = sampleCode;
										sample.AIMMaterialID = aimMaterialID;
										sample.DataSourceTypeID = (Int32)DataSourceTypeEnum.LabAdvantage;											
										sample.AlternateCode = row[0].Substring(0, 23);
										sample.AnalyzerMaterialCode = analyzerMaterialCode;
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
										sampleCode = Convert.ToString(sampleNumber);
										AIMManager.UpdateSampleCode(sampleNumber, sampleCode);
									}
								}
								else
								{
									sampleNumber = sample.SampleID;
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
				error.ErrorNumber = 0;
				error.RecordID = sampleNumber;
				return error;
			}
		}
	}
}
