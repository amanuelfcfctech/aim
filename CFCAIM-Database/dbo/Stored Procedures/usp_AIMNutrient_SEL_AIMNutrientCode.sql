CREATE PROCEDURE [dbo].[usp_AIMNutrient_SEL_AIMNutrientCode]
			@AIMNutrientCode			VARCHAR(10)
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  NN.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMNutrient NN WITH(NOLOCK)
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON NN.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--		dbo.aspnet_Users AU WITH(NOLOCK)
	--			ON AU.UserId = U.UserID

WHERE 
	NN.AIMNutrientCode = @AIMNutrientCode
END
