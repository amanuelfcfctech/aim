CREATE PROCEDURE [dbo].[usp_AnalyzerNutrient_SEL_AnalyzerNutrientID_DataSourceTypeID]
		@AnalyzerNutrientID		INT
		, @DataSourceTypeID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  NN.*	
FROM 
	dbo.AnalyzerNutrient NN WITH(NOLOCK)
	
WHERE 
	NN.AnalyzerNutrientID = @AnalyzerNutrientID and NN.DataSourceTypeID = @DataSourceTypeID

END