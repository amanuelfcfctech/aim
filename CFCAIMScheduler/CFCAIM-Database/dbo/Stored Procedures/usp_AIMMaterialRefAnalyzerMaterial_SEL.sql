CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SEL]
AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMMaterialRefAnalyzerMaterial_SEL_All

SELECT
	    XREF.AIMMaterialRefAnalyzerMaterialID
	  , XREF.AIMMaterialID
	  , XREF.AnalyzerMaterialID
	  , XREF.UpdateStamp
	  , A.AIMMaterialCode
	  , A.AIMMaterialName
	  , N.AnalyzerMaterialCode
	  , N.AnalyzerMaterialName
	  , N.DataSourceTypeID	  
	  , DST.Name as DataSourceTypeName	  

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 
	dbo.AIMMaterialRefAnalyzerMaterial XREF WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMMaterial A with (nolock)
			ON XREF.AIMMaterialID = A.AIMMaterialID
	LEFT OUTER JOIN	dbo.AnalyzerMaterial N	with (nolock)
			ON XREF.AnalyzerMaterialID = N.AnalyzerMaterialID
	LEFT OUTER JOIN dbo.DataSourceType DST with (nolock)
			ON N.DataSourceTypeID = DST.DataSourceTypeID	
	LEFT OUTER JOIN 	CFCUser.CFCUser U WITH (NOLOCK) 
			ON XREF.CFCUserID = U.CFCUserID

    -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN 	dbo.aspnet_Users AU WITH (NOLOCK)	
	--		ON AU.UserId = U.UserID

END