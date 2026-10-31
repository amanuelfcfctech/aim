CREATE TABLE [dbo].[SampleImportUserDefinedMapSelection] (
    [SampleImportUserDefinedMapSelectionID] INT           IDENTITY (1, 1) NOT NULL,
    [Description]                           NVARCHAR (50) NULL,
    [IsActive]                              BIT           NULL,
    [UpdateStamp]                           DATETIME      NULL,
    [CFCUserID]                             INT           NULL,
    CONSTRAINT [PK_SampleImportUserDefinedMapSelection] PRIMARY KEY CLUSTERED ([SampleImportUserDefinedMapSelectionID] ASC)
);

