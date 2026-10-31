CREATE PROCEDURE [dbo].[usp_QueryTemplate_SEL_QueryTemplateID]
				@QueryTemplateID		INT
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

WHERE 
	QueryTemplateID = @QueryTemplateID

END
