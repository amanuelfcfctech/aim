CREATE PROCEDURE [dbo].[usp_AnalyzerOwnerLocation_SEL_DataSourceTypeID_OwnerLocationCode]
			  @DataSourceTypeID						INT
			, @AnalyzerOwnerLocationCode		NVARCHAR(30)
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ANOL.*
	, AU.UserName
	, DT.Name as DataSourceUserName
FROM 
	dbo.AnalyzerOwnerLocation ANOL WITH(NOLOCK)

	LEFT OUTER JOIN
		dbo.DataSourceType DT with (nolock)
			ON ANOL.DataSourceTypeID = DT.DataSourceTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ANOL.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID
WHERE 
	ANOL.DataSourceTypeID = @DataSourceTypeID
	AND ANOL.AnalyzerOwnerLocationCode = @AnalyzerOwnerLocationCode

ORDER BY ANOL.AnalyzerOwnerLocationName

END