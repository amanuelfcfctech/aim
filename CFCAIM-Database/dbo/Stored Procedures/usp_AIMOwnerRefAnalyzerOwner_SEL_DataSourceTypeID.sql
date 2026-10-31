CREATE PROCEDURE [dbo].[usp_AIMOwnerRefAnalyzerOwner_SEL_DataSourceTypeID]
@DataSourceTypeID				INT = NULL

AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMOwnerRefAnalyzerOwner_SEL_DataSourceTypeID 11


SELECT
	    XREF.AIMOwnerRefAnalyzerOwnerID
	  , XREF.AnalyzerOwnerID	 
	  , XREF.UpdateStamp
	  , AO.AIMOwnerID
	  , AO.OwnerCode
	  , AO.OwnerName	  
	  , ANO.AnalyzerOwnerCode
	  , ANO.AnalyzerOwnerName	  	  
	  , DT.Name as DataSourceUserName

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
	  
FROM 
	dbo.AIMOwnerRefAnalyzerOwner XREF WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMOwner AO
			ON XREF.AIMOwnerID = AO.AIMOwnerID
	LEFT OUTER JOIN	dbo.AnalyzerOwner ANO
			ON XREF.AnalyzerOwnerID = ANO.AnalyzerOwnerID
	LEFT OUTER JOIN dbo.DataSourceType DT
			ON ANO.DataSourceTypeID = DT.DataSourceTypeID

	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE ANO.DataSourceTypeID = @DataSourceTypeID

ORDER BY ANO.AnalyzerOwnerName	

END