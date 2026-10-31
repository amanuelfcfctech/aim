CREATE PROCEDURE [dbo].[usp_SampleQualityControl_SAV]
			  @SampleQualityControlID	INT
			, @SampleID					INT				 = NULL
			, @AIMNutrientID			INT				 = NULL
			, @SampleWeightBegin		NUMERIC(18,8)	 = NULL
			, @SampleWeightEnd			NUMERIC(18,8)	 = NULL
			, @CFCUserID				INT				 = NULL
		

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber			INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage			VARCHAR(200) = ''

---------------------------------------- INSERT
IF @SampleQualityControlID = 0 
	BEGIN				
		INSERT INTO 
			dbo.SampleQualityControl
				(
				  SampleID 
				, AIMNutrientID
				, SampleWeightBegin
				, SampleWeightEnd
				, CFCUserID				
				
				)
		VALUES
				(
				@SampleID
				, @AIMNutrientID
				, @SampleWeightBegin
				, @SampleWeightEnd
				, @CFCUserID										
				)				
		SET	@SampleQualityControlID = @@IDENTITY
		
	END

---------------------------------------- UPDATE
ELSE
	BEGIN
		UPDATE
			dbo.SampleQualityControl
				SET					  
					 SampleID = ISNULL(@SampleID, SampleID)
					 , AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID)
					 , SampleWeightBegin = ISNULL(@SampleWeightBegin, SampleWeightBegin)
					 , SampleWeightEnd = ISNULL(@SampleWeightEnd, SampleWeightEnd)
					 , CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					 , UpdateStamp = GETDATE()
				WHERE
					SampleQualityControlID = @SampleQualityControlID
		SET @RecordID = @@IDENTITY 
	END			

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END