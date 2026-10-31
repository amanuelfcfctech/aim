CREATE PROCEDURE [dbo].[usp_CFCProductArchive_SEL_AIMMaterial]
				  @AIMMaterialID			INT
				
AS
BEGIN
SET NOCOUNT ON;

--exec usp_CFCProductArchive_SEL_AIMMaterial 1


SELECT CPA.*
		, AU.UserName 
	  
FROM 
	dbo.CFCProductArchive CPA WITH(NOLOCK)
	INNER JOIN
		dbo.CFCProduct CP 
			ON CPA.CFCProductID = CP.CFCProductID
	left JOIN
		dbo.CFCProductRefAIMMaterial X
			ON CPA.CFCProductID = X.CFCProductID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CPA.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
		dbo.aspnet_Users AU WITH(NOLOCK)
			ON AU.UserId = U.UserID	

WHERE 	
	X.AIMMaterialID = @AIMMaterialID

ORDER BY CPA.ProductionDate DESC

END