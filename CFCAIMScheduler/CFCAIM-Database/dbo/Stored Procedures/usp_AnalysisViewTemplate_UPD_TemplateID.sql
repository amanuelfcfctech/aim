CREATE PROCEDURE [dbo].[usp_AnalysisViewTemplate_UPD_TemplateID]
			  @AnalysisViewTemplateID				INT
			, @AnalysisViewTemplateCode				NVARCHAR(10)
			, @AnalysisViewTemplateName				NVARCHAR(30)
			, @AnalysisViewTemplateXML				NVARCHAR(MAX)
			, @CFCUserID							INT
			, @IsActive								BIT

			--4/4/18 -- GridNumber refers [currently] 2 different grids -- the grid for AnalysisIngredient = 1 and the grid for ReportSampleCustom = 3
			, @GridNumber							INT

			--4/12/18 TemplateType 0 = report doesn't use a gridControl or a pivotControl
            --        TemplateType 1 = report uses gridControl 
            --        TemplateType 2 = report uses pivotControl  
			, @TemplateType							INT
					

			, @AIMMaterialID						INT = NULL
			, @AIMOwnerID							INT = NULL
			, @AIMOwnerLocationID					INT = NULL
			, @AIMSupplierID						INT = NULL
			, @AIMSupplierLocationID				INT = NULL
			, @MaterialTypeID						INT = NULL

			

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AnalysisViewTemplateID > 0
BEGIN
	UPDATE
		dbo.AnalysisViewTemplate WITH(ROWLOCK)
			SET 
				  AnalysisViewTemplateCode = @AnalysisViewTemplateCode
				, AnalysisViewTemplateName = @AnalysisViewTemplateName
				, AnalysisViewTemplateXML = @AnalysisViewTemplateXML
				, CFCUserID = @CFCUserID
				, IsActive = @IsActive
				, UpdateStamp = GETDATE()
				, GridNumber = @GridNumber
				, TemplateType = @TemplateType

				,AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
				,AIMOwnerID = ISNULL(@AIMOwnerID, AIMOwnerID)
				,AIMOwnerLocationID = ISNULL(@AIMOwnerLocationID, AIMOwnerLocationID)
				,AIMSupplierID = ISNULL(@AIMSupplierID, AIMSupplierID)
				,AIMSupplierLocationID = ISNULL(@AIMSupplierLocationID, AIMSupplierLocationID)
				,MaterialTypeID = ISNULL(@MaterialTypeID, MaterialTypeID)


			WHERE 
				AnalysisViewTemplateID = @AnalysisViewTemplateID
END
ELSE
BEGIN
	INSERT
		dbo.AnalysisViewTemplate WITH(ROWLOCK)
		(
		  CFCUserID
		, UpdateStamp
		, IsActive
		, AnalysisViewTemplateCode
		, AnalysisViewTemplateName
		, AnalysisViewTemplateXML
		, GridNumber
		, TemplateType

		, AIMMaterialID
		, AIMOwnerID
		, AIMOwnerLocationID
		, AIMSupplierID
		, AIMSupplierLocationID
		, MaterialTypeID

		)
		values
		(
		  @CFCUserID
		, GETDATE()
		, 1
		, @AnalysisViewTemplateCode
		, @AnalysisViewTemplateName
		, @AnalysisViewTemplateXML
		, @GridNumber
		, @TemplateType

		, @AIMMaterialID
		, @AIMOwnerID
		, @AIMOwnerLocationID
		, @AIMSupplierID
		, @AIMSupplierLocationID
		, @MaterialTypeID

		

		)
END
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
