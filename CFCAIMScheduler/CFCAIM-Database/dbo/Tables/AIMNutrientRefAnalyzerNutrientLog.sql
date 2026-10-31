CREATE TABLE [dbo].[AIMNutrientRefAnalyzerNutrientLog] (
    [AIMNutrientRefAnalyzerNutrientLogID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMNutrientID]                       INT      NULL,
    [AnalyzerNutrientID]                  INT      NULL,
    [LogStatus]                           CHAR (1) NULL,
    [UpdateStamp]                         DATETIME CONSTRAINT [DF_AIMNutrientRefAnalyzerNutrientLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                           INT      NULL,
    CONSTRAINT [PK_AIMNutrientRefAnalyzerNutrientLog] PRIMARY KEY CLUSTERED ([AIMNutrientRefAnalyzerNutrientLogID] ASC)
);

