CREATE PROCEDURE [dbo].[usp_NutrientToleranceDetailTarget_SEL_NutrientToleranceHeaderID]

		@NutrientToleranceHeaderID	INT 		

AS
BEGIN


SET NOCOUNT ON;
SELECT 

	NTDT.NutrientToleranceDetailTargetID
	, NTDT.NutrientToleranceHeaderID
	, NTDT.AIMNutrientID
	, NTDT.MinAmount
	, NTDT.MinPercent
	, NTDT.MaxAmount
	, NTDT.MaxPercent
	, NTDT.UpdateStamp
	, NTDT.CFCUserID
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, U.UserName
FROM 
	dbo.NutrientToleranceDetailTarget NTDT 

INNER JOIN 
	dbo.AIMNutrient AN
		ON NTDT.AIMNutrientID = AN.AIMNutrientID
		
LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON NTDT.CFCUserID = U.CFCUserID

WHERE
	NTDT.NutrientToleranceHeaderID = @NutrientToleranceHeaderID 
		
END