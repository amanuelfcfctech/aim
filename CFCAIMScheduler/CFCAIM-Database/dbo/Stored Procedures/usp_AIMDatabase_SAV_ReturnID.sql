CREATE PROCEDURE [dbo].[usp_AIMDatabase_SAV_ReturnID]
	  @AIMDatabaseID			INT
	, @Name						NVARCHAR(50)	= NULL
	, @CFCPath					NVARCHAR(200)	= NULL	  
	, @DefaultForPropose		INT = NULL
	, @CFCUserID				INT = NULL
	, @IsActive					BIT = NULL
	, @AIMPriorityUsedValueID	INT = NULL
	, @DataSourceTypeID			INT = NULL	
	, @SyncTime					DATETIME = NULL

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AIMDatabaseID = 0 
	BEGIN 
		INSERT INTO
			dbo.AIMDatabase	
				(
				  Name
				, CFCPath
				, DefaultForPropose
				, IsActive
				, UpdateStamp
				, CFCUserID
				, AIMPriorityUsedValueID
				, DataSourceTypeID
				, SyncTime
			
				)
		VALUES
				(
				  @Name
				, @CFCPath
				, @DefaultForPropose
				, 1
				, GETDATE()
				, @CFCUserID
				, @AIMPriorityUsedValueID
				, @DataSourceTypeID
				, GETDATE()
		
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.AIMDatabase	
				SET
					Name = ISNULL(@Name, Name)
					, CFCPath = ISNULL(@CFCPath, CFCPath)
					, DefaultForPropose = ISNULL(@DefaultForPropose, DefaultForPropose)
					, IsActive = @IsActive
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
					, AIMPriorityUsedValueID = ISNULL(@AIMPriorityUsedValueID, AIMPriorityUsedValueID)
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
					, SyncTime = ISNULL(@SyncTime, SyncTime)
					
		WHERE
			AIMDatabaseID = @AIMDatabaseID

		SET @RecordID = @@IDENTITY
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END