CREATE PROCEDURE [dbo].[usp_ProductToleranceNutrient_SAV]
			  @ProductToleranceNutrientID		INT	
			, @ProductToleranceHeaderID			INT			    = NULL
			, @AIMNutrientID					INT			    = NULL
			, @NutrientValue					NUMERIC(18,9)	= NULL
			, @NutrientMin						NUMERIC(18,9)	= NULL
			, @NutrientMax						NUMERIC(18,9)	= NULL			
			, @MinAmount						NUMERIC(18,9)	= NULL	
			, @MinPercent						NUMERIC(18,9)	= NULL
			, @MinPlusMinus						INT				= NULL			
			, @MaxAmount						NUMERIC(18,9)	= NULL	
			, @MaxPercent						NUMERIC(18,9)	= NULL				
			, @MaxPlusMinus						INT				= NULL
			, @CFCUserID						INT				= NULL		
			, @UpdateStamp						DATETIME		= NULL
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @ProductToleranceNutrientID = 0
	BEGIN
		INSERT
			dbo.ProductToleranceNutrient
				(
				  ProductToleranceHeaderID	
				, AIMNutrientID
				, NutrientValue
				, NutrientMin
				, NutrientMax				
				, MinAmount
				, MinPercent
				, MinPlusMinus					
				, MaxAmount
				, MaxPercent	
				, MaxPlusMinus
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @ProductToleranceHeaderID		
				, @AIMNutrientID
				, @NutrientValue
				, @NutrientMin
				, @NutrientMax								
				, @MinAmount
				, @MinPercent
				, @MinPlusMinus						
				, @MaxAmount
				, @MaxPercent	
				, @MaxPlusMinus
				, @CFCUserID
				, GetDate()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.ProductToleranceNutrient 
				SET 
					  ProductToleranceHeaderID = ISNULL(@ProductToleranceHeaderID, ProductToleranceHeaderID)
					, AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID)
					, NutrientValue = ISNULL(@NutrientValue, NutrientValue)			
					, NutrientMin = ISNULL(@NutrientMin, NutrientMin)	
					, NutrientMax = ISNULL(@NutrientMax, NutrientMax)					
					, MinAmount = ISNULL(@MinAmount, MinAmount)
					, MinPercent = ISNULL(@MinPercent, MinPercent)
					, MinPlusMinus = ISNULL(@MinPlusMinus, MinPlusMinus)						
					, MaxAmount = ISNULL(@MaxAmount, MaxAmount)
					, MaxPercent = ISNULL(@MaxPercent, MaxPercent)	
					, MaxPlusMinus = ISNULL(@MaxPlusMinus, MaxPlusMinus)
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)										
				WHERE 
					ProductToleranceNutrientID = @ProductToleranceNutrientID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END