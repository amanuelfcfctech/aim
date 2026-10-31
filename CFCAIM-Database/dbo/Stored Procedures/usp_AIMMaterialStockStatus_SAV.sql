CREATE PROCEDURE  [dbo].[usp_AIMMaterialStockStatus_SAV]
			  @AIMMaterialStockStatusID			INT
			, @AIMMaterialID					INT
			, @AIMOwnerLocationID				INT
			, @CFCUserID						INT
			
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @AIMMaterialStockStatusID = 0 
	BEGIN
		IF EXISTS(SELECT 
					AIMMaterialStockStatusID 
				FROM 
					dbo.AIMMaterialStockStatus AML
				WHERE 
						AML.AIMMaterialID = @AIMMaterialID
					AND AML.AIMOwnerLocationID = @AIMOwnerLocationID	)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Material-Location combination already exists. Information is not saved!'
				SET @RecordID = (SELECT 
									AIMMaterialStockStatusID 
								FROM 
									dbo.AIMMaterialStockStatus AML
								WHERE 
									AML.AIMMaterialID = @AIMMaterialID
									AND AML.AIMOwnerLocationID = @AIMOwnerLocationID	)
			END
		ELSE
			BEGIN
				INSERT
					dbo.AIMMaterialStockStatus
						(
						  AIMMaterialID
						, AIMOwnerLocationID						
						, UpdateStamp
						, CFCUserID						
						)
				VALUES
						(
						  @AIMMaterialID
						, @AIMOwnerLocationID						
						, GETDATE()
						, @CFCUserID						
						)
				SET @RecordID = @@IDENTITY
			END
		END
ELSE
		BEGIN
			UPDATE
				dbo.AIMMaterialStockStatus WITH(ROWLOCK)
					SET 
						  AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
						, AIMOwnerLocationID = ISNULL(@AIMOwnerLocationID, AIMOwnerLocationID)
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID							
					WHERE 
						AIMMaterialID = @AIMMaterialID and
						AIMOwnerLocationID = @AIMOwnerLocationID

			SET @RecordID = @AIMMaterialStockStatusID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END