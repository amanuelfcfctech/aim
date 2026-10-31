CREATE PROCEDURE [dbo].[usp_SampleResult_INS_UnitiyForSampleQualityEquation]
      @CFCUserID  INT 
    , @AIMDatabaseID INT 
    , @ProgressBarID INT

	--11/4/22
	, @RebuildDate		VARCHAR(10)			--12/2/20

--exec dbo.usp_SampleResult_INS_UnitiyForSampleQualityEquation 1, 2, 1, '2022-10-1'


AS
BEGIN
SET NOCOUNT OFF;

DECLARE @SampleID  INT
DECLARE @ErrorMessage   VARCHAR(200) = ''
DECLARE @ResultID    INT = 0
DECLARE @ErrorCount    INT = 0
DECLARE @TotalCnt AS INT = 0
DECLARE @Cnt AS INT = 0
DECLARE @Str AS VARCHAR(100) = ''
DECLARE @Increment AS NUMERIC(18,4) = 1
DECLARE @Loop AS INT = 0 

IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##List%')
	DROP TABLE ##List


----Get increment for progress bar
SET @TotalCnt = (SELECT Count(*) 
    FROM (
     SELECT
      SQ.SampleID 
     FROM 
      dbo.SampleQuality SQ WITH(NOLOCK) 

	-- 11/4/22 - join sample table to get sample date
	INNER JOIN dbo.Sample S 
		ON S.SampleID = SQ.SampleID

    WHERE 
      SQ.IsActive = 1

	--11/4/22
	AND S.SampleDate >= @RebuildDate	


     GROUP BY 
      SQ.SampleID) T)
IF @TotalCnt <> 0
	SET @Increment = 95 / Cast(@TotalCnt as Numeric)


-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
--EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, 'Rerun Start', @CFCUserID, 0
EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, 'Rerun Start', @CFCUserID, 0, 0, 0

SELECT 
	SQ.SampleID

	-- 11/4/22
	, S.SampleDate

INTO 
	##List
FROM 
	dbo.SampleQuality SQ

-- 11/4/22 - join sample table to get sample date
INNER JOIN dbo.Sample S 
	ON S.SampleID = SQ.SampleID

WHERE
	SQ.IsActive = 1
	AND SQ.SampleID > 0	

	--11/4/22
	AND S.SampleDate >= @RebuildDate


GROUP BY 
	SQ.SampleID

	--11/4/22
	, S.SampleDate

order by
	sq.SampleID

SELECT 
	TOP 1 @SampleID = SampleID 
FROM ##List 
WHILE (SELECT 
			COUNT(*)
		FROM
			##List) > 0
	BEGIN		
		SET @Loop = @Loop + 1

		-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
		--EXEC dbo.usp_SampleResult_INS_SampleQualityEquation @SampleID, @CFCUserID, @AIMDatabaseID
		EXEC dbo.usp_SampleResult_INS_SampleQualityEquation @SampleID, @CFCUserID, @AIMDatabaseID, 0     

		IF @@Error > 0		
				SET @ErrorCount = @ErrorCount + 1   
				
		----Update progress bar 
		SET @Cnt = @Cnt + 1
		SET @Str = 'Rerun Sample Quality Equation ' + convert(varchar,@Cnt) + ' of ' + convert(varchar,@TotalCnt) 			

		--6/25/19
		--SET @Increment = @Cnt * 100 / Cast(@TotalCnt as Numeric)			
		SET @Increment = @Cnt  / Cast(@TotalCnt as Numeric)			

		-- 6/25/19 - added because of memory issues at Smithfield
		IF @Loop > 20					

			BEGIN				
				-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
				--EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, @Str, @CFCUserID, @Increment				   
				EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, @Str, @CFCUserID, @Increment, 0, 0

				SET @Loop = 0
			END

		DELETE 
			##List
		WHERE
			SampleID = @SampleID

		SELECT 
			TOP 1 @SampleID = SampleID 
		FROM 
			##List 	
					
	END

IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##List%')
	DROP TABLE ##List

-- 6/25/19 - added ReturnMessage parameter because of memory issues at Smithfield
--EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, 'Quality Equation Section Complete', @CFCUserID, 0
EXEC dbo.usp_ProgressBar_SAV_Increment @ProgressBarID, 'Quality Equation Section Complete', @CFCUserID, 0, 0, 0

IF @ErrorCount > 0	
		SET @ErrorMessage = 'Error Rebuilding and Applying Quality Control Equations to Results.'
	
SELECT 
   @ResultID AS RecordID
 , @ErrorCount AS ErrorNumber
 , @ErrorMessage AS ErrorMessage


END