CREATE PROCEDURE [dbo].[usp_MathType_SEL_IsActive]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  LD.MathTypeID
	, LD.MathTypeName
	, LD.IsActive
	, LD.UpdateStamp
	, LD.CFCUserID
	, LD.IsSystem
	, LD.Equation

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.MathType LD WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON LD.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	LD.IsActive = @IsActive
END