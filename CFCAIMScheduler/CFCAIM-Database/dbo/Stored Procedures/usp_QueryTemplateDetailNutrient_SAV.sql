CREATE PROCEDURE [dbo].[usp_QueryTemplateDetailNutrient_SAV]
			  @QueryTemplateDetailNutrientID	INT
			, @QueryTemplateDetailID			INT		= NULL
			, @AIMNutrientID					INT		= NULL			
			, @CFCUserID						INT		= NULL

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @QueryTemplateDetailNutrientID = 0
	BEGIN
		INSERT
			dbo.QueryTemplateDetailNutrient 
				(
				  QueryTemplateDetailID
				, AIMNutrientID
				, UpdateStamp
				, CFCUserID
				)
		VALUES
				(
				  @QueryTemplateDetailID
				, @AIMNutrientID				
				, GETDATE()
				, @CFCUserID
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.QueryTemplateDetailNutrient WITH(ROWLOCK)
				SET 
					  QueryTemplateDetailID = ISNULL(@QueryTemplateDetailID, QueryTemplateDetailID)
					, AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID)					
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				WHERE 
					QueryTemplateDetailNutrientID = @QueryTemplateDetailNutrientID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END