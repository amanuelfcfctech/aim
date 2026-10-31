CREATE PROCEDURE [dbo].[usp_NutrientToleranceHeader_SEL]

AS
BEGIN

--exec usp_NutrientToleranceHeader_SEL

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

		
END