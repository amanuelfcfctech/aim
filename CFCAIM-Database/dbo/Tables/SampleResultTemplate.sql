CREATE TABLE [dbo].[SampleResultTemplate] (
    [SampleResultTemplateID]   INT           IDENTITY (1, 1) NOT NULL,
    [SampleResultTemplateName] NVARCHAR (50) NULL,
    [DataSourceTypeID]         INT           NULL,
    [AIMMaterialID]            INT           NULL,
    [UpdateStamp]              DATETIME      NULL,
    [CFCUserID]                INT           NULL,
    [CurrentIteration]         INT           NULL,
    [IsGroupTemplate]          INT           NULL,
    CONSTRAINT [PK_SampleResultTemplate] PRIMARY KEY CLUSTERED ([SampleResultTemplateID] ASC)
);

