CREATE PROCEDURE [dbo].[usp_Sample_SEL_AlternateCode]
	@AlternateCode		VARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    S.*

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName	 
	  , U.UserName	 
	 
FROM 
	dbo.Sample S WITH(NOLOCK)
	
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON S.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	S.AlternateCode = @AlternateCode
END