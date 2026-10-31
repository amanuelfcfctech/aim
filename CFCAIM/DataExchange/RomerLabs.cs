using System;
using System.Data.SqlTypes;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;

namespace CFCAIM.DataExchange
{
	//8/12/22 - left RomerLabsCSV code here, but it was decided to move the Romer import to User-Defined (so, currently, this is not used.)
	public class RomerLabsCSV
	{
		CFCUser.Global userGlobal = new CFCUser.Global();

		public ErrorInfo CSVRowProcess(
						FilesInfo file,
						FilesCountInfo filesCount,
						ref SampleInfo sample,
						Int32 headerRow,
						Int32 detailRow,
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


			String material = "";			
			String nutrient = "";
			Decimal resultValue = 0;
			
			String strip = "";
			String serialNumber = "";

			Int32 sampleNumber = 0;
			String sampleDate;
			Int32 rowNumber = 0;
			Int32 columnNumber = 0;
			Int32 csvRow = 1;			
		
			String units = "";


			using (ReadWriteCSV.CsvFileReader reader = new ReadWriteCSV.CsvFileReader(file.ProcessPathAndName))
			{
				ReadWriteCSV.CsvRow row = new ReadWriteCSV.CsvRow();

				String delimiterType = ",";
				if (delimiterTypeID == (Int32)DelimiterTypeEnum.Semicolon)
				{
					delimiterType = ";";
				}

				//while (reader.ReadRow(row, ';'))
				while (reader.ReadRow(row, Convert.ToChar(delimiterType)))
				{
					rowNumber++;
					if (rowNumber > headerRow)
					{
						foreach (string column in row)			
						{					
						
							if (columnNumber == 0)
							{
								Int32.TryParse(column, out sampleNumber);								
							}
							else if (columnNumber == 2)
							{
								sampleDate = column;
							}
							else if (columnNumber == 3)
							{
								nutrient = column;
							}
							else if (columnNumber == 5)
							{
								strip = column;
							}
							else if (columnNumber == 6)
							{
								material = column;
							}
							else if (columnNumber == 10)
							{
								Decimal.TryParse(column, out resultValue);															
							}
							else if (columnNumber == 14)
							{
								serialNumber = column;
							}

							else if (columnNumber > 14)
							{
								////////add sample header								
								//////String analyzerMaterialCode = material;
								//////String sampleCode = "";
								//////Int32 aimMaterialID = 1;
								
								//////String date = row[0].Substring(3, 2) + "/" + row[0].Substring(5, 2) + "/" + row[0].Substring(7, 2);
								//////analysisDate = AIMGlobal.ConvertStringToDate(date);

								//////sample = new SampleInfo();
								sample.SampleID = 0;
								//////sample.SampleCode = sampleCode;
								//////sample.AIMMaterialID = aimMaterialID;
								//////sample.DataSourceTypeID = (Int32)DataSourceTypeEnum.Romer;
								//////sample.AlternateCode = row[0].Substring(0, 23);
								//////sample.AnalyzerMaterialCode = analyzerMaterialCode;
								//////sample.AIMSupplierID = aimsourceID;
								//////sample.AIMSupplierLocationID = aimsourceLocationID;
								//////sample.AIMOwnerID = aimOwnerID;
								//////sample.AIMOwnerLocationID = aimOwnerLocationID;
								//////sample.SampleDate = analysisDate;
								//////sample.AnalysisDate = analysisDate;
								//////sample.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								//////sampleNumber = AIMManager.SaveSample(ref sample).RecordID;

								////////sample.SampleID = sampleNumber;
								////////sampleCode = Convert.ToString(sampleNumber);
								////////AIMManager.UpdateSampleCode(sampleNumber, sampleCode);


								////////add sample result

								//////sampleResult.SampleID = sampleNumber;
								//////sampleResult.AnalyzerNutrientCode = nutrient;
								//////sampleResult.ResultValue = resultValue;
								////////sampleResult.Unit = units;
								//////sampleResult.UpdateStamp = DateTime.Now;
								//////sampleResult.IsActive = true;
								//////sampleResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								//////sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.LabAdvantage;

								//////sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;
								//////sampleResult.SqlResultID = String.Empty;

								//////AIMManager.InsertSampleResult(sampleResult, null);
																
								nutrient = "";
								resultValue = 0;
								units = "";
								
							}							
							columnNumber += 1;
						}
						////columnNumber = 0;						
						csvRow += 1;
					}
					
				}
				filesCount.SuccessCount += 1;
				error.ErrorNumber = 0;
				error.RecordID = sampleNumber;
				return error;
			}
		}
	}
}
