CREATE procedure [dbo].[usp_QueryTemplateDetailNutrient_SEL_QueryTemplateID]
				@QueryTemplateID		INT

-- exec usp_QueryTemplateDetailNutrient_SEL_QueryTemplateID 15

AS
BEGIN
SET NOCOUNT ON;

SELECT	TOP 1
	  QTDN.*
	  , U.UserName
	  	
FROM 
	dbo.QueryTemplateDetail QTD
	INNER JOIN 
		dbo.QueryTemplateDetailNutrient QTDN 
			ON QTD.QueryTemplateDetailID = QTDN.QueryTemplateDetailID

	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON QTDN.CFCUserID = U.CFCUserID
	
WHERE 
	QTD.QueryTemplateID = @QueryTemplateID
	
END