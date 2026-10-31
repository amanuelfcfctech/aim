CREATE TABLE [dbo].[CFCNutrientRefAIMNutrientLog] (
    [CFCNutrientRefAIMNutrientLogID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCNutrientID]                  INT      NULL,
    [AIMNutrientID]                  INT      NULL,
    [LogStatus]                      CHAR (1) NULL,
    [UpdateStamp]                    DATETIME CONSTRAINT [DF_CFCNutrientRefAIMNutrientLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                      INT      NULL,
    CONSTRAINT [PK_CFCNutrientRefAIMNutrientLog] PRIMARY KEY CLUSTERED ([CFCNutrientRefAIMNutrientLogID] ASC)
);

