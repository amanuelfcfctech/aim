CREATE PROCEDURE [dbo].[usp_AIMOwner_SEL_OwnerName]
		  @OwnerName		NVARCHAR(50)

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  O.*
	
	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMOwner O WITH(NOLOCK)
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON O.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID

WHERE 

	O.OwnerName = @OwnerName	
	AND O.IsActive = 1
END