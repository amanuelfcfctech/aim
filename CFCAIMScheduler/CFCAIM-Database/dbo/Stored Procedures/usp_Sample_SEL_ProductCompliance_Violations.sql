CREATE PROCEDURE [dbo].[usp_Sample_SEL_ProductCompliance_Violations]
		  @DataSourceTypeID			INT		
		, @ToDate					DATE	= NULL
		, @FromDate					DATE	= NULL
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_Sample_SEL_ProductCompliance 5, '2017-02-28', '2017-02-01'


SELECT 
	  AO.OwnerName 
	, AOL.LocationName 
	, AM.AIMMaterialCode 
	, CPA.ProductionVersion
	, S.SampleDate 
	, S.SampleCode
	, AN.AIMNutrientName
	, UT.UnitTypeDescription
	, SA.AIMUsedValue 
	, ISNULL(CPAS.NutrientMin, 0) AS NutrientMin
	, ISNULL(CPAS.NutrientMax, 0) AS NutrientMax
	, ISNULL(CPAN.NutrientValue, 0) AS NutrientValue
FROM
	dbo.Sample S
	INNER JOIN 
		dbo.SampleAIM SA
			ON SA.SampleID = S.SampleID 
	INNER JOIN 
		dbo.AIMOwner AO
			ON AO.AIMOwnerID = S.AIMOwnerID 
	INNER JOIN 
		dbo.AIMOwnerLocation AOL
			ON AOL.AIMOwnerLocationID = S.AIMOwnerLocationID 
	INNER JOIN 
		dbo.AIMMaterial AM
			ON AM.AIMMaterialID = S.AIMMaterialID 
	INNER JOIN 
		dbo.AIMNutrient AN
			ON AN.AIMNutrientID = SA.AIMNutrientID 
	LEFT OUTER JOIN 
		dbo.UnitType UT
			ON UT.UnitTypeID = AN.UnitTypeID 
	INNER JOIN 
		dbo.CFCProductRefAIMMaterial CPRAM
			ON CPRAM.AIMMaterialID = S.AIMMaterialID 
	INNER JOIN 
		dbo.CFCProductArchive CPA 
			ON CPA.CFCProductID = CPRAM.CFCProductID 
	LEFT OUTER JOIN 
		dbo.CFCNutrientRefAIMNutrient CNRAN
			ON CNRAN.AIMNutrientID = SA.AIMNutrientID 
	LEFT OUTER JOIN 
		dbo.CFCProductArchiveSpec CPAS
			ON	CPAS.CFCProductArchiveID = CPA.CFCProductArchiveID
			AND	CPAS.CFCNutrientID = CNRAN.CFCNutrientID 
	LEFT OUTER JOIN 
		dbo.CFCProductArchiveNutrient CPAN
			ON  CPAN.CFCProductArchiveID = CPA.CFCProductArchiveID 
			AND	CPAN.CFCNutrientID = CNRAN.CFCNutrientID 
WHERE 
	--S.DataSourceTypeID = @DataSourceTypeID	AND 
	S.SampleDate >= ISNULL(@FromDate, '1753-1-1')
	AND S.SampleDate <= ISNULL(@ToDate, GETDATE())	
	AND AM.MaterialTypeID = 2
	AND ((AIMUsedValue < NutrientMin AND NutrientMin <> 0) OR 
		(AIMUsedValue > NutrientMax AND NutrientMax <> 0))

	OR (

	(AIMUsedValue < (NutrientValue - (NutrientValue * .10)) OR AIMUsedValue > (NutrientValue + (NutrientValue * .10)) )
	
	)

END