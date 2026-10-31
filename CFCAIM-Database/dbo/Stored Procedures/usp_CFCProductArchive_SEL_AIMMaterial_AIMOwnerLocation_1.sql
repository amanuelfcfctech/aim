CREATE PROCEDURE [dbo].[usp_CFCProductArchive_SEL_AIMMaterial_AIMOwnerLocation]
				  @AIMMaterialID			INT
				, @AIMOwnerLocationID		INT 
				
AS
BEGIN
SET NOCOUNT ON;

--exec usp_CFCProductArchive_SEL_AIMMaterial_AIMOwnerLocation 716, 2


SELECT Max(CPA.CFCProductArchiveID) as CFCProductArchiveID
		, CPA.ProductionVersion
		, CPA.ProductionDate
		, CPA.CFCProductID
		--, U.UserName 
	  
FROM 
	dbo.CFCProductArchive CPA WITH(NOLOCK)
	INNER JOIN
		dbo.CFCProduct CP 
			ON CPA.CFCProductID = CP.CFCProductID

	-- 3/9/18
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
    
	X.AIMMaterialID = @AIMMaterialID AND
	PX.AIMOwnerLocationID = @AIMOwnerLocationID AND
	CPA.IsActive = 1

GROUP BY CPA.ProductionVersion
		, CPA.ProductionDate
		, CPA.CFCProductID		
		--, U.UserName 

ORDER BY CPA.ProductionDate DESC, CPA.ProductionVersion DESC

END