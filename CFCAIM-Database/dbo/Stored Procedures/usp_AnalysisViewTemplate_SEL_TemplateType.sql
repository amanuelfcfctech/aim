CREATE PROCEDURE [dbo].[usp_AnalysisViewTemplate_SEL_TemplateType]
				  @TemplateType			INT
								
AS
BEGIN
SET NOCOUNT ON;

SELECT
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
	dbo.AnalysisViewTemplate R 

--3/8/23 - added to show last username/updatestamp for pivot reports
LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON R.CFCUserID = U.CFCUserID

	
WHERE
	R.TemplateType = @TemplateType 
	
END