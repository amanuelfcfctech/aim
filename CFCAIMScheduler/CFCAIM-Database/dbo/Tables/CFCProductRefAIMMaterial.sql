CREATE TABLE [dbo].[CFCProductRefAIMMaterial] (
    [CFCProductRefAIMMaterialID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCProductID]               INT      NULL,
    [AIMMaterialID]              INT      NULL,
    [UpdateStamp]                DATETIME CONSTRAINT [DF_CFCProductRefAIMProduct_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                  INT      NULL,
    CONSTRAINT [PK_CFCProductRefAIMProduct] PRIMARY KEY CLUSTERED ([CFCProductRefAIMMaterialID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [CFCProductRefAIMMaterial_CFCProductID]
    ON [dbo].[CFCProductRefAIMMaterial]([CFCProductID] ASC);


GO
CREATE NONCLUSTERED INDEX [CFCProductRefAIMMaterial_AIMMaterialID]
    ON [dbo].[CFCProductRefAIMMaterial]([AIMMaterialID] ASC);

