CREATE PROCEDURE [dbo].[usp_LabelFormat_DEL]
			@LabelFormatID INT
AS
BEGIN


SET NOCOUNT ON;

DELETE
FROM 
	dbo.LabelFormat 	
WHERE
	LabelFormatID = @LabelFormatID


END