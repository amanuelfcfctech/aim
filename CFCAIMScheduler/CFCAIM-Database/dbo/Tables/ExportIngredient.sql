CREATE TABLE [dbo].[ExportIngredient] (
    [ExportIngredientID]      INT      IDENTITY (1, 1) NOT NULL,
    [ExportIngredientGroupID] INT      NULL,
    [CFCIngredientID]         INT      NULL,
    [IsActive]                BIT      CONSTRAINT [DF_ExportIngredient_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]             DATETIME CONSTRAINT [DF_ExportIngredient_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT      NULL,
    CONSTRAINT [PK_ExportIngredient] PRIMARY KEY CLUSTERED ([ExportIngredientID] ASC)
);

