CREATE TABLE [dbo].[QueryTemplateDetail] (
    [QueryTemplateDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [QueryTemplateID]       INT      NULL,
    [QueryFieldID]          INT      NULL,
    [QueryOperationID]      INT      NULL,
    [Value]                 INT      NULL,
    [UpdateStamp]           DATETIME CONSTRAINT [DF_QueryTemplateDetail_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]             INT      CONSTRAINT [DF_QueryTemplateDetail_CFCUserID] DEFAULT ((0)) NULL,
    [IsAnd]                 BIT      CONSTRAINT [DF_QueryTemplateDetail_IsAnd] DEFAULT ((0)) NULL,
    [DateValue]             DATE     NULL,
    [SecondDate]            DATE     NULL,
    [SecondValue]           INT      NULL,
    [OrderBy]               INT      NULL,
    CONSTRAINT [PK_QueryTemplateDetail] PRIMARY KEY CLUSTERED ([QueryTemplateDetailID] ASC)
);





