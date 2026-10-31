CREATE TABLE [dbo].[AIMMaterialStockStatus] (
    [AIMMaterialStockStatusID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMMaterialID]            INT      NULL,
    [AIMOwnerLocationID]       INT      NULL,
    [UpdateStamp]              DATETIME CONSTRAINT [DF_AIMMaterialStockStatus_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                INT      NULL,
    CONSTRAINT [PK_AIMMaterialStockStatus] PRIMARY KEY CLUSTERED ([AIMMaterialStockStatusID] ASC)
);

