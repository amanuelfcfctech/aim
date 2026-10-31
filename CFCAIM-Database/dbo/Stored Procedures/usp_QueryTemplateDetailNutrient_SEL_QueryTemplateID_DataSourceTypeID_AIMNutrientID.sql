CREATE Procedure [dbo].[usp_QueryTemplateDetailNutrient_SEL_QueryTemplateID_DataSourceTypeID_AIMNutrientID]
				@QueryTemplateID			INT
				, @DataSourceTypeID			INT
				, @AIMNutrientID			INT

--exec usp_QueryTemplateDetailNutrient_SEL_QueryTemplateID_DataSourceTypeID_AIMNutrientID 21, 1, 1

AS
BEGIN
SET NOCOUNT ON;

SELECT	
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
	and QTD.Value = @DataSourceTypeID 
	and QTDN.AIMNutrientID = @AIMNutrientID

END