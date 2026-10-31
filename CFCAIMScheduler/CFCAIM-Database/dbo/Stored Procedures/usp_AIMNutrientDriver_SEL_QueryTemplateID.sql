CREATE PROCEDURE [dbo].[usp_AIMNutrientDriver_SEL_QueryTemplateID]
	@QueryTemplateID INT

AS
BEGIN
SET NOCOUNT ON;
  
SELECT
     ND.*

FROM 
   dbo.AIMNutrientDriver ND WITH(NOLOCK)

WHERE
	ND.QueryTemplateID = @QueryTemplateID
END