CREATE PROCEDURE [dbo].[usp_CFCProductArchive_SEL_CFCProductID]
				
	@CFCProductID		INT

AS
BEGIN
SET NOCOUNT ON;

--exec dbo.usp_CFCProductArchive_SEL_CFCProductID 1353

SELECT


	CPA.CFCProductArchiveID
	, CPA.CFCProductID
	, CPA.ArchiveVersion
	, CPA.ArchiveDate
	, CPA.ArchiveType
	, CPA.ProductionDate
	, CPA.ProductionVersion
	, CPA.TrialDate
	, CPA.TrialVersion
	, CPA.CFCUserID
	, CPA.UpdateStamp
	, CP.CFCProductCode
	, CP.CFCProductName
	, P.CFCPlantCode
	, P.CFCPlantName
	, U.UserName

FROM
	dbo.CFCProductArchive CPA
INNER JOIN
	dbo.CFCProduct CP
		ON CPA.CFCProductID = CP.CFCProductID
LEFT OUTER JOIN 
	dbo.CFCPlant P
		ON CP.CFCPlantID = P.CFCPlantID
LEFT OUTER JOIN
	CFCUser.CFCUser U
		ON CPA.CFCUserID = U.CFCUserID

WHERE CPA.CFCProductID = @CFCProductID

END