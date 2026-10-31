CREATE PROCEDURE [dbo].[usp_AIMSupplier_SAV]
			  @AIMSupplierID					INT			
			, @SupplierCode						NVARCHAR(20)
			, @SupplierName						VARCHAR(50)
			, @IsActive							BIT
			, @CFCUserID						INT
			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @ReturnIntegerValue INT = 0

DECLARE @SampleCodeList VARCHAR(1000) = ''
DECLARE @SampleCode VARCHAR (20) = ''
DECLARE @ID INT

DECLARE @Status BIT = 0;
DECLARE @StatusText VARCHAR(10) = ''


IF @SupplierCode IS NULL and @SupplierName IS NOT NULL
	SET @SupplierCode = @SupplierName


IF @AIMSupplierID = 0
	BEGIN
		IF EXISTS(SELECT TOP 1 AIMSourceID FROM AIMSource WHERE SourceName = @SupplierName)
			BEGIN
				SET @ErrorNumber = 1

				SET @Status = (SELECT TOP 1 IsActive From dbo.AIMSource 
									WHERE	SourceName = @SupplierName)
				
				IF @Status = 0
					SET @ErrorMessage = 'This Supplier Name already exists. (Supplier is inactive.) Please enter a different code. Information is not saved!'
				ELSE
					SET @ErrorMessage = 'This Supplier Name already exists. Please select a different name. Information is not saved!'

				SET @ReturnIntegerValue = (SELECT top 1 AIMSourceID FROM dbo.AIMSource WHERE SourceName = @SupplierName)
			END

		ELSE IF EXISTS(SELECT TOP 1 AIMSourceID FROM AIMSource WHERE AIMSupplierCode = @SupplierCode)
			BEGIN
				SET @ErrorNumber = 1

				SET @Status = (SELECT TOP 1 IsActive From dbo.AIMSource 
									WHERE	AIMSupplierCode = @SupplierCode)
				
				IF @Status = 0
					SET @ErrorMessage = 'This Supplier Code already exists. (Supplier is inactive.) Please enter a different code. Information is not saved!'
				ELSE
					SET @ErrorMessage = 'This Supplier Code already exists. Please select a different code. Information is not saved!'

				SET @ReturnIntegerValue = (SELECT top 1 AIMSourceID FROM dbo.AIMSource WHERE AIMSupplierCode = @SupplierCode)
			END

		ELSE
			BEGIN
				INSERT
					dbo.AIMSource 
						(							
						    SourceName
						  ,	AIMSupplierCode
						  , CFCUserID
						)
				VALUES
						(
							@SupplierName
						  , @SupplierCode
						  , @CFCUserID
						)
			END
	END
ELSE
	BEGIN

		IF @IsActive = 1 
			BEGIN
				IF EXISTS(SELECT TOP 1 AIMSourceID FROM dbo.AIMSource 
						WHERE   SourceName = @SupplierName
								and AIMSourceID <> @AIMSupplierID)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'This Supplier Name already exists. Please select a different name. Information is not saved!'						
					END

				ELSE IF EXISTS(SELECT TOP 1 AIMSourceID FROM dbo.AIMSource 
						WHERE   AIMSupplierCode = @SupplierCode
								and AIMSourceID <> @AIMSupplierID)
					BEGIN
						SET @ErrorNumber = 1
						SET @ErrorMessage = 'This Supplier Code already exists. Please select a different code. Information is not saved!'						
					END
			END						

		IF @IsActive = 0
			BEGIN
				
				-- 10/13/17
				--IF EXISTS(SELECT TOP 1 AIMSourceID FROM dbo.AIMSourceLocation ASL WITH(NOLOCK) 
				--			WHERE ASL.AIMSourceID = @AIMSupplierID and
				--				  ASL.IsActive = 1)
				--	BEGIN
				--		SET @ErrorNumber = 1
				--		SET @ErrorMessage = 'Unable to inactivate supplier because it is used in the Supplier Location table.'
				--	END	
				
				--12/16/20
				IF EXISTS(SELECT TOP 1 AIMSourceID FROM dbo.UserDefinedDataSourceDefinition DEF WITH(NOLOCK) 
							WHERE DEF.AIMSourceID = @AIMSupplierID )
					BEGIN
						SET @ErrorNumber = 3
						SET @ErrorMessage = 'Unable to inactivate Supplier because it is being used in user-defined import(s).'									
					END

				IF @ErrorNumber = 0
					Begin		
						UPDATE
							dbo.AIMSourceLocation with (rowlock)
								SET 
									IsActive = 0
									, UpdateStamp = GETDATE()
									, CFCUserID = ISNULL(@CFCUserID, CFCUserID) 
								WHERE
									AIMSourceID = @AIMSupplierID
					End
			END
	END
IF @ErrorNumber = 0
	BEGIN
		UPDATE
			dbo.AIMSource WITH(ROWLOCK)
				SET 
					
					  AIMSupplierCode = ISNULL(@SupplierCode, AIMSupplierCode)  
					, SourceName = ISNULL(@SupplierName, SourceName)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					AIMSourceID = @AIMSupplierID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @ReturnIntegerValue as ReturnIntegerValue

END