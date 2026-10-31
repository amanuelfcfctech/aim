CREATE PROCEDURE [dbo].[usp_SampleAim_UPD_Priority]
	--  @AimNutrientID				INT
	--, @AimPriorityUsedValueID		INT
	--, @DatabaseTypeID				INT 
	--, @ProgressBarID				INT 
	--, @ProgressBarCFCUserID			INT 

--exec usp_SampleAim_UPD_Priority 
--exec usp_SampleAim_UPD_Priority 4, 2, 0, 1064, 0

AS
BEGIN

-- hard-coded parameters for TESTING 
DECLARE  @AimNutrientID				INT = 4
DECLARE  @AimPriorityUsedValueID	INT = 2
DECLARE  @DatabaseTypeID			INT = 0
DECLARE  @ProgressBarID				INT = 1064
DECLARE  @ProgressBarCFCUserID		INT = 0
--------------------------------------------


SET NOCOUNT ON;

DECLARE @ErrorNumber	INT = 0
DECLARE @RecordID		INT = 0 
DECLARE @ErrorMessage	VARCHAR(200) = ''

DECLARE @SampleID		INT = 0
DECLARE @SampleResultID INT = 0
DECLARE @ResultValue	DECIMAL(18,8) 
DECLARE @UpdateStamp	DATETIME 
DECLARE @CFCUserID		INT = 0

DECLARE @HoldSampleID	    INT = 0
DECLARE @HoldSampleResultID INT = 0
DECLARE @HoldResultValue	DECIMAL(18,8) 
DECLARE @HoldUpdateStamp	DATETIME 
DECLARE @HoldCFCUserID		INT = 0

DECLARE @i int
DECLARE @count int = 500
DECLARE @msg VarChar(50)

EXEC dbo.usp_ProgressBar_SAV @ProgressBarID, 'PriorityUpdateBegin', @ProgressBarCFCUserID	

SET @i = 0;
    WHILE (@i < @count) 
	BEGIN			
			
		DELETE FROM dbo.SampleAIM with (rowlock) WHERE AIMNutrientID = @AimNutrientID 	

		IF @AimPriorityUsedValueID IN (2,3)
		BEGIN					
			DECLARE TEMPCUR SCROLL CURSOR FOR		
			SELECT SR.SampleID, SR.SampleResultID, SR.ResultValue, SR.UpdateStamp, SR.CFCUserID 					
				FROM dbo.SampleResult SR with (nolock)
				INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient ANRAN with (nolock) on SR.AnalyzerNutrientID = ANRAN.AnalyzerNutrientID						
				WHERE ANRAN.AIMNutrientID = @AimNutrientID 	--and sr.sampleid in (4184, 4185)										
				ORDER BY SR.SampleID, SR.UpdateStamp									
			OPEN TEMPCUR	
			FETCH NEXT FROM TEMPCUR INTO @SampleID, @SampleResultID, @ResultValue, @UpdateStamp, @CFCUserID			
			WHILE @@FETCH_STATUS = 0	
				Begin						
					------------------------------------------------------------								
					If @AimPriorityUsedValueID = 2 
						Begin							
							If @HoldSampleID <> @SampleID 
							Begin	
								if @i = 10
									begin
										EXEC dbo.usp_ProgressBar_SAV @ProgressBarID, 'Updating2-3', 2		
										SET @i = 0						
									end
								Set @i = @i + 1
								----Set @msg = Convert(VarChar(10), @i) + ' PERCENT COMPLETE'		
								RaisError(@msg, 1, 1) With NoWait								

															
								INSERT INTO dbo.SampleAIM (SampleID, SampleResultID, AIMNutrientID, AIMUsedValue, UpdateStamp, CFCUserID)		
								VALUES (@SampleID, @SampleResultID, @AimNutrientID, @ResultValue, @UpdateStamp, @CFCUserID)	
								SET @RecordID = @SampleID
								SET @HoldSampleID = @SampleID								
							End				
						End					
					------------------------------------------------------------	
					If @AimPriorityUsedValueID = 3 
						Begin					
							If @HoldSampleID <> @SampleID						
							Begin
								INSERT INTO dbo.SampleAIM (SampleID, SampleResultID, AIMNutrientID, AIMUsedValue, UpdateStamp, CFCUserID)		
								VALUES (@HoldSampleID, @HoldSampleResultID, @AimNutrientID, @HoldResultValue, @HoldUpdateStamp, @HoldCFCUserID)
								SET @RecordID = @HoldSampleID
							End	
							SET @HoldSampleID = @SampleID	
							SET @HoldSampleResultID = @SampleResultID
							SET @HoldResultValue = @ResultValue
							SET @HoldUpdateStamp = @UpdateStamp	
							SET @HoldCFCUserID = @CFCUserID						
						End						
														
				FETCH NEXT FROM TEMPCUR INTO @SampleID, @SampleResultID, @ResultValue, @UpdateStamp, @CFCUserID	
				End				
			CLOSE TEMPCUR				
			DEALLOCATE TempCur	
			SET @i = @count
			
		END

		------------------------------------------------------------------------
		IF @AimPriorityUsedValueID = 4
		BEGIN	

			DECLARE TEMPCUR SCROLL CURSOR FOR		
			SELECT  SR.SampleID, SR.SampleResultID, SR.ResultValue, SR.UpdateStamp, SR.CFCUserID					
				FROM dbo.SampleResult SR with (nolock)
				INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient ANRAN with (nolock) on SR.AnalyzerNutrientID = ANRAN.AnalyzerNutrientID						
				INNER JOIN dbo.AnalyzerNutrient AN with (nolock) on ANRAN.AnalyzerNutrientID = AN.AnalyzerNutrientID	
				WHERE ANRAN.AIMNutrientID = @AimNutrientID and AN.DataSourceTypeID = @DatabaseTypeID												
				ORDER BY SR.SampleID, SR.UpdateStamp									
			OPEN TEMPCUR	
			FETCH NEXT FROM TEMPCUR INTO @SampleID, @SampleResultID, @ResultValue, @UpdateStamp, @CFCUserID			
			WHILE @@FETCH_STATUS = 0	
				Begin																										
					If @HoldSampleID <> @SampleID						
					Begin						
						if @i = 10
							begin
								EXEC dbo.usp_ProgressBar_SAV @ProgressBarID, 'Updating2-3', 2		
								SET @i = 0						
							end
						Set @i = @i + 1
						-----Set @msg = Convert(VarChar(10), @i) + ' PERCENT COMPLETE'		
						RaisError(@msg, 1, 1) With NoWait
													
						INSERT INTO dbo.SampleAIM (SampleID, SampleResultID, AIMNutrientID, AIMUsedValue, UpdateStamp, CFCUserID)		
						VALUES (@HoldSampleID, @HoldSampleResultID, @AimNutrientID, @HoldResultValue, @HoldUpdateStamp, @HoldCFCUserID)
						SET @RecordID = @HoldSampleID
					End	
					SET @HoldSampleID = @SampleID	
					SET @HoldSampleResultID = @SampleResultID
					SET @HoldResultValue = @ResultValue
					SET @HoldUpdateStamp = @UpdateStamp	
					SET @HoldCFCUserID = @CFCUserID						
																						
					FETCH NEXT FROM TEMPCUR INTO @SampleID, @SampleResultID, @ResultValue, @UpdateStamp, @CFCUserID	
				End
							
			CLOSE TEMPCUR				
			DEALLOCATE TempCur	
			SET @i = @count
		END		
    END

	EXEC dbo.usp_ProgressBar_SAV @ProgressBarID, 'Finished', @ProgressBarCFCUserID
	
	SELECT 
			  @@IDENTITY as RecordID
			, @ErrorNumber AS ErrorNumber
			, @ErrorMessage AS ErrorMessage	
END