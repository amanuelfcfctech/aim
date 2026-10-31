CREATE PROCEDURE [dbo].[usp_CFCProductArchive_SEL_AIMMaterial_AIMOwnerLocation]
				  @AIMMaterialID			INT
				, @AIMOwnerLocationID		INT 
				
AS
BEGIN
SET NOCOUNT ON;

--exec usp_CFCProductArchive_SEL_AIMMaterial_AIMOwnerLocation 5150, 16


SELECT Max(CPA.CFCProductArchiveID) as CFCProductArchiveID
		, CPA.ProductionVersion
		, CPA.ProductionDate
		, CPA.CFCProductID
		, AU.UserName 
	  
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
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID	

WHERE 	
    
	X.AIMMaterialID = @AIMMaterialID AND
	PX.AIMOwnerLocationID = @AIMOwnerLocationID

GROUP BY CPA.ProductionVersion
		, CPA.ProductionDate
		, CPA.CFCProductID
		, AU.UserName 

ORDER BY CPA.ProductionDate DESC, CPA.ProductionVersion DESC

END