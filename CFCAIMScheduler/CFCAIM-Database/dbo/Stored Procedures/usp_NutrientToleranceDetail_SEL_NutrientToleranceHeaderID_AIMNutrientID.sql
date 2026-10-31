CREATE PROCEDURE [dbo].[usp_NutrientToleranceDetail_SEL_NutrientToleranceHeaderID_AIMNutrientID]

		@NutrientToleranceHeaderID		INT 
		, @AIMNutrientID				INT

AS
BEGIN

--exec [usp_NutrientToleranceDetail_SEL_NutrientToleranceHeaderID_AIMNutrientID] 1

SET NOCOUNT ON;
SELECT 

	NTD.NutrientToleranceDetailID
	, NTD.NutrientToleranceHeaderID
	, NTD.AIMNutrientID
	, NTD.MinAmount
	, NTD.MinPercent
	, NTD.MaxAmount	
	, NTD.MaxPercent
	, NTD.UpdateStamp
	, NTD.CFCUserID
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, U.UserName
FROM 
	dbo.NutrientToleranceDetail NTD 

INNER JOIN 
	dbo.AIMNutrient AN
		ON NTD.AIMNutrientID = AN.AIMNutrientID
		
LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON NTD.CFCUserID = U.CFCUserID

WHERE
	NTD.NutrientToleranceHeaderID = @NutrientToleranceHeaderID AND
	NTD.AIMNutrientID = @AIMNutrientID
		
END