CREATE PROCEDURE [dbo].[usp_AIMSourceLocationGuarantee_SEL_AIMNutrientID]
		@AIMNutrientID			INT
		, @IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	    OL.*

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 
	dbo.AIMSourceLocationGuarantee OL WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.AIMSource AM 
			on OL.AIMSourceID = AM.AIMSourceID
	LEFT OUTER JOIN
		dbo.AIMSourceLocation ASL 
			on OL.AIMSourceLocationID = ASL.AIMSourceLocationID
	LEFT OUTER JOIN
		dbo.AIMMaterial AMM
			on OL.AIMMaterialID = AMM.AIMMaterialID
	LEFT OUTER JOIN 
		dbo.AIMNutrient AN
			ON OL.AIMNutrientID = AN.AIMNutrientID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON OL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	OL.AIMNutrientID = @AIMNutrientID 
	and OL.IsActive = @IsActive

END