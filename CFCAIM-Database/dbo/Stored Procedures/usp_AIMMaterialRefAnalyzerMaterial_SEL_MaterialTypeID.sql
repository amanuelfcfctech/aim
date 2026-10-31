CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SEL_MaterialTypeID]
@MaterialTypeID INT

AS
BEGIN
SET NOCOUNT ON;

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
	  , U.UserName

FROM 
	dbo.AIMMaterialRefAnalyzerMaterial XREF WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMMaterial A 
			ON XREF.AIMMaterialID = A.AIMMaterialID
	LEFT OUTER JOIN	dbo.AnalyzerMaterial N	
			ON XREF.AnalyzerMaterialID = N.AnalyzerMaterialID
	LEFT OUTER JOIN 	CFCUser.CFCUser U WITH (NOLOCK) 
			ON XREF.CFCUserID = U.CFCUserID

WHERE A.MaterialTypeID = @MaterialTypeID

END