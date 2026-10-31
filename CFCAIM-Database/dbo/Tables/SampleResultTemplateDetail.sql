CREATE TABLE [dbo].[SampleResultTemplateDetail] (
    [SampleResultTemplateDetailID] INT             IDENTITY (1, 1) NOT NULL,
    [SampleResultTemplateID]       INT             NULL,
    [AnalyzerNutrientID]           INT             NULL,
    [SortOrder]                    INT             NULL,
    [UpdateStamp]                  DATETIME        NULL,
    [CFCUserID]                    INT             NULL,
    [ResultValue]                  NUMERIC (18, 8) NULL,
    [SampleID]                     INT             NULL,
    [ResultValueString]            VARCHAR (15)    NULL,
    CONSTRAINT [PK_SampleResultTemplateDetail] PRIMARY KEY CLUSTERED ([SampleResultTemplateDetailID] ASC)
);





