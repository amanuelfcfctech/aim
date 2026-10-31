CREATE PROCEDURE [dbo].[usp_SampleResult_SEL_MessageInside_SampleID_SampleResultPredictID]
			  @SampleID					INT 				
			, @SampleResultPredictID	INT 
			, @SampleResultMessageType	INT

AS
BEGIN

-- exec usp_SampleResult_SEL_MessageInside_SampleID_SampleResultPredictID 4183, 2798, 3

SET NOCOUNT ON;

CREATE TABLE #Inside (
    SampleResultPredictID Int, 	 
	AnalyzerNutrientID Int, 
    Message varchar(50), 
	Counter Int )
INSERT INTO #Inside
EXEC usp_SampleResult_SEL_Messages_Inside @SampleResultMessageType, @SampleID, NULL, NULL



SELECT S.SampleID
	, SRP.SampleResultPredictID	
	, SR.SampleResultID
	, AN.AnalyzerNutrientCode
	, AM.AIMMaterialName
	, AMN.AIMNutrientName
	, SR.ResultValue
	, WL.Min as MinWarning
	, WL.Max as MaxWarning

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName 	
	, U.UserName 	

	, #Inside.Message	

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
	INNER JOIN
		dbo.AIMNutrientRefAnalyzerNutrient ANRAN with (nolock)
			ON SR.AnalyzerNutrientID = ANRAN.AnalyzerNutrientID
	INNER JOIN 
		dbo.WarningLimits WL with (nolock)
			ON S.AIMMaterialID = WL.AIMMaterialID 
				AND ANRAN.AIMNutrientID = WL.AIMNutrientID

	LEFT OUTER JOIN 
		dbo.AIMMaterial AM with (nolock)
			ON WL.AIMMaterialID = AM.AIMMaterialID
	LEFT OUTER JOIN 
		dbo.AIMNutrient AMN with (nolock)
			ON WL.AIMNutrientID = AMN.AIMNutrientID
	LEFT OUTER JOIN #Inside 
			ON SRP.SampleResultPredictID = #Inside.SampleResultPredictID
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
	and #Inside.Counter > 0

	

DROP TABLE #Inside

END