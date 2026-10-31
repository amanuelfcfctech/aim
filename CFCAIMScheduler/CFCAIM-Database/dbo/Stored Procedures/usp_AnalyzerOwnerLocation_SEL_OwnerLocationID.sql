CREATE PROCEDURE [dbo].[usp_AnalyzerOwnerLocation_SEL_OwnerLocationID]
			@AnalyzerOwnerLocationID			INT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ANOL.*
	  
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerOwnerLocation ANOL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ANOL.CFCUserID = U.CFCUserID

	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	ANOL.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID
END