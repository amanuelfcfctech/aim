CREATE TABLE [dbo].[CFCIngredientFactoringNutrient] (
    [CFCIngredientFactoringNutrientID] INT      NOT NULL,
    [Nutrient]                         INT      NULL,
    [FactorNutrient]                   INT      NULL,
    [UpdateStamp]                      DATETIME CONSTRAINT [DF_CFCIngredientFactoringNutrient_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                        INT      CONSTRAINT [DF_CFCIngredientFactoringNutrient_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_CFCIngredientFactoringNutrient] PRIMARY KEY CLUSTERED ([CFCIngredientFactoringNutrientID] ASC)
);

