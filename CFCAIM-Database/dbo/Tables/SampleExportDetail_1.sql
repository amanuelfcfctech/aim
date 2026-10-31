CREATE TABLE [dbo].[SampleExportDetail] (
    [SampleExportDetailID] INT           IDENTITY (1, 1) NOT NULL,
    [SampleExportID]       INT           NULL,
    [MaterialType]         NVARCHAR (30) NULL,
    [MaterialCode]         NVARCHAR (30) NULL,
    [SampleDate]           DATETIME      NULL,
    [SampleCode]           NVARCHAR (50) NULL,
    [UpdateStamp]          DATETIME      NULL,
    [CFCUserID]            INT           NULL,
    CONSTRAINT [PK_SampleExportDetail] PRIMARY KEY CLUSTERED ([SampleExportDetailID] ASC)
);

