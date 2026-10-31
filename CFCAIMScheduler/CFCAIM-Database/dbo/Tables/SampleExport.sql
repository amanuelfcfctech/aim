CREATE TABLE [dbo].[SampleExport] (
    [SampleExportID]   INT            IDENTITY (1, 1) NOT NULL,
    [SampleExportName] NVARCHAR (100) NULL,
    [SampleExportType] INT            NULL,
    [SampleExportPath] NVARCHAR (100) NULL,
    [UpdateStamp]      DATETIME       CONSTRAINT [DF_SampleExport_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]        INT            CONSTRAINT [DF_SampleExport_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_SampleExportHeader] PRIMARY KEY CLUSTERED ([SampleExportID] ASC)
);

