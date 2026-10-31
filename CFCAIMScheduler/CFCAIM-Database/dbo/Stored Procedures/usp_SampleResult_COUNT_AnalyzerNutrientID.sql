CREATE PROCEDURE [dbo].[usp_SampleResult_COUNT_AnalyzerNutrientID]
	  @AnalyzerNutrientID	INT

-- exec usp_SampleResult_COUNT_AnalyzerNutrientID 1
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @ReturnIntegerValue INT = 0


SET @ReturnIntegerValue = 
 
(SELECT count(*) as cnt
FROM
	dbo.SampleResult SR

INNER JOIN 
	dbo.AnalyzerNutrient AN WITH(NOLOCK)
		ON  AN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
			AND AN.IsActive = 1	
WHERE
	SR.AnalyzerNutrientID = @AnalyzerNutrientID) 


SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue AS ReturnIntegerValue

END