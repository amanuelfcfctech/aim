CREATE TABLE [dbo].[UpdateIngredientDetail] (
    [UpdateIngredientDetailID] INT             IDENTITY (1, 1) NOT NULL,
    [UpdateIngredientHeaderID] INT             NULL,
    [CFCIngredientID]          INT             NULL,
    [CFCNutrientID]            INT             NULL,
    [ProposedValue]            NUMERIC (12, 4) NULL,
    [CreateStamp]              DATETIME        CONSTRAINT [DF_UpdateIngredientDetail_CreateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                INT             NULL,
    [UpdateStamp]              DATETIME        NULL,
    [UpdateCFCUserID]          INT             NULL,
    [Override]                 NUMERIC (12, 4) CONSTRAINT [DF_UpdateIngredientDetail_Override] DEFAULT ((0)) NULL,
    [Mean]                     NUMERIC (12, 4) NULL,
    [StandardDeviation]        NUMERIC (12, 4) NULL,
    [Status]                   INT             NULL,
    [CFCPlantID]               INT             NULL,
    [NumberOfSamples]          INT             NULL,
    [IsOverlay]                BIT             NULL,
    CONSTRAINT [PK_UpdateIngredientDetail] PRIMARY KEY CLUSTERED ([UpdateIngredientDetailID] ASC)
);





