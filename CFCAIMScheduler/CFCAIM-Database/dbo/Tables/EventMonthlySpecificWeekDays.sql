CREATE TABLE [dbo].[EventMonthlySpecificWeekDays] (
    [EventMonthlySpecificWeekDaysID] INT      IDENTITY (1, 1) NOT NULL,
    [EventMonthlyID]                 INT      NULL,
    [WeekID]                         INT      NULL,
    [UpdateStamp]                    DATETIME CONSTRAINT [DF_EventMonthlySpecificWeekDays_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventMonthlySpecificWeekDays] PRIMARY KEY CLUSTERED ([EventMonthlySpecificWeekDaysID] ASC)
);

