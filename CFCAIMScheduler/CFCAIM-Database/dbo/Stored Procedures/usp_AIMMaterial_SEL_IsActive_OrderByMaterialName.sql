CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_IsActive_OrderByMaterialName]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  AM.AIMMaterialID
	, AM.MaterialTypeID	
	, AM.AIMMaterialName
	, AM.AIMMaterialCode
	, AM.IsActive
	, AM.UpdateStamp
	, AM.CFCUserID	
	, AM.AIMOwnerLocationID

	-- 1/11/21 - changed to remove aspnet_User table
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMMaterial AM WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	AM.IsActive = @IsActive


ORDER BY AM.AIMMaterialName


END