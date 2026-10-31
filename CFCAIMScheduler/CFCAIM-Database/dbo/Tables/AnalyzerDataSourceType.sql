CREATE TABLE [dbo].[AnalyzerDataSourceType] (
    [AnalyzerDataSourceTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [AnalyzerDataSourceTypeName] VARCHAR (50)  NULL,
    [DataSourceTypeID]           INT           NULL,
    [IsActive]                   BIT           NULL,
    [UpdateStamp]                DATETIME      CONSTRAINT [DF_AnalysisDataSourceType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                  INT           NULL,
    [AnalyzerDataSourceTypeCode] NVARCHAR (20) NULL,
    CONSTRAINT [PK_AnalysisDataSourceType] PRIMARY KEY CLUSTERED ([AnalyzerDataSourceTypeID] ASC)
);

