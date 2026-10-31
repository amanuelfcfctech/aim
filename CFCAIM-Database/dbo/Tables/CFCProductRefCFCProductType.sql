CREATE TABLE [dbo].[CFCProductRefCFCProductType] (
    [CFCProductRefCFCProductTypeID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCProductID]                  INT      NULL,
    [CFCProductTypeID]              INT      NULL,
    [UpdateStamp]                   DATETIME NULL,
    [CFCUserID]                     INT      NULL,
    [AIMDatabaseID]                 INT      NULL,
    CONSTRAINT [PK_CFCProductRefCFCProductType] PRIMARY KEY CLUSTERED ([CFCProductRefCFCProductTypeID] ASC)
);



