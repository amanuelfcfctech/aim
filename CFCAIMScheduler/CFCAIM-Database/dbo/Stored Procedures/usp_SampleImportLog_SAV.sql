CREATE PROCEDURE [dbo].[usp_SampleImportLog_SAV]
			  @SampleImportLogID					INT
			, @SampleID								INT = NULL			
			, @Message								NVARCHAR(100) = NULL
			, @Status								INT = NULL			
			, @CFCUserID							INT = NULL
			, @UpdateStamp							DATETIME = NULL
			, @UserDefinedDataSourceDefinitionID	INT = NULL
			, @BatchID								INT = NULL
			, @SampleCode							NVARCHAR(20) = NULL
			
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleImportLogID = 0
	BEGIN
		INSERT
			dbo.SampleImportLog 
				(				
				 SampleID				
				, [Status]
				, [Message]
				, CFCUserID
				, UpdateStamp			
				, UserDefinedDataSourceDefinitionID
				, BatchID
				, SampleCode							
							
				)
		VALUES
				(
				  @SampleID				
				, @Status
				, @Message
				, @CFCUserID													
				, GETDATE()
				, @UserDefinedDataSourceDefinitionID
				, @BatchID
				, @SampleCode
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleImportLog WITH(ROWLOCK)
				SET 
					  SampleID = ISNULL(@SampleID, SampleID)					
					, [Message] = ISNULL(@Message, [Message])
					, [Status] = ISNULL(@Status, [Status])
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, UpdateStamp = GETDATE()
					, UserDefinedDataSourceDefinitionID = ISNULL(@UserDefinedDataSourceDefinitionID, UserDefinedDataSourceDefinitionID)
					, BatchID = ISNULL(@BatchID, BatchID)
					, SampleCode = ISNULL(@SampleCode, SampleCode)
					
				WHERE 
					@SampleImportLogID = @SampleImportLogID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END