CREATE PROCEDURE [dbo].[usp_SampleQualityEquation_SEL_IsActive_SortBy_Equation]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SQE.SampleQualityEquationID
	, SQE.SampleQualityEquationName
	, SQE.AnalyzerNutrientID
	, SQE.DataSourceTypeID
	, SQE.Equation	
	, SQE.IsActive
	, SQE.UpdateStamp
	, SQE.CFCUserID		
	, AU.UserName
FROM 
	dbo.SampleQualityEquation SQE WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SQE.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID
WHERE 
	
	SQE.IsActive = @IsActive

ORDER BY
	SQE.Equation DESC
END