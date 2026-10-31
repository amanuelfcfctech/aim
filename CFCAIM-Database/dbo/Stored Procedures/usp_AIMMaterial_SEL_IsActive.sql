CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_IsActive]
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

	--3/8/18
	, IsNull(AOL.LocationName, '') as AIMOwnerLocationName

	-- 1/11/21 - changed to remove aspnet_User table
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMMaterial AM WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID
	--3/8/18
	LEFT OUTER JOIN
		dbo.AIMOwnerLocation AOL with (nolock)
			ON AM.AIMOwnerLocationID = AOL.AIMOwnerLocationID

	-- 1/11/21 - changed to remove aspnet_User table				
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
		
WHERE 
	AM.IsActive = @IsActive

--2/11/21 - order by removed for CSA speed issue
--ORDER BY AM.AIMMaterialCode


END
