CREATE PROCEDURE [dbo].[usp_ImportConfig_SAV]
			  @ImportConfigID					INT
			, @Name								NVARCHAR(50) = NULL
			, @ProcessDirectory					NVARCHAR(250) = NULL
			, @BackUpDirectory					NVARCHAR(250) = NULL
			, @CFCUserID						INT = NULL
			, @FileTypeID						INT = NULL
			, @DelimiterTypeID					INT = NULL
			, @DataSourceTypeID					INT = NULL
			, @AIMDatabaseID					INT = NULL
			, @IsCreateSample					BIT = NULL
			, @ExcelSheetNumber					INT = NULL
			, @ServerName						NVARCHAR(50) = NULL
			, @SqlUserName						NVARCHAR(25) = NULL
			, @SqlPassword						NVARCHAR(25) = NULL
			, @IsSqlAuthentication				BIT = NULL
			, @DatabaseName						NVARCHAR(25) = NULL
			, @SelectRequiredAimFields			BIT = NULL
			, @ConnectName						NVARCHAR(50) = NULL
			, @WebServiceID						INT = NULL
			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @ImportConfigID = 0
	BEGIN
		INSERT
			dbo.ImportConfig 
				(
				  Name
				, ProcessDirectory
				, BackUpDirectory
				, UpdateStamp
				, CFCUserID
				, FileTypeID
				, DataSourceTypeID
				, IsCreateSample	
				, AIMDatabaseID
				, DelimiterTypeID
				, ExcelSheetNumber	
				, DatabaseName	
				, SelectRequiredAimFields	
				, ConnectName
				, ServerName
				, SqlUserName	
				, SqlPassword
				, IsSqlAuthentication
				, WebServiceID								
				)
		VALUES
				(
				  @Name
				, @ProcessDirectory
				, @BackUpDirectory
				, GETDATE()
				, @CFCUserID
				, @FileTypeID
				, @DataSourceTypeID
				, @IsCreateSample
				, @AIMDatabaseID
				, @DelimiterTypeID
				, @ExcelSheetNumber				
				, @DatabaseName	
				, @SelectRequiredAimFields
				, @ConnectName
				, @ServerName
				, @SqlUserName	
				, @SqlPassword
				, @IsSqlAuthentication
				, @WebServiceID																
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.ImportConfig WITH(ROWLOCK)
				SET 
					  Name = ISNULL(@Name, Name)
					, ProcessDirectory = ISNULL(@ProcessDirectory, ProcessDirectory)
					, BackUpDirectory = ISNULL(@BackUpDirectory, BackUpDirectory)
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, FileTypeID = ISNULL(@FileTypeID, FileTypeID)
					, DelimiterTypeID = ISNULL(@DelimiterTypeID, DelimiterTypeID)
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
					, AIMDatabaseID = ISNULL(@AIMDatabaseID, AIMDatabaseID)
					, UpdateStamp = GETDATE()
					, IsCreateSample = @IsCreateSample
					, ExcelSheetNumber = ISNULL(@ExcelSheetNumber, ExcelSheetNumber)
					, ServerName = ISNULL(@ServerName, ServerName)
					, SqlUserName = ISNULL(@SqlUserName, SqlUserName)
					, SqlPassword = ISNULL(@SqlPassword, SqlPassword)
					, IsSqlAuthentication = ISNULL(@IsSqlAuthentication, IsSqlAuthentication)
					, DatabaseName = ISNULL(@DatabaseName, DatabaseName)
					, SelectRequiredAimFields = ISNULL(@SelectRequiredAimFields, SelectRequiredAimFields)
					, ConnectName = ISNULL(@ConnectName, ConnectName)
					, WebServiceID = ISNULL(@WebServiceID, WebServiceID)					
				WHERE 
					ImportConfigID = @ImportConfigID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
