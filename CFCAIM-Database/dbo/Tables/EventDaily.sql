CREATE TABLE [dbo].[EventDaily] (
    [EventDailyID] INT      IDENTITY (1, 1) NOT NULL,
    [EventID]      INT      NULL,
    [DayID]        INT      NULL,
    [RecurEvery]   INT      NULL,
    [UpdateStamp]  DATETIME CONSTRAINT [DF_EventDaily_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventDaily] PRIMARY KEY CLUSTERED ([EventDailyID] ASC)
);

