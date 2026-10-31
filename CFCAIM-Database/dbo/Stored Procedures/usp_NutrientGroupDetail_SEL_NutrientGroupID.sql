CREATE PROCEDURE [dbo].[usp_NutrientGroupDetail_SEL_NutrientGroupID]
	@NutrientGroupID		INT
AS

SELECT 
	    NGD.NutrientGroupDetailID
      , NGD.NutrientGroupID
      , NGD.AIMNutrientID
	  , N.AIMNutrientName AS NutrientName
	  , UT.UnitTypeDescription AS Units
      , NGD.SortOrder
      , NGD.UpdateStamp
      , NGD.CFCUserID
	  , U.UserName
FROM 
	dbo.NutrientGroupDetail NGD
INNER JOIN
	dbo.AIMNutrient N
	ON N.AIMNutrientID = NGD.AIMNutrientID
INNER JOIN
	dbo.UnitType UT
	ON UT.UnitTypeID = N.UnitTypeID
INNER JOIN
	CFCUser.CFCUser U
	ON U.CFCUserID = NGD.CFCUserID
WHERE
	NGD.NutrientGroupID = @NutrientGroupID
ORDER BY
	NGD.SortOrder