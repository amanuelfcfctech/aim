CREATE PROCEDURE [dbo].[usp_AnalyzerDataSourceType_SEL_DataSourceTypeID_IsActive]		
		 @DataSourceTypeID				INT
		, @IsActive						BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ADST.*	
	, DT.Name as DataSourceUserName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerDataSourceType ADST WITH(NOLOCK)
LEFT OUTER JOIN
	dbo.DataSourceType DT with (nolock)
		ON ADST.DataSourceTypeID = DT.DataSourceTypeID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON ADST.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

WHERE
	ADST.DataSourceTypeID = @DataSourceTypeID 
	and ADST.IsActive = @IsActive

ORDER BY ADST.AnalyzerDataSourceTypeName

END