CREATE PROCEDURE [dbo].[usp_AIMPriorityUsedValue_SEL_ID]
	@AIMPriorityUsedValueID		INT
		
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	 P.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMPriorityUsedValue P WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON P.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE P.AIMPriorityUsedValueID = @AIMPriorityUsedValueID

END