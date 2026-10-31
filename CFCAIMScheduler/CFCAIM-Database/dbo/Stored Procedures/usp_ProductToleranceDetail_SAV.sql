CREATE PROCEDURE [dbo].[usp_ProductToleranceDetail_SAV]
			  @ProductToleranceDetailID		INT
			, @ProductToleranceHeaderID		INT				= NULL
			, @AIMMaterialID				INT				= NULL			
			, @UpdateStamp					DATETIME		= NULL
			, @CFCUserID					INT				= NULL
						
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @ProductToleranceDetailID = 0
	BEGIN

		IF EXISTS(SELECT 
						ProductToleranceDetailID 
				FROM 
						dbo.ProductToleranceDetail PTD
				WHERE 							
						PTD.ProductToleranceDetailID = @ProductToleranceDetailID and 
						PTD.AIMMaterialID = @AIMMaterialID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This material already exists in group.'
			END
		ELSE
			BEGIN			

				INSERT
					dbo.ProductToleranceDetail
						(
						ProductToleranceHeaderID
						, AIMMaterialID						
						, CFCUserID
						, UpdateStamp				
						)
				VALUES
						(
						@ProductToleranceHeaderID
						, @AIMMaterialID												
						, @CFCUserID
						, GetDate()
						)
				SET @RecordID = @@IDENTITY 
			END
	END
ELSE
	BEGIN
		IF @ErrorNumber = 0
			UPDATE
				dbo.ProductToleranceDetail 
					SET 
						  ProductToleranceHeaderID = ISNULL(@ProductToleranceHeaderID, ProductToleranceHeaderID)
						, AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)																							
						, UpdateStamp = GETDATE()							
						, CFCUserID = ISNULL(@CFCUserID, CFCUserID)											
					WHERE 
						ProductToleranceDetailID = @ProductToleranceDetailID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END