CREATE TABLE [dbo].[EventDay] (
    [EventDayID]  INT      IDENTITY (1, 1) NOT NULL,
    [EventID]     INT      NULL,
    [DayID]       INT      NULL,
    [UpdateStamp] DATETIME CONSTRAINT [DF_EventDay_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventDay] PRIMARY KEY CLUSTERED ([EventDayID] ASC)
);

