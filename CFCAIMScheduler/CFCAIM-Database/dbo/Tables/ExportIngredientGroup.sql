CREATE TABLE [dbo].[ExportIngredientGroup] (
    [ExportIngredientGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [ExportIngredientGroupCode] NVARCHAR (50) NULL,
    [AIMMaterialID]             INT           NULL,
    [IsActive]                  BIT           CONSTRAINT [DF_ExportIngredientGroup_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]               DATETIME      CONSTRAINT [DF_ExportIngredientGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                 INT           NULL,
    CONSTRAINT [PK_ExportIngredientGroup] PRIMARY KEY CLUSTERED ([ExportIngredientGroupID] ASC)
);

