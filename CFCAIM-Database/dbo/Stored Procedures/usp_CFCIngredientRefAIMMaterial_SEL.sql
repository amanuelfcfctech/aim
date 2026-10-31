
CREATE PROCEDURE [dbo].[usp_CFCIngredientRefAIMMaterial_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    CNNN.CFCIngredientRefAIMMaterialID
	  , CNNN.CFCIngredientID
	  , CNNN.AIMMaterialID
	  , CNNN.UpdateStamp
	  , CN.CFCIngredientCode
	  , CN.CFCIngredientName
	  , NN.AIMMaterialCode
	  , NN.AIMMaterialName

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.CFCIngredientRefAIMMaterial CNNN WITH(NOLOCK)
	INNER JOIN
		dbo.CFCIngredient CN
			ON CNNN.CFCIngredientID = CN.CFCIngredientID
	INNER JOIN
		dbo.AIMMaterial NN
			ON CNNN.AIMMaterialID = NN.AIMMaterialID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CNNN.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

END
