CREATE PROCEDURE [dbo].[usp_SampleResult_SEL_MessageOutside_SampleID_SampleResultPredictID]
			  @SampleID					INT 			
			, @SampleResultPredictID	INT 
			, @SampleResultMessageType	INT

AS
BEGIN

-- exec usp_SampleResult_SEL_MessageOutside_SampleID_SampleResultPredictID 4183, 2798, 3

SET NOCOUNT ON;

CREATE TABLE #Outside (
    SampleResultPredictID Int, 	 
	AnalyzerNutrientID Int, 
    Message varchar(50), 
	Counter Int )
INSERT INTO #Outside
EXEC usp_SampleResult_SEL_Messages_Outside @SampleResultMessageType, @SampleID, NULL, NULL
--EXEC usp_SampleResult_SEL_Messages_Outside 3, 4183, NULL, NULL


SELECT S.SampleID, 
	SRP.*	
	, SR.AnalyzerNutrientID
	, SR.ResultValue
	, AN.AnalyzerNutrientCode 
	, AN.DataSourceTypeID 	
	
	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName 
	, U.UserName 

	, #Outside.Message
	

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

	LEFT OUTER JOIN #Outside 
			ON SRP.SampleResultPredictID = #Outside.SampleResultPredictID
	LEFT OUTER JOIN 
		CFCUser.CFCUser U WITH(NOLOCK)
			ON S.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN	
	--	dbo.aspnet_Users AU WITH(NOLOCK) 
	--		ON AU.UserId = U.UserID

		
WHERE 
	S.SampleID = @SampleID
	and SRP.SampleResultPredictID = @SampleResultPredictID
	and (#Outside.Counter > 0 )

	

DROP TABLE #Outside

END