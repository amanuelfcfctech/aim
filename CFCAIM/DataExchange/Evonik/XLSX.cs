using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace CFCAIM.DataExchange
{	
	public class EvonikXLSX
	{
		CFCUser.Global userGlobal = new CFCUser.Global();

		public static bool CheckDecimal(String val)
		{
			try
			{
				decimal valDecimal = 0;
				Decimal.TryParse(val, out valDecimal);
				return true;
			}
			catch
			{
				return false;
			}
		}		

		public Boolean ExcelLandscape_Detail(
									FilesInfo file,
									ref FilesCountInfo filesCount,
									SampleInfo sample,
									Int32 dataSourceTypeID,
									Int32 sheetNumber)
		{
			/*
			DateTime analysisDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
					
			Decimal resultValue = 0;			
			Int32 beginRow = 1;
			String sampleCode = "";
			Int32 sampleID = 0;										
			Decimal checkResultValue = 0;
			Boolean addDetail = true;
			String analyzerNutrientCode = "";
					

			//OPEN EXCEL SPREADSHEET
			Excel.Application ExcelApp = new Excel.Application();
			Excel.Workbook xlWorkbook = ExcelApp.Workbooks.Open(file.ProcessPathAndName);
			Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[sheetNumber];
			Excel.Range xlRange = xlWorksheet.UsedRange;
			ExcelApp.ErrorCheckingOptions.NumberAsText = false;
			
			try
			{
				//-------------------------------------------------------------------
				//GET COLUMN and ROW COUNTS
				Int32 rowCount = xlWorksheet.Rows.CurrentRegion.EntireRow.Count;
				Int32 colCount = xlRange.Columns.Count;

				//----------------------------------------------------------------------			
				//LOOP THROUGH EXCEL ROWS 
				for (int i = beginRow + 1; i <= rowCount; i++)
				{

					//----------------------------------------------------------------------
					//LOOP THROUGH EXCEL COLUMNS			
					for (int j = 1; j <= colCount; j++)
					{
						try
						{
							//sample-id field (Evonik calls it Lab-Code)
							if (j == 5)
							{
								if (xlRange.Cells[i, j].Value2 == null)
								{
									sampleCode = "0";
									sampleID = 0;

									ErrorInfo error = new ErrorInfo();
									filesCount.SampleMismatchCount += 1;
									error.ErrorNumber = 2;
									error.ErrorMessage = "Missing Sample";
								}
								else
								{
									sampleCode = xlRange.Cells[i, j].Value2.ToString();

									//check to make sure sampleCode exists
									//if sampleCode doesn't exist, skip detail
									//get SampleID for insert below; if there are duplicates, get most recent sampleID							
									ErrorInfo error = new ErrorInfo();
									error = AIMManager.CountSampleCode(sampleCode);
									sampleID = error.ReturnIntegerValue;
									if (sampleID == 0)
									{										
										filesCount.SampleMismatchCount += 1;
										error.ErrorNumber = 2;
										error.ErrorMessage = "Missing Sample#";
									}
								}
							}

							//detail fields
							if (j > 10 && sampleID != 0)
							{

								addDetail = true;
								analyzerNutrientCode = xlRange.Cells[1, j].Value2.ToString();

								if (xlRange.Cells[i, j].Value2 == null)
								{
									addDetail = false;
								}
								else
								{
									Decimal.TryParse(xlRange.Cells[i, j].Value2.ToString(), out resultValue);
									if (Decimal.TryParse(xlRange.Cells[i, j].Value2.ToString(), out checkResultValue))
									{
									}
									else
									{
										addDetail = false;
									}
								}

								if (addDetail == true)
								{
									//Add Detail record										
									SampleResultInfo sampleResult = new SampleResultInfo();
									sampleResult.SampleID = sampleID;
									sampleResult.AnalyzerNutrientCode = analyzerNutrientCode;
									sampleResult.ResultValue = resultValue;
									sampleResult.Unit = "%";
									sampleResult.UnitType = "%";
									sampleResult.DataSourceTypeID = dataSourceTypeID;
									AIMManager.InsertSampleResult(sampleResult);
								}
								else
								{
									filesCount.WrongFormatCount += 1;
								}
							}
						}
						catch (Exception ex)
						{
							ExcelApp.Quit();
							MessageBox.Show(ex.Message);
						}
					}
					filesCount.SuccessCount += 1;
				}
			}

			//xlWorkbook.Close(true);
			//ExcelApp.Quit();
			//System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
			finally
			{
				xlWorkbook.Close(true);
				ExcelApp.Quit();
				ExcelApp = null;
			}	

			 */
			return true;
		}		
	}

	
}
