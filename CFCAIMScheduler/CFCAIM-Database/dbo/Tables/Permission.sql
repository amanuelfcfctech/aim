CREATE TABLE [dbo].[Permission] (
    [PermissionID] INT          IDENTITY (1, 1) NOT NULL,
    [Description]  VARCHAR (50) NULL,
    [IsActive]     BIT          NULL,
    [UpdateStamp]  DATETIME     CONSTRAINT [DF_Permission_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]    INT          NULL,
    CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED ([PermissionID] ASC)
);

