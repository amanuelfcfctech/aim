CREATE PROCEDURE [dbo].[usp_ProductToleranceNutrient_SEL_ProductToleranceHeaderID]

@ProductToleranceHeaderID	INT

AS
BEGIN

--exec usp_ProductToleranceNutrient_SEL_ProductToleranceHeaderID

SET NOCOUNT ON;
SELECT 	
	PTN.ProductToleranceNutrientID
	,PTN.ProductToleranceHeaderID
	,PTN.AIMNutrientID
	,PTN.NutrientValue
	,PTN.NutrientMin
	,PTN.NutrientMax
	,PTN.MinAmount
	,PTN.MinPercent
	,PTN.MinPlusMinus
	,PTN.MaxAmount
	,PTN.MaxPercent
	,PTN.MaxPlusMinus
	,PTN.UpdateStamp
	,PTN.CFCUserID
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, U.UserName
FROM 
	dbo.ProductToleranceNutrient PTN 

INNER JOIN 
	dbo.AIMNutrient AN
		ON PTN.AIMNutrientID = AN.AIMNutrientID
		
LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON PTN.CFCUserID = U.CFCUserID

WHERE
	PTN.ProductToleranceHeaderID = @ProductToleranceHeaderID
		
END