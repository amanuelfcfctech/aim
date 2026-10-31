CREATE PROCEDURE [dbo].[usp_AIMOwnerRefAnalyzerOwner_SAV]
			  @AIMOwnerRefAnalyzerOwnerID			INT
			, @AIMOwnerID							INT
			, @AnalyzerOwnerID						INT
			, @CFCUserID							INT
			, @DataSourceTypeID						INT 
			


AS
BEGIN
SET NOCOUNT ON;

DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''


--4/17/18
DECLARE @LogStatus		CHAR = ''

BEGIN
IF @AIMOwnerRefAnalyzerOwnerID = 0	
	BEGIN
		-- 4/17/18 - allow user to assign more the same ANALYZER owner/datasource to different AIM owners (so, multiple cross-refs for an analyzer owner/datasource). 		
		IF EXISTS (SELECT 
						XREF.AnalyzerOwnerID 
					FROM 
						dbo.AIMOwnerRefAnalyzerOwner XREF 
					LEFT OUTER JOIN dbo.AnalyzerOwner AM 
						ON XRef.AnalyzerOwnerID = AM.AnalyzerOwnerID
					WHERE AM.DataSourceTypeID = @DataSourceTypeID 
							and XRef.AIMOwnerID = @AIMOwnerID 
							and XRef.AnalyzerOwnerID = @AnalyzerOwnerID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'More than one cross-reference for this AIM Owner and Analyzer Owner/DataSource is not allowed. Information is not saved!'
			END
		ELSE		
			BEGIN
				SET @LogStatus = 'A'
				INSERT
					dbo.AIMOwnerRefAnalyzerOwner 
					(
					  AIMOwnerID
					, AnalyzerOwnerID
					, UpdateStamp
					, CFCUserID
					)
			VALUES
					(
					  @AIMOwnerID
					, @AnalyzerOwnerID
					, GETDATE()
					, @CFCUserID
					)
				SET @RecordID = @@IDENTITY		
			END
	END
ELSE	
	BEGIN

		SET @LogStatus = 'C'
		UPDATE
			dbo.AIMOwnerRefAnalyzerOwner WITH(ROWLOCK)
				SET 
					AIMOwnerID = ISNULL(@AIMOwnerID, AIMOwnerID), 
					AnalyzerOwnerID = ISNULL(@AnalyzerOwnerID, AnalyzerOwnerID)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
				WHERE 
					AIMOwnerRefAnalyzerOwnerID = @AIMOwnerRefAnalyzerOwnerID
		SET @RecordID = @AIMOwnerRefAnalyzerOwnerID
	END
END



--4/17/18 ----------------------------------------
IF  @ErrorNumber = 0
	Begin
		INSERT INTO AIMOwnerRefAnalyzerOwnerLog (AIMOwnerID, AnalyzerOwnerID, LogStatus, UpdateStamp, CFCUserID)
		SELECT	    @AIMOwnerID, @AnalyzerOwnerID, @LogStatus, Getdate(), @CFCUserID 
	End
--------------------------------------------------
		

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END