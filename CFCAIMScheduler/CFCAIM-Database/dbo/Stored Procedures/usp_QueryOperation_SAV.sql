create PROCEDURE [dbo].[usp_QueryOperation_SAV]
			  @QueryOperationID					INT
			, @Operation						NVARCHAR(20)	= NULL
			, @IsActive							BIT				= NULL
			, @CFCUserID						INT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @QueryOperationID = 0
	BEGIN
		INSERT
			dbo.QueryOperation 
				(
				  Operation
				, IsActive
				, UpdateStamp
				, CFCUserID
				)
		VALUES
				(
				  @Operation
				, @IsActive
				, GETDATE()
				, @CFCUserID
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.QueryOperation WITH(ROWLOCK)
				SET 
					  Operation = ISNULL(@Operation, Operation)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					QueryOperationID = @QueryOperationID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
