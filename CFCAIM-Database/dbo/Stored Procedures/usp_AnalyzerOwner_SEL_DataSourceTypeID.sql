CREATE PROCEDURE [dbo].[usp_AnalyzerOwner_SEL_DataSourceTypeID]
		@DataSourceTypeID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  ANO.*	
	, DT.Name as DataSourceUserName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerOwner ANO WITH(NOLOCK)
LEFT OUTER JOIN
	dbo.DataSourceType DT with (nolock)
		ON ANO.DataSourceTypeID = DT.DataSourceTypeID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON ANO.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

WHERE 
	ANO.DataSourceTypeID = @DataSourceTypeID


ORDER BY ANO.AnalyzerOwnerName

END