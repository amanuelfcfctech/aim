CREATE TABLE [dbo].[QueryOperation] (
    [QueryOperationID] INT            IDENTITY (1, 1) NOT NULL,
    [Operation]        NVARCHAR (20)  NULL,
    [IsActive]         BIT            CONSTRAINT [DF_QueryOperation_IsActive] DEFAULT ((0)) NULL,
    [UpdateStamp]      DATETIME       CONSTRAINT [DF_QueryOperation_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]        INT            CONSTRAINT [DF_QueryOperation_CFCUserID] DEFAULT ((0)) NULL,
    [Command]          NVARCHAR (100) NULL,
    CONSTRAINT [PK_QueryOperation] PRIMARY KEY CLUSTERED ([QueryOperationID] ASC)
);

