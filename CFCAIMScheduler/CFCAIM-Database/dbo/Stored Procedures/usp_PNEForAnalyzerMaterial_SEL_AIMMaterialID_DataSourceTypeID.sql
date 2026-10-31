CREATE PROCEDURE [dbo].[usp_PNEForAnalyzerMaterial_SEL_AIMMaterialID_DataSourceTypeID]
			  @AIMMaterialID			INT	
			  , @DataSourceTypeID		INT

--usp_PNEForAnalyzerMaterial_SEL_AIMMaterialID 6, 11

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 

	ANM.AnalyzerMaterialID,
	ANM.DataSourceTypeID,
	ANM.AnalyzerMaterialCode,
	ANM.AnalyzerMaterialName,
	ANM.IsActive,
	ANM.UpdateStamp,
	ANM.CFCUserID

FROM
	dbo.AIMMaterial AM

INNER JOIN 
	dbo.AIMMaterialRefAnalyzerMaterial X 
		ON AM.AIMMaterialID = X.AIMMaterialID
INNER JOIN
	dbo.AnalyzerMaterial ANM
		ON X.AnalyzerMaterialID = ANM.AnalyzerMaterialID			
INNER JOIN 
	dbo.PNEMaterialType PMT
		ON ANM.AnalyzerMaterialID = PMT.AnalyzerMaterialID
INNER JOIN	
	dbo.PNEMaterialSampleType PMST
		ON PMT.PNEMaterialSampleTypeID = PMST.PNEMaterialSampleTypeID

WHERE
	AM.AIMMaterialID = @AIMMaterialID and
	ANM.DataSourceTypeID = @DataSourceTypeID

END