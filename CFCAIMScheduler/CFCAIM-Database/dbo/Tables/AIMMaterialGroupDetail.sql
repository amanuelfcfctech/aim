CREATE TABLE [dbo].[AIMMaterialGroupDetail] (
    [AIMMaterialGroupDetailID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMMaterialGroupHeaderID] INT      NULL,
    [AIMMaterialID]            INT      NULL,
    [UpdateStamp]              DATETIME NULL,
    [CFCUserID]                INT      NULL,
    CONSTRAINT [PK_AIMMaterialGroupDetail] PRIMARY KEY CLUSTERED ([AIMMaterialGroupDetailID] ASC)
);

