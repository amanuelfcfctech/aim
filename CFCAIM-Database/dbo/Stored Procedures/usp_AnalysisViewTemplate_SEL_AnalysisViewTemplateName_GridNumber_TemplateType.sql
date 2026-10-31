
CREATE PROCEDURE [dbo].[usp_AnalysisViewTemplate_SEL_AnalysisViewTemplateName_GridNumber_TemplateType]
				  @AnalysisViewTemplateName				VARCHAR(30)
				  , @GridNumber							INT
				  , @TemplateType						INT			


AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 

	--3/8/23
	--R.*
	R.AnalysisViewTemplateID
	, R.AnalysisViewTemplateCode
	, R.AnalysisViewTemplateName
	, R.AnalysisViewTemplateXML
	, R.UpdateStamp
	, R.CFCUserID
	, R.IsActive
	, R.GridNumber
	, R.TemplateType
	, R.AIMMaterialID as templateAIMMaterialID
	, R.AIMOwnerID as templateAIMOwnerID
	, R.AIMOwnerLocationID as templateAIMOwnerLocationID
	, R.AIMSupplierID as templateAIMSupplierID
	, R.AIMSupplierLocationID as templateAIMSupplierLocationID
	, R.MaterialTypeID as templateMaterialTypeID

	--3/8/23
	, U.UserName

FROM 
	dbo.AnalysisViewTemplate R WITH(NOLOCK)

--3/8/23 - added to show last username/updatestamp for pivot reports
LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON R.CFCUserID = U.CFCUserID
	
WHERE
	R.AnalysisViewTemplateName = @AnalysisViewTemplateName AND
	R.GridNumber = @GridNumber AND
	R.TemplateType = @TemplateType
	
END