CREATE PROCEDURE [dbo].[usp_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_SAV]
			  @AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID			INT
			, @AIMSupplierID															INT
			, @AIMSupplierLocationID													INT			
			, @AnalyzerSupplierLocationCombinedID										INT
			, @CFCUserID																INT
			, @DataSourceTypeID															INT 


AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @Count			INT = 0

DECLARE @LogStatus		CHAR = ''

DECLARE @ProcName nvarchar(128);
SET @ProcName = OBJECT_NAME(@@PROCID);  

BEGIN			
IF @AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID = 0	
	BEGIN		
		IF EXISTS (SELECT 
						XREF.AnalyzerSupplierLocationCombinedID 
					FROM 
						dbo.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined XREF 
					LEFT OUTER JOIN dbo.AnalyzerSupplierLocationCombined ASOL
						ON XRef.AnalyzerSupplierLocationCombinedID = ASOL.AnalyzerSupplierLocationCombinedID
					WHERE ASOL.DataSourceTypeID = @DataSourceTypeID 
							and XRef.AIMSupplierID = @AIMSupplierID 
							and XRef.AIMSupplierLocationID = @AIMSupplierLocationID							
							and XRef.AnalyzerSupplierLocationCombinedID = @AnalyzerSupplierLocationCombinedID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'More than one cross-reference for this AIM Supplier Location and Analyzer Supplier Location/DataSource is not allowed. Information is not saved!'
			END
		ELSE		
			Begin
				SET @LogStatus = 'A'
				INSERT
					dbo.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined 
					(
					  AIMSupplierID
					, AIMSupplierLocationID					
					, AnalyzerSupplierLocationCombinedID
					, UpdateStamp
					, CFCUserID					
					)
				VALUES
					(
					  @AIMSupplierID
					, @AIMSupplierLocationID					
					, @AnalyzerSupplierLocationCombinedID
					, GETDATE()
					, @CFCUserID					
					)
				SET @RecordID = @@IDENTITY		
			End
		END
	ELSE	
		BEGIN
			SET @LogStatus = 'C'
			UPDATE
				dbo.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined WITH(ROWLOCK)
					SET 
						AIMSupplierID = ISNULL(@AIMSupplierID, AIMSupplierID), 
						AIMSupplierLocationID = ISNULL(@AIMSupplierLocationID, AIMSupplierLocationID), 						
						AnalyzerSupplierLocationCombinedID = ISNULL(@AnalyzerSupplierLocationCombinedID, AnalyzerSupplierLocationCombinedID)
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID
					WHERE 
						@AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID = @AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID
			SET @RecordID = @AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID
		END
	END	

IF @ErrorNumber = 0
	Begin
		INSERT INTO AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedLog (AIMSupplierID, AIMSupplierLocationID, AnalyzerSupplierLocationCombinedID, LogStatus, UpdateStamp, CFCUserID, Comment)
		SELECT	    @AIMSupplierID, @AIMSupplierLocationID, @AnalyzerSupplierLocationCombinedID, @LogStatus, Getdate(), @CFCUserID, @ProcName
	End
--------------------------------------------------

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END