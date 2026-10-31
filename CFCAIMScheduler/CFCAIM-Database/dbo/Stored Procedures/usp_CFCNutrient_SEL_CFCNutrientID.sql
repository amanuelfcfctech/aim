CREATE PROCEDURE [dbo].[usp_CFCNutrient_SEL_CFCNutrientID]
		@CFCNutrientID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    CN.*

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)	
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.CFCNutrient CN WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CN.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	CN.CFCNutrientID = @CFCNutrientID
END