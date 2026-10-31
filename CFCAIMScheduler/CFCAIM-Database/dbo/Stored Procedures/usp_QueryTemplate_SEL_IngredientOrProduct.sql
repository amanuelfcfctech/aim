CREATE PROCEDURE [dbo].[usp_QueryTemplate_SEL_IngredientOrProduct]

	@IngredientOrProduct nvarchar(1)

--used on Analysis screen
--exec dbo.usp_QueryTemplate_SEL_IngredientOrProduct 'I'

AS
BEGIN
SET NOCOUNT ON;

SELECT

	  QT.QueryTemplateID
	, QT.Name
	, QT.UpdateStamp
	, QT.CFCUserID
	, QT.ExportTargetID
	, QT.AIMOwnerLocationID
	, QT.IsAnalysis
	, U.UserName
	, QT.HideInAnalysis


FROM 
	dbo.QueryTemplate QT WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON QT.CFCUserID = U.CFCUserID 

WHERE QT.HideInAnalysis = 0 
		and IngredientOrProduct = @IngredientOrProduct
			
ORDER BY QT.Name 


END