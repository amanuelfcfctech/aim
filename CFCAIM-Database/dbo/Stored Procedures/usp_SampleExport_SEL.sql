create PROCEDURE [dbo].[usp_SampleExport_SEL]
AS
BEGIN
SET NOCOUNT ON;
SELECT 
	  SE.*
	, AU.UserName
FROM 
	dbo.SampleExport SE WITH(NOLOCK)
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON SE.CFCUserID = U.CFCUserID
			LEFT OUTER JOIN
				dbo.aspnet_Users AU WITH(NOLOCK)
					ON AU.UserId = U.UserID


END