CREATE PROCEDURE [dbo].[usp_UnitType_SEL_IsActive]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  UT.UnitTypeID	
	, UT.UnitTypeCode
	, UT.UnitTypeDescription
	, UT.IsActive
	, UT.CFCUserID
	, UT.UpdateStamp

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.UnitType UT WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON UT.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	UT.IsActive = @IsActive;
END