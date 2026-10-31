CREATE PROCEDURE [dbo].[usp_AIMSourceLocationGuarantee_SEL_IsActive_AIMSourceID]
		@IsActive					BIT
		, @AIMSupplierID			INT
		
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	    SLG.*	  
	  , AM.SourceName
	  , ASL.SourceLocationName
	  , AMM.AIMMaterialName
	  , AN.AIMNutrientName

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 
	dbo.AIMSourceLocationGuarantee SLG WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.AIMSource AM 
			on SLG.AIMSourceID = AM.AIMSourceID
	LEFT OUTER JOIN
		dbo.AIMSourceLocation ASL 
			on SLG.AIMSourceLocationID = ASL.AIMSourceLocationID
	LEFT OUTER JOIN
		dbo.AIMMaterial AMM
			on SLG.AIMMaterialID = AMM.AIMMaterialID
	LEFT OUTER JOIN 
		dbo.AIMNutrient AN
			ON SLG.AIMNutrientID = AN.AIMNutrientID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SLG.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	SLG.IsActive = @IsActive
	and SLG.AIMSourceID = @AIMSupplierID
	

ORDER BY
	AM.SourceName, ASL.SourceLocationName, AMM.AIMMaterialName, AN.AIMNutrientName

END