CREATE PROCEDURE [dbo].[usp_AnalysisViewTemplate_SEL_AnalysisViewTemplateID]
				  @AnalysisViewTemplateID			INT
				


AS
BEGIN
SET NOCOUNT ON;

SELECT
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
	, R.PrintLayoutXML --1175FG 7/1/2026

	--3/8/23
	, U.UserName
	
FROM 
	dbo.AnalysisViewTemplate R WITH(NOLOCK)

--3/8/23 - added to show last username/updatestamp for pivot reports
LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON R.CFCUserID = U.CFCUserID
	
WHERE
	R.AnalysisViewTemplateID = @AnalysisViewTemplateID 
	
END