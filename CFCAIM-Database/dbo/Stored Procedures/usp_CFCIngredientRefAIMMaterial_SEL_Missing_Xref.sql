CREATE PROCEDURE [dbo].[usp_CFCIngredientRefAIMMaterial_SEL_Missing_Xref]

--exec usp_CFCIngredientRefAIMMaterial_SEL_Missing_Xref
--added to mimix Product-Missing-Xref's  2/17/25

AS
BEGIN

SELECT 
	 CI.CFCIngredientID,
	 CI.CFCIngredientCode,
	 CI.CFCIngredientName,	 
	 X.CFCIngredientRefAIMMaterialID,
	 X.AIMMaterialID

FROM  
	dbo.CFCIngredient CI
LEFT OUTER JOIN
	dbo.CFCIngredientRefAIMMaterial X 
		on CI.CFCIngredientID = X.CFCIngredientID

WHERE 
	X.CFCIngredientID IS NULL and 
		
	(SELECT TOP 1 X.AIMMaterialID 
		   FROM dbo.AIMMaterial AM
		   INNER JOIN dbo.CFCIngredientRefAIMMaterial X on AM.AIMMaterialID = X.AIMMaterialID
		   WHERE AM.AIMMaterialCode = CI.CFCIngredientCode AND
				AM.MaterialTypeID = 1) is null

ORDER BY
	CI.CFCIngredientCode

END