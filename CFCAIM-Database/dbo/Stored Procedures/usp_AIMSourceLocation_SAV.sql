CREATE PROCEDURE [dbo].[usp_AIMSourceLocation_SAV]
			  @AIMSourceID								INT
			, @AIMSourceLocationID						INT
			, @AIMSupplierLocationCode				NVARCHAR(20)
			, @SourceLocationName					VARCHAR(50)
			, @IsActive								BIT
			, @CFCUserID							INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

DECLARE @Status BIT = 0;
DECLARE @StatusText VARCHAR(10) = ''

IF @AIMSupplierLocationCode IS NULL and @SourceLocationName IS NOT NULL
	SET @AIMSupplierLocationCode = @SourceLocationName

IF @AIMSourceLocationID = 0
	BEGIN

		IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM AIMSourceLocation 
						WHERE	AIMSourceID = @AIMSourceID 
								and SourceLocationName = @SourceLocationName)
			BEGIN
				SET @ErrorNumber = 1

				SET @Status = (SELECT TOP 1 IsActive From dbo.AIMSourceLocation 
									WHERE	AIMSourceID = @AIMSourceID 
											and SourceLocationName = @SourceLocationName)
				IF @Status = 0
					SET @ErrorMessage = 'Supplier Location name already exists. (Supplier Location is inactive.) Please enter a different name. Information is not saved!'
				ELSE
					SET @ErrorMessage = 'Supplier Location name already exists for this supplier. Please select a different name. Information is not saved!'																	
			END

		ELSE IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM AIMSourceLocation 
						WHERE	AIMSourceID = @AIMSourceID 
								and AIMSupplierLocationCode = @AIMSupplierLocationCode)
			BEGIN
				SET @ErrorNumber = 1

				SET @Status = (SELECT TOP 1 IsActive From dbo.AIMSourceLocation 
									WHERE	AIMSourceID = @AIMSourceID 
											and AIMSupplierLocationCode = @AIMSupplierLocationCode)
				IF @Status = 0
					SET @ErrorMessage = 'Supplier Location code already exists. (Supplier Location is inactive.) Please enter a different code. Information is not saved!'
				ELSE
					SET @ErrorMessage = 'Supplier Location code already exists for this supplier. Please select a different code. Information is not saved!'																	
			END
		ELSE
			BEGIN
			
				INSERT
					dbo.AIMSourceLocation 
						(
						  AIMSourceID
						, AIMSupplierLocationCode
						, SourceLocationName
						, CFCUserID
						)
				VALUES
						(
						  @AIMSourceID
						, @AIMSupplierLocationCode
						, @SourceLocationName
						, @CFCUserID
						)
			END
	END
ELSE
	BEGIN
		IF @IsActive = 1
			BEGIN

				IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM AIMSourceLocation 
						WHERE	AIMSourceID = @AIMSourceID 
								and SourceLocationName = @SourceLocationName
								and AIMSourceLocationID <> @AIMSourceLocationID)
					BEGIN
						SET @ErrorNumber = 1

						SET @Status = (SELECT TOP 1 IsActive From dbo.AIMSourceLocation 
											WHERE	AIMSourceID = @AIMSourceID 
													and SourceLocationName = @SourceLocationName
													and AIMSourceLocationID <> @AIMSourceLocationID)
						IF @Status = 0
							SET @ErrorMessage = 'Supplier Location name already exists. (Supplier Location is inactive.) Please enter a different name. Information is not saved!'
						ELSE
							SET @ErrorMessage = 'Supplier Location name already exists for this supplier. Please enter a different name. Information is not saved!'																	
					END

				ELSE IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM AIMSourceLocation 
								WHERE	AIMSourceID = @AIMSourceID 
										and AIMSupplierLocationCode = @AIMSupplierLocationCode
										and AIMSourceLocationID <> @AIMSourceLocationID)
					BEGIN
						SET @ErrorNumber = 1

						SET @Status = (SELECT TOP 1 IsActive From dbo.AIMSourceLocation 
											WHERE	AIMSourceID = @AIMSourceID 
													and AIMSupplierLocationCode = @AIMSupplierLocationCode
													and AIMSourceLocationID <> @AIMSourceLocationID)
						IF @Status = 0
							SET @ErrorMessage = 'Supplier Location code already exists. (Supplier Location is inactive.) Please enter a different code. Information is not saved!'
						ELSE
							SET @ErrorMessage = 'Supplier Location code already exists for this supplier. Please enter a different code. Information is not saved!'																	
					END
				ELSE

					IF EXISTS(SELECT TOP 1
							S.AIMSourceID
						FROM
							dbo.AIMSource S WITH(NOLOCK)
						WHERE
								S.AIMSourceID = @AIMSourceID
							AND S.IsActive = 0)
						BEGIN
							SET @ErrorNumber = 1
							SET @ErrorMessage = 'Unable to reactivate Source Location because the Source is not active.'
						END
											
			END
		
		--12/16/20 ---------------------------------------
		ELSE
			BEGIN
				IF EXISTS(SELECT TOP 1 AIMSourceLocationID FROM dbo.UserDefinedDataSourceDefinition DEF WITH(NOLOCK) 
							WHERE DEF.AIMSourceLocationID = @AIMSourceLocationID )
					BEGIN
						SET @ErrorNumber = 3
						SET @ErrorMessage = 'Unable to inactivate Supplier Location because it is being used in user-defined import(s).'									
					END

			END		
		------------------------------------------------- 

		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.AIMSourceLocation WITH(ROWLOCK)
						SET 
							  AIMSupplierLocationCode = ISNULL(@AIMSupplierLocationCode, AIMSupplierLocationCode)
							, SourceLocationName = ISNULL(@SourceLocationName, SourceLocationName)
							, AIMSourceID = ISNULL(@AIMSourceID, AIMSourceID)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
						WHERE 
							AIMSourceLocationID = @AIMSourceLocationID
			END
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END