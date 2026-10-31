CREATE TABLE [dbo].[AIMMaterialRefAnalyzerMaterial] (
    [AIMMaterialRefAnalyzerMaterialID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMMaterialID]                    INT      NULL,
    [AnalyzerMaterialID]               INT      NULL,
    [UpdateStamp]                      DATETIME CONSTRAINT [DF_AIMMaterialRefAnalyzerMaterial_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                        INT      NULL,
    CONSTRAINT [PK_AIMMaterialRefAnalyzerMaterial] PRIMARY KEY CLUSTERED ([AIMMaterialRefAnalyzerMaterialID] ASC)
);

