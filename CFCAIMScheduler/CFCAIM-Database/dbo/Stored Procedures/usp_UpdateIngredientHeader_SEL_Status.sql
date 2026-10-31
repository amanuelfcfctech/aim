CREATE PROCEDURE [dbo].[usp_UpdateIngredientHeader_SEL_Status]
(@Status as Int)

AS
BEGIN

--exec usp_UpdateIngredientHeader_SEL_Status 1

SET NOCOUNT ON;

SELECT 
	h.*, 

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--AU.UserName 
	U.UserName 

FROM 
	dbo.UpdateIngredientHeader h with (nolock)
LEFT OUTER JOIN 
	CFCUser.CFCUser U with (nolock) 
		ON h.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN	
--	dbo.aspnet_Users AU WITH(NOLOCK) 
--		ON AU.UserId = U.UserID)

WHERE h.Status = @Status 

END
