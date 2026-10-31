CREATE PROCEDURE [dbo].[usp_AnalyzerOwner_SEL_DataSourceTypeID_IsActive]
		@DataSourceTypeID	INT
		, @IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ANO.*
	, ANO.UpdateStamp

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.AnalyzerOwner ANO WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ANO.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	ANO.DataSourceTypeID = @DataSourceTypeID and 
	ANO.IsActive = @IsActive
END