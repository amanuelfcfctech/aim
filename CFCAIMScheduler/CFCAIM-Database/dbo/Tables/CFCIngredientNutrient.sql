CREATE TABLE [dbo].[CFCIngredientNutrient] (
    [CFCIngredientNutrientID] INT        IDENTITY (1, 1) NOT NULL,
    [CFCIngredientID]         INT        NULL,
    [CFCNutrientID]           INT        NULL,
    [Value]                   FLOAT (53) NULL,
    [UpdateStamp]             DATETIME   NULL,
    [IsComputed]              BIT        NULL,
    CONSTRAINT [PK_CFCIngredientNutrient] PRIMARY KEY CLUSTERED ([CFCIngredientNutrientID] ASC)
);






GO
CREATE NONCLUSTERED INDEX [CFCIngredientNutrient_CFCIngredientID_CFCNutrientID]
    ON [dbo].[CFCIngredientNutrient]([CFCIngredientID] ASC, [CFCNutrientID] ASC);

