CREATE PROCEDURE [dbo].[usp_SampleExportTemplate_SAV]
			  @SampleExportTemplateID	INT			
			, @TemplateName				NVARCHAR(50)	= NULL
			, @FileTypeID				INT				= NULL	
			, @DelimiterTypeID			INT				= NULL	
			, @ExportPath				NVARCHAR(100)	= NULL
			, @ExportFileName			NVARCHAR(50)	= NULL
			, @CFCUserID				INT				= NULL		
			, @UpdateStamp				DATETIME		= NULL
			
--exec usp_SampleExportTemplate_SAV 0, 'test', 1, 'testpath', 1, '20160520' 
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleExportTemplateID = 0
	BEGIN
		INSERT
			dbo.SampleExportTemplate
				(
				  TemplateName
				, FileTypeID
				, DelimiterTypeID
				, ExportPath
				, ExportFileName
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @TemplateName
				, @FileTypeID
				, @DelimiterTypeID
				, @ExportPath
				, @ExportFileName
				, @CFCUserID
				, GetDate()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleExportTemplate WITH(ROWLOCK)
				SET 
					  TemplateName = ISNULL(@TemplateName, TemplateName)
					, FileTypeID = ISNULL(@FileTypeID, FileTypeID)
					, DelimiterTypeID = ISNULL(@DelimiterTypeID, DelimiterTypeID)
					, ExportPath = ISNULL(@ExportPath, ExportPath)
					, ExportFileName = ISNULL(@ExportFileName, ExportFileName)
					, UpdateStamp = GETDATE()							
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					
					
				WHERE 
					SampleExportTemplateID = @SampleExportTemplateID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END