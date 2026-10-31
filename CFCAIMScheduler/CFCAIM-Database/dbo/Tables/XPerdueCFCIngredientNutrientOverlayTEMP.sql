CREATE TABLE [dbo].[XPerdueCFCIngredientNutrientOverlayTEMP] (
    [CFCIngredientNutrientOverlayTEMPID] INT             IDENTITY (1, 1) NOT NULL,
    [AIMDatabaseID]                      INT             NULL,
    [CFCPlantCode]                       VARCHAR (20)    NULL,
    [CFCIngredientCode]                  VARCHAR (20)    NULL,
    [CFCNutrientCode]                    VARCHAR (20)    NULL,
    [Value]                              NUMERIC (12, 4) NULL,
    [UpdateStamp]                        DATETIME        CONSTRAINT [DF_CFCIngredientNutrientOverlayTEMP_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                          INT             CONSTRAINT [DF_CFCIngredientNutrientOverlayTEMP_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_CFCIngredientNutrientOverlayTEMP] PRIMARY KEY CLUSTERED ([CFCIngredientNutrientOverlayTEMPID] ASC)
);

