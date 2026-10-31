CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_SEL_Missing_Xref]

--exec usp_CFCProductRefAIMMaterial_SEL_Missing_Xref

AS
BEGIN

SELECT 
	 CP.CFCProductID,
	 CP.CFCProductCode,
	 CP.CFCProductName,
	 CP.CFCPlantID,
	 P.CFCPlantCode,
	 P.CFCPlantName,
	 AOL.AIMOwnerLocationID,
	 AOL.LocationName,
	 X.CFCProductRefAIMMaterialID,
	 X.AIMMaterialID

	 -- Moved to Where Stmt 2/17/25 per Mark
	 --,(SELECT TOP 1 X.AIMMaterialID 
		--   FROM dbo.AIMMaterial AM
		--   INNER JOIN dbo.CFCProductRefAIMMaterial X on AM.AIMMaterialID = X.AIMMaterialID
		--   WHERE AM.AIMMaterialCode = CP.CFCProductCode AND
		--		AM.MaterialTypeID = 2 and
		--		AM.AIMOwnerLocationID <> AOL.AIMOwnerLocationID) AS AlreadyExists 			
	
FROM  
	dbo.CFCProduct CP
LEFT OUTER JOIN
	dbo.CFCProductRefAIMMaterial X 
		on CP.CFCProductID = X.CFCProductID
LEFT OUTER JOIN 
	dbo.CFCPlantRefAIMOwnerLocation XP
		on CP.CFCPlantID = XP.CFCPlantID
LEFT OUTER JOIN
	dbo.CFCPlant P
		ON CP.CFCPLANTID = P.CFCPLANTID
LEFT OUTER JOIN
	dbo.AIMOwnerLocation AOL
		ON XP.AIMOwnerLocationID = AOL.AIMOwnerLocationID
WHERE 
	X.CFCProductID IS NULL and 

	-- Added to Where Stmt from above 2/17/25 per Mark
	(SELECT TOP 1 X.AIMMaterialID 
		   FROM dbo.AIMMaterial AM
		   INNER JOIN dbo.CFCProductRefAIMMaterial X on AM.AIMMaterialID = X.AIMMaterialID
		   WHERE AM.AIMMaterialCode = CP.CFCProductCode AND
				AM.MaterialTypeID = 2 and
				AM.AIMOwnerLocationID <> AOL.AIMOwnerLocationID) is null

ORDER BY
	AOL.LocationName DESC

END