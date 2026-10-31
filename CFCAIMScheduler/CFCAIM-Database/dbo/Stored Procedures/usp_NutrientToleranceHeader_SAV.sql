CREATE PROCEDURE [dbo].[usp_NutrientToleranceHeader_SAV]
			  @NutrientToleranceHeaderID		INT			
			, @NutrientToleranceHeaderName		NVARCHAR(50)	= NULL				
			, @CFCUserID						INT				= NULL		
			, @UpdateStamp						DATETIME		= NULL
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @NutrientToleranceHeaderID = 0
	BEGIN
		INSERT
			dbo.NutrientToleranceHeader
				(
				  NutrientToleranceHeaderName					
				, CFCUserID
				, UpdateStamp			
				)
		VALUES
				(
				  @NutrientToleranceHeaderName						
				, @CFCUserID
				, GetDate()			
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.NutrientToleranceHeader 
				SET 
					  NutrientToleranceHeaderName = ISNULL(@NutrientToleranceHeaderName, NutrientToleranceHeaderName)					
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
								
				WHERE 
					NutrientToleranceHeaderID = @NutrientToleranceHeaderID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END