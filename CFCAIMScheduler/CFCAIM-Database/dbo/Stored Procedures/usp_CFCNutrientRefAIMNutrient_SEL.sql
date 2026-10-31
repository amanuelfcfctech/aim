

CREATE PROCEDURE [dbo].[usp_CFCNutrientRefAIMNutrient_SEL]
AS

BEGIN
SET NOCOUNT ON;

--exec dbo.usp_CFCNutrientRefAIMNutrient_SEL


SELECT

	    CNNN.CFCNutrientRefAIMNutrientID
	  , CNNN.CFCNutrientID
	  , CNNN.AIMNutrientID	  
	  , CNNN.UpdateStamp
	  , CN.CFCNutrientCode
	  , CN.CFCNutrientName	  
	  , CN.Unit
	  , AN.AIMNutrientCode
	  , AN.AIMNutrientName
	  , UT.UnitTypeDescription as UnitType	  
	  , cn.AIMDatabaseID
	  , *

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.CFCNutrientRefAIMNutrient CNNN WITH(NOLOCK)

		LEFT OUTER JOIN
			dbo.CFCNutrient CN
			ON CNNN.CFCNutrientID = CN.CFCNutrientID					

		LEFT OUTER JOIN
			dbo.AIMNutrient AN
			ON CNNN.AIMNutrientID = AN.AIMNutrientID

		LEFT OUTER JOIN
			dbo.UnitType UT 
			ON AN.UnitTypeID = UT.UnitTypeID

		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
			ON CNNN.CFCUserID = U.CFCUserID

	    -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--		dbo.aspnet_Users AU WITH(NOLOCK)
		--			ON AU.UserId = U.UserID

END



