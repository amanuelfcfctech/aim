CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SEL_AnalyzerMaterialID]
@AnalyzerMaterialID INT

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	    XREF.AIMMaterialRefAnalyzerMaterialID
	  , XREF.AIMMaterialID
	  , XREF.AnalyzerMaterialID
	  , XREF.UpdateStamp
	  , A.AIMMaterialCode
	  , A.AIMMaterialName
	  , N.AnalyzerMaterialCode
	  , N.AnalyzerMaterialName
	  , N.DataSourceTypeID

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 
	dbo.AIMMaterialRefAnalyzerMaterial XREF WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMMaterial A 
			ON XREF.AIMMaterialID = A.AIMMaterialID
	LEFT OUTER JOIN	dbo.AnalyzerMaterial N	
			ON XREF.AnalyzerMaterialID = N.AnalyzerMaterialID
	LEFT OUTER JOIN 	CFCUser.CFCUser U WITH (NOLOCK) 
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN 	dbo.aspnet_Users AU WITH (NOLOCK)	
	--		ON AU.UserId = U.UserID

WHERE N.AnalyzerMaterialID = @AnalyzerMaterialID

END