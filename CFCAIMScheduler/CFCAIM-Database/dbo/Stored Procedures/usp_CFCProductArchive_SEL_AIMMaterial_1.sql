CREATE PROCEDURE [dbo].[usp_CFCProductArchive_SEL_AIMMaterial]
				  @AIMMaterialID			INT
								
AS
BEGIN
SET NOCOUNT ON;

--exec usp_CFCProductArchive_SEL_AIMMaterial 1391


SELECT Max(CPA.CFCProductArchiveID) as CFCProductArchiveID
		, CPA.ProductionVersion
		, CPA.ProductionDate
		, CPA.CFCProductID	
		, CPA.ArchiveVersion
	  
FROM 
	dbo.CFCProductArchive CPA WITH(NOLOCK)
	INNER JOIN
		dbo.CFCProduct CP 
			ON CPA.CFCProductID = CP.CFCProductID

	LEFT OUTER JOIN
		dbo.CFCPlantRefAIMOwnerLocation PX 
			on CP.CFCPlantID = PX.CFCPlantID

	LEFT OUTER JOIN
		dbo.CFCProductRefAIMMaterial X
			ON CPA.CFCProductID = X.CFCProductID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CPA.CFCUserID = U.CFCUserID

WHERE 	
    
	X.AIMMaterialID = @AIMMaterialID

GROUP BY CPA.ProductionVersion
		, CPA.ProductionDate
		, CPA.CFCProductID		
		, CPA.ArchiveVersion
		

ORDER BY CPA.ProductionDate DESC, CPA.ProductionVersion DESC

END