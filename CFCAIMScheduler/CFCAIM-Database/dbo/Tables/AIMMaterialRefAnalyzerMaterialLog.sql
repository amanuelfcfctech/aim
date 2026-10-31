CREATE TABLE [dbo].[AIMMaterialRefAnalyzerMaterialLog] (
    [AIMMaterialRefAnalyzerMaterialLogID] INT           IDENTITY (1, 1) NOT NULL,
    [AIMMaterialID]                       INT           NULL,
    [AnalyzerMaterialID]                  INT           NULL,
    [LogStatus]                           CHAR (1)      NULL,
    [UpdateStamp]                         DATETIME      CONSTRAINT [DF_AIMMaterialRefAnalyzerMaterialLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                           INT           NULL,
    [Comment]                             VARCHAR (250) NULL,
    [OLDanalyzerMaterialID]               INT           NULL,
    CONSTRAINT [PK_AIMMaterialRefAnalyzerMaterialLog] PRIMARY KEY CLUSTERED ([AIMMaterialRefAnalyzerMaterialLogID] ASC)
);



