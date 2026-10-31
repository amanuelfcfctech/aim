
CREATE PROCEDURE [dbo].[usp_AnalysisTemplate_UPD_AnalysisTemplateID]
			  @AnalysisTemplateID					INT
			, @AnalysisTemplateName					NVARCHAR(30)
			, @MathType								INT
			, @CFCUserID							INT
			, @IsActive								BIT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AnalysisTemplateID > 0
BEGIN
	UPDATE
		dbo.AnalystTemplate WITH(ROWLOCK)
			SET 
				  AnalysisTemplateName = @AnalysisTemplateName
				, MathType = @MathType
				, CFCUserID = @CFCUserID
				, IsActive = @IsActive
				, UpdateStamp = GETDATE()
			WHERE 
				AnalysisTemplateID = @AnalysisTemplateID
END
ELSE
BEGIN
	INSERT
		dbo.AnalystTemplate WITH(ROWLOCK)
		(
		  CFCUserID
		, UpdateStamp
		, IsActive
		, AnalysisTemplateName
		, MathType
		)
		values
		(
		  @CFCUserID
		, GETDATE()
		, 1
		, @AnalysisTemplateName
		, @MathType
		)
END
SELECT 
	  @@IDENTITY AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
