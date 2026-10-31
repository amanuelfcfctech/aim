CREATE PROCEDURE [dbo].[usp_AnalyzerMaterial_DEL_AnalyzerMaterialID]
	  @ID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AnalyzerMaterial	WITH(ROWLOCK)
WHERE 
	AnalyzerMaterialID = @ID
END