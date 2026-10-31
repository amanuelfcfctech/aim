
CREATE PROCEDURE [dbo].[usp_UpdateIngredientHeader_SEL]
(@UpdateIngredientHeaderID as Int)

AS
BEGIN

SET NOCOUNT ON;

SELECT 
	h.UpdateIngredientHeaderID, 
	h.UpdateName 
FROM 
	dbo.UpdateIngredientHeader h with (nolock)
LEFT OUTER JOIN 
	CFCUser.CFCUser U with (nolock) 
		ON h.CFCUserID = U.CFCUserID


-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)	
--LEFT OUTER JOIN	
--	dbo.aspnet_Users AU WITH(NOLOCK) 
--		ON AU.UserId = U.UserID

WHERE h.UpdateIngredientHeaderID = @UpdateIngredientHeaderID

END
