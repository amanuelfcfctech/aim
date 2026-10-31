CREATE PROCEDURE [dbo].[usp_SampleResultTemplate_SAV]
			  @SampleResultTemplateID	INT			
			, @SampleResultTemplateName NVARCHAR(50)	= NULL
			, @DataSourceTypeID	INT						= NULL	
			, @AIMMaterialID	INT						= NULL
			, @CFCUserID		INT						= NULL		
			, @CurrentIteration	INT
			, @IsGroupTemplate	BIT

			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleResultTemplateID = 0
	BEGIN
		INSERT
			dbo.SampleResultTemplate
				(
				  SampleResultTemplateName
				, DataSourceTypeID
				, AIMMaterialID
				, CFCUserID
				, UpdateStamp			
				, CurrentIteration
				, IsGroupTemplate
				)
		VALUES
				(
				  @SampleResultTemplateName
				, @DataSourceTypeID
				, @AIMMaterialID									
				, @CFCUserID
				, GetDate()		
				, @CurrentIteration	
				, @IsGroupTemplate		
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleResultTemplate WITH(ROWLOCK)
				SET 
					  SampleResultTemplateName = ISNULL(@SampleResultTemplateName, SampleResultTemplateName)
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
					, AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)						
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, CurrentIteration = ISNULL(@CurrentIteration, CurrentIteration)
					, IsGroupTemplate = ISNULL(@IsGroupTemplate, IsGroupTemplate)
				WHERE 
					SampleResultTemplateID = @SampleResultTemplateID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END