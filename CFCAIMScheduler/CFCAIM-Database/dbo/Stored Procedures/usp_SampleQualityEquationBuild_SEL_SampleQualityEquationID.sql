CREATE PROCEDURE [dbo].[usp_SampleQualityEquationBuild_SEL_SampleQualityEquationID]
		@SampleQualityEquationID				INT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SEB.SampleQualityEquationBuildID
	, SEB.SampleQualityEquationID
	, SEB.ValueString	
	, SEB.[Status]
	, SEB.UpdateStamp
	, SEB.CFCUserID		
	, SEB.SampleQualityParameterID
	

FROM 
	dbo.SampleQualityEquationBuild SEB WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SEB.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID
WHERE 
	SEB.SampleQualityEquationID = @SampleQualityEquationID
	and SEB.[Status] = 1

ORDER BY
	SEB.SampleQualityEquationBuildID
END