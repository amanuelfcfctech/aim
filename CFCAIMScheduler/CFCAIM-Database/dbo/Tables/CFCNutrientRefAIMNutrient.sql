CREATE TABLE [dbo].[CFCNutrientRefAIMNutrient] (
    [CFCNutrientRefAIMNutrientID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCNutrientID]               INT      NULL,
    [AIMNutrientID]               INT      NULL,
    [UpdateStamp]                 DATETIME CONSTRAINT [DF_CFCNutrientRefAIMNutrient_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                   INT      NULL,
    CONSTRAINT [PK_CFCNutrientRefAIMNutrient] PRIMARY KEY CLUSTERED ([CFCNutrientRefAIMNutrientID] ASC)
);

