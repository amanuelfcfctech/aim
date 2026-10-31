CREATE TABLE [dbo].[Application] (
    [AppID]       INT           NOT NULL,
    [AppName]     NVARCHAR (50) NULL,
    [IsActive]    BIT           NULL,
    [UpdateStamp] DATETIME      NULL,
    [CFCUserID]   INT           NULL,
    CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED ([AppID] ASC)
);

