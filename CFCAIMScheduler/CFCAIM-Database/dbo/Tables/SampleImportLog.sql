CREATE TABLE [dbo].[SampleImportLog] (
    [SampleImportLogID]                 INT            IDENTITY (1, 1) NOT NULL,
    [SampleID]                          INT            NULL,
    [Message]                           NVARCHAR (100) NULL,
    [Status]                            INT            NULL,
    [CFCUserID]                         INT            NULL,
    [UpdateStamp]                       DATETIME       CONSTRAINT [DF_SampleImportLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [UserDefinedDataSourceDefinitionID] INT            NULL,
    [BatchID]                           INT            NULL,
    [SampleCode]                        NVARCHAR (20)  NULL,
    CONSTRAINT [PK_SampleImportLogID] PRIMARY KEY CLUSTERED ([SampleImportLogID] ASC)
);






















GO
CREATE NONCLUSTERED INDEX [SampleImportLog_SampleID]
    ON [dbo].[SampleImportLog]([SampleID] ASC);

