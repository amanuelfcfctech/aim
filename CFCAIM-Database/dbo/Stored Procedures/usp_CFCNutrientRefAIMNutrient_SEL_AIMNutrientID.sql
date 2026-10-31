CREATE PROCEDURE [dbo].[usp_CFCNutrientRefAIMNutrient_SEL_AIMNutrientID]

@AIMNutrientID INT

AS

BEGIN
SET NOCOUNT ON;

--exec dbo.usp_CFCNutrientRefAIMNutrient_SEL_CFCNutrientID 2


SELECT TOP 1 

	    CNNN.*
		, CN.*
FROM 
	dbo.CFCNutrientRefAIMNutrient CNNN WITH(NOLOCK)
	LEFT OUTER JOIN
			dbo.CFCNutrient CN
			ON CNNN.CFCNutrientID = CN.CFCNutrientID
	
WHERE CNNN.AIMNutrientID = @AIMNutrientID

END