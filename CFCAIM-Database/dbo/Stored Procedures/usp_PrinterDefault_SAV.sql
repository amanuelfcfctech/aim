CREATE PROCEDURE [dbo].[usp_PrinterDefault_SAV]
				@PrinterDefaultID		INT
			  , @PrinterTypeID			INT			  
			  , @PrinterName			NVARCHAR(50) = NULL
			  , @CFCUserID				INT			 = NULL

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @PrinterDefaultID = 0
	BEGIN
		INSERT
			dbo.PrinterDefault
				(
				  PrinterTypeID
				, PrinterName
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @PrinterTypeID
				, @PrinterName									
				, @CFCUserID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.PrinterDefault WITH(ROWLOCK)
				SET 
					PrinterTypeID = ISNULL(@PrinterTypeID, PrinterTypeID)
					, PrinterName = ISNULL(@PrinterName,PrinterName)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					PrinterDefaultID = @PrinterDefaultID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END