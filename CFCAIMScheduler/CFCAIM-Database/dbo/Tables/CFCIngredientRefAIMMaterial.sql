CREATE TABLE [dbo].[CFCIngredientRefAIMMaterial] (
    [CFCIngredientRefAIMMaterialID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCIngredientID]               INT      NULL,
    [AIMMaterialID]                 INT      NULL,
    [UpdateStamp]                   DATETIME CONSTRAINT [DF_CFCIngredientRefAIMMaterial_UpdateStamp] DEFAULT (getdate()) NULL,
    [IsActive]                      BIT      CONSTRAINT [DF_CFCIngredientRefAIMMaterial_IsActive] DEFAULT ((1)) NULL,
    [CFCUserID]                     INT      NULL,
    CONSTRAINT [PK_CFCIngredientRefAIMMaterial] PRIMARY KEY CLUSTERED ([CFCIngredientRefAIMMaterialID] ASC)
);

