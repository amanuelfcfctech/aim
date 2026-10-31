CREATE TABLE [dbo].[aspnet_Applications] (
    [ApplicationName]        NVARCHAR (256)   NOT NULL,
    [LoweredApplicationName] NVARCHAR (256)   NOT NULL,
    [ApplicationId]          UNIQUEIDENTIFIER CONSTRAINT [DF__aspnet_Ap__Appli__1A9EF37A] DEFAULT (newid()) NOT NULL,
    [Description]            NVARCHAR (256)   NULL,
    CONSTRAINT [PK__aspnet_A__C93A4C985821F794] PRIMARY KEY NONCLUSTERED ([ApplicationId] ASC),
    CONSTRAINT [UQ__aspnet_A__17477DE4BE68EF4C] UNIQUE NONCLUSTERED ([LoweredApplicationName] ASC),
    CONSTRAINT [UQ__aspnet_A__30910331F4485396] UNIQUE NONCLUSTERED ([ApplicationName] ASC)
);


GO
CREATE CLUSTERED INDEX [aspnet_Applications_Index]
    ON [dbo].[aspnet_Applications]([LoweredApplicationName] ASC);

