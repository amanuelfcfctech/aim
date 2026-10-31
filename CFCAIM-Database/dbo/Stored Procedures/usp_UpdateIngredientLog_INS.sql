CREATE PROCEDURE [dbo].[usp_UpdateIngredientLog_INS]
			  @UpdateIngredientDetailID		INT				
			, @PlantCode					NVARCHAR(20)	= NULL
			, @IngredientCode				NVARCHAR(20)	= NULL
			, @NutrientCode					NVARCHAR(20)	= NULL
			, @BeforeNutrientValue			NUMERIC(12, 4)	= NULL
			, @AfterNutrientValue			NUMERIC(12, 4)	= NULL
			, @IsOverlay					BIT				= NULL
			, @ErrorID						INT				= NULL
			, @CreateStamp					DATETIME		= NULL
			, @CFCUserID					INT				= NULL
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0


INSERT INTO
	dbo.UpdateIngredientLog 
		(
			UpdateIngredientDetailID 
		,	PlantCode 
		,	IngredientCode 
		,	NutrientCode
		,	BeforeNutrientValue 
		,	AfterNutrientValue 
		,	IsOverlay
		,	ErrorID
		,	CreateStamp 
		,	CFCUserID 
		)
VALUES
		(
			@UpdateIngredientDetailID 
		,	@PlantCode 
		,	@IngredientCode 
		,	@NutrientCode
		,	@BeforeNutrientValue 
		,	@AfterNutrientValue 
		,	@IsOverlay
		,	@ErrorID
		,	GETDATE() 
		,	@CFCUserID 
		)
SET @RecordID = @@IDENTITY

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
