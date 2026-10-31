CREATE PROCEDURE [dbo].[usp_SampleResultPredict_SEL_SampleID]
			@SampleID					INT 
		  , @SampleResultMessageType	INT					--> will always be type 2
		 

-- exec usp_SampleResultPredict_SEL_SampleID 4183, 2

AS
BEGIN
SET NOCOUNT ON;

CREATE TABLE #WarningOutside (
    SampleResultPredictID Int, 	
    Message varchar(50) )
INSERT INTO #WarningOutside
EXEC usp_SampleResult_SEL_Messages_Outside @SampleResultMessageType, @SampleID, NULL, NULL, NULL

CREATE TABLE #WarningInside (
    SampleResultPredictID Int, 	
    Message varchar(50) )
INSERT INTO #WarningInside
EXEC usp_SampleResult_SEL_Messages_Inside @SampleResultMessageType, @SampleID, NULL, NULL, NULL



SELECT S.SampleID, 
	SRP.*	
	, SR.AnalyzerNutrientID
	, AN.AnalyzerNutrientCode 
	, AN.DataSourceTypeID 	

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName 
	, U.UserName 

	, SR.ResultValue
	, #WarningOutside.Message as MessageOutside
	, #WarningInside.Message as MessageInside

FROM 
	dbo.Sample S with (nolock)
	INNER JOIN 
		SampleResult SR with (nolock)
			ON S.SampleID = SR.SampleID
	INNER JOIN 
		SampleResultPredict SRP with (nolock)
			ON SR.SampleResultID = SRP.SampleResultID
	INNER JOIN 
		dbo.AnalyzerNutrient AN WITH(NOLOCK)
			ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
			AND AN.IsActive = 1
	INNER JOIN #WarningOutside 
			ON SRP.SampleResultPredictID = #WarningOutside.SampleResultPredictID
	INNER JOIN #WarningInside
			ON SRP.SampleResultPredictID = #WarningInside.SampleResultPredictID
	LEFT OUTER JOIN 
		CFCUser.CFCUser U WITH(NOLOCK)
			ON S.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN	
	--	dbo.aspnet_Users AU WITH(NOLOCK) 
	--		ON AU.UserId = U.UserID

		
WHERE 
	S.SampleID = @SampleID

DROP TABLE #WarningOutside
DROP TABLE #WarningInside

END