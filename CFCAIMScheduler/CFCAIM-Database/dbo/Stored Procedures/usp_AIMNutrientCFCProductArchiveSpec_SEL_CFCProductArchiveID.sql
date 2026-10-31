CREATE PROCEDURE [dbo].[usp_AIMNutrientCFCProductArchiveSpec_SEL_CFCProductArchiveID]
				@CFCProductArchiveID				INT


--exec dbo.usp_AIMNutrientCFCProductArchiveSpec_SEL_CFCProductArchiveID 4


AS
BEGIN
SET NOCOUNT ON;

SELECT 
	am.AIMNutrientID
	, am.AIMNutrientCode
	, am.AIMNutrientName
	, x.CFCNutrientID
	, CPAS.CFCProductArchiveID
	, CPAS.CFCProductArchiveSpecID
	, CPAS.NutrientMin
	, CPAS.NutrientMax
	, CPAN.NutrientValue
	, U.UserName

FROM
	dbo.AIMNutrient am
INNER JOIN
	dbo.CFCNutrientRefAIMNutrient x 
		ON am.AIMNutrientID = x.AIMNutrientID
INNER JOIN
	dbo.CFCProductArchiveSpec CPAS
		ON x.CFCNutrientID = CPAS.CFCNutrientID
INNER JOIN
	dbo.CFCProductArchiveNutrient CPAN
		ON CPAS.CFCProductArchiveID = CPAN.CFCProductArchiveID and
		   CPAS.CFCNutrientID = CPAN.CFCNutrientID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON am.CFCUserID = U.CFCUserID
WHERE 
	CPAS.CFCProductArchiveID = @CFCProductArchiveID


END