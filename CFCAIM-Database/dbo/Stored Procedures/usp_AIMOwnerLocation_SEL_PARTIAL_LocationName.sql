CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SEL_PARTIAL_LocationName]
		  @PartialName		NVARCHAR(50)
		, @Num				INT

AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMOwnerLocation_SEL_PARTIAL_LocationName 'Owner', 5


SELECT TOP 1 
	  O.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMOwnerLocation O WITH(NOLOCK)
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON O.CFCUserID = U.CFCUserID

			-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
			--LEFT OUTER JOIN
			--	dbo.aspnet_Users AU WITH(NOLOCK)
			--		ON AU.UserId = U.UserID

WHERE 

	LEFT(O.LocationName,@Num) = @PartialName
	AND O.IsActive = 1

END