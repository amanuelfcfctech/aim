CREATE PROCEDURE [dbo].[usp_AIMSource_SEL_IsActive_HasSourceLocations]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT DISTINCT
	  AM.AIMSourceID	
	, AM.SourceName
	, AM.IsActive
	, AM.UpdateStamp

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName	
	, U.UserName	

FROM 
	dbo.AIMSource AM WITH(NOLOCK)
	INNER JOIN 
		dbo.AIMSourceLocation ASL
			ON AM.AIMSourceID = ASL.AIMSourceID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	AM.IsActive = @IsActive
	and ASL.IsActive = @IsActive;
END