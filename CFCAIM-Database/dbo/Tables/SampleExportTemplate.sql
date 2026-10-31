CREATE TABLE [dbo].[SampleExportTemplate] (
    [SampleExportTemplateID] INT            IDENTITY (1, 1) NOT NULL,
    [TemplateName]           NVARCHAR (100) NULL,
    [FileTypeID]             INT            NULL,
    [DelimiterTypeID]        INT            NULL,
    [ExportPath]             NVARCHAR (100) NULL,
    [ExportFileName]         NVARCHAR (50)  NULL,
    [UpdateStamp]            DATETIME       CONSTRAINT [DF_SampleExportTemplate_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]              INT            CONSTRAINT [DF_SampleExportTemplate_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_SampleExportTemplate] PRIMARY KEY CLUSTERED ([SampleExportTemplateID] ASC)
);

