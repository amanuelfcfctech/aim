
CREATE PROCEDURE [dbo].[usp_Sample_SEL_Chart]
						  @StartDate		DATETIME	= NULL
						, @EndDate			DATETIME	= NULL
						, @AIMNutrientID	INT			= NULL
						, @AIMMaterialID	INT			= NULL
AS
BEGIN
SET NOCOUNT ON;

SET	@StartDate = '2014-12-01'
SET	@EndDate = '2015-01-22'
SET @AIMMaterialID = 4
SET @AIMNutrientID = 3

CREATE TABLE #Temp
	(
	  IsCalculate					BIT	NULL
	, CFCIngredientName				NVARCHAR(50) NULL
	, CurrentNutrientVal			NUMERIC(12,4) NULL
	, SampleValue					NUMERIC(12,4) NULL
	, CFCIngredientCode				NVARCHAR(50) NULL
	, Unit							NVARCHAR(50) NULL
	, AIMMaterialID					INT NULL
	, AIMMaterialCode				NVARCHAR(50) NULL
	, AIMMaterialName				NVARCHAR(50) NULL
	, AIMNutrientID					INT NULL
	, AIMNutrientCode				NVARCHAR(50) NULL
	, AIMNutrientName				NVARCHAR(50) NULL
	, CFCNutrientCode				NVARCHAR(50) NULL
	, CFCNutrientName				NVARCHAR(50) NULL
	, NumberOfSample				INT NULL
	, SampleDate					DATETIME	  NULL
	, Quartile1						NUMERIC(12,4) NULL
	, Quartile3						NUMERIC(12,4) NULL
	, Mean							NUMERIC(12,4) NULL
	, StandardDeviation				NUMERIC(12,4) NULL
	, Min							NUMERIC(12,4) NULL
	, Max							NUMERIC(12,4) NULL
	, Upper95Mean					NUMERIC(12,4) NULL
	, Lower95Mean					NUMERIC(12,4) NULL
	, Upper90Mean					NUMERIC(12,4) NULL	
	, Lower90Mean					NUMERIC(12,4) NULL
	, MeanPlusStandardDeviation		NUMERIC(12,4) NULL
	, MeanMinusStandardDeviation	NUMERIC(12,4) NULL
	)

INSERT #Temp
EXEC dbo.usp_Sample_SEL_GroupBy_Date @StartDate, @EndDate 

SELECT  
	  AIMMaterialID 
	, AIMNutrientID
	, 'Min' AS ValueName
	, SampleDate
	, Min AS Value
FROM 
	#Temp T
WHERE 
	T.IsCalculate = 1
	AND AIMMaterialID = @AIMMaterialID
	AND AIMNutrientID = @AIMNutrientID
UNION ALL
SELECT  
	  AIMMaterialID 
	, AIMNutrientID
	, 'Max' AS ValueName
	, SampleDate
	, Max AS Value
FROM 
	#Temp T
WHERE 
	T.IsCalculate = 1
	AND AIMMaterialID = @AIMMaterialID
	AND AIMNutrientID = @AIMNutrientID
UNION ALL
SELECT  
	  AIMMaterialID 
	, AIMNutrientID
	, 'Mean' AS ValueName
	, SampleDate
	, Mean AS Value
FROM 
	#Temp T
WHERE 
	T.IsCalculate = 1
	AND AIMMaterialID = @AIMMaterialID
	AND AIMNutrientID = @AIMNutrientID
UNION ALL
SELECT  
	  AIMMaterialID 
	, AIMNutrientID
	, AIMNutrientName AS ValueName
	, SampleDate
	, SampleValue AS Value
FROM 
	#Temp T
WHERE 
	T.IsCalculate = 1
	AND AIMMaterialID = @AIMMaterialID
	AND AIMNutrientID = @AIMNutrientID

DROP TABLE #Temp 

END
