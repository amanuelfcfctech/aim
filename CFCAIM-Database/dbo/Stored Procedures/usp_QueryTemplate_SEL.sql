CREATE PROCEDURE [dbo].[usp_QueryTemplate_SEL]

--used on Analysis screen
--exec dbo.usp_QueryTemplate_SEL 0



--10/31/22 -- This stp may no longer be getting used


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

	--10/31/22
	, QT.IngredientOrProduct


FROM 
	dbo.QueryTemplate QT WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON QT.CFCUserID = U.CFCUserID 

WHERE QT.HideInAnalysis = 0
			
ORDER BY QT.Name 


END
