CREATE PROCEDURE [dbo].[xusp_T1_Quant2_Results_SEL_DateTimeAnalysisSampleCode]
		  @SampleCode		NVARCHAR(255)	= NULL
		, @AnalysisStartTime	DATETIME		= NULL
		, @AnalysisEndTime		DATETIME		= NULL
AS
BEGIN
SET NOCOUNT ON;

--SELECT 
--	  P.Value_Product_Info_1 as SampleCode
--	, P.Product as Material
--	, R.Date_Time_Analysis as SampleDate
--	, R.Component_Name as Nutrient
--	, R.Component_Unit as UnitType
--	, R.Prediction_Calculated as ResultValue	
--	, R.Spectrum_Name + '-' + cast(R.Method_ID AS VARCHAR) UniqueID
--FROM            
--	dbo.T1_Lab_And_Process_Parameters  P
--INNER JOIN
--    dbo.T1_Quant2_Results R						 
--		ON		P.Path = R.Spectrum_Path 
--		AND		P.File_Name = R.Spectrum_Name
--WHERE
--	R.Date_Time_Analysis >= ISNULL(@AnalysisStartTime, R.Date_Time_Analysis)
--	AND R.Date_Time_Analysis <= ISNULL(@AnalysisEndTime, R.Date_Time_Analysis)
--	AND P.Value_Product_Info_1 = ISNULL(@SampleCode, P.Value_Product_Info_1)
--	AND R.Prediction_Calculated IS NOT NULL
--ORDER BY
--	  P.Value_Product_Info_1
--	, R.Date_Time_Analysis 

END