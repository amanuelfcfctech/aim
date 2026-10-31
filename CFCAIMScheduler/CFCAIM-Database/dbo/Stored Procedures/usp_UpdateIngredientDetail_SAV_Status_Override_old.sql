CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_SAV_Status_Override_old]
	(@UpdateIngredientDetailID			AS INT,
	@Status								AS INT,
	@Target								AS INT,
	@BypassIsOverlay					AS BIT --user may want to create an overlay even if one does not exist
												--used on export maintenance screen

	, @OverrideAmt						AS	NUMERIC(12,4) = NULL
)	

AS
BEGIN

SET NOCOUNT ON;

--exec usp_UpdateIngredientDetail_SAV_Status 3228, 2

DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0
DECLARE @IsOverlay as INT
DECLARE @IsComputed BIT = 0

DECLARE @Override NUMERIC(12,4) = 0

--6/6/19
IF @OverrideAmt IS NOT NULL
	SET @Override = @OverrideAmt


SET @IsComputed = (SELECT IsComputed FROM dbo.CFCNutrient N 
					INNER JOIN dbo.UpdateIngredientDetail D 
					ON N.CFCNutrientID = D.CFCNutrientID
					WHERE D.UpdateIngredientDetailID = @UpdateIngredientDetailID)

SET @IsOverlay = (SELECT IsOverlay FROM dbo.UpdateIngredientDetail D 					
					WHERE D.UpdateIngredientDetailID = @UpdateIngredientDetailID)

-- if @BypassIsOverlay = 1, user may want to create an overlay even if one does not exist

If @IsComputed = 1
	Begin
		SET @Status = 3
		SET @ResultNumber = 3
	End

--2/5/24 
--Else if @Target <> 1 and @IsOverlay = 0 and @BypassIsOverlay = 0
--Else if @Target <> 1 and @Target <> 4 and @IsOverlay = 0 and @BypassIsOverlay = 0		--Target 1=Base; Target 4=BaseFor
Else if (@Target = 2 OR @Target = 3) and @IsOverlay = 0 and @BypassIsOverlay = 0 
	Begin
		SET @Status = 4
		SET @ResultNumber = 4	
	End

Begin		
	UPDATE dbo.UpdateIngredientDetail with (rowlock)
	SET	Status = @Status 	
	, [Override] = @Override										--6/6/19
	WHERE UpdateIngredientDetailID	 = @UpdateIngredientDetailID	
End

SELECT 
	  @RecordID AS RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END