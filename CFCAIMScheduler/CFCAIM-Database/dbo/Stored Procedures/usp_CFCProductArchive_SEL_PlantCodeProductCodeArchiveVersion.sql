CREATE PROCEDURE [dbo].[usp_CFCProductArchive_SEL_PlantCodeProductCodeArchiveVersion]
				  @PlantCode			VARCHAR(8)
				, @ProductCode			VARCHAR(8)
				, @ArchiveVersion		VARCHAR(8)
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  CPA.CFCProductArchiveID
	, CPA.CFCProductID
	, CPA.ArchiveVersion
	, CPA.ArchiveDate
	, CPA.ArchiveType
	, CPA.ArchiveUser
	, CPA.ProductionDate
	, CPA.ProductionVersion
	, CPA.ProductionUser
	, CPA.TrialDate
	, CPA.TrialVersion
	, CPA.TrialUser
	, CPA.UpdateStamp
	, CPA.CFCUserID
FROM
	dbo.CFCProductArchive CPA
	INNER JOIN 
		dbo.CFCProduct CP
			ON CPA.CFCProductID = CP.CFCProductID 	
	INNER JOIN 
		dbo.CFCPlant CPT
			ON CPT.CFCPlantID = CP.CFCPlantID 
WHERE
	CPT.CFCPlantCode = @PlantCode
	AND CP.CFCProductCode = @ProductCode
	AND CPA.ArchiveVersion = @ArchiveVersion
END