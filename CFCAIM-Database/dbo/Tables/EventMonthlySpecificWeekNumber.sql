CREATE TABLE [dbo].[EventMonthlySpecificWeekNumber] (
    [EventMonthlySpecificWeekNumberID] INT      IDENTITY (1, 1) NOT NULL,
    [EventMonthlyID]                   INT      NULL,
    [WeekNumberID]                     INT      NULL,
    [UpdateStamp]                      DATETIME CONSTRAINT [DF_EventMonthlySpecificWeekNumber_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventMonthlySpecificWeekNumber] PRIMARY KEY CLUSTERED ([EventMonthlySpecificWeekNumberID] ASC)
);

