CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SAV]
			  @AIMOwnerLocationRefAnalyzerOwnerLocationID			INT
			, @AIMOwnerID											INT
			, @AIMOwnerLocationID									INT
			, @AnalyzerOwnerID										INT
			, @AnalyzerOwnerLocationID								INT
			, @CFCUserID												INT
			, @DataSourceTypeID											INT 

-- exec usp_AIMOwnerLocationRefAnalyzerOwnerLocation_SAV 12, 18, 4, 2, 9

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @Count			INT = 0

--4/17/18
DECLARE @LogStatus		CHAR = ''

BEGIN			
IF @AIMOwnerLocationRefAnalyzerOwnerLocationID = 0	
	BEGIN
		-- 4/17/18 - allow user to assign more the same ANALYZER Owner-loc/datasource to different AIM Owner locations (so, multiple cross-refs for an analyzer Owner-loc/datasource). 		
		IF EXISTS (SELECT 
						XREF.AnalyzerOwnerLocationID 
					FROM 
						dbo.AIMOwnerLocationRefAnalyzerOwnerLocation XREF 
					LEFT OUTER JOIN dbo.AnalyzerOwnerLocation ASOL
						ON XRef.AnalyzerOwnerLocationID = ASOL.AnalyzerOwnerLocationID
					WHERE ASOL.DataSourceTypeID = @DataSourceTypeID 
							and XRef.AIMOwnerID = @AIMOwnerID 
							and XRef.AIMOwnerLocationID = @AIMOwnerLocationID
							and XRef.AnalyzerOwnerID = @AnalyzerOwnerID
							and XREF.AnalyzerOwnerLocationID = @AnalyzerOwnerLocationID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'More than one cross-reference for this AIM Owner Location and Analyzer Owner Location/DataSource is not allowed. Information is not saved!'
			END
		ELSE	
	
			Begin
				SET @LogStatus = 'A'
				INSERT
					dbo.AIMOwnerLocationRefAnalyzerOwnerLocation 
					(
					  AIMOwnerID
					, AIMOwnerLocationID
					, AnalyzerOwnerID
					, AnalyzerOwnerLocationID
					, UpdateStamp
					, CFCUserID
					)
				VALUES
					(
					  @AIMOwnerID
					, @AIMOwnerLocationID
					, @AnalyzerOwnerID
					, @AnalyzerOwnerLocationID
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
				dbo.AIMOwnerLocationRefAnalyzerOwnerLocation WITH(ROWLOCK)
					SET 
						AIMOwnerID = ISNULL(@AIMOwnerID, AIMOwnerID), 
						AIMOwnerLocationID = ISNULL(@AIMOwnerLocationID, AIMOwnerLocationID), 
						AnalyzerOwnerID = ISNULL(@AnalyzerOwnerID, AnalyzerOwnerID),
						AnalyzerOwnerLocationID = ISNULL(@AnalyzerOwnerLocationID, AnalyzerOwnerLocationID)
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID
					WHERE 
						AIMOwnerLocationRefAnalyzerOwnerLocationID = @AIMOwnerLocationRefAnalyzerOwnerLocationID
			SET @RecordID = @AIMOwnerLocationRefAnalyzerOwnerLocationID
		END
	END
		
--4/17/18 ----------------------------------------
IF @ErrorNumber = 0
	Begin
		INSERT INTO AIMOwnerLocationRefAnalyzerOwnerLocationLog (AIMOwnerID, AIMOwnerLocationID, AnalyzerOwnerID, AnalyzerOwnerLocationID, LogStatus, UpdateStamp, CFCUserID)
		SELECT	    @AIMOwnerID, @AIMOwnerLocationID, @AnalyzerOwnerID, @AnalyzerOwnerLocationID, @LogStatus, Getdate(), @CFCUserID 
	End
--------------------------------------------------

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
END