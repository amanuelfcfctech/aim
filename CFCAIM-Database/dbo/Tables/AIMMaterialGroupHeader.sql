CREATE TABLE [dbo].[AIMMaterialGroupHeader] (
    [AIMMaterialGroupHeaderID]   INT           IDENTITY (1, 1) NOT NULL,
    [AIMMaterialGroupHeaderName] NVARCHAR (50) NULL,
    [AIMMaterialID]              INT           NULL,
    [AIMMaterialCode]            NVARCHAR (20) NULL,
    [MaterialTypeID]             INT           NULL,
    [UpdateStamp]                DATETIME      NULL,
    [CFCUserID]                  INT           NULL,
    [SpecFrom]                   INT           NULL,
    CONSTRAINT [PK_AIMMaterialGroupHeader] PRIMARY KEY CLUSTERED ([AIMMaterialGroupHeaderID] ASC)
);







