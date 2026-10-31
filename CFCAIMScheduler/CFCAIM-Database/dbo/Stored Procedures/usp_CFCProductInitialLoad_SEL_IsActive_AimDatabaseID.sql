CREATE PROCEDURE [dbo].[usp_CFCProductInitialLoad_SEL_IsActive_AimDatabaseID]
				@IsActive				BIT
				, @AIMDatabaseID		INT
AS				
BEGIN
SET NOCOUNT ON;

--exec usp_CFCProductInitialLoad_SEL_IsActive_AimDatabaseID 1, 1


-- PRODUCTS -------------------------------------------------------
SELECT CP.CFCProductID as ID, CP.CFCProductCode as Code, CP.CFCProductName as Name, CP.MaterialType as Type, CP.CFCPlantID as PlantID 
FROM
	(Select CP.CFCProductID, CP.CFCProductCode, CP.CFCProductName, 2 as MaterialType, CP.CFCPlantID
	From
		dbo.CFCProduct CP WITH (NOLOCK)	
	LEFT OUTER JOIN 
		dbo.AIMDatabase AD with (nolock) 
			ON CP.AIMDatabaseID = AD.AIMDatabaseID
	LEFT OUTER JOIN 
		dbo.AIMMaterial AM with (nolock) 
			ON CP.CFCProductName = AM.AIMMaterialName 
	WHERE 
		CP.IsActive = @IsActive AND
		CP.AIMDatabaseID = @AIMDatabaseID 
		--AND	AM.AIMMaterialID IS NULL
		) CP

UNION

-- INGREDIENTS ----------------------------------------------------
SELECT CI.CFCIngredientID as ID, CI.CFCIngredientCode as Code, CI.CFCIngredientName as Name, 1 as Type, 0 as PlantID
FROM
	(Select CI.CFCIngredientID, CI.CFCIngredientCode, CI.CFCIngredientName, 1 as MaterialType
	From
		dbo.CFCIngredient CI WITH (NOLOCK)	
	LEFT OUTER JOIN 
		dbo.AIMDatabase AD with (nolock) 
			ON CI.AIMDatabaseID = AD.AIMDatabaseID
	LEFT OUTER JOIN 
		dbo.AIMMaterial AM with (nolock) 
			ON CI.CFCIngredientName = AM.AIMMaterialName 
	WHERE 
		CI.IsActive = @IsActive AND
		CI.AIMDatabaseID = @AIMDatabaseID 
		--AND	AM.AIMMaterialID IS NULL
		) CI			

ORDER BY Name

END