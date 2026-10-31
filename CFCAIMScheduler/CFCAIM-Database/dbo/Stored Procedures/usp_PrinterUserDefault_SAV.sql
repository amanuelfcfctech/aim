CREATE PROCEDURE [dbo].[usp_PrinterUserDefault_SAV]
				@PrinterUserDefaultID		INT
			  , @CFCUserID					INT
			  , @PrinterTypeID				INT			  
			  , @PrinterName				NVARCHAR(50) = NULL
			  , @UpdateCFCUserID			INT			 = NULL

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @PrinterUserDefaultID = 0
	BEGIN
		INSERT
			dbo.PrinterUserDefault
				(
				  CFCUserID
				, PrinterTypeID
				, PrinterName				
				, UpdateStamp
				, UpdateCFCUserID				
				)
		VALUES
				(
				  @CFCUserID
				, @PrinterTypeID
				, @PrinterName									
				, GETDATE()
				, @UpdateCFCUserID
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.PrinterUserDefault WITH(ROWLOCK)
				SET 
					  CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, PrinterTypeID = ISNULL(@PrinterTypeID, PrinterTypeID)
					, PrinterName = ISNULL(@PrinterName,PrinterName)
					, UpdateStamp = GETDATE()
					, UpdateCFCUserID = ISNULL(@UpdateCFCUserID, UpdateCFCUserID)
				WHERE 
					PrinterUserDefaultID = @PrinterUserDefaultID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END