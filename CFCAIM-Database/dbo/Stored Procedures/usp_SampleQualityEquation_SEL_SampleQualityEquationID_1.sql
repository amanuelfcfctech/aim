CREATE PROCEDURE [dbo].[usp_SampleQualityEquation_SEL_SampleQualityEquationID]
		@SampleQualityEquationID				INT
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

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.SampleQualityEquation SQE WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SQE.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	SQE.SampleQualityEquationID = @SampleQualityEquationID
END