CREATE TABLE [dbo].[AIMMaterialLocation] (
    [AIMMaterialLocationID] INT      IDENTITY (1, 1) NOT NULL,
    [AIMMaterialID]         INT      NULL,
    [AIMOwnerLocationID]    INT      NULL,
    [UpdateStamp]           DATETIME CONSTRAINT [DF_AIMMaterialLocation_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]             INT      NULL,
    CONSTRAINT [PK_AIMMaterialLocation] PRIMARY KEY CLUSTERED ([AIMMaterialLocationID] ASC)
);

