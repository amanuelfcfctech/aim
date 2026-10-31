CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SAV]
			  @AIMOwnerLocationID					INT
			, @AIMOwnerID							INT
			, @LocationCode							VARCHAR(20)
			, @LocationName							VARCHAR(50)			
			, @IsActive								BIT
			, @CFCUserID							INT
			, @IsFormulationPlant					BIT
AS


BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @Status BIT = 0;
DECLARE @StatusText VARCHAR(10) = ''

IF @AIMOwnerLocationID = 0
	BEGIN
		IF EXISTS(SELECT TOP 1 IsActive FROM dbo.AIMOwnerLocation 
					WHERE	AIMOwnerID = @AIMOwnerID 
							and LocationCode = @LocationCode)
				BEGIN
					SET @ErrorNumber = 1

					SET @Status = (SELECT TOP 1 IsActive From dbo.AIMOwnerLocation 
									WHERE	AIMOwnerID = @AIMOwnerID 
											and LocationCode = @LocationCode)
					IF @Status = 0
						SET @ErrorMessage = 'Owner Location code already exists for this owner. (Owner Location is inactive.) Please enter a different code. Information is not saved!'
					ELSE
						SET @ErrorMessage = 'Owner Location code already exists for this owner. Please enter a different code. Information is not saved!'

				END
			ELSE IF EXISTS(SELECT TOP 1 IsActive FROM dbo.AIMOwnerLocation 
					WHERE	AIMOwnerID = @AIMOwnerID 
							and LocationName = @LocationName)
				BEGIN
					SET @ErrorNumber = 1

					SET @Status = (SELECT TOP 1 IsActive From dbo.AIMOwnerLocation 
									WHERE	AIMOwnerID = @AIMOwnerID 
											and LocationName = @LocationName)
					IF @Status = 0
						SET @ErrorMessage = 'Owner Location name already exists for this owner. (Owner Location is inactive.) Please enter a different name. Information is not saved!'
					ELSE
						SET @ErrorMessage = 'Owner Location name already exists for this owner. Please enter a different name. Information is not saved!'
				END

			ELSE
				BEGIN
					INSERT
						dbo.AIMOwnerLocation
							(
								AIMOwnerID
							  , LocationCode
							  , LocationName
							  , CFCUserID
							  , IsFormulationPlant
							)
					VALUES
							(
								@AIMOwnerID
							  , @LocationCode
							  , @LocationName
							  , @CFCUserID
							  , @IsFormulationPlant
							)
				END
	END
ELSE
	BEGIN
		IF @IsActive = 1
			BEGIN
				IF EXISTS(SELECT TOP 1 AIMOwnerLocationID FROM AIMOwnerLocation 
						WHERE	AIMOwnerID = @AIMOwnerID 
								and LocationName = @LocationName
								and AIMOwnerLocationID <> @AIMOwnerLocationID)
					BEGIN
						SET @ErrorNumber = 1

						SET @Status = (SELECT TOP 1 IsActive From dbo.AIMOwnerLocation 
											WHERE	AIMOwnerID = @AIMOwnerID 
													and LocationName = @LocationName
													and AIMOwnerLocationID <> @AIMOwnerLocationID)
						IF @Status = 0
							SET @ErrorMessage = 'Owner Location name already exists. (Owner Location is inactive.) Please enter a different name. Information is not saved!'
						ELSE
							SET @ErrorMessage = 'Owner Location name already exists for this owner. Please enter a different name. Information is not saved!'																	
					END

				ELSE IF EXISTS(SELECT TOP 1 AIMOwnerLocationID FROM AIMOwnerLocation 
								WHERE	AIMOwnerID = @AIMOwnerID 
										and LocationCode = @LocationCode
										and AIMOwnerLocationID <> @AIMOwnerLocationID)
					BEGIN
						SET @ErrorNumber = 1

						SET @Status = (SELECT TOP 1 IsActive From dbo.AIMOwnerLocation 
											WHERE	AIMOwnerID = @AIMOwnerID 
													and LocationCode = @LocationCode
													and AIMOwnerLocationID <> @AIMOwnerLocationID)
						IF @Status = 0
							SET @ErrorMessage = 'Owner Location code already exists. (Owner Location is inactive.) Please enter a different code. Information is not saved!'
						ELSE
							SET @ErrorMessage = 'Owner Location code already exists for this owner. Please enter a different code. Information is not saved!'																	
					END
				ELSE

					IF EXISTS(SELECT TOP 1
							S.AIMOwnerID
						FROM
							dbo.AIMOwner S WITH(NOLOCK)
						WHERE
								S.AIMOwnerID = @AIMOwnerID
							AND S.IsActive = 0)
						BEGIN
							SET @ErrorNumber = 1
							SET @ErrorMessage = 'Unable to reactivate Owner Location because the Owner is not active.'
						END
			END	
		--12/16/20 -----------------------------------
		ELSE								
			BEGIN
				IF EXISTS(SELECT TOP 1 AIMOwnerLocationID FROM dbo.UserDefinedDataSourceDefinition DEF WITH(NOLOCK) 
							WHERE DEF.AIMOwnerLocationID = @AIMOwnerLocationID )
					BEGIN
						SET @ErrorNumber = 3
						SET @ErrorMessage = 'Unable to inactivate OwnerLocation because it is being used in user-defined import(s).'									
					END
			END


		IF @ErrorNumber = 0
			BEGIN
				UPDATE
					dbo.AIMOwnerLocation WITH(ROWLOCK)
						SET 
							  AIMOwnerID = ISNULL(@AIMOwnerID, AIMOwnerID)
							, LocationCode = ISNULL(@LocationCode, LocationCode)
							, LocationName = ISNULL(@LocationName, LocationName)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
							, IsFormulationPlant = ISNULL(@IsFormulationPlant, IsFormulationPlant)
						WHERE 
							AIMOwnerLocationID = @AIMOwnerLocationID
			END
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END