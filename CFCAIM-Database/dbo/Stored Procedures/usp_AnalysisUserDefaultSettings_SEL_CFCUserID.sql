CREATE PROCEDURE [dbo].[usp_AnalysisUserDefaultSettings_SEL_CFCUserID]

	@CFCUserID		INT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  A.*
	, U.UserName
	
FROM 
	dbo.AnalysisUserDefaultSettings A WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON A.CFCUserID = U.CFCUserID

	WHERE
		A.CFCUserID = @CFCUserID

END