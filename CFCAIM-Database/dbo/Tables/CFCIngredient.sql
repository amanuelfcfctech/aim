CREATE TABLE [dbo].[CFCIngredient] (
    [CFCIngredientID]   INT           IDENTITY (1, 1) NOT NULL,
    [CFCIngredientCode] NVARCHAR (20) NULL,
    [CFCIngredientName] NVARCHAR (40) NULL,
    [IsActive]          BIT           CONSTRAINT [DF_CFCIngredient_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]       DATETIME      NULL,
    [AIMDatabaseID]     INT           NULL,
    [CFCUserID]         INT           NULL,
    [MathType]          INT           NULL,
    CONSTRAINT [PK_CFCIngredient] PRIMARY KEY CLUSTERED ([CFCIngredientID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [CFCIngredient_AIMDatabaseIDCFCIngredientCode]
    ON [dbo].[CFCIngredient]([AIMDatabaseID] ASC, [CFCIngredientCode] ASC);

