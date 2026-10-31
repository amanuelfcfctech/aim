CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetail_SEL_SampleResultTemplateID_cm]

@SampleResultTemplateID INT
, @SampleID INT
, @DatasourceTypeID INT

AS
BEGIN

--exec usp_SampleResultTemplateDetail_SEL_SampleResultTemplateID_cm 5, 5, 10000

SET NOCOUNT ON;

SELECT 
	SRTD.*	
	, AN.AnalyzerNutrientCode
	, AN.AnalyzerNutrientName
	, AN.UnitType
	, AIMN.AIMNutrientName
	, AU.UserName
	, X.ResultValue AS CurrentValue
		
FROM 
	dbo.SampleResultTemplateDetail SRTD with (nolock)
INNER JOIN
	dbo.AnalyzerNutrient AN with (nolock)
		ON SRTD.AnalyzerNutrientID = AN.AnalyzerNutrientID

LEFT JOIN
	(SELECT AN.AnalyzerNutrientID, IsNull(ResultValue,0) as ResultValue
	FROM 
			dbo.SampleResult SR
		INNER JOIN 
			dbo.AnalyzerNutrient AN WITH(NOLOCK)
				ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
				AND AN.DataSourceTypeID = @DatasourceTypeID
				AND SR.SampleID = @SampleID
				AND AN.IsActive = 1				
				AND SR.IsActive = 1) 
	X ON AN.AnalyzerNutrientID = X.AnalyzerNutrientID

LEFT OUTER JOIN
	dbo.AIMNutrientRefAnalyzerNutrient ANX with (nolock)
		ON AN.AnalyzerNutrientID = ANX.AnalyzerNutrientID		
LEFT OUTER JOIN 
	dbo.AIMNutrient AIMN with (nolock)
		ON ANX.AIMNutrientID = AIMN.AIMNutrientID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON SRTD.CFCUserID = U.CFCUserID
LEFT OUTER JOIN
	dbo.aspnet_Users AU WITH(NOLOCK)
		ON AU.UserId = U.UserID

WHERE SRTD.SampleResultTemplateID = @SampleResultTemplateID

ORDER BY SRTD.SortOrder 

END