CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetail_SAV]
			  @SampleResultTemplateDetailID	INT
			, @SampleResultTemplateID		INT				= NULL
			, @AnalyzerNutrientID			INT				= NULL
			, @SortOrder					INT				= NULL			
			, @UpdateStamp					DATETIME		= NULL
			, @CFCUserID					INT				= NULL
			, @ResultValue					DECIMAL(18,8)	= NULL
			, @SampleID						INT				= NULL
			, @CurrentValue					DECIMAL(18,8)	= NULL
						
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleResultTemplateDetailID = 0
	BEGIN

		IF EXISTS(SELECT 
						SampleResultTemplateDetailID 
				FROM 
						dbo.SampleResultTemplateDetail SRTD
				WHERE 							
						SRTD.SampleResultTemplateID = @SampleResultTemplateID and 
						SRTD.AnalyzerNutrientID = @AnalyzerNutrientID)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This nutrient already exists on template.'
			END
		ELSE
			BEGIN			

				INSERT
					dbo.SampleResultTemplateDetail
						(
						SampleResultTemplateID
						, AnalyzerNutrientID
						, SortOrder
						, CFCUserID
						, UpdateStamp				
						)
				VALUES
						(
						@SampleResultTemplateID
						, @AnalyzerNutrientID
						, @SortOrder									
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
				dbo.SampleResultTemplateDetail WITH(ROWLOCK)
					SET 
						  SampleResultTemplateID = ISNULL(@SampleResultTemplateID, SampleResultTemplateID)
						, AnalyzerNutrientID = ISNULL(@AnalyzerNutrientID, AnalyzerNutrientID)					
						, SortOrder = ISNULL(@SortOrder, SortOrder)														
						, UpdateStamp = GETDATE()							
						, CFCUserID = ISNULL(@CFCUserID, CFCUserID)											
					WHERE 
						SampleResultTemplateDetailID = @SampleResultTemplateDetailID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END