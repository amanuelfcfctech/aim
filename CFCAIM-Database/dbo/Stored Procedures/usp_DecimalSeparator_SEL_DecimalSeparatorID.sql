CREATE PROCEDURE [dbo].[usp_DecimalSeparator_SEL_DecimalSeparatorID]
		@DecimalSeparatorID			INT
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_DecimalSeparator_SEL_DecimalSeparatorID 2

SELECT 
	  DS.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.DecimalSeparator DS WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DS.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
	DS.DecimalSeparatorID = @DecimalSeparatorID

END