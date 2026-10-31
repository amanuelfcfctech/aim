create PROCEDURE [dbo].[Xusp_AIMMaterial_SAV_old]
			  @AIMMaterialID					INT
			, @MaterialTypeID					INT
			, @AIMMaterialCode					NVARCHAR(20)  = NULL
			, @AIMMaterialName					NVARCHAR(30)  = NULL
			, @IsActive							BIT
			, @CFCUserID						INT			
	
	
-- EXEC usp_AIMMaterial_SAV 0, 1, 'POULTRY MEAL 2/3/16','POULTRY MEAL 2/3/16', 0, 2 		
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @ReturnIntegerValue INT = 0

IF @AIMMaterialID = 0

	BEGIN
			IF EXISTS(SELECT 
						AIMMaterialID 
					FROM 
						dbo.AIMMaterial AM
					WHERE 
							AM.AIMMaterialCode = @AIMMaterialCode and 
							AM.MaterialTypeID = @MaterialTypeID)
				BEGIN

				
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'This Material code already exists. Please select a different code. Information is not saved!'

					SET @ReturnIntegerValue = (SELECT top 1
						AIMMaterialID 
					FROM 
						dbo.AIMMaterial AM
					WHERE 
						AM.AIMMaterialCode = @AIMMaterialCode and 
						AM.MaterialTypeID = @MaterialTypeID)
				
				END

			ELSE
				BEGIN
					INSERT
						dbo.AIMMaterial 
							(
								MaterialTypeID
							, AIMMaterialCode
							, AIMMaterialName							
							, UpdateStamp
							, CFCUserID
							)
					VALUES
							(
								@MaterialTypeID
							, @AIMMaterialCode
							, @AIMMaterialName							 
							, GETDATE()
							, @CFCUserID
							)
					SET @RecordID = @@IDENTITY
				END
	END
ELSE
	BEGIN
		IF @ErrorNumber = 0
			BEGIN

				UPDATE
					dbo.AIMMaterial WITH(ROWLOCK)
						SET 
							  MaterialTypeID = ISNULL(@MaterialTypeID, MaterialTypeID)
							, AIMMaterialCode = ISNULL(@AIMMaterialCode, AIMMaterialCode)
							, AIMMaterialName = ISNULL(@AIMMaterialName, AIMMaterialName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
						WHERE 
							AIMMaterialID = @AIMMaterialID
				SET @RecordID = @AIMMaterialID 
			END
	END

SELECT 
	  @RecordID as RecordID
	, @ReturnIntegerValue as ReturnIntegerValue
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END