CREATE PROCEDURE [dbo].[usp_SampleAim_UPD_Priority_test]
	  @AIMNutrientID				INT
	, @AIMPriorityUsedValueID		INT
	, @DatabaseTypeID				INT = NULL

--EXEC usp_SampleAim_UPD_Priority 4, 2, 0

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber	INT = 0
DECLARE @RecordID		INT = 0 
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @SampleID		INT = 0

Declare @i Int;
Declare @msg VarChar(50);

Set @i = 0;
    while (@i < 100) 
	Begin
	    ------WaitFor Delay '00:00:01';	      

		-- delete records in SampleAIM for this aimnutrientid
		DELETE FROM dbo.SampleAIM with (rowlock) WHERE AIMNutrientID = @AIMNutrientID 
		--and SampleID = 5557

		---------------------------------------------------------------------------------------
		-- get sampleIDs			
		DECLARE TEMPCUR SCROLL CURSOR FOR
		SELECT SampleID FROM dbo.SampleResult SR with (nolock)
			INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient X with (nolock) on SR.AnalyzerNutrientID = X.AnalyzerNutrientID
			WHERE X.AIMNutrientID = @AIMNutrientID	--and SR.SampleID = 5557	
			GROUP BY SR.SampleID
			ORDER BY SR.SampleID		

		---------------------------------------------------------------------------------------
		IF @AIMPriorityUsedValueID = 2  -- (first in)
		Begin
	
			OPEN TEMPCUR	
			FETCH NEXT FROM TEMPCUR INTO @SampleID
			WHILE @@FETCH_STATUS = 0	
				Begin

					Set @i = @i + 20;
					Set @msg = Convert(VarChar(10), @i) + ' PERCENT COMPLETE';		
					RaisError(@msg, 1, 1) With NoWait

					INSERT INTO dbo.SampleAIM (SampleID, SampleResultID, AIMNutrientID, AIMUsedValue, UpdateStamp, CFCUserID)		
						SELECT TOP 1 SR.SampleID, SR.SampleResultID, @AIMNutrientID, SR.ResultValue, SR.UpdateStamp, SR.CFCUserID
						FROM dbo.SampleResult SR with (nolock)
						INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient ANRAN with (nolock) on SR.AnalyzerNutrientID = ANRAN.AnalyzerNutrientID
						WHERE ANRAN.AIMNutrientID = @AIMNutrientID and SampleID = @SampleID 
						ORDER BY SR.UpdateStamp
					SET @RecordID = @@IDENTITY
					FETCH NEXT FROM TEMPCUR INTO @SampleID
				End				
			CLOSE TEMPCUR
			
		End

		---------------------------------------------------------------------------------------
		IF @AIMPriorityUsedValueID = 3  -- (last in)
		Begin
	
			OPEN TEMPCUR
			FETCH NEXT FROM TEMPCUR INTO @SampleID
			WHILE @@FETCH_STATUS = 0	
				Begin

					Set @i = @i + 20;
					Set @msg = Convert(VarChar(10), @i) + ' PERCENT COMPLETE';		
					RaisError(@msg, 1, 1) With NoWait

					INSERT INTO dbo.SampleAIM (SampleID, SampleResultID, AIMNutrientID, AIMUsedValue, UpdateStamp, CFCUserID)		
						SELECT TOP 1 SR.SampleID, SR.SampleResultID, @AIMNutrientID, SR.ResultValue, SR.UpdateStamp, SR.CFCUserID
						FROM dbo.SampleResult SR with (nolock)
						INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient ANRAN with (nolock) on SR.AnalyzerNutrientID = ANRAN.AnalyzerNutrientID
						WHERE ANRAN.AIMNutrientID = @AIMNutrientID and SampleID = @SampleID 
						ORDER BY SR.UpdateStamp DESC 
					SET @RecordID = @@IDENTITY
					FETCH NEXT FROM TEMPCUR INTO @SampleID
				End				
			CLOSE TEMPCUR
			
		End

		---------------------------------------------------------------------------------------
		IF @AIMPriorityUsedValueID = 4  -- (select datasource)
		Begin
		OPEN TEMPCUR
			FETCH NEXT FROM TEMPCUR INTO @SampleID
			WHILE @@FETCH_STATUS = 0
				Begin

					Set @i = @i + 20;
					Set @msg = Convert(VarChar(10), @i) + ' PERCENT COMPLETE';		
					RaisError(@msg, 1, 1) With NoWait

					INSERT INTO dbo.SampleAIM (SampleID, SampleResultID, AIMNutrientID, AIMUsedValue, UpdateStamp, CFCUserID)		
						SELECT TOP 1 SR.SampleID, SR.SampleResultID, @AIMNutrientID, SR.ResultValue, SR.UpdateStamp, SR.CFCUserID
						FROM dbo.SampleResult SR with (nolock)
						INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient ANRAN with (nolock) on SR.AnalyzerNutrientID = ANRAN.AnalyzerNutrientID
						INNER JOIN dbo.AnalyzerNutrient AN with (nolock) on ANRAN.AnalyzerNutrientID = AN.AnalyzerNutrientID			
						WHERE ANRAN.AIMNutrientID = @AIMNutrientID and SR.SampleID = @SampleID and AN.DataSourceTypeID = @DatabaseTypeID
						ORDER BY SR.UpdateStamp DESC
					SET @RecordID = @@IDENTITY
					FETCH NEXT FROM TEMPCUR INTO @SampleID
				End				
			CLOSE TEMPCUR
			
		End

		---------------------------------------------------------------------------------------
		DEALLOCATE TempCur
		set @i = 100;
		SELECT 
			  @@IDENTITY as RecordID
			, @ErrorNumber AS ErrorNumber
			, @ErrorMessage AS ErrorMessage		
		
    End

END