CREATE TABLE [dbo].[EventMonthlySpecificDays] (
    [EventMonthlySpecificDaysID] INT      IDENTITY (1, 1) NOT NULL,
    [EventMonthlyID]             INT      NULL,
    [DayID]                      INT      NULL,
    [UpdateStamp]                DATETIME CONSTRAINT [DF_EventMonthlySpecificDays_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventMonthlySpecificDays] PRIMARY KEY CLUSTERED ([EventMonthlySpecificDaysID] ASC)
);

