CREATE TABLE [dbo].[SampleExportDetail] (
    [SampleExportDetailID] INT          NOT NULL,
    [SampleExportHeaderID] INT          NOT NULL,
    [MaterialType]         CHAR (1)     NULL,
    [MaterialCode]         VARCHAR (20) NULL,
    [SampleDate]           DATETIME     NULL,
    [SampleCode]           VARCHAR (20) NULL,
    CONSTRAINT [PK_SampleExportDetail] PRIMARY KEY CLUSTERED ([SampleExportDetailID] ASC, [SampleExportHeaderID] ASC)
);

