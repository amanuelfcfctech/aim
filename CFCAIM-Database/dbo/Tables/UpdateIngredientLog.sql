CREATE TABLE [dbo].[UpdateIngredientLog] (
    [UpdateIngredientLogID]    INT             IDENTITY (1, 1) NOT NULL,
    [UpdateIngredientDetailID] INT             NULL,
    [PlantCode]                NVARCHAR (20)   NULL,
    [IngredientCode]           NVARCHAR (20)   NULL,
    [NutrientCode]             NVARCHAR (20)   NULL,
    [BeforeNutrientValue]      NUMERIC (12, 4) NULL,
    [AfterNutrientValue]       NUMERIC (12, 4) NULL,
    [IsOverlay]                BIT             NULL,
    [CreateStamp]              DATETIME        CONSTRAINT [DF_UpdateIngredientLog_CreateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                INT             NULL,
    [ErrorID]                  INT             NULL,
    CONSTRAINT [PK_UpdateIngredientLog] PRIMARY KEY CLUSTERED ([UpdateIngredientLogID] ASC)
);



