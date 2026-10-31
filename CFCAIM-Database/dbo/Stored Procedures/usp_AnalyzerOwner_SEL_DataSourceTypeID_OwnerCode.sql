CREATE PROCEDURE [dbo].[usp_AnalyzerOwner_SEL_DataSourceTypeID_OwnerCode]
		@DataSourceTypeID				INT
		, @AnalyzerOwnerCode			NVARCHAR(30)
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ANO.*
	, AU.UserName
	, DT.Name as DataSourceUserName

FROM 
	dbo.AnalyzerOwner ANO WITH(NOLOCK)
LEFT OUTER JOIN
	dbo.DataSourceType DT with (nolock)
		ON ANO.DataSourceTypeID = DT.DataSourceTypeID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON ANO.CFCUserID = U.CFCUserID
LEFT OUTER JOIN
	dbo.aspnet_Users AU WITH(NOLOCK)
		ON AU.UserId = U.UserID

WHERE 
	ANO.DataSourceTypeID = @DataSourceTypeID
	and ANO.AnalyzerOwnerCode = @AnalyzerOwnerCode
	and ANO.IsActive = 1

ORDER BY ANO.AnalyzerOwnerName

END