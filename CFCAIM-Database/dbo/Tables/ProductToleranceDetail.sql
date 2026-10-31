CREATE TABLE [dbo].[ProductToleranceDetail] (
    [ProductToleranceDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [ProductToleranceHeaderID] INT      NULL,
    [AIMMaterialID]            INT      NULL,
    [UpdateStamp]              DATETIME NULL,
    [CFCUserID]                INT      NULL,
    CONSTRAINT [PK_ProductToleranceDetail] PRIMARY KEY CLUSTERED ([ProductToleranceDetailID] ASC)
);

