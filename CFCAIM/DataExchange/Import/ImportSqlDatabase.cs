using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Linq;
using Core.Library.SQLDatabaseConnection;


namespace CFCAIM.DataExchange.Import
{

	//class ImportSqlDatabase
	public partial class ImportSqlDatabase : Form
	{		
		public static Boolean SqlDatabase(
			DataSourceTypeEnum dataSourceTypeID,
			DateTime startDate, 
			DateTime endDate,
			String sampleCode,			
			Int32 sqlLogID,
			String connectionName,
			ref FilesCountInfo filesCount)		
		{
			try
			{
				Boolean rtn = false;	
                CFCUser.Global userGlobal = new CFCUser.Global();	 

				switch (dataSourceTypeID)
				{
					#region Perten
					case DataSourceTypeEnum.Perten:

						//CHANGE TO SQL DATABASE WHERE SAMPLE DATA RESIDES						
						ChangeSqlDatabaseForSamples(connectionName);

						//GET SAMPLE DATA	 				
						List<PertenSQLInfo> sampleData = new List<PertenSQLInfo>();
						sampleData = AIMManager.GetPertenSqlData(sampleCode, startDate, endDate);

                        //10/14/20
                        // * * * THE STORED PROCEDURE CALLED IN GetPertenSqlData above (usp_EstimateEvent_SEL_AnalysisEndTimeSampleNumber)
                        //       can be found in: CFCTech\Development\Source\CFCAIM\Imports\SQLExternalStoredProcedures.txt

                        //CHANGE BACK TO ORIGINAL DATABASE CONNECTION			
                        ChangeSqlDatabaseBack();

						//LOOP THRU SAMPLE DATA
						for (int row = 0; row < sampleData.Count; row++)
						{
							//Get AIM sampleID
							Int32 sampleID = 0;
							sampleID = GetAIMSampleID(sampleData[row].SampleNumber.ToString(), (Int32)DataSourceTypeEnum.Perten);
							if (sampleID != 0)
							{	
								//Get Perten Result ID and check to make sure it doesn't already exist on SampleResult table								                                
                                String resultID = "";
                                resultID = sampleData[row].ID.ToString();

                                List<SampleResultInfo> checkResult = new List<SampleResultInfo>();
                                checkResult = AIMManager.GetSampleResultForSQLResultID(sampleID, resultID);
                                if (checkResult.Count == 0)
                                {
                                    try
                                    {
                                        SampleResultInfo sampleResult = new SampleResultInfo();
                                        sampleResult.SampleID = sampleID;
                                        sampleResult.AnalyzerNutrientCode = sampleData[row].ParameterTypeName;
                                        sampleResult.ResultValue = sampleData[row].ReportedResult;
                                        sampleResult.Unit = sampleData[row].UnitTypeName;
                                        sampleResult.DataSourceTypeID = (Int32)dataSourceTypeID;

                                        //9/9/20 - change to use AIMCoreGlobal
                                        //sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
                                        sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                                        sampleResult.SqlResultID = resultID;

                                        //8/17/22
                                        //AIMManager.InsertSampleResult(sampleResult, null);
                                        AIMManager.InsertSampleResult(sampleResult, null, 0);

                                        filesCount.SampleImportAcceptCount++;
                                        rtn = true;
                                    }
                                    catch 
                                    {
                                        rtn = false;
                                    }
                                }
							}
							else
							{
								try
								{
									//Write record to "Missing" log
									ErrorInfo error = new ErrorInfo();
									error = AIMManager.SaveImportSQLSampleMissing(sqlLogID, sampleData[row].SampleNumber.ToString(), userGlobal.CFCLoginUser.CFCUserID);
									filesCount.SampleImportRejectCount++;
									rtn = true;
								}
								catch 
								{
									rtn = false;
								}
							}
							filesCount.TotalFilesCount++;
						}
						break;
                    #endregion

                    #region Bruker
                    case DataSourceTypeEnum.Bruker:

                        //CHANGE TO SQL DATABASE WHERE SAMPLE DATA RESIDES						
                        ChangeSqlDatabaseForSamples(connectionName);

                        //GET SAMPLE DATA	 				
                        List<BrukerSQLInfo> sampleDataBruker = new List<BrukerSQLInfo>();
                        sampleDataBruker = AIMManager.GetBrukerSqlData(sampleCode, startDate, endDate);

                        //10/14/20
                        // * * * THE STORED PROCEDURE CALLED IN GetBrukerSqlData above (usp_T1_Quant2_Results_SEL_DateTimeAnalysisSampleCode) 
                        //       can be found in: CFCTech\Development\Source\CFCAIM\Imports\SQLExternalStoredProcedures.txt

                        //CHANGE BACK TO ORIGINAL DATABASE CONNECTION			
                        ChangeSqlDatabaseBack();

                        //LOOP THRU SAMPLE DATA
                        for (int row = 0; row < sampleDataBruker.Count; row++)
                        {
                            //Get AIM sampleID
                            Int32 sampleID = 0;
                            sampleID = GetAIMSampleID(sampleDataBruker[row].SampleCode.ToString(), (Int32)DataSourceTypeEnum.Bruker);
                            if (sampleID != 0)
                            {
                                //Get Bruker Result ID and check to make sure it doesn't already exist on SampleResult table								                                
                                String resultID = "";
                                resultID = sampleDataBruker[row].UniqueID.ToString();

                                List<SampleResultInfo> checkResult = new List<SampleResultInfo>();
                                checkResult = AIMManager.GetSampleResultForSQLResultID(sampleID, resultID);
                                if (checkResult.Count == 0)
                                {
                                    try
                                {
                                        SampleResultInfo sampleResult = new SampleResultInfo();
                                        sampleResult.SampleID = sampleID;
                                        sampleResult.AnalyzerNutrientCode = sampleDataBruker[row].Nutrient;
                                        sampleResult.ResultValue = sampleDataBruker[row].ResultValue;
                                        sampleResult.Unit = sampleDataBruker[row].UnitType;
                                        sampleResult.DataSourceTypeID = (Int32)dataSourceTypeID;

                                        //9/9/20 - change to use AIMCoreGlobal
                                        //sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
                                        sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                                        sampleResult.SqlResultID = resultID;

                                        //8/17/22
                                        //AIMManager.InsertSampleResult(sampleResult, null);
                                        AIMManager.InsertSampleResult(sampleResult, null, 0);

                                        filesCount.SampleImportAcceptCount++;
                                        rtn = true;
                                    }
                                    catch
                                    {
                                        rtn = false;
                                    }
                                }
                            }
                                else
                            {
                                try
                                {
                                    //Write record to "Missing" log
                                    ErrorInfo error = new ErrorInfo();
                                    error = AIMManager.SaveImportSQLSampleMissing(sqlLogID, sampleDataBruker[row].SampleCode.ToString(), userGlobal.CFCLoginUser.CFCUserID);
                                    filesCount.SampleImportRejectCount++;
                                    rtn = true;
                                }
                                catch
                                {
                                    rtn = false;
                                }
                            }
                            filesCount.TotalFilesCount++;
                        }
                        break;
                    #endregion

                    default:
						break;
				}
                

                

                return rtn;
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
        }

		public static void ChangeSqlDatabaseForSamples(String connectionName)
		{
			String serverName = "";
			Boolean isSqlAuth = false;
			String user = "";
			String password = "";
			String database = "";

			//----------------------------------------------------------------------------
			//CHANGE TO SQL DATABASE WHERE SAMPLE DATA RESIDES		
			AIMManager.CloseAIMData();
			List<SQLDatabaseInfo> dbase = new List<SQLDatabaseInfo>();
			SQLDatabaseConnection.LoadConnection(ref dbase, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
			var connections = from g in dbase
							  where g.ConnectName == connectionName
							  select g;
			foreach (var item in connections)
			{
				serverName = item.ServerName;
				isSqlAuth = item.IsSQLAuthentication;
				user = item.User;
				password = item.Password;
				database = item.Database;
			}
			AIMManager.IsMultipleDatabase = true;
			AIMManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(serverName, isSqlAuth, user, password, database);
		}

		public static void ChangeSqlDatabaseBack()
		{
			//----------------------------------------------------------------------------
			//CHANGE BACK TO ORIGINAL DATABASE CONNECTION
			AIMManager.CloseAIMData();
			List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
			Int32 databaseID = 0;
			SQLDatabaseConnection.LoadConnection(ref databases, true, SQLDatabaseConnection.CFCAIMDatabaseFile);
			databaseID = SQLDatabaseConnection.GetDefaultConnection(ref databases);
			AIMManager.IsMultipleDatabase = true;
			AIMManager.DatabaseConnection = SQLDatabaseConnection.GetSQLConnection(ref databases, databaseID);

		}

		private static Int32 GetAIMSampleID(String sampleCode, Int32 dataSourceTypeID)
		{			
			try
			{
				Int32 rtn = 0;

                //////Check using Alternate Code -- No, this was used initially, but now using perten's sample#               
                ////Int32 sampleNumber;
                ////switch ((DataSourceTypeEnum)dataSourceTypeID)
                ////{
                ////    case DataSourceTypeEnum.Perten:
                ////        //Get AIM sampleID for Perten SampleNumber	(Perten SampleNumber example: "Hot_0008265")			
                ////        Int32 length = 0;
                ////        Int32 dash = 0;
                ////        String numberPart = "";
                ////        length = sampleCode.Length;
                ////        dash = sampleCode.IndexOf("_") + 1;
                ////        numberPart = sampleCode.Substring(dash, length - dash);
                ////        numberPart = numberPart.TrimStart('0');         //strip leading zeros					
                ////        Int32.TryParse(numberPart, out sampleNumber);   //make sure of valid number	
                ////        break;
                ////    default:
                ////        Int32.TryParse(sampleCode, out sampleNumber);   //make sure of valid number	
                ////        break;
                ////}

                //Check Sample Table using SampleCode
                SampleInfo sample = new SampleInfo();                
                sample = AIMManager.GetSample(sampleCode);  //the "sampleCode" is really Perten's sampleNumber that's being used to compare with AIM SampleCode
                if (sample != null)
				{					
					rtn = sample.SampleID;
				}

                //////Check using Alternate Code -- No, this was used initially, but now using perten's sample#               
                //sample = AIMManager.GetSampleAlternateCode(sampleNumber.ToString());
                //if (sample != null)
                //{
                //    rtn = sample.SampleID;
                //}

                return rtn;				

			}
			catch
			{
				return 0;
			}
		}
	}
}
