
CREATE PROCEDURE [dbo].[usp_SampleResult_SEL_SampleID_IsActive]
	  @SampleID					INT 
	, @IsActive					BIT 
	, @SampleResultMessageType	INT		 			--> will always be types 2 (for SampleResultPredictID) or  4 (for AnalyzerNutrientID)
	
	
-- exec usp_SampleResult_SEL_SampleID_IsActive 4183, 1, 4	

AS
BEGIN
SET NOCOUNT ON;

CREATE TABLE #Outside (	
    AnalyzerNutrientID Int, 	 
    Message Int    )
INSERT INTO #Outside
EXEC usp_SampleResult_SEL_Messages_Outside @SampleResultMessageType, @SampleID, NULL, NULL, @IsActive

CREATE TABLE #Inside (	
    AnalyzerNutrientID Int, 	 
    Message Int	)
INSERT INTO #Inside
EXEC usp_SampleResult_SEL_Messages_Inside @SampleResultMessageType, @SampleID, NULL, NULL, @IsActive


SELECT 
	  SR.SampleResultID
	, SR.SampleID
	, SR.AnalyzerNutrientID
	, AN.AnalyzerNutrientName
	, SR.ResultValue
	, SR.UpdateStamp
	, SR.IsActive
	, SR.AnalysisDate
	, SR.CFCUserID
	, SR.IsCalculate
	, AN.DataSourceTypeID
	, SR.Unit
	, AN.AnalyzerNutrientCode 	 

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

	, AN.UnitType		
	, DST.Name as DataSourceTypeName	
	, AMN.AIMNutrientName	
	, #Outside.Message as MessageOutside
	, #Inside.Message as MessageInside
	, ISNULL(CASE WHEN ResultValue < WL.Min THEN 1 ELSE CASE WHEN ResultValue > WL.Max THEN 2 ELSE 0 END END, 0) AS ResultValueFlag
		
FROM 
	dbo.SampleResult SR
	INNER JOIN 
		dbo.AnalyzerNutrient AN WITH(NOLOCK)
			ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
			AND AN.IsActive = 1

	LEFT OUTER JOIN #Outside 
			ON AN.AnalyzerNutrientID = #Outside.AnalyzerNutrientID
	LEFT OUTER JOIN #Inside 
			ON AN.AnalyzerNutrientID = #Inside.AnalyzerNutrientID

	LEFT OUTER JOIN
		dbo.DataSourceType DST with (nolock)
			ON AN.DataSourceTypeID = DST.DataSourceTypeID	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SR.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID	

	LEFT JOIN dbo.AIMNutrientRefAnalyzerNutrient XREF WITH(NOLOCK) 
			ON XREF.AnalyzerNutrientID = AN.AnalyzerNutrientID
	LEFT JOIN dbo.AIMNutrient AMN 
			ON XREF.AIMNutrientID = AMN.AIMNutrientID

	INNER JOIN
		dbo.Sample S
		ON S.SampleID = SR.SampleID
	LEFT JOIN 
		dbo.WarningLimits WL with (nolock)
			ON S.AimMaterialID = WL.AimMaterialID AND
				XREF.AIMNutrientID = WL.AimNutrientID	
WHERE

		SR.SampleID = @SampleID
	AND SR.IsActive = @IsActive	
	
ORDER BY AN.AnalyzerNutrientCode

DROP TABLE #Outside
DROP TABLE #Inside

END