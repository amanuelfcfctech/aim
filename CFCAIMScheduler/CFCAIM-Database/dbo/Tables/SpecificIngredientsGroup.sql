CREATE TABLE [dbo].[SpecificIngredientsGroup] (
    [SpecificIngredientsGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [SpecificIngredientsGroupCode] NVARCHAR (50) NULL,
    [IsActive]                     BIT           CONSTRAINT [DF_SpecificIngredientsGroup_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]                  DATETIME      CONSTRAINT [DF_SpecificIngredientsGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                    INT           NULL,
    CONSTRAINT [PK_SpecificIngredientsGroup] PRIMARY KEY CLUSTERED ([SpecificIngredientsGroupID] ASC)
);

