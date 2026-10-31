CREATE TABLE [dbo].[AnalyzerPendingSample] (
    [AnalyzerPendingSampleID] INT           IDENTITY (1, 1) NOT NULL,
    [SampleID]                INT           NOT NULL,
    [CreatedStamp]            DATETIME      CONSTRAINT [DF_AnalyzerPendingSample_CreatedStamp] DEFAULT (getdate()) NULL,
    [SentStamp]               DATETIME      NULL,
    [RequestCode]             NVARCHAR (50) NULL,
    [ReceivedStamp]           DATETIME      NULL,
    [SpectraName]             NVARCHAR (50) NULL,
    [NIRAnalysis]             DATETIME      NULL,
    [SampleFurtherAnalysisID] INT           NULL,
    CONSTRAINT [PK_AnalyzerPendingSample] PRIMARY KEY CLUSTERED ([AnalyzerPendingSampleID] ASC)
);



