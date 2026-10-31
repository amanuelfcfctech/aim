CREATE TABLE [dbo].[PNEForAnalyzerMaterialRefAnalyzerNutrient] (
    [PNEForAnalyzerMaterialRefAnalyzerNutrientID] INT      IDENTITY (1, 1) NOT NULL,
    [AnalyzerMaterialID]                          INT      NULL,
    [AnalyzerNutrientID]                          INT      NULL,
    [IsActive]                                    BIT      CONSTRAINT [DF_PNEForAnalyzerMaterialRefAnalyzerNutrient_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]                                 DATETIME CONSTRAINT [DF_PNEForAnalyzerMaterialRefAnalyzerNutrient_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                                   INT      CONSTRAINT [DF_PNEForAnalyzerMaterialRefAnalyzerNutrient_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_PNEForAnalyzerMaterialRefAnalyzerNutrient] PRIMARY KEY CLUSTERED ([PNEForAnalyzerMaterialRefAnalyzerNutrientID] ASC)
);

