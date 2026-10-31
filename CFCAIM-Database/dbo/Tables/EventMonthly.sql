CREATE TABLE [dbo].[EventMonthly] (
    [EventMonthlyID] INT      IDENTITY (1, 1) NOT NULL,
    [EventID]        INT      NULL,
    [MonthID]        INT      NULL,
    [SpecificDays]   BIT      NULL,
    [SpecificWeek]   BIT      NULL,
    [UpdateStamp]    DATETIME CONSTRAINT [DF_EventMonthly_UpdateStamp] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_EventMonthly] PRIMARY KEY CLUSTERED ([EventMonthlyID] ASC)
);



