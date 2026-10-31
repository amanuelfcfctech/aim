CREATE PROCEDURE [dbo].[usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID_OwnerID_OwnerLocationName]
			  @DataSourceTypeID						INT
			, @AnalyzerOwnerID					INT
			, @AnalyzerOwnerLocationName		NVARCHAR(30)
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID_OwnerID_OwnerLocationName 10000, 1, '04'

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
	AND anol.AnalyzerOwnerID = @AnalyzerOwnerID
	AND ANOL.AnalyzerOwnerLocationName = @AnalyzerOwnerLocationName

ORDER BY ANOL.AnalyzerOwnerLocationName

END