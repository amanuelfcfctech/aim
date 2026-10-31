CREATE TABLE [dbo].[EventType] (
    [EventTypeID]   INT           NOT NULL,
    [EventTypeName] NVARCHAR (50) NULL,
    [UpdateStamp]   DATETIME      CONSTRAINT [DF_EventType_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED ([EventTypeID] ASC)
);



