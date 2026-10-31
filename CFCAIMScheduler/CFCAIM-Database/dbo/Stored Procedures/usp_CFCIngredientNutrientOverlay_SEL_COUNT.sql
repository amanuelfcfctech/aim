CREATE PROCEDURE [dbo].[usp_CFCIngredientNutrientOverlay_SEL_COUNT]

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


SET @RecordID = (SELECT Count(*) as Count 
	  
FROM 
	dbo.CFCIngredientNutrientOverlay CINO WITH(NOLOCK) )

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END