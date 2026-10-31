CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_ProductCodes]
				
AS
BEGIN
SET NOCOUNT ON;

SELECT 	 

	AM.AIMMaterialCode	
	, (Select top 1 AimMaterialName from dbo.AIMMaterial a where a.AIMMaterialCode = AM.AIMMaterialCode) as AimMaterialName
	, (Select top 1 AimMaterialID from dbo.AIMMaterial a where a.AIMMaterialCode = AM.AIMMaterialCode) as AimMaterialID
	
	
FROM 
	dbo.AIMMaterial AM 
WHERE 
	AM.IsActive = 1 and
	AM.MaterialTypeID = 2
GROUP BY 
	AM.AIMMaterialCode		

END