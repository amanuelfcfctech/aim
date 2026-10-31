CREATE TABLE [dbo].[UserMenuAccess] (
    [UserMenuAccessID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCUserID]        INT      NOT NULL,
    [MenuItemsID]      INT      NOT NULL,
    [UpdateStamp]      DATETIME CONSTRAINT [DF_UserMenuAccess_UpdateStamp] DEFAULT (getdate()) NULL,
    [UpdateUserID]     INT      NULL,
    [IsNotAllowed]     BIT      NULL,
    CONSTRAINT [PK_UserMenuAccess] PRIMARY KEY CLUSTERED ([UserMenuAccessID] ASC, [CFCUserID] ASC, [MenuItemsID] ASC)
);

