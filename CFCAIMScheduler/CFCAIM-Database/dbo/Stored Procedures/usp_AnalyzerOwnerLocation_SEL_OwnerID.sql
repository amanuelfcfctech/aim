CREATE PROCEDURE [dbo].[usp_AnalyzerOwnerLocation_SEL_OwnerID]
			@AnalyzerOwnerID			INT
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_AnalyzerOwnerLocation_SEL_OwnerID 12

SELECT 
	  ANOL.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerOwnerLocation ANOL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ANOL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	ANOL.AnalyzerOwnerID = @AnalyzerOwnerID
END