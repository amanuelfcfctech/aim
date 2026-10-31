CREATE PROCEDURE [dbo].[usp_AnalyzerNutrient_SEL_AnalyzerNutrientID_DataSourceTypeID_AnalyzerNutrientCode]
		@DataSourceTypeID		INT

	  --10/30/18
	  --, @AnalyzerNutrientCode	VARCHAR(30)
	  , @AnalyzerNutrientCode	VARCHAR(100)

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  NN.*	
FROM 
	dbo.AnalyzerNutrient NN
	
WHERE 
	NN.DataSourceTypeID = @DataSourceTypeID AND
	NN.AnalyzerNutrientCode = @AnalyzerNutrientCode 

	--5/4/18 commented
	--AND 	NN.IsActive = 1
	

END