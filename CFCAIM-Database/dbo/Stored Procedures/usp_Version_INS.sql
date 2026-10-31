CREATE PROCEDURE [dbo].[usp_Version_INS]
			 @Version				NVARCHAR(15)
		   , @CreatedDate			DATETIME
					
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @AIMOwnerID		INT	

INSERT INTO	
	dbo.[Version] 
		(
		  [Version]
		, CreatedDate
		)
VALUES
	(
		  @Version
		, @CreatedDate		
	)

SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber  AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END