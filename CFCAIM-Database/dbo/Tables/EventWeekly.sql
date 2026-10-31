CREATE TABLE [dbo].[EventWeekly] (
    [EventWeeklyID] INT      IDENTITY (1, 1) NOT NULL,
    [EventID]       INT      NULL,
    [WeekID]        INT      NULL,
    [RecurEvery]    INT      NULL,
    [UpdateStamp]   DATETIME CONSTRAINT [DF_EventWeekly_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventWeekly] PRIMARY KEY CLUSTERED ([EventWeeklyID] ASC)
);



