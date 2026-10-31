CREATE PROCEDURE [dbo].[usp_ImportSQLSampleMissing_SAV]
			  @ImportSQLSampleLogID			INT			
			, @MissingSampleCode			NVARCHAR(20)		
			, @CFCUserID					INT 

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''


BEGIN								
	INSERT
		dbo.ImportSQLSampleMissing
			(
			  ImportSQLSampleLogID
			, MissingSampleCode							
			, CreatedStamp
			, CFCUserID
			)
	VALUES
			(
			  @ImportSQLSampleLogID
			, @MissingSampleCode			
			, GETDATE()
			, @CFCUserID
			)
	SET @RecordID = @@IDENTITY
			
END


SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END