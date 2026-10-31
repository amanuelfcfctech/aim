CREATE TABLE [dbo].[AIMOwnerLocationRefAnalyzerOwnerLocationLog] (
    [AIMOwnerLocationRefAnalyzerOwnerLocationLogID] INT      IDENTITY (1, 1) NOT NULL,
    [AnalyzerOwnerID]                               INT      NULL,
    [AnalyzerOwnerLocationID]                       INT      NULL,
    [AIMOwnerID]                                    INT      NULL,
    [AIMOwnerLocationID]                            INT      NULL,
    [LogStatus]                                     CHAR (1) NULL,
    [UpdateStamp]                                   DATETIME CONSTRAINT [DF_AIMOwnerLocationRefAnalyzerOwnerLocationLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                                     INT      NULL,
    CONSTRAINT [PK_AIMOwnerLocationRefAnalyzerOwnerLocationLog] PRIMARY KEY CLUSTERED ([AIMOwnerLocationRefAnalyzerOwnerLocationLogID] ASC)
);

