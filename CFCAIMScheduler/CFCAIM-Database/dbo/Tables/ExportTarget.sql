CREATE TABLE [dbo].[ExportTarget] (
    [ExportTargetID] INT           NOT NULL,
    [Name]           NVARCHAR (30) NULL,
    [CFCUserID]      INT           CONSTRAINT [DF_ExportTarget_CFCUserID] DEFAULT ((0)) NULL,
    [UpdateStamp]    DATETIME      CONSTRAINT [DF_ExportTarget_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_ExportTarget] PRIMARY KEY CLUSTERED ([ExportTargetID] ASC)
);

