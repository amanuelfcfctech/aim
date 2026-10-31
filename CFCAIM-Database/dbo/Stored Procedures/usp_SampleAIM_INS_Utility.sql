CREATE PROCEDURE [dbo].[usp_SampleAIM_INS_Utility]
	  @ProgressBarID	INT
	, @CFCUserID		INT	
	--, @RebuildDate		VARCHAR(8)		--6/27/19 - added for Smithfield
	, @RebuildDate		VARCHAR(10)			--12/2/20 - changed to 10 (CSA was getting date conversion error)

--exec dbo.usp_SampleAIM_INS_Utility 72, 1, '1753-1-1'
--exec dbo.usp_SampleAIM_INS_Utility 72, 1, '2019-4-1'

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SampleID		INT
DECLARE @SampleResultID INT
DECLARE @AIMNutrientID	INT
DECLARE @ResultValue	NUMERIC(18,9)
DECLARE @TotalCnt		INT = 0
DECLARE @Cnt			INT = 0
DECLARE @Str			VARCHAR(100) = ''
DECLARE @Increment		NUMERIC(18,4) = 1
DECLARE @Loop			INT = 0 

---------------------------------------------------------------------------------------------
--Progress Bar 
SET @TotalCnt = (SELECT Count(*) 
		FROM 
			dbo.Sample S WITH(NOLOCK)
		INNER JOIN 
			dbo.SampleResult SR WITH(NOLOCK) ON SR.SampleID = S.SampleID 
		INNER JOIN 
			dbo.AIMNutrientRefAnalyzerNutrient ANRAN WITH(NOLOCK) ON ANRAN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
		WHERE SR.IsActive = 1)

IF @TotalCnt <> 0
	SET @Increment = 100 / Cast(@TotalCnt as Numeric)

DECLARE @strIncrement varchar(50)
SET @strIncrement = convert(varchar(50), @Increment)

-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
--EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, 'Rebuilding ...', @CFCUserID, 0
EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, 'Rebuilding ...', @CFCUserID, 0, 0, 0
---------------------------------------------------------------------------------------------


DECLARE db_cursor 
CURSOR FOR  
	SELECT 
		  S.SampleID 
		, SR.SampleResultID 
		, ANRAN.AIMNutrientID 
		, SR.ResultValue 
	FROM
		dbo.Sample S WITH(NOLOCK)
		INNER JOIN 
			dbo.SampleResult SR WITH(NOLOCK)
				ON SR.SampleID = S.SampleID 
		INNER JOIN 
			dbo.AIMNutrientRefAnalyzerNutrient ANRAN WITH(NOLOCK)
				ON ANRAN.AnalyzerNutrientID = SR.AnalyzerNutrientID 
	WHERE
		SR.IsActive = 1

		--AND S.SampleDate > '2018-01-01'
		AND S.SampleDate >= @RebuildDate

	ORDER BY
		  S.SampleID 
		, SR.AnalyzerNutrientID
		, SR.AnalysisDate  

OPEN db_cursor   
FETCH NEXT FROM 
	db_cursor 
INTO 
	  @SampleID 
	, @SampleResultID 
	, @AIMNutrientID 
	, @ResultValue    

WHILE @@FETCH_STATUS = 0   
	BEGIN  
		
		SET @Loop = @Loop + 1
		DELETE
			dbo.SampleAIM 
		WHERE
				SampleID = @SampleID
			AND AIMNutrientID = @AIMNutrientID 

		INSERT INTO 
			dbo.SampleAIM 
				(
				  SampleID 
				, SampleResultID 
				, AIMNutrientID 
				, AIMUsedValue  
				, UpdateStamp 
				, CFCUserID 
				, IsActive  
				)
		VALUES
				(
				  @SampleID 
				, @SampleResultID 
				, @AIMNutrientID 
				, @ResultValue  
				, GETDATE()
				, 0
				, 1
				)

        ---------------------------------------------------------------------------------------------
		--Update Progress Bar table	
		SET @Cnt = @Cnt + 1	
		SET @Str = 'Rebuilding ' + convert(varchar,@Cnt) + ' of ' + convert(varchar,@TotalCnt) 

		-- 6/25/19 - added loop because of memory issues at Smithfield
		IF @Loop > 200
			BEGIN

				-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
				--EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, @Str, @CFCUserID, @Increment
				EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, @Str, @CFCUserID, @Increment, 0, 0

				SET @Loop = 0
			END

		FETCH NEXT FROM 
			db_cursor 
		INTO 
			  @SampleID 
			, @SampleResultID 
			, @AIMNutrientID 
			, @ResultValue
		---------------------------------------------------------------------------------------------
		    
	END   

-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
--EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, 'Rebuild Complete', @CFCUserID, 0
EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, 'Rebuild Complete', @CFCUserID, 0, 0, 0


CLOSE db_cursor   
DEALLOCATE db_cursor 


END