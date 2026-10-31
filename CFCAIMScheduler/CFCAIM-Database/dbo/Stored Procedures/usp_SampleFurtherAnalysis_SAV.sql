CREATE PROCEDURE [dbo].[usp_SampleFurtherAnalysis_SAV]
			@SampleFurtherAnalysisID	INT
			, @SampleID					INT		 = NULL			
			, @DataSourceTypeID			INT		 = NULL		
			, @CFCUserID				INT		 = NULL			
			, @AIMMessageID				INT		 = NULL			
			, @ProcessDate				DATETIME = NULL

--exec usp_SampleFurtherAnalysis_SAV 0, 1, 1, '2022-06-22', 1, '2022-06-01'
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleFurtherAnalysisID = 0
	BEGIN
		INSERT
			dbo.SampleFurtherAnalysis
				(
				  SampleID	
				, DataSourceTypeID
				, AIMMessageID
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @SampleID	
				, @DataSourceTypeID
				, @AIMMessageID
				, @CFCUserID														
				, GetDate()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleFurtherAnalysis WITH(ROWLOCK)
				SET 
					  SampleID = ISNULL(@SampleID, SampleID)
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
					, AIMMessageID = ISNULL(@AIMMessageID, AIMMessageID)
					, ProcessDate = ISNULL(@ProcessDate, ProcessDate)							
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
				WHERE 
					SampleFurtherAnalysisID = @SampleFurtherAnalysisID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END