CREATE PROCEDURE [dbo].[usp_SampleTestTypeResult_SAV_IsIncluded]
			  @SampleTestTypeResultID	INT			
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
		dbo.SampleTestTypeResult
			SET				
				 IsIncluded = ISNULL(@IsIncluded, IsIncluded)				
				, CFCUserID = @CFCUserID

	WHERE 
		SampleTestTypeResultID = @SampleTestTypeResultID
	SET @RecordID = @@IDENTITY
END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END