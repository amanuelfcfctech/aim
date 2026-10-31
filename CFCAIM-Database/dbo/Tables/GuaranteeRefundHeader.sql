CREATE TABLE [dbo].[GuaranteeRefundHeader] (
    [GuaranteeRefundHeaderID] INT      IDENTITY (1, 1) NOT NULL,
    [BatchID]                 INT      NULL,
    [BatchSentDate]           DATETIME NULL,
    [AIMSupplierID]           INT      NULL,
    [AIMSupplierLocationID]   INT      NULL,
    [AIMMaterialID]           INT      NULL,
    [UpdateStamp]             DATETIME NULL,
    [CFCUserID]               INT      NULL,
    CONSTRAINT [PK_GuaranteeRefundHeader] PRIMARY KEY CLUSTERED ([GuaranteeRefundHeaderID] ASC)
);

