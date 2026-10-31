using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;
using System.Data.SqlTypes;

namespace CFCAIM.DataExchange
{
	public class PertenDatabase
	{
		CFCUser.Global _UserGlobal = new CFCUser.Global();

		public Boolean CSVRowProcess(
						FilesInfo file,
						FilesCountInfo filesCount,
						ref SampleInfo sample,
						Int32 aimOwnerID,
						Int32 aimOwnerLocationID,
						Int32 aimSourceID,
						Int32 aimsourceLocationID,
						Boolean isSampleAdded,
						Int32 delimiterTypeID)
		{
			DateTime analysisDate = (DateTime)SqlDateTime.MinValue;
			List<SampleResultInfo> sampleResult = new List<SampleResultInfo>();
			Decimal resultValue = 0;
			Int32 sampleNumber = 0;
			Int32 nutrientColumn = 0;
			Int32 columnNumber = 1;
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
											sample.DataSourceTypeID = (Int32)DataSourceTypeEnum.Perten;
											sample.AnalyzerMaterialCode = row[2];
											sample.AIMSupplierID = aimSourceID;
											sample.AIMOwnerID = aimOwnerID;
											sample.AIMSupplierLocationID = aimsourceLocationID;
											sample.AIMOwnerLocationID = aimOwnerLocationID;
											sample.SampleDate = DateTime.Now;
											sample.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;

											//8/17/23
											sample.SampleTestTypeID = 0;

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
									sampleResult[nutrientColumn].CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;
									sampleResult[nutrientColumn].DataSourceTypeID = (Int32)DataSourceTypeEnum.Adisseo;
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
					columnNumber = 1;
					nutrientColumn = 0;
					csvRow += 1;
				}
				filesCount.SuccessCount += 1;
				return true;
			}
		}
	}
}
