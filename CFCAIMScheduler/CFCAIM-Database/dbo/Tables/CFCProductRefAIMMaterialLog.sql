CREATE TABLE [dbo].[CFCProductRefAIMMaterialLog] (
    [CFCProductRefAIMMaterialLogID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCProductID]                  INT      NULL,
    [AIMMaterialID]                 INT      NULL,
    [LogStatus]                     CHAR (1) NULL,
    [UpdateStamp]                   DATETIME CONSTRAINT [DF_CFCProductRefAIMProductLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                     INT      NULL,
    CONSTRAINT [PK_CFCProductRefAIMProductLog] PRIMARY KEY CLUSTERED ([CFCProductRefAIMMaterialLogID] ASC)
);

