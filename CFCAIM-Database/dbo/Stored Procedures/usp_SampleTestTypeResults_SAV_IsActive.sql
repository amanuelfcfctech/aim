CREATE PROCEDURE [dbo].[usp_SampleTestTypeResults_SAV_IsActive]
			  @SampleTestTypeResultsID	INT			
			, @IsActive 				BIT = NULL
			, @CFCUserID				INT = Null
AS
BEGIN
SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


BEGIN		
	UPDATE
		dbo.SampleTestTypeResults
			SET				
				 IsActive = ISNULL(@IsActive, IsActive)				
				, CFCUserID = @CFCUserID

	WHERE 
		SampleTestTypeResultsID = @SampleTestTypeResultsID
	SET @RecordID = @@IDENTITY
END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END