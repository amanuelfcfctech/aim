CREATE PROCEDURE [dbo].[usp_UpdateIngredientHeaderAndDetail_DEL]
			  @UpdateIngredientHeaderID	INT			
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

-- 2/14/18 - created this stp to delete newly-created exports where there's nothing to export because cross-references
--				cross-references have to be created.

DELETE FROM dbo.UpdateIngredientDetail with (rowlock) WHERE UpdateIngredientHeaderID = @UpdateIngredientHeaderID
DELETE FROM dbo.UpdateIngredientHeader with (rowlock) WHERE UpdateIngredientHeaderID = @UpdateIngredientHeaderID

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END