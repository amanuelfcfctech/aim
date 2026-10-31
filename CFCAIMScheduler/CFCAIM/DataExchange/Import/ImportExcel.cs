using System;
using System.IO;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.ComponentModel;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;

namespace CFCAIM.DataExchange.Import
{
    public class ImportExcel
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
		public static string GetUnitsFromString(String str, Int32 length)
			{
				String checkChar = "";
				String newString = "";
				Int32 pos = 0;
				Int32 parenBeg = 0;
				Int32 parenEnd = 0;				
				Int32 unitLen = 0;

				if (str.Substring(length - 1, 1) != ")")
				{
					return "";
				}
				else
				{	
					//loop thru string, starting at the end, until "(" is found. If it's not found, return ""				
					pos = length - 1;					
					for (int i = 1; i < length; i++)
					{
						pos--;
						checkChar = str.Substring(length - i, 1);					

						if (checkChar == ")" && parenEnd == 0)
						{
							parenEnd = pos;
						}
						if (checkChar == "(" && parenBeg == 0)
						{
							parenBeg = pos;
						}					   
					}	   

					if (parenBeg != 0)
					{												
						newString = "";
						unitLen = parenEnd - parenBeg - 1;
						newString = str.Substring(parenBeg + 2, unitLen);					  
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
					//loop thru string, looking for "(".  If it's found, return only the chars up to the "("; otherwise, return the whole string.
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
	
		public static Boolean Landscape(
									FilesInfo file,
									ref FilesCountInfo filesCount,
									SampleInfo sample,
									Int32 dataSourceTypeID,
									Int32 sheetNumber,
									Int32 sampleCodeColumn,
									Int32 detailStartsInColumn,
									String excelFileExtension,

									BackgroundWorker backgroundWorker)
		{
			DateTime analysisDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
			Decimal resultValue = 0;
			Int32 beginRow = 0;
			String sampleCode = "";
			Int32 sampleID = 0;
			Decimal checkResultValue = 0;
			Boolean addDetail = true;
			String analyzerNutrientCode = "";

			using (SpreadsheetControl spreadsheetControl = new SpreadsheetControl())
			{				
				try
				{

					//see if file is open -- should be closed; otherwise import file won't move to Archive
					if (IsFileOpen(file.ProcessPathAndName) == true)
					{
						AIMGlobal.SplashScreenHide();
						MessageBox.Show("Excel file must be closed before importing.  Please close the file and try again.");
						return false;
					}                  

                    //added this to keep from getting DragDrop registration did not succeed error
                    spreadsheetControl.AllowDrop = false;

                    //Load a workbook from a file. 

                    //5/26/20
                    //if (excelFileExtension == ".xls")
                    if (excelFileExtension.ToUpper() == ".XLS")

                    {
                        spreadsheetControl.LoadDocument(file.ProcessPathAndName, DocumentFormat.Xls);
                    }
                    else
                    {
                        spreadsheetControl.LoadDocument(file.ProcessPathAndName, DocumentFormat.Xlsx);
                    }

                    //if the data is on the excel sheet 1, the code sees that as sheet 0						

                    //8/5/20
                    //Range range;
                    CellRange range;

                    Worksheet worksheet = spreadsheetControl.Document.Worksheets[sheetNumber - 1];
                    range = worksheet.GetDataRange();

                    //-------------------------------------------------------------------
                    //GET COLUMN and ROW COUNTS
                    Int32 rowCount = range.RowCount;
                    Int32 colCount = range.ColumnCount;

                    //----------------------------------------------------------------------			
                    //LOOP THROUGH EXCEL ROWS 
                    for (int i = beginRow + 1; i < rowCount; i++)
					{
						CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);

						//----------------------------------------------------------------------
						//LOOP THROUGH EXCEL COLUMNS			
						for (int j = 1; j < colCount; j++)
						{
							try
							{
								//sample-id field		(Evonik calls it Lab-Code)
								//						(Specman calls it Source)								
								if (j == sampleCodeColumn)
								{
                                    //check row for sampleCode value
									if (worksheet[i, j].Value == null) 
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
										sampleCode = worksheet[i, j].Value.ToString();

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
								if (j >= detailStartsInColumn && sampleID != 0)
								{
									addDetail = true;

                                    //row is hard-coded to 0 below - to get nutrient-code from heading
									analyzerNutrientCode = worksheet[0, j].Value.ToString();

									if (worksheet[i, j].Value == null)
									{
										addDetail = false;
									}
									else
									{
										Decimal.TryParse(worksheet[i, j].Value.ToString(), out resultValue);
										if (Decimal.TryParse(worksheet[i, j].Value.ToString(), out checkResultValue))
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

										//9/9/20 - change to use AIMCoreGlobal
										//sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
										sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                                        sampleResult.SqlResultID = String.Empty;

										//8/17/22
                                        //AIMManager.InsertSampleResult(sampleResult, null);
										AIMManager.InsertSampleResult(sampleResult, null, 0);

									}
									else
									{
										filesCount.WrongFormatCount += 1;
									}
								}
							}
							catch (Exception ex)
							{
                                spreadsheetControl.Dispose();
                                MessageBox.Show(ex.Message);
							}
						}
						filesCount.SuccessCount += 1;

						//----------------------------------------------------------
						//SEE IF USER CANCELLED PROCESS
						if (AIMGlobal.stopPreviewingImport == true)
						{
							AIMGlobal.stopPreviewingImport = false;
							break;
						}
					}
				}

				finally
				{
                    spreadsheetControl.Dispose();
                }

			}
			return true;
		}

		public static Boolean Portrait(
									FilesInfo file,
									ref FilesCountInfo filesCount,
									SampleInfo sample,
									Int32 dataSourceTypeID,
									Int32 sheetNumber,
									String excelFileExtension,
									BackgroundWorker backgroundWorker)
		{
			DateTime analysisDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
			Decimal resultValue = 0;
			Int32 beginRow = 1;
			String sampleCode = "";
			Int32 sampleID = 0;
			Decimal checkResultValue = 0;
			Boolean addDetail = true;
			Boolean headingsAreCorrent = false;

			//detail fields			
			String str = "";
			Int32 length = 0;
			String analyzerNutrientCode = "";
			String unitType = "";

			SampleInfo sampleInfo = new SampleInfo();			  

			using (SpreadsheetControl spreadsheetControl = new SpreadsheetControl())
			{
				try
				{
					//see if file is open -- should be closed; otherwise import file won't move to Archive
					if (IsFileOpen(file.ProcessPathAndName) == true)
					{
						AIMGlobal.SplashScreenHide();
						MessageBox.Show("Excel file must be closed before importing.  Please close the file and try again.");
						return false;
					}
                    	
                    //8/5/20				
					//Range range;
                    CellRange range;

                    Int32 lastColumn = 0;
					Int32 lastRow = 0;

					//added this to keep from getting DragDrop registration did not succeed error
					spreadsheetControl.AllowDrop = false;

                    //Load a workbook from a file. 

                    //5/26/20
                    //if (excelFileExtension == ".xls")
                    if (excelFileExtension.ToUpper() == ".XLS")

                    {
                        spreadsheetControl.LoadDocument(file.ProcessPathAndName, DocumentFormat.Xls);
                    }
                    else
                    {
                        spreadsheetControl.LoadDocument(file.ProcessPathAndName, DocumentFormat.Xlsx);
                    }

                    //if the data is on the excel sheet 1, the code sees that as sheet 0						
                    Worksheet worksheet = spreadsheetControl.Document.Worksheets[sheetNumber - 1];
					range = worksheet.GetDataRange();
					lastRow = range.RowCount;
					lastColumn = range.ColumnCount;

					//----------------------------------------------------------------------
					//MAKE SURE COLUMN HEADERS ARE CORRECT
					headingsAreCorrent = CheckPortraitHeaders(worksheet);
					if (headingsAreCorrent == false)
					{
						AIMGlobal.SplashScreenHide();
						MessageBox.Show("Spreadsheet headings do not match the System Import layout.  Please correct the file and try again.");
						return false;
					}						

					//----------------------------------------------------------------------
					//GET ROW and COLUMN COUNTS
					Int32 rowCount = range.RowCount;
					Int32 colCount = range.ColumnCount;									

					//----------------------------------------------------------------------			
					//LOOP THROUGH EXCEL ROWS 
					for (int i = beginRow + 1; i <= rowCount; i++)
					{
						CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckBackgroundWorker(backgroundWorker);

						// only these 3 columns are used in this import (existing samples):
						//column 2 - sample reference	 (sample code)		
						//column 6 - nutrient and units
						//column 7 - result value							

						//column 0 - sample-id field		
						//column 1 - spectrum reference 					
						//column 2 - material
						//column 4 - harvest year
						//column 5 - region				
						//column 8 - sample date			

						
						if (worksheet[i, 2].Value == null)
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
							//sample code							
							sampleCode = worksheet[i, 2].Value.ToString();
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
														
							//parse out units from column 6 (includes both nutrient and units)
							if (worksheet[i, 6].Value == null)
							{
								addDetail = false;
							}
							else
							{
								str = worksheet[i, 6].Value.ToString();
								length = str.Length;
							}

							//units						
							unitType = GetUnitsFromString(str, length);

							//nutrient						
							analyzerNutrientCode = GetNutrientFromString(str, length);

							//result value
							Decimal.TryParse(worksheet[i, 7].Value.ToString(), out resultValue);
							if (Decimal.TryParse(worksheet[i, 7].Value.ToString(), out checkResultValue))
							{ }
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

							//9/9/20 - change to use AIMCoreGlobal
							//sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
							sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                            sampleResult.SqlResultID = String.Empty;
                            
							//8/17/22
							//AIMManager.InsertSampleResult(sampleResult, null);
							AIMManager.InsertSampleResult(sampleResult, null, 0);

							filesCount.SuccessCount++;
						}
						else
						{
							filesCount.WrongFormatCount += 1;
						}
						filesCount.TotalFilesCount += 1;

						//----------------------------------------------------------
						//SEE IF USER CANCELLED PROCESS
						if (AIMGlobal.stopPreviewingImport == true)
						{
							AIMGlobal.stopPreviewingImport = false;
							break;
						}
					}	   
				}

				finally
				{					
					spreadsheetControl.Dispose();
				}
			}
			return true;
		}

		public static Boolean CheckPortraitHeaders(Worksheet worksheet)
		{
			Boolean rtn = false;
			try
			{						
				//----------------------------------------------------------------------
				//MAKE SURE COLUMN HEADERS ARE CORRECT
				
					if ((worksheet[0, 0].Value == "Sample ID") &&
						(worksheet[0, 1].Value == "Spectrum Reference") &&
						(worksheet[0, 2].Value == "Sample Reference") &&
						(worksheet[0, 3].Value == "Sample Type") &&
						(worksheet[0, 4].Value == "Harvest Year") &&
						(worksheet[0, 5].Value == "Region") &&
						(worksheet[0, 6].Value == "Parameter") &&
						(worksheet[0, 7].Value == "Result") &&
						(worksheet[0, 8].Value == "Date"))
						{
							rtn = true;
						}
				
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			return rtn;
		}
        		
		public static Boolean IsFileOpen(string path)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
				return false;
			}
			catch 
			{
				return true;
			}
			finally
			{
				if (fs != null)
					fs.Close();
			}
		}

	}

}
