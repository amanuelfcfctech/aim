CREATE PROCEDURE [dbo].[usp_CFCProduct_SEL_PlantID_ProductCode]
				  @PlantID				INT
				, @ProductCode			VARCHAR(8)
				
AS

-- usp_CFCProduct_SEL_PlantID_ProductCode 1, '71500'


BEGIN
SET NOCOUNT ON;


SELECT 
 
  CP.CFCProductID
  , CP.CFCPlantID
  , PL.CFCPlantCode
  , CP.CFCProductCode
  , CP.CFCProductName



FROM 
	dbo.CFCProduct CP
INNER JOIN 
	dbo.CFCPlant PL
		ON PL.CFCPlantID = CP.CFCPlantID

WHERE 

	PL.CFCPlantID = @PlantID  AND    	
	CP.CFCProductCode = @ProductCode 


END