CREATE TABLE [dbo].[CFCProduct] (
    [CFCProductID]   INT          IDENTITY (1, 1) NOT NULL,
    [CFCPlantID]     INT          NULL,
    [CFCProductCode] VARCHAR (20) NULL,
    [CFCProductName] VARCHAR (40) NULL,
    [IsActive]       BIT          CONSTRAINT [DF_CFCProduct_IsActive] DEFAULT ((1)) NULL,
    [UpdateStamp]    DATETIME     CONSTRAINT [DF_CFCProduct_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]      INT          NULL,
    [AIMDatabaseID]  INT          NULL,
    CONSTRAINT [PK_CFCProduct] PRIMARY KEY CLUSTERED ([CFCProductID] ASC)
);






GO
CREATE NONCLUSTERED INDEX [CFCProduct_CFCProductCode]
    ON [dbo].[CFCProduct]([CFCProductCode] ASC);


GO
CREATE NONCLUSTERED INDEX [CFCProduct_CFCPlantID]
    ON [dbo].[CFCProduct]([CFCPlantID] ASC);

