CREATE PROCEDURE [dbo].[usp_NutrientToleranceDetail_SAV]
			  @NutrientToleranceDetailID				INT	
			, @NutrientToleranceHeaderID				INT				= NULL
			, @AIMNutrientID							INT			    = NULL						
			, @MinAmount								NUMERIC(18,9)	= NULL	
			, @MinPercent								NUMERIC(18,9)	= NULL					
			, @MaxAmount								NUMERIC(18,9)	= NULL	
			, @MaxPercent								NUMERIC(18,9)	= NULL				
			, @CFCUserID								INT				= NULL		
			, @UpdateStamp								DATETIME		= NULL
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @NutrientToleranceDetailID = 0
	BEGIN
		INSERT
			dbo.NutrientToleranceDetail
				(
				  NutrientToleranceHeaderID	
				, AIMNutrientID			
				, MinAmount
				, MinPercent
				, MaxAmount
				, MaxPercent				
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @NutrientToleranceHeaderID		
				, @AIMNutrientID								
				, @MinAmount
				, @MinPercent				
				, @MaxAmount
				, @MaxPercent						
				, @CFCUserID
				, GetDate()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.NutrientToleranceDetail 
				SET 
					  NutrientToleranceHeaderID = ISNULL(@NutrientToleranceHeaderID, NutrientToleranceHeaderID)
					, AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID)					
					, MinAmount = ISNULL(@MinAmount, MinAmount)
					, MinPercent = ISNULL(@MinPercent, MinPercent)					
					, MaxAmount = ISNULL(@MaxAmount, MaxAmount)
					, MaxPercent = ISNULL(@MaxPercent, MaxPercent)							
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)										
				WHERE 
					NutrientToleranceDetailID = @NutrientToleranceDetailID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END