CREATE PROCEDURE [dbo].[usp_Sample_SEL_TOP1_SampleDate]
	
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

SET @ErrorMessage = (

SELECT	TOP 1
		S.SampleDate
FROM 
	dbo.Sample S WITH(NOLOCK)

WHERE
	IsDate(Convert(Varchar,S.SampleDate)) = 1			-- 12/28/21 make sure sampledate is a valid date

ORDER BY 
	S.SampleDate )


SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage


END