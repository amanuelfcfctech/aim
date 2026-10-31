CREATE PROCEDURE [dbo].[usp_CFCProduct_SEL_PlantCodeProductCodeProductionVersion]
				  @PlantCode			VARCHAR(8)
				, @ProductCode			VARCHAR(8)
				, @ProductionVersion	VARCHAR(8)
AS

-- exec usp_CFCProduct_SEL_PlantCodeProductCodeProductionVersion 'ASH', '03000', '32'

BEGIN
SET NOCOUNT ON;


DECLARE @ArchiveVersion		varchar(8)
SET @ArchiveVersion = 
	(SELECT TOP 1 ArchiveVersion
	FROM 
		dbo.CFCProduct CP
	INNER JOIN 
		dbo.CFCPlant CPL
			ON CPL.CFCPlantID = CP.CFCPlantID
	INNER JOIN 
		dbo.CFCProductArchive CPA
			ON CPA.CFCProductID = CP.CFCProductID
	INNER JOIN 
		dbo.CFCProductArchiveNutrient CPAN
			ON CPAN.CFCProductArchiveID = CPA.CFCProductArchiveID
	INNER JOIN 
		dbo.CFCNutrientRefAIMNutrient CNRAN
			ON CNRAN.CFCNutrientID = CPAN.CFCNutrientID
	INNER JOIN 
		dbo.AIMNutrient AN
			ON AN.AIMNutrientID = CNRAN.AIMNutrientID
	LEFT OUTER JOIN 
		dbo.UnitType UT
			ON UT.UnitTypeID = AN.UnitTypeID
	WHERE 
		CPL.CFCPlantCode = @PlantCode  AND    	
		CP.CFCProductCode = @ProductCode AND	
		CPA.ProductionVersion = @ProductionVersion

	ORDER BY ArchiveVersion
	)



SELECT 
 AN.AIMNutrientCode
 , AN.AIMNutrientName 
 , UT.UnitTypeDescription
 , CPAN.NutrientValue
 , CPL.CFCPlantCode

FROM 
	dbo.CFCProduct CP
INNER JOIN 
	dbo.CFCPlant CPL
		ON CPL.CFCPlantID = CP.CFCPlantID
INNER JOIN 
	dbo.CFCProductArchive CPA
		ON CPA.CFCProductID = CP.CFCProductID
INNER JOIN 
	dbo.CFCProductArchiveNutrient CPAN
		ON CPAN.CFCProductArchiveID = CPA.CFCProductArchiveID
INNER JOIN 
	dbo.CFCNutrientRefAIMNutrient CNRAN
		ON CNRAN.CFCNutrientID = CPAN.CFCNutrientID
INNER JOIN 
	dbo.AIMNutrient AN
		ON AN.AIMNutrientID = CNRAN.AIMNutrientID
LEFT OUTER JOIN 
	dbo.UnitType UT
		ON UT.UnitTypeID = AN.UnitTypeID

WHERE 

	CPL.CFCPlantCode = @PlantCode  AND    	
	CP.CFCProductCode = @ProductCode AND	
	CPA.ProductionVersion = @ProductionVersion AND
	CPA.ArchiveVersion = @ArchiveVersion


END