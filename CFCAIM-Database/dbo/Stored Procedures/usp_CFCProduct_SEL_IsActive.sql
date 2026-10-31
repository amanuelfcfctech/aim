
CREATE PROCEDURE [dbo].[usp_CFCProduct_SEL_IsActive]
				@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  CP.*
	, CPL.CFCPlantCode
	, CPL.CFCPlantName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.CFCProduct CP WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CP.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID

		LEFT OUTER JOIN
			dbo.CFCPlant CPL WITH(NOLOCK)
				ON CP.CFCPlantID = CPL.CFCPlantID

WHERE 
	CP.IsActive = @IsActive

END