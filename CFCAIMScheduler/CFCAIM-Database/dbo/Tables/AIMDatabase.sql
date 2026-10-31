CREATE TABLE [dbo].[AIMDatabase] (
    [AIMDatabaseID]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]                   VARCHAR (50)  NULL,
    [CFCPath]                VARCHAR (200) NULL,
    [DefaultForPropose]      INT           NULL,
    [UpdateStamp]            DATETIME      CONSTRAINT [DF_AIMDatabase_UpdateStamp] DEFAULT (getdate()) NULL,
    [IsActive]               BIT           CONSTRAINT [DF_AIMDatabase_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]              INT           NULL,
    [SyncTime]               DATETIME      NULL,
    [SyncProgressPercentage] INT           NULL,
    [AIMPriorityUsedValueID] INT           NULL,
    [DataSourceTypeID]       INT           CONSTRAINT [DF_AIMDatabase_DataSourceTypeID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Database] PRIMARY KEY CLUSTERED ([AIMDatabaseID] ASC)
);








GO


CREATE TRIGGER [dbo].[AIMDatabase_UPD_SyncProgress] 
   ON  [dbo].[AIMDatabase] 
   AFTER UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF (UPDATE(SyncProgressPercentage))
	BEGIN
		IF (Exists(select SyncProgressPercentage from AIMDatabase WHERE SyncProgressPercentage = 100))
		BEGIN
			update dbo.AIMDatabase
			set
				SyncTime = GETDATE()
			where
				AIMDatabaseID = (SELECT AIMDatabaseID FROM deleted)
		END
	END
END
