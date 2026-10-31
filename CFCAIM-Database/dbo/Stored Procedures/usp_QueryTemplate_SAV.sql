CREATE PROCEDURE [dbo].[usp_QueryTemplate_SAV]
			  @QueryTemplateID					INT

			-- 6/15/22 - table allows for 30 characters
			--, @Name							NVARCHAR(20)	= NULL
			, @Name								NVARCHAR(30)	= NULL

			, @ExportTargetID					INT				= NULL
			, @AIMOwnerLocationID				INT				= NULL
			, @CFCUserID						INT
			, @IsAnalysis						INT				= NULL
			, @HideInAnalysis					BIT				= NULL

			--10/31/22
			, @IngredientOrProduct				NVARCHAR(1)		= NULL

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @QueryTemplateID = 0
	BEGIN
		INSERT
			dbo.QueryTemplate 
				(
				  Name
				, ExportTargetID
				, AIMOwnerLocationID
				, UpdateStamp
				, CFCUserID
				, IsAnalysis
				, HideInAnalysis
				, IngredientOrProduct
				)
		VALUES
				(
				  @Name
				, @ExportTargetID
				, @AIMOwnerLocationID
				, GETDATE()
				, @CFCUserID
				, @IsAnalysis
				, @HideInAnalysis
				, @IngredientOrProduct
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.QueryTemplate WITH(ROWLOCK)
				SET 
					  Name = ISNULL(@Name, Name)
					, ExportTargetID = ISNULL(@ExportTargetID, ExportTargetID)
					, AIMOwnerLocationID = ISNULL(@AIMOwnerLocationID, AIMOwnerLocationID)
					, UpdateStamp = GETDATE()
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, IsAnalysis = ISNULL(@IsAnalysis, IsAnalysis)
					, HideInAnalysis = ISNULL(@HideInAnalysis, HideInAnalysis)
					, IngredientOrProduct = ISNULL(@IngredientOrProduct, IngredientOrProduct)
				WHERE 
					QueryTemplateID = @QueryTemplateID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
