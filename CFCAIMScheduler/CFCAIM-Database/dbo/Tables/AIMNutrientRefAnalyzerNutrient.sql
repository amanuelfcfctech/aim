CREATE TABLE [dbo].[AIMNutrientRefAnalyzerNutrient] (
    [AIMNutrientRefAnalyzerNutrientID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMNutrientID]                    INT      NULL,
    [AnalyzerNutrientID]               INT      NULL,
    [UpdateStamp]                      DATETIME CONSTRAINT [DF_AIMNutrientRefAnalyzerNutrient_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                        INT      NULL,
    CONSTRAINT [PK_AIMNutrientRefAnalyzerNutrient] PRIMARY KEY CLUSTERED ([AIMNutrientRefAnalyzerNutrientID] ASC)
);

