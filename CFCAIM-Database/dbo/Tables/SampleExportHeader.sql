CREATE TABLE [dbo].[SampleExportHeader] (
    [SampleExportHeaderID] INT           IDENTITY (1, 1) NOT NULL,
    [ExportType]           INT           NULL,
    [ExportPath]           VARCHAR (100) NULL,
    CONSTRAINT [PK_SampleExportHeader] PRIMARY KEY CLUSTERED ([SampleExportHeaderID] ASC)
);

