CREATE TABLE [dbo].[DefaultSettings] (
    [DefaultSettingsID] INT      IDENTITY (1, 1) NOT NULL,
    [DataSourceTypeID]  INT      NULL,
    [UpdateStamp]       DATETIME NULL,
    [CFCUserID]         INT      NULL,
    CONSTRAINT [PK_DefaultSettings] PRIMARY KEY CLUSTERED ([DefaultSettingsID] ASC)
);



