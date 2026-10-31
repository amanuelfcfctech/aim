CREATE TABLE [dbo].[APIPendingAIMMaterial] (
    [APIPendingAIMMaterialID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMMaterialID]           INT      NULL,
    [DataSourceTypeID]        INT      NULL,
    [IsActive]                BIT      NULL,
    [UpdateStamp]             DATETIME NULL,
    [CFCUserID]               INT      NULL,
    CONSTRAINT [PK_APIPendingAIMMaterial] PRIMARY KEY CLUSTERED ([APIPendingAIMMaterialID] ASC)
);

