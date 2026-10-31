CREATE TABLE [dbo].[CFCIngredientRefAIMMaterialLog] (
    [CFCIngredientRefAIMMaterialLogID] INT      IDENTITY (1, 1) NOT NULL,
    [CFCIngredientID]                  INT      NULL,
    [AIMMaterialID]                    INT      NULL,
    [LogStatus]                        CHAR (1) NULL,
    [UpdateStamp]                      DATETIME CONSTRAINT [DF_CFCIngredientRefAIMMaterialLog_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]                        INT      NULL,
    CONSTRAINT [PK_CFCIngredientRefAIMMaterialLog] PRIMARY KEY CLUSTERED ([CFCIngredientRefAIMMaterialLogID] ASC)
);

