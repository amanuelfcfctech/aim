CREATE PROCEDURE [dbo].[usp_SampleTestTypeResults_SAV_IsIncluded]
			  @SampleTestTypeResultsID	INT			
			, @IsIncluded				BIT = NULL
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
				 IsIncluded = ISNULL(@IsIncluded, IsIncluded)				
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