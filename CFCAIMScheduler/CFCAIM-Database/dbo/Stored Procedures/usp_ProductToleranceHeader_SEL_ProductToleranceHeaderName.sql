CREATE PROCEDURE [dbo].[usp_ProductToleranceHeader_SEL_ProductToleranceHeaderName]

@ProductToleranceHeaderName	NVarchar(50)

AS
BEGIN

--exec usp_ProductToleranceHeader_SEL_ProductToleranceHeaderName

SET NOCOUNT ON;
SELECT 
	  PTH.ProductToleranceHeaderID
	, PTH.ProductToleranceHeaderName
	, PTH.AIMMaterialID
	, PTH.AIMMaterialCode
	, PTH.UpdateStamp
	, PTH.CFCUserID	
	, PTH.SpecFrom
	, U.UserName
	, AM.AIMMaterialCode
	, AM.AIMMaterialName

FROM 
	dbo.ProductToleranceHeader PTH 

LEFT OUTER JOIN 
	dbo.AIMMaterial AM
		ON PTH.AIMMaterialID = AM.AIMMaterialID
		
LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON PTH.CFCUserID = U.CFCUserID

WHERE
	PTH.ProductToleranceHeaderName = @ProductToleranceHeaderName
		
END