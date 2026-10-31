CREATE TABLE [dbo].[Event] (
    [EventID]          INT           IDENTITY (1, 1) NOT NULL,
    [EventName]        NVARCHAR (50) NULL,
    [EventRunTypeID]   INT           NULL,
    [EventTypeID]      INT           NULL,
    [TemplateID]       INT           NULL,
    [EventDescription] VARCHAR (100) NULL,
    [StartDateTime]    DATETIME      NULL,
    [EventStatus]      BIT           NULL,
    [UpdateStamp]      DATETIME      CONSTRAINT [DF_Event_UpdateStamp] DEFAULT (getdate()) NULL,
    [CFCUserID]        INT           NULL,
    [LastRunDateTime]  DATETIME      NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([EventID] ASC)
);





