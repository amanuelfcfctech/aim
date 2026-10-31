CREATE TABLE [dbo].[SpecificIngredients] (
    [SpecificIngredientsID]      INT      IDENTITY (1, 1) NOT NULL,
    [SpecificIngredientsGroupID] INT      NULL,
    [AIMMaterialID]              INT      NULL,
    [IsActive]                   BIT      CONSTRAINT [DF_SpecificIngredients_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]                DATETIME CONSTRAINT [DF_SpecificIngredients_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                  INT      NULL,
    CONSTRAINT [PK_SpecificIngredients] PRIMARY KEY CLUSTERED ([SpecificIngredientsID] ASC)
);



