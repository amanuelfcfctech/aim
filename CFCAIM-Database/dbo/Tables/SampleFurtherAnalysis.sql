CREATE TABLE [dbo].[SampleFurtherAnalysis] (
    [SampleFurtherAnalysisID] INT      IDENTITY (1, 1) NOT NULL,
    [SampleID]                INT      NULL,
    [DataSourceTypeID]        INT      NULL,
    [ProcessDate]             DATETIME NULL,
    [UpdateStamp]             DATETIME CONSTRAINT [DF_SampleFurtherAnalysis_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT      NULL,
    [AIMMessageID]            INT      NULL,
    CONSTRAINT [PK_SampleFurtherAnalysis] PRIMARY KEY CLUSTERED ([SampleFurtherAnalysisID] ASC)
);





