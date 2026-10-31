CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceType_SAV]
			  @DataSourceTypeID		INT
			, @Name					VARCHAR(30) = NULL
			, @DataSourceTypeCode	VARCHAR(20) = NULL
			, @IsActive				BIT			= NULL									
			, @IsFurtherAnalyses	BIT			= NULL
			, @CFCUserID			INT			= NULL
			, @IsUserDefined		BIT			= NULL
			, @IsSQLDatabase		BIT			= NULL
	

--exec usp_UserDefinedDataSourceType_SAV 0, 'cm', 'cm', 1, 0,1, 1, 0
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @DataSourceTypeID = 0
	BEGIN
		INSERT
			dbo.DataSourceType
				(
				  Name
				, DataSourceTypeCode
				, IsActive
				, IsFurtherAnalyses
				, IsUserDefined
				, CFCUserID
				, UpdateStamp
				, IsSQLDatabase				
				)
		VALUES
				(
				  @Name
				, @DataSourceTypeCode
				, @IsActive
				, @IsFurtherAnalyses
				, @IsUserDefined												
				, @CFCUserID
				, GETDATE()
				, @IsSQLDatabase
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.DataSourceType WITH(ROWLOCK)
				SET 
					  Name = ISNULL(@Name, Name)
					, DataSourceTypeCode = ISNULL(@DataSourceTypeCode, DataSourceTypeCode)
					, IsActive = ISNULL(@IsActive, IsActive)	
					, IsFurtherAnalyses = ISNULL(@IsFurtherAnalyses, IsFurtherAnalyses)			
					, IsUserDefined = ISNULL(@IsUserDefined, IsUserDefined)			
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					, IsSQLDatabase = ISNULL(@IsSQLDatabase, IsSQLDatabase)			
					
				WHERE 
					DataSourceTypeID = @DataSourceTypeID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END