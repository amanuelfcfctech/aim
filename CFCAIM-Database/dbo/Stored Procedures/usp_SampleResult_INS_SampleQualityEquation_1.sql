CREATE PROCEDURE [dbo].[usp_SampleResult_INS_SampleQualityEquation]
				  @SampleID			INT 
				, @CFCUserID		INT 
				, @AimDatabaseID	INT 

				-- 6/25/19 - added because of memory issues at Smithfield
				, @ReturnMessage	BIT

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SampleQualityEquationID		INT
DECLARE @SampleQualityEquationName		NVARCHAR(50)
DECLARE @AnalyzerNutrientID				INT
DECLARE @DataSourceTypeID				INT
DECLARE @ValueString					VARCHAR(50)
DECLARE @AnalyzerNutrientCode			NVARCHAR(100)
DECLARE @SampleQualityParameterID		INT
DECLARE @ResultValueNumeric				NUMERIC(16,4)
DECLARE @Result							NUMERIC(16,4)
DECLARE @IsDiv							BIT	= 0
DECLARE @Formula						NVARCHAR(2000) = ''
DECLARE @LastSampleQualityEquationID	INT	= 0
DECLARE @SQL							NVARCHAR(3000) = ''
DECLARE @IsActive						BIT = 1
DECLARE @AnalysisDate					DATETIME = GETDATE()
DECLARE @PrevAnalyzerNutrientID			INT	= 0
DECLARE @ErrorMessage					VARCHAR(200) = ''
DECLARE @ResultID						INT = 0
DECLARE @ErrorCount						INT = 0

DECLARE @SampleQualityEquationBuildID		INT

IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '#Equation%')
	DROP TABLE #Equation

SELECT 
	  SQE.SampleQualityEquationID
	, SQE.SampleQualityEquationName 
	, SQE.AnalyzerNutrientID 
	, SQE.DataSourceTypeID 
	, SQEB.ValueString
	, SQEB.SampleQualityParameterID
	, SQ.ResultValueNumeric 
	, SQEB.SampleQualityEquationBuildID

INTO
	#Equation

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

	SELECT
		TOP 1 @SampleQualityEquationID = SampleQualityEquationID
			, @SampleQualityEquationName = SampleQualityEquationName
			, @AnalyzerNutrientID = AnalyzerNutrientID
			, @DataSourceTypeID = DataSourceTypeID
			, @ValueString = ValueString
			, @SampleQualityParameterID = SampleQualityParameterID
			, @ResultValueNumeric = ResultValueNumeric
			, @SampleQualityEquationBuildID = SampleQualityEquationBuildID
		FROM
			#Equation
		ORDER BY 
			SampleQualityEquationID 
		  , SampleQualityEquationBuildID 

	WHILE (SELECT 
				COUNT(*)
			FROM 
				#Equation) > 0

	BEGIN
		
		IF @PrevAnalyzerNutrientID = 0
			BEGIN				
				SET @PrevAnalyzerNutrientID = @AnalyzerNutrientID				
			END

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
								
								-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
								--EXEC usp_SampleResult_INS @SampleID=@SampleID, @AnalyzerNutrientID=@PrevAnalyzerNutrientID, @AnalyzerNutrientCode=@AnalyzerNutrientCode, @ResultValue=@Result, @AnalysisDate=@AnalysisDate, @IsActive=@IsActive, @CFCUserID=@CFCUserID, @DataSourceTypeID=@DataSourceTypeID, @AimDatabaseID=@AimDatabaseID																
								EXEC usp_SampleResult_INS @SampleID=@SampleID, @AnalyzerNutrientID=@PrevAnalyzerNutrientID, @AnalyzerNutrientCode=@AnalyzerNutrientCode, @ResultValue=@Result, @AnalysisDate=@AnalysisDate, @IsActive=@IsActive, @CFCUserID=@CFCUserID, @DataSourceTypeID=@DataSourceTypeID, @AimDatabaseID=@AimDatabaseID, @ReturnMessage=0

								SET @PrevAnalyzerNutrientID = @AnalyzerNutrientID														
								IF @@Error > 0
									SET @ErrorCount = @ErrorCount + 1								
							END
				
					END
				
				SET @PrevAnalyzerNutrientID = @AnalyzerNutrientID
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


		DELETE 
			#Equation
		WHERE
			SampleQualityEquationID = @SampleQualityEquationID and
			SampleQualityEquationBuildID = @SampleQualityEquationBuildID
			

		SELECT 
			TOP 1 @SampleQualityEquationID = SampleQualityEquationID
			, @SampleQualityEquationName = SampleQualityEquationName
			, @AnalyzerNutrientID = AnalyzerNutrientID
			, @DataSourceTypeID = DataSourceTypeID
			, @ValueString = ValueString
			, @SampleQualityParameterID = SampleQualityParameterID
			, @ResultValueNumeric = ResultValueNumeric
			, @SampleQualityEquationBuildID = SampleQualityEquationBuildID

		FROM 
			#Equation
		ORDER BY 
			SampleQualityEquationID 
		  , SampleQualityEquationBuildID  		  			  	

	END
	

	IF @PrevAnalyzerNutrientID > 0
		BEGIN
			
			SET @LastSampleQualityEquationID = @SampleQualityEquationID
			IF @Formula != '' 
				BEGIN

					SET @SQL = N'SET @Result = ' + @Formula 
					EXEC sp_executesql @sql, N'@result NUMERIC(16,4) output', @result = @result OUTPUT					
					IF @Result IS NOT NULL
						BEGIN
							SET @AnalyzerNutrientCode = LEFT(@SampleQualityEquationName, 30)																					

							-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
							--EXEC usp_SampleResult_INS @SampleID=@SampleID, @AnalyzerNutrientID=@AnalyzerNutrientID, @AnalyzerNutrientCode=@AnalyzerNutrientCode, @ResultValue=@Result, @AnalysisDate=@AnalysisDate, @IsActive=@IsActive, @CFCUserID=@CFCUserID, @DataSourceTypeID=@DataSourceTypeID, @AimDatabaseID=@AimDatabaseID																						
							EXEC usp_SampleResult_INS @SampleID=@SampleID, @AnalyzerNutrientID=@AnalyzerNutrientID, @AnalyzerNutrientCode=@AnalyzerNutrientCode, @ResultValue=@Result, @AnalysisDate=@AnalysisDate, @IsActive=@IsActive, @CFCUserID=@CFCUserID, @DataSourceTypeID=@DataSourceTypeID, @AimDatabaseID=@AimDatabaseID, @ReturnMessage=0

							IF @@Error > 0
								SET @ErrorCount = @ErrorCount + 1								
						END
				END		
		END

IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '#Equation%')
	DROP TABLE #Equation


IF @ErrorCount > 0	
	SET @ErrorMessage = 'Error Applying Quality Control Equation to Results.'
	
	
-- 6/25/19 - added because of memory issues at Smithfield
IF @ReturnMessage = 1
	BEGIN

		SELECT 
			  @ResultID AS RecordID
			, @ErrorCount AS ErrorNumber
			, @ErrorMessage AS ErrorMessage
	END

END