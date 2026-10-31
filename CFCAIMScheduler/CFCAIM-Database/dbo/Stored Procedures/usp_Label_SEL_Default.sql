CREATE PROCEDURE [dbo].[usp_Label_SEL_Default]	
		@ControlName	VARCHAR(50)			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @Val INT = 0

--exec usp_Label_SEL_Default 'lblid'

SET @Val = (SELECT TOP 1
	L.LabelsID
FROM
	 dbo.Labels L 
INNER JOIN 
	dbo.Control C
		ON L.ControlID = C.ControlID
WHERE
	C.ControlName = @ControlName)

SELECT 
	  @Val as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END