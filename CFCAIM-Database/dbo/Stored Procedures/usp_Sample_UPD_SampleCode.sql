create PROCEDURE [dbo].[usp_Sample_UPD_SampleCode]
			  @SampleID						INT
			, @SampleCode					VARCHAR(30)
			
AS

BEGIN

SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

UPDATE
	dbo.Sample WITH(ROWLOCK)
		SET 
			SampleCode = @SampleCode
			
		WHERE 
			SampleID = @SampleID

SELECT 
	  @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END