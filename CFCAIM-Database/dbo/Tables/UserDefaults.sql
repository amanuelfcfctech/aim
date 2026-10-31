CREATE TABLE [dbo].[UserDefaults] (
    [UserDefaultsID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCUserID]      INT      NULL,
    [FormID]         INT      NULL,
    [ControlID]      INT      NULL,
    [ValueOnOff]     BIT      NULL,
    [UpdateStamp]    DATETIME NULL,
    [UpdateUserID]   INT      NULL,
    CONSTRAINT [PK_UserDefaults] PRIMARY KEY CLUSTERED ([UserDefaultsID] ASC)
);

