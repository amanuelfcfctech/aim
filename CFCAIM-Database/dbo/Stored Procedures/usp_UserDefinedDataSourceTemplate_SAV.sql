CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_SAV]
			  @UserDefinedDataSourceTemplateID		INT
			, @UserDefinedDataSourceDefinitionID	INT			 = NULL							
			, @SourceColumnName						NVARCHAR(30) = NULL
			, @SourceColumnNumber					INT			 = NULL
			, @MapColumnID							INT			 = NULL
			, @MapColumnName						NVARCHAR(30) = NULL
			, @IsManualEntry						BIT			 = NULL
			, @CFCUserID							INT			 = NULL
			, @IsDetail								BIT			 = NULL
			, @UseAsSampleCode						BIT			 = NULL
			
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @UserDefinedDataSourceTemplateID = 0
	BEGIN
		INSERT
			dbo.UserDefinedDataSourceTemplate 
				(
				UserDefinedDataSourceDefinitionID								
				, SourceColumnName		
				, SourceColumnNumber		
				, MapColumnID
				, MapColumnName
				, IsManualEntry
				, CFCUserID
				, UpdateStamp	
				, IsDetail
				, UseAsSampleCode
					
				)
		VALUES
				(
				@UserDefinedDataSourceDefinitionID											
				, @SourceColumnName
				, @SourceColumnNumber
				, @MapColumnID
				, @MapColumnName		
				, @IsManualEntry								
				, @CFCUserID
				, GETDATE()
				, @IsDetail
				, @UseAsSampleCode
			
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.UserDefinedDataSourceTemplate WITH(ROWLOCK)
				SET 
					UserDefinedDataSourceDefinitionID = ISNULL(@UserDefinedDataSourceDefinitionID, UserDefinedDataSourceDefinitionID)																								
					, SourceColumnName = ISNULL(@SourceColumnName, SourceColumnName)
					, SourceColumnNumber = ISNULL(@SourceColumnNumber, SourceColumnNumber)
					, MapColumnID = ISNULL(@MapColumnID, MapColumnID)													
					, MapColumnName = ISNULL(@MapColumnName, MapColumnName)													
					, IsManualEntry = ISNULL(@IsManualEntry, IsManualEntry)	
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					, IsDetail = ISNULL(@IsDetail, IsDetail)	
					, UseAsSampleCode = ISNULL(@UseAsSampleCode, UseAsSampleCode)	
				
					
				WHERE 
					UserDefinedDataSourceTemplateID = @UserDefinedDataSourceTemplateID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END