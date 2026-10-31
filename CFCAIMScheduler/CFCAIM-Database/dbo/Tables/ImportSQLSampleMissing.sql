CREATE TABLE [dbo].[ImportSQLSampleMissing] (
    [ImportSQLSampleMissingID] INT           IDENTITY (1, 1) NOT NULL,
    [ImportSQLSampleLogID]     INT           NULL,
    [MissingSampleCode]        NVARCHAR (20) NULL,
    [CreatedStamp]             DATETIME      NULL,
    [CFCUserID]                INT           NULL,
    CONSTRAINT [PK_ImportSQLSampleMissing] PRIMARY KEY CLUSTERED ([ImportSQLSampleMissingID] ASC)
);

