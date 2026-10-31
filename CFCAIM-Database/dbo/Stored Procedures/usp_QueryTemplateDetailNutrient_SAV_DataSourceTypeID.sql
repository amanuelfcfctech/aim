CREATE PROCEDURE [dbo].[usp_QueryTemplateDetailNutrient_SAV_DataSourceTypeID]
			  @QueryTemplateDetailNutrientID	INT
			, @QueryTemplateDetailID			INT		= NULL
			, @DataSourceTypeID					INT		= NULL			
			, @AIMNutrientID					INT		= NULL			
			, @CFCUserID						INT		= NULL

--exec dbo.usp_QueryTemplateDetailNutrient_SAV_DataSourceTypeID 0, 136, 10000, 1, 1

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
				, DataSourceTypeID
				, AIMNutrientID
				, UpdateStamp
				, CFCUserID
				)
		VALUES
				(
				  @QueryTemplateDetailID
				, @DataSourceTypeID
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
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)					
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