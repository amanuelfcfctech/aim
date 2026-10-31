CREATE PROCEDURE [dbo].[usp_QueryField_SEL_IsActive]
				@IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  QF.QueryFieldID
	, QF.Field
	, QF.IsActive
	, QF.UpdateStamp
	, QF.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.QueryField QF WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON QF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	QF.IsActive = @IsActive

END
