CREATE TABLE [dbo].[UserAccess] (
    [UserAccessID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCUserID]    INT      NOT NULL,
    [PermissionID] INT      NOT NULL,
    [UpdateStamp]  DATETIME CONSTRAINT [DF_UserAccess_UpdateStamp] DEFAULT (getdate()) NULL,
    [UpdateUserID] INT      NULL,
    CONSTRAINT [PK_UserAccess] PRIMARY KEY CLUSTERED ([UserAccessID] ASC, [CFCUserID] ASC, [PermissionID] ASC)
);

