CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetailGroup_SAV]
		@SampleResultTemplateDetailGroupID		INT = NULL
      , @SampleResultTemplateID					INT = NULL
      , @NutrientGroupID						INT = NULL
      , @TestFrequency							INT = NULL
      , @IsDateFrequency						BIT = NULL
	  , @StartDate								DATETIME = NULL
      , @UpdateStamp							DATETIME = NULL
      , @CFCUserID								INT = NULL
						
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleResultTemplateDetailGroupID = 0
	BEGIN			

		INSERT
			dbo.SampleResultTemplateDetailGroup
				(
				    SampleResultTemplateID
				  , NutrientGroupID
				  , TestFrequency
				  , IsDateFrequency
				  , StartDate
				  , UpdateStamp
				  , CFCUserID			
				)
		VALUES
				(
				  @SampleResultTemplateID
				, @NutrientGroupID	
				, @TestFrequency
				, @IsDateFrequency	
				, @StartDate
				, GetDate()
				, @CFCUserID
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		IF @ErrorNumber = 0
			UPDATE
				dbo.SampleResultTemplateDetailGroup WITH(ROWLOCK)
					SET 
						   SampleResultTemplateID = ISNULL(@SampleResultTemplateID, SampleResultTemplateID)
						  , NutrientGroupID = ISNULL(@NutrientGroupID, NutrientGroupID)
						  , TestFrequency = ISNULL(@TestFrequency, TestFrequency)
						  , IsDateFrequency = ISNULL(@IsDateFrequency, IsDateFrequency)	
						  , StartDate = ISNULL(@StartDate, StartDate)
						  , UpdateStamp = GETDATE()							
						  , CFCUserID = ISNULL(@CFCUserID, CFCUserID)											
					WHERE 
						SampleResultTemplateDetailGroupID = @SampleResultTemplateDetailGroupID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END