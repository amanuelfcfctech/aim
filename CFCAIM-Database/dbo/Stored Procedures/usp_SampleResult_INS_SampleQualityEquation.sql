CREATE PROCEDURE [dbo].[usp_SampleResult_INS_SampleQualityEquation]
				  @SampleID			INT 
				, @CFCUserID		INT 
				, @AimDatabaseID	INT 
AS
BEGIN
SET NOCOUNT ON;

DECLARE @SampleQualityEquationID		INT
DECLARE @SampleQualityEquationName		NVARCHAR(50)
DECLARE @AnalyzerNutrientID				INT
DECLARE @DataSourceTypeID				INT
DECLARE @ValueString					VARCHAR(50)
DECLARE @AnalyzerNutrientCode			NVARCHAR(30)
DECLARE @SampleQualityParameterID		INT
DECLARE @ResultValueNumeric				NUMERIC(16,4)
DECLARE @Result							NUMERIC(16,4)
DECLARE @IsDiv							BIT	= 0
DECLARE @Formula						NVARCHAR(2000) = ''
DECLARE @LastSampleQualityEquationID	INT	= 0
DECLARE @SQL							NVARCHAR(3000) = ''
DECLARE @IsActive						BIT = 1
DECLARE @AnalysisDate					DATETIME = GETDATE()


DECLARE @ErrorMessage			VARCHAR(200) = ''
DECLARE @ResultID				INT = 0
DECLARE @ErrorCount				INT = 0

DECLARE 
	FormulaCur 
CURSOR FOR
	SELECT 
		  SQE.SampleQualityEquationID
		, SQE.SampleQualityEquationName 
		, SQE.AnalyzerNutrientID 
		, SQE.DataSourceTypeID 
		, SQEB.ValueString
		, SQEB.SampleQualityParameterID
		, SQ.ResultValueNumeric 
	FROM 
		dbo.SampleQualityEquation SQE
		INNER JOIN
			dbo.SampleQualityEquationBuild SQEB	
				ON SQEB.SampleQualityEquationID = SQE.SampleQualityEquationID
		LEFT OUTER JOIN
			dbo.SampleQuality SQ
				ON	SQEB.SampleQualityParameterID = SQ.SampleQualityParameterID 
				AND SQ.SampleID = @SampleID
				AND SQ.IsActive = 1
	WHERE 
		SQEB.Status = 1	
	ORDER BY 
			SQEB.SampleQualityEquationID 
		, SQEB.SampleQualityEquationBuildID 

OPEN FormulaCur
FETCH NEXT FROM
		FormulaCur
	INTO @SampleQualityEquationID, @SampleQualityEquationName, @AnalyzerNutrientID, @DataSourceTypeID, @ValueString, @SampleQualityParameterID, @ResultValueNumeric

WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @LastSampleQualityEquationID != @SampleQualityEquationID 
			BEGIN
				SET @LastSampleQualityEquationID = @SampleQualityEquationID
				IF @Formula != '' 
					BEGIN
						SET @SQL = N'SET @Result = ' + @Formula 
						EXEC sp_executesql @sql, N'@result NUMERIC(16,4) output', @result = @result OUTPUT
						IF @Result IS NOT NULL
							BEGIN
								SET @AnalyzerNutrientCode = LEFT(@SampleQualityEquationName, 30)
								EXEC usp_SampleResult_INS @SampleID, @AnalyzerNutrientID, @AnalyzerNutrientCode, @Result, @AnalysisDate, @IsActive, @CFCUserID, @DataSourceTypeID, @AimDatabaseID
								IF @@Error > 0
									SET @ErrorCount = @ErrorCount + 1
								
							END
					END
				SET @Formula = ''
			END
		IF @SampleQualityParameterID = 0 
			BEGIN
				IF @ValueString = '/'
					BEGIN
						SET @IsDiv = 1
					END
				ELSE
					BEGIN
						SET @IsDiv = 0
					END
				SET @Formula = @Formula + ' ' + @ValueString + ' '					
			END
		ELSE
			BEGIN
				IF @IsDiv = 1
					BEGIN
						SET @Formula = @Formula + N' NULLIF(' + CAST(@ResultValueNumeric AS VARCHAR) + ',0)'
					END
				ELSE
					BEGIN
						SET @Formula = @Formula + CAST(@ResultValueNumeric AS VARCHAR)
					END
				SET @IsDiv = 0
			END

		FETCH NEXT FROM
				FormulaCur
			INTO @SampleQualityEquationID, @SampleQualityEquationName, @AnalyzerNutrientID, @DataSourceTypeID, @ValueString, @SampleQualityParameterID, @ResultValueNumeric
	END

CLOSE FormulaCur
DEALLOCATE FormulaCur

IF @ErrorCount > 0
	SET @ErrorMessage = 'Error Applying Quality Control Equation to Results.'

SELECT 
	  @ResultID AS RecordID
	, @ErrorCount AS ErrorNumber
	, @ErrorMessage AS ErrorMessage


END