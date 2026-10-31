CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetail_SEL_SampleResultTemplateID]

@SampleResultTemplateID INT

AS
BEGIN

--exec usp_SampleResultTemplateDetail_SEL_SampleResultTemplateID 5

SET NOCOUNT ON;

SELECT 
	SRTD.*	
	, AN.AnalyzerNutrientCode
	, AN.AnalyzerNutrientName
	, AN.UnitType
	, AIMN.AIMNutrientName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
	
FROM 
	dbo.SampleResultTemplateDetail SRTD with (nolock)
INNER JOIN
	dbo.AnalyzerNutrient AN with (nolock)
		ON SRTD.AnalyzerNutrientID = AN.AnalyzerNutrientID
LEFT OUTER JOIN
	dbo.AIMNutrientRefAnalyzerNutrient ANX with (nolock)
		ON AN.AnalyzerNutrientID = ANX.AnalyzerNutrientID		
LEFT OUTER JOIN 
	dbo.AIMNutrient AIMN with (nolock)
		ON ANX.AIMNutrientID = AIMN.AIMNutrientID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON SRTD.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

WHERE SRTD.SampleResultTemplateID = @SampleResultTemplateID

ORDER BY SRTD.SortOrder 

END