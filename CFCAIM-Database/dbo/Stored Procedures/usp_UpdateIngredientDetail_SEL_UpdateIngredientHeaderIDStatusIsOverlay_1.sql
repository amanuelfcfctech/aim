CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderIDStatusIsOverlay]
			  @UpdateIngredientHeaderID		INT
			, @Status						INT
			, @IsOverlay					BIT
AS
BEGIN

SET NOCOUNT ON;

--exec usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderID 51

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0
Declare @RecordCount INT = 0

Declare @TotCnt INT = 0

SELECT 
	@TotCnt = COUNT(*) 
FROM 
	dbo.UpdateIngredientDetail D

WHERE 
	D.UpdateIngredientHeaderID = @UpdateIngredientHeaderID
	AND D.Status = @Status
	AND D.IsOverlay = @IsOverlay

SELECT 
	  @TotCnt AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	, @RecordCount AS RecordCount

END