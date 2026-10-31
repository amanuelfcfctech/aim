CREATE PROCEDURE [dbo].[usp_WarningLimits_SEL_AIMNutrientID]

	@AIMNutrientID			INT
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  W.*
	
	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.WarningLimits W WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON W.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	W.AIMNutrientID = @AIMNutrientID

END