CREATE PROCEDURE [dbo].[usp_AnalyzerSupplier_SEL_DataSourceTypeID_IsActive]
		@DataSourceTypeID	INT
		, @IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ANS.*
	, ANS.UpdateStamp

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerSupplier ANS WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ANS.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--		dbo.aspnet_Users AU WITH(NOLOCK)
	--			ON AU.UserId = U.UserID
WHERE 
	ANS.DataSourceTypeID = @DataSourceTypeID and 
	ANS.IsActive = @IsActive
END