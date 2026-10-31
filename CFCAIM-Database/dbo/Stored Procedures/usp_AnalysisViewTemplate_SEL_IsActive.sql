CREATE PROCEDURE [dbo].[usp_AnalysisViewTemplate_SEL_IsActive]
				@IsActive				BIT
				, @GridNumber			INT
				, @TemplateType			INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    R.*
	  , AU.UserName
FROM 
	dbo.AnalysisViewTemplate R WITH(NOLOCK)
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON R.CFCUserID = U.CFCUserID
		LEFT OUTER JOIN
			dbo.aspnet_Users AU WITH(NOLOCK)
				ON AU.UserId = U.UserID
WHERE
	R.IsActive = @IsActive

	--4/4/18
	AND R.GridNumber = @GridNumber

	--4/12/18
	AND R.TemplateType = @TemplateType

END
