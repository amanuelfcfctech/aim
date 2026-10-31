CREATE TABLE [dbo].[QueryFieldRefQueryOperation] (
    [QueryFieldRefQueryOperationID] INT      IDENTITY (1, 1) NOT NULL,
    [QueryFieldID]                  INT      NULL,
    [QueryOperationID]              INT      NULL,
    [CreateStamp]                   DATETIME CONSTRAINT [DF_QueryFieldRefQueryOperation_CreateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                     INT      CONSTRAINT [DF_QueryFieldRefQueryOperation_CFCUserID] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_QueryFieldRefQueryOperation] PRIMARY KEY CLUSTERED ([QueryFieldRefQueryOperationID] ASC)
);

