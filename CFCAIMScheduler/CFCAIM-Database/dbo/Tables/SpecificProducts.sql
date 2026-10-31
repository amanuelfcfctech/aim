CREATE TABLE [dbo].[SpecificProducts] (
    [SpecificProductsID]      INT      IDENTITY (1, 1) NOT NULL,
    [SpecificProductsGroupID] INT      NULL,
    [AIMMaterialID]           INT      NULL,
    [IsActive]                BIT      CONSTRAINT [DF_SpecificProducts_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]             DATETIME CONSTRAINT [DF_SpecificProducts_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]               INT      NULL,
    CONSTRAINT [PK_SpecificProducts] PRIMARY KEY CLUSTERED ([SpecificProductsID] ASC)
);

