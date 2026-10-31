CREATE PROCEDURE [dbo].[usp_DateFormat_SEL_DateFormatID]
	@DateFormatID		INT
		
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_DateFormat_SEL

SELECT 
	  DF.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.DateFormat DF WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	DF.DateFormatID = @DateFormatID

END