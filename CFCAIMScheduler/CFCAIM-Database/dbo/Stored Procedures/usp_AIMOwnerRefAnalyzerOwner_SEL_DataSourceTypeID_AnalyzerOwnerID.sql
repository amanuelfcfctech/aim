CREATE PROCEDURE [dbo].[usp_AIMOwnerRefAnalyzerOwner_SEL_DataSourceTypeID_AnalyzerOwnerID]
	 @DataSourceTypeID				INT = NULL
	,@AnalyzerOwnerID			INT = NULL

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  XREF.*,
	  ANO.*	

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
	  	  
FROM 

	dbo.AnalyzerOwner ANO
	INNER JOIN dbo.AIMOwnerRefAnalyzerOwner XREF on ANO.AnalyzerOwnerID = XREF.AnalyzerOwnerID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE ANO.DataSourceTypeID = @DataSourceTypeID and XREF.AnalyzerOwnerID = @AnalyzerOwnerID

ORDER BY ANO.AnalyzerOwnerName	

END