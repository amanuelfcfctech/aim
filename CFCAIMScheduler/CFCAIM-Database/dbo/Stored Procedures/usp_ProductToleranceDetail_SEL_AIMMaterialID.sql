CREATE PROCEDURE [dbo].[usp_ProductToleranceDetail_SEL_AIMMaterialID]

@AIMMaterialID INT

AS
BEGIN

SET NOCOUNT ON;

SELECT 
	PTD.ProductToleranceDetailID
	, PTD.ProductToleranceHeaderID
	, PTD.AIMMaterialID
	, PTD.UpdateStamp
	, PTD.CFCUserID
	, PTH.ProductToleranceHeaderName
	, AM.AIMMaterialCode
	, AM.AIMMaterialName	
	, AM.MaterialTypeID
	, AOL.LocationName as AIMOwnerLocationName
	, U.UserName	
	
FROM 
	dbo.ProductToleranceDetail PTD 
INNER JOIN
	dbo.ProductToleranceHeader PTH 
		ON PTD.ProductToleranceHeaderID = PTH.ProductToleranceHeaderID
LEFT OUTER JOIN 
	dbo.AIMMaterial AM 
		ON PTD.AIMMaterialID = AM.AIMMaterialID
LEFT OUTER JOIN
	dbo.AIMOwnerLocation AOL
		ON AM.AIMOwnerLocationID = AOL.AIMOwnerLocationID
LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON PTD.CFCUserID = U.CFCUserID

WHERE PTD.AIMMaterialID = @AIMMaterialID

END