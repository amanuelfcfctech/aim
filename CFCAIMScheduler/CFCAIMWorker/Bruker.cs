using CFCAIMCore.DataExchange;
using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Adisseo;
using CFCAIMCore.Entity.API;
using CFCAIMCore.Entity.Bruker;
using CFCAIMCore.Entity.CFCAPI;
using CFCAIMCore.Manager;
using CFCAIMWorker.Setup;
using Core.Library.Notification;
using Core.Library.SQLDatabaseConnection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CFCAIMWorker
{
    public class Bruker
    {
        #region Member Variables
        //7/22/22
        private static List<String> _path;
        private static List<String> _fileName;
        private static List<Int32> _brukerSampleResultID;
        private static List<Int32> _sampleID;
        private static String _databaseXML;
        private static Int32 _currentCount;
        private static Int32 _totalCount;
        private static Boolean _isCancel;
        #endregion

        #region Parameter
        public static List<String> Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
        public static List<String> FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        public static List<Int32> BrukerSampleResultID
        {
            get
            {
                return _brukerSampleResultID;
            }
            set
            {
                _brukerSampleResultID = value;
            }
        }
        public static List<Int32> SampleID
        {
            get
            {
                return _sampleID;
            }
            set
            {
                _sampleID = value;
            }
        }
        public static String DatabaseXML
        {
            get
            {
                return _databaseXML;
            }
            set
            {
                _databaseXML = value;
            }
        }
        public static Int32 CurrentCount
        {
            get
            {
                return _currentCount;
            }
            set
            {
                _currentCount = value;
            }
        }
        public static Int32 TotalCount
        {
            get
            {
                return _totalCount;
            }
            set
            {
                _totalCount = value;
            }
        }
        public static Boolean IsCancel
        {
            get
            {
                return _isCancel;
            }
            set
            {
                _isCancel = value;
            }
        }
        #endregion

        public static void ClearAllVariable()
        {
            ClearPath();
            ClearFileName();
            ClearBrukerSampleResultID();
            ClearSampleID();
        }

        public static void ClearPath()
        {
            _path = new List<String>();
        }
        public static void ClearFileName()
        {
            _fileName = new List<String>();
        }
        public static void ClearBrukerSampleResultID()
        {
            _brukerSampleResultID = new List<Int32>();
        }

        public static void ClearSampleID()
        {
            _sampleID = new List<Int32>();
        }

        public static Boolean UpdateTableStpToCurrent()
        {
            Boolean isError = false;
            Boolean isFound = false;
            String version = String.Empty;
            String sql = String.Empty;

            try
            {
                isFound = BrukerManager.GetSysObject("CFCVersion");

                if (isFound == false)
                {
                    sql = @"CREATE TABLE dbo.CFCVersion(
							CFCVersionID int IDENTITY(1, 1) NOT NULL,
							Version nvarchar(15) NULL,
							CreatedDate datetime NULL,
							CONSTRAINT PK_CFCVersion PRIMARY KEY CLUSTERED
							(
							CFCVersionID ASC
							)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
							ON [PRIMARY]) ON [PRIMARY]";
                    BrukerManager.ExecuteNonQuerySQLCommand(sql);

                    sql = "ALTER TABLE dbo.CFCVersion ADD CONSTRAINT DF_CFCVersion_CreatedDate  DEFAULT(getdate()) FOR CreatedDate";
                    BrukerManager.ExecuteNonQuerySQLCommand(sql);

                    sql = "INSERT INTO dbo.CFCVersion (Version) VALUES('24.02.01')";
                    BrukerManager.ExecuteNonQuerySQLCommand(sql);
                }

                isFound = BrukerManager.GetSysObject("CFCLogResult");

                if (isFound == false)
                {
                    sql = @"CREATE TABLE dbo.CFCLogResult(
								Path nvarchar(255) NOT NULL,
								FileName nvarchar(255) NOT NULL,
								DataBlock nvarchar(30) NOT NULL,
								MethodID int NOT NULL,
								DateTimeAnalysis datetime NOT NULL,
								GMT int NOT NULL,
								ComponentName nvarchar(30) NOT NULL,
								IsError bit NOT NULL,
								CreateStamp datetime NULL,
								ErrorMessage varchar(1000) NULL,
							PRIMARY KEY CLUSTERED 
							(
								Path ASC,
								FileName ASC,
								DataBlock ASC,
								MethodID ASC,
								DateTimeAnalysis ASC,
								GMT ASC,
								ComponentName ASC
							)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
							 ON [PRIMARY]) ON [PRIMARY]";
                    BrukerManager.ExecuteNonQuerySQLCommand(sql);

                    sql = @"ALTER TABLE dbo.CFCLogResult ADD  CONSTRAINT DF_CFCLogResult_CreateStamp  DEFAULT (getdate()) FOR CreateStamp";
                    BrukerManager.ExecuteNonQuerySQLCommand(sql);
                }

                isFound = BrukerManager.GetSysObject("CFCLogSpectrum");

                if (isFound == false)
                {
                    sql = @"CREATE TABLE dbo.CFCLogSpectrum(
								Path nvarchar(255) NOT NULL,
								FileName nvarchar(255) NOT NULL,
								IsError bit NOT NULL,
								CreateStamp datetime NULL,
								ErrorMessage varchar(1000) NULL,
								UpdateStamp datetime NULL,
							PRIMARY KEY CLUSTERED 
							(
								Path ASC,
								FileName ASC
							)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
							ON [PRIMARY]) ON [PRIMARY]";
                    BrukerManager.ExecuteNonQuerySQLCommand(sql);

                    sql = @"ALTER TABLE dbo.CFCLogSpectrum ADD  CONSTRAINT DF_CFCLogSpectrum_CreateStamp  DEFAULT (getdate()) FOR CreateStamp";
                    BrukerManager.ExecuteNonQuerySQLCommand(sql);
                }

                //version = BrukerManager.GetCurrentVersion();

                //if (version.Length == 0)
                //{
                //	sql = @"CREATE PROCEDURE dbo.usp_CFCLogResult_INS
                //						  @Path					NVARCHAR(255)	
                //						, @FileName				NVARCHAR(255)	
                //						, @DataBlock			NVARCHAR(30) 
                //						, @MethodID				INT
                //						, @DateTimeAnalysis		DATETIME
                //						, @GMT					INT
                //						, @ComponentName		NVARCHAR(30)
                //						, @IsError				BIT
                //						, @ErrorMessage			VARCHAR(1000)	
                //				AS
                //				BEGIN
                //				SET NOCOUNT ON;

                //				IF NOT EXISTS(SELECT 
                //								CLR.PATH
                //							FROM
                //								dbo.CFCLogResult CLR
                //							WHERE
                //								CLR.Path = @Path
                //								AND CLR.FileName = @FileName
                //								AND CLR.DataBlock = @DataBlock
                //								AND CLR.MethodID = @MethodID
                //								AND CLR.DateTimeAnalysis = @DateTimeAnalysis
                //								AND CLR.GMT = @GMT
                //								AND CLR.ComponentName = @ComponentName
                //								)
                //					BEGIN
                //						INSERT INTO		
                //							dbo.CFCLogResult 
                //								(	 
                //								  Path 
                //								, FileName
                //								, DataBlock		
                //								, MethodID			
                //								, DateTimeAnalysis	
                //								, GMT				
                //								, ComponentName	
                //								, IsError
                //								, ErrorMessage
                //								)
                //						VALUES
                //								(	 
                //								  @Path 
                //								, @FileName
                //								, @DataBlock		
                //								, @MethodID			
                //								, @DateTimeAnalysis	
                //								, @GMT				
                //								, @ComponentName	
                //								, @IsError
                //								, @ErrorMessage
                //								)
                //					END
                //				END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"CREATE PROCEDURE dbo.usp_CFCLogSpectrum_INS
                //						  @Path					NVARCHAR(255)	
                //						, @FileName				NVARCHAR(255)	
                //						, @IsError				BIT
                //						, @ErrorMessage			VARCHAR(1000)	
                //				AS
                //				BEGIN
                //				SET NOCOUNT ON;

                //				IF NOT EXISTS(SELECT 
                //								CLS.PATH
                //							FROM
                //								dbo.CFCLogSpectrum CLS
                //							WHERE
                //								CLS.Path = @Path
                //								AND CLS.FileName = @FileName)
                //					BEGIN
                //						INSERT INTO		
                //							dbo.CFCLogSpectrum 
                //								(	 
                //								  Path 
                //								, FileName
                //								, IsError
                //								, ErrorMessage
                //								)
                //						VALUES
                //								(	 
                //								  @Path 
                //								, @FileName
                //								, @IsError
                //								, @ErrorMessage
                //								)
                //					END
                //				END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"CREATE PROCEDURE dbo.usp_T1FileList_SEL_PathFileName
                //						  @Path					NVARCHAR(255)	
                //						, @FileName				NVARCHAR(255)	
                //				AS
                //				BEGIN
                //				SET NOCOUNT ON;


                //				SELECT 
                //					  TFL.Path
                //					, TFL.File_Name
                //					, TFL.Name_Part1
                //					, TFL.Extension
                //					, TFL.Date_Time_Entry
                //					, TFL.GMT_Entry
                //					, TFL.Date_Time_Last_Change
                //					, TFL.GMT_Last_Change
                //					, TFL.Spectrum
                //					, TFL.DBCodePage
                //				FROM 
                //					dbo.T1_File_List TFL
                //				WHERE 
                //					TFL.Path = @Path
                //					AND TFL.File_Name = @FileName
                //				END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"CREATE PROCEDURE dbo.usp_T1LabAndProcessParameters_SEL_NewResult
                //				AS
                //				BEGIN
                //				SET NOCOUNT ON;

                //				SELECT 
                //					  TLAPP.Path
                //					, TLAPP.File_Name
                //					, TLAPP.Scenario_Name
                //					, TLAPP.Measuring_Point_Name
                //					, TLAPP.Recipe
                //					, TLAPP.Batch_Number
                //					, TLAPP.Product_Group
                //					, TLAPP.Product
                //					, TLAPP.Label_Product_Info_1
                //					, TLAPP.Value_Product_Info_1
                //					, TLAPP.Label_Product_Info_2
                //					, TLAPP.Value_Product_Info_2
                //					, TLAPP.Label_Product_Info_3
                //					, TLAPP.Value_Product_Info_3
                //					, TLAPP.Label_Product_Info_4
                //					, TLAPP.Value_Product_Info_4
                //					, TLAPP.Label_Product_Info_5
                //					, TLAPP.Value_Product_Info_5
                //					, TLAPP.Device_Type
                //					, TLAPP.Device_Description
                //					, TLAPP.Position_Microplate
                //					, TLAPP.Sample_Wheel_Position
                //					, TLAPP.Spectral_Series
                //					, TLAPP.Info_Message_Ident
                //					, TLAPP.Info_Message_Quant
                //					, TLAPP.Comment_Ident
                //					, TLAPP.Comment_Quant
                //					, TLAPP.Temperature
                //					, TLAPP.Pressure
                //					, TLAPP.Flow_Rate
                //					, TLAPP.Flow_Speed
                //					, TLAPP.Humidity
                //					, TLAPP.pH_Value
                //					, TLAPP.Conductivity
                //					, TLAPP.Container_Number
                //					, TLAPP.Sample_ID
                //					, TLAPP.DBCodePage
                //				FROM            
                //					dbo.T1_Lab_And_Process_Parameters TLAPP
                //					INNER JOIN 
                //						dbo.T1_Quant2_Results TQR
                //							ON TQR.Spectrum_Path = TLAPP.Path 
                //							AND TQR.Spectrum_Name = TLAPP.File_Name 
                //					INNER JOIN
                //						dbo.T1_Spectra_Parameters TSP
                //							ON TLAPP.Path = TSP.Path 
                //							AND TLAPP.File_Name = TSP.File_Name 
                //					LEFT OUTER JOIN
                //						dbo.CFCLogResult CLR
                //							ON CLR.Path = TQR.Spectrum_Path
                //							AND CLR.FileName = TQR.Spectrum_Name 
                //							AND CLR.DataBlock = TQR.Data_Block 
                //							AND CLR.MethodID = TQR.Method_ID 
                //							AND CLR.DateTimeAnalysis = TQR.Date_Time_Analysis 
                //							AND CLR.GMT = TQR.GMT
                //							AND CLR.ComponentName = TQR.Component_Name 
                //				WHERE
                //					CLR.Path IS NULL
                //					AND TSP.ScSm_Date_Time_of_Measurement IS NOT NULL
                //					AND DATEADD(ss,TSP.Scan_Time + 60, TSP.ScSm_Date_Time_of_Measurement) < GETDATE()
                //				GROUP BY
                //					  TLAPP.Path
                //					, TLAPP.File_Name
                //					, TLAPP.Scenario_Name
                //					, TLAPP.Measuring_Point_Name
                //					, TLAPP.Recipe
                //					, TLAPP.Batch_Number
                //					, TLAPP.Product_Group
                //					, TLAPP.Product
                //					, TLAPP.Label_Product_Info_1
                //					, TLAPP.Value_Product_Info_1
                //					, TLAPP.Label_Product_Info_2
                //					, TLAPP.Value_Product_Info_2
                //					, TLAPP.Label_Product_Info_3
                //					, TLAPP.Value_Product_Info_3
                //					, TLAPP.Label_Product_Info_4
                //					, TLAPP.Value_Product_Info_4
                //					, TLAPP.Label_Product_Info_5
                //					, TLAPP.Value_Product_Info_5
                //					, TLAPP.Device_Type
                //					, TLAPP.Device_Description
                //					, TLAPP.Position_Microplate
                //					, TLAPP.Sample_Wheel_Position
                //					, TLAPP.Spectral_Series
                //					, TLAPP.Info_Message_Ident
                //					, TLAPP.Info_Message_Quant
                //					, TLAPP.Comment_Ident
                //					, TLAPP.Comment_Quant
                //					, TLAPP.Temperature
                //					, TLAPP.Pressure
                //					, TLAPP.Flow_Rate
                //					, TLAPP.Flow_Speed
                //					, TLAPP.Humidity
                //					, TLAPP.pH_Value
                //					, TLAPP.Conductivity
                //					, TLAPP.Container_Number
                //					, TLAPP.Sample_ID
                //					, TLAPP.DBCodePage
                //				END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"CREATE PROCEDURE dbo.usp_T1LabAndProcessParameters_SEL_NewSpectrum
                //				AS
                //				BEGIN
                //				SET NOCOUNT ON;

                //				SELECT 
                //					  P.Path
                //					, P.File_Name
                //					, P.Scenario_Name
                //					, P.Measuring_Point_Name
                //					, P.Recipe
                //					, P.Batch_Number
                //					, P.Product_Group
                //					, P.Product
                //					, P.Label_Product_Info_1
                //					, P.Value_Product_Info_1
                //					, P.Label_Product_Info_2
                //					, P.Value_Product_Info_2
                //					, P.Label_Product_Info_3
                //					, P.Value_Product_Info_3
                //					, P.Label_Product_Info_4
                //					, P.Value_Product_Info_4
                //					, P.Label_Product_Info_5
                //					, P.Value_Product_Info_5
                //					, P.Device_Type
                //					, P.Device_Description
                //					, P.Position_Microplate
                //					, P.Sample_Wheel_Position
                //					, P.Spectral_Series
                //					, P.Info_Message_Ident
                //					, P.Info_Message_Quant
                //					, P.Comment_Ident
                //					, P.Comment_Quant
                //					, P.Temperature
                //					, P.Pressure
                //					, P.Flow_Rate
                //					, P.Flow_Speed
                //					, P.Humidity
                //					, P.pH_Value
                //					, P.Conductivity
                //					, P.Container_Number
                //					, P.Sample_ID
                //					, P.DBCodePage
                //				FROM            
                //					dbo.T1_Lab_And_Process_Parameters  P
                //					LEFT OUTER JOIN
                //						dbo.T1_Spectra_Parameters TSP
                //							ON P.Path = TSP.Path 
                //							AND P.File_Name = TSP.File_Name 
                //					LEFT OUTER JOIN
                //						dbo.CFCLogSpectrum CLS	 
                //							ON	CLS.Path = P.Path 
                //							AND	CLS.FileName = P.File_Name
                //				WHERE
                //					CLS.Path IS NULL
                //					AND TSP.ScSm_Date_Time_of_Measurement IS NOT NULL
                //					AND DATEADD(ss,TSP.Scan_Time + 60, TSP.ScSm_Date_Time_of_Measurement) < GETDATE()
                //				END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"CREATE PROCEDURE dbo.usp_T1Quant2Results_SEL_DateTimeAnalysisSampleCode
                //						  @SampleCode		NVARCHAR(255)	= NULL
                //						, @AnalysisStartTime	DATETIME		= NULL
                //						, @AnalysisEndTime		DATETIME		= NULL
                //				AS
                //				BEGIN
                //				SET NOCOUNT ON;

                //				SELECT 
                //					  P.Value_Product_Info_1 as SampleCode
                //					, P.Product AS Material
                //					, P.Path 
                //					, P.File_Name AS FileName
                //					, R.Date_Time_Analysis as SampleDate
                //					, R.Component_Name AS Nutrient
                //					, R.Component_Unit AS UnitType
                //					, R.Prediction_Calculated as ResultValue	
                //					, R.Spectrum_Name + '-' + cast(R.Method_ID AS VARCHAR) UniqueID
                //				FROM            
                //					dbo.T1_Lab_And_Process_Parameters  P
                //				INNER JOIN
                //					dbo.T1_Quant2_Results R						 
                //						ON		P.Path = R.Spectrum_Path 
                //						AND		P.File_Name = R.Spectrum_Name
                //				WHERE
                //					R.Date_Time_Analysis >= ISNULL(@AnalysisStartTime, R.Date_Time_Analysis)
                //					AND R.Date_Time_Analysis <= ISNULL(@AnalysisEndTime, R.Date_Time_Analysis)
                //					AND P.Value_Product_Info_1 = ISNULL(@SampleCode, P.Value_Product_Info_1)
                //					AND R.Prediction_Calculated IS NOT NULL
                //				ORDER BY
                //					  P.Value_Product_Info_1
                //					, R.Date_Time_Analysis 
                //				END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"CREATE PROCEDURE dbo.usp_T1Quant2Results_SEL_NewResult
                //				AS
                //				BEGIN
                //				SET NOCOUNT ON;

                //				SELECT 
                //					  P.Value_Product_Info_1 as SampleCode
                //					, P.Product as Material
                //					, P.Path 
                //					, P.File_Name AS FileName
                //					, R.Date_Time_Analysis as SampleDate
                //					, R.Component_Name as Nutrient
                //					, R.Component_Unit as UnitType
                //					, R.Prediction_Calculated as ResultValue	
                //					, R.Spectrum_Name + '-' + cast(R.Method_ID AS VARCHAR) UniqueID
                //				FROM            
                //					dbo.T1_Lab_And_Process_Parameters  P
                //					INNER JOIN
                //						dbo.T1_Quant2_Results R						 
                //							ON	P.Path = R.Spectrum_Path 
                //							AND	P.File_Name = R.Spectrum_Name
                //					LEFT OUTER JOIN
                //						dbo.CFCLog CL	 
                //							ON	CL.Path = P.Path 
                //							AND	CL.FileName = P.File_Name
                //				WHERE
                //					CL.Path IS NULL
                //					AND R.Prediction_Calculated IS NOT NULL
                //				ORDER BY
                //					  P.Value_Product_Info_1
                //					, R.Date_Time_Analysis 
                //				END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"CREATE PROCEDURE [dbo].[usp_T1Quant2Results_SEL_SpectrumPathSpectrumName]
                //						  @SpectrumPath				NVARCHAR(255)	
                //						, @SpectrumName				NVARCHAR(255)	
                //				AS
                //				BEGIN
                //				SET NOCOUNT ON;

                //				SELECT 
                //					  TQR.Spectrum_Path
                //					, TQR.Spectrum_Name
                //					, TQR.Data_Block
                //					, TQR.Method_ID
                //					, TQR.Date_Time_Analysis
                //					, TQR.GMT
                //					, TQR.X_Startpoint
                //					, TQR.X_Endpoint
                //					, TQR.Component_Name
                //					, TQR.Component_Unit
                //					, TQR.Formatting
                //					, TQR.Prediction
                //					, TQR.Prediction_Outside
                //					, TQR.Target_Concentration
                //					, TQR.Warning_Upper_Limit
                //					, TQR.Warning_Lower_Limit
                //					, TQR.Alarm_Upper_Limit
                //					, TQR.Alarm_Lower_Limit
                //					, TQR.Mah_Dist
                //					, TQR.Mah_Dist_Limit
                //					, TQR.Mah_Dist_Outlier
                //					, TQR.Spec_Residual
                //					, TQR.F_Value
                //					, TQR.F_Prob
                //					, TQR.F_Prob_Limit
                //					, TQR.Spec_Residual_Outlier
                //					, TQR.Component_Value_Density
                //					, TQR.Component_Value_Density_Limit
                //					, TQR.Process_Channel_Name
                //					, TQR.Component_Name_User
                //					, TQR.Component_Unit_User
                //					, TQR.Prediction_Calculated
                //					, TQR.Prediction_Calculated_Outside
                //					, TQR.Formula
                //					, TQR.External_Bias
                //					, TQR.Mah_Dist_Limit_By_Factor
                //					, TQR.MDI
                //					, TQR.Method_Path
                //					, TQR.Method_Name
                //					, TQR.Method_Date_Time
                //					, TQR.Method_GMT
                //					, TQR.DBCodePage
                //				FROM 
                //					dbo.T1_Quant2_Results TQR
                //					LEFT OUTER JOIN 
                //						dbo.CFCLogResult CLR
                //							ON CLR.Path = TQR.Spectrum_Path
                //							AND CLR.FileName = TQR.Spectrum_Name 
                //							AND CLR.DataBlock = TQR.Data_Block 
                //							AND CLR.MethodID = TQR.Method_ID 
                //							AND CLR.DateTimeAnalysis = TQR.Date_Time_Analysis 
                //							AND CLR.GMT = TQR.GMT
                //							AND CLR.ComponentName = TQR.Component_Name 
                //				WHERE 
                //					CLR.Path IS NULL
                //					AND TQR.Spectrum_Path = @SpectrumPath
                //					AND TQR.Spectrum_Name = @SpectrumName
                //				END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"INSERT INTO dbo.CFCVersion(Version, CreatedDate) VALUES('22.04.11', GETDATE())";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);
                //}

                //version = BrukerManager.GetCurrentVersion();

                //if (version == "22.04.11")
                //{
                //	sql = @"ALTER PROCEDURE dbo.usp_T1LabAndProcessParameters_SEL_NewSpectrum
                //			AS
                //			BEGIN
                //			SET NOCOUNT ON;

                //			SELECT 
                //				  P.Path
                //				, P.File_Name
                //				, P.Scenario_Name
                //				, P.Measuring_Point_Name
                //				, P.Recipe
                //				, P.Batch_Number
                //				, P.Product_Group
                //				, P.Product
                //				, P.Label_Product_Info_1
                //				, P.Value_Product_Info_1
                //				, P.Label_Product_Info_2
                //				, P.Value_Product_Info_2
                //				, P.Label_Product_Info_3
                //				, P.Value_Product_Info_3
                //				, P.Label_Product_Info_4
                //				, P.Value_Product_Info_4
                //				, P.Label_Product_Info_5
                //				, P.Value_Product_Info_5
                //				, P.Device_Type
                //				, P.Device_Description
                //				, P.Position_Microplate
                //				, P.Sample_Wheel_Position
                //				, P.Spectral_Series
                //				, P.Info_Message_Ident
                //				, P.Info_Message_Quant
                //				, P.Comment_Ident
                //				, P.Comment_Quant
                //				, P.Temperature
                //				, P.Pressure
                //				, P.Flow_Rate
                //				, P.Flow_Speed
                //				, P.Humidity
                //				, P.pH_Value
                //				, P.Conductivity
                //				, P.Container_Number
                //				, P.Sample_ID
                //				, P.DBCodePage
                //			FROM            
                //				dbo.T1_Lab_And_Process_Parameters  P
                //				LEFT OUTER JOIN
                //					dbo.T1_Spectra_Parameters TSP
                //						ON P.Path = TSP.Path 
                //						AND P.File_Name = TSP.File_Name 
                //				LEFT OUTER JOIN
                //					dbo.CFCLogSpectrum CLS	 
                //						ON	CLS.Path = P.Path 
                //						AND	CLS.FileName = P.File_Name
                //			WHERE
                //				CLS.Path IS NULL
                //				AND TSP.ScSm_Date_Time_of_Measurement IS NOT NULL
                //				AND DATEADD(ss,ISNULL(TSP.Scan_Time, 600) + 60, TSP.ScSm_Date_Time_of_Measurement) < GETDATE()
                //			END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"ALTER PROCEDURE [dbo].[usp_T1LabAndProcessParameters_SEL_NewResult]
                //			AS
                //			BEGIN
                //			SET NOCOUNT ON;

                //			SELECT 
                //				  TLAPP.Path
                //				, TLAPP.File_Name
                //				, TLAPP.Scenario_Name
                //				, TLAPP.Measuring_Point_Name
                //				, TLAPP.Recipe
                //				, TLAPP.Batch_Number
                //				, TLAPP.Product_Group
                //				, TLAPP.Product
                //				, TLAPP.Label_Product_Info_1
                //				, TLAPP.Value_Product_Info_1
                //				, TLAPP.Label_Product_Info_2
                //				, TLAPP.Value_Product_Info_2
                //				, TLAPP.Label_Product_Info_3
                //				, TLAPP.Value_Product_Info_3
                //				, TLAPP.Label_Product_Info_4
                //				, TLAPP.Value_Product_Info_4
                //				, TLAPP.Label_Product_Info_5
                //				, TLAPP.Value_Product_Info_5
                //				, TLAPP.Device_Type
                //				, TLAPP.Device_Description
                //				, TLAPP.Position_Microplate
                //				, TLAPP.Sample_Wheel_Position
                //				, TLAPP.Spectral_Series
                //				, TLAPP.Info_Message_Ident
                //				, TLAPP.Info_Message_Quant
                //				, TLAPP.Comment_Ident
                //				, TLAPP.Comment_Quant
                //				, TLAPP.Temperature
                //				, TLAPP.Pressure
                //				, TLAPP.Flow_Rate
                //				, TLAPP.Flow_Speed
                //				, TLAPP.Humidity
                //				, TLAPP.pH_Value
                //				, TLAPP.Conductivity
                //				, TLAPP.Container_Number
                //				, TLAPP.Sample_ID
                //				, TLAPP.DBCodePage
                //			FROM            
                //				dbo.T1_Lab_And_Process_Parameters TLAPP
                //				INNER JOIN 
                //					dbo.T1_Quant2_Results TQR
                //						ON TQR.Spectrum_Path = TLAPP.Path 
                //						AND TQR.Spectrum_Name = TLAPP.File_Name 
                //				INNER JOIN
                //					dbo.T1_Spectra_Parameters TSP
                //						ON TLAPP.Path = TSP.Path 
                //						AND TLAPP.File_Name = TSP.File_Name 
                //				LEFT OUTER JOIN
                //					dbo.CFCLogResult CLR
                //						ON CLR.Path = TQR.Spectrum_Path
                //						AND CLR.FileName = TQR.Spectrum_Name 
                //						AND CLR.DataBlock = TQR.Data_Block 
                //						AND CLR.MethodID = TQR.Method_ID 
                //						AND CLR.DateTimeAnalysis = TQR.Date_Time_Analysis 
                //						AND CLR.GMT = TQR.GMT
                //						AND CLR.ComponentName = TQR.Component_Name 
                //			WHERE
                //				CLR.Path IS NULL
                //				AND TSP.ScSm_Date_Time_of_Measurement IS NOT NULL
                //				AND DATEADD(ss,ISNULL(TSP.Scan_Time, 600) + 60, TSP.ScSm_Date_Time_of_Measurement) < GETDATE()
                //			GROUP BY
                //				  TLAPP.Path
                //				, TLAPP.File_Name
                //				, TLAPP.Scenario_Name
                //				, TLAPP.Measuring_Point_Name
                //				, TLAPP.Recipe
                //				, TLAPP.Batch_Number
                //				, TLAPP.Product_Group
                //				, TLAPP.Product
                //				, TLAPP.Label_Product_Info_1
                //				, TLAPP.Value_Product_Info_1
                //				, TLAPP.Label_Product_Info_2
                //				, TLAPP.Value_Product_Info_2
                //				, TLAPP.Label_Product_Info_3
                //				, TLAPP.Value_Product_Info_3
                //				, TLAPP.Label_Product_Info_4
                //				, TLAPP.Value_Product_Info_4
                //				, TLAPP.Label_Product_Info_5
                //				, TLAPP.Value_Product_Info_5
                //				, TLAPP.Device_Type
                //				, TLAPP.Device_Description
                //				, TLAPP.Position_Microplate
                //				, TLAPP.Sample_Wheel_Position
                //				, TLAPP.Spectral_Series
                //				, TLAPP.Info_Message_Ident
                //				, TLAPP.Info_Message_Quant
                //				, TLAPP.Comment_Ident
                //				, TLAPP.Comment_Quant
                //				, TLAPP.Temperature
                //				, TLAPP.Pressure
                //				, TLAPP.Flow_Rate
                //				, TLAPP.Flow_Speed
                //				, TLAPP.Humidity
                //				, TLAPP.pH_Value
                //				, TLAPP.Conductivity
                //				, TLAPP.Container_Number
                //				, TLAPP.Sample_ID
                //				, TLAPP.DBCodePage
                //			END";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);

                //	sql = @"INSERT INTO dbo.CFCVersion(Version, CreatedDate) VALUES('22.10.06', GETDATE())";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);
                //}

                //version = BrukerManager.GetCurrentVersion();

                //if (version == "22.10.06")
                //{
                //	sql = @"INSERT INTO dbo.CFCVersion(Version, CreatedDate) VALUES('22.10.14', GETDATE())";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);
                //}


                //if (version == "22.10.14")
                //{
                //	sql = @"INSERT INTO dbo.CFCVersion(Version, CreatedDate) VALUES('23.01.05', GETDATE())";
                //	BrukerManager.ExecuteNonQuerySQLCommand(sql);
                //}

                //version = BrukerManager.GetCurrentVersion();

                //if (version == "23.01.05")
                //{
                isError = true;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            return isError;
        }

        public static void SendSampleToAPI()
        {
            try
            {
                List<BrukerSampleInfo> brukerSampleList = new List<BrukerSampleInfo>();
                List<BrukerSampleInfo> brukerSample = new List<BrukerSampleInfo>();
                List<BrukerSampleResultInfo> brukerSampleResult = new List<BrukerSampleResultInfo>();
                List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
                List<APITokenInfo> apiToken = new List<APITokenInfo>();
                List<InstrumentInfo> instrument = new List<InstrumentInfo>();

                //7/22/22
                List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();

                TokenInfo tokenKey = new TokenInfo();
                BrukerCFCLogResultInfo brukerCFCLog = new BrukerCFCLogResultInfo();
                BrukerFileListInfo brukerFileList = new BrukerFileListInfo();
                FilesCountInfo filesCount = new FilesCountInfo();
                SettingInfo setting = new SettingInfo();
                SampleInfo sample = new SampleInfo();
                ErrorInfo error = new ErrorInfo();
                FilesInfo file = new FilesInfo();
                BatchInfo batch = new BatchInfo();

                String cfcAIMWorkerFile = String.Empty;
                String fileName = String.Empty;
                String argument = String.Empty;
                String aimApiKey = String.Empty;
                String aimInstrumentKey = String.Empty;
                String token = String.Empty;
                String sql = String.Empty;
                String str = String.Empty;
                Int32 brukerSampleID = 0;
                Int32 instrumentID = 0;
                Int32 customerID = 0;
                Int32 batchID = 0;

                //				Utility.LoadConnection(ref setting);
                Library.LoadSetting(ref setting, Utility.SettingXML);

                Library.WriteErrorLogFile("Start get bruker data");

                //7/22/22
                //aimApiKey = Properties.Settings.Default.AIMAPIKey;
                //CFCAPIManager.APIUrl = Properties.Settings.Default.APIUrl;
                aimApiKey = setting.AIMAPIKey;
                aimInstrumentKey = setting.AIMInstrumentKey;
                //CFCAPIManager.APIUrl = setting.APIUrl;
                CFCAPIManager.Setting = setting;

                BrukerManager.IsMessageDisplay = false;

                //7/22/22
                //MW 220913 databases = new List<SQLDatabaseInfo>();
                SQLDatabaseConnection.LoadConnection(ref databases, true, _databaseXML);

                //7/22/22
                //BrukerManager.CreateBrukerConnection(Properties.Settings.Default.Server, Properties.Settings.Default.BrukerDatabase, true, Properties.Settings.Default.User, Properties.Settings.Default.Pwd);								
                BrukerManager.CreateBrukerConnection(databases[0].ServerName, databases[0].Database, databases[0].IsSQLAuthentication, databases[0].User, databases[0].Password);

                if (UpdateTableStpToCurrent() == false)
                {
                    //7/22/22
                    //Library.WriteErrorLogFile("Bruker database does not have update to date AIM tables.");
                    Library.WriteErrorLogFile("Bruker database does not have up-to-date AIM tables.");
                }
                else
                {
                    // Bruker Spectrum
                    brukerSampleList = BrukerManager.GetBrukerSpectrum();

                    if (brukerSampleList.Count > 0)
                    {
                        apiToken = CFCAPIManager.GetToken(aimApiKey);

                        if (apiToken == null)
                        {
                            Library.WriteErrorLogFile("Batch api is not working.");
                        }
                        else
                        {
                            token = apiToken[0].Token;
                            customerID = apiToken[0].CustomerID;

                            instrument = CFCAPIManager.GetInstrument(token, aimInstrumentKey);

                            if (instrument == null)
                            {
                                Library.WriteErrorLogFile("CFC Tech Api GetInstrument is not working");
                            }
                            else
                            {
                                instrumentID = instrument[0].InstrumentID;

                                batch.CustomerID = customerID;
                                batch.InstrumentID = instrument[0].InstrumentID;
                                batch.Token = token;
                                error = CFCAPIManager.PutBatch(batch);
                                batch.BatchID = error.RecordID;
                                batchID = error.RecordID;


                                if (error.RecordID == 0)
                                {
                                    Library.WriteErrorLogFile("Batch api is not working.");
                                }
                                else
                                {
                                    foreach (BrukerSampleInfo item in brukerSampleList)
                                    {
                                        item.Token = token;
                                        item.BatchID = batch.BatchID;
                                        error = CFCAPIManager.PutBrukerSample(item);
                                        if (error == null)
                                        {
                                            Library.WriteErrorLogFile("CFC Tech Api is not working");
                                        }
                                        else
                                        {
                                            if (error.ErrorNumber > 0)
                                            {
                                                Library.WriteErrorLogFile(error.ErrorNumber.ToString() + ' ' + error.ErrorMessage);
                                            }
                                            else
                                            {
                                                brukerSampleID = error.RecordID;
                                                // If path to the sample file does not exists log an error - else process the contents of the sample file

                                                if (item.Path.Substring(0, 1) == "*")
                                                {
                                                    brukerFileList = BrukerManager.GetBrukerFileList(item.Path, item.FileName);

                                                    if (brukerFileList != null)
                                                    {
                                                        BrukerSpectrumInfo brukerSpectrumFile = new BrukerSpectrumInfo();
                                                        brukerSpectrumFile.BrukerSampleID = brukerSampleID;
                                                        brukerSpectrumFile.Token = token;
                                                        //	brukerSpectrumFile.UpdateStamp = brukerFileList.DateTimeLastChange;														
                                                        brukerSpectrumFile.CreateStamp = DateTime.Now;
                                                        brukerSpectrumFile.BrukerSpectrum = brukerFileList.Spectrum;
                                                        error = CFCAPIManager.PutBrukerSpectrumFile(brukerSpectrumFile);
                                                        CFCAPIManager.PutBrukerSpectrumReceive(token, error.RecordID);
                                                        BrukerManager.InsertCFCLogSpectrum(item.Path, item.FileName);
                                                    }
                                                }
                                                else
                                                {
                                                    if (!Directory.Exists(item.Path))
                                                    {
                                                        str = "sample File PATH not found. Did not write spectrum file to the api database.";
                                                        BrukerManager.InsertCFCLogSpectrum(item.Path, item.FileName, true, str);
                                                        Library.WriteErrorLogFile(fileName + " " + str);
                                                    }
                                                    else
                                                    {
                                                        // Check if the file exists too
                                                        if (!File.Exists(fileName + @"\" + item.FileName))
                                                        {
                                                            str = "sample FILE not found. Did not write spectrum file to the api database.";
                                                            BrukerManager.InsertCFCLogSpectrum(item.Path, item.FileName, true, str);
                                                            Library.WriteErrorLogFile(item.FileName + " " + str);
                                                        }
                                                        else
                                                        {
                                                            BrukerSpectrumInfo brukerSpectrumFile = new BrukerSpectrumInfo();
                                                            brukerSpectrumFile.BrukerSampleID = brukerSampleID;
                                                            brukerSpectrumFile.Token = token;
                                                            //		brukerSpectrumFile.UpdateStamp = brukerFileList.DateTimeLastChange;
                                                            brukerSpectrumFile.CreateStamp = DateTime.Now;
                                                            byte[] byteFile = System.IO.File.ReadAllBytes(fileName + @"\" + item.FileName);

                                                            brukerSpectrumFile.BrukerSpectrum = byteFile;
                                                            error = CFCAPIManager.PutBrukerSpectrumFile(brukerSpectrumFile);
                                                            CFCAPIManager.PutBrukerSpectrumReceive(token, error.RecordID);
                                                            BrukerManager.InsertCFCLogSpectrum(item.Path, item.FileName);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                        }
                    }

                    //Bruker Sample Result

                    brukerSampleList = BrukerManager.GetBrukerSample();

                    if (brukerSampleList.Count > 0)
                    {
                        if (token == String.Empty)
                        {
                            instrument = CFCAPIManager.GetInstrument(token, aimInstrumentKey);
                            instrumentID = instrument[0].InstrumentID;

                            apiToken = CFCAPIManager.GetToken(aimApiKey);
                            token = apiToken[0].Token;
                            customerID = apiToken[0].CustomerID;
                            batch.CustomerID = customerID;
                            batch.InstrumentID = instrument[0].InstrumentID;
                            error = CFCAPIManager.PutBatch(batch);
                            batch.BatchID = error.RecordID;
                            batchID = error.RecordID;
                        }

                        foreach (BrukerSampleInfo item in brukerSampleList)
                        {
                            item.BatchID = batch.BatchID;
                            brukerSample = CFCAPIManager.GetBrukerSample(token, customerID, item.Path, item.FileName);

                            if (brukerSample == null)
                            {
                                Library.WriteErrorLogFile("CFC Tech Api GetBrukerSample is not working");
                            }
                            else
                            {
                                if (brukerSample.Count == 0)
                                {
                                    Library.WriteErrorLogFile("No sample found with api.");
                                }
                                else
                                {
                                    brukerSampleID = brukerSample[0].BrukerSampleID;
                                    brukerSampleResult = BrukerManager.GetBrukerSampleResultNotSent(item.Path, item.FileName);

                                    if (brukerSampleResult == null)
                                    {
                                        Library.WriteErrorLogFile("CFC Tech Api is not working");
                                    }
                                    else
                                    {
                                        if (brukerSampleResult.Count == 0)
                                        {
                                            Library.WriteErrorLogFile("No sample result found.");
                                        }
                                        else
                                        {
                                            foreach (BrukerSampleResultInfo result in brukerSampleResult)
                                            {
                                                result.BrukerSampleID = brukerSampleID;
                                                result.Token = token;
                                                CFCAPIManager.PutBrukerSampleResult(result);
                                                CFCAPIManager.PutBrukerSampleResultReceive(token, brukerSampleID);
                                                brukerCFCLog = new BrukerCFCLogResultInfo();
                                                brukerCFCLog.Path = result.SpectrumPath;
                                                brukerCFCLog.FileName = result.SpectrumName;
                                                brukerCFCLog.DataBlock = result.DataBlock;
                                                brukerCFCLog.MethodID = result.MethodID;
                                                brukerCFCLog.DateTimeAnalysis = result.DateTimeAnalysis;
                                                brukerCFCLog.GMT = result.GMT;
                                                brukerCFCLog.ComponentName = result.ComponentName;
                                                brukerCFCLog.IsError = false;
                                                brukerCFCLog.ErrorMessage = String.Empty;
                                                BrukerManager.InsertCFCLogResult(brukerCFCLog);  //Start here
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (token != String.Empty)
                    {
                        batch.Token = token;
                        tokenKey.Token = token;
                        error = CFCAPIManager.PutBatchEnd(batch);
                        error = CFCAPIManager.PutTokenEnd(tokenKey);
                    }
                }

                Library.WriteErrorLogFile("End further analyses");
            }
            catch (Exception ex)
            {
                Library.WriteErrorLogFile("***Error*****" + ex.Message);
            }
        }

        public static void SendTangoSampleToAPI()
        {
            try
            {
                List<BrukerSampleInfo> brukerSampleList = new List<BrukerSampleInfo>();
                List<BrukerSampleInfo> brukerSample = new List<BrukerSampleInfo>();
                List<BrukerSampleResultInfo> brukerSampleResult = new List<BrukerSampleResultInfo>();
                List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
                //DB230623 List<APITokenInfo> apiToken = new List<APITokenInfo>();
                List<APITokenV1Info> apiToken = new List<APITokenV1Info>();     //DB230623
                                                                                //7/22/22
                List<SQLDatabaseInfo> _databases = new List<SQLDatabaseInfo>();

                BrukerCFCLogResultInfo brukerCFCLog = new BrukerCFCLogResultInfo();
                BrukerFileListInfo brukerFileList = new BrukerFileListInfo();
                TokenInfo tokenKey = new TokenInfo();
                SampleInfo sample = new SampleInfo();
                ErrorInfo error = new ErrorInfo();
                FilesInfo file = new FilesInfo();
                BatchInfo batch = new BatchInfo();
                FilesCountInfo filesCount = new FilesCountInfo();
                String argument = String.Empty;
                String aimApiKey = String.Empty;
                String aimInstrumentKey = String.Empty;             //DB230623
                String token = String.Empty;
                String sql = String.Empty;
                String str = String.Empty;
                Int32 brukerSampleID = 0;
                //				Int32 instrumentID = 0;
                Int32 customerID = 0;
                Int32 batchID = 0;

                //7/22/22
                SettingInfo setting = new SettingInfo();
                //Utility.LoadConnection(ref setting);
                Library.LoadSetting(ref setting, Utility.SettingXML);

                //7/22/22
                //aimApiKey = Properties.Settings.Default.AIMAPIKey;
                //CFCAPIManager.APIUrl = Properties.Settings.Default.APIUrl;
                aimApiKey = setting.AIMAPIKey;
                aimInstrumentKey = setting.AIMInstrumentKey;                //DB230623
                                                                            //CFCAPIManager.APIUrl = setting.APIUrl;
                CFCAPIManager.Setting = setting;

                Library.WriteErrorLogFile("Start get bruker Tango data");
                BrukerManager.IsMessageDisplay = false;

                //7/22/22
                _databases = new List<SQLDatabaseInfo>();
                SQLDatabaseConnection.LoadConnection(ref _databases, true, _databaseXML);

                //7/22/22
                //BrukerManager.CreateBrukerConnection(Properties.Settings.Default.Server, Properties.Settings.Default.BrukerDatabase, true, Properties.Settings.Default.User, Properties.Settings.Default.Pwd);				
                BrukerManager.CreateBrukerConnection(_databases[0].ServerName, _databases[0].Database, _databases[0].IsSQLAuthentication, _databases[0].User, _databases[0].Password);

                if (UpdateTableStpToCurrent() == false)
                {
                    //7/22/22
                    //Library.WriteErrorLogFile("Bruker database does not have update to date AIM tables.");
                    Library.WriteErrorLogFile("Bruker database does not have up-to-date AIM tables.");
                }
                else
                {
                    // Bruker Spectrum
                    brukerSampleList = BrukerManager.GetBrukerSpectrum();
                    Library.WriteErrorLogFile("BrukerSampleList = " + brukerSampleList.Count.ToString());

                    if (brukerSampleList.Count > 0)
                    {
                        //DB230623 apiToken = CFCAPIManager.GetToken(aimApiKey);
                        apiToken = CFCAPIManager.GetToken(aimApiKey, aimInstrumentKey);            //DB230623

                        if (apiToken == null)
                        {
                            Library.WriteErrorLogFile("Batch api is not working.");
                        }
                        else
                        {
                            token = apiToken[0].Token;
                            customerID = apiToken[0].CustomerID;
                            batch.CustomerID = customerID;
                            batch.InstrumentID = apiToken[0].InstrumentID;
                            batch.Token = token;
                            error = CFCAPIManager.PutBatch(batch);
                            batch.BatchID = error.RecordID;
                            batchID = error.RecordID;

                            if (error.RecordID == 0)
                            {
                                Library.WriteErrorLogFile("Batch api is not working.");
                            }
                            else
                            {
                                foreach (BrukerSampleInfo item in brukerSampleList)
                                {
                                    item.Token = token;
                                    item.BatchID = batch.BatchID;
                                    error = CFCAPIManager.PutBrukerSample(item);
                                    if (error == null)
                                    {
                                        Library.WriteErrorLogFile("CFC Tech Api is not working");
                                    }
                                    else
                                    {
                                        if (error.ErrorNumber > 0)
                                        {
                                            Library.WriteErrorLogFile(error.ErrorNumber.ToString() + ' ' + error.ErrorMessage);
                                        }
                                        else
                                        {
                                            brukerSampleID = error.RecordID;
                                            // If path to the sample file does not exists log an error - else process the contents of the sample file
                                            brukerFileList = BrukerManager.GetBrukerFileList(item.Path, item.FileName);
                                            if (brukerFileList == null)
                                            {
                                                str = "sample File PATH not found. Did not write spectrum file to the api database.";
                                                BrukerManager.InsertCFCLogSpectrum(item.Path, item.FileName, true, str);
                                                Library.WriteErrorLogFile(item.Path + " " + str);
                                            }
                                            else
                                            {
                                                BrukerSpectrumInfo brukerSpectrumFile = new BrukerSpectrumInfo();
                                                brukerSpectrumFile.BrukerSampleID = brukerSampleID;
                                                brukerSpectrumFile.Token = token;
                                                brukerSpectrumFile.CreateStamp = DateTime.Now;
                                                brukerSpectrumFile.BrukerSpectrum = brukerFileList.Spectrum;
                                                error = CFCAPIManager.PutBrukerSpectrumFile(brukerSpectrumFile);
                                                CFCAPIManager.PutBrukerSpectrumReceive(token, error.RecordID);
                                                BrukerManager.InsertCFCLogSpectrum(item.Path, item.FileName);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Bruker Sample Result

                    brukerSampleList = BrukerManager.GetBrukerSample();

                    if (brukerSampleList.Count > 0)
                    {
                        Library.WriteErrorLogFile("Bruker Sample Result Count = " + brukerSampleList.Count.ToString());

                        if (token == String.Empty)
                        {
                            //DB230623 apiToken = CFCAPIManager.GetToken(aimApiKey);
                            apiToken = CFCAPIManager.GetToken(aimApiKey, aimInstrumentKey); //DB230623 
                            token = apiToken[0].Token;
                            customerID = apiToken[0].CustomerID;
                            batch.CustomerID = customerID;
                            batch.InstrumentID = apiToken[0].InstrumentID;
                            batch.Token = token;                    //DB240531
                            error = CFCAPIManager.PutBatch(batch);
                            batch.BatchID = error.RecordID;
                            batchID = error.RecordID;
                        }

                        foreach (BrukerSampleInfo item in brukerSampleList)
                        {
                            item.BatchID = batch.BatchID;
                            Library.WriteErrorLogFile("File Name = " + item.FileName);
                            brukerSample = CFCAPIManager.GetBrukerSample(token, customerID, item.Path, item.FileName);

                            if (brukerSample == null)
                            {
                                Library.WriteErrorLogFile("CFC Tech Api GetBrukerSample is not working");
                            }
                            else
                            {
                                if (brukerSample.Count == 0)
                                {
                                    Library.WriteErrorLogFile("No sample found with api.");
                                }
                                else
                                {
                                    brukerSampleID = brukerSample[0].BrukerSampleID;
                                    brukerSampleResult = BrukerManager.GetBrukerSampleResultNotSent(item.Path, item.FileName);

                                    if (brukerSampleResult == null)
                                    {
                                        Library.WriteErrorLogFile("CFC Tech Api is not working");
                                    }
                                    else
                                    {
                                        if (brukerSampleResult.Count == 0)
                                        {
                                            Library.WriteErrorLogFile("No sample result found.");
                                        }
                                        else
                                        {
                                            Library.WriteErrorLogFile("Result Count = " + brukerSampleResult.Count.ToString());

                                            foreach (BrukerSampleResultInfo result in brukerSampleResult)
                                            {
                                                result.BrukerSampleID = brukerSampleID;
                                                result.Token = token;
                                                error = CFCAPIManager.PutBrukerSampleResult(result);

                                                if (error.ErrorNumber == 0)
                                                {
                                                    CFCAPIManager.PutBrukerSampleResultReceive(token, error.RecordID);
                                                    Library.WriteErrorLogFile("PutBrukerSampleResultReceive   RecordID = " + error.RecordID.ToString());
                                                    brukerCFCLog = new BrukerCFCLogResultInfo();
                                                    brukerCFCLog.Path = result.SpectrumPath;
                                                    brukerCFCLog.FileName = result.SpectrumName;
                                                    brukerCFCLog.DataBlock = result.DataBlock;
                                                    brukerCFCLog.MethodID = result.MethodID;
                                                    brukerCFCLog.DateTimeAnalysis = result.DateTimeAnalysis;
                                                    brukerCFCLog.GMT = result.GMT;
                                                    brukerCFCLog.ComponentName = result.ComponentName;
                                                    brukerCFCLog.IsError = false;
                                                    brukerCFCLog.ErrorMessage = String.Empty;

                                                    BrukerManager.InsertCFCLogResult(brukerCFCLog);  //Start here
                                                }
                                                else
                                                {
                                                    Library.WriteErrorLogFile("Error PutBrukerSampleResult   ErrorNumber = " + error.ErrorNumber.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (token != String.Empty)
                    {
                        batch.Token = token;
                        tokenKey.Token = token;
                        error = CFCAPIManager.PutBatchEnd(batch);
                        error = CFCAPIManager.PutTokenEnd(tokenKey);
                    }
                }

                Library.WriteErrorLogFile("End further analyses");
            }
            catch (Exception ex)
            {
                StackTrace stack = new StackTrace(ex, true);
                StackFrame frame = stack.GetFrame(0);
                Int32 line = frame.GetFileLineNumber();
                Library.WriteErrorLogFile("***Error***** Line = " + line.ToString() + " Message =" + ex.Message);
            }
        }

        public static void GetSampleFromAPI()
        {
            List<BrukerSampleInfo> brukerSampleList = new List<BrukerSampleInfo>();
            List<BrukerSpectrumInfo> brukerSpectrumFile = new List<BrukerSpectrumInfo>();
            List<BrukerSampleResultInfo> brukerSampleResult = new List<BrukerSampleResultInfo>();
            List<BrukerSpectrumSentInfo> brukerSpectrumSentList = new List<BrukerSpectrumSentInfo>();
            List<BrukerSampleResultSentInfo> brukerSampleResultSentList = new List<BrukerSampleResultSentInfo>();
            List<CustomerInfo> customer = new List<CustomerInfo>();
            List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
            List<SampleResultInfo> checkResult = new List<SampleResultInfo>();
            List<APITokenInfo> apiToken = new List<APITokenInfo>();
            List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
            List<String> analysesCodes = new List<string>();
            List<AnalyzerPendingSampleInfo> analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
            List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
            List<AnalyzerMaterialInfo> analyzerMaterial = new List<AnalyzerMaterialInfo>();
            List<AnalyzerPendingSampleInfo> pendingSample = new List<AnalyzerPendingSampleInfo>();
            List<AnalysesMaterialInfo> analysesMaterialList = new List<AnalysesMaterialInfo>();
            List<PNECreditInfo> pneCredits = new List<PNECreditInfo>();
            List<APILogInfo> apiLog = new List<APILogInfo>();
            List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
            List<SampleFurtherAnalysisInfo> sampleFurtherAnalysiss = new List<SampleFurtherAnalysisInfo>();
            SampleFurtherAnalysisInfo sampleFurtherAnalysis = new SampleFurtherAnalysisInfo();
            AIMMessageInfo aimMessage = new AIMMessageInfo();
            AnalysisRequestParameterInfo newAnalysisRequest = new AnalysisRequestParameterInfo();
            APIPendingAIMMaterialInfo apiPendingAIMMaterial = new APIPendingAIMMaterialInfo();
            AnalyzerPendingSampleInfo analyzerPending = new AnalyzerPendingSampleInfo();
            AnalysisRequestInfo analysisRequest = new AnalysisRequestInfo();
            BrukerSampleResultSentInfo brukerSampleResultSent = new BrukerSampleResultSentInfo();
            BrukerSpectrumSentInfo brukerSpectrumSent = new BrukerSpectrumSentInfo();
            BrukerSampleInfo brukerSample = new BrukerSampleInfo();
            AIMMaterialInfo aimMaterial = new AIMMaterialInfo();
            FilesCountInfo filesCount = new FilesCountInfo();
            SampleInfo sample = new SampleInfo();
            APICreditInfo apiCredit = new APICreditInfo();
            PNECreditInfo pneCredit = new PNECreditInfo();
            SampleResultInfo sampleResult = new SampleResultInfo();
            SettingInfo setting = new SettingInfo();
            FilesInfo files = new FilesInfo();
            ErrorInfo error = new ErrorInfo();
            List<InstrumentInfo> instrument = new List<InstrumentInfo>();               //DB 230516
            Int32 sampleID = 0;
            Int32 customerID = 0;
            Int32 aimMaterialID = 0;
            String argument = String.Empty;
            String sql = String.Empty;
            String fileName = String.Empty;
            String sampleFile = String.Empty;
            String resultID = String.Empty;
            String aimApiKey = String.Empty;
            String token = String.Empty;
            Boolean isError = false;
            Boolean isFirst = true;
            Boolean blnExistsOnLog = false;
            Int32 index = 0;
            Int32 brukerSampleID = 0;
            Boolean isPNEDown = false;

            try
            {
                Library.WriteErrorLogFile("Start get sample from api");
                //Utility.LoadConnection(ref setting);
                Library.LoadSetting(ref setting, Utility.SettingXML);
                aimApiKey = setting.AIMAPIKey;
                CFCAPIManager.Setting = setting;
                BrukerManager.IsMessageDisplay = false;

                if (setting.IsPNETestUrl == true)
                {
                    APIManager.PNEAPIUrl = setting.PNEAPITestUrl;
                }
                else
                {
                    APIManager.PNEAPIUrl = setting.PNEAPIUrl;
                }

                apiToken = CFCAPIManager.GetToken(aimApiKey);

                if (apiToken == null)
                {
                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - apiToken=null");
                }
                else
                {
                    Library.WriteErrorLogFile("Token " + apiToken[0].Token);
                    //MW 220913 databases = new List<SQLDatabaseInfo>();
                    isError = true;
                    SQLDatabaseConnection.LoadConnection(ref databases, true, _databaseXML);

                    if (databases.Count > 0)
                    {
                        for (Int32 i = 0; i < databases.Count; i++)
                        {
                            if (databases[i].ConnectName == "AIM")
                            {
                                index = i;
                                isError = false;
                            }
                        }
                    }
                    /////TODO MARKW ADD BACK
                    isPNEDown = true;

                    if (isError == true)
                    {
                        //AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "No database connection is setup");
                        Library.WriteErrorLogFile("No database connection is setup");
                    }
                    else
                    {
                        token = apiToken[0].Token;
                        customerID = apiToken[0].CustomerID;
                        Library.WriteErrorLogFile("CustomerID = " + customerID.ToString());

                        if (customerID > 0)
                        {
                            AIMManager.IsMultipleDatabase = true;
                            AIMManager.CreateConnection(databases[index].ServerName, databases[index].Database, databases[index].IsSQLAuthentication, databases[index].User, databases[index].Password);
                            instrument = CFCAPIManager.GetInstrument(token, customerID);

                            for (int i = 0; i < instrument.Count; i++)
                            {
                                APIManager.Token = instrument[i].PNEKey;
                                Library.WriteErrorLogFile("PNEKey = " + APIManager.Token);
                                /////TODO MARKW ADD BACK
                                //////////////////////////isPNEDown = AdisseoAPI.AnalyseCode(ref analysesMaterialList);

                                //////////////////////////if (isPNEDown == true)
                                //////////////////////////{
                                //////////////////////////    Library.WriteErrorLogFile("PNE Not working. PNEKey used = " + instrument[i].PNEKey.ToString());
                                //////////////////////////}
                                //////////////////////////else
                                //////////////////////////{
                                //////////////////////////    Library.WriteErrorLogFile("Get MaterialCode ");
                                    
                                //////////////////////////    isPNEDown = AdisseoAPI.MaterialCode();
                                //////////////////////////}
                            }

                            Library.WriteErrorLogFile("AimApiKey = " + aimApiKey);

                            brukerSampleList = CFCAPIManager.GetBrukerSampleNotSentResult(token, customerID);
                            customer = CFCAPIManager.GetCustomer(token, customerID);

                            if (customer == null)
                            {
                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - customer=null");
                            }
                            else
                            {
                                // DB230617 APIManager.Token = customer[0].PNEKey;

                                //------------------------------------------------------------------------------------------------------
                                //1) GET RESULTS FROM BRUKER AND PUT INTO SAMPLE TABLE 	
                                #region Section 1

                                Library.WriteErrorLogFile("GET RESULTS FROM BRUKER AND PUT INTO SAMPLE TABLE");
                                if (brukerSampleList == null)
                                {
                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - brukerSampleList=null");
                                }
                                else
                                {
                                    Library.WriteErrorLogFile("BrukerSampleList = " + brukerSampleList.Count.ToString());
                                    if (brukerSampleList.Count > 0)
                                    {
                                        foreach (BrukerSampleInfo item in brukerSampleList)
                                        {
                                            if (item == null)
                                            {
                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - item=null");
                                            }
                                            else
                                            {
                                                Library.WriteErrorLogFile("SampleID = " + item.ValueProductInfo1);
                                                Int32.TryParse(item.ValueProductInfo1, out sampleID);

                                                if (sampleID == 0)
                                                {
                                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample ID " + item.ValueProductInfo1 + " is not numeric and will be skip");
                                                    error = CFCAPIManager.PutBrukerSentLog(token, customerID, item.BrukerSampleID, true);

                                                    if (error.ErrorNumber > 0)
                                                    {
                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api BrukerSentLog - sampleID=0");
                                                    }
                                                }
                                                else
                                                {
                                                    sample = AIMManager.GetSample(sampleID);

                                                    if (sample == null)
                                                    {
                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "SampleID " + sampleID.ToString() + " is not found and will be Skip ");
                                                        error = CFCAPIManager.PutBrukerSentLog(token, customerID, item.BrukerSampleID, true);

                                                        if (error.ErrorNumber > 0)
                                                        {
                                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api BrukerSentLog - sample=null");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Library.WriteErrorLogFile("Sample Found ");
                                                        brukerSampleResult = CFCAPIManager.GetBrukerSampleResultNotSent(token, customerID, item.BrukerSampleID);
                                                        if (brukerSampleResult == null)
                                                        {
                                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api did not return sample result.");
                                                        }
                                                        else
                                                        {
                                                            AIMManager.InsertAPILog(sample.SampleID, item.BrukerSampleID, (Int32)DataSourceTypeEnum.Bruker);
                                                            Library.WriteErrorLogFile("BrukerSampleResult = " + brukerSampleResult.Count.ToString());
                                                            //Loop thru Bruker Samples
                                                            foreach (BrukerSampleResultInfo result in brukerSampleResult)
                                                            {
                                                                resultID = "CFCAPI" + result.BrukerSampleResultID.ToString();
                                                                checkResult = AIMManager.GetSampleResultForSQLResultID(sampleID, resultID);
                                                                if (checkResult.Count == 0)
                                                                {
                                                                    Library.WriteErrorLogFile("Add Result " + resultID);
                                                                    sampleResult.SampleID = sampleID;
                                                                    sampleResult.AnalyzerNutrientID = 0;
                                                                    sampleResult.CFCUserID = 0;
                                                                    sampleResult.AnalyzerNutrientCode = result.ComponentName;
                                                                    sampleResult.Unit = result.ComponentUnit;
                                                                    sampleResult.ResultValue = result.PredictionCalculated;
                                                                    sampleResult.DataSourceTypeID = (Int32)DataSourceTypeEnum.Bruker;
                                                                    sampleResult.AimDatabaseID = 0;
                                                                    sampleResult.SqlResultID = resultID;
                                                                    AIMManager.InsertSampleResult(sampleResult, null, 0);

                                                                    brukerSampleResultSent = new BrukerSampleResultSentInfo();
                                                                    brukerSampleResultSent.Token = token;
                                                                    brukerSampleResultSent.CustomerID = customerID;
                                                                    brukerSampleResultSent.BrukerSampleResultID = result.BrukerSampleResultID;
                                                                    CFCAPIManager.PutBrukerSampleResultSent(brukerSampleResultSent);
                                                                }
                                                                else
                                                                {
                                                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample " + sampleID.ToString() + "~" + resultID + " result Already in database.");
                                                                    error = CFCAPIManager.PutBrukerSentLog(token, customerID, item.BrukerSampleID, true);

                                                                    if (error.ErrorNumber > 0)
                                                                    {
                                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api BrukerSentLog - checkResult.Count>0");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        if (brukerSampleList.Count > 0)
                                        {
                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "End get bruker result data");
                                        }
                                    }
                                }


                                #endregion

                                //------------------------------------------------------------------------------------------------------
                                //2) SPECTRUM SECTION
                                #region Section 2

                                if (isPNEDown == false)
                                {
                                    Library.WriteErrorLogFile("SPECTRUM SECTION");
                                    isFirst = true;
//                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "Start get bruker spectrum data");
                                    Library.WriteErrorLogFile("GetBrukerSampleNotSentSpectrum start");
                                    brukerSampleList = CFCAPIManager.GetBrukerSampleNotSentSpectrum(token, customerID);
                                    Library.WriteErrorLogFile("GetBrukerSampleNotSentSpectrum end " + brukerSampleList.Count.ToString());
                                    if (brukerSampleList == null)
                                    {
                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSampleNotSentSpectrum Api is not working");
                                    }
                                    else
                                    {
                                        if (brukerSampleList.Count > 0)
                                        {
                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "Start get bruker result data");
                                            customer = CFCAPIManager.GetCustomer(token, customerID);
                                            Library.WriteErrorLogFile("GetCustomer " + customer.Count.ToString());

                                            if (customer == null)
                                            {
                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - customer=null");
                                            }
                                            else
                                            {
                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "Start send spectrum data");

                                                foreach (BrukerSampleInfo item in brukerSampleList)
                                                {
                                                    Int32.TryParse(item.ValueProductInfo1, out sampleID);
                                                    Library.WriteErrorLogFile("GetCustomer " + customer.Count.ToString());

                                                    if (sampleID == 0)
                                                    {
                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample ID " + item.ValueProductInfo1 + " is not numeric and will be skip");
                                                        error = CFCAPIManager.PutBrukerSentLog(token, customerID, item.BrukerSampleID, true);

                                                        if (error.ErrorNumber > 0)
                                                        {
                                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api BrukerSentLog");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Library.WriteErrorLogFile("GetSample " + sampleID.ToString());
                                                        sample = AIMManager.GetSample(sampleID);

                                                        if (sample == null)
                                                        {
                                                            Library.WriteErrorLogFile("GetSample is null ");
                                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample ID " + item.ValueProductInfo1 + " was not found in AIM and will be skip");

                                                            brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(token, item.BrukerSampleID);
                                                            if (brukerSpectrumFile is null)
                                                            {
                                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample ID " + item.ValueProductInfo1 + " does not have a spectrum file");
                                                            }
                                                            else if (brukerSpectrumFile.Count == 0)
                                                            {
                                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Sample ID " + item.ValueProductInfo1 + " does not have a spectrum file");
                                                            }
                                                            else
                                                            {
                                                                brukerSpectrumSent = new BrukerSpectrumSentInfo();
                                                                brukerSpectrumSent.Token = token;
                                                                brukerSpectrumSent.CustomerID = customerID;
                                                                brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[0].BrukerSpectrumID;
                                                                brukerSpectrumSent.IsSkip = 1;
                                                                CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);

                                                                //AIMManager.InsertAPISprectrumLog(sampleID, brukerSpectrumFile[0].BrukerSpectrumID, (Int32)EnumAPINIRType.Bruker);
                                                                AIMManager.InsertAPILog(sampleID, brukerSpectrumFile[0].BrukerSampleID, (Int32)DataSourceTypeEnum.Bruker);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            aimMaterialID = sample.AIMMaterialID;
                                                            aimMaterial = AIMManager.GetAIMMaterial(aimMaterialID, true);
                                                            Library.WriteErrorLogFile("GetAIMMaterial " + aimMaterialID.ToString());

                                                            if (aimMaterial == null)
                                                            {
                                                                Library.WriteErrorLogFile("aimMaterial == null ");
                                                                brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(token, item.BrukerSampleID);

                                                                if (brukerSpectrumFile == null)
                                                                {
                                                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum Api is not working");
                                                                }
                                                                else
                                                                {
                                                                    brukerSpectrumSent = new BrukerSpectrumSentInfo();
                                                                    brukerSpectrumSent.Token = token;
                                                                    brukerSpectrumSent.CustomerID = customerID;
                                                                    brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[0].BrukerSpectrumID;
                                                                    brukerSpectrumSent.IsSkip = 1;
                                                                    CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);

                                                                    //AIMManager.InsertAPISprectrumLog(sampleID, brukerSpectrumFile[0].BrukerSpectrumID, (Int32)EnumAPINIRType.Bruker);
                                                                    AIMManager.InsertAPILog(sampleID, brukerSpectrumFile[0].BrukerSampleID, (Int32)DataSourceTypeEnum.Bruker);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Library.WriteErrorLogFile("FurtherAnalysisID " + sample.FurtherAnalysisID.ToString() + " " + sample.AIMSupplierID.ToString());

                                                                if (sample.FurtherAnalysisID == (Int32)DataSourceTypeEnum.AdisseoAPI && aimMaterial.MaterialTypeID == (Int32)MaterialTypeEnum.Ingredient)
                                                                {
                                                                    if (isFirst == true)
                                                                    {
                                                                        AdisseoAPI.ValidateAnalysisCode();
                                                                        isFirst = false;
                                                                    }

                                                                    Library.WriteErrorLogFile("IsFirst ");
                                                                    if (isError == true)
                                                                    {
                                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "PNE Analyse and Material Api is not working");
                                                                    }
                                                                    else
                                                                    {
                                                                        Library.WriteErrorLogFile("GetBrukerSpectrumFile " + item.BrukerSampleID.ToString());
                                                                        brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(token, item.BrukerSampleID);

                                                                        if (brukerSpectrumFile == null)
                                                                        {
                                                                            Library.WriteErrorLogFile("brukerSpectrumFile == null ");
                                                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum Api is not working");
                                                                        }
                                                                        else
                                                                        {
                                                                            Library.WriteErrorLogFile("brukerSpectrumFile " + brukerSpectrumFile.Count.ToString());
                                                                            if (brukerSpectrumFile.Count > 0)
                                                                            {
                                                                                analyzerMaterial = AIMManager.GetAnalyzerMaterial(DataSourceTypeEnum.AdisseoAPI, sample.AIMMaterialID);
                                                                                Library.WriteErrorLogFile("GetAnalyzerMaterial " + analyzerMaterial.Count.ToString());

                                                                                if (analyzerMaterial.Count == 0)
                                                                                {
                                                                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum a cross reference needs to be set up for AdisseoAPI for " + sample.AIMMaterialCode);
                                                                                    apiPendingAIMMaterial = AIMManager.GetAPIPendingAIMMaterial(sample.AIMMaterialID, (Int32)DataSourceTypeEnum.AdisseoAPI, true);

                                                                                    if (apiPendingAIMMaterial is null)
                                                                                    {
                                                                                        apiPendingAIMMaterial = new APIPendingAIMMaterialInfo();
                                                                                        apiPendingAIMMaterial.APIPendingAIMMaterialID = 0;
                                                                                        apiPendingAIMMaterial.AIMMaterialID = sample.AIMMaterialID;
                                                                                        apiPendingAIMMaterial.DataSourceTypeID = (Int32)DataSourceTypeEnum.AdisseoAPI;
                                                                                        apiPendingAIMMaterial.IsActive = 1;
                                                                                        apiPendingAIMMaterial.CFCUserID = 0;
                                                                                        error = AIMManager.SaveAPIPendingAIMMaterial(ref apiPendingAIMMaterial);
                                                                                    }

                                                                                    for (int i = 0; i < brukerSpectrumFile.Count; i++)
                                                                                    {
                                                                                        brukerSpectrumSent = new BrukerSpectrumSentInfo();
                                                                                        brukerSpectrumSent.Token = token;
                                                                                        brukerSpectrumSent.CustomerID = customerID;
                                                                                        brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[i].BrukerSpectrumID;
                                                                                        brukerSpectrumSent.IsSkip = 1;
                                                                                        CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);

                                                                                        //AIMManager.InsertAPISprectrumLog(sampleID, brukerSpectrumFile[i].BrukerSpectrumID, (Int32)EnumAPINIRType.Bruker);
                                                                                        AIMManager.InsertAPILog(sampleID, brukerSpectrumFile[0].BrukerSampleID, (Int32)DataSourceTypeEnum.Bruker);
                                                                                    }

                                                                                }
                                                                                else
                                                                                {
                                                                                    analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)DataSourceTypeEnum.AdisseoAPIAnalyseCode, true);
                                                                                    Library.WriteErrorLogFile("GetAnalyzerNutrient " + analyzerNutrient.Count.ToString());

                                                                                    if (analyzerNutrient.Count == 0)
                                                                                    {
                                                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Analyzer Nutrient API did not return ID.  ID = " + item.BrukerSampleID.ToString());
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Library.WriteErrorLogFile("Load analysesCodes = " + analyzerNutrient.Count.ToString());
                                                                                        analysesCodes = new List<string>();
                                                                                        for (int i = 0; i < analyzerNutrient.Count; i++)
                                                                                        {
                                                                                            //if (analyzerNutrient[i].AnalyzerNutrientCode != "STTP")
                                                                                            //{
                                                                                            analysesCodes.Add(analyzerNutrient[i].AnalyzerNutrientCode);
                                                                                            //}
                                                                                        }

                                                                                        Library.WriteErrorLogFile("Create analysis rec ");
                                                                                        newAnalysisRequest.materialCode = analyzerMaterial[0].AnalyzerMaterialCode.Trim();  // "COR";
                                                                                        Library.WriteErrorLogFile("newAnalysisRequest.materialCode = " + analyzerMaterial[0].AnalyzerMaterialCode.Trim());
                                                                                        newAnalysisRequest.analysesCodes = analysesCodes;
                                                                                        Library.WriteErrorLogFile("newAnalysisRequest.analysesCodes = " + analysesCodes.Count.ToString());
                                                                                        newAnalysisRequest.sampleCode = sample.SampleID.ToString();
                                                                                        Library.WriteErrorLogFile("newAnalysisRequest.sampleCode = " + sample.SampleID.ToString());
                                                                                        //newAnalysisRequest.description = sample.SampleCode;

                                                                                        Library.WriteErrorLogFile("newAnalysisRequest.description = " + sample.SampleCode);
                                                                                        newAnalysisRequest.spectraFileName = item.FileName;
                                                                                        Library.WriteErrorLogFile("newAnalysisRequest.spectraFileName = " + item.FileName);

                                                                                        if (sample.IsGround == true)
                                                                                        {
                                                                                            newAnalysisRequest.samplePresentation = "ground";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            newAnalysisRequest.samplePresentation = "whole";
                                                                                        }

                                                                                        // convert file byte to base 64
                                                                                        newAnalysisRequest.spectraFile = Convert.ToBase64String(brukerSpectrumFile[0].BrukerSpectrum);

                                                                                        APIManager.Token = brukerSpectrumFile[0].PNEKey;                //DB 230517
                                                                                                                                                        //APIManager.Token = "0cf3d0aa957c670ef0cc2ac4e7453e6f";			//mw remove
                                                                                        Library.WriteErrorLogFile("newAnalysisRequest.spectraFile ");
                                                                                        analysisRequest = APIManager.CreateAnalysisRequest(newAnalysisRequest);
                                                                                        Library.WriteErrorLogFile("CreateAnalysisRequest ");

                                                                                        if (analysisRequest.Id == null)
                                                                                        {
                                                                                            Library.WriteErrorLogFile("analysisRequest id is null ");
                                                                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum API did not return ID.  ID = " + item.BrukerSampleID.ToString());

                                                                                            brukerSpectrumSent = new BrukerSpectrumSentInfo();
                                                                                            brukerSpectrumSent.Token = token;
                                                                                            brukerSpectrumSent.CustomerID = customerID;
                                                                                            brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[0].BrukerSpectrumID;
                                                                                            brukerSpectrumSent.IsSkip = 1;
                                                                                            CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);

                                                                                            //AIMManager.InsertAPISprectrumLog(sampleID, brukerSpectrumFile[0].BrukerSpectrumID, (Int32)EnumAPINIRType.Bruker);
                                                                                            AIMManager.InsertAPILog(sampleID, brukerSpectrumFile[0].BrukerSampleID, (Int32)DataSourceTypeEnum.Bruker);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Library.WriteErrorLogFile("analysisRequest id " + analysisRequest.Id);
                                                                                            analyzerPending = new AnalyzerPendingSampleInfo();
                                                                                            analyzerPending.SampleID = sampleID;
                                                                                            analyzerPending.RequestCode = analysisRequest.Id;
                                                                                            analyzerPending.SentStamp = DateTime.Now;
                                                                                            Library.WriteErrorLogFile("item.FileName " + item.FileName);
                                                                                            analyzerPending.SpectraName = item.FileName;
                                                                                            AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);
                                                                                            Library.WriteErrorLogFile("SaveAnalyzerPendingSample " + item.FileName);

                                                                                            brukerSpectrumSent = new BrukerSpectrumSentInfo();
                                                                                            brukerSpectrumSent.Token = token;
                                                                                            brukerSpectrumSent.CustomerID = customerID;
                                                                                            brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[0].BrukerSpectrumID;
                                                                                            brukerSpectrumSent.IsSkip = 0;
                                                                                            CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);
                                                                                            Library.WriteErrorLogFile("PutBrukerSpectrumSent ");

                                                                                            //AIMManager.InsertAPISprectrumLog(sampleID, brukerSpectrumFile[0].BrukerSpectrumID, (Int32)EnumAPINIRType.Bruker);
                                                                                            AIMManager.InsertAPILog(sampleID, brukerSpectrumFile[0].BrukerSampleID, (Int32)DataSourceTypeEnum.Bruker);
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum API row missing.  ID = " + item.BrukerSampleID.ToString());
                                                                            }
                                                                        }

                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "No further analysis");
                                                                    //Get BrukerSpectrumID so it can be skip
                                                                    brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(token, item.BrukerSampleID);
                                                                    if (brukerSpectrumFile == null)
                                                                    {
                                                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum Api is not working");
                                                                    }
                                                                    else
                                                                    {
                                                                        for (int i = 0; i < brukerSpectrumFile.Count; i++)
                                                                        {
                                                                            brukerSpectrumSent = new BrukerSpectrumSentInfo();
                                                                            brukerSpectrumSent.Token = token;
                                                                            brukerSpectrumSent.CustomerID = customerID;
                                                                            brukerSpectrumSent.BrukerSpectrumID = brukerSpectrumFile[i].BrukerSpectrumID;
                                                                            brukerSpectrumSent.IsSkip = 1;
                                                                            error = CFCAPIManager.PutBrukerSpectrumSent(brukerSpectrumSent);

                                                                            //AIMManager.InsertAPISprectrumLog(sampleID, brukerSpectrumFile[i].BrukerSpectrumID, (Int32)EnumAPINIRType.Bruker);
                                                                            AIMManager.InsertAPILog(sampleID, brukerSpectrumFile[0].BrukerSampleID, (Int32)DataSourceTypeEnum.Bruker);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "End send spectrum data");
                                            }
                                        }
                                    }
                                }
                                #endregion

                                //------------------------------------------------------------------------------------------
                                //3) RE-RUN SAMPLE FROM AIM SAMPLE SCREEN
                                #region Section 3

                                if (isPNEDown == false)
                                {
                                    Library.WriteErrorLogFile("RE-RUN SAMPLE FROM AIM SAMPLE SCREEN");
                                    sampleFurtherAnalysiss = AIMManager.GetSampleFurtherAnalysis();
                                    if (sampleFurtherAnalysiss.Count > 0)
                                    {

                                        analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)DataSourceTypeEnum.AdisseoAPIAnalyseCode, true);
                                        Library.WriteErrorLogFile("GetAnalyzerNutrient " + analyzerNutrient.Count.ToString());

                                        if (analyzerNutrient.Count == 0)
                                        {
                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Analyzer Nutrient API did not return ID.  DataSource = " + DataSourceTypeEnum.AdisseoAPIAnalyseCode);
                                        }
                                        else
                                        {
                                            Library.WriteErrorLogFile("Further Analysis");
                                            foreach (SampleFurtherAnalysisInfo item in sampleFurtherAnalysiss)
                                            {
                                                //Gets the last record in the log for the sampleid/datasourcetypeID
                                                apiLog = new List<APILogInfo>();
                                                //apiSprectrumLog = new List<APISprectrumLogInfo>();
                                                blnExistsOnLog = false;
                                                Library.WriteErrorLogFile("Further Analysis Loop1");

                                                apiLog = AIMManager.GetAPILog(item.SampleID, (Int32)DataSourceTypeEnum.Bruker);
                                                //apiSprectrumLog = AIMManager.GetAPISprectrumLog(item.SampleID, EnumAPINIRType.Bruker);

                                                if (apiLog.Count > 0)
                                                {
                                                    blnExistsOnLog = true;
                                                    brukerSampleID = apiLog[0].APISampleID;
                                                }
                                                else
                                                {
                                                    Library.WriteErrorLogFile("Rerun sample ");
                                                    //if apilog null, go to API(using sampleID) and get the BrukerSampleID
                                                    List<BrukerSampleInfo> bruker = new List<BrukerSampleInfo>();
                                                    bruker = CFCAPIManager.GetBrukerSample(token, customerID, item.SampleID.ToString());

                                                    //if still not there, skip and go to next record
                                                    if (bruker.Count > 0)
                                                    {
                                                        if (bruker[0].ValueProductInfo1 == item.SampleID.ToString())
                                                        {
                                                            blnExistsOnLog = true;
                                                            brukerSampleID = bruker[0].BrukerSampleID;
                                                        }
                                                        else
                                                        {
                                                            error = AIMManager.InsertAIMMessage(4, "Spectrum file sample id does not match AIM sample id", 0);
                                                            sampleFurtherAnalysis = item;
                                                            sampleFurtherAnalysis.ProcessDate = DateTime.Now;
                                                            sampleFurtherAnalysis.AIMMessageID = error.RecordID;
                                                            sampleFurtherAnalysis.CFCUserID = 0;
                                                            AIMManager.SaveSampleFurtherAnalysis(ref sampleFurtherAnalysis);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        error = AIMManager.InsertAIMMessage(4, "There is no spectrum file for this sample", 0);
                                                        sampleFurtherAnalysis = item;
                                                        sampleFurtherAnalysis.ProcessDate = DateTime.Now;
                                                        sampleFurtherAnalysis.AIMMessageID = error.RecordID;
                                                        sampleFurtherAnalysis.CFCUserID = 0;
                                                        AIMManager.SaveSampleFurtherAnalysis(ref sampleFurtherAnalysis);
                                                    }
                                                }

                                                if (blnExistsOnLog == true)                //no spectrum so can't rerun
                                                {
                                                    //Get the samplecode
                                                    sample = AIMManager.GetSample(item.SampleID);

                                                    if (sample == null)
                                                    {
                                                        error = AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "SampleID does not exists. SampleID = " + item.SampleID.ToString());
                                                        sampleFurtherAnalysis = item;
                                                        sampleFurtherAnalysis.ProcessDate = DateTime.Now;
                                                        sampleFurtherAnalysis.AIMMessageID = error.RecordID;
                                                        sampleFurtherAnalysis.CFCUserID = 0;
                                                        AIMManager.SaveSampleFurtherAnalysis(ref sampleFurtherAnalysis);
                                                    }
                                                    else
                                                    {
                                                        Library.WriteErrorLogFile("Further Analysis Loop2");
                                                        //Get analyzer material
                                                        analyzerMaterial = AIMManager.GetAnalyzerMaterial(DataSourceTypeEnum.AdisseoAPI, sample.AIMMaterialID);

                                                        if (analyzerMaterial.Count == 0)
                                                        {
                                                            apiPendingAIMMaterial = AIMManager.GetAPIPendingAIMMaterial(sample.AIMMaterialID, (Int32)DataSourceTypeEnum.AdisseoAPI, true);
                                                            error = AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum a cross reference needs to be set up for AdisseoAPI for " + sample.AIMMaterialCode);
                                                            sampleFurtherAnalysis = item;
                                                            sampleFurtherAnalysis.ProcessDate = DateTime.Now;
                                                            sampleFurtherAnalysis.AIMMessageID = error.RecordID;
                                                            sampleFurtherAnalysis.CFCUserID = 0;
                                                            AIMManager.SaveSampleFurtherAnalysis(ref sampleFurtherAnalysis);

                                                            if (apiPendingAIMMaterial is null)
                                                            {
                                                                apiPendingAIMMaterial = new APIPendingAIMMaterialInfo();
                                                                apiPendingAIMMaterial.APIPendingAIMMaterialID = 0;
                                                                apiPendingAIMMaterial.AIMMaterialID = sample.AIMMaterialID;
                                                                apiPendingAIMMaterial.DataSourceTypeID = (Int32)DataSourceTypeEnum.AdisseoAPI;
                                                                apiPendingAIMMaterial.IsActive = 1;
                                                                apiPendingAIMMaterial.CFCUserID = 0;
                                                                error = AIMManager.SaveAPIPendingAIMMaterial(ref apiPendingAIMMaterial);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Library.WriteErrorLogFile("Load analysesCodes = " + analyzerNutrient.Count.ToString());
                                                            analysesCodes = new List<string>();

                                                            for (int i = 0; i < analysesMaterialList.Count; i++)
                                                            {
                                                                if (analyzerMaterial[0].AnalyzerMaterialCode.Trim() == analysesMaterialList[i].MaterialCodes)
                                                                {
                                                                    analysesCodes.Add(analysesMaterialList[i].AnalysesCode);
                                                                }
                                                            }

                                                            Library.WriteErrorLogFile("Further Analysis Loop3");
                                                            //Get spectrum file
                                                            brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(token, brukerSampleID);

                                                            if (brukerSpectrumFile != null)
                                                            {
                                                                if (brukerSpectrumFile.Count > 0)
                                                                {
                                                                    //Get File Name  
                                                                    brukerSampleList = CFCAPIManager.GetBrukerSample(token, customerID, brukerSpectrumFile[0].BrukerSampleID);
                                                                    Library.WriteErrorLogFile("Further Analysis Loop4 " + brukerSpectrumFile[0].BrukerSampleID);
                                                                    newAnalysisRequest.materialCode = analyzerMaterial[0].AnalyzerMaterialCode.Trim();
                                                                    newAnalysisRequest.analysesCodes = analysesCodes;
                                                                    newAnalysisRequest.sampleCode = sample.SampleID.ToString();
                                                                    //remove mw230530																newAnalysisRequest.description = sample.SampleCode;
                                                                    newAnalysisRequest.spectraFileName = brukerSampleList[0].FileName;

                                                                    if (sample.IsGround == true)
                                                                    {
                                                                        newAnalysisRequest.samplePresentation = "ground";
                                                                    }
                                                                    else
                                                                    {
                                                                        newAnalysisRequest.samplePresentation = "whole";
                                                                    }

                                                                    newAnalysisRequest.spectraFile = Convert.ToBase64String(brukerSpectrumFile[0].BrukerSpectrum);
                                                                    APIManager.Token = brukerSpectrumFile[0].PNEKey;            //DB 230517 
                                                                    analysisRequest = APIManager.CreateAnalysisRequest(newAnalysisRequest);
                                                                    sampleFurtherAnalysis = item;
                                                                    sampleFurtherAnalysis.ProcessDate = DateTime.Now;
                                                                    sampleFurtherAnalysis.AIMMessageID = 0;
                                                                    sampleFurtherAnalysis.CFCUserID = 0;

                                                                    if (analysisRequest.Id == null)
                                                                    {
                                                                        error = AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "PNE API call did not come back with an ID.  SampleID = " + sample.SampleID.ToString());
                                                                        Library.WriteErrorLogFile("PNE did not return and ID for sampleid " + sample.SampleID.ToString());
                                                                        sampleFurtherAnalysis.AIMMessageID = error.RecordID;

                                                                    }
                                                                    else
                                                                    {
                                                                        error = new ErrorInfo();
                                                                        Library.WriteErrorLogFile("Further Analysis Loop5 " + analysisRequest.Id);
                                                                        analyzerPending = new AnalyzerPendingSampleInfo();
                                                                        analyzerPending.SampleID = sample.SampleID;
                                                                        analyzerPending.RequestCode = analysisRequest.Id;
                                                                        analyzerPending.SentStamp = DateTime.Now;
                                                                        analyzerPending.SpectraName = brukerSampleList[0].FileName;
                                                                        AIMManager.SaveAnalyzerPendingSample(ref analyzerPending);
                                                                    }

                                                                    Library.WriteErrorLogFile("Further Analysis Loop6 ");
                                                                    AIMManager.SaveSampleFurtherAnalysis(ref sampleFurtherAnalysis);
                                                                    Library.WriteErrorLogFile("Further Analysis Loop7 ");
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion


                                //------------------------------------------------------------------------------------------
                                //4) GET PNE RESULTS	
                                #region Section 4

                                if (isPNEDown == false)
                                {
                                    Library.WriteErrorLogFile("GET PNE RESULTS");

                                    if (token.Length > 0)
                                    {
                                        apiToken = CFCAPIManager.GetToken(aimApiKey);

                                        if (apiToken == null)
                                        {
                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - apiToken=null");
                                        }
                                        else
                                        {
                                            customerID = apiToken[0].CustomerID;
                                            instrument = CFCAPIManager.GetInstrument(token, customerID);

                                            if (instrument == null)
                                            {
                                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - instrument=null");
                                            }
                                            else
                                            {
                                                for (int i = 0; i < instrument.Count; i++)
                                                {
                                                    APIManager.Token = instrument[i].PNEKey;
                                                    AdisseoAPI.GettingRequest();
                                                }
                                            }

                                        }
                                    }
                                }
                                //AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "End further analyses");
                                #endregion

                                //------------------------------------------------------------------------------------------
                                //5) GET PNE Credits	
                                #region Section 5

                                if (isPNEDown == false)
                                {
                                    Library.WriteErrorLogFile("GET PNE Credit");

                                    customerID = apiToken[0].CustomerID;
                                    instrument = CFCAPIManager.GetInstrument(token, customerID);
                                    pneCredits = AIMManager.GetPNECredit();

                                    if (instrument != null)
                                    {
                                        for (int i = 0; i < instrument.Count; i++)
                                        {
                                            APIManager.Token = instrument[i].PNEKey;
                                            apiCredit = AdisseoAPI.GetPNECredit();

                                            if (apiCredit != null)
                                            {
                                                if (pneCredits.Count == 0)
                                                {
                                                    pneCredit = new PNECreditInfo();
                                                    pneCredit.PNECreditID = 0;
                                                    pneCredit.PNEKey = instrument[i].PNEKey;
                                                    pneCredit.PNEDescription = String.Empty;
                                                    pneCredit.CFCUserID = 0;
                                                }
                                                else
                                                {
                                                    for (int c = 0; c < pneCredits.Count; c++)
                                                    {
                                                        if (pneCredits[c].PNEKey == instrument[i].PNEKey)
                                                        {
                                                            pneCredit = pneCredits[c];
                                                            break;
                                                        }
                                                    }                                                    
                                                }

                                                pneCredit.Credit = apiCredit.credits;
                                                pneCredit.CFCUserID = 0;
                                                error = AIMManager.SavePNECredit(ref pneCredit);

                                                if (error.ErrorNumber > 0)
                                                {
                                                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Save of PNE Credit Information");
                                                }
                                            }
                                        }
                                    }
                                }
                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Info, "End further analyses");
                                #endregion
                            }
                        }
                    }
                    Library.WriteErrorLogFile("End get sample from api");
                }
            }
            catch (Exception ex)
            {
                StackTrace stackTrace = new StackTrace(ex, true);
                StackFrame stackFrame = new StackFrame(0);
                Int32 line = stackFrame.GetFileLineNumber();
                ApplicationLog.WriteLog("***Line**** Line = " + line.ToString() + " Message = " + ex.Message, EventLogEntryType.Error);
            }
        }

        public static void CheckSampleToAPI()
        {
            try
            {
                List<BrukerSampleInfo> brukerSampleList = new List<BrukerSampleInfo>();
                List<BrukerSampleInfo> brukerSample = new List<BrukerSampleInfo>();
                List<BrukerSampleResultInfo> brukerSampleResultAPI = new List<BrukerSampleResultInfo>();
                List<BrukerSampleResultInfo> brukerSampleResult = new List<BrukerSampleResultInfo>();
                BrukerFileListInfo brukerSpectrumFile = new BrukerFileListInfo();
                List<BrukerSpectrumInfo> brukerSpectrumFileAPI = new List<BrukerSpectrumInfo>();
                List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
                List<APITokenInfo> apiToken = new List<APITokenInfo>();

                //7/22/22
                List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
                BrukerCFCLogResultInfo brukerCFCLog = new BrukerCFCLogResultInfo();
                BrukerFileListInfo brukerFileList = new BrukerFileListInfo();
                FilesCountInfo filesCount = new FilesCountInfo();
                TokenInfo tokenKey = new TokenInfo();
                SettingInfo setting = new SettingInfo();
                SampleInfo sample = new SampleInfo();
                ErrorInfo error = new ErrorInfo();
                FilesInfo file = new FilesInfo();
                BatchInfo batch = new BatchInfo();
                String cfcAIMWorkerFile = String.Empty;
                String fileName = String.Empty;
                String argument = String.Empty;
                String aimApiKey = String.Empty;
                String token = String.Empty;
                String sql = String.Empty;
                String str = String.Empty;
                String errorMessage = String.Empty;
                Int32 customerID = 0;

                IsCancel = false;
                //Utility.LoadConnection(ref setting);
                Library.LoadSetting(ref setting, Utility.SettingXML);

                Library.WriteErrorLogFile("Start check bruker data");

                aimApiKey = setting.AIMAPIKey;
                CFCAPIManager.Setting = setting;

                BrukerManager.IsMessageDisplay = false;

                SQLDatabaseConnection.LoadConnection(ref databases, true, _databaseXML);

                BrukerManager.CreateBrukerConnection(databases[0].ServerName, databases[0].Database, databases[0].IsSQLAuthentication, databases[0].User, databases[0].Password);

                if (UpdateTableStpToCurrent() == false)
                {
                    Library.WriteErrorLogFile("Bruker database does not have up-to-date AIM tables.");
                }
                else
                {
                    // Bruker Spectrum
                    brukerSampleList = BrukerManager.GetBrukerSampleAll();

                    if (brukerSampleList.Count > 0)
                    {
                        apiToken = CFCAPIManager.GetToken(aimApiKey);

                        if (apiToken == null)
                        {
                            Library.WriteErrorLogFile("Batch api is not working.");
                        }
                        else
                        {
                            CurrentCount = 0;
                            TotalCount = brukerSampleList.Count;
                            token = apiToken[0].Token;
                            customerID = apiToken[0].CustomerID;

                            Library.WriteErrorLogFile("***************** Missing Bruker Check ***********************");
                            foreach (BrukerSampleInfo item in brukerSampleList)
                            {
                                CurrentCount += 1;

                                if (IsCancel == true)
                                {
                                    return;
                                }

                                brukerSample = CFCAPIManager.GetBrukerSample(token, customerID, item.Path, item.FileName);

                                if (brukerSample.Count == 0)
                                {
                                    Bruker.Path.Add(item.Path);
                                    Bruker.FileName.Add(item.FileName);
                                    Library.WriteErrorLogFile("Missing header record FileName = '" + item.FileName.ToString() + "', Created on " + item.CreateStamp.ToString());
                                }
                                else
                                {
                                    brukerSampleResult = BrukerManager.GetBrukerSampleResult(item.Path, item.FileName);

                                    if (brukerSampleResult.Count > 0)
                                    {
                                        brukerSampleResultAPI = CFCAPIManager.GetBrukerSampleResult(token, brukerSample[0].BrukerSampleID);

                                        if (brukerSampleResultAPI.Count == 0)
                                        {
                                            Bruker.Path.Add(item.Path);
                                            Bruker.FileName.Add(item.FileName);
                                            Library.WriteErrorLogFile("Missing result record API Count = " + brukerSampleResultAPI.Count.ToString() + ", Bruker Count = " + brukerSampleResult.Count.ToString());
                                        }
                                    }

                                    brukerSpectrumFile = BrukerManager.GetBrukerFileList(item.Path, item.FileName);

                                    if (brukerSpectrumFile != null)
                                    {
                                        brukerSpectrumFileAPI = CFCAPIManager.GetBrukerSpectrumFile(token, brukerSample[0].BrukerSampleID);

                                        if (brukerSpectrumFileAPI.Count == 0)
                                        {
                                            Bruker.Path.Add(item.Path);
                                            Bruker.FileName.Add(item.FileName);
                                            Library.WriteErrorLogFile("Missing Spectrum record file name = '" + item.FileName + "' ");
                                        }
                                    }
                                }
                            }
                        }

                    }

                    if (token != String.Empty)
                    {
                        batch.Token = token;
                        tokenKey.Token = token;
                        error = CFCAPIManager.PutBatchEnd(batch);
                        error = CFCAPIManager.PutTokenEnd(tokenKey);
                    }
                }

                Library.WriteErrorLogFile("Finish check sample to API");
            }
            catch (Exception ex)
            {
                Library.WriteErrorLogFile("***Error*****" + ex.Message);
            }
        }

        public static void CheckSampleFromAPI()
        {
            try
            {
                List<BrukerSampleInfo> brukerSampleList = new List<BrukerSampleInfo>();
                List<BrukerSampleInfo> brukerSample = new List<BrukerSampleInfo>();
                List<BrukerSampleResultInfo> brukerSampleResultAPI = new List<BrukerSampleResultInfo>();
                List<SampleResultInfo> sampleResult = new List<SampleResultInfo>();
                BrukerFileListInfo brukerSpectrumFile = new BrukerFileListInfo();
                List<BrukerSpectrumInfo> brukerSpectrumFileAPI = new List<BrukerSpectrumInfo>();
                List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
                List<APITokenInfo> apiToken = new List<APITokenInfo>();

                //7/22/22
                List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
                BrukerCFCLogResultInfo brukerCFCLog = new BrukerCFCLogResultInfo();
                BrukerFileListInfo brukerFileList = new BrukerFileListInfo();
                FilesCountInfo filesCount = new FilesCountInfo();
                TokenInfo tokenKey = new TokenInfo();
                SettingInfo setting = new SettingInfo();
                SampleInfo sample = new SampleInfo();
                ErrorInfo error = new ErrorInfo();
                FilesInfo file = new FilesInfo();
                BatchInfo batch = new BatchInfo();

                Boolean isError = true;
                Boolean isFound = true;
                String cfcAIMWorkerFile = String.Empty;
                String fileName = String.Empty;
                String argument = String.Empty;
                String aimApiKey = String.Empty;
                String token = String.Empty;
                String sql = String.Empty;
                String str = String.Empty;
                String errorMessage = String.Empty;
                Int32 customerID = 0;
                Int32 index = 0;
                Int32 sampleID = 0;

                //				Utility.LoadConnection(ref setting);
                Library.LoadSetting(ref setting, Utility.SettingXML);
                Library.WriteErrorLogFile("Start check bruker data");

                aimApiKey = setting.AIMAPIKey;
                CFCAPIManager.Setting = setting;
                BrukerManager.IsMessageDisplay = false;
                SQLDatabaseConnection.LoadConnection(ref databases, true, _databaseXML);

                if (databases.Count > 0)
                {
                    for (Int32 i = 0; i < databases.Count; i++)
                    {
                        if (databases[i].ConnectName == "AIM")
                        {
                            index = i;
                            isError = false;
                        }
                    }
                }

                if (isError == true)
                {
                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "No database connection is setup");
                }
                else
                {
                    AIMManager.IsMultipleDatabase = true;
                    AIMManager.CreateConnection(databases[index].ServerName, databases[index].Database, databases[index].IsSQLAuthentication, databases[index].User, databases[index].Password);

                    apiToken = CFCAPIManager.GetToken(aimApiKey);

                    if (apiToken == null)
                    {
                        Library.WriteErrorLogFile("Batch api is not working.");
                    }
                    else
                    {
                        Library.WriteErrorLogFile("***************** Missing API Bruker Check ***********************");
                        ClearBrukerSampleResultID();
                        token = apiToken[0].Token;
                        customerID = apiToken[0].CustomerID;
                        CurrentCount = 0;

                        brukerSampleList = CFCAPIManager.GetBrukerSample(token, customerID);
                        TotalCount = brukerSampleList.Count;

                        foreach (BrukerSampleInfo item in brukerSampleList)
                        {
                            CurrentCount += 1;

                            if (IsCancel == true)
                            {
                                return;
                            }

                            Int32.TryParse(item.ValueProductInfo1, out sampleID);

                            if (sampleID > 0)
                            {
                                sample = AIMManager.GetSample(sampleID);

                                if (sample == null)
                                {
                                    Library.WriteErrorLogFile("AIM missing SampleID = '" + sampleID.ToString() + "'");
                                }
                                else
                                {
                                    brukerSampleResultAPI = CFCAPIManager.GetBrukerSampleResult(token, item.BrukerSampleID);

                                    if (brukerSampleResultAPI.Count > 0)
                                    {
                                        sampleResult = AIMManager.GetAllSampleResult(sampleID);

                                        if (sampleResult.Count == 0)
                                        {
                                            BrukerSampleResultID.Add(item.BrukerSampleID);
                                            Library.WriteErrorLogFile("Missing AIM result record from API Count = " + brukerSampleResultAPI.Count.ToString() + ", AIM Count = " + sampleResult.Count.ToString() + ", SampleID = '" + sampleID.ToString() + "', BrukerSampleID = '" + item.BrukerSampleID.ToString() + "'");
                                        }
                                        else
                                        {
                                            errorMessage = String.Empty;
                                            foreach (BrukerSampleResultInfo itemResultAPI in brukerSampleResultAPI)
                                            {
                                                isFound = false;
                                                foreach (SampleResultInfo itemResult in sampleResult)
                                                {
                                                    if (itemResult.SqlResultID == "CFCAPI" + itemResultAPI.BrukerSampleResultID.ToString())
                                                    {
                                                        isFound = true;
                                                        break;
                                                    }
                                                }

                                                if (isFound == false)
                                                {
                                                    errorMessage += errorMessage + "Missing API result record SampleID = '" + sampleID.ToString() + "', BrukerSampleResultID = '" + itemResultAPI.BrukerSampleResultID.ToString() + "'" + Environment.NewLine;
                                                }
                                            }

                                            if (errorMessage.Length > 0)
                                            {
                                                Library.WriteErrorLogFile(errorMessage);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (token != String.Empty)
                    {
                        batch.Token = token;
                        tokenKey.Token = token;
                        error = CFCAPIManager.PutBatchEnd(batch);
                        error = CFCAPIManager.PutTokenEnd(tokenKey);
                    }
                }
                Library.WriteErrorLogFile("Finish check sample to API");
            }
            catch (Exception ex)
            {
                Library.WriteErrorLogFile("***Error*****" + ex.Message);
            }
        }

        public static void CheckPNEFromAPI()
        {
            try
            {
                //List<APISprectrumLogInfo> apiSprectrumLog = new List<APISprectrumLogInfo>();
                List<APILogInfo> apiLog = new List<APILogInfo>();
                List<BrukerSpectrumListInfo> brukerSpectrumList = new List<BrukerSpectrumListInfo>();
                List<APITokenInfo> apiToken = new List<APITokenInfo>();
                List<SampleInfo> sampleList = new List<SampleInfo>();

                List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
                FilesCountInfo filesCount = new FilesCountInfo();
                TokenInfo tokenKey = new TokenInfo();
                SettingInfo setting = new SettingInfo();
                SampleInfo sample = new SampleInfo();
                ErrorInfo error = new ErrorInfo();
                FilesInfo file = new FilesInfo();
                BatchInfo batch = new BatchInfo();

                Boolean isError = true;
                String cfcAIMWorkerFile = String.Empty;
                String fileName = String.Empty;
                String aimApiKey = String.Empty;
                String token = String.Empty;
                Int32 customerID = 0;
                Int32 index = 0;
                Int32 sampleID = 0;

                //Utility.LoadConnection(ref setting);
                Library.LoadSetting(ref setting, Utility.SettingXML);
                Library.WriteErrorLogFile("Start check bruker data");

                aimApiKey = setting.AIMAPIKey;
                CFCAPIManager.Setting = setting;
                BrukerManager.IsMessageDisplay = false;
                SQLDatabaseConnection.LoadConnection(ref databases, true, _databaseXML);

                if (databases.Count > 0)
                {
                    for (Int32 i = 0; i < databases.Count; i++)
                    {
                        if (databases[i].ConnectName == "AIM")
                        {
                            index = i;
                            isError = false;
                        }
                    }
                }

                if (isError == true)
                {
                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "No database connection is setup");
                }
                else
                {
                    AIMManager.IsMultipleDatabase = true;
                    AIMManager.CreateConnection(databases[index].ServerName, databases[index].Database, databases[index].IsSQLAuthentication, databases[index].User, databases[index].Password);

                    apiToken = CFCAPIManager.GetToken(aimApiKey);

                    if (apiToken == null)
                    {
                        Library.WriteErrorLogFile("Batch api is not working.");
                    }
                    else
                    {
                        Library.WriteErrorLogFile("***************** Missing PNE Bruker Check ***********************");
                        token = apiToken[0].Token;
                        customerID = apiToken[0].CustomerID;

                        brukerSpectrumList = CFCAPIManager.GetBrukerSpectrumFileList(token, customerID);
                        TotalCount = brukerSpectrumList.Count;

                        foreach (BrukerSpectrumListInfo item in brukerSpectrumList)
                        {
                            if (IsCancel == true)
                            {
                                return;
                            }

                            Int32.TryParse(item.ValueProductInfo1, out sampleID);

                            if (sampleID > 0)
                            {
                                sample = AIMManager.GetSample(sampleID);  //Check to see if is PNE

                                if (sample == null)
                                {
                                    Library.WriteErrorLogFile("AIM missing SampleID = '" + sampleID.ToString() + "'");
                                }
                                else
                                {
                                    //apiSprectrumLog = AIMManager.GetAPISprectrumLog(sampleID, item.BrukerSpectrumID);
                                    apiLog = AIMManager.GetAPILog(sampleID, (Int32)DataSourceTypeEnum.Bruker);

                                    if (apiLog.Count == 0)
                                    {
                                        //error = AIMManager.InsertAPISprectrumLog(sampleID, item.BrukerSpectrumID, (Int32)EnumAPINIRType.Bruker);
                                        error = AIMManager.InsertAPILog(sampleID, item.BrukerSampleID, (Int32)DataSourceTypeEnum.Bruker);
                                    }
                                }
                            }
                            CurrentCount += 1;
                        }
                    }

                    sampleList = AIMManager.GetSampleMissingFurtherAnalysis(11, MaterialTypeEnum.Ingredient, DateTime.Now.AddDays(-300));

                    foreach (SampleInfo item in sampleList)
                    {
                        SampleID.Add(item.SampleID);
                        Library.WriteErrorLogFile(item.SampleID.ToString());
                        //Library.WriteErrorLogFile("Missing PNE request.  SampleID = '" + item.SampleID.ToString() + "'");
                    }

                    sampleList = AIMManager.GetSampleMissingFurtherAnalysisResult(11, MaterialTypeEnum.Ingredient, DateTime.Now.AddDays(-300), "PNEAPI");

                    foreach (SampleInfo item in sampleList)
                    {
                        SampleID.Add(item.SampleID);
                        Library.WriteErrorLogFile(item.SampleID.ToString());
                        //Library.WriteErrorLogFile("Missing result from PNE request.  SampleID = '" + item.SampleID.ToString() + "'");
                    }

                    if (token != String.Empty)
                    {
                        batch.Token = token;
                        tokenKey.Token = token;
                        error = CFCAPIManager.PutBatchEnd(batch);
                        error = CFCAPIManager.PutTokenEnd(tokenKey);
                    }
                }
                Library.WriteErrorLogFile("Finish check sample to API");
            }
            catch (Exception ex)
            {
                Library.WriteErrorLogFile("***Error*****" + ex.Message);
            }
        }

        public static void FixMissingResult()
        {
            List<APITokenInfo> apiToken = new List<APITokenInfo>();
            TokenInfo tokenKey = new TokenInfo();
            ErrorInfo error;
            SettingInfo setting = new SettingInfo();
            String aimApiKey = String.Empty;
            String token = String.Empty;
            Int32 customerID = 0;

            try
            {
                //Utility.LoadConnection(ref setting);
                Library.LoadSetting(ref setting, Utility.SettingXML);
                Library.WriteErrorLogFile("Start Fix missing bruker result");

                aimApiKey = setting.AIMAPIKey;
                CFCAPIManager.Setting = setting;

                apiToken = CFCAPIManager.GetToken(aimApiKey);

                if (apiToken == null)
                {
                    Library.WriteErrorLogFile("CFC api is not working.");
                }
                else
                {
                    CurrentCount = 0;
                    token = apiToken[0].Token;
                    customerID = apiToken[0].CustomerID;
                    TotalCount = BrukerSampleResultID.Count;

                    foreach (Int32 brukerSampleResultID in BrukerSampleResultID)
                    {
                        CurrentCount += 1;
                        Library.WriteErrorLogFile("Bruker sample result will be sent.  BrukerSampleResultID = " + brukerSampleResultID);
                        CFCAPIManager.DeleteBrukerSampleResultSent(token, customerID, brukerSampleResultID);
                        CFCAPIManager.DeleteBrukerSentLog(token, customerID, brukerSampleResultID, true);
                    }

                    tokenKey.Token = token;
                    error = CFCAPIManager.PutTokenEnd(tokenKey);
                }

                Library.WriteErrorLogFile("Finish Fix missing bruker result");
            }
            catch (Exception ex)
            {
                Library.WriteErrorLogFile("***Error*****" + ex.Message);
            }
        }

        public static void FixMissingPNERequest()
        {
            SettingInfo setting = new SettingInfo();
            String aimApiKey = String.Empty;
            String token = String.Empty;

            try
            {
                //Utility.LoadConnection(ref setting);
                Library.LoadSetting(ref setting, Utility.SettingXML);
                Library.WriteErrorLogFile("Start Fix missing PNE request");

                CFCAPIManager.Setting = setting;

                CurrentCount = 0;
                TotalCount = Bruker.TotalCount;

                foreach (Int32 sampleID in SampleID)
                {
                    if (IsCancel == true)
                    {
                        return;
                    }

                    CurrentCount += 1;
                    AIMManager.SaveSampleFurtherAnalysis(0, sampleID, 11, 0);
                    Library.WriteErrorLogFile("PNE request created.  SampleID = " + sampleID);
                }

                Library.WriteErrorLogFile("Finish Fix missing PNE request");
            }
            catch (Exception ex)
            {
                Library.WriteErrorLogFile("***Error*****" + ex.Message);
            }
        }

        public static void FixMissingBruker()
        {
            SettingInfo setting = new SettingInfo();
            String aimApiKey = String.Empty;
            String token = String.Empty;

            try
            {
                //Utility.LoadConnection(ref setting);
                Library.LoadSetting(ref setting, Utility.SettingXML);
                Library.WriteErrorLogFile("Start Fix missing Bruker records");

                CFCAPIManager.Setting = setting;

                CurrentCount = 0;
                TotalCount = Bruker.FileName.Count;

                for (int i = 0; i < TotalCount; i++)
                {
                    if (IsCancel == true)
                    {
                        return;
                    }

                    CurrentCount += 1;
                    BrukerManager.DeleteCFCLogResult(Path[i], FileName[i]);
                    Library.WriteErrorLogFile("clear CFCLogResult.  Path = " + Path[i] + " and file name = " + FileName[i]);
                    BrukerManager.DeleteCFCLogSpectrum(Path[i], FileName[i]);
                    Library.WriteErrorLogFile("clear CFCLogSpectrum.  Path = " + Path[i] + " and file name = " + FileName[i]);

                }

                Library.WriteErrorLogFile("Finish Fix missing PNE request");
            }
            catch (Exception ex)
            {
                Library.WriteErrorLogFile("***Error*****" + ex.Message);
            }
        }

        public static void TestAPI(Int32 sampleID)
        {
            AnalysisRequestParameterInfo newAnalysisRequest = new AnalysisRequestParameterInfo();
            List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
            List<AnalysesMaterialInfo> analysesMaterialList = new List<AnalysesMaterialInfo>();
            List<AnalyzerMaterialInfo> analyzerMaterial = new List<AnalyzerMaterialInfo>();
            List<BrukerSpectrumInfo> brukerSpectrumFile = new List<BrukerSpectrumInfo>();
            List<BrukerSampleInfo> brukerSampleList = new List<BrukerSampleInfo>();
            List<InstrumentInfo> instrument = new List<InstrumentInfo>();
            List<SQLDatabaseInfo> databases = new List<SQLDatabaseInfo>();
            List<APITokenInfo> apiToken = new List<APITokenInfo>();
            List<APILogInfo> apiLog = new List<APILogInfo>();
            List<string> analysesCodes = new List<string>();

            APIPendingAIMMaterialInfo apiPendingAIMMaterial = new APIPendingAIMMaterialInfo();
            AnalysisRequestInfo analysisRequest = new AnalysisRequestInfo();
            SampleInfo sample = new SampleInfo();
            SettingInfo setting = new SettingInfo();
            ErrorInfo error = new ErrorInfo();
            Boolean blnExistsOnLog = false;
            Boolean isError = false;
            Boolean isPNEDown = false;
            Int32 brukerSampleID = 0;
            Int32 customerID = 0;
            Int32 index = 0;
            String token = String.Empty;
            String aimApiKey = String.Empty;

            Library.LoadSetting(ref setting, Utility.SettingXML);
            aimApiKey = setting.AIMAPIKey;
            CFCAPIManager.Setting = setting;
            BrukerManager.IsMessageDisplay = false;

            if (setting.IsPNETestUrl == true)
            {
                APIManager.PNEAPIUrl = setting.PNEAPITestUrl;
            }
            else
            {
                APIManager.PNEAPIUrl = setting.PNEAPIUrl;
            }

            apiToken = CFCAPIManager.GetToken(aimApiKey);

            if (apiToken == null)
            {
                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "CFC Tech Api is not working - apiToken=null");
            }
            else
            {
                Library.WriteErrorLogFile("Token " + apiToken[0].Token);
                isError = true;
                SQLDatabaseConnection.LoadConnection(ref databases, true, _databaseXML);

                if (databases.Count > 0)
                {
                    for (Int32 i = 0; i < databases.Count; i++)
                    {
                        if (databases[i].ConnectName == "AIM")
                        {
                            index = i;
                            isError = false;
                        }
                    }
                }

                if (isError == true)
                {
                    AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "No database connection is setup");
                }
                else
                {
                    token = apiToken[0].Token;
                    customerID = apiToken[0].CustomerID;

                    if (customerID > 0)
                    {
                        AIMManager.IsMultipleDatabase = true;
                        AIMManager.CreateConnection(databases[index].ServerName, databases[index].Database, databases[index].IsSQLAuthentication, databases[index].User, databases[index].Password);
                        instrument = CFCAPIManager.GetInstrument(token, customerID);

                        for (int i = 0; i < instrument.Count; i++)
                        {
                            APIManager.Token = instrument[i].PNEKey;
                            isPNEDown = AdisseoAPI.AnalyseCode(ref analysesMaterialList);

                            if (isPNEDown == true)
                            {
                                Library.WriteErrorLogFile("PNE Not working. PNEKey used = " + instrument[i].PNEKey.ToString());
                            }
                            else
                            {
                                isPNEDown = AdisseoAPI.MaterialCode();
                            }
                        }

                        if (sampleID > 0)
                        {
                            analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)DataSourceTypeEnum.AdisseoAPIAnalyseCode, true);
                            Library.WriteErrorLogFile("GetAnalyzerNutrient " + analyzerNutrient.Count.ToString());

                            if (analyzerNutrient.Count == 0)
                            {
                                AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "Analyzer Nutrient API did not return ID.  DataSource = " + DataSourceTypeEnum.AdisseoAPIAnalyseCode);
                            }
                            else
                            {

                                //Gets the last record in the log for the sampleid/datasourcetypeID
                                apiLog = new List<APILogInfo>();
                                //apiSprectrumLog = new List<APISprectrumLogInfo>();
                                blnExistsOnLog = false;
                                Library.WriteErrorLogFile("Further Analysis Loop1");

                                apiLog = AIMManager.GetAPILog(sampleID, (Int32)DataSourceTypeEnum.Bruker);
                                //apiSprectrumLog = AIMManager.GetAPISprectrumLog(item.SampleID, EnumAPINIRType.Bruker);

                                if (apiLog.Count > 0)
                                {
                                    blnExistsOnLog = true;
                                    brukerSampleID = apiLog[0].APISampleID;
                                }
                                else
                                {
                                    blnExistsOnLog = false;
                                }

                                if (blnExistsOnLog == true)                //no spectrum so can't rerun
                                {
                                    //Get the samplecode
                                    sample = AIMManager.GetSample(sampleID);

                                    if (sample == null)
                                    {
                                        AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "SampleID does not exists. SampleID = " + sampleID.ToString());
                                        //update process date on Samplefurtheranalysis table						

                                    }
                                    else
                                    {
                                        Library.WriteErrorLogFile("Further Analysis Loop2");
                                        //Get analyzer material
                                        analyzerMaterial = AIMManager.GetAnalyzerMaterial(DataSourceTypeEnum.AdisseoAPI, sample.AIMMaterialID);

                                        if (analyzerMaterial.Count == 0)
                                        {
                                            apiPendingAIMMaterial = AIMManager.GetAPIPendingAIMMaterial(sample.AIMMaterialID, (Int32)DataSourceTypeEnum.AdisseoAPI, true);
                                            AIMManager.InsertAIMMessage((Int32)EnumAIMMessageType.Error, "BrukerSprectrum a cross reference needs to be set up for AdisseoAPI for " + sample.AIMMaterialCode);

                                        }
                                        else
                                        {
                                            Library.WriteErrorLogFile("Load analysesCodes = " + analyzerNutrient.Count.ToString());
                                            analysesCodes = new List<string>();

                                            for (int i = 0; i < analysesMaterialList.Count; i++)
                                            {
                                                if (analyzerMaterial[0].AnalyzerMaterialCode.Trim() == analysesMaterialList[i].MaterialCodes)
                                                {
                                                    analysesCodes.Add(analysesMaterialList[i].AnalysesCode);
                                                }
                                            }

                                            Library.WriteErrorLogFile("Further Analysis Loop3");
                                            //Get spectrum file
                                            brukerSpectrumFile = CFCAPIManager.GetBrukerSpectrumFile(token, brukerSampleID);

                                            if (brukerSpectrumFile != null)
                                            {
                                                if (brukerSpectrumFile.Count > 0)
                                                {
                                                    //Get File Name  
                                                    brukerSampleList = CFCAPIManager.GetBrukerSample(token, customerID, brukerSpectrumFile[0].BrukerSampleID);
                                                    Library.WriteErrorLogFile("Further Analysis Loop4 " + brukerSpectrumFile[0].BrukerSampleID);
                                                    newAnalysisRequest.materialCode = analyzerMaterial[0].AnalyzerMaterialCode.Trim();
                                                    Library.WriteErrorLogFile("PNE " + newAnalysisRequest.materialCode);
                                                    newAnalysisRequest.analysesCodes = analysesCodes;

                                                    for (int i = 0; i < analysesCodes.Count - 1; i++)
                                                    {
                                                        Library.WriteErrorLogFile("PNE " + analysesCodes[i]);
                                                    }

                                                    newAnalysisRequest.sampleCode = sample.SampleID.ToString();
                                                    Library.WriteErrorLogFile("PNE " + newAnalysisRequest.sampleCode);
                                                    //remove mw230530																newAnalysisRequest.description = sample.SampleCode;
                                                    newAnalysisRequest.spectraFileName = brukerSampleList[0].FileName;
                                                    Library.WriteErrorLogFile("PNE " + brukerSampleList[0].FileName);

                                                    if (sample.IsGround == true)
                                                    {
                                                        newAnalysisRequest.samplePresentation = "ground";
                                                    }
                                                    else
                                                    {
                                                        newAnalysisRequest.samplePresentation = "whole";
                                                    }
                                                    Library.WriteErrorLogFile("PNE " + newAnalysisRequest.samplePresentation);

                                                    newAnalysisRequest.spectraFile = Convert.ToBase64String(brukerSpectrumFile[0].BrukerSpectrum);
                                                    Library.WriteErrorLogFile("PNE " + newAnalysisRequest.spectraFile);
                                                    APIManager.Token = brukerSpectrumFile[0].PNEKey;
                                                    Library.WriteErrorLogFile("PNE Token " + brukerSpectrumFile[0].PNEKey);
                                                    analysisRequest = APIManager.CreateAnalysisRequest(newAnalysisRequest);

                                                    if (analysisRequest.Id == null)
                                                    {
                                                        MessageBox.Show("API Return Null", "Message...");
                                                        Library.WriteErrorLogFile("PNE did not return and ID for sampleid " + APIManager.ErrorMessage);
                                                    }
                                                    else
                                                    {
                                                        //	MessageBox.Show(analysisRequest.Id.ToString(), "Message...");
                                                        Library.WriteErrorLogFile("PNE ID " + analysisRequest.Id.ToString());
                                                    }

                                                    Library.WriteErrorLogFile("Further Analysis Loop7 ");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

    }
}
