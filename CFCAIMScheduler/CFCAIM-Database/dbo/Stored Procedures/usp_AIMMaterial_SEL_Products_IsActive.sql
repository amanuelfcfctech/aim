CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_Products_IsActive]
		@IsActive				BIT				
AS
BEGIN
SET NOCOUNT ON;

--exec [usp_AIMMaterial_SEL_Products_IsActive] 1

SELECT
	0 as AimMaterialID		--> needed for entity
	, AM.AIMMaterialCode	
	, AM.MaterialTypeID	
	, (select top 1 a.AIMMaterialName from AIMMaterial a 
		where a.AIMMaterialCode = am.AIMMaterialCode) as AimMaterialName	

FROM 
	dbo.AIMMaterial AM WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID
			
    -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	AM.IsActive = @IsActive AND	
	AM.MaterialTypeID = 2

GROUP BY
	AM.AIMMaterialCode	
	, AM.MaterialTypeID
	
	
order by am.AIMMaterialCode

END