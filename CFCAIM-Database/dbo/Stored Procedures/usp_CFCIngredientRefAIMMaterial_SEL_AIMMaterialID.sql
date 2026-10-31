CREATE PROCEDURE [dbo].[usp_CFCIngredientRefAIMMaterial_SEL_AIMMaterialID]

@AIMMaterialID INT

AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 

	CNNN.*
	, CN.*
	, NN.*
	  
FROM 
	dbo.CFCIngredientRefAIMMaterial CNNN WITH(NOLOCK)
	INNER JOIN
		dbo.CFCIngredient CN
			ON CNNN.CFCIngredientID = CN.CFCIngredientID

	INNER JOIN
		dbo.AIMMaterial NN
			ON CNNN.AIMMaterialID = NN.AIMMaterialID


WHERE CNNN.AIMMaterialID = @AIMMaterialID

END