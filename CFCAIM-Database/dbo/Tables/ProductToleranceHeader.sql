CREATE TABLE [dbo].[ProductToleranceHeader] (
    [ProductToleranceHeaderID]   INT           IDENTITY (1, 1) NOT NULL,
    [ProductToleranceHeaderName] NVARCHAR (50) NULL,
    [AIMMaterialID]              INT           NULL,
    [AIMMaterialCode]            NVARCHAR (20) NULL,
    [UpdateStamp]                DATETIME      NULL,
    [CFCUserID]                  INT           NULL,
    [SpecFrom]                   INT           NULL,
    CONSTRAINT [PK_ProductToleranceHeader] PRIMARY KEY CLUSTERED ([ProductToleranceHeaderID] ASC)
);

