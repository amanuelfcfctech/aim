CREATE PROCEDURE [dbo].[usp_QueryOperation_SEL_IsActive]
				@IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  QO.QueryOperationID
	, QO.Operation
	, QO.IsActive
	, QO.UpdateStamp
	, QO.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.QueryOperation QO WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON QO.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	QO.IsActive = @IsActive

END
