CREATE PROCEDURE [dbo].[usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID_OwnerLocationCode]
			  @DataSourceTypeID						INT
			, @AnalyzerOwnerLocationCode		NVARCHAR(30)
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ANOL.*	
	, DT.Name as DataSourceUserName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerOwnerLocation ANOL WITH(NOLOCK)

	LEFT OUTER JOIN
		dbo.DataSourceType DT with (nolock)
			ON ANOL.DataSourceTypeID = DT.DataSourceTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ANOL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	ANOL.DataSourceTypeID = @DataSourceTypeID
	AND ANOL.AnalyzerOwnerLocationCode = @AnalyzerOwnerLocationCode

ORDER BY ANOL.AnalyzerOwnerLocationName

END