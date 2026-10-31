CREATE PROCEDURE [dbo].[usp_MapSelection_SEL_IsActive]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SS.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.SampleImportUserDefinedMapSelection SS WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SS.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	SS.IsActive = @IsActive
END