CREATE PROCEDURE [dbo].[usp_AIMSourceLocationGuarantee_SAV]
			 @AIMSourceLocationGuaranteeID		INT
			, @AIMSourceID						INT				= NULL
			, @AIMSourceLocationID				INT				= NULL
			, @AIMNutrientID					INT				= NULL	
			, @AIMMaterialID					INT				= NULL	
			, @Min								numeric(18,8)	= NULL
			, @Max								numeric(18,8)	= NULL	
			, @Target							numeric(18,8)	= NULL		
			, @Moisture							numeric(18,8)	= NULL		
			, @IsActive							BIT				= NULL
			, @CFCUserID						INT				= NULL

--exec usp_AIMSourceLocationGuarantee_SAV 10, 1, 1, 2, 3, 3.0, 4.0, true, 2

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0
DECLARE @Status BIT = 0;
DECLARE @StatusText VARCHAR(10) = ''
DECLARE @ReturnIntegerValue INT = 0


IF @AIMSourceLocationGuaranteeID = 0
	BEGIN

		IF EXISTS(SELECT TOP 1 AIMSourceLocationGuaranteeID FROM AIMSourceLocationGuarantee 
			WHERE AIMSourceID = @AIMSourceID
				and AIMSourceLocationID = @AIMSourceLocationID
				and AIMMaterialID = @AIMMaterialID
				and AIMNutrientID = @AIMNutrientID)
			BEGIN
				
				SET @Status = (SELECT TOP 1 IsActive From dbo.AIMSourceLocationGuarantee 
									WHERE AIMSourceID = @AIMSourceID
										and AIMSourceLocationID = @AIMSourceLocationID
										and AIMMaterialID = @AIMMaterialID
										and AIMNutrientID = @AIMNutrientID)
				
				IF @Status = 0
					Begin
						SET @ErrorMessage = 'This Supplier Guarantee already exists. (Supplier Guarantee is inactive.) Information is not saved!'				
						SET @ErrorNumber = 1
					end

				SET @ReturnIntegerValue = (SELECT top 1 AIMSourceLocationGuaranteeID FROM dbo.AIMSourceLocationGuarantee 
									WHERE AIMSourceID = @AIMSourceID
									and AIMSourceLocationID = @AIMSourceLocationID
									and AIMMaterialID = @AIMMaterialID
									and AIMNutrientID = @AIMNutrientID)
			END

		If @ErrorNumber = 0
			BEGIN
				INSERT
					dbo.AIMSourceLocationGuarantee
						(
						  AIMSourceID
						, AIMSourceLocationID
						, AIMNutrientID
						, AIMMaterialID
						, [Min]
						, [Max]
						, [Moisture]
						, [Target]
						, CFCUserID
						)
				VALUES
						(
						  @AIMSourceID
						, @AIMSourceLocationID
						, @AIMNutrientID
						, @AIMMaterialID
						, @Min
						, @Max
						, @Moisture
						, @Target
						, @CFCUserID
						)
				SET @RecordID = @@IDENTITY
			END
	END
ELSE
	BEGIN
		IF @IsActive = 1
			BEGIN
				IF EXISTS(SELECT TOP 1
							S.AIMSourceID
						FROM
							dbo.AIMSource S WITH(NOLOCK)
						WHERE
								S.AIMSourceID = @AIMSourceID AND S.IsActive = 0)
					BEGIN
						SET @ResultNumber = 1
						SET @ResultMessage = 'Unable to reactivate Source Location Guarantee because the Source is not active.'
						SET @RecordID = @@IDENTITY
					END
			END
			BEGIN
				IF EXISTS(SELECT TOP 1
							S.AIMSourceLocationID
						FROM
							dbo.AIMSourceLocation S WITH(NOLOCK)
						WHERE
								S.AIMSourceLocationID = @AIMSourceLocationID AND S.IsActive = 0)
					BEGIN
						SET @ResultNumber = 1
						SET @ResultMessage = 'Unable to reactivate Source Location Guarantee because the Source Location is not active.'
						SET @RecordID = @@IDENTITY
					END
			END

		IF @ResultNumber = 0
			BEGIN
				UPDATE
					dbo.AIMSourceLocationGuarantee WITH(ROWLOCK)
						SET 
							AIMSourceID = ISNULL(@AIMSourceID, AIMSourceID)
							, AIMSourceLocationID = ISNULL(@AIMSourceLocationID, AIMSourceLocationID)
							, AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID)
							, AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
							, [Min] = ISNULL(@Min, [Min])
							, [Max] = ISNULL(@Max, [Max])
							, [Moisture] = ISNULL(@Moisture, [Moisture])
							, [Target] = ISNULL(@Target, [Target])
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
						WHERE 
							AIMSourceLocationGuaranteeID = @AIMSourceLocationGuaranteeID
					SET @RecordID = @@IDENTITY
			END
	END

SELECT 
	  @RecordID as RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END