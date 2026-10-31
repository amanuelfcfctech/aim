CREATE PROCEDURE [dbo].[usp_AnalyzerDataSourceType_SAV]
			  @AnalyzerDataSourceTypeID			INT						
			, @AnalyzerDataSourceTypeName		NVARCHAR(30)  = NULL
			, @AnalyzerDataSourceTypeCode		NVARCHAR(20)  = NULL
			, @DataSourceTypeID					INT = NULL
			, @IsActive							BIT
			, @CFCUserID						INT


AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''



IF @AnalyzerDataSourceTypeID = 0

	BEGIN
			IF EXISTS(SELECT 
						AnalyzerDataSourceTypeID
					FROM 
						dbo.AnalyzerDataSourceType ADST
					WHERE 
						ADST.AnalyzerDataSourceTypeName = @AnalyzerDataSourceTypeName 
						and ADST.DataSourceTypeID = @DataSourceTypeID)
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'This Data Source name already exists. Please enter a different name. Information is not saved.'

					SET @RecordID = (SELECT 
									AnalyzerDataSourceTypeID 
								FROM 
									dbo.AnalyzerDataSourceType ADST
								WHERE 
									ADST.AnalyzerDataSourceTypeName = @AnalyzerDataSourceTypeName
									and ADST.DataSourceTypeID = @DataSourceTypeID)
				END
							

			ELSE
				BEGIN
					INSERT
						dbo.AnalyzerDataSourceType
							(
							  AnalyzerDataSourceTypeName	
							, AnalyzerDataSourceTypeCode						
							, DataSourceTypeID
							, IsActive
							, UpdateStamp
							, CFCUserID
							)
					VALUES
							(
							  @AnalyzerDataSourceTypeName
							, @AnalyzerDataSourceTypeCode		
							, @DataSourceTypeID					 
							, @IsActive
							, GETDATE()
							, @CFCUserID
							)
					SET @RecordID = @@IDENTITY
				END
	END
ELSE
	BEGIN
		IF @ErrorNumber = 0
			BEGIN

				UPDATE
					dbo.AnalyzerDataSourceType WITH(ROWLOCK)
						SET 
							  AnalyzerDataSourceTypeName = ISNULL(@AnalyzerDataSourceTypeName, AnalyzerDataSourceTypeName)
							, AnalyzerDataSourceTypeCode = ISNULL(@AnalyzerDataSourceTypeCode, AnalyzerDataSourceTypeCode)
							, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
							, IsActive = ISNULL(@IsActive, IsActive)
							, UpdateStamp = GETDATE()
							, CFCUserID = @CFCUserID
						WHERE 
							AnalyzerDataSourceTypeID = @AnalyzerDataSourceTypeID
				SET @RecordID = @AnalyzerDataSourceTypeID
			END
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END