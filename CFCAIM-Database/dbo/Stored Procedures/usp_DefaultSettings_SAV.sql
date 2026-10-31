CREATE PROCEDURE [dbo].[usp_DefaultSettings_SAV]
			  @DefaultSettingsID				INT
			, @DataSourceTypeID					INT			
			, @CFCUserID						INT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @DefaultSettingsID = 0 
	BEGIN
		IF EXISTS(SELECT 
					@DefaultSettingsID 
				FROM 
					dbo.DefaultSettings DS
				WHERE 
						DS.DefaultSettingsID = @DefaultSettingsID )
					
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This default already exists. Information is not saved.'
			END
		ELSE
			BEGIN
				INSERT
					dbo.DefaultSettings
						(
						  DataSourceTypeID						
						, UpdateStamp
						, CFCUserID
						
						)
				VALUES
						(
						  @DataSourceTypeID						
						, GETDATE()
						, @CFCUserID
						
						)
				SET @RecordID = @@IDENTITY
			END
		END
ELSE
	BEGIN	
		
		UPDATE
			dbo.DefaultSettings WITH(ROWLOCK)
				SET 
					  DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)					
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
					
				WHERE 
					DefaultSettingsID = @DefaultSettingsID
		SET @RecordID = @DefaultSettingsID
		
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END