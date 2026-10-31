CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SEL_DataSourceTypeID]

@DataSourceTypeID				INT = NULL

AS
BEGIN
SET NOCOUNT ON;

SELECT
	    XREF.AIMOwnerLocationRefAnalyzerOwnerLocationID	  
	  , XREF.AnalyzerOwnerID
	  , XREF.AnalyzerOwnerLocationID	  
	  , XREF.UpdateStamp
	  , AOL.AIMOwnerID
	  , AOL.AIMOwnerLocationID	 
	  , AOL.LocationCode
	  , AOL.LocationName	 
	  , ANOL.AnalyzerOwnerLocationCode
	  , ANOL.AnalyzerOwnerLocationName 	  
	  , DT.Name as DataSourceUserName

	  --1/8/21
	  , AO.OwnerName
	  , ANO.AnalyzerOwnerName

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
	 
FROM 
	dbo.AIMOwnerLocationRefAnalyzerOwnerLocation XREF WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMOwnerLocation AOL 
			ON XREF.AIMOwnerLocationID = AOL.AIMOwnerLocationID

	-- 1/8/21
	LEFT OUTER JOIN dbo.AIMOwner AO 
			ON AOL.AIMOwnerID = AO.AIMOwnerID				

	LEFT OUTER JOIN	dbo.AnalyzerOwnerLocation ANOL	
			ON XREF.AnalyzerOwnerLocationID = ANOL.AnalyzerOwnerLocationID

	-- 1/8/21
	LEFT OUTER JOIN dbo.AnalyzerOwner ANO
			ON ANOL.AnalyzerOwnerID = ANO.AnalyzerOwnerID

	LEFT OUTER JOIN dbo.DataSourceType DT
			ON ANOL.DataSourceTypeID = DT.DataSourceTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE ANOL.DataSourceTypeID = @DataSourceTypeID

ORDER BY ANOL.AnalyzerOwnerLocationName

END