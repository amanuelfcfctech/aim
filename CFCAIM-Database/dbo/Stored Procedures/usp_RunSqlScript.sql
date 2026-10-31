CREATE PROCEDURE [dbo].[usp_RunSqlScript]
			  	@SqlString		NVARCHAR(4000)	= NULL
			  
AS
BEGIN

--exec usp_RunSqlScript 'Select * from Version'


SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


EXEC (@SqlString)
--print @SqlString


SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END