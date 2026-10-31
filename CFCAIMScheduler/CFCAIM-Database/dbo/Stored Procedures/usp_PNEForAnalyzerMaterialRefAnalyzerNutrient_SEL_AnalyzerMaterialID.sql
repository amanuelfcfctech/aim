CREATE PROCEDURE [dbo].[usp_PNEForAnalyzerMaterialRefAnalyzerNutrient_SEL_AnalyzerMaterialID]
			  @AnalyzerMaterialID			INT		
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  PFAMRAN.PNEForAnalyzerMaterialRefAnalyzerNutrientID
    , PFAMRAN.AnalyzerMaterialID
	, AM.AnalyzerMaterialCode
    , PFAMRAN.AnalyzerNutrientID
	, AN.AnalyzerNutrientCode
	, PFAMRAN.IsActive
	, PFAMRAN.UpdateStamp
	, PFAMRAN.CFCUserID
	, U.UserName 
FROM 
	dbo.PNEForAnalyzerMaterialRefAnalyzerNutrient PFAMRAN
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON U.CFCUserID = PFAMRAN.CFCUserID
	LEFT OUTER JOIN
		dbo.AnalyzerMaterial AM
			ON AM.AnalyzerMaterialID = PFAMRAN.AnalyzerMaterialID
	LEFT OUTER JOIN
		dbo.AnalyzerNutrient AN
			ON AN.AnalyzerNutrientID = PFAMRAN.AnalyzerNutrientID
WHERE
	PFAMRAN.AnalyzerMaterialID = @AnalyzerMaterialID

END