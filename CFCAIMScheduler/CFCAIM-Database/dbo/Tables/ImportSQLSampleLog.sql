CREATE TABLE [dbo].[ImportSQLSampleLog] (
    [ImportSQLSampleLogID] INT      IDENTITY (1, 1) NOT NULL,
    [ImportConfigID]       INT      NULL,
    [LastImportDate]       DATETIME NULL,
    [CreatedStamp]         DATETIME NULL,
    [CFCUserID]            INT      NULL,
    CONSTRAINT [PK_ImportSQLSampleLog] PRIMARY KEY CLUSTERED ([ImportSQLSampleLogID] ASC)
);

