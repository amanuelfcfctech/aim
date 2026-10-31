CREATE PROCEDURE [dbo].[usp_AIMDatabase_SAV]
	  @AIMDatabaseID			INT
	, @Name						NVARCHAR(50)	= NULL
	, @CFCPath					NVARCHAR(200)	= NULL	  
	, @DefaultForPropose		INT
	, @CFCUserID				INT
	, @IsActive					BIT
	, @AIMPriorityUsedValueID	INT
	, @DataSourceTypeID			INT
	, @SyncTime					DATETIME = NULL


--exec usp_AIMDatabase_SAV 1, null, null, 6, 1, 1, 4, 5, '2017-10-18 13:16:34'


AS
BEGIN
SET NOCOUNT ON;

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
	END
END
