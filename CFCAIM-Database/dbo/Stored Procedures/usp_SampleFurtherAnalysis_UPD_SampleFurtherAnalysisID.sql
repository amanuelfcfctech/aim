CREATE PROCEDURE [dbo].[usp_SampleFurtherAnalysis_UPD_SampleFurtherAnalysisID]

			@SampleFurtherAnalysisID	INT
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

	BEGIN
		UPDATE
			dbo.SampleFurtherAnalysis WITH(ROWLOCK)
		SET 
			ProcessDate = GetDate()										
		WHERE 
			SampleFurtherAnalysisID = @SampleFurtherAnalysisID
	
		SET @RecordID = @SampleFurtherAnalysisID 
	END

	SELECT 
		  @RecordID AS RecordID
		, @ResultNumber AS ErrorNumber
		, @ResultMessage AS ErrorMessage

END