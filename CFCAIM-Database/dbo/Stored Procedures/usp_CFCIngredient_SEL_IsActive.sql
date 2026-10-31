
CREATE PROCEDURE [dbo].[usp_CFCIngredient_SEL_IsActive]
				@IsActive				BIT
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  CI.CFCIngredientID
	, CI.CFCIngredientCode
	, CI.CFCIngredientName
	, CI.IsActive
	, CI.UpdateStamp	
	, CI.AIMDatabaseID 

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.CFCIngredient CI WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CI.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	CI.IsActive = @IsActive
	
ORDER BY 
	CI.CFCIngredientCode
END
