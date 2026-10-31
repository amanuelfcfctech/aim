CREATE TABLE [dbo].[CFCProductType] (
    [CFCProductTypeID]  INT          IDENTITY (1, 1) NOT NULL,
    [ProductTypeNumber] INT          NOT NULL,
    [ProductTypeName]   VARCHAR (40) NULL,
    [AIMDatabaseID]     INT          NULL,
    [UpdateStamp]       DATETIME     NULL,
    [CFCUserID]         INT          NULL,
    CONSTRAINT [PK_CFCProductType] PRIMARY KEY CLUSTERED ([CFCProductTypeID] ASC)
);



