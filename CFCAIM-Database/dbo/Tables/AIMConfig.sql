CREATE TABLE [dbo].[AIMConfig] (
    [CFCUserID]     INT      NOT NULL,
    [AIMDatabaseID] INT      CONSTRAINT [DF_AIMConfig_ActiveDatabaseID] DEFAULT ((0)) NULL,
    [IsActive]      BIT      CONSTRAINT [DF_AIMConfig_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]   DATETIME CONSTRAINT [DF_AIMConfig_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_AIMConfig] PRIMARY KEY CLUSTERED ([CFCUserID] ASC)
);

