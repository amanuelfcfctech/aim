CREATE PROCEDURE [dbo].[usp_LogTest_SAV]
			  @Text			Varchar(50)
			

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


	INSERT
		dbo.LogTest 
		(
			Field1
		, UpdateStamp		
		)
VALUES
		(
			@Text
		, GETDATE()		
		)

SELECT  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	
END