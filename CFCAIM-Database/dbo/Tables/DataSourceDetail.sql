CREATE TABLE [dbo].[DataSourceDetail] (
    [DataSourceDetailID]       INT      IDENTITY (1, 1) NOT NULL,
    [DataSourceHeaderID]       INT      NULL,
    [DataSourceTypeID]         INT      NULL,
    [AnalyzerDataSourceTypeID] INT      NULL,
    [UpdateStamp]              DATETIME CONSTRAINT [DF_AnalysisDataSourceTypeRefDataSourceType_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                INT      NULL,
    CONSTRAINT [PK_DataSourceDetail] PRIMARY KEY CLUSTERED ([DataSourceDetailID] ASC)
);

