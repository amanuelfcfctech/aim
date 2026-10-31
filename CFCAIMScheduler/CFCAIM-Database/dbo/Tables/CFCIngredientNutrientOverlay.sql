CREATE TABLE [dbo].[CFCIngredientNutrientOverlay] (
    [CFCIngredientNutrientOverlayID] INT             IDENTITY (1, 1) NOT NULL,
    [CFCPlantID]                     INT             NULL,
    [CFCIngredientID]                INT             NULL,
    [CFCNutrientID]                  INT             NULL,
    [NutrientValue]                  NUMERIC (12, 4) NULL,
    [UpdateStamp]                    DATETIME        CONSTRAINT [DF_CFCIngredientNutrientOverlay_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                      INT             CONSTRAINT [DF_CFCIngredientNutrientOverlay_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_CFCIngredientNutrientOverlay] PRIMARY KEY CLUSTERED ([CFCIngredientNutrientOverlayID] ASC)
);






GO


