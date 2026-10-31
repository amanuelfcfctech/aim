CREATE PROCEDURE [dbo].[usp_CFCProductArchive_SEL_CFCProductCFCPlantCFCProductRefAIMMaterialAIMMaterial]
				
AS
BEGIN
SET NOCOUNT ON;


SELECT CFCX.CFCProductRefAIMMaterialID
	, CPA.CFCProductArchiveID
	, CPA.CFCProductID
	, CPA.ArchiveVersion
	, CPA.ArchiveDate
	, CPA.ProductionVersion
	, CPA.ProductionDate
	, AM.AIMMaterialID
	, AM.AIMMaterialCode
	, AM.AIMMaterialName		
FROM

	(Select CP.CFCProductID, Max(CPA.ArchiveVersion) as ArchiveVersion, Max( CPA.ArchiveDate) as ArchiveDate		
	From	
		dbo.CFCProductArchive CPA
	Inner Join
		dbo.CFCProduct CP 
			on CPA.CFCProductID = CP.CFCProductID	
	Group By  CP.CFCProductID) X

INNER JOIN 
	dbo.CFCProductArchive CPA 
		ON X.CFCProductID = cpa.CFCProductID AND X.ArchiveVersion = CPA.ArchiveVersion 
INNER JOIN 
	dbo.CFCProduct CP 
		ON CPA.CFCProductID = CP.CFCProductID
INNER JOIN 
	dbo.CFCPlant CPL 
		ON CP.CFCPlantID = CPL.CFCPlantID
INNER JOIN
	dbo.CFCProductRefAIMMaterial CFCX
		ON x.CFCProductID = CFCX.CFCProductID
INNER JOIN
	dbo.AIMMaterial AM 	
		ON cfcx.AIMMaterialID = am.AIMMaterialID

WHERE AM.isactive = 1

END