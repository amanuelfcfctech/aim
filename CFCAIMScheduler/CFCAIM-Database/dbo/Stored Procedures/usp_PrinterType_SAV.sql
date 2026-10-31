CREATE PROCEDURE [dbo].[usp_PrinterType_SAV]
				@PrinterTypeID			INT
			  , @PrinterTypeName		nVarChar(50) = NULL
			  --, @PrinterName			nVarChar(50) = NULL
			  , @CFCUserID				INT			 = NULL

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @PrinterTypeID = 0
	BEGIN
		INSERT
			dbo.PrinterType 
				(
				  PrinterTypeName
				--, PrinterName
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @PrinterTypeName
				--, @PrinterName									
				, @CFCUserID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.PrinterType WITH(ROWLOCK)
				SET 
					PrinterTypeName = ISNULL(@PrinterTypeName, PrinterTypeName)
					--, PrinterName = ISNULL(@PrinterName,PrinterName)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					PrinterTypeID = @PrinterTypeID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
