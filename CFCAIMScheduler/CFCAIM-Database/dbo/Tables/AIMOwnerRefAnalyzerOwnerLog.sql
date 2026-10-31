CREATE TABLE [dbo].[AIMOwnerRefAnalyzerOwnerLog] (
    [AIMOwnerRefAnalyzerOwnerLogID] INT      IDENTITY (1, 1) NOT NULL,
    [AnalyzerOwnerID]               INT      NULL,
    [AIMOwnerID]                    INT      NULL,
    [LogStatus]                     CHAR (1) NULL,
    [UpdateStamp]                   DATETIME CONSTRAINT [DF_AIMOwnerRefAnalyzerOwnerLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                     INT      NULL,
    CONSTRAINT [PK_AIMOwnerRefAnalyzerOwnerLog] PRIMARY KEY CLUSTERED ([AIMOwnerRefAnalyzerOwnerLogID] ASC)
);

