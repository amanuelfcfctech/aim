CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_SAV_Status_Override]
	(@UpdateIngredientDetailID			AS INT
	, @Status							AS INT
	, @OverrideAmt						AS	NUMERIC(12,4) = NULL

	--2/9/24 - commented (moved logic to program - Export Ingredient Maintenance)
	--,@Target							AS INT
	--,@BypassIsOverlay					AS BIT --user may want to create an overlay even if one does not exist
												--used on export maintenance screen	
)	

AS
BEGIN

SET NOCOUNT ON;

DECLARE @Override NUMERIC(12,4) = 0

--2/9/24 - commented (moved logic to program - Export Ingredient Maintenance)
--DECLARE @ResultNumber INT = 0
--DECLARE @ResultMessage VARCHAR(200) = ''
--DECLARE @RecordID INT = 0
--DECLARE @IsOverlay as INT
--DECLARE @IsComputed BIT = 0
--SET @IsComputed = (SELECT IsComputed FROM dbo.CFCNutrient N 
--					INNER JOIN dbo.UpdateIngredientDetail D 
--					ON N.CFCNutrientID = D.CFCNutrientID
--					WHERE D.UpdateIngredientDetailID = @UpdateIngredientDetailID)
--SET @IsOverlay = (SELECT IsOverlay FROM dbo.UpdateIngredientDetail D 					
--					WHERE D.UpdateIngredientDetailID = @UpdateIngredientDetailID)
---- if @BypassIsOverlay = 1, user may want to create an overlay even if one does not exist
--If @IsComputed = 1
--	Begin
--		SET @Status = 3
--		SET @ResultNumber = 3
--	End
--Else if (@Target = 2 OR @Target = 3) and @IsOverlay = 0 and @BypassIsOverlay = 0 
--	Begin
--		SET @Status = 4
--		SET @ResultNumber = 4	
--	End

IF @OverrideAmt IS NOT NULL
	SET @Override = @OverrideAmt

Begin		
	UPDATE dbo.UpdateIngredientDetail with (rowlock)
	SET	Status = @Status 	
	, [Override] = @Override										--6/6/19
	WHERE UpdateIngredientDetailID	 = @UpdateIngredientDetailID	
End

--2/9/24 - commented (moved logic to program - Export Ingredient Maintenance)
--SELECT 
--	  @RecordID AS RecordID
--	, @ResultNumber AS ErrorNumber
--	, @ResultMessage AS ErrorMessage

END