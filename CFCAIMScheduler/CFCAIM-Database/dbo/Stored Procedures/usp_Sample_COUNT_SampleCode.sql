
CREATE PROCEDURE [dbo].[usp_Sample_COUNT_SampleCode]
	  @SampleCode	NVARCHAR(50)

-- exec usp_Sample_COUNT_SampleCode 'RPMG BN 1-15 1/15/2016'
-- exec usp_Sample_COUNT_SampleCode '994'


AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
Declare @SampleID INT = 0


SET @SampleID =  
			(SELECT count(*) as cnt
			FROM
				dbo.Sample S with (nolock)
			WHERE
				S.SampleCode = Left(@SampleCode,20) )

IF @SampleID > 0	
	
	Begin
	SET @SampleID = (Select TOP 1 SampleID
					FROM 
						dbo.Sample S with (nolock)
					WHERE 
						S.SampleCode = Left(@SampleCode, 20) 
					ORDER BY 
						CreatedStamp desc)
	End

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @SampleID AS ReturnIntegerValue

END