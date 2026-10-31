CREATE TABLE [dbo].[aspnet_Users] (
    [ApplicationId]    UNIQUEIDENTIFIER NOT NULL,
    [UserId]           UNIQUEIDENTIFIER CONSTRAINT [DF__aspnet_Us__UserI__1E6F845E] DEFAULT (newid()) NOT NULL,
    [UserName]         NVARCHAR (256)   NOT NULL,
    [LoweredUserName]  NVARCHAR (256)   NOT NULL,
    [MobileAlias]      NVARCHAR (16)    CONSTRAINT [DF__aspnet_Us__Mobil__1F63A897] DEFAULT (NULL) NULL,
    [IsAnonymous]      BIT              CONSTRAINT [DF__aspnet_Us__IsAno__2057CCD0] DEFAULT ((0)) NOT NULL,
    [LastActivityDate] DATETIME         NOT NULL,
    CONSTRAINT [PK__aspnet_U__1788CC4DCA2B60ED] PRIMARY KEY NONCLUSTERED ([UserId] ASC),
    CONSTRAINT [FK__aspnet_Us__Appli__1D7B6025] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
);


GO
CREATE UNIQUE CLUSTERED INDEX [aspnet_Users_Index]
    ON [dbo].[aspnet_Users]([ApplicationId] ASC, [LoweredUserName] ASC);


GO
CREATE NONCLUSTERED INDEX [aspnet_Users_Index2]
    ON [dbo].[aspnet_Users]([ApplicationId] ASC, [LastActivityDate] ASC);

