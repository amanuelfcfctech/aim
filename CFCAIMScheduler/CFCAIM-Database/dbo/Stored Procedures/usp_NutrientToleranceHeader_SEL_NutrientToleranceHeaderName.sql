CREATE PROCEDURE [dbo].[usp_NutrientToleranceHeader_SEL_NutrientToleranceHeaderName]

@NutrientToleranceHeaderName	NVarchar(50)

AS
BEGIN

--exec usp_NutrientToleranceHeader_SEL_NutrientToleranceHeaderName

SET NOCOUNT ON;
SELECT 
	  NTH.NutrientToleranceHeaderID
	, NTH.NutrientToleranceHeaderName
	, NTH.UpdateStamp
	, NTH.CFCUserID	
	, U.UserName
	
FROM 
	dbo.NutrientToleranceHeader NTH 

LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON NTH.CFCUserID = U.CFCUserID

WHERE
	NTH.NutrientToleranceHeaderName = @NutrientToleranceHeaderName
		
END