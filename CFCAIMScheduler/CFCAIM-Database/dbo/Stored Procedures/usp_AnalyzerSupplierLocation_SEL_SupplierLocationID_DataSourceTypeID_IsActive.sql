CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocation_SEL_SupplierLocationID_DataSourceTypeID_IsActive]
		@AnalyzerSupplierLocationID		INT
		, @DataSourceTypeID				INT
		, @IsActive						BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ANSL.*
	, ANSL.UpdateStamp

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.AnalyzerSupplierLocation ANSL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ANSL.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
WHERE 
	ANSL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID and 
	ANSL.DataSourceTypeID = @DataSourceTypeID and 
	ANSL.IsActive = @IsActive
END