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
	public static class StringExtensions
	{
		public static string GetUnitsFromString(String str, Int32 length)
		{
			String checkChar = "";
			String newString = "";
			Int32 pos = 0;
			Int32 paren1 = 0;
			Int32 paren2 = 0;
			if (str.Substring(length - 1, 1) != ")")
			{
				return "";
			}
			else
			{
				//loop thru str until "(" is found.  If it's not found, return ""
				for (int i = 1; i < length; i++)
				{
					pos++;
					checkChar = str.Substring(length - i, 1);
					if (checkChar == "(" && paren1 == 0)
					{
						paren1 = pos;
					}
					else if (checkChar == "(" && paren1 != 0)
					{
						paren2 = pos;
					}
				}
				if (paren2 != 0)
				{
					newString = "";
					for (int j = 1; j < paren1 - j; j++)
					{
						newString += str.Substring(length - paren1 + 1, 1);
					}
				}

				return newString;
			}
		}

		public static string GetNutrientFromString(String str, Int32 length)
		{
			String checkChar = "";
			String newString = "";
			Int32 pos = 0;
			Int32 paren1 = 0;
		
			if (str.Substring(length - 1, 1) != ")")
			{
				return "";
			}
			else
			{
				//loop thru str, looking for "(".  If it's found, return only the chars up to the "("; otherwise, return the whole string.
				for (int i = 1; i < length && paren1 == 0; i++)
				{
					pos++;
					checkChar = str.Substring(i, 1);
					if (checkChar == "(" && paren1 == 0)
					{
						paren1 = pos;
					}
					
				}
				if (paren1 != 0)
				{									
					newString = str.Substring(0, paren1 - 1);					
				}
				else
				{
					newString = str;
				}

				return newString;
			}
		}

		
	}	

	public class ABVistaXLSX
	{
		public Boolean ExcelPortrait_Detail(
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

			//detail fields			
			String str = "";			
			Int32 length = 0;
			String analyzerNutrientCode = "";
			String unitType = "";
			
			SampleInfo sampleInfo = new SampleInfo();

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
					//column 1 - sample-id field
					//column 7 - nutrient and units
					//column 8 - result value

					if (xlRange.Cells[i, 1].Value2 == null)
					{
						sampleCode = "0";
						sampleID = 0;

						ErrorInfo error = new ErrorInfo();
						filesCount.SampleMismatchCount += 1;
						error.ErrorNumber = 2;
						error.ErrorMessage = "Missing Sample";
						addDetail = false;
					}
					else
					{
						sampleCode = xlRange.Cells[i, 1].Value2.ToString();
						addDetail = true;

						//check to make sure sampleCode exists. If sampleCode doesn't exist on header, skip detail
						//get SampleID for insert below; if there are duplicates, get most recent sampleID							
						ErrorInfo error = new ErrorInfo();
						error = AIMManager.CountSampleCode(sampleCode);
						sampleID = error.ReturnIntegerValue;
						if (sampleID == 0)
						{
							filesCount.SampleMismatchCount += 1;
							error.ErrorNumber = 2;
							error.ErrorMessage = "Missing Sample#";
							addDetail = false;
						}
						
						//parse out units from column 7 (includes both nutrient and units)
						if (xlRange.Cells[i, 7].Value2 == null)
						{
							addDetail = false;
						}
						else
						{
							str = xlRange.Cells[i, 7].Value2.ToString();
							length = str.Length;
						}

						//units
						unitType = StringExtensions.GetUnitsFromString(str, length);

						//nutrient
						analyzerNutrientCode = StringExtensions.GetNutrientFromString(str, length);

						//result value
						Decimal.TryParse(xlRange.Cells[i, 8].Value2.ToString(), out resultValue);
						if (Decimal.TryParse(xlRange.Cells[i, 8].Value2.ToString(), out checkResultValue))
						{}
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
						sampleResult.ResultValue = checkResultValue;
						sampleResult.Unit = unitType;
						sampleResult.UnitType = unitType;
						sampleResult.DataSourceTypeID = dataSourceTypeID;
						AIMManager.InsertSampleResult(sampleResult);
						filesCount.SuccessCount++;
					}
					else
					{
						filesCount.WrongFormatCount += 1;
					}
					filesCount.TotalFilesCount += 1;
				}
			}

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
