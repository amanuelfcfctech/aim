CREATE TABLE [dbo].[SpecificProductsGroup] (
    [SpecificProductsGroupID]   INT           IDENTITY (1, 1) NOT NULL,
    [SpecificProductsGroupCode] NVARCHAR (50) NULL,
    [IsActive]                  BIT           CONSTRAINT [DF_SpecificProductsGroup_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]               DATETIME      CONSTRAINT [DF_SpecificProductsGroup_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                 INT           NULL,
    CONSTRAINT [PK_SpecificProductsGroup] PRIMARY KEY CLUSTERED ([SpecificProductsGroupID] ASC)
);

