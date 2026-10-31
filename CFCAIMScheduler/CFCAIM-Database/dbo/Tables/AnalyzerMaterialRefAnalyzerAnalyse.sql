CREATE TABLE [dbo].[AnalyzerMaterialRefAnalyzerAnalyse] (
    [AnalyzerMaterialRefAnalyzerAnalyseID] INT      IDENTITY (1, 1) NOT NULL,
    [AnalyzerMaterialID]                   INT      NULL,
    [AnalyzerAnalyseID]                    INT      NULL,
    [UpdateStamp]                          DATETIME CONSTRAINT [DF_AnalyzerMaterialRefAnalyzerAnalyse_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                            INT      NULL,
    CONSTRAINT [PK_AnalyzerMaterialRefAnalyzerAnalyse] PRIMARY KEY CLUSTERED ([AnalyzerMaterialRefAnalyzerAnalyseID] ASC)
);

