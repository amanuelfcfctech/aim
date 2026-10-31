CREATE PROCEDURE [dbo].[usp_EmailAddressXRefMessage_SEL_ALL]
	
--exec usp_EmailAddressXRefMessage_SEL_ALL	
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  EAXRM.*
	, U.EmailAddress 

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName as UpdateUserName 
	, U.UserName as UpdateUserName 
	
FROM 
	dbo.EmailAddressXRefMessage EAXRM WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON EAXRM.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

END