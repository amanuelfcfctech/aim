CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SEL_DataSourceTypeID_AnalyzerOwnerLocationID]
	 @DataSourceTypeID						INT = NULL
	,@AnalyzerOwnerLocationID			INT = NULL
	,@AnalyzerOwnerID					INT = NULL
	

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  XREF.*,
	  ANOL.*	

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
	  	  
FROM 

	dbo.AnalyzerOwnerLocation ANOL
	INNER JOIN dbo.AIMOwnerLocationRefAnalyzerOwnerLocation XREF 
		on ANOL.AnalyzerOwnerLocationID = XREF.AnalyzerOwnerLocationID and
		   ANOL.AnalyzerOwnerID = XREF.AnalyzerOwnerID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE ANOL.DataSourceTypeID = @DataSourceTypeID and 
		XREF.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID and
		XREF.AnalyzerOwnerID	= @AnalyzerOwnerID

ORDER BY ANOL.AnalyzerOwnerLocationName	

END